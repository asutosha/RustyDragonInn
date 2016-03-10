using System;
using RustyDragonBasesAndInterfaces.Models;

namespace RustyDragonBasesAndInterfaces.BusinessLogics
{
    public interface IPriceCalculationRulesContainer
    {
        Action<ICheese, DateTime> GetRule(CheeseTypes cheeseType);
    }
}
