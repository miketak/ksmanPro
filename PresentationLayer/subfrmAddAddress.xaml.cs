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
        /// Holds old Address Type Name for Combo box selection change
        /// </summary>
        string _oldAddressTypeName;

        /// <summary>
        /// Constructor call in edit mode.
        /// </summary>
        public subfrmAddAddress(List<Address> address)
        {
            _addressList = address;
            isEditMode = true;
            InitializeComponent();
            setupWindow();
        }

        /// <summary>
        /// Constructor call in add mode
        /// </summary>
        public subfrmAddAddress()
        {
            isEditMode = false;
            InitializeComponent();
            setupWindow();
        }


        /// <summary>
        /// Sets Up Window
        /// </summary>
        private void setupWindow()
        {
            if (isEditMode)
            {
                //Set form title
                this.Title = "Edit Address";


                fillComboBoxes();

                //Set Address Type
                AddressType addressType = _addressTypes.Find(x => x.AddressTypeId == _addressList[0].AddressTypeId);
                cmbAddressType.SelectedItem = addressType.Name;
                _oldAddressTypeName = addressType.Name; //setting instance variable

                //Initial Setting
                txtAddressLine1.Text = _addressList[0].AddressLines[0];
                txtAddressLine2.Text = _addressList[0].AddressLines[1];
                txtAddressLine3.Text = _addressList[0].AddressLines[2];
                txtCity.Text = _addressList[0].City;

                //Select Employee State
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

        /// <summary>
        /// Fills Combo Boxes
        /// </summary>
        public void fillComboBoxes()
        {
            var employeeManager = new EmployeeManager();
            var utilityManager = new UtilityManager();

            //Execute for Edit Mode - False or True------------------------------------------

            //fill country box
            _countries = utilityManager.RetrieveCountries();
            foreach (Country c in _countries)
            {
                cmbCountry.Items.Add(c.NiceName);
            }

            //load addressesOfEmployee types into memory
            _addressTypes = employeeManager.RetrieveAddressTypeByID(-1, true);

            //load states box
            _states = utilityManager.RetrieveStates();
            foreach (State st in _states)
            {
                cmbState.Items.Add(st.StateCode);
            }


            if (isEditMode)
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
                foreach (var ad in _addressTypes)
                {
                    cmbAddressType.Items.Add(ad.Name);
                }

            }


        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmbAddressType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
       
            if (_addressList != null)
            {
                Console.WriteLine("Address List Count on selection change = " + _addressList.Count().ToString());

                //Update _addressList
                updateAddressList();

                //clear all text boxes
                txtAddressLine1.Clear();
                txtAddressLine2.Clear();
                txtAddressLine3.Clear();
                txtCity.Clear();
                cmbState.SelectedItem = null;
                txtZip.Clear();
                cmbCountry.SelectedItem = null;


                //Get Selection and addresstypeID
                string selectedAddressText = (string)cmbAddressType.SelectedItem;
                var selectedAddress = _addressTypes.Find(x => x.Name == selectedAddressText);

                //Search in employee object for addressesOfEmployee
                var addressFromEmployee = _addressList;
                var newAddressToDisplay = addressFromEmployee.Find(x => x.AddressTypeId == selectedAddress.AddressTypeId);

                if (newAddressToDisplay != null)
                {
                    //Set New Information
                    txtAddressLine1.Text = newAddressToDisplay.AddressLines[0];
                    txtAddressLine2.Text = newAddressToDisplay.AddressLines[1];
                    txtAddressLine3.Text = newAddressToDisplay.AddressLines[2];
                    txtCity.Text = newAddressToDisplay.City;
                    txtZip.Text = newAddressToDisplay.Zip;

                    //Set Combo Boxes
                    //State
                    State employeesState = _states.Find(x => x.StateID == newAddressToDisplay.StateID);
                    cmbState.SelectedItem = employeesState.StateCode;

                    //Country
                    Country employeesCountry = _countries.Find(x => x.CountryID == newAddressToDisplay.CountryID);
                    cmbCountry.SelectedItem = employeesCountry.NiceName;

                }

            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Updates Local Address List
        /// </summary>
        private void updateAddressList()
        {
            //int addressCount = _addressTypes.Count();

            if (txtAddressLine1.Text.Count() + txtAddressLine2.Text.Count() + txtAddressLine3.Text.Count() != 0)
            {

                //int addressIndex = 0;
                foreach (var address in _addressList)
                {
                    Console.WriteLine(address.AddressTypeId.ToString() );

                        if ( address.AddressTypeId ==  _addressTypes.Find( x => x.Name == _oldAddressTypeName/*(string)cmbAddressType.SelectedItem*/ ).AddressTypeId )//doesn't change
                        {
                            int addressIndex = address.AddressTypeId - 1;
                            Console.WriteLine("Address Type Name: " + _addressTypes.Find( x => x.AddressTypeId == address.AddressTypeId).Name );
                            Console.WriteLine("Selected Item: " + (string)cmbAddressType.SelectedItem);
                            Console.WriteLine("@ Address List Index: " + addressIndex);
                            int lineIndex = 0;

                            if (
                                _addressList[addressIndex].AddressLines[lineIndex] != txtAddressLine1.Text ||
                                _addressList[addressIndex].AddressLines[lineIndex + 1] != txtAddressLine2.Text ||
                                _addressList[addressIndex].AddressLines[lineIndex + 2] != txtAddressLine3.Text ||
                                _addressList[addressIndex].City != txtCity.Text ||
                                _addressList[addressIndex].StateID != _states.Find(x => x.StateCode == (string)cmbState.SelectedItem).StateID ||
                                _addressList[addressIndex].CountryID != _countries.Find(x => x.NiceName == (string)cmbCountry.SelectedItem).CountryID

                                )
                            {

                                Console.WriteLine("I have set the textboxes");

                                _addressList[addressIndex].AddressLines[lineIndex] = txtAddressLine1.Text;
                                _addressList[addressIndex].AddressLines[lineIndex + 1] = txtAddressLine2.Text;
                                _addressList[addressIndex].AddressLines[lineIndex + 2] = txtAddressLine2.Text;
                                _addressList[addressIndex].City = txtCity.Text;
                                _addressList[addressIndex].StateID = _states.Find(x => x.StateCode == (string)cmbState.SelectedItem).StateID;
                                _addressList[addressIndex].CountryID = _countries.Find(x => x.NiceName == (string)cmbCountry.SelectedItem).CountryID;

                            }  
                        } // end of if statement
                        else if (  _addressList.Count() < cmbAddressType.SelectedIndex && txtAddressLine1.Text.Count() + txtAddressLine2.Text.Count() + txtAddressLine3.Text.Count() != 0 )
                        {
                            var newAddress = new Address();
                            newAddress.AddressLines.Add(txtAddressLine1.Text);
                            newAddress.AddressLines.Add(txtAddressLine2.Text);
                            newAddress.AddressLines.Add(txtAddressLine3.Text);
                            newAddress.City = txtCity.Text;
                            newAddress.StateID = _states.Find(x => x.StateCode == (string)cmbState.SelectedItem).StateID;
                            newAddress.CountryID = _countries.Find(x => x.NiceName == (string)cmbCountry.SelectedItem).CountryID;
                            _addressList.Add(newAddress);
                        }
                        
                } //end of foreach statement [addresses]      
            }
            _oldAddressTypeName = (string)cmbAddressType.SelectedItem;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //Raise event to update parent form
            this.UpdateParentForm(e);

        }

        /// <summary>
        /// Event Handler to update parent form
        /// </summary>
        public static event EventHandler UpdateEvent;
        private void UpdateParentForm(EventArgs e)
        {
            if (_addressList != null)
            {
                UpdateEvent(this, e);
            }
        }

        /// <summary>
        /// Method Declaration for Parent form Call
        /// </summary>
        /// <returns></returns>
        public List<Address> getUpdatedAddress()
        {
            return _addressList;
        }


    }
}
