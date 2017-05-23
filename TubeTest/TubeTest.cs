
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TubeLibrary;
using System.Linq;
using System.Collections.Generic;

namespace TubeTest
{
    [TestClass]
    public class TubeShould
    {
        Tube tube = null;

        [TestInitialize]
        public void Initialize()
        {
            tube = new Tube(new FakeData());
        }

        [TestMethod]
        public void ReturnNothingIfStationNotFound()
        {
            var stations = tube.GetStationsNHopsAway("F", 1);
            Assert.IsTrue(stations.Count() == 0);
        }


        [TestMethod]
        public void ReturnCorrectResultFor0Hop()
        {
            var stations = tube.GetStationsNHopsAway("A", 0);

            Assert.IsTrue(stations.Count() == 1);
            Assert.AreEqual("A", stations.First());
        }

        [TestMethod]
        public void ReturnCorrectResultsFor1Hop()
        {
            var stations = tube.GetStationsNHopsAway("A", 1);

            Assert.IsTrue(stations.Any(x => x == "B"));
            Assert.IsTrue(stations.Any(x => x == "D"));
        }

        [TestMethod]
        public void ExcludeStationInLoop()
        {
            var stations = tube.GetStationsNHopsAway("A", 1);

            Assert.IsFalse(stations.Any(z => z == "A"));
        }

        [TestMethod]
        public void ReturnSortedResults()
        {
            var stations = tube.GetStationsNHopsAway("A", 1);

            Assert.IsTrue(stations.SequenceEqual(new List<string> { "B","C", "D" }));
        }

        [TestMethod]
        public void ReturnCorrectResultFor2Hop()
        {
            var stations = tube.GetStationsNHopsAway("A", 2);

            Assert.IsTrue(stations.SequenceEqual(new List<string> { "E" }));
        }
        
        [TestCleanup]
        public void CleanUp()
        {
            tube = null;
        }

    }
}
