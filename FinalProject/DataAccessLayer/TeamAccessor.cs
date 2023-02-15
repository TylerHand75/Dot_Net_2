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
    public class TeamAccessor : ITeamAccessor
    {
        
           List<Teams> ITeamAccessor.SelectTeamsByStatus(bool active)
        {
                List<Teams> teams = new List<Teams>();

                var connectionFactory = new DBConnection();
                var conn = connectionFactory.GetConnection();

                var cmdText = "sp_select_Teams_by_Status";

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

                            var team = new Teams();

                            team.Team = reader.GetString(0);
                            team.TeamRanking = reader.GetInt32(1);


                            teams.Add(team);

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

                return teams;
            }
        }
    }

