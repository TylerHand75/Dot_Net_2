using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;
using LogicLayerInterfaces;
using DataAccessLayer;


namespace LogicLayer
{
    public class StatsManager : IStatsManger
    {
        private IStatsAccessor _statsAccessor = null;

        public StatsManager()
        {
            _statsAccessor = new DataAccessLayer.StatsAccessor();
        }
        public StatsManager(IStatsAccessor sa)
        {
            _statsAccessor = sa;
        }

        public StatsVM populatetStatByRank(StatsVM statsVM)
        { 
            try
            {
                statsVM.PlayerStats = _statsAccessor.SelectRankByRanks(statsVM.Rank);
            }
            catch (Exception up)
            {
                throw new ApplicationException("Stats not found.", up);
            }
            return statsVM;
        }

        public List<StatsVM> retrieveStatsVmsByRankId(string rankID)
        {
            List<StatsVM> playStats = null;
            try
            {
                playStats = _statsAccessor.SelectStatsByRankID(rankID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found.", ex);
            }

            return playStats;
        }
    }
}
