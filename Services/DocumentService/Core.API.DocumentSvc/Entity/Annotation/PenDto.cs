using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace Core.API.DocumentSvc.Entity
{
     [DataContract]
    //[XmlRoot("Pen")]
    public class PenDto
    {
        public PenDto()
        {
        }

         [DataMember]
        public float Width { get; set; }

         [DataMember]
        public string Color { get; set; }
    }
}