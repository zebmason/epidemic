using System.Collections.Generic;
using NUnit.Framework;

using Epidemic;

namespace EpidemicTests
{
    public class PathsTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var paths = new Paths(new Complete(new Random(0), 6));
            paths.Generate();
            var (n, m) = paths.Neighbours(0);
            Assert.AreEqual(4, n);
            Assert.AreEqual(2, m);
            (n, m) = paths.Neighbours(3);
            Assert.AreEqual(2, n);
            Assert.AreEqual(5, m);
            (n, m) = paths.Neighbours(1);
            Assert.AreEqual(1, n);
            Assert.AreEqual(1, m);
        }

        [Test]
        public void Test2()
        {
            // paths = 325013, 44
            var random = new MockRandom(new List<int>() { 3, 2, 3, 0, 0, 0, 0 });
            var paths = new Paths(new Complete(random, 6));
            paths.Generate();
            var (n, m) = paths.Neighbours(5);
            Assert.AreEqual(0, n);
            Assert.AreEqual(2, m);
            (n, m) = paths.Neighbours(1);
            Assert.AreEqual(3, n);
            Assert.AreEqual(0, m);
            (n, m) = paths.Neighbours(4);
            Assert.AreEqual(4, n);
            Assert.AreEqual(4, m);
        }

        [Test]
        public void Test2a()
        {
            // paths = 325013, 44
            var random = new MockRandom(new List<int>() { 3, 2, 3, 0, 0, 0, 0 });
            var paths = new Paths(new Complete2(random, 6));
            paths.Generate();
            var (n, m) = paths.Neighbours(5);
            Assert.AreEqual(0, n);
            Assert.AreEqual(2, m);
            (n, m) = paths.Neighbours(1);
            Assert.AreEqual(3, n);
            Assert.AreEqual(0, m);
            (n, m) = paths.Neighbours(4);
            Assert.AreEqual(4, n);
            Assert.AreEqual(4, m);
        }

        [Test]
        public void Test3()
        {
            // paths = 0123450
            var random = new MockRandom(new List<int>() { 0, 0, 0, 1, 1, 1, 0 });
            var paths = new Paths(new Complete(random, 6));
            paths.Generate();
            var (n, m) = paths.Neighbours(0);
            Assert.AreEqual(1, n);
            Assert.AreEqual(5, m);
            (n, m) = paths.Neighbours(3);
            Assert.AreEqual(4, n);
            Assert.AreEqual(2, m);
        }

        [Test]
        public void Test3a()
        {
            // paths = 0123450
            var random = new MockRandom(new List<int>() { 0, 0, 0, 1, 1, 1, 0 });
            var paths = new Paths(new Complete2(random, 6));
            paths.Generate();
            var (n, m) = paths.Neighbours(0);
            Assert.AreEqual(1, n);
            Assert.AreEqual(5, m);
            (n, m) = paths.Neighbours(3);
            Assert.AreEqual(4, n);
            Assert.AreEqual(2, m);
        }

        [Test]
        public void Test4()
        {
            // paths = 5432105
            var random = new MockRandom(new List<int>() { 5, 4, 3, 2, 1, 0, 0 });
            var paths = new Paths(new Complete(random, 6));
            paths.Generate();
            var (n, m) = paths.Neighbours(5);
            Assert.AreEqual(4, n);
            Assert.AreEqual(0, m);
            (n, m) = paths.Neighbours(2);
            Assert.AreEqual(1, n);
            Assert.AreEqual(3, m);
        }

        [Test]
        public void Test4a()
        {
            // paths = 5432105
            var random = new MockRandom(new List<int>() { 5, 4, 3, 2, 1, 0, 0 });
            var paths = new Paths(new Complete2(random, 6));
            paths.Generate();
            var (n, m) = paths.Neighbours(5);
            Assert.AreEqual(4, n);
            Assert.AreEqual(0, m);
            (n, m) = paths.Neighbours(2);
            Assert.AreEqual(1, n);
            Assert.AreEqual(3, m);
        }

        [Test]
        public void Test5()
        {
            // paths = 02130, 454
            var random = new MockRandom(new List<int>() { 0, 1, 0, 1, 0, 0, 0, 0 });
            var paths = new Paths(new Complete(random, 6));
            paths.Generate();
            var (n, m) = paths.Neighbours(0);
            Assert.AreEqual(2, n);
            Assert.AreEqual(3, m);
            (n, m) = paths.Neighbours(1);
            Assert.AreEqual(3, n);
            Assert.AreEqual(2, m);
            (n, m) = paths.Neighbours(4);
            Assert.AreEqual(5, n);
            Assert.AreEqual(5, m);
        }

        [Test]
        public void Test5a()
        {
            // paths = 02130, 454
            var random = new MockRandom(new List<int>() { 0, 1, 0, 1, 0, 0, 0, 0 });
            var paths = new Paths(new Complete2(random, 6));
            paths.Generate();
            var (n, m) = paths.Neighbours(0);
            Assert.AreEqual(2, n);
            Assert.AreEqual(3, m);
            (n, m) = paths.Neighbours(1);
            Assert.AreEqual(3, n);
            Assert.AreEqual(2, m);
            (n, m) = paths.Neighbours(4);
            Assert.AreEqual(5, n);
            Assert.AreEqual(5, m);
        }
    }
}