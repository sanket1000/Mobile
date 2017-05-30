using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.API.DataAccess;
using Core.API.Entities;
using Core.API.EntityModel;

namespace Core.API.UserSvc
{
    public class AutomapBootstrap
    {
        public static void InitializeMap()
        {
            Mapper.CreateMap<List<tblUserGroupMember>, List<UserGroupMember>>();
            Mapper.CreateMap<List<tblUserGroup>, List<UserGroup>>();
            Mapper.CreateMap<List<tblUserPermission>, List<UserPermission>>();
            Mapper.CreateMap<List<tblUser>, List<Users>>();
        }
    }
}
