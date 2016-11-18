using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;

namespace DataAccessLayer.Test
{
    [TestClass]
    public class UserAccessorTests
    {
        [TestMethod]
        [TestCategory("Validation")]
        public void ValidateUsernameAndPasswordCorrectInfo()
        {
            bool expectedResult = true;
            bool result = UserAccessor.verifyUsernameAndPassword("SCSYMCX", "hello");

            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        [TestCategory("Validation")]
        public void ValidateUsernameAndPasswordWrongUsername()
        {
            bool expectedResult = true;
            bool result = UserAccessor.verifyUsernameAndPassword("SCSYMAA", "hello");
            Assert.AreNotEqual(expectedResult, result);

        }

        [TestMethod]
        [TestCategory("Validation")]
        public void ValidateUsernameAndPasswordWrongPassword()
        {
            bool expectedResult = true;
            bool result = UserAccessor.verifyUsernameAndPassword("SCSYMCX", "hilo");
            Assert.AreNotEqual(expectedResult, result);
        }

        [TestMethod]
        [TestCategory("Retrievals")]
        public void RetrieveUserbyUsername()
        {
            var expectedResult = new User()
            {
                UserId = 10000,
                Username = "SCSYMCX",
                FirstName = "Martin",
                LastName = "Cox",
                OtherNames = "John",
                DepartmentId = "EGDEPT",
                isEmployed = true,
                isBlocked = false,
                UserRolesId = "DESENG",
                ClearanceLevelId = 30003
            };
            var resultfromDB = UserAccessor.retrieveUserByUsername("SCSYMCX");
            Assert.AreEqual(expectedResult.UserId, resultfromDB.UserId);
            Assert.AreEqual(expectedResult.isEmployed, resultfromDB.isEmployed);

        }
    }
}
