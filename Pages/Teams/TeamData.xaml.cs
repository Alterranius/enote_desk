using eNote_desk.Models;
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
    /// Логика взаимодействия для TeamData.xaml
    /// </summary>
    public partial class TeamData : Page
    {
        private readonly TeamVM vm;
        public TeamData(string token, Project project)
        {
            InitializeComponent();
            TeamVM vm = new TeamVM(token, project);
            this.vm = vm;
            vm.GetTaskStats();
            DataContext = vm;
        }

        private void lvTeams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.GetTaskStats();
        }
    }
}
