using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;
using System.Data;

namespace DataAccessLayer
{
    public class CabinAccessor : ICabinAccessor
    {
        public List<string> SelectAmenitiesByCabinType(string cabinType)
        {
            List<string> amenities = new List<string>();

            // connection
            var connectionFactory = new DBConnection();
            var conn= connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_amenities_by_cabintypeid";

            // command object
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@cabintypeid", SqlDbType.NVarChar, 25);
            cmd.Parameters["@cabintypeid"].Value = cabinType;

            try
            {
                conn.Open();

                // execute
                var reader = cmd.ExecuteReader();

                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        amenities.Add(reader.GetString(0));
                    }
                } 
                else
                {
                    throw new ArgumentException("Invalid cabin type.");
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

            return amenities;
        }

        public List<CabinVM> SelectCabinsByStatus(string cabinStatus)
        {

            List<CabinVM> cabins = new List<CabinVM>();
            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_cabins_by_cabinstatusid";

            // command object
            var cmd = new SqlCommand(cmdText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters
            cmd.Parameters.Add("@CabinStatusID", SqlDbType.NVarChar, 25);

            // values
            cmd.Parameters["@CabinStatusID"].Value = cabinStatus;

            // try catch finally
            try
            { 
                // open a connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();

                // process results
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        // [CabinID], [Trail], [Site], [CabinTypeID], [CabinStatusID], [Active]
                        var cabin = new CabinVM();
                        cabin.CabinID = reader.GetInt32(0);
                        cabin.Trail = reader.GetString(1);
                        cabin.Site = reader.GetString(2);
                        cabin.CabinTypeID = reader.GetString(3);
                        cabin.CabinStatusID = reader.GetString(4);
                        cabin.Active = reader.GetBoolean(5);

                        cabins.Add(cabin);

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

            return cabins;
        }
    }
}
