using RustyDragonBasesAndInterfaces.Helper;
using RustyDragonBasesAndInterfaces.Models;
using RustyDragonBasesAndInterfaces.Printer;
using System;
using System.Collections.Generic;

namespace RustyDragonInn.Printer
{
    /// <summary>
    /// Printer encapsulates all of the features required for showing the results on the screen.
    /// it draws a table showing the list of the cheeses (Print Items).
    /// it can also write a line on the screen (PrintLine).
    /// </summary>
    public class Printer : IPrinter
    {
        private string[] _header;

        public Printer()
        {
        }

        public void Print(List<ICheese> cheeses, DateTime now)
        {
            if (cheeses == null) throw new ArgumentNullException(nameof(cheeses));
            if (cheeses.Count == 0) throw new ArgumentException("Argument is empty collection", nameof(cheeses));
            _header = new string[] { "RustyDragonInn", "(Grocery Store)", "Today", now.ToShortDateString() };
            PrintItems(cheeses);
        }

        public void PrintLine(string message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            Console.WriteLine(message + Environment.NewLine);
        }

        private void PrintItems(IList<ICheese> cheeseList)
        {
            if (cheeseList == null) throw new ArgumentNullException(nameof(cheeseList));
            if (cheeseList.Count == 0) throw new ArgumentException("Argument is empty collection", nameof(cheeseList));
            Shell.From(cheeseList).AddHeader(_header).Write();
        }
    }
}