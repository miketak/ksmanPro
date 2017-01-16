using BusinessLogic;
using DataObjects;
using DataObjects.ProgramDataObjects;
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
        /// <summary>
        /// Current User
        /// </summary>
        User _user = null;

        /// <summary>
        /// Central Dashboard Constructor
        /// </summary>
        /// <param name="user"></param>
        public frmCentralDashboard(User user)
        {
            _user = user;
            InitializeComponent();
            RetrieveUserAccess();
            SetupWindow();
        }

        private void RetrieveUserAccess()
        {
            var m = new UserManager();

            _user.ClearanceAccess = m.RetrieveUserAccess(_user);
        }

        private void SetupWindow()
        {
            //Set first and last name
            txtName.Text = _user.FirstName + " " + _user.LastName;

            //Set status message
            txtStatusMessage.Content += "Welcome back " + _user.FirstName + " " + _user.LastName;

            //Hide All Buttons
            HideAllButtons();

            //Setup Available Buttons
            foreach (var k in _user.ClearanceAccess)
            {
                EnforceUserAccess(k.FeatureName.Replace(" ", ""), k.hasAccess);
            }
            

            
        }

        private void EnforceUserAccess(string feature, bool access)
        {

           

            UserFeatures uf;

            if ( !Enum.TryParse(feature.ToUpper(), out uf))
            {
                MessageBox.Show("Enum Parse Error " + feature.ToUpper() );
            }
            

            switch (uf)
            {
                case UserFeatures.ADMINCENTRAL:
                    if (access)
                        adminCentral.Visibility = Visibility.Visible;
                    break;
                case UserFeatures.EMPLOYEECENTRAL:
                    if (access)
                        employeeCentral.Visibility = Visibility.Visible;
                    break;
                case UserFeatures.SCANNOUNCEMENTS:
                    if (access)
                        scannouncement.Visibility = Visibility.Visible;
                    break;
                case UserFeatures.SCHRM:
                    if ( access )
                        schrm.Visibility = Visibility.Visible;
                    break;
                case UserFeatures.TIMEENTRYAPP:
                    if ( access )
                        timeEntryApp.Visibility = Visibility.Visible;
                    break;
            }
            
        }

        private void HideAllButtons()
        {
            timeEntryApp.Visibility = Visibility.Collapsed;
            employeeCentral.Visibility = Visibility.Collapsed;
            adminCentral.Visibility = Visibility.Collapsed;
            leaveCentral.Visibility = Visibility.Collapsed;
            scjobPro.Visibility = Visibility.Collapsed;
            scSales.Visibility = Visibility.Collapsed;
            scLogistics.Visibility = Visibility.Collapsed;
            scLearningModules.Visibility = Visibility.Collapsed;
            scProjectsPortal.Visibility = Visibility.Collapsed;
            scCareersPortal.Visibility = Visibility.Collapsed;
            schrm.Visibility = Visibility.Collapsed;
            scannouncement.Visibility = Visibility.Collapsed;
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

        /// <summary>
        /// Toggles fly out open and closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            mySettings.IsOpen = true;
        }

        /// <summary>
        /// Toggles fly out open and closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            mySettings.IsOpen = false;
        }

        /// <summary>
        /// Opens Admin Central App
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdminCentral(object sender, RoutedEventArgs e)
        {
            //this.Hide();
            var frmAdminCentral = new frmAdminCentral(_user);
            frmAdminCentral.Show();
        }

        /// <summary>
        /// Update Password function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChangePassword(object sender, RoutedEventArgs e)
        {
            
            var mySettings = new MetroDialogSettings()
            {
                ColorScheme = MetroDialogColorScheme.Theme
            };

            var subfrmChangePassword = new subfrmChangePassword(_user);
            subfrmChangePassword.ShowDialog();

            
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }



 
    }
}
