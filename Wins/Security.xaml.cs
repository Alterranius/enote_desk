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
    /// Логика взаимодействия для Security.xaml
    /// </summary>
    public partial class Security : Window
    {
        private static Security win;
        public Security(string token)
        {
            InitializeComponent();
            SecurityVM vm = new SecurityVM(token);
            DataContext = vm;
            tbOldPassword.Text = vm.AccountData.Password;
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
