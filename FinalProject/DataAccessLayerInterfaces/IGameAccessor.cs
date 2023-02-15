using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayerInterfaces
{
    public interface IGameAccessor
    {

        List<GamesVM> spSelectGamesByStatus(string  gameStatus);


        List<string>selectGamesByMaps(string  maps);
       
    }
}
