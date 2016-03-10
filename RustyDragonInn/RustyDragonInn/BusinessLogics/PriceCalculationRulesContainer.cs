using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using RustyDragonBasesAndInterfaces.BusinessLogics;
using RustyDragonBasesAndInterfaces.Models;

namespace RustyDragonInn.BusinessLogics
{
    /// <summary>
    /// This class encapsulates the business logics required for price calculations.
    /// it stores those logics in the a dictionary (cheese types are keys) 
    /// and passes the appropriate logics using the type of chesses which was passed before.
    /// </summary>
    public class PriceCalculationRulesContainer : IPriceCalculationRulesContainer
    {
        private Dictionary<CheeseTypes, Action<ICheese,DateTime>> _rules;

        public PriceCalculationRulesContainer()
        {
            _rules = new Dictionary<CheeseTypes, Action<ICheese, DateTime>>();
            RegisterRules();
        }

        private void RegisterRules()
        {
            _rules.Add(CheeseTypes.Aged, (ICheese cheese, DateTime now) =>
            {
                if (cheese.DaysToSell==0)
                {
                    cheese.Price = 0.00d;
                    return;
                }
                if (cheese.BestBeforeDate < now)
                {
                    cheese.Price *= 0.9; // 10% price reduction 2 times more than 5%
                }
                else
                {
                    cheese.Price *= 1.05; // 5% price raise
                }
                cheese.Price= Math.Round(cheese.Price, 2, MidpointRounding.ToEven);// rounding
            }); 

            _rules.Add(CheeseTypes.Unique, (ICheese cheese, DateTime now) => {  }); // No action

            _rules.Add(CheeseTypes.Fresh, (ICheese cheese, DateTime now) =>
            {
                if (cheese.DaysToSell == 0)
                {
                    cheese.Price = 0.00d;
                    return;
                }

                if (cheese.BestBeforeDate >= now)
                {
                    cheese.Price *= 0.9; // 10% price reduction 2 times more than 5%
                }
                else
                {
                    cheese.Price *= 0.8; // 20% price reduction as it has passed the BestBeforeDate 2 times more than 10%
                }
                cheese.Price = Math.Round(cheese.Price, 2,MidpointRounding.ToEven);// rounding
            });

            _rules.Add(CheeseTypes.Special, (ICheese cheese, DateTime now) =>
            {
                if (cheese.DaysToSell == 0)
                {
                    cheese.Price = 0.00d;
                    return;
                }

                if (cheese.BestBeforeDate < now)
                {
                    cheese.Price *= 0.9; // 10% price reduction 2 times more than 5%
                    cheese.Price = Math.Round(cheese.Price, 2, MidpointRounding.ToEven);// rounding
                    return;
                }

                if (cheese.DaysToSell <= 10 && cheese.DaysToSell > 5)
                {
                    cheese.Price *= 1.05; // 5% price raise
                }
                if (cheese.DaysToSell <= 5 && cheese.DaysToSell > 0)
                {
                    cheese.Price *= 1.1; // 10% price raise
                }
                cheese.Price = Math.Round(cheese.Price, 2, MidpointRounding.ToEven);// rounding
            });

            _rules.Add(CheeseTypes.Standard, (ICheese cheese, DateTime now) =>
            {
                if (cheese.DaysToSell == 0)
                {
                    cheese.Price = 0.00d;
                    return;
                }

                if (cheese.BestBeforeDate >= now)
                {
                    cheese.Price *= 0.95; // 5% price reduction
                }
                else
                {
                    cheese.Price *= 0.9; // 10% price reduction as it has passed the BestBeforeDate
                }
                cheese.Price = Math.Round(cheese.Price, 2, MidpointRounding.ToEven); // rounding 
            });

        }

       
        public Action<ICheese, DateTime> GetRule(CheeseTypes cheeseType)
        {
            return _rules[cheeseType];
        }
    }
}
