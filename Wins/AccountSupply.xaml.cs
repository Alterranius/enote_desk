using eNote_desk.Pages.Accounts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace eNote_desk.Wins
{
    /// <summary>
    /// Логика взаимодействия для AccountSupply.xaml
    /// </summary>
    public partial class AccountSupply : Window
    {
        private readonly Menu menu;
        public AccountSupply(string token, string pass, Menu menu)
        {
            InitializeComponent();
            switch (pass)
            {
                case "AccountStatistics":
                    frmContent.Navigate(new AccountStatistics(token));
                    break;
                case "AccountProjects":
                    frmContent.Navigate(new AccountProjects(token, menu, this));
                    break;
                case "AccountInvites":
                    frmContent.Navigate(new AccountInvites(token));
                    break;
            }
            this.menu = menu;
        }
    }
}
