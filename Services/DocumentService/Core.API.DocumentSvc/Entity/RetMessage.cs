using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Core.API.DocumentSvc.Entity
{
    // [Serializable]
    [DataContract]
    public class RetMessage 
    {
        [DataMember]
        public Int32 Id { get; set; }

        [DataMember]
        public string Message { get; set; }

        public RetMessage()
        {
        }

        public RetMessage(int _Id, string _Message)
        {
            Id = _Id;
            _Message = Message;
        }

        //public string GetXMLString()
        //{
        //    string xmlMsg = base.SerializeToString(this);
        //    return xmlMsg;
        //}
    }
}
