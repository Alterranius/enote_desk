using System;
using System.Collections.Generic;
using System.Text;

namespace eNote_desk.Models
{
    public class Comment
    {
        public string Id { get; set; }
        public string Head { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public Account Account { get; set; }
        public Task Task { get; set; }

        public Comment()
        {
        }

        public Comment(string head, string content, DateTime createdAt, Account account, Task task)
        {
            Head = head;
            Content = content;
            CreatedAt = createdAt;
            Account = account;
            Task = task;
        }
    }
}
