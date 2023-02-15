using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayerInterfaces
{
    public interface IUserAccessor
    {
        int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash);    // interface methods..?
        User SelectUserByEmail(string email);
        List<string> SelectRolesByEmployeeID(int employeeID);
        int UpdatePasswordHash(int employeeID, string passwordHash, string oldPasswordHash);

    }
}
