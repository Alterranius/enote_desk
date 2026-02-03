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
using System.Windows.Shapes;

namespace eNote_desk.Wins
{
    /// <summary>
    /// Логика взаимодействия для DepartChoseWin.xaml
    /// </summary>
    public partial class DepartChoseWin : Window
    {
        private readonly Menu menu;
        public DepartChoseWin(string token, Menu menu, Project project)
        {
            InitializeComponent();
            DepartVM vm = new DepartVM(token, project);
            DataContext = vm;
            this.menu = menu;
        }

        private void btnAssign_Click(object sender, RoutedEventArgs e)
        {
            if (cbDepart.SelectedItem == null)
            {
                MessageBox.Show("Выберите отдел!");
                return;
            }
            menu.ChosenDepart = cbDepart.SelectedItem as Depart;
            Close();
        }
    }
}
