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

namespace eNote_desk.ViewModels.Delegations
{
    public class DelegationVM : BindableBase, INotifyPropertyChanged
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
        private List<Delegation> _delegations;
        public List<Delegation> Delegations
        {
            get { return _delegations; }
            set { SetProperty(ref _delegations, value); }
        }
        private List<Account> _accountsCmb;
        public List<Account> AccountsCmb
        {
            get { return _accountsCmb; }
            set { SetProperty(ref _accountsCmb, value); }
        }
        private List<Task> _tasksCmb;
        public List<Task> TasksCmb
        {
            get { return _tasksCmb; }
            set { SetProperty(ref _tasksCmb, value); }
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
        private Delegation _delegation;
        public Delegation Delegation
        {
            get { return _delegation; }
            set { SetProperty(ref _delegation, value); }
        }
        private Project _project;
        public Project Project
        {
            get { return _project; }
            set { SetProperty(ref _project, value); }
        }
        private string _token;
        public string Token
        {
            get { return _token; }
            set { SetProperty(ref _token, value); }
        }
        private string _completedAt;
        public string CompletedAt
        {
            get { return _completedAt; }
            set { SetProperty(ref _completedAt, value); }
        }
        #endregion
        #region ICommands
        public DelegateCommand PostDelegationAddClick { get; set; }
        public DelegateCommand<Delegation> DeleteDelegationRemoveClick { get; set; }
        public DelegateCommand<Delegation> PutDelegationUpdateClick { get; set; }
        public DelegateCommand<Delegation> PutDelegationCompleteClick { get; set; }
        public DelegateCommand GetDelegationsLoad { get; set; }
        #endregion
        #region Constructor
        public DelegationVM(string token, Project project)
        {
            Delegation = new Delegation();
            PostDelegationAddClick = new DelegateCommand(AddDelegation);
            GetDelegationsLoad = new DelegateCommand(GetDelegations);
            Token = token;
            Project = project;
            GetDelegations();
            GetAccountsCmb();
            GetTasksCmb();
        }
        public DelegationVM(string token, Delegation delegation, Project project)
        {
            DeleteDelegationRemoveClick = new DelegateCommand<Delegation>(RemoveDelegation);
            PutDelegationUpdateClick = new DelegateCommand<Delegation>(UpdateDelegation);
            PutDelegationCompleteClick = new DelegateCommand<Delegation>(CompleteDelegation);
            Token = token;
            Project = project;
            Delegation = delegation;
            GetAccountsCmb();
            GetTasksCmb();
        }
        #endregion
        #region Service
        private void AddDelegation()
        {
            if (!string.IsNullOrEmpty(Delegation.Error))
            {
                return;
            }
            Delegation.CreatedAt = DateTime.Now.ToString();
            try
            {
                var response = WebAPI.PostCall(URIs.DELEGATION, Delegation, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    Message = "Успешно добавлено";
                    eNote_desk.Wins.DelegationDetails.Performed();
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
        private void RemoveDelegation(Delegation delegation)
        {
            try
            {
                var response = WebAPI.DeleteCall(URIs.DELEGATION + "/" + delegation.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Message = "Успешно удалено";
                    eNote_desk.Wins.DelegationDetails.Performed();
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
        private void UpdateDelegation(Delegation delegation)
        {
            if (!string.IsNullOrEmpty(delegation.Error))
            {
                return;
            }
            try
            {
                var response = WebAPI.PutCall(URIs.DELEGATION + "/" + delegation.Id, delegation, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Message = "Успешно обновлено";
                    eNote_desk.Wins.DelegationDetails.Performed();
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
        private void CompleteDelegation(Delegation delegation)
        {
            try
            {
                var response = WebAPI.PutCall(URIs.DELEGATION_COMPLETE + "/" + delegation.Id, new object(), Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Message = "Успешно обновлено";
                    eNote_desk.Wins.DelegationDetails.Complete();
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
        public void GetDelegations()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.DELEGATIONS + "/" + Project.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Delegations = response.Result.Content.ReadAsAsync<List<Delegation>>().Result;
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
        public void GetTasksCmb()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.TASK + "/project/" + Project.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TasksCmb = response.Result.Content.ReadAsAsync<List<Task>>().Result;
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
