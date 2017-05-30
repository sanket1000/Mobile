using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Core.API.TLSettings;
using Core.API.TLSettings.Models;
using Core.API.ConfigSvc.Model;

namespace Core.API.ConfigSvc.Model
{
    [XmlRoot("Configurations")]
    public class Configurations
    {
        [XmlElement("HeaderConfiguration")]
        public  HeaderConfig hdConfig {get; set;}
        
        [XmlElement("DistributionConfigurations")]
        public DistConfigs distConfigs {get; set;}

        public Configurations()
        {
        }
        public Configurations(int ConnectionID)
        {
            hdConfig = new HeaderConfig(ConnectionID);

            distConfigs = new DistConfigs(ConnectionID);
        }
    }
}
