using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Core.API.DocumentSvc.Entity
{
    [DataContract]
    //[XmlRoot("AnnotationData")]
    public class AnnotationDataDto
    {
        public AnnotationDataDto()
        {

        }
        
        [DataMember]
        //[XmlArrayItem(Type = typeof(AnnotationPageDto), ElementName = "AnnotationPage")]
        public List<AnnotationPageDto> Pages { get; set; }
        //{
        //    get { return pages; }
        //    set { pages = value ?? new List<AnnotationPageDto>(); }
        //}
    }
}
