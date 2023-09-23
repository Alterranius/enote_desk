using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace eNote_desk.Models
{
    public class Appeal : IDataErrorInfo
    {
        public string Id { get; set; }
        public string Head { get; set; }
        public string Content { get; set; }
        public string CreatedAt { get; set; }
        public string IsResponse { get; set; }
        public Account Sender { get; set; }
        public Account Receiver { get; set; }

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
                        if (string.IsNullOrEmpty(Content) || !(Content.Length > 1 && Content.Length < 50))
                        {
                            error = "Содержание должно быть от 2 до 20 символов";
                        }
                        break;
                    case "Receiver":
                        if (Receiver == null)
                        {
                            error = "Получатель не должен быть пустым";
                        }
                        break;
                }
                Error = error;
                return error;
            }
        }

        public Appeal()
        {
        }

        public Appeal(string head, string content, string createdAt, string isResponse, Account sender, Account receiver)
        {
            Head = head;
            Content = content;
            CreatedAt = createdAt;
            IsResponse = isResponse;
            Sender = sender;
            Receiver = receiver;
        }
    }
}
