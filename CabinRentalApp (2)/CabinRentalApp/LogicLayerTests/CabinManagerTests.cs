using DataAccessLayerFakes;

using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using DataAccessLayer;
using DataObjects;
using System.Collections.Generic;

namespace LogicLayerTests
{
    [TestClass]
    public class CabinManagerTests
    {
        private CabinManager _cabinManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            // _cabinManager = new CabinManager(new DataAccessLayer.CabinAccessor());
            _cabinManager = new CabinManager(new CabinAccessorFake());
        }

        [TestMethod]
        public void TestRetrieveCabinsByStatusReturnsCorrectList()
        {
            // arrange
            string cabinStatus = "Available";
            int expectedCount = 3;
            int actualCount = 0;

            // act
            var cabins = _cabinManager.RetrieveCabinVMsByStatus(cabinStatus);
            actualCount = cabins.Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestPopulateAmenitiesOnCabinVMReturnsCorrectList()
        {
            // arrange
            const string cabinType = "Rustic";
            const int expectedCount = 2;
            CabinVM cabinVM = new CabinVM() { CabinTypeID = cabinType };
            int actualCount;

            // act
            cabinVM = _cabinManager.PopulateAmenitiesOnCabinVM(cabinVM);
            actualCount = cabinVM.Amenitites.Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestPopulateAmenitiesOnCabinVMThrowsExceptionWithBadCabinType()
        {
            // arrange 
            const string cabinType = "Invalid";
            CabinVM cabinVM = new CabinVM() { CabinTypeID = cabinType };

            // act
            cabinVM = _cabinManager.PopulateAmenitiesOnCabinVM(cabinVM);

            // assert- nothing to do, expecting exception
        }
    }
}