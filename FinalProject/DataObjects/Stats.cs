using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Stats
    { 
        public int RankID { get; set; }
        public string Rank { get; set; }
        public string KDRatio {get; set;}
        public string ACS { get; set; }
    }
    public class StatsVM : Stats
    {
        public List<string>PlayerStats {get; set;}
        
    }
}
