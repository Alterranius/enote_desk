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

namespace eNote_desk.ViewModels.Accounts
{
    public class AuthVM : BindableBase, INotifyPropertyChanged
    {
        #region Properties
        private AccountResponseBody _responseBody;
        public AccountResponseBody ResponseBody
        {
            get { return _responseBody; }
            set { SetProperty(ref _responseBody, value); }
        }
        private AccountData _accountData;
        public AccountData AccountData
        {
            get { return _accountData; }
            set { SetProperty(ref _accountData, value); }
        }
        private ISet<Role> _roles;
        public ISet<Role> Roles
        {
            get { return _roles; }
            set { SetProperty(ref _roles, value); }
        }
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
        private string _jwt_Token;
        public string JWT_Token
        {
            get { return _jwt_Token; }
            set { SetProperty(ref _jwt_Token, value); }
        }
        private bool _succeed;
        public bool Succeed
        {
            get { return _succeed; }
            set { SetProperty(ref _succeed, value); }
        }
        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }
        #endregion
        #region ICommands
        public DelegateCommand<object> PostAuthClick { get; set; }
        #endregion
        #region Constructor
        public AuthVM()
        {
            PostAuthClick = new DelegateCommand<object>(Authenticate);
        }
        #endregion
        #region Auth
        private void Authenticate(object commandParameter)
        {
            Password = ((PasswordBox)commandParameter).Password.ToString();
            AccountData currentData = new AccountData(Username, Password);
            try
            {
                var response = WebAPI.PostCall(URIs.AUTH, currentData);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Message = "Пользователь не найден";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Succeed = false;
                    ResponseBody = response.Result.Content.ReadAsAsync<AccountResponseBody>().Result;
                    Succeed = ResponseBody.Succeed;
                    JWT_Token = ResponseBody.JWT_Token;
                    Roles = ResponseBody.Roles;
                    if (Succeed)
                    {
                        Message = "Успешный вход";
                        eNote_desk.Wins.Auth.Complete(JWT_Token);
                        return;
                    }
                    Message = "Неверный пароль";
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
