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
    /// Логика взаимодействия для SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        private readonly SignUpVM vm;
        public SignUp()
        {
            InitializeComponent();
            SignUpVM vm = new SignUpVM();
            DataContext = vm;
            this.vm = vm;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            new Auth().Show();
            Close();
        }

        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                vm.Errors += 1;
            }
            if (e.Action == ValidationErrorEventAction.Removed)
            {
                vm.Errors -= 1;
            }
        }
    }
}
