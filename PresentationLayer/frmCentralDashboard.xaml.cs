using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
    /// Interaction logic for frmCentralDashboard.xaml
    /// </summary>
    public partial class frmCentralDashboard 
    {
        public frmCentralDashboard()
        {
            InitializeComponent();
        }

        private async void tryMe_Click(object sender, RoutedEventArgs e)
        {
             var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "OK",
                ColorScheme = MetroDialogColorScheme.Theme
            };

            await this.ShowMessageAsync("Hello", "This is information",
                MessageDialogStyle.Affirmative, mySettings);
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            myFlyOut.IsOpen = true;
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            myFlyOut.IsOpen = false;
        }

 
    }
}
