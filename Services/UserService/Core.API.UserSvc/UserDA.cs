using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.API.EntityModel;
using Core.API.Utilities;
using System.Data.EntityClient;
using System.Data.SqlClient;

namespace Core.API.UserSvc
{
    internal class UserDA
    {
        string _TimberScanConnString = null;
        internal string TimberScanConnString
        {
            get {
                if (string.IsNullOrEmpty(_TimberScanConnString))
                {
                    _TimberScanConnString = Core.API.Utilities.Helpers.APIUtilityHelper.TimberScanEntityConnectionString;
                }
                return _TimberScanConnString;
            }
        }
        //public UserDA()
        //{
        //    if (string.IsNullOrEmpty(_TimberScanConnString))
        //    {
        //        _TimberScanConnString = Core.API.Utilities.Helpers.APIUtilityHelper.TimberScanConnectionString;
        //    }
        //}
        public string GetUsers()
        {
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                var us = ctx.GetUsers().AsQueryable().First().ToString();
                return us;
            }
        }

        public string GetUsersByRowVersion(long rowVersion)
        {
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                var us = ctx.GetUsesrByRowVersion(rowVersion).AsQueryable().First().ToString();
                return us;
            }
        }

        public string GetUserGroups()
        {
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                var ugs = ctx.GetUserGroupsAll().AsQueryable().First().ToString();
                return ugs;
            }
        }

        public string GetUserGroupsByRowVersion(long rowVersion)
        {
            //using (var ctx = new CoreAPIEntities(ECB.ConnectionString))
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                var ugs = ctx.GetUserGroupsByRowVersion(rowVersion).AsQueryable().First().ToString();
                return ugs;
            }
        }

        public string GetUserGroupMembersAll()
        {
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                var ugms = ctx.GetUserGroupMembers().AsQueryable().First().ToString();
                return ugms;
            }
        }

        public string GetUserGroupMembersByGroupId(Int32 groupId)
        {
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                var ugms = ctx.GetUserGroupMembersByGroupId(groupId).AsQueryable().First().ToString();
                return ugms;
            }
        }

        public string GetUserGroupMembersByRowVersion(long rowVersion)
        {
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                var ugms = ctx.GetUserGroupMembersByRwVersion(rowVersion).AsQueryable().First().ToString();
                return ugms;
            }
        }


        public string GetUsersPermissions(int? ConnectionID)
        {
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                var usps = ctx.GetUsersPermissions(ConnectionID).AsQueryable().First().ToString();
                return usps;
            }
        }

        public string GetUsersPermissionsByRowVersion(long rowVersion, int? ConnectionID)
        {
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                var usps = ctx.GetUsersPermissionsByRowVersion(rowVersion, ConnectionID).AsQueryable().First().ToString();
                return usps;
            }
        }

        public void LogError(string ScreenName, string FunctionName, string Exception, string InnserException, string ErrMessage, string UserID)
        {
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                var tblErrorLog = new EntityModel.TSErrorLog();
                tblErrorLog.FunctionName = FunctionName;
                tblErrorLog.ScreenName = ScreenName;
                tblErrorLog.Exception = Exception;
                tblErrorLog.InnerException = InnserException;
                tblErrorLog.ErrMessage = ErrMessage;
                tblErrorLog.CreatedDate = DateTime.Now;
                tblErrorLog.CreatedUserID = UserID;
                ctx.TSErrorLogs.AddObject(tblErrorLog);
                ctx.SaveChanges();

            }

        }
        
        public void LogAPIMessage(int DocumentID, string MessageType, string Message, string ScreenName, string Function, Int32 UserID)
        {
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                var apiMessageLog = new EntityModel.APIMessageLog();
                apiMessageLog.DocumentID = DocumentID;
                apiMessageLog.MessageType = MessageType;
                apiMessageLog.Message = Message;
                apiMessageLog.UserID = UserID;
                apiMessageLog.CreatedDate = DateTime.Now;
                apiMessageLog.Application = "API";
                apiMessageLog.ScreenName = ScreenName;
                apiMessageLog.Function = Function;
                ctx.APIMessageLogs.AddObject(apiMessageLog);
                ctx.SaveChanges();

            }
        }
    
        /// <summary>
        /// Returns user groups
        /// </summary>
        /// <returns></returns>
        //public List<tblUserGroup> GetUserGroups()
        //{
        //    using (var ctx = new CoreAPIEntities(ECB.ConnectionString))
        //    {
        //        var ugs = ctx.tblUserGroups.ToList();
        //        return ugs;
        //    }
        //}



        /// <summary>
        /// Returns list of user groups updated after passed row version
        /// </summary>
        /// <param name="rowVersion"></param>
        /// <returns></returns>
        //public List<tblUserGroup> GetUserGroupsByRowVersion(long rowVersion)
        //{
        //    using (var ctx = new CoreAPIEntities(ECB.ConnectionString))
        //    {
        //        var ugs = ctx.tblUserGroups.AsEnumerable().OrderBy(x => x.RwVersion, new RowVersionComparer()).ToList();
        //        return ugs;
        //    }
        //}



        /// <summary>
        /// <summary>
        /// return all User group member records
        /// </summary>
        /// <returns></returns>
        //public List<tblUserGroupMember> GetUserGroupMembers()
        //{
        //    using (var ctx = new CoreAPIEntities(ECB.ConnectionString))
        //    {
        //        var ugms = ctx.tblUserGroupMembers.ToList();
        //        return ugms;
        //    }
        //}


        /// <summary>
        /// Returns list of user group members updated after passed row version
        /// </summary>
        /// <param name="rowVersion"></param>
        /// <returns></returns>
        //public List<tblUserGroupMember> GetUserGroupMembersByRowVersion(long rowVersion)
        //{
        //    using (var ctx = new CoreAPIEntities(ECB.ConnectionString))
        //    {
        //        var ugms = ctx.tblUserGroupMembers.AsEnumerable().OrderBy(x => x.RwVersion, new RowVersionComparer()).ToList();
        //        return ugms;
        //    }
        //}

        /// <summary>
        /// Returns list of user group members by group id
        /// </summary>
        /// <param name="rowVersion"></param>
        /// <returns></returns>
        //public List<tblUserGroupMember> GetUserGroupMembersByGroupID(int groupId)
        //{
        //    using (var ctx = new CoreAPIEntities(ECB.ConnectionString))
        //    {
        //        var ugms = ctx.tblUserGroupMembers.Where(x => x.UserGroupID.Equals(groupId)).ToList();
        //        return ugms;
        //    }
        //}


        /// <summary>
        /// return all User records
        /// </summary>
        /// <returns></returns>
        //public List<tblUser> GetUsers()
        //{
        //    using (var ctx = new CoreAPIEntities(ECB.ConnectionString))
        //    {
        //        var us = ctx.tblUsers.ToList();
        //        return us;
        //    }
        //}



        /// <summary>
        /// Returns list of users updated after passed row version
        /// </summary>
        /// <param name="rowVersion"></param>
        /// <returns></returns>

        //public List<tblUser> GetUsersByRowVersion(long rowVersion)
        //{
        //    using (var ctx = new CoreAPIEntities(ECB.ConnectionString))
        //    {
        //        var us = ctx.tblUsers.AsEnumerable().OrderBy(x => x.RwVersion, new RowVersionComparer()).ToList();
        //        return us;
        //    }
        //}



        /// <summary>
        /// return all user permission records
        /// </summary>
        /// <returns></returns>
        //public List<tblUserPermission> GetUsersPermissions()
        //{
        //    using (var ctx = new CoreAPIEntities(ECB.ConnectionString))
        //    {
        //        var usps = ctx.tblUserPermissions.ToList();
        //        return usps;
        //    }
        //}


        /// <summary>
        /// Returns list of users permissions updated after passed row version
        /// </summary>
        /// <param name="rowVersion"></param>
        /// <returns></returns>
        //public List<tblUserPermission> GetUsersPermissionsByRowVersion(long rowVersion)
        //{
        //    using (var ctx = new CoreAPIEntities(ECB.ConnectionString))
        //    {
        //        var usps = ctx.tblUserPermissions.AsEnumerable().OrderBy(x => x.RwVersion, new RowVersionComparer()).ToList();
        //        return usps;
        //    }
        //}

    }
}
