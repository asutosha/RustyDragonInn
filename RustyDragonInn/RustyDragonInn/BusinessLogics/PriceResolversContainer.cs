using System;
using System.Collections.Generic;
using RustyDragonBasesAndInterfaces.BusinessLogics;
using RustyDragonBasesAndInterfaces.Models;
using RustyDragonBasesAndInterfaces.Validators;

namespace RustyDragonInn.BusinessLogics
{
    /// <summary>
    /// This class encapsulates the business logics required for resolving the price issues.
    /// it stores those logics in the a dictionary (error types are keys) 
    /// and passes the appropriate logics using the type of error which was passed before.
    /// </summary>
    public class PriceResolversContainer : IPriceResolversContainer
    {
        private Dictionary<ValidationErrorType, Action<ICheese>> _rules;

        public PriceResolversContainer()
        {
            _rules = new Dictionary<ValidationErrorType, Action<ICheese>>();
            RegisterRules();
        }

        public Action<ICheese> GetRule(ValidationErrorType errorType)
        {
            return _rules[errorType]; 
        }

        private void RegisterRules()
        {
            _rules.Add(ValidationErrorType.ExceededMinimumPrice, (ICheese cheese) => cheese.Price = 0.00);
            _rules.Add(ValidationErrorType.ExceededMaximumPrice, (ICheese cheese) => cheese.Price = 20.00);
            _rules.Add(ValidationErrorType.None, (ICheese cheese) => { });
            _rules.Add(ValidationErrorType.DaysToSellPassed, (ICheese cheese) => { });

        }
    }
}
