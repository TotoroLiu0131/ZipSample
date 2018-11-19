using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ZipSample.test
{
    [TestClass]
    public class ZipTests
    {
        [TestMethod]
        public void pair_3_girls_and_5_boys()
        {
            var girls = Repository.Get3Girls();
            var keys = Repository.Get5Keys();

            var girlAndBoyPairs = girls.MyZip(keys, (first, second) => Tuple.Create<string, string>(first.Name, second.OwnerBoy.Name)).ToList();
            var expected = new List<Tuple<string, string>>
            {
                Tuple.Create("Jean", "Joey"),
                Tuple.Create("Mary", "Frank"),
                Tuple.Create("Karen", "Bob"),
            };

            expected.ToExpectedObject().ShouldEqual(girlAndBoyPairs);
        }

        [TestMethod]
        public void pair_5_girls_and_3_boys()
        {
            var girls = Repository.Get5Girls();
            var keys = Repository.Get3Keys();

            var girlAndBoyPairs = keys.MyZip(girls,(first,second) => Tuple.Create(first.OwnerBoy.Name,second.Name)).ToList();
            var expected = new List<Tuple<string, string>>
            {
                Tuple.Create("Joey","Jean"),
                Tuple.Create("Frank","Mary"),
                Tuple.Create("Bob","Karen"),
            };
            expected.ToExpectedObject().ShouldEqual(girlAndBoyPairs);
        }
    }
}