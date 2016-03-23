using RustyDragonBasesAndInterfaces.Helper;
using RustyDragonBasesAndInterfaces.Models;
using RustyDragonBasesAndInterfaces.Reader;
using RustyDragonInn.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Schema;

namespace RustyDragonInn.Reader
{
    /// <summary>
    /// The reader loads the list of the cheeses from a text file.(The source can be changed)
    /// </summary>
    public class Reader : IReader
    {
        public IList<ICheese> Load(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"{filePath} does not exists or not found");
            }

            var xmlDoc = XDocument.Load(filePath);

            var cheeseList = (from item in xmlDoc.Root.Elements("Item")
                              let itemName = item.Element("Name")
                              let itemPrice = item.Element("Price")
                              let itemDaysToSell = item.Element("DaysToSell")
                              let itemBestBeforeDate = item.Element("BestBeforeDate")
                              let itemType = item.Element("Type")
                              where itemPrice != null && itemName != null
                              select new Cheese
                              {
                                  Name = itemName.Value,
                                  Price = double.Parse(itemPrice.Value),
                                  DaysToSell = itemDaysToSell.Value.Trim().Equals(string.Empty) ? default(int?) :
                                                Convert.ToInt32(itemDaysToSell.Value),
                                  BestBeforeDate = itemBestBeforeDate.Value.Trim().Equals(string.Empty) ?
                                                    default(DateTime?) : Convert.ToDateTime(itemBestBeforeDate.Value),
                                  Type = Helper.CheeseTypeMapper(itemType.Value),
                              }).ToList<ICheese>();

            return cheeseList;
        }
    }
}