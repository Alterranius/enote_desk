using eNote_desk.Models;
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
    /// Логика взаимодействия для DepartData.xaml
    /// </summary>
    public partial class DepartData : Page
    {
        private readonly DepartVM vm;
        public DepartData(string token, Project project)
        {
            InitializeComponent();
            DepartVM vm = new DepartVM(token, project);
            this.vm = vm;
            vm.GetTaskStats();
            DataContext = vm;
        }

        private void lvDeparts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.GetTaskStats();
        }
    }
}
