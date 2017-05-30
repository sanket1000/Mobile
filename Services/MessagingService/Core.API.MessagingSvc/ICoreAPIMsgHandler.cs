using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Net.Security;
using System.Text;

namespace Core.API.MessagingSvc
{
    
    [ServiceContract(ProtectionLevel = ProtectionLevel.None)]
    public interface ICoreAPIMsgHandler
    {
        [OperationContract(IsOneWay = true)]
        void QueueMessage(string Label, string QueueMsg);


        // TODO: Add your service operations here
    }

}
    
