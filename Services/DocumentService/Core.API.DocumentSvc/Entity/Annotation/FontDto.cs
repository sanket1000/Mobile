using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace Core.API.DocumentSvc.Entity
{
    //[XmlRoot("Font")]
    [DataContract]
    public class FontDto
    {
        private string type = "Font";

        public FontDto()
        {
        }

        

        [DataMember]
        public float Size { get; set; }

        [DataMember]
        public bool Italic { get; set; }

        [DataMember]
        public bool Underline { get; set; }

        [DataMember]
        public bool Bold { get; set; }

        [DataMember]
        public bool Strikeout { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
       // [XmlAttribute]
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        
    }
}