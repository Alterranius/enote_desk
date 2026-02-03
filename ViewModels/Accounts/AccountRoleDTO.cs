using eNote_desk.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNote_desk.ViewModels.Accounts
{
    public class AccountRoleDTO
    {
        public Account Account { get; set; }
        public Role Role { get; set; }

        public AccountRoleDTO()
        {
        }

        public AccountRoleDTO(Account account, Role role)
        {
            Account = account;
            Role = role;
        }
    }
}
