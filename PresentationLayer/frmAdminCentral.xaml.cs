using DataObjects;
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
using System.Windows.Shapes;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for frmAdminCentral.xaml
    /// </summary>
    public partial class frmAdminCentral
    {
        User _user = null;

        public frmAdminCentral(User user)
        {
            InitializeComponent();
            _user = user;
            txtName.Text = _user.FirstName + " " + _user.LastName;
        }

        private void btnManageDepartment(object sender, RoutedEventArgs e)
        {

        }

        private void btnManageEmployees(object sender, RoutedEventArgs e)
        {

        }

        private void btnLeaveSettings(object sender, RoutedEventArgs e)
        {

        }
    }
}
