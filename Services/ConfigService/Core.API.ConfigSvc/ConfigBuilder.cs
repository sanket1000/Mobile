using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Core.API.TLSettings;
using Core.API.ConfigSvc.Model;

namespace Core.API.ConfigSvc
{
    internal class ConfigBuilder
    {


        internal static string GetHeaderConfiguration(int ConnectionID)
        {
            string xmlConfig = "";
            try
            {
                HeaderConfig hdConfig = new HeaderConfig(ConnectionID);

                if (hdConfig != null)
                {
                    xmlConfig = SerializeToString(hdConfig);
                }
            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                ConfigDA.LogError("CoreAPIConfigSvc", "GetHeaderConfiguration", ex.Message, innerExMsg, "", "");

                xmlConfig = ex.Message + "  --- " + innerExMsg;
                xmlConfig = new RetMessageFormatter("EXCEPTION", xmlConfig).GetReturnMessage();
            }
            return xmlConfig;
        }

        internal static string GetDistributionConfiguration(int ConnectionID)
        {
            string xmlConfig = "";
            try
            {
                DistConfigs distConfigs = new DistConfigs(ConnectionID);

                if (distConfigs != null)
                {
                    xmlConfig = SerializeToString(distConfigs);
                }
            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                ConfigDA.LogError("CoreAPIConfigSvc", "GetDistributionConfiguration", ex.Message, innerExMsg, "", "");

                xmlConfig = ex.Message + "  --- " + innerExMsg;
                xmlConfig = new RetMessageFormatter("EXCEPTION", xmlConfig).GetReturnMessage();
            }

            return xmlConfig;
        }

        internal static string GetConfigurations(int ConnectionID)
        {
            string xmlConfig = "";
            try
            {
                Configurations configs = new Configurations(ConnectionID);

                if (configs != null)
                {
                    xmlConfig = SerializeToString(configs);
                }
            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                ConfigDA.LogError("CoreAPIConfigSvc", "GetConfigurations", ex.Message, innerExMsg, "", "");

                xmlConfig = ex.Message + "  --- " + innerExMsg;
                xmlConfig = new RetMessageFormatter("EXCEPTION", xmlConfig).GetReturnMessage();
            }
            return xmlConfig;
        }

        internal static string SerializeToString(object obj)
        {

            XmlSerializer serializer = new XmlSerializer(obj.GetType());

            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);

                return writer.ToString();
            }
        }

        internal static void LogAPIMessage(int DocumentID, string MessageType, string Message, string ScreenName, string Function, Int32 UserID)
        {
            try
            {
                ConfigDA.LogAPIMessage(DocumentID, MessageType, Message, ScreenName, Function, UserID);
            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                ConfigDA.LogError("CoreAPICongicSvc", "LogAPIMessage", ex.Message, innerExMsg, "", "");
            }
        }
    }
}
