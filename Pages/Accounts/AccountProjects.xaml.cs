using eNote_desk.ViewModels.Projects;
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
using eNote_desk.Wins;
using eNote_desk.Models;

namespace eNote_desk.Pages.Accounts
{
    /// <summary>
    /// Логика взаимодействия для AccountProjects.xaml
    /// </summary>
    public partial class AccountProjects : Page
    {
        private readonly Wins.Menu menu;
        private readonly string token;
        private readonly Window owner;
        public AccountProjects(string token, Wins.Menu menu, Window owner)
        {
            InitializeComponent();
            ProjectVM vm = new ProjectVM(token, true);
            DataContext = vm;
            this.token = token;
            this.menu = menu;
            this.owner = owner;
        }

        private void lvMyProjects_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvMyProjects.SelectedItem != null)
            {
                menu.Close();
                new Wins.Menu(token, lvMyProjects.SelectedItem as Project).Show();
                owner.Close();
            }
        }
    }
}
