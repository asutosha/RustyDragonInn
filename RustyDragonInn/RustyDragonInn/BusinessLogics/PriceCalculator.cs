using RustyDragonBasesAndInterfaces.BusinessLogics;
using RustyDragonBasesAndInterfaces.Models;
using RustyDragonBasesAndInterfaces.Validators;
using System;

namespace RustyDragonInn.BusinessLogics
{
    /// <summary>
    /// Price Calculator is responsible to Calculate the price using its own dependencies
    /// The dependencies and their responsibilities are listed below
    ///      CheeseValidator : validate the cheeses and get the relevant error type if it fails.
    ///      PriceCalculationRulesContainer : a container which holds the business logics for price calculation.
    ///      PriceResolversContainer : a container which holds the logics to resolve the errors based on the error types that the validator returns if it fails.
    /// </summary>
    public class PriceCalculator : IPriceCalculator
    {
        private readonly ICheeseValidator _cheeseValidator;
        private readonly IPriceCalculationRulesContainer _priceRuleContainer;
        private readonly IPriceResolversContainer _priceResolversContainer;

        public PriceCalculator(ICheeseValidator cheeseValidator,
            IPriceCalculationRulesContainer priceRuleContainer,
            IPriceResolversContainer priceResolversContainer)
        {
            _cheeseValidator = cheeseValidator;
            _priceRuleContainer = priceRuleContainer;
            _priceResolversContainer = priceResolversContainer;
        }

        public void CalculatePrice(ICheese cheese, DateTime now)
        {
            var clonedCheese = (ICheese)cheese.Clone();
            var priceRule = _priceRuleContainer.GetRule(clonedCheese.Type);
            priceRule.Invoke(clonedCheese, now);
            var validationResult = clonedCheese.Validate(_cheeseValidator);
            var isValid = validationResult.Item1;
            var errorType = validationResult.Item2;

            if (!isValid)
            {
                var resolverRule = _priceResolversContainer.GetRule(errorType);
                resolverRule.Invoke(clonedCheese);
            }
            clonedCheese.CopyTo(cheese);
        }
    }
}