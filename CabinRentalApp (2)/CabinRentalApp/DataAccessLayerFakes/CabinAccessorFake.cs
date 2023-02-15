using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayerFakes
{
    public class CabinAccessorFake : ICabinAccessor
    {
        List<CabinVM> cabinVMs = new List<CabinVM>();

        public CabinAccessorFake()
        {
            cabinVMs.Add(new CabinVM
            {
                CabinID = 999999,
                Trail = "Test Trail",
                Site = "T-1",
                CabinStatusID = "Available",
                CabinTypeID = "Rustic",
                Active = true,
                Amenitites = new List<string>()
            });

            cabinVMs.Add(new CabinVM
            {
                CabinID = 999998,
                Trail = "Test Trail2",
                Site = "T-2",
                CabinStatusID = "Available",
                CabinTypeID = "Standard",
                Active = true,
                Amenitites = new List<string>()
            });

            cabinVMs.Add(new CabinVM
            {
                CabinID = 999997,
                Trail = "Test Trail3",
                Site = "T-3",
                CabinStatusID = "Available",
                CabinTypeID = "Family",
                Active = true,
                Amenitites = new List<string>()
            });

            cabinVMs[0].Amenitites = new List<string>();
            cabinVMs[0].Amenitites.Add("Test 1");
            cabinVMs[0].Amenitites.Add("Test 2");
        }

        public List<string> SelectAmenitiesByCabinType(string cabinType)
        {
            var amenities = new List<string>();

            if (cabinType != "Rustic" && cabinType != "Standard" && cabinType != "Family")
            {
                throw new ArgumentException("Invalid Cabin Type");
            }

            foreach (var c in cabinVMs)
            {
                if(c.Amenitites != null)
                {
                    amenities = c.Amenitites;
                    break;
                }
            }

            return amenities;
        }

        public List<CabinVM> SelectCabinsByStatus(string cabinStatus)
        {
            return cabinVMs.Where(c => c.CabinStatusID == cabinStatus).ToList();
        }
    }
}
