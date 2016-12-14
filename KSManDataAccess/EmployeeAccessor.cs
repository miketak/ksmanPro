using DataObjects;
using DataObjects.ProgramDataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{

    /// <summary>
    /// Data Access for Employee Related Functionality
    /// </summary>
    public class EmployeeAccessor
    {

        /// <summary>
        /// Retrieve active or inactive employee based on paramater. Retrieves all details
        /// </summary>
        /// <param name="isEmployed"></param>
        /// <returns></returns>
        public static List<Employee> RetrieveEmployees(bool isEmployed)
        {
            var employeesInDB = new List<Employee>();

           //Getting connection
            var conn = DBConnection.GetConnection();

            //Using stored procedure
            var cmdText = @"sp_retrieve_user_by_activity";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;


            // add a parameter to the command
            int activeBit = isEmployed ? 1 : 0;
            cmd.Parameters.Add("@isEmployed", SqlDbType.Bit);
            cmd.Parameters["@isEmployed"].Value = activeBit;

            try
            {
                // you have to open a connection before using it
                conn.Open();

                // create a data reader with our command
                var reader = cmd.ExecuteReader();

                // check to make sure the reader has data
                if (reader.HasRows)
                {
                    // process the data reader
                    while (reader.Read())
                    {
                        // create an employee object
                        var emp = new Employee()
                        {
                            UserId = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            FirstName = reader.GetString(2),
                            LastName = reader.GetString(3),
                            OtherNames = reader.GetString(4),
                            PhoneNumber = reader.GetString(5),
                            Email = reader.GetString(6),
                            DepartmentId = reader.GetString(7),
                            Department = reader.GetString(8),
                            UserRolesId = reader.GetString(9),
                            JobDesignation = reader.GetString(10),
                            ClearanceLevelId = reader.GetInt32(11),
                            ClearanceLevel = reader.GetString(12),
                            isEmployed = reader.GetBoolean(13),
                            HireDate = reader.GetDateTime(14)
                        };

                        // Save Employees into List
                        employeesInDB.Add(emp);
                    }
                    // close the reader
                    reader.Close();
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was a problem retrieving your data.", ex);
            }
            finally
            {
                // final housekeeping
                conn.Close();
            }

            return employeesInDB;
        }

        /// <summary>
        /// Retrieve Departments based on visibility
        /// </summary>
        /// <param name="isVisible"></param>
        /// <returns>List of Department Objects</returns>
        public List<Department> RetrieveDepartments(bool isVisible)
        {
            var departmentsInDB = new List<Department>();

            //Getting connection
            var conn = DBConnection.GetConnection();

            //Using stored procedure
            var cmdText = @"sp_retrieve_departments";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;


            // add a parameter to the command
            int visibilityBit = isVisible ? 1 : 0;
            cmd.Parameters.Add("@Visibility", SqlDbType.Bit);
            cmd.Parameters["@Visibility"].Value = visibilityBit;

            try
            {
                // you have to open a connection before using it
                conn.Open();

                // create a data reader with our command
                var reader = cmd.ExecuteReader();

                // check to make sure the reader has data
                if (reader.HasRows)
                {
                    // process the data reader
                    while (reader.Read())
                    {
                        // create an employee object
                        var dept = new Department()
                        {
                            DepartmentId = reader.GetString(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Visibility = reader.GetBoolean(3)      
                        };

                        // Save Employees into List
                        departmentsInDB.Add(dept);
                    }
                    // close the reader
                    reader.Close();
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was a problem retrieving your data.", ex);
            }
            finally
            {
                // final housekeeping
                conn.Close();
            }

            return departmentsInDB;
        }

        /// <summary>
        /// Retrieve employee roles by department
        /// </summary>
        /// <param name="departmentId">Department Id employee for retrieval</param>
        /// <param name="isAll">Indicator to retrieve all roles irrespective departmentId</param>
        /// <returns>Returns a list of UserRole Objects</returns>
        public List<UserRoles> RetrieveUserRoles(string departmentId, bool isAll)
        {
            var userRolesInDb = new List<UserRoles>();

            //Getting connection
            var conn = DBConnection.GetConnection();

            //Using stored procedure
            var cmdText = @"sp_retrieve_userroles";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;


            // add a parameter to the command based on selection type
            cmd.Parameters.Add("@DepartmentId", SqlDbType.VarChar);
            string departmentIdParameter = isAll ? "dbsall" : departmentId;
            cmd.Parameters["@DepartmentId"].Value = departmentIdParameter;
            

            try
            {
                // you have to open a connection before using it
                conn.Open();

                // create a data reader with our command
                var reader = cmd.ExecuteReader();

                // check to make sure the reader has data
                if (reader.HasRows)
                {
                    // process the data reader
                    while (reader.Read())
                    {
                        // create an employee object
                        var roles = new UserRoles()
                        {
                            UserRolesId = reader.GetString(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            DepartmentId = reader.GetString(3)
                        };

                        // Save Employees into List
                        userRolesInDb.Add(roles);
                    }
                    // close the reader
                    reader.Close();
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was a problem retrieving employee roles data.", ex);
            }
            finally
            {
                // final housekeeping
                conn.Close();
            }

            return userRolesInDb;
            
        }

        /// <summary>
        /// Retrieves all data of a single employee.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static Employee RetrieveEmployeeByUsername(string username)
        {
            //Retrieve all details
            Employee employee = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_all_employee_data_by_username";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Username", SqlDbType.VarChar, 20);
            cmd.Parameters["@Username"].Value = username;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    employee = new Employee()
                    {
                        UserId = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        FirstName = reader.GetString(2),
                        LastName = reader.GetString(3),
                        OtherNames = reader.IsDBNull(4) ? null : reader.GetString(4),
                        DepartmentId = reader.IsDBNull(5) ? null : reader.GetString(5),
                        Department = reader.IsDBNull(6) ? null : reader.GetString(6),
                        PhoneNumber = reader.IsDBNull(7) ? null : reader.GetString(7),
                        Email = reader.IsDBNull(8) ? null : reader.GetString(8),
                        PicUrl = reader.IsDBNull(9) ? null : reader.GetString(9),
                        isEmployed = reader.IsDBNull(10) ? false : reader.GetBoolean(10),
                        isBlocked = reader.IsDBNull(11) ? false : reader.GetBoolean(11),
                        UserRolesId = reader.IsDBNull(12) ? null :  reader.GetString(12),
                        JobDesignation = reader.IsDBNull(13) ? null : reader.GetString(13), //role name
                        ClearanceLevelId = reader.IsDBNull(14) ? -1 : reader.GetInt32(14),
                        ClearanceLevel = reader.IsDBNull(15) ? null : reader.GetString(15),
                        PersonalInfoId = reader.IsDBNull(16) ? -1 : reader.GetInt32(16),
                        //Gender = reader.IsDBNull(17) ? null : reader.GetBoolean(17),
                        DateOfBirth = reader.IsDBNull(18) ? DateTime.MinValue : reader.GetDateTime(18),
                        CountryId = reader.IsDBNull(19) ? -1 : reader.GetInt32(19),
                        Nationality = reader.IsDBNull(20) ? null : reader.GetString(20),
                        AdditonalInfo = reader.IsDBNull(21) ? null : reader.GetString(21),
                        HireDate = reader.IsDBNull(22) ? DateTime.MinValue : reader.GetDateTime(22),
                        MaritalStatus = reader.IsDBNull(23) ? false : reader.GetBoolean(23),
                        PersonalEmail = reader.IsDBNull(24) ? null : reader.GetString(24),
                        PersonalPhoneNumber = reader.IsDBNull(25) ? null : reader.GetString(25)
                    };

                    //Null gender if not exists in database
                    if (reader.IsDBNull(17))
                        employee.Gender = null;
                    else
                        employee.Gender = reader.GetBoolean(17);

                }}
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            //Get List of Personal Addresses
            var addressList = new List<Address>();
            if ( employee.PersonalInfoId != -1)
            {
                addressList.Clear();
                addressList = retrieveAddressesByPeronalInfoId(employee.PersonalInfoId);
                employee.Address = addressList;
            }
            
            return employee;
           
        }

        /// <summary>
        /// Retrieves Addresses By Personal Information ID
        /// </summary>
        /// <param name="PersonalInformationID">Personal Information ID Parameter for Select Query</param>
        /// <returns>Returns a list of Addresses</returns>
        public static List<Address> retrieveAddressesByPeronalInfoId(int PersonalInformationID) //change back to private
        {
            //Retrieve all details
            Address address = null;
            var addressList = new List<Address>();
            var addressLinesInDB = new List<String>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_address_by_personalID";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PersonalInformationID", SqlDbType.Int);
            cmd.Parameters["@PersonalInformationID"].Value = PersonalInformationID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                // check to make sure the reader has data
                if (reader.HasRows)
                {
                    // process the data reader
                    while (reader.Read())
                    {
                        address = new Address()
                        {
                           UserAddressId = reader.GetInt32(0),
                           City = reader.IsDBNull(4) ? null : reader.GetString(4),
                           StateID = reader.IsDBNull(5) ? -1 : reader.GetInt32(5),
                           Zip = reader.IsDBNull(8) ? null : reader.GetString(8),
                           CountryID = reader.IsDBNull(9) ? -1 : reader.GetInt32(9),
                           AddressTypeId = reader.IsDBNull(11) ? -1 : reader.GetInt32(11),
                           UserPersonalInformationId = reader.IsDBNull(13) ? -1 : reader.GetInt32(13)                            
                        };

                        //Process AddressLines
                        addressLinesInDB = new List<String>();
                        addressLinesInDB.Add( reader.IsDBNull(2) ? null : reader.GetString(1) );
                        addressLinesInDB.Add( reader.IsDBNull(3) ? null : reader.GetString(2) );
                        addressLinesInDB.Add( reader.IsDBNull(4) ? null : reader.GetString(3) );
                        address.AddressLines = addressLinesInDB;

                        // Saving Address to list
                        addressList.Add(address);
                    }
                    // close the reader
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            //Return Address list
            return addressList;
        }

        /// <summary>
        /// Data Access Method to Retrieve Countries from Database
        /// </summary>
        /// <returns>List of Countries</returns>
        public List<Country> RetrieveCountries()
        {
            var countriesInDB = new List<Country>();

            //Getting connection
            var conn = DBConnection.GetConnection();

            //Using stored procedure
            var cmdText = @"sp_retrieve_country_by_country_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CountryID", SqlDbType.Int);
            cmd.Parameters["@CountryID"].Value = -1;

            try
            {
                // you have to open a connection before using it
                conn.Open();

                // create a data reader with our command
                var reader = cmd.ExecuteReader();
                

                // check to make sure the reader has data
                if (reader.HasRows)
                {
                    // process the data reader
                    while (reader.Read())
                    {
                        var country = new Country()
                        {
                            CountryID = reader.GetInt32(0),
                            ISO = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Name = reader.IsDBNull(2) ? null : reader.GetString(2),
                            NiceName = reader.IsDBNull(3) ? null : reader.GetString(3),
                            ISO3 = reader.IsDBNull(4) ? null : reader.GetString(4),
                            NumCode = reader.IsDBNull(5) ? -1 : reader.GetInt32(5),
                            PhoneCode = reader.IsDBNull(6) ? -1 : reader.GetInt32(6)
                        };
                       countriesInDB.Add( country );
                    }
                    // close the reader
                    reader.Close();
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was a problem retrieving countries list data." + ex);
            }
            finally
            {
                // final housekeeping
                conn.Close();
            }

            return countriesInDB;
            
        }

        /// <summary>
        /// Retrieves Country By ID
        /// </summary>
        /// <param name="countryID"></param>
        /// <returns>Country Object</returns>
        public Country RetrieveCountryByID(int countryID)
        {
            //Retrieve country by Id
            Country country = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_country_by_country_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CountryID", SqlDbType.Int);
            cmd.Parameters["@CountryID"].Value = countryID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    country = new Country()
                    {
                        CountryID = reader.GetInt32(0),
                        ISO = reader.GetString(1),
                        Name = reader.GetString(2),
                        NiceName = reader.GetString(3),
                        ISO3 = reader.GetString(4),
                        NumCode = reader.GetInt32(5),
                        PhoneCode = reader.GetInt32(6)              
                    };
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return country;
        }

        /// <summary>
        /// Retrieves state by ID
        /// </summary>
        /// <param name="stateID">ID for Query</param>
        /// <param name="retrieveAll">Signal to retrieve all</param>
        /// <returns></returns>
        public static State RetrieveStateByID(int stateID)
        {
            //Retrieve country by Id
            State state = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_state_by_state_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@StateID", SqlDbType.Int);
            cmd.Parameters["@StateID"].Value = stateID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    state = new State()
                    {
                        StateID = reader.GetInt32(0),
                        StateCode = reader.GetString(1),
                        StateName = reader.GetString(2)                       
                    };
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return state;
        }


        /// <summary>
        /// Retrieves States Data into List
        /// </summary>
        /// <returns>States List</returns>
        public static List<State> RetrieveStates()
        {
            var statesList = new List<State>();

            //Getting connection
            var conn = DBConnection.GetConnection();

            //Using stored procedure
            var cmdText = @"sp_retrieve_state_by_state_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@StateID", SqlDbType.Int);
            cmd.Parameters["@StateID"].Value = -1;

            try
            {
                // you have to open a connection before using it
                conn.Open();

                // create a data reader with our command
                var reader = cmd.ExecuteReader();
                State state;

                // check to make sure the reader has data
                if (reader.HasRows)
                {
                    // process the data reader
                    while (reader.Read())
                    {
                        state = new State()
                        {
                            StateID = reader.GetInt32(0),
                            StateCode = reader.GetString(1),
                            StateName = reader.GetString(2)
                        };
                        statesList.Add(state);
                    }
                    
                    // close the reader
                    reader.Close();
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was a problem retrieving states data.", ex);
            }
            finally
            {
                // final housekeeping
                conn.Close();
            }

            return statesList;

        }

        /// <summary>
        /// Retreive Clearance Levels from Database based on Department
        /// </summary>
        /// <param name="departmentId">Department ID</param>
        /// <param name="retrieveAll">Signal to Retrieve all irresepective of Department ID</param>
        /// <returns>Returns a List of Clearance Levels</returns>
        public List<ClearanceLevel> RetrieveClearanceByDeptID(string departmentId, bool retrieveAll)
        {
            var clearanceLevelInDB = new List<ClearanceLevel>();

            //Getting connection
            var conn = DBConnection.GetConnection();

            //Using stored procedure
            var cmdText = @"sp_retrieve_clearance_levels_by_deptId";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DepartmentId", SqlDbType.VarChar);
            string departmentIdParameter = retrieveAll ? "dbsall" : departmentId;
            cmd.Parameters["@DepartmentId"].Value = departmentIdParameter;

            try
            {
                // you have to open a connection before using it
                conn.Open();

                // create a data reader with our command
                var reader = cmd.ExecuteReader();

                // check to make sure the reader has data
                if (reader.HasRows)
                {
                    // process the data reader
                    while (reader.Read())
                    {
                        // create an clearancelevel object
                        var cll = new ClearanceLevel()
                        {
                            PriorityNumber = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            DepartmentId = reader.GetString(3)

                        };

                        // Save Employees into List
                        clearanceLevelInDB.Add(cll);
                    }
                    // close the reader
                    reader.Close();
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was a problem retrieving clearance level data.", ex);
            }
            finally
            {
                // final housekeeping
                conn.Close();
            }

            return clearanceLevelInDB;
        }

        /// <summary>
        /// Retrieve AddressTypes by AddressType ID
        /// </summary>
        /// <param name="addresstypeID"></param>
        /// <param name="retrieveAll"></param>
        /// <returns>Returns a list of AddressTypes</returns>
        public static List<AddressType> RetrieveAddressTypesByID(int addresstypeID, bool retrieveAll)
        {
            var addressTypeList = new List<AddressType>();

            //Getting connection
            var conn = DBConnection.GetConnection();

            //Using stored procedure
            var cmdText = @"sp_retrieve_addresstype_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AddressTypeID", SqlDbType.Int);
            int addressTypeIdParameter = retrieveAll ? -1 : addresstypeID;
            cmd.Parameters["@AddressTypeID"].Value = addressTypeIdParameter;

            try
            {
                // you have to open a connection before using it
                conn.Open();

                // create a data reader with our command
                var reader = cmd.ExecuteReader();

                // check to make sure the reader has data
                
                if (reader.HasRows)
                {
                    // process the data reader
                    while (reader.Read())
                    {
                        var addresstype = new AddressType()
                        {
                            AddressTypeId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2)
                        };
                        addressTypeList.Add(addresstype);
                    }
                    // close the reader
                    reader.Close();
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was a problem retrieving address type data.", ex);
            }
            finally
            {
                // final housekeeping
                conn.Close();
            }

            return addressTypeList;            
        }
    }
}
