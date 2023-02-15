using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;
using System.Data;
using System.Data.SqlClient;



namespace DataAccessLayer
{
    public class GameAccessor : IGameAccessor
    {

        public List<string> selectGamesByMaps(string maps)
        {
            List<string> matches = new List<string>();
          
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

           var cmdText = "sp_select_Games_by_Maps";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@MapType", SqlDbType.NVarChar, 50);
            cmd.Parameters["@MapType"].Value = maps;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        matches.Add(reader.GetString(0));
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid Map");
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

            return matches;
        }



        public List<GamesVM> spSelectGamesByStatus(string gameStatus)
        {
            List<GamesVM> games= new List<GamesVM>();


            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();


            var cmdText = "sp_select_Games_by_Status";


            var cmd = new SqlCommand(cmdText, conn);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@GameStatusID", SqlDbType.NVarChar, 25);
            cmd.Parameters["@GameStatusID"].Value = gameStatus;

            try
            {
                
                conn.Open();

              
                var reader = cmd.ExecuteReader();

                
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                      
                        var game = new GamesVM();
                        game.GameID = reader.GetInt32(0);
                        game.Maps = reader.GetString(1);
                        game.MapType = reader.GetString(2);
                        game.Score = reader.GetString(3);
                        game.GameTime = reader.GetString(4);
                        game.GameStatusID = reader.GetString(5);
                        game.Active = reader.GetBoolean(6);

                        games.Add(game);

                    }
                }
            }
            catch (Exception e)
            {
                throw e ;
            }
            finally
            {
                conn.Close();
            }

            return games;
        }
    }
    }
