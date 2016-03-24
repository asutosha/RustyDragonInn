using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using RustyDragonInn.BusinessLogics;

namespace RustyDragonTests
{
    [TestClass]
    public class DaysManagerTests
    {
        private const int Interval = 100;
        private readonly DateTime _now = DateTime.Parse("2016-03-20");
        private DaysManager _daysManager;

        [TestInitialize]
        public void DaysManagerTestsSetup()
        {
            _daysManager = new DaysManager(Interval, _now);
        }

        [TestMethod]
        [Timeout(1000)]
        public void DaysManager_ifOnNextDayRaisedAndReturnsCorrectValues()
        {
            var actualDayNumber = 0;
            var actualNow = _now;
            _daysManager.OnNextDay += (sender, e) =>
            {
                actualDayNumber = e.DayNumber;
                actualNow = e.Now;
            };

            var currentObject = new PrivateObject(_daysManager);
            currentObject.Invoke("_internalTimer_Elapsed", new object[] {null, null});

            const int expectedDaysNumber = 2;
            var expectedNow = _now.AddDays(1);

            Assert.AreEqual(expectedDaysNumber, actualDayNumber);
            Assert.AreEqual(expectedNow, actualNow);
        }

        [TestMethod]
        [Timeout(1000)]
        public void DaysManager_ifACorrectDaysComesBack()
        {
            var wasCalled = false;
            _daysManager.OnNextDay += (sender, e) =>
                wasCalled = true;

            var currentObject = new PrivateObject(_daysManager);
            currentObject.Invoke("_internalTimer_Elapsed", new object[] {null, null});

            Assert.IsTrue(wasCalled);
        }

        [TestMethod]
        [Timeout(1000)]
        public void DaysManager_ifStartMethodCalled()
        {
            var daysManager = Substitute.For<DaysManager>(Interval, _now);
            daysManager.Start();
            daysManager.Received().Start();
            daysManager.Dispose();
        }

        [TestCleanup]
        public void DaysManagerTestsCleanup()
        {
            _daysManager.Stop();
            _daysManager.Dispose();
            _daysManager = null;
        }
    }
}