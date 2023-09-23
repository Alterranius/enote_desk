using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace eNote_desk.Models
{
    public class Task : IDataErrorInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DelegatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public TaskCategory TaskCategory { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public TaskType TaskType { get; set; }
        public Account Account { get; set; }
        public Team Team { get; set; }

        public string Error { get; set; }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Name":
                        if (string.IsNullOrEmpty(Name) || !(Name.Length > 1 && Name.Length < 50))
                        {
                            error = "Название должно быть от 2 до 50 символов";
                        }
                        break;
                    case "Code":
                        if (string.IsNullOrEmpty(Code) || !(Code.Length > 1 && Code.Length < 20))
                        {
                            error = "Кодификатор должен быть от 2 до 20 символов";
                        }
                        break;
                    case "Priority":
                        if (Priority < 0)
                        {
                            error = "Приоритет должен быть положителен";
                        }
                        break;
                    case "Description":
                        if (string.IsNullOrEmpty(Description) || !(Description.Length > 1 && Description.Length < 200))
                        {
                            error = "Описание должно быть от 2 до 200 символов";
                        }
                        break;
                    case "TaskCategory":
                        if (TaskCategory == null)
                        {
                            error = "Категория не должна быть пустой";
                        }
                        break;
                    case "TaskStatus":
                        if (TaskStatus == null)
                        {
                            error = "Статус не должен быть пустым";
                        }
                        break;
                    case "TaskType":
                        if (TaskType == null)
                        {
                            error = "Тип не должен быть пустым";
                        }
                        break;
                    case "Team":
                        if (Team == null)
                        {
                            error = "Команда не должна быть пустой";
                        }
                        break;
                }
                Error = error;
                return error;
            }
        }

        public Task()
        {
        }

        public Task(string name, string code, string description, int priority, DateTime createdAt, DateTime? delegatedAt, DateTime? completedAt, TaskCategory taskCategory, TaskStatus taskStatus, TaskType taskType, Account account, Team team)
        {
            Name = name;
            Code = code;
            Description = description;
            Priority = priority;
            CreatedAt = createdAt;
            DelegatedAt = delegatedAt;
            CompletedAt = completedAt;
            TaskCategory = taskCategory;
            TaskStatus = taskStatus;
            TaskType = taskType;
            Account = account;
            Team = team;
        }
    }
}
