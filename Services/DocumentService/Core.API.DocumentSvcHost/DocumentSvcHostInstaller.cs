using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Windows.Forms;


namespace Core.API.DocumentSvcHost
{
    [RunInstaller(true)]
    public partial class DocumentSvcHostInstaller : System.Configuration.Install.Installer
    {
        public DocumentSvcHostInstaller()
        {
            InitializeComponent();
            this.serviceInstaller1.AfterInstall += new InstallEventHandler(serviceInstaller1_AfterInstall);
            this.serviceInstaller1.BeforeUninstall += new InstallEventHandler(serviceInstaller1_BeforeUninstall);
        }

        //[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e)
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

        //[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public void serviceInstaller1_BeforeUninstall(object sender, InstallEventArgs e)
        {
             try
            {
                //MessageBox.Show("Enter serviceInstaller1_BeforeUninstall");
                //System.Threading.Thread.Sleep(10000);
                using (var controller = new System.ServiceProcess.ServiceController(serviceInstaller1.ServiceName))
                {
                    //MessageBox.Show(controller.Status.ToString());
                    if (controller.Status == ServiceControllerStatus.Running || controller.Status == ServiceControllerStatus.Paused)
                    {
                        //MessageBox.Show("Stopping Service");
                        controller.Stop();
                        controller.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 0, 30));
                        //MessageBox.Show("Closing Service");
                        controller.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
    }
}
