using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ZipSample.test
{
    [TestClass]
    public class ConcatTests
    {
        [TestMethod]
        public void concat_integers()
        {
            var first = new int[] { 1, 3, 5 };
            var second = new int[] { 2, 4, 6 };

            var actual = first.MyConcat(second).ToArray();

            var expected = new int[] { 1, 3, 5, 2, 4, 6 };
            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void concat_employee()
        {
            var first = new List<Employee>()
            {
                new Employee() {Id = 123, Name = "Ben"}
            };

            var second = new List<Employee>()
            {
                new Employee() {Id = 111, Name = "Eric"},
                new Employee() {Id = 124, Name = "Joy"}
            };

            var actual = first.MyConcat(second).ToList();

            var expected = new List<Employee>()
            {
                new Employee() {Id = 123, Name = "Ben"},
                new Employee() {Id = 111, Name = "Eric"},
                new Employee() {Id = 124, Name = "Joy"}
            };

            expected.ToExpectedObject().ShouldEqual(actual);
        }
    }
}