﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Tests.SampleServer;

namespace Simple.Tests.DataAccess
{
    [TestFixture]
    public class NorthwindCheckFixture
    {
        [Test]
        public void CheckCategories()
        {
            Assert.AreEqual(8, Category.CountAll());
            Category.ListAll();
        }

        [Test]
        public void CheckCustomer()
        {
            Assert.AreEqual(91, Customer.CountAll());
            Customer.ListAll();
        }
    }
}
