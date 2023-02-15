using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;
using LogicLayerInterfaces;

namespace LogicLayer
{
    public class GameManager : IGameManager

    {
        private IGameAccessor _gameAccessor = null;

       
        public GameManager()
        {
            _gameAccessor = new DataAccessLayer.GameAccessor();
        }

     
        public GameManager(IGameAccessor ga)
        {
            _gameAccessor = ga;
        }


        public GamesVM populateGamesOnGamesVm(GamesVM gamesVM)
        {
            try
            {
                gamesVM.MapTypes = _gameAccessor.selectGamesByMaps(gamesVM.MapType);
            }
            catch (Exception up)
            {
                throw new ApplicationException("Games not found.", up);
            }
            return gamesVM;
        }

        public List<GamesVM> retrieveGamesVmsByStatus(string status)
        {
            List<GamesVM> games = null;
            try
            {
                games = _gameAccessor.spSelectGamesByStatus(status);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found.", ex);
            }

            return games;
        }
    }
}
