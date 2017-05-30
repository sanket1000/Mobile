using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.API.UserSvc;

namespace Core.API.UserSvc.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            // DIRECT dll ref tests
            //bool blnGetUserGroups = GetUserGroups();

            //bool blnGetUserGroupsByRowVersion = GetUserGroupsByRowVersion();
            GetUserGroupsByRowVersion();
            //tcp user service test

            //bool blnGetUserGroupsSVC = GetUserGroupsByRowVersionSVC();
            //Core.API.UserSvc.Users usrs = new Users();
            //string xml1 = usrs.usersall();
            //UserServiceHandler.UsersClient client = new UserServiceHandler.UsersClient();
            //UserServiceHandler
            string xml = "";
             //xml = client.usersall();
             int i = 0;
            //string xml = client.users(
        }

        private static void GetUserGroups()
        {
            //UserServiceHandler.UsersClient client = new  UserServiceHandler.UsersClient();
            //client.groups(
            //string userGroups = new Core.API.DataAccess.UsersDA().GetUserGroups();
            //return string.IsNullOrEmpty(userGroups);

          
        }

        private static void GetUserGroupsByRowVersion()
        {
            //0x000000000028D799
            long rwVersion = 2740255;
            //long rwVersion = Convert.ToInt64(;
            //string strRwVersion = rwVersion.ToString();
            //string userGroups = new Core.API.DataAccess.UsersDA().GetUserGroupsByRowVersion(rwVersion).ToString();
            //return string.IsNullOrEmpty(userGroups);
        }


        private static void GetUserGroupsByRowVersionSVC()
        {
            long rwVersion = 2740255;
            //CoreAPIUserSvc.UsersClient usersSvc = new CoreAPIUserSvc.UsersClient();
            //string userGroups = usersSvc.groups(rwVersion);
            //return string.IsNullOrEmpty(userGroups);
        }
    }
}
