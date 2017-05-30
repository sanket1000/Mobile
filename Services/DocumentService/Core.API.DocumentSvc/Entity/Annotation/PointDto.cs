using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace Core.API.DocumentSvc.Entity
{
     [DataContract]
    //[XmlRoot("Point")]
    public class PointDto
    {
        public PointDto()
        {
        }

         [DataMember]
        //[XmlAttribute]
        public float X { get; set; }

         [DataMember]
        //[XmlAttribute]
        public float Y { get; set; }
    }
}