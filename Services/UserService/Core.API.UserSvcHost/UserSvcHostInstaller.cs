using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;


namespace Core.API.UserSvcHost
{
    [RunInstaller(true)]
    public partial class UserSvcHostInstaller : System.Configuration.Install.Installer
    {
        public UserSvcHostInstaller()
        {
            InitializeComponent();
        }
    }
}
