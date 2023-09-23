using eNote_desk.Models;
using eNote_desk.ViewModels.Appeals;
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
    /// Логика взаимодействия для AppealDetails.xaml
    /// </summary>
    public partial class AppealDetails : Window
    {
        private static AppealDetails win;
        private readonly string token;
        private readonly Appeal appeal;
        private readonly bool isResponse;
        private readonly Project project;
        public AppealDetails(string token, Appeal appeal, Project project)
        {
            InitializeComponent();
            this.token = token;
            this.appeal = appeal;
            isResponse = Convert.ToBoolean(appeal.IsResponse);
            AppealVM vm = new AppealVM(token, appeal, project);
            DataContext = vm;
            cbReciever.SelectedItem = appeal.Receiver;
            cbReciever.IsEditable = false;
            btnAdd.Command = vm.PutAppealUpdateClick;
            btnAdd.CommandParameter = vm.SelectedAppeal;
            win = this;
            this.project = project;
            if (vm.SelectedAppeal.Receiver != null)
                cbReciever.SelectedItem = vm.AccountsCmb.FirstOrDefault(x => x.Id == appeal.Receiver.Id);
            if (vm.SelectedAppeal.Sender != null)
                cbSender.SelectedItem = vm.AccountsCmb.FirstOrDefault(x => x.Id == appeal.Sender.Id);
        }
        public AppealDetails(string token, Project project)
        {
            InitializeComponent();
            this.token = token;
            isResponse = false;
            btnDelete.Visibility = Visibility.Hidden;
            AppealVM vm = new AppealVM(token, project);
            DataContext = vm;
            btnAdd.Command = vm.PostAppealAddClick;
            win = this;
            this.project = project;
            try
            {
                if (vm.SelectedAppeal.Receiver != null)
                    cbReciever.SelectedItem = vm.AccountsCmb.FirstOrDefault(x => x.Id == appeal.Receiver.Id);
                if (vm.SelectedAppeal.Sender != null)
                    cbSender.SelectedItem = vm.AccountsCmb.FirstOrDefault(x => x.Id == appeal.Sender.Id);
            }
            catch (Exception e)
            {
            }
        }
        public AppealDetails(string token, Account receiver, Project project)
        {
            InitializeComponent();
            this.token = token;
            isResponse = true;
            btnAnswer.Visibility = Visibility.Hidden;
            btnDelete.Visibility = Visibility.Hidden;
            AppealVM vm = new AppealVM(token, project);
            vm.IsResponse = true;
            vm.Receiver = receiver;
            cbReciever.ItemsSource = vm.AccountsCmb;
            cbReciever.IsEditable = false;
            DataContext = vm;
            btnAdd.Command = vm.PostAppealAddClick;
            win = this;
            this.project = project;
            if (vm.SelectedAppeal.Sender != null)
                cbSender.SelectedItem = vm.AccountsCmb.FirstOrDefault(x => x.Id == appeal.Sender.Id);
            vm.IsResponse = true;
            cbReciever.SelectedItem = vm.AccountsCmb.FirstOrDefault(x => x.Id == receiver.Id);
        }

        private void btnAnswer_Click(object sender, RoutedEventArgs e)
        {
            new AppealDetails(token, appeal.Sender, project).ShowDialog();
            Close();
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
