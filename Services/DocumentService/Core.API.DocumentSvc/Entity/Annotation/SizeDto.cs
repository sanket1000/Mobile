using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace Core.API.DocumentSvc.Entity
{
     [DataContract]
    //[XmlRoot("Size")]
    public class SizeDto
    {
        public SizeDto()
        {
        }
         [DataMember]
        //[XmlAttribute]
        public float Width { get; set; }

         [DataMember]
        //[XmlAttribute]
        public float Height { get; set; }
    }
}