using System;
using RustyDragonBasesAndInterfaces.Models;

namespace RustyDragonBasesAndInterfaces.BusinessLogics
{
    public interface IPriceCalculator
    {
        void CalculatePrice(ICheese cheesem, DateTime now);
    }
}
