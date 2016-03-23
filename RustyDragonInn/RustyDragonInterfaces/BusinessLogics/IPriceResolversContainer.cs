using RustyDragonBasesAndInterfaces.Models;
using RustyDragonBasesAndInterfaces.Validators;
using System;

namespace RustyDragonBasesAndInterfaces.BusinessLogics
{
    public interface IPriceResolversContainer
    {
        Action<ICheese> GetRule(ValidationErrorType errorType);
    }
}