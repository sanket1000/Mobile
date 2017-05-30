using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.API.UserSvc
{
    internal class UserBO
    {
        internal string GetUserGroups()
        {
            string retMsg = "";
            try
            {
                retMsg = new UserDA().GetUserGroups();
            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                new UserDA().LogError("CoreAPIUserSvc", "GetUserGroups", ex.Message, innerExMsg, "", "");

                retMsg = ex.Message + "  --- " + innerExMsg;
                retMsg = new RetMessageFormatter("EXCEPTION", retMsg).GetReturnMessage();

            }

            return retMsg;
        }

        internal string GetUserGroupsByRowVersion(long rowVersion)
        {
            string retMsg = "";
            try
            {
                return new UserDA().GetUserGroupsByRowVersion(rowVersion);
            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                new UserDA().LogError("CoreAPIUserSvc", "GetUserGroupsByRowVersion", ex.Message, innerExMsg, "", "");

                retMsg = ex.Message + "  --- " + innerExMsg;
                retMsg = new RetMessageFormatter("EXCEPTION", retMsg).GetReturnMessage();
            }
            return retMsg;
        }


        ///////////////////////////

        internal string GetUserGroupMembersAll()
        {
            string retMsg = "";
            try
            {
                return new UserDA().GetUserGroupMembersAll();
            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                new UserDA().LogError("CoreAPIUserSvc", "GetUserGroupMembersAll", ex.Message, innerExMsg, "", "");

                retMsg = ex.Message + "  --- " + innerExMsg;
                retMsg = new RetMessageFormatter("EXCEPTION", retMsg).GetReturnMessage();
            }
            return retMsg;
        }

        internal string GetUserGroupMembersByGroupId(Int32 groupId)
        {
            string retMsg = "";
            try
            {
                return new UserDA().GetUserGroupMembersByGroupId(groupId);
            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                new UserDA().LogError("CoreAPIUserSvc", "GetUserGroupMembersByGroupId", ex.Message, innerExMsg, "", "");

                retMsg = ex.Message + "  --- " + innerExMsg;
                retMsg = new RetMessageFormatter("EXCEPTION", retMsg).GetReturnMessage();
            }
            return retMsg;
        }

        internal string GetUserGroupMembersByRowVersion(long rowVersion)
        {
            string retMsg = "";
            try
            {
                return new UserDA().GetUserGroupMembersByRowVersion(rowVersion);
            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                new UserDA().LogError("CoreAPIUserSvc", "GetUserGroupMembersByRowVersion", ex.Message, innerExMsg, "", "");

                retMsg = ex.Message + "  --- " + innerExMsg;
                retMsg = new RetMessageFormatter("EXCEPTION", retMsg).GetReturnMessage();
            }
            return retMsg;
        }
        //////////////////////////////

        internal string GetUsers()
        {
            string retMsg = "";
            try
            {
                return new UserDA().GetUsers();
            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                new UserDA().LogError("CoreAPIUserSvc", "GetUsers", ex.Message, innerExMsg, "", "");

                retMsg = ex.Message + "  --- " + innerExMsg;
                retMsg = new RetMessageFormatter("EXCEPTION", retMsg).GetReturnMessage();
            }
            return retMsg;
        }

        internal string GetUsersByRowVersion(long rowVersion)
        {
            string retMsg = "";
            try
            {
                return new UserDA().GetUsersByRowVersion(rowVersion);
            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                new UserDA().LogError("CoreAPIUserSvc", "GetUsersByRowVersion", ex.Message, innerExMsg, "", "");

                retMsg = ex.Message + "  --- " + innerExMsg;
                retMsg = new RetMessageFormatter("EXCEPTION", retMsg).GetReturnMessage();
            }
            return retMsg;
        }

        internal string GetUsersPermissions(int? ConnectionID)
        {
            string retMsg = "";
            try
            {
                return new UserDA().GetUsersPermissions(ConnectionID);
            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                new UserDA().LogError("CoreAPIUserSvc", "GetUsersPermissions", ex.Message, innerExMsg, "", "");

                retMsg = ex.Message + "  --- " + innerExMsg;
                retMsg = new RetMessageFormatter("EXCEPTION", retMsg).GetReturnMessage();
            }
            return retMsg;
        }


        internal string GetUsersPermissionsByRowVersion(long rowVersion, int? ConnectionID)
        {
            string retMsg = "";
            try
            {
                return new UserDA().GetUsersPermissionsByRowVersion(rowVersion, ConnectionID);
            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                new UserDA().LogError("CoreAPIUserSvc", "GetUsersPermissionsByRowVersion", ex.Message, innerExMsg, "", "");

                retMsg = ex.Message + "  --- " + innerExMsg;
                retMsg = new RetMessageFormatter("EXCEPTION", retMsg).GetReturnMessage();
            }
            return retMsg;
        }

        internal void LogAPIMessage(int DocumentID, string MessageType, string Message, string ScreenName, string Function, Int32 UserID)
        {
            try
            {
                new UserDA().LogAPIMessage(DocumentID, MessageType, Message, ScreenName, Function, UserID);
            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                new UserDA().LogError("CoreAPIUserSvc", "LogAPIMessage", ex.Message, innerExMsg, "", "");
            }
        }

    }
}
