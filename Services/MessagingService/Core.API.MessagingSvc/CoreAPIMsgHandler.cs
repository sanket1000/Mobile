using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Configuration;
using Core.API.MSMQ;
using Core.API.Utilities;
using Core.API.Utilities.Helpers;

namespace Core.API.MessagingSvc
{
    public class CoreAPIMsgHandler : ICoreAPIMsgHandler
    {
        public void QueueMessage(string Label, string QueueMsg)
        {
            try
            {
                // log incoming message
                APIMessageLogger.LogAPIMessage(0, "REQUEST", QueueMsg, Label, "Core.API.MessagingSvc.CoreAPIMsgHandler.QueueMessage", 0);

                string machineName = "";
                string queueName = "";

                if (System.Configuration.ConfigurationManager.AppSettings["MSMQMachineName"] != null)
                    machineName = System.Configuration.ConfigurationManager.AppSettings["MSMQMachineName"];

                if (System.Configuration.ConfigurationManager.AppSettings["MSMQName"] != null)
                    queueName = System.Configuration.ConfigurationManager.AppSettings["MSMQName"];

                CoreAPIMessageHandler.QueueMessage(machineName, queueName, Label, QueueMsg);
            }
            catch (Exception ex)
            {
                TSErrorLogger.LogError("MessagingSvc.CoreAPIMsgHandler", "QueueMessage", string.IsNullOrEmpty(ex.Message) ? "" : ex.Message, (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message)) ? ex.InnerException.Message : "", string.IsNullOrEmpty(ex.StackTrace) ? "" : ex.StackTrace, "");
            }
        }
        
    }
}
