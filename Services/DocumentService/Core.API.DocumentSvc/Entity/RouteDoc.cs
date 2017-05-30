using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Core.API.DocumentSvc.Entity
{
    [Serializable]
    public class RouteDoc : Base
    {
        int DocumentID;
        int ActionLevel; int RoutingUserID; int RouteToID; string RouteToComment;

        public RouteDoc()
        {
        }

        public RouteDoc(int _DocumentID, int _ActionLevel, int _RoutingUserID, int _RouteToID, string _RouteToComment)
        {
            DocumentID = _DocumentID;
            ActionLevel = _ActionLevel;
            RoutingUserID = _RoutingUserID;
            RouteToID = _RouteToID;
            RouteToComment = _RouteToComment;
        }

        public string GetXMLString()
        {
            string xmlMsg = base.SerializeToString(this);
            return xmlMsg;
        }
    }
}
