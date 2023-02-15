using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Cabin
    {
        public int CabinID { get; set; }
        public string Trail { get; set; }
        public string Site { get; set; }
        public string CabinTypeID { get; set; }
        public string CabinStatusID { get; set; }
        public bool Active { get; set; }
    }

    // inherites all properties from Cabin, plus includes its own which is the list of Amenities
    // this is a view model
    public class CabinVM : Cabin
    {
        public List<string> Amenitites { get; set; }
    }
}
