using eNote_desk.Models;
using eNote_desk.ViewModels.Departs;
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

namespace eNote_desk.Pages.Departs
{
    /// <summary>
    /// Логика взаимодействия для DepartManagement.xaml
    /// </summary>
    public partial class DepartManagement : Page
    {
        private readonly string token;
        private readonly Project project;
        private readonly DepartVM vm;
        public DepartManagement(string token, Project project)
        {
            InitializeComponent();
            DepartVM vm = new DepartVM(token, project);
            DataContext = vm;
            btnDelete.Command = vm.DeleteDepartRemoveClick;
            btnDelete.CommandParameter = vm.SelectedDepart;
            this.token = token;
            this.project = project;
            this.vm = vm;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            new DepartDetails(token, project).ShowDialog();
            vm.GetDeparts();
        }

        private void lvDeparts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvDeparts.SelectedItem != null)
            {
                new DepartDetails(token, lvDeparts.SelectedItem as Depart).ShowDialog();
                vm.GetDeparts();
            }
        }
    }
}
