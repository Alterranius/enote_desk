using eNote_desk.Models;
using eNote_desk.ViewModels.Appeals;
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

namespace eNote_desk.Pages.Appeals
{
    /// <summary>
    /// Логика взаимодействия для Appeals.xaml
    /// </summary>
    public partial class Appeals : Page
    {
        private readonly string token;
        private readonly Project project;
        private readonly AppealVM vm;
        public Appeals(string token, Project project)
        {
            InitializeComponent();
            AppealVM vm = new AppealVM(token, project, true);
            DataContext = vm;
            this.token = token;
            this.project = project;
            this.vm = vm;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            new AppealDetails(token, project).ShowDialog();
            vm.GetAppeals();
        }

        private void lvAppeals_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvAppeals.SelectedItem != null)
            {
                new AppealDetails(token, lvAppeals.SelectedItem as Appeal, project).ShowDialog();
                vm.GetAppeals();
            }
        }
    }
}
