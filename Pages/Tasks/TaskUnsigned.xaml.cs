using eNote_desk.Models;
using eNote_desk.ViewModels.Tasks;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace eNote_desk.Pages.Tasks
{
    /// <summary>
    /// Логика взаимодействия для TaskUnsigned.xaml
    /// </summary>
    public partial class TaskUnsigned : Page
    {
        private readonly TaskVM vm;
        public TaskUnsigned(string token, Project project)
        {
            InitializeComponent();
            TaskVM vm = new TaskVM(token, project, -1).Unsigned();
            DataContext = vm;
            this.vm = vm;
        }

        private void lvTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvTasks.SelectedItem != null)
            {
                if ((lvTasks.SelectedItem as Task).Account == null)
                {
                    return;
                }
                cnSign.SelectedItem = vm.AccountsCmb.FirstOrDefault(x => x.Id == (lvTasks.SelectedItem as Task).Account.Id);
            }
        }
    }
}
