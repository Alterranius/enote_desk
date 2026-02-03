using eNote_desk.Models;
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

namespace eNote_desk.Pages.Projects
{
    /// <summary>
    /// Логика взаимодействия для OrganisationDetails.xaml
    /// </summary>
    public partial class OrganisationDetails : Page
    {
        private readonly ProjectVM vm;
        private readonly Project project;
        private readonly Wins.Menu menu;
        private bool deleted = false;
        private string token;
        public OrganisationDetails(string token, Project project, Wins.Menu menu)
        {
            InitializeComponent();
            ProjectVM vm = new ProjectVM(token, project);
            DataContext = vm;
            btnAdd.Command = vm.PutProjectUpdateClick;
            btnAdd.CommandParameter = vm.SelectedProject;
            this.vm = vm;
            this.project = project;
            this.menu = menu;
            this.token = token;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (deleted)
            {
                btnDelete.Command = vm.DeleteProjectRemoveClick;
                btnDelete.CommandParameter = project;
                menu.Close();
                new Wins.Menu(token).Show();
                return;
            }
            MessageBox.Show("Вы уверены, что хотите удалить проект?\n" +
                            "Тогда нажмите ещё раз");
            deleted = true;
        }
    }
}
