using System;
using System.Collections.Generic;
using RustyDragonBasesAndInterfaces.Models;

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
