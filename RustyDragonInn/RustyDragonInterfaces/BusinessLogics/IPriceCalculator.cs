using RustyDragonBasesAndInterfaces.Models;
using System;

namespace RustyDragonBasesAndInterfaces.BusinessLogics
{
    public interface IPriceCalculator
    {
        void CalculatePrice(ICheese cheesem, DateTime now);
    }
}