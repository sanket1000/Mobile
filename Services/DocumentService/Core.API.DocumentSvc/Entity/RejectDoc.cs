using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Core.API.DocumentSvc.Entity
{
    [Serializable]
    public class RejectDoc : Base
    {
        public int DocumentID { get; set; }
        public int ActionLevel { get; set; }
        public int RejectLevel { get; set; }
        public string RejectReason { get; set; }
        public DateTime RejectDate { get; set; }
        public int UserId { get; set; }

        public RejectDoc()
        {
        }

        public RejectDoc(int _DocumentID, int _ActionLevel, int _RejectLevel, string _RejectReason, DateTime _RejectDate, int _UserId)
        {
            DocumentID = _DocumentID;
            ActionLevel = _ActionLevel;
            RejectLevel = _RejectLevel;
            RejectReason = _RejectReason;
            RejectDate = _RejectDate;
            UserId = _UserId;
        }

        public string GetXMLString()
        {
            string xmlMsg = base.SerializeToString(this);
            return xmlMsg;
        }
    }
}
