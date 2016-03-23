using Microsoft.VisualStudio.TestTools.UnitTesting;
using RustyDragonInn.Models;
using System;
using NSubstitute;
using RustyDragonBasesAndInterfaces.Models;
using RustyDragonBasesAndInterfaces.Validators;

namespace RustyDragonTests
{
    [TestClass]
    public class CheeseTests
    {
        private Cheese _cheese;

        [TestInitialize]
        public void InitializeTest()
        {
            _cheese = new Cheese();
        }

        [TestMethod]
        public void Test_Cheese_Name_Property_Returns_Correct_Value()
        {
            const string expected = "Unique";
            _cheese.Name = expected;
            Assert.AreEqual(expected, _cheese.Name);
        }

        [TestMethod]
        public void Test_Cheese_DaysToSell_Property_Returns_correct_Value()
        {
            const int expected = 2;
            _cheese.DaysToSell = expected;
            Assert.IsTrue(_cheese.DaysToSell.HasValue);
            Assert.AreEqual(expected, _cheese.DaysToSell.Value);
        }

        [TestMethod]
        public void Test_Cheese_Price_Property_Returns_Correct_Value()
        {
            const double expected = 3.2;
            _cheese.Price = expected;
            Assert.AreEqual(expected, _cheese.Price);
        }

        [TestMethod]
        public void Test_Cheese_BestBeforeDate_Property_Returns_Correct_Value()
        {
            var expected = DateTime.Parse("2016-03-19");
            _cheese.BestBeforeDate = expected;
            Assert.AreEqual(expected, _cheese.BestBeforeDate);
        }

        [TestMethod]
        public void Test_Cheese_CopyTo_Returns_Coppied_Cheese()
        {
            _cheese.Price = 10;
            _cheese.BestBeforeDate= DateTime.Parse("2016-03-19");
            _cheese.DaysToSell = 5;
            _cheese.Name = "Good Cheese";
            _cheese.Type = CheeseTypes.Aged;

            var newCheese=new Cheese();
            _cheese.CopyTo(newCheese);
            Assert.IsNotNull(newCheese);

            Assert.AreEqual(_cheese.Price,newCheese.Price);
            Assert.AreEqual(_cheese.BestBeforeDate, newCheese.BestBeforeDate);
            Assert.AreEqual(_cheese.DaysToSell, newCheese.DaysToSell);
            Assert.AreEqual(_cheese.Name, newCheese.Name);
            Assert.AreEqual(_cheese.Type, newCheese.Type);

        }

        [TestMethod]
        public void Test_Cheese_CopyTo_Returns_Cloned()
        {
            _cheese.Price = 10;
            _cheese.BestBeforeDate = DateTime.Parse("2016-03-19");
            _cheese.DaysToSell = 5;
            _cheese.Name = "Good Cheese";
            _cheese.Type = CheeseTypes.Aged;

            var newCheese = _cheese.Clone() as ICheese;
            Assert.IsNotNull(newCheese);

            Assert.AreEqual(_cheese.Price, newCheese.Price);
            Assert.AreEqual(_cheese.BestBeforeDate, newCheese.BestBeforeDate);
            Assert.AreEqual(_cheese.DaysToSell, newCheese.DaysToSell);
            Assert.AreEqual(_cheese.Name, newCheese.Name);
            Assert.AreEqual(_cheese.Type, newCheese.Type);

        }

        [TestMethod]
        public void Test_Cheese_Validate_Cheese_Returns_True()
        {
            _cheese.Price = 10;
            _cheese.BestBeforeDate = DateTime.Parse("2016-03-19");
            _cheese.DaysToSell = 5;
            _cheese.Name = "Good Cheese";
            _cheese.Type = CheeseTypes.Aged;

            var cheeseValidator = Substitute.For<ICheeseValidator>();
            cheeseValidator.Validate(_cheese)
                .Returns(new Tuple<bool, ValidationErrorType>(true, ValidationErrorType.None));
            var validationResult = _cheese.Validate(cheeseValidator);

            Assert.IsTrue(validationResult.Item1);
            Assert.AreEqual(ValidationErrorType.None, (ValidationErrorType) validationResult.Item2);
        }

        [TestCleanup]
        public void CleanUpTest()
        {
            _cheese = null;
        }
    }
}