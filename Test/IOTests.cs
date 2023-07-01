using Xunit;
using System;
using System.Collections.Generic;
using App.Services;
using App.Interfaces;
using System.Xml.Linq;
using System.Linq;

namespace Test
{
    public class IOTests
    {
        [Fact]
        public void GetCurrenciesTest()
        {
            IFileDataContext<Dictionary<string,string>> fileContext = new JsonContext("currency.json");
            Dictionary<string,string> expect = 
            new Dictionary<string, string>()
            {
                {"USD", "United States Dollar"},
                {"CNY", "Chinese Yuan"},
                {"JPY", "Japanese Yen"},
                {"EUR", "Euro"},
                {"GBP", "British Pound"},
                {"INR", "Indian Rupee"},
                {"CAD", "Canadian Dollar"},
                {"BRL", "Brazilian Real"}
            };

            Dictionary<string,string> actual = 
            fileContext.GetContextualData().Result;

            Assert.Equal(expect, actual);
        }
        
        [Fact]
        public void GetApiKeyTest()
        {
            Dictionary<string,string> except = 
            new Dictionary<string, string>()
            {
                {"None","WLO7YixpVAnRVgpb0VvIvg==AGSGNh3MZPCuQvcb"}
            };

            string jsonString = App.Utils.IO.ReadFile.RetrieveStringFromFile("api_key.json").Result; 
            Dictionary<string,string> actual = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string,string>>(jsonString);

            Assert.Equal(except, actual);
        }


        [Fact]
        public void GetApiKeyTest2()
        {
            string expected = "[Api Key]";
            XElement root = XElement.Load("Path to xml file"); 

            IEnumerable<XElement> apiKeys = 
                from el in root.Elements("apikey")
                where (string) el.Attribute("name") == "convertCurrency"
                select el;

            string actual = apiKeys.SingleOrDefault(x => (string)x.Attribute("name") == "convertCurrency").Value;

            Assert.Equal(expected, actual);


        }
        [Fact]
        public void TestInterfaceFileDataContextForXML()
        {
            IFileDataContext<string> arrange = 
            new XmlContext.SingleDataAsString(
                "Path to xml file",
                "apikey",
                new XElement("apikey"),
                (x => (string)x.Attribute("name")=="convertCurrency"),
                (o => (string)o.Attribute("name") == "convertCurrency")
            );
            string expected = "[Api Key]";
            string actual = arrange.GetContextualData().Result;
            
            Assert.Equal(expected,actual);

        }

            


    }
}