using eNote_desk.Models;
using eNote_desk.Pages.Departs;
using eNote_desk.Pages.Teams;
using eNote_desk.ViewModels.Accounts;
using eNote_desk.Wins;
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

namespace eNote_desk.Pages.Shared
{
    /// <summary>
    /// Логика взаимодействия для RoleMembers.xaml
    /// </summary>
    public partial class RoleMembers : Page
    {
        private readonly string token;
        private readonly bool isDepart = false;
        private readonly bool isTeam = false;
        private readonly Project project;
        private readonly Project actual;
        private readonly Depart depart;
        private readonly Team team;
        private readonly Frame frame;
        private readonly RoleVM vm;
        public RoleMembers(string token, Project project, Frame frame)
        {
            InitializeComponent();
            RoleVM vm = new RoleVM(token, project);
            DataContext = vm;
            btnDelete.Command = vm.DeleteRoleAccountClick;
            btnBack.Visibility = Visibility.Hidden;
            this.project = project;
            this.token = token;
            this.frame = frame;
            this.vm = vm;
        }

        public RoleMembers(string token, Depart depart, Frame frame, Project project)
        {
            InitializeComponent();
            RoleVM vm = new RoleVM(token, depart);
            DataContext = vm;
            btnDelete.Command = vm.DeleteRoleAccountClick;
            btnInvite.Visibility = Visibility.Hidden;
            this.depart = depart;
            this.token = token;
            this.frame = frame;
            actual = project;
            isDepart = true;
            this.vm = vm;
        }

        public RoleMembers(string token, Team team, Frame frame, Project project)
        {
            InitializeComponent();
            RoleVM vm = new RoleVM(token, team);
            DataContext = vm;
            btnDelete.Command = vm.DeleteRoleAccountClick;
            btnInvite.Visibility = Visibility.Hidden;
            this.team = team;
            this.token = token;
            this.frame = frame;
            actual = project;
            isTeam = true;
            this.vm = vm;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (isDepart)
            {
                frame.Navigate(new DepartMembers(token, actual, frame));
            }
            if (isTeam)
            {
                frame.Navigate(new TeamMembers(token, actual, frame));
            }
            if (project != null)
            {
                frame.Navigate(new TeamMembers(token, actual, frame));
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (project != null)
            {
                new RoleSetter(token, project).ShowDialog();
                vm.GetProjectAccounts();
                return;
            }
            if (depart != null)
            {
                new RoleSetter(token, depart).ShowDialog();
                vm.GetDepartAccounts();
                return;
            }
            if (team != null)
            {
                new RoleSetter(token, team).ShowDialog();
                vm.GetTeamAccounts();
                return;
            }
        }

        private void btnInvite_Click(object sender, RoutedEventArgs e)
        {
            new Wins.Invite(token, project).ShowDialog();
        }
    }
}
