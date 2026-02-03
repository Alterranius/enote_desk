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

namespace eNote_desk.ViewModels.Departs
{
    public class DepartVM : BindableBase, INotifyPropertyChanged
    {
        #region Properties
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }
        private string _product;
        public string Product
        {
            get { return _product; }
            set { SetProperty(ref _product, value); }
        }
        private List<Depart> _departs;
        public List<Depart> Departs
        {
            get { return _departs; }
            set { SetProperty(ref _departs, value); }
        }
        private Project _project;
        public Project Project
        {
            get { return _project; }
            set { SetProperty(ref _project, value); }
        }
        private Depart _depart;
        public Depart SelectedDepart
        {
            get { return _depart; }
            set { SetProperty(ref _depart, value); }
        }
        private DepartDataDTO _departDataDTO;
        public DepartDataDTO DepartDataDTO
        {
            get { return _departDataDTO; }
            set { SetProperty(ref _departDataDTO, value); }
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
        public DelegateCommand PostDepartAddClick { get; set; }
        public DelegateCommand<Depart> DeleteDepartRemoveClick { get; set; }
        public DelegateCommand<Depart> PutDepartUpdateClick { get; set; }
        public DelegateCommand GetDepartsLoad { get; set; }
        public DelegateCommand GetTaskStatsLoad { get; set; }
        #endregion
        #region Constructor
        public DepartVM(string token, Depart depart)
        {
            DeleteDepartRemoveClick = new DelegateCommand<Depart>(RemoveDepart);
            PutDepartUpdateClick = new DelegateCommand<Depart>(UpdateDepart);
            Token = token;
            SelectedDepart = depart;
        }
        public DepartVM(string token, Project project)
        {
            SelectedDepart = new Depart();
            PostDepartAddClick = new DelegateCommand(AddDepart);
            DeleteDepartRemoveClick = new DelegateCommand<Depart>(RemoveDepart);
            GetDepartsLoad = new DelegateCommand(GetDeparts);
            GetTaskStatsLoad = new DelegateCommand(GetTaskStats);
            Token = token;
            Project = project;
            GetDeparts();
        }
        #endregion
        #region Service
        private void AddDepart()
        {
            SelectedDepart.Project = Project;
            if (!string.IsNullOrEmpty(SelectedDepart.Error))
            {
                return;
            }
            try
            {
                var response = WebAPI.PostCall(URIs.DEPART, SelectedDepart, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    Message = "Успешно добавлено";
                    eNote_desk.Wins.DepartDetails.Performed();
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
        private void RemoveDepart(Depart depart)
        {
            try
            {
                var response = WebAPI.DeleteCall(URIs.DEPART + "/" + depart.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Message = "Успешно удалено";
                    eNote_desk.Wins.DepartDetails.Performed();
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
        private void UpdateDepart(Depart depart)
        {
            if (!string.IsNullOrEmpty(depart.Error))
            {
                return;
            }
            try
            {
                var response = WebAPI.PutCall(URIs.DEPART + "/" + depart.Id, depart, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Message = "Успешно обновлено";
                    eNote_desk.Wins.DepartDetails.Performed();
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
        public void GetDeparts()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.DEPART + "/" + Project.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Departs = response.Result.Content.ReadAsAsync<List<Depart>>().Result;
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
        public void GetTaskStats()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.DEPART_STATS + "/" + SelectedDepart.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    DepartDataDTO = response.Result.Content.ReadAsAsync<DepartDataDTO>().Result;
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
