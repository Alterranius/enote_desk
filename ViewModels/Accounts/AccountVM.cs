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
    public class AccountVM : BindableBase, INotifyPropertyChanged
    {
        #region Properties
        private Account _account;
        public Account Account
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
        public DelegateCommand<Account> PutAccountClick { get; set; }
        public DelegateCommand<Account> DeleteAccountClick { get; set; }
        #endregion
        #region Constructor
        public AccountVM(string token)
        {
            PutAccountClick = new DelegateCommand<Account>(Update);
            DeleteAccountClick = new DelegateCommand<Account>(Remove);
            Token = token;
            GetAccount();
        }
        #endregion
        #region Service
        private void Update(Account account)
        {
            if (!string.IsNullOrEmpty(Account.Error))
            {
                return;
            }
            try
            {
                var response = WebAPI.PutCall(URIs.ACCOUNT + "/" + Account.Id, account, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Message = "Успешно обновлено";
                    eNote_desk.Wins.AccountSettings.Performed();
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
        private void Remove(Account account)
        {
            try
            {
                var response = WebAPI.DeleteCall(URIs.ACCOUNT + "/" + account.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Message = "Успешно удалено";
                    eNote_desk.Wins.AccountSettings.Performed();
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
        private void GetAccount()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.ACCOUNT, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Account = response.Result.Content.ReadAsAsync<Account>().Result;
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
