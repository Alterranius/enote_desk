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
using System.Windows.Shapes;

namespace eNote_desk.Wins
{
    /// <summary>
    /// Логика взаимодействия для TaskDetails.xaml
    /// </summary>
    public partial class TaskDetails : Window
    {
        private static TaskDetails win;
        public TaskDetails(string token, Project project)
        {
            InitializeComponent();
            TaskVM vm = new TaskVM(token, project, true);
            DataContext = vm;
            btnDelete.Visibility = Visibility.Hidden;
            btnAdd.Command = vm.PostTaskAddClick;
            btnComment.IsEnabled = false;
            tbComment.IsEnabled = false;
            win = this;
        }

        public TaskDetails(string token, Task task)
        {
            InitializeComponent();
            TaskVM vm = new TaskVM(token, task);
            DataContext = vm;
            btnDelete.Command = vm.DeleteTaskRemoveClick;
            btnDelete.CommandParameter = vm.SelectedTask;
            btnAdd.Command = vm.PutTaskUpdateClick;
            btnAdd.CommandParameter = vm.SelectedTask;
            win = this;
            if (task.Account != null)
                cbAccount.SelectedItem = vm.AccountsCmb.FirstOrDefault(x => x.Id == task.Account.Id);
            if (task.Team != null)
                cbTeam.SelectedItem = vm.TeamsCmb.FirstOrDefault(x => x.Id == task.Team.Id);
            try
            {
                cbCategory.SelectedItem = vm.CategoriesCmb.FirstOrDefault(x => x.Id == task.TaskCategory.Id);
                cbStatus.SelectedItem = vm.StatusCmb.FirstOrDefault(x => x.Id == task.TaskStatus.Id);
                cbType.SelectedItem = vm.TypesCmb.FirstOrDefault(x => x.Id == task.TaskType.Id);
            }
            catch (Exception e)
            {
            }
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
