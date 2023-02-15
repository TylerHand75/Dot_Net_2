using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataAccessLayerInterfaces;
using DataObjects;
using LogicLayer;
using DataAccessLayerFakes;

namespace LogicLayerTestUnit
{
    [TestClass]
    public class UserManagerTests
    {
        UserManager userManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            userManager = new UserManager(new UserAccessorFakes());
        }

        [TestMethod]
        public void TestAuthenticateUserPassesWithCorrectUsernameAndPasswordHash()
        {
            //arrange
            const string email = "tess@company.com";
            const string password = "newuser";
            int expectedResult = 999999;
            int acutalResult = 0;
            //act
            User tessUser = userManager.LoginUser(email, password);
            acutalResult = tessUser.UserID;

            //assert
            Assert.AreEqual(expectedResult, acutalResult);

        }
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAuthenticateUserFailsWithIncorrectUsername()
        {
            //arrange
            const string email = "xtess@company.com";
            const string password = "newuser";
            
            
            //act
            User tessUser = userManager.LoginUser(email, password);
        
        }
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAuthenticateUserFailsWithIncorrectPasswordHash()
        {
            //arrange
            const string email = "tess@company.com";
            const string password = "xnewuser";


            //act
            User tessUser = userManager.LoginUser(email, password);

        }

        [TestMethod]
        public void TestGetSha256ReturnsCorrectHashValue()
        {
            // Arrange
            const string source = "newuser";
            const string expectedResult =
                "9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e";
            string result = "";


            // Act
            result = userManager.HashSha256(source);

            // Assert
            Assert.AreEqual(expectedResult, result);


        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestGetSha256ThrowsArugmentNullExceptionForMissingInput()
       
        {
            // Arrange
            const string source = null;
          //  const string expectedResult =   ""; , this shoulf throw exception
            


            // Act
            userManager.HashSha256(source);

            // Assert
           //nothing to assert


        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestGetSha256ThrowsArugmentNullExceptionForEmptyString()

        {
            // Arrange
            const string source = "";
            //  const string expectedResult =   ""; , this shoulf throw exception



            // Act
            userManager.HashSha256(source);

            // Assert
            //nothing to assert


        }
    }
}
