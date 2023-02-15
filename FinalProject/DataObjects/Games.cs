using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
   public  class Games
    {
        public int GameID { get; set; }
        public string Maps { get; set; }
        public string MapType { get; set; }
        public string Score { get; set; }
        public  string GameTime { get; set; }
        public string GameStatusID { get; set; }
        public bool Active { get; set; }


    }

   public class GamesVM : Games
   {
        public List<string> MapTypes { get; set; }
        
   }
}
