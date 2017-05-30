using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace Core.API.DocumentSvc.Entity
{
     [DataContract]
    //[XmlRoot("PenAnnotation")]
    public class PenAnnotationDto : PointBaseAnnotationDto
    {
        public PenAnnotationDto()
        {
            Pen = new PenDto();
        }
         [DataMember]
        public PenDto Pen { get; set; }

       
    }
}