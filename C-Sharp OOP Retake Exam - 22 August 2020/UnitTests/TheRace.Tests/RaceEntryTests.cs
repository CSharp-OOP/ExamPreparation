using NUnit.Framework;
using System;

namespace TheRace.Tests
{
    public class RaceEntryTests
    {
        private RaceEntry raceEntry;

        [SetUp]
        public void Setup()
        {
            raceEntry = new RaceEntry();
        }

        [Test]
        public void Counter_IsZeroByDefault()
        {
            Assert.That(raceEntry.Counter, Is.Zero);
        }

        [Test]
        public void Counter_Increases_WhenAddingDriver()
        {
            //UnitCar car = new UnitCar("Audi", 177, 1000);
            //UnitDriver driver = new UnitDriver("Pesho", car);
            //raceEntry.AddDriver(driver);
            raceEntry.AddDriver(new UnitDriver("Pesho", new UnitCar("Tesla", 400, 10000)));

            Assert.That(raceEntry.Counter, Is.EqualTo(1));
        }

        [Test]
        public void AddDriver_ThrowsException_WhenDriverIsNull()
        {
            Assert.Throws<InvalidOperationException>(() => raceEntry.AddDriver(null));
        }

        [Test]
        public void AddDriver_ThrowsException_WhenDriverNameExists()
        {
            var drivername = "Pesho";
            raceEntry.AddDriver(new UnitDriver(drivername, new UnitCar("Mercedes", 177, 10000)));
            Assert.Throws<InvalidOperationException>(() => raceEntry.AddDriver(new UnitDriver(drivername, new UnitCar("Audi", 140, 10000))));
        }

        [Test]
        public void AddDriver_ReturnsExpectedResultMessage()
        {
            var drivername = "Pesho";
            var currentResult = raceEntry.AddDriver(new UnitDriver(drivername, new UnitCar("Tesla", 400, 10000)));
            var expected = $"Driver {drivername} added in race."; ;
            Assert.That(currentResult, Is.EqualTo(expected));
        }

        [Test]
        public void CalculateAverageHorsePower_ThrowsException_WhenDriversAreLessThanRequired()
        {
            Assert.Throws<InvalidOperationException>(() => raceEntry.CalculateAverageHorsePower());

            raceEntry.AddDriver(new UnitDriver("Pesho", new UnitCar("Tesla", 400, 10000)));

            Assert.Throws<InvalidOperationException>(() => raceEntry.CalculateAverageHorsePower());
        }

        [Test]
        public void CalculateAverageHorsePower_ReturnsExpectedCalculation()
        {
            int n = 10;
            double expeced = 0;

            for (int i = 0; i < n; i++)
            {
                int hp = 450 + i;
                expeced += hp;
                raceEntry.AddDriver(new UnitDriver($"Name{i}", new UnitCar("Model", hp, 550)));
            }

            expeced /= n;

            double actual = raceEntry.CalculateAverageHorsePower();
            Assert.That(actual,Is.EqualTo(expeced));
        }
    }
}