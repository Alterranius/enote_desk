using eNote_desk.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNote_desk.ViewModels.Accounts
{
    public class AccountResponseBody
    {
        public string JWT_Token { get; set; }
        public bool Succeed { get; set; }
        public ISet<Role> Roles { get; set; }
    }
}
