using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayerInterfaces
{
    public interface ICabinAccessor
    {
        List<CabinVM> SelectCabinsByStatus(string cabinStatus);
        List<string> SelectAmenitiesByCabinType(string cabinType);
    }
}
