﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Tests.SampleServer;

namespace Simple.Tests.DataAccess
{
    [TestFixture]
    public class ExpressionFilterFixture 
    {
        [Test]
        public void TestCustomerLoadById()
        {
            var c = Customer.LoadByExpression(x => x.Id == "DRACD");
            Assert.IsNotNull(c);
            Assert.AreEqual("Drachenblut Delikatessen", c.CompanyName);
        }

        [Test]
        public void TestCustomerLoadIdContains()
        {
            var c = Customer.LoadByExpression(x => x.Id.Contains("DRACD"));
            Assert.IsNotNull(c);
            Assert.AreEqual("Drachenblut Delikatessen", c.CompanyName);
        }

        [Test]
        public void TestCustomerLoadIdContainsFalse()
        {
            var c = Customer.LoadByExpression(x => x.Id.Contains("DRACDasd"));
            Assert.IsNull(c);
        }

        [Test]
        public void TestCustomerLoadMultipleColumn()
        {
            var c = Customer.LoadByExpression(x => x.Id.Contains("DRACD") && x.Country == "Germany");
            Assert.IsNotNull(c);
            Assert.AreEqual("Drachenblut Delikatessen", c.CompanyName);
        }

        [Test]
        public void TestProductLoadByIdAndCategory()
        {
            var p = Product.LoadByExpression(x => x.Id == 2 && x.Category.Name == "Beverages");
            Assert.IsNotNull(p);
            Assert.AreEqual("Chang", p.Name);
        }

        [Test]
        public void TestProductLoadByIdAndCategoryFalse()
        {
            var p = Product.LoadByExpression(x => x.Id == 2 && x.Category.Name == "OutroNome");
            Assert.IsNull(p);
        }
    }
}
