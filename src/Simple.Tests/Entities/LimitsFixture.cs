﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Tests.Resources;
using Simple.Entities;
using Simple.Expressions;
using NUnit.Framework;
using Simple.IO.Serialization;

namespace Simple.Tests.Entities
{
    public class LimitsFixture
    {
        [Test]
        public void CanOnlySkip()
        {
            var spec = Customer.Do.Skip(10);
            var queryable = new EmptyQueryable<Customer>("q").ApplySpecs(spec);
            Assert.AreEqual("q.Skip(10)", queryable.Expression.ToString());
        }

        [Test]
        public void CanOnlyTake()
        {
            var spec = Customer.Do.Take(10);
            var queryable = new EmptyQueryable<Customer>("q").ApplySpecs(spec);
            Assert.AreEqual("q.Take(10)", queryable.Expression.ToString());

        }

        [Test]
        public void CanSkipAndTake()
        {
            var spec = Customer.Do.Skip(10).Take(11);
            var queryable = new EmptyQueryable<Customer>("q").ApplySpecs(spec);
            Assert.AreEqual("q.Skip(10).Take(11)", queryable.Expression.ToString());
        }

        [Test]
        public void CanTakeAndSkip()
        {
            var spec = Customer.Do.Take(10).Skip(11);
            var queryable = new EmptyQueryable<Customer>("q").ApplySpecs(spec);
            Assert.AreEqual("q.Take(10).Skip(11)", queryable.Expression.ToString());

        }

        [Test]
        public void CanSerializeLimits()
        {
            var spec = Customer.Do.Take(10).Skip(11);
            var spec2 = SimpleSerializer.Binary().RoundTrip(spec);

            Assert.AreNotSame(spec, spec2);
        }
    }
}