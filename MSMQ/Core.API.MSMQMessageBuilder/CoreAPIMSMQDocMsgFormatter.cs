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
    public class CoreAPIMSMQDocMsgFormatter
    {
        public CoreAPIMSMQDocMsgFormatter()
        {
        }

        public CoreAPIMSMQDocMsgFormatter(string eventName, string eventDate, string connectionId, string documentId, string approvalLevel, string userId, string reason)
        {
            EventName = eventName;
            EventDate = eventDate;
            ConnectionId = connectionId;
            DocumentId = documentId;
            ApprovelLevel = approvalLevel;
            UserID = userId;
            Reason = reason;
            //Version = version;
        }
        public string EventName { get; set; }
        public string EventDate { get; set; }
        public string ConnectionId { get; set; }
        public string DocumentId { get; set; }
        public string ApprovelLevel { get; set; }
        public string UserID { get; set; }
        public string Reason { get; set; }

        //public string Version { get; set; }

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
