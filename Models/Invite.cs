using System;
using System.Collections.Generic;
using System.Text;

namespace eNote_desk.Models
{
    public class Invite
    {
        public string Id { get; set; }
        public string CreatedAt { get; set; }
        public string Status { get; set; }
        public string Account { get; set; }
        public string Project { get; set; }
        public string Acted { get; set; }

        public Invite()
        {
        }

        public Invite(string id, string createdAt, string status, string account, string project, string acted)
        {
            Id = id;
            CreatedAt = createdAt;
            Status = status;
            Account = account;
            Project = project;
            Acted = acted;
        }

        public Invite(string createdAt, string status, string account, string project, string acted)
        {
            CreatedAt = createdAt;
            Status = status;
            Account = account;
            Project = project;
            Acted = acted;
        }
    }
}
