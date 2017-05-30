using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Core.API.DocumentSvc.Entity
{
     [DataContract]
    //[XmlRoot("AnnotationPage")]
    public class AnnotationPageDto
    {
         public AnnotationPageDto()
         {
         }

       [DataMember]
        public int PageIndex { get; set; }

        [DataMember]
        //[XmlArrayItem(Type = typeof(AnnotationLayerDto), ElementName = "AnnotationLayer")]
        public List<AnnotationLayerDto> Layers {get ; set;}
        
    }
}