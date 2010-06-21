﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Generator;

namespace Simple.Tests.Generator
{
    public class GeneratorResolverFixture
    {
        [Test]
        public void CanSelectCorrectGenerator()
        {
            var resolver = new GeneratorResolver();

            resolver.Register(() => new SampleStringList(), "test1");
            resolver.Register(() => new SampleDateTimeList(), "test2");

            var generator1 = resolver.Resolve("test1 test");
            var generator2 = resolver.Resolve("test2 test");


            Assert.IsInstanceOf<SampleStringList>(generator1);
            Assert.IsInstanceOf<SampleDateTimeList>(generator2);
        }

        [Test]
        public void ShowErrorWhenStringOnlyStartsWithCorrectValue()
        {
            var resolver = new GeneratorResolver();
            resolver.Register(() => new SampleStringList(), "test1");

            Assert.Throws<ArgumentException>(()=>resolver.Resolve("test1t(test)"));
        }


        [Test]
        public void CanSelectCorrectGeneratorStartingWithSpace()
        {
            var resolver = new GeneratorResolver();
            resolver.Register(() => new SampleStringList(), "test1");
            resolver.Register(() => new SampleDateTimeList(), "test2");

            var generator1 = resolver.Resolve(" test1 test");
            var generator2 = resolver.Resolve(" test2 test");


            Assert.IsInstanceOf<SampleStringList>(generator1);
            Assert.IsInstanceOf<SampleDateTimeList>(generator2);
        }

        [Test]
        public void CanSelectCorrectGeneratorEndingWithSpace()
        {
            var resolver = new GeneratorResolver();
            resolver.Register(() => new SampleStringList(), "test1");
            resolver.Register(() => new SampleDateTimeList(), "test2");

            var generator1 = resolver.Resolve("test1 test ");
            var generator2 = resolver.Resolve("test2 test ");


            Assert.IsInstanceOf<SampleStringList>(generator1);
            Assert.IsInstanceOf<SampleDateTimeList>(generator2);
        }


        [Test]
        public void CanSelectCorrectGeneratorWithSpacedInput()
        {
            var resolver = new GeneratorResolver();
            resolver.Register(() => new SampleStringList(), "test1 a");
            resolver.Register(() => new SampleDateTimeList(), "test2 b");

            var generator1 = resolver.Resolve("test1    a");
            var generator2 = resolver.Resolve("test2    b");


            Assert.IsInstanceOf<SampleStringList>(generator1);
            Assert.IsInstanceOf<SampleDateTimeList>(generator2);
        }

        [Test]
        public void CanSelectCorrectGeneratorWithSpacedInputAndParenthesisEnd()
        {
            var resolver = new GeneratorResolver();
            resolver.Register(() => new SampleStringList(), "test1 a");
            resolver.Register(() => new SampleDateTimeList(), "test2 b");

            var generator1 = resolver.Resolve("test1    a(test)");
            var generator2 = resolver.Resolve("test2    b(test)");


            Assert.IsInstanceOf<SampleStringList>(generator1);
            Assert.IsInstanceOf<SampleDateTimeList>(generator2);
        }

        [Test]
        public void CanSelectCorrectGeneratorWithSpacedInputAndSpaceEnd()
        {
            var resolver = new GeneratorResolver();
            resolver.Register(() => new SampleStringList(), "test1 a");
            resolver.Register(() => new SampleDateTimeList(), "test2 b");

            var generator1 = resolver.Resolve("test1    a (test)");
            var generator2 = resolver.Resolve("test2    b (test)");


            Assert.IsInstanceOf<SampleStringList>(generator1);
            Assert.IsInstanceOf<SampleDateTimeList>(generator2);
        }

        [Test, ExpectedException(typeof(ArgumentException), 
            ExpectedMessage="SampleStringList, SampleDateTimeList",
            MatchType=MessageMatch.Contains)]
        public void CannotSelectCorrectGeneratorWhenFoundMultiple()
        {
            var resolver = new GeneratorResolver();
            resolver.Register(() => new SampleStringList(), "test1 a");
            resolver.Register(() => new SampleDateTimeList(), "test1");
            resolver.Resolve("test1    a (test)");
        }


     

        public class SampleStringList : IGenerator
        {
            public void Execute() { }
        }

        public class SampleDateTimeList : IGenerator
        {
            public void Execute() { }
        }

    }

}
