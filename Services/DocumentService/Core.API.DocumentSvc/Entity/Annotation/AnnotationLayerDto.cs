using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Core.API.DocumentSvc.Entity
{
    [DataContract]
    //[XmlRoot("AnnotationLayer")]
    public class AnnotationLayerDto
    {
        public AnnotationLayerDto()
        {
            PenAnnotations = new List<PenAnnotationDto>();
            TextAnnotations = new List<TextAnnotationDto>();
            RectanngleAnnotations = new List<RectangleAnnotationDto>();
        }

        [DataMember]
        public List<PenAnnotationDto> PenAnnotations { get; set; }

        [DataMember]
        public List<TextAnnotationDto> TextAnnotations { get; set; }

        [DataMember]
        public List<RectangleAnnotationDto> RectanngleAnnotations { get; set; }
        
    }
}