using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace Core.API.DocumentSvc.Entity
{
    [DataContract]
    public abstract class PointBaseAnnotationDto : AnnotationDto
    {
        protected PointBaseAnnotationDto()
        {
            Points = new List<PointDto>();
        }
        
        [DataMember]
        //[XmlArrayItem(Type = typeof (PointDto), ElementName = "Point")]
        public List<PointDto> Points {get; set;}
        
    }
}