using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Core.API.UserSvc
{
    
    public class Users : IUsers
    {
        public string groups(long version)
        {
            string retMsg = "";

            UserBO _userBO = new UserBO();

            _userBO.LogAPIMessage(0, "REQUEST", "", "UserSvc", "groups - " + version.ToString(), 0);

            retMsg = _userBO.GetUserGroupsByRowVersion(version);

            _userBO.LogAPIMessage(0, "RESPONSE", retMsg, "UserSvc", "groups - " + version.ToString(), 0);

            return retMsg;
        }

        public string groupsall()
        {
            string retMsg = "";

            UserBO _userBO = new UserBO();

            _userBO.LogAPIMessage(0, "REQUEST", "", "UserSvc", "groupsall", 0);

            retMsg = _userBO.GetUserGroups();

            _userBO.LogAPIMessage(0, "RESPONSE", retMsg, "UserSvc", "groupsall", 0);

            return retMsg;
        }


        public string groupmembership(int groupid)
        {
            string retMsg = "";

            UserBO _userBO = new UserBO();

            _userBO.LogAPIMessage(0, "REQUEST", "", "UserSvc", "groupmembership - " + groupid.ToString(), 0);

            retMsg = _userBO.GetUserGroupMembersByGroupId(groupid);

            _userBO.LogAPIMessage(0, "RESPONSE", retMsg, "UserSvc", "groupmembership - " + groupid.ToString(), 0);

            return retMsg;
        }
        public string groupmembershipall()
        {
            string retMsg = "";
            UserBO _userBO = new UserBO();

            _userBO.LogAPIMessage(0, "REQUEST", "", "UserSvc", "groupmembershipall", 0);

            retMsg = _userBO.GetUserGroupMembersAll();

            _userBO.LogAPIMessage(0, "RESPONSE", retMsg, "UserSvc", "groupmembershipall", 0);

            return retMsg;
        }

        public string groupmembershipbyrwversion(long version)
        {
            string retMsg = "";
            UserBO _userBO = new UserBO();

            _userBO.LogAPIMessage(0, "REQUEST", "", "UserSvc", "groupmembershipbyrwversion - " + version.ToString(), 0);

            retMsg = _userBO.GetUserGroupMembersByRowVersion(version);

            _userBO.LogAPIMessage(0, "RESPONSE", retMsg, "UserSvc", "groupmembershipbyrwversion - " + version.ToString(), 0);

            return retMsg;
        }
        public string users(long version)
        {
            string retMsg = "";
            UserBO _userBO = new UserBO();

            _userBO.LogAPIMessage(0, "REQUEST", "", "UserSvc", "users - " + version.ToString(), 0);

            retMsg = _userBO.GetUsersByRowVersion(version);

            _userBO.LogAPIMessage(0, "RESPONSE", retMsg, "UserSvc", "users - " + version.ToString(), 0);

             return retMsg;
        }

        public string usersall()
        {
            string retMsg = "";

            UserBO _userBO = new UserBO();

            _userBO.LogAPIMessage(0, "REQUEST", "", "UserSvc", "usersall", 0);

            retMsg = _userBO.GetUsers();

            _userBO.LogAPIMessage(0, "RESPONSE", retMsg, "UserSvc", "usersall", 0);

            return retMsg;
        }

        public string permission(long version, int? ConnectionID)
        {
            string retMsg = "";
            UserBO _userBO = new UserBO();

            _userBO.LogAPIMessage(0, "REQUEST", "", "UserSvc", "permission - " + version.ToString(), 0);

            retMsg = _userBO.GetUsersPermissionsByRowVersion(version, ConnectionID);

            _userBO.LogAPIMessage(0, "RESPONSE", retMsg, "UserSvc", "permission - " + version.ToString(), 0);

             return retMsg;
        }

        public string permissionall(int? ConnectionID)
        {
            string retMsg = "";

            UserBO _userBO = new UserBO();

            _userBO.LogAPIMessage(0, "REQUEST", "", "UserSvc", "permissionall", 0);

            retMsg = _userBO.GetUsersPermissions(ConnectionID);

            _userBO.LogAPIMessage(0, "RESPONSE", retMsg, "UserSvc", "permissionall", 0);

            return retMsg;
        }
       
    }
}
