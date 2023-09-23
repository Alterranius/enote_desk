using eNote_desk.ViewModels.Projects;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace eNote_desk.Pages.Accounts
{
    /// <summary>
    /// Логика взаимодействия для AccountInvites.xaml
    /// </summary>
    public partial class AccountInvites : Page
    {
        public AccountInvites(string token)
        {
            InitializeComponent();
            DataContext = new InviteVM(token);
        }
    }
}
