using RustyDragonBasesAndInterfaces.Models;
using System;
using System.Collections.Generic;

namespace RustyDragonBasesAndInterfaces.BusinessLogics
{
    public interface IStoreManager
    {
        IList<ICheese> Cheeses { get; set; }

        void CalculatePrices(DateTime now);

        void OpenStore();

        void CloseStore();
    }
}