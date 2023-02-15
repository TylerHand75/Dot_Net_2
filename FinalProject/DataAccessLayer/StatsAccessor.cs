using DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace DataAccessLayer
{
    public class StatsAccessor : IStatsAccessor
    {


        public List<string> SelectRankByRanks(string userID)
        {
            List<string> playerName = new List<string>();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_Stats_by_Rank";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Ranks", SqlDbType.NVarChar,(50));
           
            cmd.Parameters["@Ranks"].Value = playerName;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        playerName.Add(reader.GetString(0));
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid User");
                }
            }
            catch (Exception up)
            {
                throw up;
            }
            finally
            {
                conn.Close();
            }

            return playerName;
        }


        public List<StatsVM> SelectStatsByRankID(string userStats)
        {
            List<StatsVM> stats = new List<StatsVM>();


            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();


            var cmdText = "sp_select_Stats_by_Rank";


            var cmd = new SqlCommand(cmdText, conn);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Ranks", SqlDbType.NVarChar,(50));
            cmd.Parameters["@Ranks"].Value = userStats;

            try
            {

                conn.Open();


                var reader = cmd.ExecuteReader();


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        var stat = new StatsVM();
                        stat.RankID = reader.GetInt32(0);
                        stat.Rank = reader.GetString(1);
                        stat.KDRatio = reader.GetString(2);
                        stat.ACS = reader.GetString(3);

                        stats.Add(stat);

                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }

            return stats;
        }
    }
}



