using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic;
using DataObjects;

namespace BusinessLogic.Test
{
    [TestClass]
    public class UserManagerTests
    {
        //[TestMethod]
        //[Ignore]
        //public void TryHashSHA256()
        //{
        //    var umg = new UserManager();
        //    string hashedString = umg.HashSHA256("word");
        //    Assert.AreEqual("98c1eb4ee93476743763878fcb96a25fbc9a175074d64004779ecb5242f645e6", hashedString);

        //}

        [TestMethod]
        public void TryAuthentication()
        {
            var umg = new UserManager();

            var userfromDB = umg.AuthenticateUser("SCSYMCX", "hello");
            // Assert.AreEqual(expectedResult, userfromDB);
            Assert.AreEqual(userfromDB.FirstName, "Martin");
            Assert.AreEqual(userfromDB.LastName, "Cox");

        }
    }
}
