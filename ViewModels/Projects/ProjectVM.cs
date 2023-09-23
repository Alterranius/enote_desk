using eNote_desk.APIService;
using eNote_desk.Models;
using eNote_desk.ViewModels.Consts;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace eNote_desk.ViewModels.Projects
{
    public class ProjectVM : BindableBase, INotifyPropertyChanged
    {
        #region Properties
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _prefix;
        public string Prefix
        {
            get { return _prefix; }
            set { SetProperty(ref _prefix, value); }
        }
        private string _specialization;
        public string Specialization
        {
            get { return _specialization; }
            set { SetProperty(ref _specialization, value); }
        }
        private string _mission;
        public string Mission
        {
            get { return _mission; }
            set { SetProperty(ref _mission, value); }
        }
        private string _product;
        public string Product
        {
            get { return _product; }
            set { SetProperty(ref _product, value); }
        }
        private List<Project> _projects;
        public List<Project> Projects
        {
            get { return _projects; }
            set { SetProperty(ref _projects, value); }
        }
        private DateTime _deadline;
        public DateTime Deadline
        {
            get { return _deadline; }
            set { SetProperty(ref _deadline, value); }
        }
        private DateTime _hardDeadline;
        public DateTime HardDeadline
        {
            get { return _hardDeadline; }
            set { SetProperty(ref _hardDeadline, value); }
        }
        private Methodology _methodology;
        public Methodology Methodology
        {
            get { return _methodology; }
            set { SetProperty(ref _methodology, value); }
        }
        private Project _selectedProject;
        public Project SelectedProject
        {
            get { return _selectedProject; }
            set { SetProperty(ref _selectedProject, value); }
        }
        private List<Methodology> _methodologiesCmb;
        public List<Methodology> MethodologiesCmb
        {
            get { return _methodologiesCmb; }
            set { SetProperty(ref _methodologiesCmb, value); }
        }
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
        private string _token;
        public string Token
        {
            get { return _token; }
            set { SetProperty(ref _token, value); }
        }
        #endregion
        #region ICommands
        public DelegateCommand PostProjectAddClick { get; set; }
        public DelegateCommand<Project> DeleteProjectRemoveClick { get; set; }
        public DelegateCommand<Project> PutProjectUpdateClick { get; set; }
        public DelegateCommand GetProjectsLoad { get; set; }
        public DelegateCommand<Project> DeleteProjectLeaveClick { get; set; }
        #endregion
        #region Constructor
        public ProjectVM(string token)
        {
            SelectedProject = new Project();
            SelectedProject.Deadline = DateTime.Now;
            SelectedProject.HardDeadline = DateTime.Now;
            PostProjectAddClick = new DelegateCommand(AddProject);
            DeleteProjectRemoveClick = new DelegateCommand<Project>(RemoveProject);
            PutProjectUpdateClick = new DelegateCommand<Project>(UpdateProject);
            GetProjectsLoad = new DelegateCommand(GetProjects);
            Token = token;
            GetMethodologies();
        }
        public ProjectVM(string token, Project project)
        {
            DeleteProjectRemoveClick = new DelegateCommand<Project>(RemoveProject);
            PutProjectUpdateClick = new DelegateCommand<Project>(UpdateProject);
            Token = token;
            SelectedProject = project;
            GetMethodologies();
        }
        public ProjectVM(string token, bool my)
        {
            SelectedProject = new Project();
            GetProjectsLoad = new DelegateCommand(GetProjects);
            DeleteProjectLeaveClick = new DelegateCommand<Project>(LeaveProject);
            PutProjectUpdateClick = new DelegateCommand<Project>(UpdateProject);
            Token = token;
            GetProjects();
            GetMethodologies();
        }
        #endregion
        #region Service
        private void AddProject()
        {
            if (!string.IsNullOrEmpty(SelectedProject.Error))
            {
                return;
            }
            SelectedProject.CreatedAt = DateTime.Now;
            try
            {
                var response = WebAPI.PostCall(URIs.PROJECT, SelectedProject, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    Message = "Успешно добавлено";
                    eNote_desk.Wins.ProjectDetails.Performed();
                }
                else
                {
                    Message = "Ошибка добавления";
                }
            }
            catch (Exception e)
            {
                Message = "Нет подключения";
            }
            MessageBox.Show(Message);
        }
        private void RemoveProject(Project project)
        {
            try
            {
                var response = WebAPI.DeleteCall(URIs.PROJECT + "/" + project.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Message = "Успешно удалено";
                }
                else
                {
                    Message = "Ошибка удаления";
                }
            }
            catch (Exception e)
            {
                Message = "Нет подключения";
            }
            MessageBox.Show(Message);
        }

        private void LeaveProject(Project project)
        {
            try
            {
                var response = WebAPI.PutCall(URIs.PROJECT_LEAVE + "/" + project.Id, project, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Message = "Вы покинули проект " + project.Name;
                    GetProjects();
                }
                else
                {
                    Message = "Ошибка удаления";
                }
            }
            catch (Exception e)
            {
                Message = "Нет подключения";
            }
            MessageBox.Show(Message);
        }
        private void UpdateProject(Project project)
        {
            if (!string.IsNullOrEmpty(project.Error))
            {
                return;
            }
            try
            {
                var response = WebAPI.PutCall(URIs.PROJECT + "/" + project.Id, project, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Message = "Успешно обновлено";
                    eNote_desk.Wins.ProjectDetails.Performed();
                }
                else
                {
                    Message = "Ошибка обновления";
                }
            }
            catch (Exception e)
            {
                Message = "Нет подключения";
            }
            MessageBox.Show(Message);
        }
        private void GetProjects()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.PROJECT, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Projects = response.Result.Content.ReadAsAsync<List<Project>>().Result;
                    Message = "Успешно загружено";
                }
                else
                {
                    Message = "Ошибка загрузки";
                }
            }
            catch (Exception e)
            {
                Message = "Нет подключения";
            }
        }
        private void GetMethodologies()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.PROJECT + "/methodologies", Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    MethodologiesCmb = response.Result.Content.ReadAsAsync<List<Methodology>>().Result;
                    Message = "Успешно загружено";
                }
                else
                {
                    Message = "Ошибка загрузки";
                }
            }
            catch (Exception e)
            {
                Message = "Нет подключения";
            }
        }
        #endregion
    }
}
