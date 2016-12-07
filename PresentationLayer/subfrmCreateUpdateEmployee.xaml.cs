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
    /// Interaction logic for subfrmCreateUpdateEmployee.xaml
    /// </summary>
    public partial class subfrmCreateUpdateEmployee
    {

        /// <summary>
        /// Constructor information of Current User and Employee information to Edit
        /// </summary>
        public subfrmCreateUpdateEmployee()
        {
            InitializeComponent();
        }

        private void btnEditAddress(object sender, RoutedEventArgs e)
        {
            var subfrmAddAddress = new subfrmAddAddress();
            subfrmAddAddress.ShowDialog();
        }

        private void btnAddPersonEmails(object sender, RoutedEventArgs e)
        {
            var subfrmAddEmail = new subfrmAddEmail();
            subfrmAddEmail.ShowDialog();
        }

        private void btnAddPersonalTelephone(object sender, RoutedEventArgs e)
        {
            var subfrmAddPersonalTelephone = new subfrmAddPersonalTelephone();
            subfrmAddPersonalTelephone.ShowDialog();
        }
    }
}
