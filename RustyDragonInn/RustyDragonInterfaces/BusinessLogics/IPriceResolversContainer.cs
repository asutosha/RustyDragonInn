using System;
using RustyDragonBasesAndInterfaces.Models;
using RustyDragonBasesAndInterfaces.Validators;

namespace RustyDragonBasesAndInterfaces.BusinessLogics
{
    public interface IPriceResolversContainer
    {
        Action<ICheese> GetRule(ValidationErrorType errorType);
    }
}
