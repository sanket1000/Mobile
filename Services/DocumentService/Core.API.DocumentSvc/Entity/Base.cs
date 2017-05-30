using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Data;
using Core.API.Utilities;
using Core.API.Utilities.Helpers;

namespace Core.API.DocumentSvc.Entity
{
    public class Base
    {
        internal string SerializeToString(object obj)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            using (StringWriter writer = new StringWriter())
            {
                var xmlWriter = XmlWriter.Create(writer, settings);
                serializer.Serialize(xmlWriter, obj, ns);

                return writer.ToString();
            }
        }
    }
}
