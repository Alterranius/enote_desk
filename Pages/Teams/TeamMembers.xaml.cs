using eNote_desk.Models;
using eNote_desk.Pages.Shared;
using eNote_desk.ViewModels.Teams;
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
    /// Логика взаимодействия для TeamMembers.xaml
    /// </summary>
    public partial class TeamMembers : Page
    {
        private readonly string token;
        private readonly Frame frame;
        private readonly Project actual;
        public TeamMembers(string token, Project project, Frame frame)
        {
            InitializeComponent();
            TeamVM vm = new TeamVM(token, project);
            DataContext = vm;
            this.token = token;
            this.frame = frame;
            actual = project;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (lvTeams.SelectedItem != null)
            {
                frame.Navigate(new RoleMembers(token, lvTeams.SelectedItem as Team, frame, actual));
            }
        }
    }
}
