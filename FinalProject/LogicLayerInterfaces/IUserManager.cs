using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IUserManager
    {
        // this method should return the user or throw a bad argument exception
        User LoginUser(string email, string password);
        string HashSha256(string source);
        bool ResetPassword(User user, string email, string password, string oldPassword);
    }
}
