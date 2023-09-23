using eNote_desk.Models;
using eNote_desk.ViewModels.Delegations;
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
    /// Логика взаимодействия для DelegationDetails.xaml
    /// </summary>
    public partial class DelegationDetails : Window
    {
        private static DelegationDetails win;
        public DelegationDetails(string token, Project project)
        {
            InitializeComponent();
            DelegationVM vm = new DelegationVM(token, project);
            DataContext = vm;
            btnAdd.Command = vm.PostDelegationAddClick;
            btnAdd.CommandParameter = null;
            btnDelete.Visibility = Visibility.Hidden;
            win = this;
        }

        public DelegationDetails(string token, Delegation delegation, Project project)
        {
            InitializeComponent();
            DelegationVM vm = new DelegationVM(token, delegation, project);
            DataContext = vm;
            btnAdd.Command = vm.PutDelegationUpdateClick;
            btnDelete.Command = vm.DeleteDelegationRemoveClick;
            btnAdd.CommandParameter = vm.Delegation;
            btnDelete.CommandParameter = vm.Delegation;
            btnComplete.Command = vm.PutDelegationCompleteClick;
            btnComplete.CommandParameter = vm.Delegation;
            if (!string.IsNullOrEmpty(delegation.CompletedAt))
            {
                btnComplete.IsEnabled = false;
            }
            win = this;
            if (vm.Delegation.Receiver != null)
                cbReciever.SelectedItem = vm.AccountsCmb.FirstOrDefault(x => x.Id == delegation.Receiver.Id);
            if (vm.Delegation.Sender != null)
                cbSender.SelectedItem = vm.AccountsCmb.FirstOrDefault(x => x.Id == delegation.Receiver.Id);
            if (vm.Delegation.Task != null)
                cbTask.SelectedItem = vm.TasksCmb.FirstOrDefault(x => x.Id == delegation.Task.Id);
        }

        public static void Complete()
        {
            if (win != null)
            {
                win.btnComplete.IsEnabled = false;
            }
        }
        public static void Performed()
        {
            if (win != null)
            {
                win.Close();
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
