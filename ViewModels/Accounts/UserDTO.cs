using eNote_desk.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNote_desk.ViewModels.Accounts
{
    public class UserDTO
    {
        public Account Account{ get; set; }
        public AccountData AccountData { get; set; }

        public UserDTO()
        {
        }
        public UserDTO(Account account, AccountData accountData)
        {
            Account = account;
            AccountData = accountData;
        }
    }
}
