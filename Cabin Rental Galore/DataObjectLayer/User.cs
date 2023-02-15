using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class User
    {
        public int UserID { get; set; }
        public String GivenName { get; set; }
        public String FamilyName { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public bool Active { get; set; }
         public List <string> Roles { get; set; }

    }
}
