using eNote_desk.ViewModels.Accounts;
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
    /// Логика взаимодействия для AccountSettings.xaml
    /// </summary>
    public partial class AccountSettings : Window
    {
        private static AccountSettings win;
        public AccountSettings(string token)
        {
            InitializeComponent();
            DataContext = new AccountVM(token);
            win = this;
        }

        public static void Performed()
        {
            if (win != null)
            {
                win.Close();
            }
        }
    }
}
