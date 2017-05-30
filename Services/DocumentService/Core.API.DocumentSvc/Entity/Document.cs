using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace Core.API.DocumentSvc.Entity
{
    //[Serializable]
    //[XmlRoot("Document")]
    [DataContract]
    public class DocumentEnt 
    {
        [DataMember]
        public DocHeader Header { get; set; }

        //[XmlElement("Distributions")]
        [DataMember]
        public List<DocDistribution>  Distributions { get; set; }

        [DataMember]
        public int DocumentID { get; set; }

        [DataMember]
        public int ConnectionID { get; set; }

        [DataMember]
        public int ApproveLevel { get; set; }

        [DataMember]
        public string ApproveType { get; set; }

        [DataMember]
        public bool OnHold { get; set; }

        [DataMember]
        public bool OnAPHold { get; set; }


        [DataMember]
        public int GroupID { get; set; }

        [DataMember]
        public int UserID { get; set; }

        [DataMember]
        public string DocumentType { get; set; }

        [DataMember]
        public string DocumentSubType { get; set; }

        //[XmlElement("ApprovalHistory")]
        //[DataMember]
        //public DocApproval Approvals { get; set; }

        //[DataMember]
        //public DocFile File { get; set; }

        public DocumentEnt()
        {
            Header = new DocHeader();
            Distributions = new List<DocDistribution>();
            //Approvals = new DocApproval();
            //File = new DocFile();
        }

        //public string GetXMLString()
        //{
        //    string xmlMsg = base.SerializeToString(this);
        //    return xmlMsg;
        //}
    }
}
