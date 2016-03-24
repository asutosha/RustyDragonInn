using RustyDragonBasesAndInterfaces.Models;
using System;
using System.Collections.Generic;

namespace RustyDragonBasesAndInterfaces.Printer
{
    public interface IPrinter
    {
        void Print(List<ICheese> cheeses, DateTime now);

        void PrintLine(string message);
    }
}