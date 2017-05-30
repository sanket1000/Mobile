using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Core.API.ConfigSvc
{
    
    [ServiceContract]
    public interface IConfig
    {

        [OperationContract]
        string configuration(int ConnectionID = 1);

        [OperationContract]
        string headerconfiguration(int ConnectionID = 1);

         [OperationContract]
        string distributionconfiguration(int ConnectionID = 1);
    }
}
