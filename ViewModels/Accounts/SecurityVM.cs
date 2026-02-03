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
    public class SecurityVM : BindableBase, INotifyPropertyChanged
    {
        #region Properties
        private AccountData _accountData;
        public AccountData AccountData
        {
            get { return _accountData; }
            set { SetProperty(ref _accountData, value); }
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
        public DelegateCommand<AccountData> PutAccountDataClick { get; set; }
        #endregion
        #region Constructor
        public SecurityVM(string token)
        {
            PutAccountDataClick = new DelegateCommand<AccountData>(Update);
            Token = token;
            GetAccountData();
        }
        #endregion
        #region Service
        private void Update(AccountData accountData)
        {
            if (!string.IsNullOrEmpty(accountData.Error))
            {
                return;
            }
            try
            {
                var response = WebAPI.PutCall(URIs.ACCOUNT_SECURITY + "/" + AccountData.Id, accountData, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Message = "Успешно обновлено";
                    eNote_desk.Wins.Security.Performed();
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
        }
        private void GetAccountData()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.ACCOUNT_SECURITY, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    AccountData = response.Result.Content.ReadAsAsync<AccountData>().Result;
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
