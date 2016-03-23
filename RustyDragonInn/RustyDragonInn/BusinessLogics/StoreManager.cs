using RustyDragonBasesAndInterfaces.BusinessLogics;
using RustyDragonBasesAndInterfaces.Models;
using RustyDragonBasesAndInterfaces.Printer;
using System;
using System.Collections.Generic;

namespace RustyDragonInn.BusinessLogics
{
    /// <summary>
    /// Store Manager is responsible to calculate all of the prices every day for a week (7 days).
    /// it does its responsibilities through the following dependencies.
    ///     PriceCalculator : Calculates the price of a cheese
    ///     Printer : Prints the result on the console
    ///     DaysManager : Notifies the store manager a new day has come along with a new date and time.
    /// </summary>
    public class StoreManager : IStoreManager
    {
        public IList<ICheese> Cheeses { get; set; }

        private readonly IPriceCalculator _priceCalculator;
        private readonly IPrinter _printer;
        private readonly IDaysManager _daysManager;
        private const int Duration = 7;

        public StoreManager(IPriceCalculator priceCalculator,
                            IPrinter printer,
                            IDaysManager daysManager)
        {
            _priceCalculator = priceCalculator;
            _printer = printer;
            _daysManager = daysManager;
            _daysManager.OnNextDay += DaysManager_OnNextDay;
        }

        private void DaysManager_OnNextDay(object sender, DaysManagerEventArgs e)
        {
            _printer.PrintLine($"Day Number: {e.DayNumber}");
            CalculatePrices(e.Now);
            if (e.DayNumber > Duration)
            {
                CloseStore();
            }
        }

        public void CalculatePrices(DateTime now)
        {
            foreach (var cheese in Cheeses)
            {
                DecrementDaysToSell(cheese);
                _priceCalculator.CalculatePrice(cheese, now);
            }
            _printer.Print(Cheeses, now);
        }

        public void OpenStore()
        {
            _printer.PrintLine("Welcome to Store Manager ....The cheese have been loaded as listed below.");
            _printer.PrintLine("Day Number: 1 ");
            _printer.Print(Cheeses, _daysManager.Now);
            _daysManager.Start();
        }

        public void CloseStore()
        {
            _daysManager.Stop();
            _printer.PrintLine("The store is now closed....Thank you for your shopping.");
        }

        private void DecrementDaysToSell(ICheese cheese)
        {
            if (cheese.DaysToSell > 0)
                cheese.DaysToSell--;
        }
    }
}