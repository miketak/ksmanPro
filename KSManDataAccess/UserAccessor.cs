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
    public class UserAccessor
    {
        public static bool verifyUsernameAndPassword(string username, string passwordHash)
        {
            var result = false;

            // get a connection
            var conn = DBConnection.GetConnection();

            // get command text
            var cmdText = @"sp_authenticate_user";

            // create a command object
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters
            cmd.Parameters.Add("@Username", SqlDbType.VarChar, 20);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.VarChar, 100);

            // set parameter values
            cmd.Parameters["@Username"].Value = username;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            // try-catch-finally
            try
            {
                // open a connection
                conn.Open();

                // execute and capture the result
                if (1 == (int)cmd.ExecuteScalar())
                    result = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

  
            return result;

        }

        public static User retrieveUserByUsername(string username)
        {
            User user = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_user_by_username";
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
                    user = new User()
                    {
                        UserId = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        FirstName = reader.GetString(2),
                        LastName = reader.GetString(3),
                        OtherNames = reader.GetString(4),
                        DepartmentId = reader.GetString(5), 
                        isEmployed = reader.GetBoolean(6),
                        isBlocked = reader.GetBoolean(7),
                        UserRolesId = reader.GetString(8),
                        ClearanceLevelId = reader.GetInt32(9)
                    };
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return user;
        }
    }
}
