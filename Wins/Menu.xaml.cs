using eNote_desk.Models;
using eNote_desk.Pages.Appeals;
using eNote_desk.Pages.Delegations;
using eNote_desk.Pages.Departs;
using eNote_desk.Pages.Plans;
using eNote_desk.Pages.Projects;
using eNote_desk.Pages.Shared;
using eNote_desk.Pages.Tasks;
using eNote_desk.Pages.Teams;
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
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        private readonly string token;
        public Project Project { get; set; }
        public Team ChosenTeam { get; set; }
        public Depart ChosenDepart { get; set; }
        public string Name { get; set; }
        private Menu win;
        public Menu(string token)
        {
            InitializeComponent();
            this.token = token;
            win = this;
        }

        public Menu(string token, Project project)
        {
            InitializeComponent();
            this.token = token;
            Project = project;
            win = this;
            tProject.Text = Project.Name;
            tName.Text = "Добро пожаловать";
        }

        private void btnAccountStat_Click(object sender, RoutedEventArgs e)
        {
            new AccountSupply(token, "AccountStatistics", win).ShowDialog();
        }

        private void btnMyProjects_Click(object sender, RoutedEventArgs e)
        {
            new AccountSupply(token, "AccountProjects", win).ShowDialog();
        }

        private void btnInvites_Click(object sender, RoutedEventArgs e)
        {
            new AccountSupply(token, "AccountInvites", win).ShowDialog();
        }

        private void btnAccount_Click(object sender, RoutedEventArgs e)
        {
            new AccountSettings(token).ShowDialog();
        }

        private void btnSecurity_Click(object sender, RoutedEventArgs e)
        {
            new Security(token).ShowDialog();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            new Auth().Show();
            Close();
        }

        private void btnAddProject_Click(object sender, RoutedEventArgs e)
        {
            new ProjectDetails(token).ShowDialog();
        }

        private void btnMyTasks_Click(object sender, RoutedEventArgs e)
        {
            TasksFrame.Navigate(new MyTasks(token, Project));
        }

        private void btnTasks_Click(object sender, RoutedEventArgs e)
        {
            TasksFrame.Navigate(new TaskManagement(token, Project));
        }

        private void btnTasksInWork_Click(object sender, RoutedEventArgs e)
        {
            TasksFrame.Navigate(new TaskInWork(token, Project));
        }

        private void btnTasksUnsigned_Click(object sender, RoutedEventArgs e)
        {
            TasksFrame.Navigate(new TaskUnsigned(token, Project));
        }

        private void btnTaskHistory_Click(object sender, RoutedEventArgs e)
        {
            TasksFrame.Navigate(new TaskHistory(token, Project));
        }

        private void btnTeams_Click(object sender, RoutedEventArgs e)
        {
            TeamsFrame.Navigate(new TeamManagement(token, Project));
        }

        private void btnTeamMembers_Click(object sender, RoutedEventArgs e)
        {
            TeamsFrame.Navigate(new TeamMembers(token, Project, TeamsFrame));
        }

        private void btnTeamData_Click(object sender, RoutedEventArgs e)
        {
            TeamsFrame.Navigate(new TeamData(token, Project));
        }

        private void btnDelegations_Click(object sender, RoutedEventArgs e)
        {
            TeamsFrame.Navigate(new Delegations(token, Project));
        }

        private void btnTeamStatistics_Click(object sender, RoutedEventArgs e)
        {
            new TeamChoseWin(token, this, Project).ShowDialog();
            if (ChosenTeam == null)
            {
                return;
            }
            TeamsFrame.Navigate(new UnitStatistics(token, ChosenTeam));
        }

        private void btnDeparts_Click(object sender, RoutedEventArgs e)
        {
            DepartsFrame.Navigate(new DepartManagement(token, Project));
        }

        private void btnDepartMembers_Click(object sender, RoutedEventArgs e)
        {
            DepartsFrame.Navigate(new DepartMembers(token, Project, DepartsFrame));
        }

        private void btnDepartData_Click(object sender, RoutedEventArgs e)
        {
            DepartsFrame.Navigate(new DepartData(token, Project));
        }

        private void btnDepartPlans_Click(object sender, RoutedEventArgs e)
        {
            new DepartChoseWin(token, this, Project).ShowDialog();
            if (ChosenDepart == null)
            {
                return;
            }
            DepartsFrame.Navigate(new Plans(token, ChosenDepart));
        }

        private void btnDepartStatistics_Click(object sender, RoutedEventArgs e)
        {
            new DepartChoseWin(token, this, Project).ShowDialog();
            if (ChosenDepart == null)
            {
                return;
            }
            DepartsFrame.Navigate(new UnitStatistics(token, ChosenDepart));
        }

        private void btnProjects_Click(object sender, RoutedEventArgs e)
        {
            ProjectsFrame.Navigate(new OrganisationDetails(token, Project, win));
        }

        private void btnProjectMembers_Click(object sender, RoutedEventArgs e)
        {
            ProjectsFrame.Navigate(new RoleMembers(token, Project, ProjectsFrame));
        }

        private void btnAppeals_Click(object sender, RoutedEventArgs e)
        {
            ProjectsFrame.Navigate(new Appeals(token, Project));
        }

        private void btnProjectPlans_Click(object sender, RoutedEventArgs e)
        {
            ProjectsFrame.Navigate(new Plans(token, Project));
        }

        private void btnProjectStatistics_Click(object sender, RoutedEventArgs e)
        {
            ProjectsFrame.Navigate(new UnitStatistics(token, Project));
        }
    }
}
