using RustyDragonBasesAndInterfaces.Validators;
using System;

namespace RustyDragonBasesAndInterfaces.Models
{
    public interface ICheese : ICloneable
    {
        string Name { get; set; }
        DateTime? BestBeforeDate { get; set; }
        int? DaysToSell { get; set; }
        double Price { get; set; }
        CheeseTypes Type { get; set; }

        Tuple<bool, ValidationErrorType> Validate(ICheeseValidator cheeseValidator);

        void CopyTo(ICheese cheese);
    }

    public enum CheeseTypes
    {
        Fresh,
        Unique,
        Special,
        Aged,
        Standard
    }
}