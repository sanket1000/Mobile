using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace Core.API.DocumentSvc.Entity
{
     [DataContract]
   // [XmlRoot("RectangleAnnotation")]
    public class RectangleAnnotationDto : AnnotationDto
    {
        public RectangleAnnotationDto()
        {
        }

         [DataMember]
        public PenDto Outline { get; set; }

         [DataMember]
        public BrushDto Fill { get; set; }

    }
}