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
    public class InviteVM : BindableBase, INotifyPropertyChanged
    {
        #region Properties
        private Invite _selectedInvite;
        public Invite SelectedInvite
        {
            get { return _selectedInvite; }
            set { SetProperty(ref _selectedInvite, value); }
        }
        private Project _project;
        public Project Project
        {
            get { return _project; }
            set { SetProperty(ref _project, value); }
        }
        private List<Invite> _invites;
        public List<Invite> Invites
        {
            get { return _invites; }
            set { SetProperty(ref _invites, value); }
        }
        private string _account;
        public string Account
        {
            get { return _account; }
            set { SetProperty(ref _account, value); }
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
        public DelegateCommand<Invite> PutAcceptClick { get; set; }
        public DelegateCommand<Invite> PutDeclineClick { get; set; }
        public DelegateCommand GetInvitesLoad { get; set; }
        public DelegateCommand PostInviteSendClick { get; set; }
        #endregion
        #region Constructor
        public InviteVM(string token)
        {
            PutAcceptClick = new DelegateCommand<Invite>(Accept);
            PutDeclineClick = new DelegateCommand<Invite>(Decline);
            GetInvitesLoad = new DelegateCommand(GetInvites);
            Token = token;
            GetInvites();
        }
        public InviteVM(string token, Project project)
        {
            PostInviteSendClick = new DelegateCommand(SendInvite);
            Token = token;
            Project = project;
        }
        #endregion
        #region Service
        private void Accept(Invite invite)
        {
            try
            {
                var response = WebAPI.PutCall(URIs.INVITE_ACCEPT + "/" + SelectedInvite.Id, invite, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Message = "Успешно принято! Проверьте ваши проекты";
                    GetInvites();
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
        private void Decline(Invite invite)
        {
            try
            {
                var response = WebAPI.PutCall(URIs.INVITE_DECLINE + "/" + SelectedInvite.Id, invite, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Message = "Вы отклонили приглашение!";
                    GetInvites();
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
        private void SendInvite()
        {
            try
            {
                var response = WebAPI.PostCall(URIs.INVITE + "/" + Account, Project.Id.ToString(), Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    Message = "Успешно отправлено";
                }
                else
                {
                    Message = "Ошибка отправления - пользователь не найден";
                }
                eNote_desk.Wins.Invite.Performed();
            }
            catch (Exception e)
            {
                Message = "Нет подключения";
            }
            MessageBox.Show(Message);
        }
        private void GetInvites()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.INVITE, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Invites = response.Result.Content.ReadAsAsync<List<Invite>>().Result;
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
    }
    #endregion
}