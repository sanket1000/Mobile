using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using Core.API.DocumentSvc;

namespace Core.API.DocumentSvcHost
{
    public partial class DocumentSvcHost : ServiceBase
    {
        internal static ServiceHost coreAPIDocumentSvcHost = null;
        public DocumentSvcHost()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (coreAPIDocumentSvcHost != null)
            {
                coreAPIDocumentSvcHost.Close();
            }

            coreAPIDocumentSvcHost = new ServiceHost(typeof(Document));
            coreAPIDocumentSvcHost.Open();
        }

        protected override void OnStop()
        {
            if (coreAPIDocumentSvcHost != null)
            {
                coreAPIDocumentSvcHost.Close();
                coreAPIDocumentSvcHost = null;
            }
        }
    }
}
