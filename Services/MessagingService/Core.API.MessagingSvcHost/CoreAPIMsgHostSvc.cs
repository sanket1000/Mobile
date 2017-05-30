using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using Core.API.MessagingSvc;

namespace Core.API.MessagingSvcHost
{
    public partial class CoreAPIMsgHostSvc : ServiceBase
    {
        internal static ServiceHost coreAPIMsgSvcHost = null;
        public CoreAPIMsgHostSvc()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (coreAPIMsgSvcHost != null)
            {
                coreAPIMsgSvcHost.Close();
            }
            coreAPIMsgSvcHost = new ServiceHost(typeof(CoreAPIMsgHandler));
            coreAPIMsgSvcHost.Open();
        }
        protected override void OnShutdown()
        {
            if (coreAPIMsgSvcHost != null)
            {
                coreAPIMsgSvcHost.Close();
                coreAPIMsgSvcHost = null;
            }
        }
        protected override void OnStop()
        {
            if (coreAPIMsgSvcHost != null)
            {
                coreAPIMsgSvcHost.Close();
                coreAPIMsgSvcHost = null;
            }
        }
    }
}
