using DataAccessLayer;
using DataAccessLayerInterfaces;
using DataObjects;
using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class TeamManager : ITeamManager
    {
      
       private ITeamAccessor _teamAccessor = null;
        public TeamManager()
        {
            _teamAccessor = new DataAccessLayer.TeamAccessor();
        }

        // overload for testing
        public TeamManager(TeamAccessor ta)
        {
            _teamAccessor = ta;
        }



        public List<Teams> RetrieveTeamsByActive(bool active)
        {
            List<Teams> teams = null;
            try
            {
                teams = _teamAccessor.SelectTeamsByStatus(active);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found.", ex);
            }

            return teams;
        }
    }
}
