using System;
using System.Linq;
using System.Xml;
using System.IO;
using System.Xml.Schema;
using System.Xml.Linq;

namespace ValidateXML
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = new Uri(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
            XmlSchemaSet schema = new XmlSchemaSet();
            schema.Add("", path + "\\index.xsd");
            XmlReader rd = XmlReader.Create(path + "\\index.xml");
            XDocument doc = XDocument.Load(rd);
            doc.Validate(schema, ValidationEventHandler);
            Console.WriteLine("Validated");

        }
        static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            XmlSeverityType type = XmlSeverityType.Warning;
            if (Enum.TryParse<XmlSeverityType>("Error", out type))
            {
                if (type == XmlSeverityType.Error)
                    throw new Exception(e.Message);

            }
        }
    }
}
