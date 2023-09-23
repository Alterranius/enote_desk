using eNote_desk.Models;
using eNote_desk.ViewModels.Teams;
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

namespace eNote_desk.Pages.Teams
{
    /// <summary>
    /// Логика взаимодействия для TeamManagement.xaml
    /// </summary>
    public partial class TeamManagement : Page
    {
        private readonly string token;
        private readonly Depart depart;
        private readonly Project project;
        private readonly TeamVM vm;
        public TeamManagement(string token, Project project)
        {
            InitializeComponent();
            TeamVM vm = new TeamVM(token, depart, project);
            DataContext = vm;
            btnDelete.Command = vm.DeleteTeamRemoveClick;
            btnDelete.CommandParameter = vm.SelectedTeam;
            this.token = token;
            this.project = project;
            this.vm = vm;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            new TeamDetails(token, depart, project).ShowDialog();
            vm.GetProjectTeams();
        }

        private void lvTeams_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvTeams.SelectedItem != null)
            {
                new TeamDetails(token, lvTeams.SelectedItem as Team).ShowDialog();
                vm.GetProjectTeams();
            }
        }
    }
}
