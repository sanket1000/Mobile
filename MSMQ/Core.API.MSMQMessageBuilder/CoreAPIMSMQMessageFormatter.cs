using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Core.API.MSMQMessageBuilder
{
    [Serializable]
    [XmlRoot("TScanEvent")]
    public  class CoreAPIMSMQMessageFormatter
    {
        public CoreAPIMSMQMessageFormatter()
        {
        }

        public CoreAPIMSMQMessageFormatter(string eventName, string id, string version)
        {
            EventName = eventName;
            Id = id;
            Version = version;
        }

        public CoreAPIMSMQMessageFormatter(string eventName, string id, string userid, string version)
        {
            EventName = eventName;
            Id = id;
            UserId = userid;
            Version = version;
        }

        public string EventName { get; set; }

        public string Id { get; set; }
        public string UserId { get; set; }
        public string Version { get; set; }

        internal string GetMSMQMsg()
        {
            string xmlMsg = SerializeToString(this);
            return xmlMsg;
        }

        static string SerializeToString(object obj)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());

            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);

                return writer.ToString();
            }
        }
    }
}
