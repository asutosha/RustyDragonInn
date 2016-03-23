using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RustyDragonInn.Reader;

namespace RustyDragonTests
{
    /// <summary>
    /// Summary description for Reader
    /// </summary>
    [TestClass]
    public class ReaderTests
    {
        private const string SampleXmlFile = "<?xml version='1.0' encoding='utf-8' ?>"+
                                                "<Items>"+
                                                  "<Item>"+
                                                    "<Name>Cheddar</Name>"+
                                                    "<Type>Standard</Type>"+
                                                    "<Price>3.87</Price>"+
                                                    "<DaysToSell>8</DaysToSell>"+
                                                    "<BestBeforeDate>2016-02-15</BestBeforeDate>"+
                                                  "</Item>"+
                                                "</Items>";

        private const string WrongXmlFile = "wrong";

        private static readonly string SampleXmlFileName = Environment.CurrentDirectory+"\\SampleInput.xml";
        private static readonly string SampleWrongXmlFileName = Environment.CurrentDirectory + "\\SampleWrongXMLInput.xml";

        private const string WrongPath = "c:\\xyzRusty.XML";

        private Reader _reader;

        [TestInitialize]
        public void ReaderTestsSetup()
        {
            var sampleFile = XDocument.Parse(SampleXmlFile);
            sampleFile.Save(SampleXmlFileName);

            var sampleWrongFileWriter = File.CreateText(SampleWrongXmlFileName);
            sampleWrongFileWriter.Write(WrongXmlFile);
            sampleWrongFileWriter.Flush();
            sampleWrongFileWriter.Close();
            sampleWrongFileWriter.Dispose();

            _reader = new Reader();
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Reader_FailsToLoad_ThrowException()
        {
            _reader.Load(WrongPath);
        }


        [TestMethod]
        [ExpectedException(typeof(XmlException))]
        public void Reader_FailsToLoad_NoRoot_ThrowException()
        {
            _reader.Load(SampleWrongXmlFileName);
        }

        [TestMethod]
        public void Reader_Loads_Returns_CheeseList()
        {
           var cheeseList= _reader.Load(SampleXmlFileName);
            Assert.IsNotNull(cheeseList);
            Assert.AreEqual(1,cheeseList.Count);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _reader = null;
        }
    }
}
