using eNote_desk.Models;
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
    /// Логика взаимодействия для Invite.xaml
    /// </summary>
    public partial class Invite : Window
    {
        private static Invite win;
        public Invite(string token, Project project)
        {
            InitializeComponent();
            DataContext = new InviteVM(token, project);
            win = this;
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
