using RustyDragonBasesAndInterfaces.Models;
using RustyDragonBasesAndInterfaces.Validators;
using System;

namespace RustyDragonInn.Validators
{
    /// <summary>
    /// CheeseValidator encapsulates the logics which is detected in a cheese.
    /// AN instance of this class can be pass to the validation function of a
    /// cheese to get the error type if it fails.
    /// </summary>
    public class CheeseValidator : ICheeseValidator
    {
        public Tuple<bool, ValidationErrorType> Validate(ICheese cheese)
        {
            return cheese.DaysToSell == 0
                ? Tuple.Create<bool, ValidationErrorType>(false, ValidationErrorType.DaysToSellPassed)
                : (cheese.Price < 0
                    ? Tuple.Create<bool, ValidationErrorType>(false, ValidationErrorType.ExceededMinimumPrice)
                    : (cheese.Price > 20
                        ? Tuple.Create<bool, ValidationErrorType>(false, ValidationErrorType.ExceededMaximumPrice)
                        : Tuple.Create<bool, ValidationErrorType>(true, ValidationErrorType.None)));
        }
    }
}