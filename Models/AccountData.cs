using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace eNote_desk.Models
{
    public class AccountData : IDataErrorInfo
    {
        public int Id { get; set; }
        public Account Account { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }

        public string Error { get; set; }

        public string this[string columnName] 
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Username":
                        if (string.IsNullOrEmpty(Username) || !(Username.Length > 1 && Username.Length < 20))
                        {
                            error = "Логин должен быть от 1 до 20 символов";
                        }
                        break;
                    case "Password":
                        if (string.IsNullOrEmpty(Password) || !(Password.Length > 1 && Password.Length < 20))
                        {
                            error = "Пароль должен быть от 1 до 20 символов";
                        }
                        break;
                    case "Phone":
                        if (string.IsNullOrEmpty(Phone) || !Regex.IsMatch(Phone, "\\d{11}"))
                        {
                            error = "Телефон должен быть формата 79189302930";
                        }
                        break;
                    case "Email":
                        if (string.IsNullOrEmpty(Email) || !Regex.IsMatch(Email, "[A-z0-9_]+@(mail|gmail|yandex).(ru|com)"))
                        {
                            error = "Почта должна быть формата lorem@mail.ru";
                        }
                        break;
                }
                Error = error;
                return error;
            }
        }

        public AccountData()
        {
        }
        public AccountData(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public AccountData(string username, string password, string phone, string email, DateTime createdAt) : this(username, password)
        {
            Phone = phone;
            Email = email;
            CreatedAt = createdAt;
        }
    }
}
