using eNote_desk.Models;
using eNote_desk.ViewModels.Tasks;
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
    /// Логика взаимодействия для TaskHistory.xaml
    /// </summary>
    public partial class TaskHistory : Page
    {
        public TaskHistory(string token, Project project)
        {
            InitializeComponent();
            TaskVM vm = new TaskVM(token, project, DateTime.Now);
            DataContext = vm;
        }
    }
}
