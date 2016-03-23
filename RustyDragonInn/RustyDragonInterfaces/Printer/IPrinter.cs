using System;
using System.Collections.Generic;
using RustyDragonBasesAndInterfaces.Models;

namespace RustyDragonBasesAndInterfaces.Printer
{
    public interface IPrinter
    {
        void Print(List<ICheese> cheeses, DateTime now);

        void PrintLine(string message);
    }
}