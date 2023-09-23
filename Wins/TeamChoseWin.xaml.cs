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
using System.Windows.Shapes;

namespace eNote_desk.Wins
{
    /// <summary>
    /// Логика взаимодействия для TeamChoseWin.xaml
    /// </summary>
    public partial class TeamChoseWin : Window
    {
        private readonly Menu menu;
        public TeamChoseWin(string token, Menu menu, Project project)
        {
            InitializeComponent();
            TeamVM vm = new TeamVM(token, project);
            DataContext = vm;
            this.menu = menu;
        }

        private void btnAssign_Click(object sender, RoutedEventArgs e)
        {
            if (cbTeam.SelectedItem == null)
            {
                MessageBox.Show("Выберите команду!");
                return;
            }
            menu.ChosenTeam = cbTeam.SelectedItem as Team;
            Close();
        }
    }
}
