using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.API.DocumentSvc;
using Core.API.DocumentSvc.Entity;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


namespace DocLibraryTest
{
    [TestClass]
    public class DocRoutingTest
    {
        [TestMethod]
        public void RoutingDocumentPass()
        {
            string expected = string.Empty;
            string actual;

            actual = new Document().RouteDocument(60, 2, 5, 5, "Routing Document Pass Unit Test");
            Assert.AreEqual<string>(expected, actual, actual);
            Console.WriteLine(actual);
        }
    }
}
