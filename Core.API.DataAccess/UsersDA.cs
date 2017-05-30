using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.Entity;
using Core.API.EntityModel;
using Core.API.Helpers;

namespace Core.API.DataAccess
{
    public class UsersDA : DataAccessBase
    {

        public string GetUsers()
        {
            using (var ctx = new CoreAPIEntities(ECB.ConnectionString))
            {
                var us = ctx.GetUsers().AsQueryable().First().ToString();
                return us;
            }
        }

        public string GetUsersByRowVersion(long rowVersion)
        {
            using (var ctx = new CoreAPIEntities(ECB.ConnectionString))
            {
                var us = ctx.GetUsesrByRowVersion(rowVersion).AsQueryable().First().ToString();
                return us;
            }
        }

        public string GetUserGroups()
        {
            using (var ctx = new CoreAPIEntities())
            {
                var ugs = ctx.GetUserGroupsAll().AsQueryable().First().ToString();
                return ugs;
            }
        }

        public string GetUserGroupsByRowVersion(long rowVersion)
        {
            //using (var ctx = new CoreAPIEntities(ECB.ConnectionString))
            using (var ctx = new CoreAPIEntities())
            {
                var ugs = ctx.GetUserGroupsByRowVersion(rowVersion).AsQueryable().First().ToString();
                return ugs;
            }
        }

        public string GetUserGroupMembersAll()
        {
            using (var ctx = new CoreAPIEntities(ECB.ConnectionString))
            {
                var ugms = ctx.GetUserGroupMembers().AsQueryable().First().ToString();
                return ugms;
            }
        }

        public string GetUserGroupMembersByGroupId(Int32 groupId)
        {
            using (var ctx = new CoreAPIEntities(ECB.ConnectionString))
            {
                var ugms = ctx.GetUserGroupMembersByGroupId(groupId).AsQueryable().First().ToString();
                return ugms;
            }
        }

        public string GetUsersPermissions(int? ConnectionID)
        {
            using (var ctx = new CoreAPIEntities(ECB.ConnectionString))
            {
                var usps = ctx.GetUsersPermissions(ConnectionID).AsQueryable().First().ToString();
                return usps;
            }
        }

        public string GetUsersPermissionsByRowVersion(long rowVersion, int? ConnectionID)
        {
            using (var ctx = new CoreAPIEntities(ECB.ConnectionString))
            {
                var usps = ctx.GetUsersPermissionsByRowVersion(rowVersion, ConnectionID).AsQueryable().First().ToString();
                return usps;
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
