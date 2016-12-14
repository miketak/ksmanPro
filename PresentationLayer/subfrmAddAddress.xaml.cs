using BusinessLogic;
using DataObjects;
using DataObjects.ProgramDataObjects;
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
    /// Interaction logic for subfrmAddAddress.xaml
    /// </summary>
    public partial class subfrmAddAddress
    {
        /// <summary>
        /// Address instance variable from employee page
        /// </summary>
        List<Address> _addressList;

        /// <summary>
        /// List of addressesOfEmployee types
        /// </summary>
        List<AddressType> _addressTypes;

        /// <summary>
        /// List of States
        /// </summary>
        List<State> _states;

        /// <summary>
        /// Indicates if in editAddress mode
        /// </summary>
        bool isEditMode;

        /// <summary>
        /// Countries
        /// </summary>
        List<Country> _countries;



        /// <summary>
        /// Add addressesOfEmployee to Parent form Manage Employee
        /// </summary>
        public subfrmAddAddress(List<Address> address)
        {
            _addressList = address;
            isEditMode = true;
            InitializeComponent();
            setupWindow();
        }

        public subfrmAddAddress( )
        {
            isEditMode = false;
            InitializeComponent();
            setupWindow();
        }

        private void setupWindow()
        {
            if (isEditMode)
            {
                this.Title = "Edit Address";

                fillComboBoxes();

                //Set Address Type
                AddressType addressType = _addressTypes.Find(x => x.AddressTypeId == _addressList[0].AddressTypeId);
                cmbAddressType.SelectedItem = addressType.Name;

                //Initial Setting
                txtAddressLine1.Text = _addressList[0].AddressLines[0];
                txtAddressLine2.Text = _addressList[0].AddressLines[1];
                txtAddressLine3.Text = _addressList[0].AddressLines[2];
                txtCity.Text = _addressList[0].City;     
           
                State employeesState = _states.Find(x => x.StateID == _addressList[0].StateID);
                cmbState.SelectedItem = employeesState.StateCode;

                txtZip.Text = _addressList[0].Zip;

                Country employeesCountry = _countries.Find(x => x.CountryID == _addressList[0].CountryID);
                cmbCountry.SelectedItem = employeesCountry.NiceName;

               
                
            }
            else
            {
                this.Title = "Add Address";
                fillComboBoxes();
            }

        }

        public void fillComboBoxes()
        {
            var employeeManager = new EmployeeManager();

            //Execute for Edit Mode - False or True------------------------------------------

            //fill country box
            _countries = employeeManager.RetrieveCountries();
            foreach (Country c in _countries)
            {
                cmbCountry.Items.Add(c.NiceName);
            }

            //load addressesOfEmployee types into memory
            _addressTypes = employeeManager.RetrieveAddressTypeByID(-1, true);

            //load states box
            _states = employeeManager.RetrieveStates();
            foreach ( State st in _states)
            {
                cmbState.Items.Add(st.StateCode);
            }


            if ( isEditMode )
            {
                //fill only loaded employee types
                //foreach (var addType in _addressList)
                //{
                //    AddressType addressType = _addressTypes.Find(x => x.AddressTypeId == addType.AddressTypeId);
                //    cmbAddressType.Items.Add(addressType.Name);
                //}
                // Add all types into combo box
                foreach (var ad in _addressTypes)
                {
                    cmbAddressType.Items.Add(ad.Name);
                }

                


                //add new address functionality required
            }
            else
            {
                //Add all types into combo box
                foreach ( var ad in _addressTypes)
                {
                    cmbAddressType.Items.Add(ad.Name);
                }

            }


        }





        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
