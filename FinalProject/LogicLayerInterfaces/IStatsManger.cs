using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerInterfaces
{
    public interface IStatsManger
    {
        List<StatsVM> retrieveStatsVmsByRankId(string rankID);
        StatsVM populatetStatByRank(StatsVM statsVM);
    }
}
