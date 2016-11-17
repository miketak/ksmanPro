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
        public static bool verifyUsernameAndPassword(string username, string password)
        {
            bool result = false;

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
                        //Department = reader.GetString(5), - will be done on another stored procedure
                        isEmployed = reader.GetInt32(5),//investigate this bit thing
                        isBlocked = reader.GetInt32(6),
                        UserRolesId = reader.GetString(7),
                        ClearanceLevelId = reader.GetInt32(8)
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
