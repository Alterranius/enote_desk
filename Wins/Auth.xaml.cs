using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace eNote_desk.Wins
{
    /// <summary>
    /// Interaction logic for Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        public static Auth auth;
        public Auth()
        {
            InitializeComponent();
            auth = this;
        }

        public static void Complete(string token)
        {
            if (auth != null)
            {
                new Menu(token).Show();
                auth.Close();
            }
        }

        private void СbVisibility_Checked(object sender, RoutedEventArgs e)
        {
            tbPass.Visibility = Visibility.Visible;
            pbPass.Visibility = Visibility.Hidden;
            tbPass.Text = pbPass.Password;
        }

        private void СbVisibility_Unchecked(object sender, RoutedEventArgs e)
        {
            pbPass.Visibility = Visibility.Visible;
            tbPass.Visibility = Visibility.Hidden;
            pbPass.Password = tbPass.Text;
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            SignUp win = new SignUp();
            win.Show();
            Close();
        }
    }
}
