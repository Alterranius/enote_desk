using eNote_desk.ViewModels.Projects;
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
using System.Windows.Shapes;

namespace eNote_desk.Wins
{
    /// <summary>
    /// Логика взаимодействия для ProjectDetails.xaml
    /// </summary>
    public partial class ProjectDetails : Window
    {
        private static ProjectDetails win;
        public ProjectDetails(string token)
        {
            InitializeComponent();
            ProjectVM vm = new ProjectVM(token);
            DataContext = vm;
            btnDelete.Visibility = Visibility.Hidden;
            btnAdd.Command = vm.PostProjectAddClick;
            win = this;
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
