using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayerInterfaces
{
   public  interface IUserAccessor
    {
        int AuthenticateUserWIthEmailAndPasswordHash(string email, string passwordHash);

        User SelectUserByEmail(string email);
        List<string> SelectRolesByUserID(int userID);
        int UpdatePasswordHash(int userID,string passwordHash, string oldPasswordHash );
    }
}
