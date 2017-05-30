using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.API.MSMQMessageBuilder
{
    public class CoreAPIMSMQMessage
    {
        public static string GetUserSvcMessage(int Id, long RwVersion, CoreAPIMSMQEnums.EnumUserSvc EnumUserSvcUpdate)
        {
            string xmlMsg = GetUserSvcMSMQMsg(Id, RwVersion,EnumUserSvcUpdate.ToString());

            //switch (EnumUserSvcUpdate)
            //{
            //    case CoreAPIMSMQEnums.EnumUserSvc.UserUpdate:
            //        xmlMsg = GetUserUpdateMsg(Id, RwVersion);
            //        break;
            //    case CoreAPIMSMQEnums.EnumUserSvc.UserGroupUpdate:
            //        xmlMsg = GetUserGroupUpdateMsg(Id, RwVersion);
            //        break;
            //    case CoreAPIMSMQEnums.EnumUserSvc.UserPermissionUpdate:
            //        xmlMsg = GetUserPermissionUpdateMsg(Id, RwVersion);
            //        break;
            //    case CoreAPIMSMQEnums.EnumUserSvc.UserGroupMembershipUpdate:
            //        xmlMsg = GetUserGroupMembershipUpdateMsg(Id, RwVersion);
            //        break;
            //}

            return xmlMsg;
        }

        public static string GetUserSvcMessage(int Id, int UserId, long RwVersion, CoreAPIMSMQEnums.EnumUserSvc EnumUserSvcUpdate)
        {
            string xmlMsg = GetUserSvcMSMQMsg(Id, UserId, RwVersion, EnumUserSvcUpdate.ToString());
            return xmlMsg;
        }

        public static string GetDocumentSvcMessage(string ConnectionId, string DocumentId, string ApprovalLevel, string UserId, string Reason, DateTime EventDate, CoreAPIMSMQEnums.EnumDocumentSvc EnumDocSvcUpdate)
        {
            string xmlMsg = GetDocumentSvcMSMQMsg(EnumDocSvcUpdate.ToString(), EventDate, ConnectionId, DocumentId, ApprovalLevel, UserId, Reason);
            return xmlMsg;
        }

        private static string GetUserSvcMSMQMsg(int Id, long RwVersion, string Event)
        {
            return new CoreAPIMSMQMessageFormatter(Event, Id.ToString(), RwVersion.ToString()).GetMSMQMsg();
        }
        private static string GetUserSvcMSMQMsg(int Id, int UserId, long RwVersion, string Event)
        {
            return new CoreAPIMSMQMessageFormatter(Event, Id.ToString(), UserId.ToString(), RwVersion.ToString()).GetMSMQMsg();
        }
        private static string GetDocumentSvcMSMQMsg(string Event, DateTime EventDate, string ConnectionId, string DocumentId, string ApprovalLevel, string UserId, string Reason)
        {
            return new CoreAPIMSMQDocMsgFormatter(Event, EventDate.ToShortDateString(), ConnectionId, DocumentId, ApprovalLevel, UserId, Reason).GetMSMQMsg();
        }
        //private static string GetUserGroupUpdateMsg(int Id, long RwVersion, string Event)
        //{
        //    return new CoreAPIMSMQMessageFormatter(Event, Id.ToString(), RwVersion.ToString()).GetMSMQMsg();
        //}

        //private static string GetUserPermissionUpdateMsg(int Id, long RwVersion, string Event)
        //{
        //    return new CoreAPIMSMQMessageFormatter(Event, Id.ToString(), RwVersion.ToString()).GetMSMQMsg();
        //}

        //private static string GetUserGroupMembershipUpdateMsg(int Id, long RwVersion, string Event)
        //{
        //    return new CoreAPIMSMQMessageFormatter(Event, Id.ToString(), RwVersion.ToString()).GetMSMQMsg();
        //}
    }
}
