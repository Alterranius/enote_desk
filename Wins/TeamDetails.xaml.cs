using eNote_desk.Models;
using eNote_desk.ViewModels.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для TeamDetails.xaml
    /// </summary>
    public partial class TeamDetails : Window
    {
        private static TeamDetails win;
        public TeamDetails(string token, Depart depart, Project project)
        {
            InitializeComponent();
            TeamVM vm = new TeamVM(token, depart, project);
            DataContext = vm;
            btnDelete.Visibility = Visibility.Hidden;
            btnAdd.Command = vm.PostTeamAddClick;
            win = this;
        }

        public TeamDetails(string token, Team team)
        {
            InitializeComponent();
            TeamVM vm = new TeamVM(token, team);
            DataContext = vm;
            btnDelete.Command = vm.DeleteTeamRemoveClick;
            btnDelete.CommandParameter = vm.SelectedTeam;
            btnAdd.Command = vm.PutTeamUpdateClick;
            btnAdd.CommandParameter = vm.SelectedTeam;
            win = this;
            cbDepart.SelectedItem = vm.DepartsCmb.FirstOrDefault(x => x.Id == team.Depart.Id);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
