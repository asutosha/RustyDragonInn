using RustyDragonBasesAndInterfaces.Models;
using System;
using System.Collections.Generic;

namespace RustyDragonBasesAndInterfaces.Printer
{
    public interface IPrinter
    {
        void Print(IList<ICheese> cheeses, DateTime now);

        void PrintLine(string message);
    }
}