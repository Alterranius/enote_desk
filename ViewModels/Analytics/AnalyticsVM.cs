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

namespace eNote_desk.ViewModels.Analytics
{
    public class AnalyticsVM : BindableBase, INotifyPropertyChanged
    {
        #region Properties
        private AccountStatsDTO accountStatsDTO;
        public AccountStatsDTO AccountStatsDTO
        {
            get { return accountStatsDTO; }
            set { SetProperty(ref accountStatsDTO, value); }
        }
        private UnitStatsDTO _unitStatsDTO;
        public UnitStatsDTO UnitStatsDTO
        {
            get { return _unitStatsDTO; }
            set { SetProperty(ref _unitStatsDTO, value); }
        }
        private Project _project;
        public Project Project
        {
            get { return _project; }
            set { SetProperty(ref _project, value); }
        }
        private Depart _depart;
        public Depart Depart
        {
            get { return _depart; }
            set { SetProperty(ref _depart, value); }
        }
        private Team _team;
        public Team Team
        {
            get { return _team; }
            set { SetProperty(ref _team, value); }
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
        public DelegateCommand GetStatisticsLoad { get; set; }
        #endregion
        #region Constructor
        public AnalyticsVM(string token, Project project)
        {
            GetStatisticsLoad = new DelegateCommand(GetProjectStatistics);
            Token = token;
            Project = project;
            GetProjectStatistics();
        }
        public AnalyticsVM(string token, Depart depart)
        {
            GetStatisticsLoad = new DelegateCommand(GetDepartStatistics);
            Token = token;
            Depart = depart;
            GetDepartStatistics();
        }
        public AnalyticsVM(string token, Team team)
        {
            GetStatisticsLoad = new DelegateCommand(GetTeamStatistics);
            Token = token;
            Team = team;
            GetTeamStatistics();
        }
        public AnalyticsVM(string token)
        {
            GetStatisticsLoad = new DelegateCommand(GetAccountStatistics);
            Token = token;
            GetAccountStatistics();
        }
        #endregion
        #region Service
        private void GetProjectStatistics()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.STATISTICS_PROJECT + "/" + Project.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    UnitStatsDTO = response.Result.Content.ReadAsAsync<UnitStatsDTO>().Result;
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
        private void GetDepartStatistics()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.STATISTICS_DEPART + "/" + Depart.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    UnitStatsDTO = response.Result.Content.ReadAsAsync<UnitStatsDTO>().Result;
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
        private void GetTeamStatistics()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.STATISTICS_TEAM + "/" + Team.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    UnitStatsDTO = response.Result.Content.ReadAsAsync<UnitStatsDTO>().Result;
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
        private void GetAccountStatistics()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.STATISTICS, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    AccountStatsDTO = response.Result.Content.ReadAsAsync<AccountStatsDTO>().Result;
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
