using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Core.API.UserSvc
{
    [Serializable]
    [XmlRoot("APIReturnMessage")]
    public class RetMessageFormatter
    {
        public string Title { get; set; }
        public string Message { get; set; }

        public RetMessageFormatter()
        {
        }

        public RetMessageFormatter(string title, string message)
        {
            Title = title;
            Message = message;
        }

        internal string GetReturnMessage()
        {
            string xmlMsg = SerializeToString(this);
            return xmlMsg;
        }

        static string SerializeToString(object obj)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, obj, ns);

                return writer.ToString();
            }
        }
    }
}
