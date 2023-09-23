using eNote_desk.Models;
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
    /// Логика взаимодействия для RoleSetter.xaml
    /// </summary>
    public partial class RoleSetter : Window
    {
        private static RoleSetter win;
        public RoleSetter(string token, Project project)
        {
            InitializeComponent();
            RoleVM vm = new RoleVM(token, project);
            DataContext = vm;
            win = this;
        }
        public RoleSetter(string token, Depart depart)
        {
            InitializeComponent();
            RoleVM vm = new RoleVM(token, depart);
            DataContext = vm;
            win = this;
        }
        public RoleSetter(string token, Team team)
        {
            InitializeComponent();
            RoleVM vm = new RoleVM(token, team);
            DataContext = vm;
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
