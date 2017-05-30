using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Drawing;

namespace Core.API.DocumentSvc.Entity
{
    [DataContract]
    //[XmlRoot("TextAnnotation")]
    public class TextAnnotationDto : AnnotationDto
    {
        public TextAnnotationDto()
        {
        }

        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public BrushDto Fill { get; set; }
        [DataMember]
        public StringAlignment LineAlignment { get; set; }
        [DataMember]
        public StringAlignment Alignment { get; set; }
        [DataMember]
        public bool Minimized { get; set; }
        [DataMember]
        public bool AutoSize { get; set; }
        [DataMember]
        public bool AllowEditing { get; set; }
        [DataMember]
        public float Padding { get; set; }
        [DataMember]
        public FontDto Font { get; set; }
        [DataMember]
        public BrushDto FontBrush { get; set; }
        [DataMember]
        public PenDto Outline { get; set; }
        [DataMember]
        public StringTrimming Trimming { get; set; }

        
    }
}