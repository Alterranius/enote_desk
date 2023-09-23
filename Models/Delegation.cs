using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace eNote_desk.Models
{
    public class Delegation : IDataErrorInfo
    {
        public string Id { get; set; }
        public string Head { get; set; }
        public string Content { get; set; }
        public string CreatedAt { get; set; }
        public string CompletedAt { get; set; }
        public Account Sender { get; set; }
        public Account Receiver { get; set; }
        public Task Task { get; set; }

        public string Error { get; set; }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Head":
                        if (string.IsNullOrEmpty(Head) || !(Head.Length > 1 && Head.Length < 50))
                        {
                            error = "Заголовок должен быть от 2 до 50 символов";
                        }
                        break;
                    case "Content":
                        if (string.IsNullOrEmpty(Content) || !(Content.Length > 1 && Content.Length < 500))
                        {
                            error = "Содержание должно быть от 2 до 500 символов";
                        }
                        break;
                    case "Receiver":
                        if (Receiver == null)
                        {
                            error = "Получатель не должен быть пустым";
                        }
                        break;
                    case "Task":
                        if (Task == null)
                        {
                            error = "Не выбрана задача";
                        }
                        break;
                }
                Error = error;
                return error;
            }
        }

        public Delegation()
        {
        }

        public Delegation(string head, string content, string createdAt, string completedAt, Account sender, Account receiver, Task task)
        {
            Head = head;
            Content = content;
            CreatedAt = createdAt;
            CompletedAt = completedAt;
            Sender = sender;
            Receiver = receiver;
            Task = task;
        }
    }
}
