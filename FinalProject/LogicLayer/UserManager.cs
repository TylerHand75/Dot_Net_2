using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using LogicLayerInterfaces;
using DataAccessLayerInterfaces;
using DataAccessLayer;
using System.Security.Cryptography;


namespace LogicLayer
{
    public class UserManager : IUserManager
    {
        private IUserAccessor userAccessor = null;

        public UserManager()
        {
            userAccessor = new UserAccessor();
        }

        public UserManager(IUserAccessor ua)
        {
            userAccessor = ua;
        }

        public string HashSha256(string source)
        {
            string result = "";

            // check for missing input
            if (source == "" || source == null)
            {
                throw new ArgumentNullException("Missing Input ya Dummy");
            }


            // create a byte array
            byte[] data;

            // create a .NET hash provider object
            using(SHA256 sha256hasher = SHA256.Create())        // different from compiler using statement, makes SHA256 object belong to this function
            {
                // hash the input
                data = sha256hasher.ComputeHash(Encoding.UTF8.GetBytes(source));
            }

            // create output with a stringbuilder object
            var s = new StringBuilder();

            // loop through the hashed output making characters from the calues in the byte array
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }
            // convert the stringbuilder into a string
            result = s.ToString().ToLower();
            
            return result;
        }

        // login system, required for final project
        public User LoginUser(string email, string password)
        {
            User user = null;

            try
            {
                password = HashSha256(password);
                if(1 == userAccessor.AuthenticateUserWithEmailAndPasswordHash(email, password))
                {
                    user = userAccessor.SelectUserByEmail(email);
                    user.Roles = userAccessor.SelectRolesByUserID(user.UserID);
                } 
                else
                {
                    throw new ApplicationException("User not found.");
                }
            
            } 
            catch(Exception up)
            {
                throw new ApplicationException("Bad Username or Password", up);
            }

            
            return user;
        }

        public bool ResetPassword(User user, string email, string password, string oldPassword)
        {
            bool success = false;
            password = HashSha256(password);
            oldPassword = HashSha256(oldPassword);

            if (user.Email != email)
            {
                success =  false;
            }
            else if (1 == userAccessor.UpdatePasswordHash(user.UserID, password , oldPassword))
            {
                success = true;
            }
            return success;
        }
    }
}
