using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RustyDragonBasesAndInterfaces.Models;
using RustyDragonInn.Models;
using RustyDragonInn.Printer;

namespace RustyDragonTests
{
    /// <summary>
    /// Summary description for PrinterTests
    /// </summary>
    [TestClass]
    public class PrinterTests
    {
        private Printer _printer;
        [TestInitialize]
        public void PrinterTestsSetup()
        {
          _printer=new Printer();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Printer_NullCheeseList_ThrowsException()
        {
            _printer.Print(null,DateTime.Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Printer_EmptyCheeseList_ThrowsException()
        {
            var cheeses=new List<ICheese>();
            _printer.Print(cheeses, DateTime.Now);
        }

        [TestMethod]
        public void Printer_EmptyCheeseList_PrintsLists()
        {
            var cheeses = new List<ICheese>();

            var cheese1 = new Cheese
            {
                Price = 10,
                BestBeforeDate = DateTime.Parse("2016-03-19"),
                DaysToSell = 5,
                Name = "Good Cheese",
                Type = CheeseTypes.Aged
            };

            cheeses.Add(cheese1);
            _printer.Print(cheeses, DateTime.Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Printer_PrintLine_NullMessage_ThrowsException()
        {
            _printer.PrintLine(null);
        }

        [TestMethod]
        public void Printer_PrintLine_WithMessage()
        {
            _printer.PrintLine("Test");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Printer_PrintItems_NullCheeseLists_ThrowsException()
        {
           var pObjectPrivateObject=new PrivateObject(_printer);
            pObjectPrivateObject.Invoke("PrintItems", new object[] {null});
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Printer_PrintItems_EmptyCheeseList_ThrowsException()
        {
            var cheeses = new List<ICheese>();
            var pObjectPrivateObject = new PrivateObject(_printer);
            pObjectPrivateObject.Invoke("PrintItems", cheeses);
        }

        [TestMethod]
        public void Printer_PrintItems_EmptyCheeseList_PrintsLists()
        {
            var cheeses = new List<ICheese>();

            var cheese1 = new Cheese
            {
                Price = 10,
                BestBeforeDate = DateTime.Parse("2016-03-19"),
                DaysToSell = 5,
                Name = "Good Cheese",
                Type = CheeseTypes.Aged
            };

            cheeses.Add(cheese1);

            var pObjectPrivateObject = new PrivateObject(_printer);
            pObjectPrivateObject.SetFieldOrProperty("_header", new string[] { "RustyDragonInn", "(Grocery Store)", "Today", DateTime.Now.ToShortDateString() });
            pObjectPrivateObject.Invoke("PrintItems", cheeses);
        }

        [TestCleanup]
        public void CleanUpTest()
        {
            _printer = null;
        }
    }
}
