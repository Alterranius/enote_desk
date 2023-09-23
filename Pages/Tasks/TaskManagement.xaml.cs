using eNote_desk.Models;
using eNote_desk.ViewModels.Tasks;
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

namespace eNote_desk.Pages.Tasks
{
    /// <summary>
    /// Логика взаимодействия для TaskManagement.xaml
    /// </summary>
    public partial class TaskManagement : Page
    {
        private readonly string token;
        private readonly Project project;
        private readonly TaskVM vm;
        public TaskManagement(string token, Project project)
        {
            InitializeComponent();
            TaskVM vm = new TaskVM(token, project, true);
            DataContext = vm;
            btnDelete.Command = vm.DeleteTaskRemoveClick;
            btnDelete.CommandParameter = vm.SelectedTask;
            this.token = token;
            this.project = project;
            this.vm = vm;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            new TaskDetails(token, project).ShowDialog();
            vm.GetTasks();
        }

        private void lvTasks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvTasks.SelectedItem != null)
            {
                new TaskDetails(token, lvTasks.SelectedItem as Task).ShowDialog();
                vm.GetTasks();
            }
        }
    }
}
