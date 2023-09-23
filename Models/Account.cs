using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace eNote_desk.Models
{
    public class Account : IDataErrorInfo
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Fathername { get; set; }
        public string Description { get; set; }
        public string Speciality { get; set; }
        public string Portfolio { get; set; }
        public string About { get; set; }
        public string Level { get; set; }
        public int Completed { get; set; }
        public int Failed { get; set; }
        public int Workable { get; set; }
        public double Speed { get; set; }
        public double Rate { get; set; }
        public string Projects { get; set; }
        public AccountData AccountData { get; set; }
        public List<Task> Tasks { get; set; }
        public List<Role> Roles { get; set; }

        public string Error { get; set; }

        public string this[string columnName] 
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Name":
                        if (string.IsNullOrEmpty(Name) || !(Name.Length > 1 && Name.Length < 20))
                        {
                            error = "Имя должно быть от 1 до 20 символов";
                        }
                        break;
                    case "Surname":
                        if (string.IsNullOrEmpty(Surname) || !(Surname.Length > 1 && Surname.Length < 20))
                        {
                            error = "Фамилия должна быть от 1 до 20 символов";
                        }
                        break;
                    case "Fathername":
                        if (string.IsNullOrEmpty(Fathername) || !(Fathername.Length > 1 && Fathername.Length < 20))
                        {
                            error = "Отчество должно быть от 1 до 20 символов";
                        }
                        break;
                    case "Nickname":
                        if (string.IsNullOrEmpty(Nickname) || !(Nickname.Length > 1 && Nickname.Length < 20))
                        {
                            error = "Ник должен быть от 1 до 20 символов";
                        }
                        break;
                }
                Error = error;
                return error;
            }
        }

        public Account()
        {
        }

        public Account(string nickname, string name, string surname, string fathername)
        {
            Nickname = nickname;
            Name = name;
            Surname = surname;
            Fathername = fathername;
        }
    }
}
