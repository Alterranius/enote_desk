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

namespace eNote_desk.ViewModels.Teams
{
    public class TeamVM : BindableBase, INotifyPropertyChanged
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
        private List<Team> _teams;
        public List<Team> Teams
        {
            get { return _teams; }
            set { SetProperty(ref _teams, value); }
        }
        private List<Depart> _departsCmb;
        public List<Depart> DepartsCmb
        {
            get { return _departsCmb; }
            set { SetProperty(ref _departsCmb, value); }
        }
        private Depart _depart;
        public Depart Depart
        {
            get { return _depart; }
            set { SetProperty(ref _depart, value); }
        }
        private Team _team;
        public Team SelectedTeam
        {
            get { return _team; }
            set { SetProperty(ref _team, value); }
        }

        private Project _project;
        public Project Project
        {
            get { return _project; }
            set { SetProperty(ref _project, value); }
        }

        private Task _selectedTask;
        public Task SelectedTask
        {
            get { return _selectedTask; }
            set { SetProperty(ref _selectedTask, value); }
        }

        private TeamDataDTO _teamDataDTO;
        public TeamDataDTO TeamDataDTO
        {
            get { return _teamDataDTO; }
            set { SetProperty(ref _teamDataDTO, value); }
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
        public DelegateCommand PostTeamAddClick { get; set; }
        public DelegateCommand<Team> DeleteTeamRemoveClick { get; set; }
        public DelegateCommand<Team> PutTeamUpdateClick { get; set; }
        public DelegateCommand GetTeamsLoad { get; set; }
        public DelegateCommand GetTaskStatsLoad { get; set; }
        public DelegateCommand GetProjectTeamsLoad { get; set; }
        #endregion
        #region Constructor
        public TeamVM(string token, Team team)
        {
            Project = team.Depart.Project;
            DeleteTeamRemoveClick = new DelegateCommand<Team>(RemoveTeam);
            PutTeamUpdateClick = new DelegateCommand<Team>(UpdateTeam);
            Token = token;
            SelectedTeam = team;
            GetDepartsCmb();
        }
        public TeamVM(string token, Depart depart, Project project)
        {
            SelectedTeam = new Team() { Depart = depart };
            PostTeamAddClick = new DelegateCommand(AddTeam);
            DeleteTeamRemoveClick = new DelegateCommand<Team>(RemoveTeam);
            GetTeamsLoad = new DelegateCommand(GetTeams);
            Token = token;
            Depart = depart;
            Project = project;
            GetProjectTeams();
            GetDepartsCmb();
        }
        public TeamVM(string token, Project project)
        {
            GetProjectTeamsLoad = new DelegateCommand(GetProjectTeams);
            GetTaskStatsLoad = new DelegateCommand(GetTaskStats);
            Token = token;
            Project = project;
            GetProjectTeams();
        }
        #endregion
        #region Service
        private void AddTeam()
        {
            if (!string.IsNullOrEmpty(SelectedTeam.Error))
            {
                return;
            }
            try
            {
                var response = WebAPI.PostCall(URIs.TEAM, SelectedTeam, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    Message = "Успешно добавлено";
                    eNote_desk.Wins.TeamDetails.Performed();
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
        private void RemoveTeam(Team team)
        {
            try
            {
                var response = WebAPI.DeleteCall(URIs.TEAM + "/" + team.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Message = "Успешно удалено";
                    eNote_desk.Wins.TeamDetails.Performed();
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
        private void UpdateTeam(Team team)
        {
            if (!string.IsNullOrEmpty(team.Error))
            {
                return;
            }
            try
            {
                var response = WebAPI.PutCall(URIs.TEAM + "/" + team.Id, team, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Message = "Успешно обновлено";
                    eNote_desk.Wins.TeamDetails.Performed();
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
        private void GetTeams()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.TEAM + "/" + Depart.Id , Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Teams = response.Result.Content.ReadAsAsync<List<Team>>().Result;
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
        public void GetProjectTeams()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.TEAMS_PROJECT + "/" + Project.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Teams = response.Result.Content.ReadAsAsync<List<Team>>().Result;
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
        public void GetDepartsCmb()
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
                    DepartsCmb = response.Result.Content.ReadAsAsync<List<Depart>>().Result;
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
                var response = WebAPI.GetCall(URIs.TEAM_STATS + "/" + SelectedTeam.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TeamDataDTO = response.Result.Content.ReadAsAsync<TeamDataDTO>().Result;
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
