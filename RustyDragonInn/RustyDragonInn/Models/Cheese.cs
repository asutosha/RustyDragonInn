using RustyDragonBasesAndInterfaces.Models;
using RustyDragonBasesAndInterfaces.Validators;
using System;

namespace RustyDragonInn.Models
{
    /// <summary>
    /// Cheese is the model of the application which encapsulates the behaviors of a cheese
    /// it validates and also clones itself in a case that a new fresh instance is required.
    /// it can also copy itself to another instance
    /// The following is its only dependency :
    ///     CheeseValidator : encapsulates the validation logics and validate the cheese.
    /// </summary>
    public class Cheese : ICheese
    {
        public DateTime? BestBeforeDate { get; set; }
        public int? DaysToSell { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public CheeseTypes Type { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public void CopyTo(ICheese cheese)
        {
            cheese.BestBeforeDate = BestBeforeDate;
            cheese.DaysToSell = DaysToSell;
            cheese.Name = Name;
            cheese.Price = Price;
            cheese.Type = Type;
        }

        public Tuple<bool, ValidationErrorType> Validate(ICheeseValidator cheeseValidator)
        {
            return cheeseValidator.Validate(this);
        }
    }
}