using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;


namespace Core.API.ConfigSvcHost
{
    [RunInstaller(true)]
    public partial class ConfigSvcHostInstaller : System.Configuration.Install.Installer
    {
        public ConfigSvcHostInstaller()
        {
            InitializeComponent();
        }
    }
}
