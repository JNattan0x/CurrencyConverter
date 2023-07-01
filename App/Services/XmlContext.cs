using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace App.Services
{
    public class XmlContext
    {

        /*
            Xml data read to get a single occurrence of data in xml file 
        */
        public class SingleDataAsString : Interfaces.IFileDataContext<string>
        {
        
            private Func<XElement, bool> forSearch, forContent;
            private string filePath,elementsName;

            public SingleDataAsString(string fileSourcePath,
            string elementName,
            XElement element, 
            Func<XElement, bool> elementSearchContext,
            Func<XElement, bool> contentSearchDefinition)
            {
                this.forSearch = elementSearchContext;
                this.forContent = contentSearchDefinition;
                this.filePath = fileSourcePath;
                this.elementsName = elementName;
            }
            public async Task<string> GetContextualData()
            {
                XElement root = XElement.Load(filePath);

                IEnumerable<XElement> apiKeys =
                from el in root.Elements(elementsName)
                where this.forSearch(el)
                select el;

                return (string)apiKeys.SingleOrDefault(forContent).Value;
                

            }
        }
    }
}