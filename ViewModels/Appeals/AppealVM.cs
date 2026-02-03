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

namespace eNote_desk.ViewModels.Appeals
{
    public class AppealVM : BindableBase, INotifyPropertyChanged
    {
        #region Properties
        private Account _sender;
        public Account Sender
        {
            get { return _sender; }
            set { SetProperty(ref _sender, value); }
        }
        private Account _receiver;
        public Account Receiver
        {
            get { return _receiver; }
            set { SetProperty(ref _receiver, value); }
        }
        private Task _task;
        public Task Task
        {
            get { return _task; }
            set { SetProperty(ref _task, value); }
        }
        private List<Appeal> _appeals;
        public List<Appeal> Appeals
        {
            get { return _appeals; }
            set { SetProperty(ref _appeals, value); }
        }
        private Appeal _selectedAppeal;
        public Appeal SelectedAppeal
        {
            get { return _selectedAppeal; }
            set { SetProperty(ref _selectedAppeal, value); }
        }
        private Project _project;
        public Project Project
        {
            get { return _project; }
            set { SetProperty(ref _project, value); }
        }
        private List<Account> _accountsCmb;
        public List<Account> AccountsCmb
        {
            get { return _accountsCmb; }
            set { SetProperty(ref _accountsCmb, value); }
        }
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
        private string _head;
        public string Head
        {
            get { return _head; }
            set { SetProperty(ref _head, value); }
        }
        private string _content;
        public string Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }
        private string _token;
        public string Token
        {
            get { return _token; }
            set { SetProperty(ref _token, value); }
        }
        private bool _isResponse;
        public bool IsResponse
        {
            get { return _isResponse; }
            set { SetProperty(ref _isResponse, value); }
        }
        public bool Response { get; set; }
        #endregion
        #region ICommands
        public DelegateCommand PostAppealAddClick { get; set; }
        public DelegateCommand<Appeal> DeleteAppealRemoveClick { get; set; }
        public DelegateCommand<Appeal> PutAppealUpdateClick { get; set; }
        public DelegateCommand GetAppealsLoad { get; set; }
        #endregion
        #region Constructor
        public AppealVM(string token, Project project)
        {
            PostAppealAddClick = new DelegateCommand(AddAppeal);
            GetAppealsLoad = new DelegateCommand(GetAppeals);
            Token = token;
            IsResponse = false;
            Project = project;
            SelectedAppeal = new Appeal();
            GetAccountsCmb();
            GetAppeals();
        }
        public AppealVM(string token, Appeal appeal, Project project)
        {
            DeleteAppealRemoveClick = new DelegateCommand<Appeal>(RemoveAppeal);
            PutAppealUpdateClick = new DelegateCommand<Appeal>(UpdateAppeal);
            Token = token;
            Project = project;
            SelectedAppeal = appeal;
            GetAccountsCmb();
            GetAppeals();
        }
        public AppealVM(string token, Project project, bool all)
        {
            GetAppealsLoad = new DelegateCommand(GetAppeals);
            Token = token;
            Project = project;
            GetAccountsCmb();
            GetAppeals();
        }
        #endregion
        #region Service
        private void AddAppeal()
        {
            if (!string.IsNullOrEmpty(SelectedAppeal.Error))
            {
                return;
            }
            SelectedAppeal.IsResponse = Boolean.FalseString;
            if (IsResponse)
            {
                SelectedAppeal.IsResponse = Boolean.TrueString;
            }
            SelectedAppeal.CreatedAt = DateTime.Now.ToString();
            try
            {
                var response = WebAPI.PostCall(URIs.APPEAL, SelectedAppeal, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    Message = "Успешно добавлено";
                    eNote_desk.Wins.AppealDetails.Performed();
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
        private void RemoveAppeal(Appeal appeal)
        {
            try
            {
                var response = WebAPI.DeleteCall(URIs.APPEAL + "/" + appeal.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Message = "Успешно удалено";
                    eNote_desk.Wins.AppealDetails.Performed();
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
        private void UpdateAppeal(Appeal appeal)
        {
            if (!string.IsNullOrEmpty(appeal.Error))
            {
                return;
            }
            try
            {
                var response = WebAPI.PutCall(URIs.APPEAL + "/" + appeal.Id, appeal, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Message = "Успешно обновлено";
                    eNote_desk.Wins.AppealDetails.Performed();
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
        public void GetAppeals()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.APPEALS + "/" + Project.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Appeals = response.Result.Content.ReadAsAsync<List<Appeal>>().Result;
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
        public void GetAccountsCmb()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.ACCOUNT + "/project/" + Project.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    AccountsCmb = response.Result.Content.ReadAsAsync<List<Account>>().Result;
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
