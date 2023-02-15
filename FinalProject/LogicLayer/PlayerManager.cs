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
    public class PlayerManager : IPlayerManager
    {
        private IPlayerAccessor _playerAccessor = null;
        public PlayerManager()
        {
            _playerAccessor = new DataAccessLayer.PlayerAccessor();
        }

        // overload for testing
        public PlayerManager(PlayerAccessor pa)
        {
            _playerAccessor = pa;
        }

        

        public List<Players> RetrievePlayerVMsByUserID(bool  active)
        {
            List<Players> players = null;
            try
            {
                players = _playerAccessor.SelectPlayerByStatus(active);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found. player manager", ex);
            }

            return players;
        }
    }     
  }
