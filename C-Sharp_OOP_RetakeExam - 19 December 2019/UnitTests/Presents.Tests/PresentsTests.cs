namespace Presents.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    public class PresentsTests
    {
        private Present present;
        private Bag bag;

        [SetUp]
        public void Setup()
        {
            present = new Present("Test", 20);
            bag = new Bag();
        }

        [Test]
        public void TestIfPresentCtorWorksCorrectly()
        {
            Assert.AreEqual("Test", this.present.Name);
            Assert.AreEqual(20, this.present.Magic);
        }

        [Test]
        public void SettersTest()
        {
           present.Name = "Gosho";
            present.Magic = 30;

            Assert.AreEqual("Gosho", this.present.Name);
            Assert.AreEqual(30, this.present.Magic);
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWhenCreatedPresenIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>this.bag.Create(null));
        }

        [Test]
        public void ShouldThrowInvalidOperationExceptionWhenTryToAddAlredyExistingPresent()
        {
            bag.Create(present);

            Assert.Throws<InvalidOperationException>(() => bag.Create(present));
        }

        [Test]
        public void TestSuccessfullyAddedPresent()
        {
            Assert.AreEqual($"Successfully added present {present.Name}.", bag.Create(present));
        }

        [Test]
        public void RemovesPresent()
        {
            bag.Create(present);

            Assert.IsTrue(bag.Remove(present));
        }

        [Test]
        public void TestGetPresentWithLeastMagic()
        {
            bag.Create(present);
            bag.Create(new Present("Test2", 30));

            Assert.AreEqual(present, bag.GetPresentWithLeastMagic());
        }

        [Test]
        public void TestGetPresent()
        {
            bag.Create(present);

            Assert.AreEqual(present, bag.GetPresent("Test"));
        }

        [Test]
        public void CountShouldIncreaseWhenPresentIsAddedSuccessfully()
        {
            bag.Create(present);

            var test = new List<Present>();

            test.Add(present);

            CollectionAssert.AreEqual(test, bag.GetPresents());
        }
    }
}
