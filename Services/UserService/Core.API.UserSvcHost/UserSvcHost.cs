using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using Core.API.UserSvc;

namespace Core.API.UserSvcHost
{
    public partial class UserSvcHost : ServiceBase
    {
        internal static ServiceHost coreAPIUserSvcHost = null;
        public UserSvcHost()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (coreAPIUserSvcHost != null)
            {
                coreAPIUserSvcHost.Close();
            }

            coreAPIUserSvcHost = new ServiceHost(typeof(Users));
            coreAPIUserSvcHost.Open();
        }

        protected override void OnStop()
        {
            if (coreAPIUserSvcHost != null)
            {
                coreAPIUserSvcHost.Close();
                coreAPIUserSvcHost = null;
            }
        }
    }
}
