using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Core.API.DocumentSvc.Entity
{
    [Serializable]
    public class APHoldDoc : Base
    {
        public int DocumentID { get; set; }
        public int ApproveLevel { get; set; }
        public string APHoldReason { get; set; }
        public bool APHold {get; set;}
        public DateTime APHoldDate { get; set; }
        public int UserId { get; set; }

        public APHoldDoc()
        {
        }

                         
        public APHoldDoc(int _DocumentID, int _ApproveLevel, string _APHoldReason, bool _APHold, DateTime _APHoldDate, int _UserId)
        {
            DocumentID = _DocumentID;
            ApproveLevel = _ApproveLevel;
            APHoldReason = _APHoldReason;
            APHold = _APHold;
            APHoldDate = _APHoldDate;
            UserId = _UserId;
        }

        public string GetXMLString()
        {
            string xmlMsg = base.SerializeToString(this);
            return xmlMsg;
        }
    }
}
