using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerInterfaces
{
    public interface IPlayerManager
    {
        List<Players> RetrievePlayerVMsByUserID (bool active);

    }
}
