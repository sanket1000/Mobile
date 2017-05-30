using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Core.API.DocumentSvc.Entity
{
    [Serializable]
    public class Comment : Base
    {
        public int DocumentID { get; set; }
        //public int ActionLevel { get; set; }
        public string NoteText { get; set; }
        public DateTime NoteDate { get; set; }
        public int UserId { get; set; }

        public Comment()
        {
        }

        public Comment(int _DocumentID, string _NoteText, DateTime _NoteDate, int _UserId)
        {
            DocumentID = _DocumentID;
            //ActionLevel = _ActionLevel;
            NoteText = _NoteText;
            NoteDate = NoteDate;
            UserId = _UserId;

        }
        
        public string GetXMLString()
        {
            string xmlMsg = base.SerializeToString(this);
            return xmlMsg;
        }
    }
}
