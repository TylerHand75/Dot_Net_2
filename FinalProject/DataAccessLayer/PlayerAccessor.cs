using DataAccessLayerInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class PlayerAccessor : IPlayerAccessor
    {
        
        List<Players> IPlayerAccessor.SelectPlayerByStatus(bool active)
        {
            List<Players> players = new List<Players>();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_Users_by_Status";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Active", SqlDbType.Bit);

            cmd.Parameters["@Active"].Value = active;

            try
            {

                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                      
                        var player = new Players();
                        player.UserID = reader.GetInt32(0);
                        player.GivenName = reader.GetString(1);
                        player.FamilyName = reader.GetString(2);
                        player.GamerTag = reader.GetString(3);
                        


                        players.Add(player);

                    }
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

            return players;
        }
    }
    }

