﻿using DataObjects;
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
        /// Retrieve active or inactive employee based on paramater
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
        /// Retrieve user roles by department
        /// </summary>
        /// <param name="departmentId">Department Id user for retrieval</param>
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

                throw new ApplicationException("There was a problem retrieving user roles data.", ex);
            }
            finally
            {
                // final housekeeping
                conn.Close();
            }

            return userRolesInDb;
            
        }
    }
}
