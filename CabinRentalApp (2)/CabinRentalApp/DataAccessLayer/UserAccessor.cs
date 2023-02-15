using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class UserAccessor : IUserAccessor
    {
        // data access method in test
        public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash)
        {
            int result = 0;

            // ADO.NET needs a connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // next need command text
            var cmdText = "sp_authenticate_user";

            // use the command text and connection to create a command object
            var cmd = new SqlCommand(cmdText, conn);

            // set the command type - this is an enum
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameter objects to the command - this is from the DB data type when you created the table
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            // set the values for the parameter objects
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            // now that command is set up, we can invoke in a try-catch
            try
            {
                //open the connection
                conn.Open();

                // execute the command appropriately, capure the results - ExecuteScalar, ExecuteNonQuery or ExecuteReader
                    // depending on whether you expect a single value, int for rows affected, or rows and columns
                result = Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch (Exception up)
            {

                throw up;
            }
            finally
            {
                // close the connection - should close automatically since opened in try block, but if theres exception this makes sures it still closes
                conn.Close();
            }

            return result;
        }

        public List<string> SelectRolesByEmployeeID(int employeeID)
        {
            List<string> roles = new List<string>();

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_roles_by_employeeID";

            //command object
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameter (from sp)
            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);   // this is what the type of the parameter is, cant specify length of int

            // values
            cmd.Parameters["@EmployeeID"].Value = employeeID;

            // try-catch-finally
            try
            {
                // open the connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();

                // process the results
                if(reader.HasRows)
                {
                    while(reader.Read())   // Read() is T/F if has more rows so if reader has more rows
                    {
                        roles.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception up)
            {

                throw up;
            }
            finally
            {
                // close the connection
                conn.Close();
            }

            return roles;
        }

        public User SelectUserByEmail(string email)
        {
            User user = null;

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_employee_by_email";

            // command object
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameter
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);

            // value
            cmd.Parameters["@Email"].Value = email;

            // try-catch-finally
            try
            {
                // open connection
                conn.Open();

                // want row and column data, execute and get a SqlDataReader
                var reader = cmd.ExecuteReader();

                user = new User();

                if(reader.HasRows)
                {
                    // most of the time there will be a while loop, not rn since were only expecting one row
                    reader.Read();
                    // [EmployeeID], [GivenName], [FamilyName], [PhoneNumber], [Email]

                    user.EmployeeID = reader.GetInt32(0);
                    user.GivenName = reader.GetString(1);
                    user.FamilyName = reader.GetString(2);
                    user.Phone = reader.GetString(3);
                    user.Email = reader.GetString(4);
                   // user.Active = reader.GetBoolean(5);    missing this in my stored procedure somehow

                }
                // close the reader
                reader.Close();

            }
            catch (Exception up)
            {
                throw up;
            }
            finally
            {
                // close connection
                conn.Close();
            }


            return user;
        }

        public int UpdatePasswordHash(int employeeID, string passwordHash, string oldPasswordHash)
        {

            int rows = 0;


            // connection 
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            //command text
            var cmdText = "sp_update_passwordHash";
            //command object
            var cmd = new SqlCommand(cmdText, conn);
            //
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
            cmd.Parameters.AddWithValue("@OldPasswordHash", oldPasswordHash);

            try
            {
                conn.Open();

                rows = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rows;

        }
    }
}
