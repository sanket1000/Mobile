using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Core.API.DocumentSvc.Entity
{
    //[Serializable]
    //[XmlRoot("File")]
       [DataContract]
    public class DocFile 
    {
        [DataMember]
        public int Id { get; set; } // invoice id
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public long FileSize { get; set; } // invoice id
        [DataMember]
        public string FileType { get; set; }
        [DataMember]
        public DateTime CreatedOnUTC { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public bool IsArchived { get; set; }

    }
}
