using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace Core.API.DocumentSvc.Entity
{

    //[XmlRoot("Brush")]
    [DataContract]
    public class BrushDto
    {

        public BrushDto()
        {
        }

        [DataMember]
        public string Color { get; set; }

        
    }
}