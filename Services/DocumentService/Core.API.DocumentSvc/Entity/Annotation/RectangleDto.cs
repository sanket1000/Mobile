using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace Core.API.DocumentSvc.Entity
{
     [DataContract]
   // [XmlRoot("Rectangle")]
    public class RectangleDto
    {
        public RectangleDto()
        {
        }

         [DataMember]
        public float Height { get; set; }

         [DataMember]
        public float Width { get; set; }

         [DataMember]
        public float Y { get; set; }

         [DataMember]
        public float X { get; set; }
    }
}