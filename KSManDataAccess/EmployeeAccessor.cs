using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class EmployeeAccessor
    {
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
    }
}
