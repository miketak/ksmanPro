﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using System.Collections.Generic;

namespace DataAccessLayer.Test
{
    [TestClass]
    public class EmployeeAccessorTests
    {
        [Ignore]
        [TestMethod]
        public void retrieveAddressesByPeronalInfoId()
        {
            int personalInformationID = 60000;
            //var empAccess = new EmployeeAccessor();

            List<Address> add = EmployeeAccessor.retrieveAddressesByPeronalInfoId(personalInformationID);
            List<Address> lad = new List<Address>();

            //Manually create some data


            Assert.AreEqual(lad, add);
        }
    }
}