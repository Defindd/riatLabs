using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Serialization
{
    class XMLView
    {
        public string output { get; set; }
        public XMLView(string xmlInput)
        {
            this.XmlString = xmlInput;
            var input = DeserializeXMLString();
            var output = Calculator.calculate(input);
            this.output = serializeOutput(output);
        }
        private string XmlString { get; set; }
        public Input DeserializeXMLString()
        {
            var serializer = new XmlSerializer(typeof(Input));
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(this.XmlString);
            using (XmlReader reader = new XmlNodeReader(xmlDoc))
            {
                return (Input)serializer.Deserialize(reader);
            }
        }
        public static string serializeOutput(Output output)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Output));
            var XmlDoc = new XmlDocument();
            using (var stringWriter = new StringWriter())
            {
                 serializer = new XmlSerializer(typeof(Output));

                serializer.Serialize(stringWriter, output);
                return stringWriter.ToString();
            }

        }
    }
}
