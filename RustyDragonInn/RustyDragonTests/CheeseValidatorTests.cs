using Microsoft.VisualStudio.TestTools.UnitTesting;
using RustyDragonBasesAndInterfaces.Validators;
using RustyDragonInn.Models;
using RustyDragonInn.Validators;

namespace RustyDragonTests
{
    /// <summary>
    /// Summary description for CheeseValidator
    /// </summary>
    [TestClass]
    public class CheeseValidatorTests
    {
        private CheeseValidator _cheeseValidator;

        [TestInitialize]
        public void ValidatorSetup()
        {
            _cheeseValidator = new CheeseValidator();
        }

        [TestMethod]
        public void ValidatorTests_DaysToSellZero_Returns_False_DaysToSellPassed()
        {
            var cheese = new Cheese { DaysToSell = 0 };

            const ValidationErrorType expectedValidationErrorType = ValidationErrorType.DaysToSellPassed;
            const bool expectedValidationResult = false;

            var validationResult = _cheeseValidator.Validate(cheese);

            Assert.AreEqual(expectedValidationResult, validationResult.Item1);
            Assert.AreEqual(expectedValidationErrorType, validationResult.Item2);
        }

        [TestMethod]
        public void ValidatorTests_PriceLessThanZero_Returns_False_ExceededMinimumPrice()
        {
            var cheese = new Cheese { Price = -1 };

            const ValidationErrorType expectedValidationErrorType = ValidationErrorType.ExceededMinimumPrice;
            const bool expectedValidationResult = false;

            var validationResult = _cheeseValidator.Validate(cheese);

            Assert.AreEqual(expectedValidationResult, validationResult.Item1);
            Assert.AreEqual(expectedValidationErrorType, validationResult.Item2);
        }

        [TestMethod]
        public void ValidatorTests_PriceBiggerThan20_Returns_False_DaysToSellPassed()
        {
            var cheese = new Cheese { Price = 21 };

            const ValidationErrorType expectedValidationErrorType = ValidationErrorType.ExceededMaximumPrice;
            const bool expectedValidationResult = false;

            var validationResult = _cheeseValidator.Validate(cheese);

            Assert.AreEqual(expectedValidationResult, validationResult.Item1);
            Assert.AreEqual(expectedValidationErrorType, validationResult.Item2);
        }

        [TestMethod]
        public void ValidatorTests_PriceLessThan20_DaysToSellBiggerThanZero_Returns_True_None()
        {
            var cheese = new Cheese { Price = 18, DaysToSell = 5 };

            const ValidationErrorType expectedValidationErrorType = ValidationErrorType.None;
            const bool expectedValidationResult = true;

            var validationResult = _cheeseValidator.Validate(cheese);

            Assert.AreEqual(expectedValidationResult, validationResult.Item1);
            Assert.AreEqual(expectedValidationErrorType, validationResult.Item2);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _cheeseValidator = null;
        }
    }
}