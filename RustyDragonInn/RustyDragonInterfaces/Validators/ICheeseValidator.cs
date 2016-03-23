using RustyDragonBasesAndInterfaces.Models;
using System;

namespace RustyDragonBasesAndInterfaces.Validators
{
    public interface ICheeseValidator
    {
        Tuple<bool, ValidationErrorType> Validate(ICheese cheese);
    }

    public enum ValidationErrorType
    {
        None = 0,
        ExceededMinimumPrice = 1,
        ExceededMaximumPrice = 2,
        DaysToSellPassed = 3
    }
}