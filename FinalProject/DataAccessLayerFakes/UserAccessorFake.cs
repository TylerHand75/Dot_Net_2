using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;


namespace DataAccessLayerFakes
{
    public class UserAccessorFake : IUserAccessor
    {
        private List<User> fakeUsers = new List<User>();
        private List<string> fakePasswordHashes = new List<string>();

        public UserAccessorFake()
        {
            // fakeUsers [0]
            fakeUsers.Add(new User()
            {
                UserID = 999999,
                Email = "tess@mncs.com",
                GivenName = "Tess",
                FamilyName = "User",
                Phone = "1234567890",
                Active = true,
                Roles = new List<string>()
            }
                );
            fakeUsers[0].Roles.Add("Staff");
            fakeUsers[0].Roles.Add("Commissioner");

            // fakeUsers [1]
            fakeUsers.Add(new User()
            {
                UserID = 999998,
                Email = "tessy@mncs.com",
                GivenName = "Tessy",
                FamilyName = "User",
                Phone = "1234567891",
                Active = true,
                Roles = new List<string>()
            }
                );

            fakeUsers.Add(new User()
            {
                UserID = 999997,
                Email = "tessa@mncs.com",
                GivenName = "Tessa",
                FamilyName = "User",
                Phone = "1234567892",
                Active = true,
                Roles = new List<string>()
            }
                );

            fakePasswordHashes.Add("9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e");
            fakePasswordHashes.Add("badhashharry");
            fakePasswordHashes.Add("superbadhashharry");
        }


        public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash)
        {
            int numAuthenticated = 0;
            // check for user record in fake data
            for(int i = 0; i < fakeUsers.Count; i++)
            {
                if(fakeUsers[i].Email == email && fakePasswordHashes[i] == passwordHash && fakeUsers[i].Active == true)
                {
                    numAuthenticated++;
                }
            }
            return numAuthenticated;
        }

        public List<string> SelectRolesByUserID(int userID)
        {
            List<string> roles = new List<string>();

            foreach (var fakeUser in fakeUsers)
            {
                if(fakeUser.UserID == userID)
                {
                    roles = fakeUser.Roles;
                    break;
                }
            }


            return roles;
        }

        public User SelectUserByEmail(string email)
        {
            User user = null;
            foreach (var fakeUser in fakeUsers)
            {
                if(fakeUser.Email == email)
                {
                    user = fakeUser;
                }
            }
            if(user == null)
            {
                throw new ApplicationException("User not found.");
            }

            return user;
        }

        public int UpdatePasswordHash(int userID, string passwordHash, string oldPasswordHash)
        {
            throw new NotImplementedException();
        }
    }
}
