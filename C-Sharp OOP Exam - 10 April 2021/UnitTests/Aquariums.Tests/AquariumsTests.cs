namespace Aquariums.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public class AquariumsTests
    {
        private List<Fish> fish;

        [SetUp]
        public void Setup()
        {
            fish = new List<Fish>();
        }

        [Test]
        public void ConstructorInitialiezeCorrectly()
        {
            string name = "MyAqua";
            int cap = 1;
            Aquarium aquarium = new Aquarium(name, cap);

            Assert.AreEqual(aquarium.Name, name);
            Assert.AreEqual(aquarium.Capacity, cap);
        }

        [Test]
        public void SetNameThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new Aquarium(null, 1));
            Assert.Throws<ArgumentNullException>(() => new Aquarium(string.Empty, 1));
        }

        [Test]
        public void CapacityThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new Aquarium("MyAqua", -15));
        }

        [Test]
        public void CountCorrect()
        {
            Fish myFish = new Fish("Nemo");
            fish.Add(myFish);
            Assert.That(fish.Count, Is.EqualTo(1));
        }

        [Test]
        public void Add_ThrowsException()
        {
            Aquarium aquarium = new Aquarium("MyAqua", 0);
            Assert.Throws<InvalidOperationException>(() => aquarium.Add(new Fish("Nemo")));
        }

        [Test]
        public void Remove_ThrowsException()
        {
            Aquarium aquarium = new Aquarium("MyAqua", 1);
            aquarium.Add(new Fish("Nemo"));
            Assert.Throws<InvalidOperationException>(() => aquarium.RemoveFish("Tito"));
        }

        [Test]
        public void Remove_Successful()
        {
            Aquarium aquarium = new Aquarium("MyAqua", 1);
            aquarium.Add(new Fish("Nemo"));
            aquarium.RemoveFish("Nemo");
            Assert.That(aquarium.Count, Is.EqualTo(0));
        }

        [Test]
        public void SellFish_ShouldThrowException()
        {
            Aquarium aquarium = new Aquarium("MyAqua", 1);
            aquarium.Add(new Fish("Nemo"));
            Assert.Throws<InvalidOperationException>(() => aquarium.SellFish("Tito"));
        }

        [Test]
        public void SellFish_Successful()
        {
            Aquarium aquarium = new Aquarium("MyAqua", 1);
            aquarium.Add(new Fish("Nemo"));

            Fish fish = aquarium.SellFish("Nemo");
            Assert.AreEqual(fish.Name, "Nemo");
            Assert.AreEqual(fish.Available, false);
        }

        [Test]
        public void Report()
        {
            Aquarium aquarium = new Aquarium("MyAqua", 1);
            aquarium.Add(new Fish("Nemo"));

            string expectedMessage = "Fish available at MyAqua: Nemo";

            Assert.AreEqual(expectedMessage, aquarium.Report());
        }
    }
}
