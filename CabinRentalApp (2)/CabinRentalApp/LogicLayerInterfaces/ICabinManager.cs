using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface ICabinManager
    {
        List<CabinVM> RetrieveCabinVMsByStatus(string status);
        CabinVM PopulateAmenitiesOnCabinVM(CabinVM cabinVM);
    }
}
