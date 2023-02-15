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
    public class AgentManager : IAgentManager
    {


        private IAgentAccessor _agentAccessor = null;
        public AgentManager()
        {
            _agentAccessor = new DataAccessLayer.AgentAccessor();
        }

        // overload for testing
        public AgentManager(AgentAccessor aa)
        {
            _agentAccessor = aa;
        }

        public List<Agents> RetrieveAgentsByActive(bool active)
        {
            List<Agents> agents = null;
            try
            {
                agents = _agentAccessor.SelectAgentsByStatus(active);

            }
            catch (Exception ex)
            {

                throw new ApplicationException("Data not found.", ex);
            }
            return agents;
        }
    }
}

