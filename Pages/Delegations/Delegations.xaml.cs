using eNote_desk.Models;
using eNote_desk.ViewModels.Delegations;
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

namespace eNote_desk.Pages.Delegations
{
    /// <summary>
    /// Логика взаимодействия для Delegations.xaml
    /// </summary>
    public partial class Delegations : Page
    {
        private readonly string token;
        private readonly Project project;
        private readonly DelegationVM vm;
        public Delegations(string token, Project project)
        {
            InitializeComponent();
            DelegationVM vm = new DelegationVM(token, project);
            DataContext = vm;
            this.token = token;
            this.project = project;
            this.vm = vm;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            new DelegationDetails(token, project).ShowDialog();
            vm.GetDelegations();
        }

        private void lvDelegations_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvDelegations.SelectedItem != null)
            {
                new DelegationDetails(token, lvDelegations.SelectedItem as Delegation, project).ShowDialog();
                vm.GetDelegations();
            }
        }
    }
}
