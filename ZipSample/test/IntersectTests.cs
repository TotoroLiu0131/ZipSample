using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ZipSample.test
{
    [TestClass]
    public class IntersectTests
    {
        [TestMethod]
        public void intersect_integers()
        {
            var first = new List<int> { 1, 3, 3, 5 };
            var second = new List<int> { 5, 5, 3, 7, 9 };

            var expected = new List<int> { 3, 5 };

            var actual = first.MyIntersect(second).ToList();
            expected.ToExpectedObject().ShouldEqual(actual);
        }


        [TestMethod]
        public void intersect_girls()
        {
            var first = new List<Girl>()
            {
                new Girl(){ Name = "Amanda"},
                new Girl(){ Name = "Lucy"}
            };

            var second = new List<Girl>()
            {
                new Girl() {Name = "Lucy"},
                new Girl() {Name = "Xinyi"},
            };

            var expected = new List<Girl>()
            {
                new Girl() {Name = "Lucy"}
            };

            var actual = first.MyIntersect(second, new GirlEquality()).ToList();
            expected.ToExpectedObject().ShouldEqual(actual);
        }

    }
}