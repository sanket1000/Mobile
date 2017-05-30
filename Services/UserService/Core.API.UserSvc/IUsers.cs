using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Core.API.UserSvc
{
    
    [ServiceContract]
    public interface IUsers
    {
        [OperationContract]
        string groups(long version);

        [OperationContract]
        string groupsall();

         [OperationContract]
        string groupmembership(int groupid);

         [OperationContract]
         string groupmembershipbyrwversion(long version);

         [OperationContract]
         string groupmembershipall();

         [OperationContract]
         string users(long version);

         [OperationContract]
         string usersall();

         [OperationContract]
         string permission(long version, int? ConnectionID);

         [OperationContract]
         string permissionall(int? ConnectionID);
        
    }
}
