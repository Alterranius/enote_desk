using eNote_desk.Models;
using eNote_desk.ViewModels.Departs;
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
    /// Логика взаимодействия для DepartDetails.xaml
    /// </summary>
    public partial class DepartDetails : Window
    {
        private static DepartDetails win;
        public Project Project { get; set; }
        public DepartDetails(string token, Project project)
        {
            InitializeComponent();
            DepartVM vm = new DepartVM(token, project);
            DataContext = vm;
            btnDelete.Visibility = Visibility.Hidden;
            btnAdd.Command = vm.PostDepartAddClick;
            win = this;
            List<Project> projects = new List<Project>();
            projects.Add(project);
            cbProject.ItemsSource = projects;
            Project = project;
            cbProject.SelectedItem = Project;
            cbProject.Visibility = Visibility.Hidden;
            stkProject.Visibility = Visibility.Hidden;
        }

        public DepartDetails(string token, Depart depart)
        {
            InitializeComponent();
            DepartVM vm = new DepartVM(token, depart);
            DataContext = vm;
            btnDelete.Command = vm.DeleteDepartRemoveClick;
            btnDelete.CommandParameter = vm.SelectedDepart;
            btnAdd.Command = vm.PutDepartUpdateClick;
            btnAdd.CommandParameter = vm.SelectedDepart;
            win = this;
            List<Project> projects = new List<Project>();
            projects.Add(depart.Project);
            cbProject.ItemsSource = projects;
            cbProject.SelectedItem = depart.Project;
            cbProject.Visibility = Visibility.Hidden;
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
