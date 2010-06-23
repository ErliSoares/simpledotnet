﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
using Simple.Generator;

namespace Simple.Tests.IO
{
    public class RootFinderFixture
    {
        DirectoryInfo dir = null;
        string currDir = null;
        [SetUp]
        public void Setup()
        {
            currDir = Environment.CurrentDirectory;

            Assert.False(Directory.Exists("test"));
            dir = new DirectoryInfo("test");

            Directory.CreateDirectory("test/a/b/c/d/e");
            File.WriteAllText("test/a/b/test.info", "teste-teste-teste");
        }

        [TearDown]
        public void Teardown()
        {
            Environment.CurrentDirectory = currDir;
            if (dir != null)
            {
                Assert.True(dir.Exists);
                dir.Delete(true);
            }
        }

        [Test]
        public void CanFindWhenExistsAndContentIsOk()
        {
            Environment.CurrentDirectory = Path.Combine(dir.FullName, "a/b/c/d/e");
            
            Assert.IsTrue(RootFinder.ChangeToPathOf("test.info", "teste-teste-teste"));
            Assert.AreEqual(Path.Combine(dir.FullName, @"a\b"), Environment.CurrentDirectory);
        }

        [Test]
        public void CanFindWhenExistsAndContentIsWrong()
        {
            Environment.CurrentDirectory = Path.Combine(dir.FullName, "a/b/c/d/e");

            Assert.IsFalse(RootFinder.ChangeToPathOf("test.info", "teste-teste-teste2"));
            Assert.AreEqual(Path.Combine(dir.FullName, @"a\b\c\d\e"), Environment.CurrentDirectory);
        }

        [Test]
        public void CanFindWhenNotExists()
        {
            Environment.CurrentDirectory = Path.Combine(dir.FullName, "a/b/c/d/e");

            Assert.IsFalse(RootFinder.ChangeToPathOf("test2.info", "teste-teste-teste"));
            Assert.AreEqual(Path.Combine(dir.FullName, @"a\b\c\d\e"), Environment.CurrentDirectory);
        }
    }
}