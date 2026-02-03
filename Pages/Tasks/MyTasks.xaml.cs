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
    /// Логика взаимодействия для MyTasks.xaml
    /// </summary>
    public partial class MyTasks : Page
    {
        private readonly string token;
        private readonly Project project;
        public MyTasks(string token, Project project)
        {
            InitializeComponent();
            TaskVM vm = new TaskVM(token, project);
            DataContext = vm;
            this.token = token;
            this.project = project;
        }

        private void lvTasks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvTasks.SelectedItem != null)
            {
                new TaskDetails(token, lvTasks.SelectedItem as Task).ShowDialog();
            }
        }
    }
}
