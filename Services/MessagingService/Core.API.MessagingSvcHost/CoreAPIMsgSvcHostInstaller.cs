using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Windows.Forms;

namespace Core.API.MessagingSvcHost
{
    [RunInstaller(true)]
    public partial class CoreAPIMsgSvcHostInstaller : System.Configuration.Install.Installer
    {
        public CoreAPIMsgSvcHostInstaller()
        {
            InitializeComponent();

            this.serviceInstaller1.AfterInstall += new InstallEventHandler(serviceInstaller1_AfterInstall);
            this.serviceInstaller1.BeforeUninstall += new InstallEventHandler(serviceInstaller1_BeforeUninstall);
        }

        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new CoreAPIMsgHostSvc() 
            };
            ServiceBase.Run(ServicesToRun);
        }

        void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {
            //MessageBox.Show("Begin Service Started new ");
            //System.Threading.Thread.Sleep(10000);
            // Auto Start the Service Once Installation is Finished.
            try
            {
                using (var controller = new System.ServiceProcess.ServiceController(serviceInstaller1.ServiceName))
                {

                    controller.Start();
                    controller.WaitForStatus(ServiceControllerStatus.Running);
                    //MessageBox.Show("End Service Started new");
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("serviceInstaller1_AfterInstall Exception");
            }
        }

        void serviceInstaller1_BeforeUninstall(object sender, InstallEventArgs e)
        {
            try
            {
                using (var controller = new System.ServiceProcess.ServiceController(serviceInstaller1.ServiceName))
                {

                    if (controller.Status == ServiceControllerStatus.Running || controller.Status == ServiceControllerStatus.Paused)
                    {
                        controller.Stop();
                        controller.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 0, 30));
                        controller.Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
