using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using Core.API.ConfigSvc;

namespace Core.API.ConfigSvcHost
{
    public partial class ConfigSvcHost : ServiceBase
    {
        internal static ServiceHost coreAPIConfigSvcHost = null;
        public ConfigSvcHost()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
             if (coreAPIConfigSvcHost != null)
            {
                coreAPIConfigSvcHost.Close();
            }
            coreAPIConfigSvcHost = new ServiceHost(typeof(Config));
            coreAPIConfigSvcHost.Open();
        }

        protected override void OnStop()
        {
            if (coreAPIConfigSvcHost != null)
            {
                coreAPIConfigSvcHost.Close();
                coreAPIConfigSvcHost = null;
            }
        }
    }
}
