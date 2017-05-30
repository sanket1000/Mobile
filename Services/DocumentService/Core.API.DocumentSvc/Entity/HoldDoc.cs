using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Core.API.DocumentSvc.Entity
{
    [Serializable]
    public class HoldDoc : Base
    {
        public int DocumentID { get; set; }
        public int ApproveLevel { get; set; }
        public string HoldReason { get; set; }
        public DateTime HoldDate { get; set; }
        public int UserId { get; set; }

        public HoldDoc()
        {
        }


        public HoldDoc(int _DocumentID, int _ApproveLevel, string _HoldReason, DateTime _HoldDate, int _UserId)
        {
            DocumentID = _DocumentID;
            ApproveLevel = _ApproveLevel;
            HoldReason = _HoldReason;
            HoldDate = _HoldDate;
            UserId = _UserId;
        }

        public string GetXMLString()
        {
            string xmlMsg = base.SerializeToString(this);
            return xmlMsg;
        }
    }
}
