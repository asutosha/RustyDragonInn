using RustyDragonBasesAndInterfaces.Exceptions;
using RustyDragonBasesAndInterfaces.Helper;
using RustyDragonBasesAndInterfaces.Models;
using RustyDragonInn.BusinessLogics;
using RustyDragonInn.Validators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Schema;

namespace RustyDragonInn.Main
{
    /// <summary>
    /// Author Masoud Zehtabi Oskuie 2/18/2016 , designed and implemented for Print Audit.
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            var printer = new Printer.Printer();
            if (args.Length == 0)
            {
                printer.PrintLine("No input file path was specified.");
                Console.Read();
                return;
            }
            try
            {
                var filePath = args[0].Trim();
                var reader = new Reader.Reader();
                var cheeseList = reader.Load(filePath);

                printer.PrintLine("");
                printer.PrintLine(
                    "This application has been designed and implemented by Masoud ZehtabiOskuie as an assessment for Senior C# Developer role");
                var currentDate = Helper.GetDateTime('_', filePath, 1);

                var cheeseValidator = new CheeseValidator();
                var priceCalculationRulesContainer = new PriceCalculationRulesContainer();
                var priceResolversContainer = new PriceResolversContainer();
                var priceCalculator = new PriceCalculator(cheeseValidator, priceCalculationRulesContainer,
                    priceResolversContainer);

                var daysManager = new DaysManager(3000, currentDate);
                var storeManager = new StoreManager(priceCalculator, printer, daysManager) { Cheeses = (List<ICheese>)cheeseList };
                storeManager.OpenStore();
            }
            catch (FileNotFoundException)
            {
                printer.PrintLine("File Does not exists. Please make sure that the path is correct.");
            }
            catch (XmlSchemaException)
            {
                printer.PrintLine("The XML files is not well format.");
            }
            catch (DateTimeFormatException dex)
            {
                printer.PrintLine(dex.Message);
            }
            Console.Read();
        }
    }
}