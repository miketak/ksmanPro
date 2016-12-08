using BusinessLogic;
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

        /// <summary>
        /// Constructor Edit Mode
        /// </summary>
        public subfrmCreateUpdateEmployee(User user, String employeeUsername)
        {
            _user = user;
            _employeeUsername = employeeUsername;
            InitializeComponent();
            setupWindowEditMode();
            
        }
        
        /// <summary>
        /// Constructor for Add Mode
        /// </summary>
        /// <param name="user"></param>
        public subfrmCreateUpdateEmployee(User user)
        {
            _user = user;
            InitializeComponent();
            setupWindowAddMode();

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

        /// <summary>
        /// Sets up window elements from User information
        /// </summary>
        private void setupWindowEditMode()
        {
            //Set up user environment
            txtNameTag.Text = _user.FirstName + " " + _user.LastName;
            
            //Sets _employee private field with employee data
            getEmployeeDetails(_employeeUsername);
            this.Title = "Edit Employee: " + _employee.FirstName + " " + _employee.LastName;

            //Set all combo boxes
            fillComboBoxes();


            //Set all textboxes for Employee information
            txtUsername.Text = _employee.Username;
            txtFirstName.Text = _employee.FirstName;
            txtLastName.Text = _employee.LastName;
            txtOtherNames.Text = _employee.OtherNames;
            cmbDepartment.SelectedItem = _employee.Department;

            //Get selected role
            UserRoles loadedEmployeeRole = _jobPositions.Find( x => x.UserRolesId == _employee.UserRolesId );
            cmbJobPosition.SelectedItem = loadedEmployeeRole.Name;

            //Active or Inactive
            chkisActive.IsChecked = _employee.isEmployed;
        }

        private void setupWindowAddMode()
        {
            //Setup user environment
            txtNameTag.Text = _user.FirstName + " " + _user.LastName;
            fillComboBoxes();

        }

        /// <summary>
        /// Fills all combo boxes
        /// </summary>
        private void fillComboBoxes()
        {
            //fill department box
            List<Department> departments = new List<Department>();
            var employeeManager = new EmployeeManager();

            //retrieve visible departments
            departments = employeeManager.RetrieveDepartmentsByVisibility(true);
            foreach ( Department d in departments)
            {
                cmbDepartment.Items.Add(d.Name);
            }
            

            //fill jobs position box based on selected department
            fillJobPositions(_employee.DepartmentId);

            //fill clearance levels
            //cmbClearanceLevel.Items.Add

            //fill nationality box
            //cmbNationality.Items.Add

            //fill email types
            //cmbEmailTypes.Items.Add

            //fill telephone types
            //cmbTelephoneTypes.Items.Add

            //fill gender types
            cmbGender.Items.Add("Male");
            cmbGender.Items.Add("Female");

            //Address types
            //cmbAddressTypes.Items.Add

        }

        /// <summary>
        /// Fill departments based on Department Id.
        /// </summary>
        /// <param name="departmentId"></param>
        private void fillJobPositions(string departmentId)
        {
            //var jobPositions = new List<UserRoles>();
            var employeeManager = new EmployeeManager();
            bool isRetrieveAll = false;
            _jobPositions = employeeManager.RetrieveJobPositionByDeptId(departmentId, isRetrieveAll);    

            foreach( UserRoles roles in _jobPositions)
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
            _employee = ld.RetrieveEmployeeByUsername(employeeUsername);

        }
    }
}
