using BusinessLogic;
using DataObjects;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        internal readonly string failure = "Wrong username or password.";
        User _user = null;


        public MainWindow()
        {
            InitializeComponent();
            txtUsername.Focus();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var authenticate = new UserManager();

            try
            {
                _user = authenticate.authenticateUser(txtUsername.Text, txtPassword.Password);
                this.Hide();
                MessageBox.Show("Welcome back, " + _user.FirstName + ".");
                var frmCentralDashboard = new frmCentralDashboard();
                frmCentralDashboard.ShowDialog();
            }
            catch (Exception ex)
            {
                lblPrompt.Content = failure;
                txtUsername.BorderBrush = Brushes.Red;
                txtPassword.BorderBrush = Brushes.Red;
                txtUsername.Focus();
            }
 
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtUsername.BorderBrush = Brushes.Blue;
            lblPrompt.Content = "";

        }


        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            txtPassword.BorderBrush = Brushes.Blue;
            lblPrompt.Content = "";
        }


    }
}
