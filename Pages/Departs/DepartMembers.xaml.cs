using eNote_desk.Models;
using eNote_desk.Pages.Shared;
using eNote_desk.ViewModels.Departs;
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
    /// Логика взаимодействия для DepartMembers.xaml
    /// </summary>
    public partial class DepartMembers : Page
    {
        private readonly string token;
        private readonly Frame frame;
        private readonly Project actual;
        public DepartMembers(string token, Project project, Frame frame)
        {
            InitializeComponent();
            DepartVM vm = new DepartVM(token, project);
            DataContext = vm;
            this.token = token;
            this.frame = frame;
            actual = project;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (lvDeparts.SelectedItem != null)
            {
                frame.Navigate(new RoleMembers(token, lvDeparts.SelectedItem as Depart, frame, actual));
            }
        }
    }
}
