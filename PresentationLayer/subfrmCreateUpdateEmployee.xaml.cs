using BusinessLogic;
using DataObjects;
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
    /// Interaction logic for subfrmCreateUpdateEmployee.xaml
    /// </summary>
    public partial class subfrmCreateUpdateEmployee
    {
        /// <summary>
        /// Current user logged into the system
        /// </summary>
        User _user = new User();

        /// <summary>
        /// Employee from database
        /// </summary>
        Employee _employee = new Employee();

        /// <summary>
        /// Employee username from calling class.
        /// </summary>
        String _employeeUsername;


        /// <summary>
        /// Job positions field for form
        /// </summary>
        List<UserRoles> _jobPositions = new List<UserRoles>();

        List<String> _countries = new List<String>();

        /// <summary>
        /// Indicates whether form is in edit mode
        /// </summary>
        bool _isEditMode;

        /// <summary>
        /// Department lists for departments combobox
        /// </summary>
        List<Department> _departments = new List<Department>();

        /// <summary>
        /// Clearance Levels list for Clearance Level Combo Box
        /// </summary>
        List<ClearanceLevel> _clearanceLevels = new List<ClearanceLevel>();

        /// <summary>
        /// Constructor Edit Mode
        /// </summary>
        public subfrmCreateUpdateEmployee(User user, String employeeUsername)
        {
            _user = user;
            _employeeUsername = employeeUsername;
            _isEditMode = true;
            InitializeComponent();
            setupWindow();

        }



        /// <summary>
        /// Constructor for Add Mode
        /// </summary>
        /// <param name="user"></param>
        public subfrmCreateUpdateEmployee(User user)
        {
            _user = user;
            _isEditMode = false;
            InitializeComponent();
            setupWindow();

        }


        /// <summary>
        /// Subform to edit and add new addresses to parent form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditAddress(object sender, RoutedEventArgs e)
        {
            var subfrmAddAddress = new subfrmAddAddress();
            subfrmAddAddress.ShowDialog();
        }

        /// <summary>
        /// Subform to edit and add new new emails to parent form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddPersonEmails(object sender, RoutedEventArgs e)
        {
            var subfrmAddEmail = new subfrmAddEmail();
            subfrmAddEmail.ShowDialog();
        }


        /// <summary>
        /// Subform to edit and add new personal telphone numbers to parent form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddPersonalTelephone(object sender, RoutedEventArgs e)
        {
            var subfrmAddPersonalTelephone = new subfrmAddPersonalTelephone();
            subfrmAddPersonalTelephone.ShowDialog();
        }

        /// <summary>
        /// Sets up window elements from User information
        /// </summary>
        private void setupWindow()
        {
            if (_isEditMode)
            {
                //Set up user environment
                txtNameTag.Text = _user.FirstName + " " + _user.LastName;

                //Sets _employee private field with employee data
                getEmployeeDetails(_employeeUsername);
                this.Title = "Edit Employee: " + _employee.FirstName + " " + _employee.LastName;

                //Set all combo boxes
                fillComboBoxes();

                //disable passwordbox
                txtPassword.IsEnabled = false;

                //Set all textboxes for Employee information
                txtUsername.Text = _employee.Username;
                txtFirstName.Text = _employee.FirstName;
                txtLastName.Text = _employee.LastName;
                txtOtherNames.Text = _employee.OtherNames;
                cmbDepartment.SelectedItem = _employee.Department;
                txtPersonalTelephone.Text = _employee.PersonalPhoneNumber;
                txtPersonalEmail.Text = _employee.PersonalEmail;
                cmbNationality.SelectedItem = _employee.Nationality;                 
                chkMaritalStatus.IsChecked = _employee.MaritalStatus ? true : false;
                if (_employee.Gender != null)
                    cmbGender.SelectedItem = _employee.Gender == true ? "Male" : "Female";
                dateDOB.SelectedDate = _employee.DateOfBirth;
                if ( dateDOB.SelectedDate == DateTime.MinValue )
                {
                    dateDOB.SelectedDate = null;
                }
                txtCompanyTelephone.Text = _employee.PhoneNumber;
                txtCompanyEmail.Text = _employee.Email;
                cmbClearanceLevel.SelectedItem = _employee.ClearanceLevel;
                chkisActive.IsChecked = _employee.isEmployed;
                txtAdditionalInfo.Text = _employee.AdditonalInfo;

                //Get selected role
                UserRoles loadedEmployeeRole = _jobPositions.Find(x => x.UserRolesId == _employee.UserRolesId);
                cmbJobPosition.SelectedItem = loadedEmployeeRole.Name;

                
                
            }
            else
            {
                //Setup user environment
                txtNameTag.Text = _user.FirstName + " " + _user.LastName;

                //disable passwordbox
                txtPassword.IsEnabled = false;

                //fillComboBoxes
                fillComboBoxes();

            }

        }

        /// <summary>
        /// Fills all combo boxes
        /// </summary>
        private void fillComboBoxes()
        {
            var employeeManager = new EmployeeManager();

            //fill gender types
            cmbGender.Items.Add("Male");
            cmbGender.Items.Add("Female");

            //fill departments combobox
            _departments = employeeManager.RetrieveDepartmentsByVisibility(true);
            foreach (Department d in _departments)
            {
                cmbDepartment.Items.Add(d.Name);
            }

            //fill nationality box
            _countries = employeeManager.RetrieveCountries();
            foreach (String s in _countries)
            {
                cmbNationality.Items.Add(s);
            }




            if (_isEditMode) //edit mode
            {
                
                //var employeeManager = new EmployeeManager();

                //fill jobs position box based on selected department               
                fillJobPositions(_employee.DepartmentId);

                //fill clearance levels
                _clearanceLevels = employeeManager.RetrieveClearanceByDeptID(_employee.DepartmentId, true);
                foreach (ClearanceLevel cl in _clearanceLevels)
                {
                    cmbClearanceLevel.Items.Add(cl.Name);
                }


                //fill email types
                //cmbEmailTypes.Items.Add

                //fill telephone types
                //cmbTelephoneTypes.Items.Add

                //Address types
                //cmbAddressTypes.Items.Add
            }
            else //add mode
            {
                //fill department all departments
                //_departments = employeeManager.RetrieveDepartmentsByVisibility(true);
                //foreach (Department d in _departments)
                //{
                //    cmbDepartment.Items.Add(d.Name);
                //}


                //fill clearance levels
                _clearanceLevels = employeeManager.RetrieveClearanceByDeptID("", true);
                foreach (ClearanceLevel cl in _clearanceLevels)
                {
                    cmbClearanceLevel.Items.Add(cl.Name);
                }

                //do not fill job positions box. indicate to user to select department
                cmbJobPosition.Items.Add("Select Department");
            }
            

        }

        /// <summary>
        /// Fill departments based on Department Id.
        /// </summary>
        /// <param name="departmentId"></param>
        private void fillJobPositions(string departmentId)
        {
            //fill job positions by department
            var employeeManager = new EmployeeManager();

            //clear job positions and combo box
            _jobPositions = null;
            cmbJobPosition.Items.Clear();

            //fill job positions
            _jobPositions = employeeManager.RetrieveJobPositionByDeptId(departmentId, false); //true means select all departments

            foreach (UserRoles roles in _jobPositions)
            {
                cmbJobPosition.Items.Add(roles.Name);
            }

        }


        /// <summary>
        /// Sets _employee id private field with employee basie date
        /// </summary>
        /// <param name="employeeUsername">Employee Username used for data retrieval</param>
        private void getEmployeeDetails(string employeeUsername)
        {
            var ld = new EmployeeManager();
            //Get full employee details
            try
            {
                _employee = ld.RetrieveEmployeeByUsername(employeeUsername);

            }
            catch (Exception e)
            {

                MessageBox.Show("Message" + e.Message);
            }
            

        }

        private void cmbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //clear _departments object
            _departments.Clear();
            
            //reload departments
            var employeeManager = new EmployeeManager();
            _departments = employeeManager.RetrieveDepartmentsByVisibility(true);


            //Get selected department id for _departments enumerable
            Department loadedDepartments = _departments.Find(x => x.Name == (string)cmbDepartment.SelectedItem);
            fillJobPositions(loadedDepartments.DepartmentId);
        
        }

        private async void btnCancel(object sender, RoutedEventArgs e)
        {
             var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Yes",
                NegativeButtonText = "Cancel",
                ColorScheme = MetroDialogColorScheme.Theme
            };

             MessageDialogResult result =  await this.ShowMessageAsync("Warning", "Closing might result in loss of Data. Do you still want to cancel?",
                 MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result == MessageDialogResult.Affirmative)
            {
                this.Close();
            }
            
        }
    }
}
