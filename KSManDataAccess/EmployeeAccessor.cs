﻿using DataObjects;
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
        //Employee CRUD

        /// <summary>
        /// Creates a new Employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Returns a boolean on success/failure</returns>
        public static bool CreateEmployee(Employee employee)
        {
            //Insert into Users Table

            //Insert into Personal Information Table

            //Insert into UserAddress Table
            return false;
        }

        /// <summary>
        /// Retrieves all Employee Data by Username
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
                        UserRolesId = reader.IsDBNull(12) ? null : reader.GetString(12),
                        JobDesignation = reader.IsDBNull(13) ? null : reader.GetString(13), //role name
                        ClearanceLevelId = reader.IsDBNull(14) ? -1 : reader.GetInt32(14),
                        ClearanceLevel = reader.IsDBNull(15) ? null : reader.GetString(15),
                        //PersonalInfoId = reader.IsDBNull(16) ? -1 : reader.GetInt32(16),
                        //Gender = reader.IsDBNull(17) ? null : reader.GetBoolean(17),
                        DateOfBirth = reader.IsDBNull(17) ? DateTime.MinValue : reader.GetDateTime(17),
                        CountryId = reader.IsDBNull(18) ? -1 : reader.GetInt32(18),
                        Nationality = reader.IsDBNull(19) ? null : reader.GetString(19),
                        AdditonalInfo = reader.IsDBNull(20) ? null : reader.GetString(20),
                        HireDate = reader.IsDBNull(21) ? DateTime.MinValue : reader.GetDateTime(21),
                        MaritalStatus = reader.IsDBNull(22) ? false : reader.GetBoolean(22),
                        PersonalEmail = reader.IsDBNull(23) ? null : reader.GetString(23),
                        PersonalPhoneNumber = reader.IsDBNull(24) ? null : reader.GetString(24)
                    };

                    //Null gender if not exists in database
                    if (reader.IsDBNull(16))
                        employee.Gender = null;
                    else
                        employee.Gender = reader.GetBoolean(16);

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

            //Get List of Personal Addresses
            var addressList = new List<Address>();
            addressList = retrieveAddressesByUserID(employee.UserId);
            employee.Address = addressList;
            

            return employee;

        }

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
        /// Updates Employee Records
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool UpdateEmployeeByID( Employee employee )
        {
            return false;
        }

        /// <summary>
        /// Deletes employee records
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool DeleteEmployeeByID ( int userID)
        {
            return false;
        }


        // Department CRUD

        /// <summary>
        /// Creates a new departments
        /// </summary>
        /// <param name="depertment"></param>
        /// <returns></returns>
        public static bool CreateDepartment ( Department depertment)
        {
            return false;
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
        /// Updates Department Information
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        public static bool UpdateDepartment ( Department department )
        {
            return false;
        }
        
        /// <summary>
        /// Deletes Department
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        public static bool DeleteDepartment ( string departmentID)
        {
            return false;
        }


        // User Role CRUD

        /// <summary>
        /// Create User Roles
        /// </summary>
        /// <param name="userRoles"></param>
        /// <returns></returns>
        public static bool CreateUserRole ( UserRoles userRoles )
        {
            return false;
        }

        /// <summary>
        /// Retrieve employee roles by department.
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
        /// Updates User Role Information
        /// </summary>
        /// <param name="userRolesID"></param>
        /// <returns></returns>
        public static bool UpdateUserRoles ( UserRoles userRoles )
        {
            return false;
        }

        /// <summary>
        /// Deletes User Roles.
        /// </summary>
        /// <param name="userRolesID"></param>
        /// <returns></returns>
        public static bool DeleteUserRoles ( int userRolesID )
        {
            return false;
        }


        // User Address CRUD

        /// <summary>
        /// Creates Address with User ID.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        private static bool CreateAddressByUserID( Employee employee )
        {
            return false;
        }

        /// <summary>
        /// Retrieves Addresses By Personal Information ID
        /// </summary>
        /// <param name="userID">Personal Information ID Parameter for Select Query</param>
        /// <returns>Returns a list of Addresses</returns>
        private static List<Address> retrieveAddressesByUserID(int userID) //remember there is a test here.
        {
            //Retrieve all details
            Address address = null;
            var addressList = new List<Address>();
            var addressLinesInDB = new List<String>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_address_by_userID";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@userID", SqlDbType.Int);
            cmd.Parameters["@userID"].Value = userID;

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
                           //UserPersonalInformationId = reader.IsDBNull(13) ? -1 : reader.GetInt32(13)                            
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
        /// Updates User Address By ID
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        private static bool UpdateAddressByUserID ( Employee employee )
        {
            return false;
        }

        /// <summary>
        /// Deletes Address By User ID.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        private static bool DeleteAddressByUserID ( int userID )
        {
            return false;
        }


        // Address Types CRUD

        /// <summary>
        /// Creates new Address Type
        /// </summary>
        /// <param name="addressType"></param>
        /// <returns></returns>
        public static bool CreateAddressType ( AddressType addressType )
        {
            return false;
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

        /// <summary>
        /// Updates Address Types
        /// </summary>
        /// <param name="addressTypeID"></param>
        /// <returns></returns>
        public static bool UpdateAddressType ( AddressType addressType )
        {
            return false;
        }

        /// <summary>
        /// Deletes Address Type
        /// </summary>
        /// <param name="addressTypeID"></param>
        /// <returns></returns>
        public static bool DeleteAddresstype ( int addressTypeID ) //very dangerous message
        {
            return false;
        }


        // Clearance Level CRUD

        /// <summary>
        /// Creates new clearance level
        /// </summary>
        /// <param name="clearanceLevel">Clerance Level DTO</param>
        /// <returns></returns>
        public static bool CreateClearanceLevel ( ClearanceLevel clearanceLevel )
        {
            return false;
        }

        /// <summary>
        /// Retreive Clearance Levels from Database based on Department
        /// </summary>
        /// <param name="departmentId">Department ID</param>
        /// <param name="retrieveAll">Signal to Retrieve all irresepective of Department ID</param>
        /// <returns>Returns a List of Clearance Levels</returns>
        public static List<ClearanceLevel> RetrieveClearanceByDeptID(string departmentId, bool retrieveAll)
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
        /// Updates Clearance Level By Clearancelevel ID
        /// </summary>
        /// <param name="clearanceLevelID">Clearance Level ID</param>
        /// <returns></returns>
        public static bool UpdateClearanceLevel ( ClearanceLevel clearanceLevel ) //potentially dangerous method
        {
            return false;
        }

        /// <summary>
        /// Deletes Clearance Level by ID
        /// </summary>
        /// <param name="clearanceLevelID">Clerance Level ID</param>
        /// <returns></returns>
        public static bool DeleteClearanceLevel ( int clearanceLevelID )
        {
            return false;
        }

    }
}
