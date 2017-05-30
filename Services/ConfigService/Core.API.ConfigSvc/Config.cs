using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Core.API.ConfigSvc
{
    
    public class Config : IConfig
    {
        public string configuration(int ConnectionID = 1)
        {
            string retMsg = "";

            ConfigBuilder.LogAPIMessage(0, "REQUEST", "", "ConfigSvc", "configuration", 0);

            retMsg = ConfigBuilder.GetConfigurations(ConnectionID);

            ConfigBuilder.LogAPIMessage(0, "RESPONSE", retMsg, "ConfigSvc", "configuration", 0);

            return retMsg;
        }

        public string headerconfiguration(int ConnectionID = 1)
        {
            string retMsg = "";

            ConfigBuilder.LogAPIMessage(0, "REQUEST", "", "ConfigSvc", "headerconfiguration", 0);

            retMsg = ConfigBuilder.GetHeaderConfiguration(ConnectionID);

            ConfigBuilder.LogAPIMessage(0, "RESPONSE", retMsg, "ConfigSvc", "headerconfiguration", 0);

            return retMsg;
        }

        public string distributionconfiguration(int ConnectionID = 1)
        {
            string retMsg = "";

            ConfigBuilder.LogAPIMessage(0, "REQUEST", "", "ConfigSvc", "distributionconfiguration", 0);

            retMsg = ConfigBuilder.GetDistributionConfiguration(ConnectionID);

            ConfigBuilder.LogAPIMessage(0, "RESPONSE", retMsg, "ConfigSvc", "distributionconfiguration", 0);
            return retMsg;
        }
    }
}
