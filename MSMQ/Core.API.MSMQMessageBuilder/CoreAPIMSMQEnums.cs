using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.API.MSMQMessageBuilder
{
    public class CoreAPIMSMQEnums
    {
        public enum EnumUserSvc
        {
            UserUpdate,
            UserDelete,
            UserGroupUpdate,
            UserGroupDelete,
            UserPermissionUpdate,
            UserPermissionDelete,
            UserGroupMemberShipInsert,
            UserGroupMemberShipDelete
        }

        public enum EnumDocumentSvc
        {
            DocumentApproved,
            DocumentCreated,
            DocumentUpdated,
            DocumentRouted,
            DocumentDelete,
            DocumentReject,
            DocumentHold,
            DocumentAPHold,
            DocumentNoteCreated,
            DocumentFileChange,
            DocumentAnnotationChange
        }
    }
}
