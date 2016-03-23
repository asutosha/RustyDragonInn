using RustyDragonBasesAndInterfaces.Models;
using System;

namespace RustyDragonBasesAndInterfaces.BusinessLogics
{
    public interface IPriceCalculationRulesContainer
    {
        Action<ICheese, DateTime> GetRule(CheeseTypes cheeseType);
    }
}