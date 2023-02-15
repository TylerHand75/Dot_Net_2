using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataObjects;
using LogicLayer;
using DataAccessLayerInterfaces;
using DataAccessLayerFakes;

namespace LogicLayerTests
{
    [TestClass]
    public class UserManagerTests
    {
        UserManager userManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            userManager = new UserManager(new UserAccessorFake());
        }

        [TestMethod]
        public void TestAuthenticateUserPassesWithCorrectUsernameAndPassword()
        {
            // arrange
            const string email = "tess@company.com";
            const string password = "newuser";
            int expectedResult = 999999;
            int actualResult = 0;

            // act
            User tessUser = userManager.LoginUser(email, password);         // want to see if we get employee object back with correct empID
            actualResult = tessUser.UserID;

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAuthenticateUserFailsWithIncorrectUsername()
        {
            // arrange
            const string email = "xtess@company.com";
            const string password = "newuser";

            // act
            User tessUser = userManager.LoginUser(email, password);         // want to see if we get employee object back with correct empID

            // assert
            
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAuthenticateUserFailsWithIncorrectPassword()
        {
            // arrange
            const string email = "tess@company.com";
            const string password = "xnewuser";

            // act
            User tessUser = userManager.LoginUser(email, password);         // want to see if we get employee object back with correct empID

            // assert

        }



        [TestMethod]
        public void TestGetSha256ReturnsCorrectHashValue()          // red green refactor = write test first on AAA method
        {
            // Arrange
            const string source = "newuser";
            const string expectedResult = "9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e";
            string result = "";

            // Act
            result = userManager.HashSha256(source);

            // Assert
            Assert.AreEqual(expectedResult, result);

            // RED
                //test now, test should fail (red) meaning it was written correctly since it has nothing to test yet. yay!

            // GREEN
                // now make the test pass by returning the expected value in easiest way possible to make sure everythings communicating and test
                // is capable of passing (green)
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]   // this is important
        public void TestGetSha256ThrowsArgumentNullExceptionForMissingInput()
        {
            // arrange
            const string source = null;
            // expected result shouldn't make a difference since should throw exception

            // Act
            userManager.HashSha256(source);

            // Assert
            // nothing to assert
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
