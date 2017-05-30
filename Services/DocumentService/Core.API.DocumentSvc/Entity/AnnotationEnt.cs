using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using CA.Imaging.Annotations;
using CA.Imaging.Annotations.Serialization;
using CA.Imaging.Models;

namespace Core.API.DocumentSvc.Entity
{
    [DataContract]
    public class AnnotationEnt
    {
        [DataMember]
        public AnnotationDataDto AnnotationData { get; set; }
        
    }
}
