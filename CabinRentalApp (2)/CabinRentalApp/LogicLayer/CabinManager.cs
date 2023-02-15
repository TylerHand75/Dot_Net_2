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
    public class CabinManager : ICabinManager
    {
        private ICabinAccessor _cabinAccessor = null;

        // real one
        public CabinManager()
        {
            _cabinAccessor = new DataAccessLayer.CabinAccessor();
        }
        
        // overload for testing
        public CabinManager(ICabinAccessor ca)
        {
            _cabinAccessor = ca;
        }

        public CabinVM PopulateAmenitiesOnCabinVM(CabinVM cabinVM)
        {
            try
            {
                cabinVM.Amenitites = _cabinAccessor.SelectAmenitiesByCabinType(cabinVM.CabinTypeID);
            }
            catch(Exception up)
            {
                throw new ApplicationException("Amenities not found.", up);
            }
            return cabinVM;
        }

        public List<CabinVM> RetrieveCabinVMsByStatus(string status)
        {
            List<CabinVM> cabins = null;
            try
            {
                cabins = _cabinAccessor.SelectCabinsByStatus(status);
            } catch (Exception ex)
            {
                throw new ApplicationException("Data not found.", ex);
            }
           
            return cabins;
        }

        
    }
}
