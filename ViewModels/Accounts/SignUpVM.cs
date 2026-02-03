using eNote_desk.APIService;
using eNote_desk.Models;
using eNote_desk.ViewModels.Consts;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace eNote_desk.ViewModels.Accounts
{
    public class SignUpVM : BindableBase, INotifyPropertyChanged
    {
        #region Properties
        private Account _account;
        public Account Account
        {
            get { return _account; }
            set { SetProperty(ref _account, value); }
        }

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
        public int Errors { get; set; }
        #endregion
        #region ICommands
        public DelegateCommand PostSignUpClick { get; set; }
        #endregion
        #region Constructor
        public SignUpVM()
        {
            PostSignUpClick = new DelegateCommand(SignUp);
            Account = new Account();
            AccountData = new AccountData();
        }
        #endregion
        #region SignUp
        private void SignUp()
        {
            if (!string.IsNullOrEmpty(Account.Error) || !string.IsNullOrEmpty(AccountData.Error))
            {
                return;
            }
            AccountData.CreatedAt = DateTime.Now;
            UserDTO user = new UserDTO(Account, AccountData);
            try
            {
                var response = WebAPI.PostCall(URIs.SIGN, user);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    MessageBox.Show(Message);
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    Message = "Такой пользователь уже существует";
                    MessageBox.Show(Message);
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("Пользователь зарегистрирован");
                    Message = "Регистрация завершена";
                }
            }
            catch (Exception e)
            {
                Message = "Нет подключения";
                MessageBox.Show(Message + e.Message);
            }
        }
        #endregion
    }
}
