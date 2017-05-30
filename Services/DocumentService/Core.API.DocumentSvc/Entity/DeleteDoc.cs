using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Core.API.DocumentSvc.Entity
{
    public class DeleteDoc : Base
    {
        public int DocumentID { get; set; }
        public int ApproveLevel { get; set; }
        public string DeleteReason { get; set; }
        public int UserId { get; set; }

        public DeleteDoc()
        {
        }

        public DeleteDoc(int _DocumentID, int _ApproveLevel, string _DeleteReason, int _UserId)
        {
            DocumentID = _DocumentID;
            ApproveLevel = _ApproveLevel;
            DeleteReason = _DeleteReason;
            UserId = _UserId;
        }

        public string GetXMLString()
        {
            string xmlMsg = base.SerializeToString(this);
            return xmlMsg;
        }
    }
}
