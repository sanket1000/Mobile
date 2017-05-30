using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.API.Utilities.Model
{
    public class SystemSettings     
    {
        /// <summary>
        /// Timberline Settings
        /// </summary>
        public string TLUserID { get; set; }  //QATEST

        public string TLPassword { get; set; } //Core1234

        public string TimberlineConnectionString { get; set; } 

        /// <summary>
        /// AP Settings
        /// </summary>
        public string APDatabase { get; set; } //TimberScan

        public string APInvoiceUserID { get; set; } //lgnTScan

        public string APInvoicePassword { get; set; } //20GDS06

        public string APInvoicesDriver { get; set; }  //{SQL Server}

        public string APInvoicesServer { get; set; }  //TESTSERVER12\DEV1_ON_S1

        public string TScanServer64 { get; set; }  //TESTSERVER12\DEV1_ON_S1

        public string TimberScanConnectionString { get; set; }  
        
        /// <summary>
        /// TSync Settings
        /// </summary>
        public string TSyncUserID { get; set; } //lgnTScan

        public string TSyncPassword { get; set; } //20GDS06

        public string TSyncDatabase { get; set; } //TimberSync

        public string TSyncServer64 { get; set; }  //TESTSERVER12\DEV1_ON_S1

        public string TSyncDriver { get; set; }  //SQLNCLI10

        public string TScanSyncDriver64 { get; set; } //SQLNCLI10

        public string TimberSyncConnectionString { get; set; } //SQLNCLI10

        /// <summary>
        /// Misc Settings
        /// </summary>
        public string SQLAuthentication { get; set; }  //SQL Server

        public string MasterEMailAddress { get; set; } //carol.cook.612@gmail.com

        public string GroupPrefix { get; set; }  //PrefixA
       
        public string SourceDirectory { get; set; }  //\\TESTSERVER12\DEVELOPMENT DATA\DEV1_ON_S1\TEMPIMAGES

        public string InvoiceFolder { get; set; } //\\TESTSERVER12\DEVELOPMENT DATA\DEV1_ON_S1\IMAGES

        public string UploadDirectory { get; set; }  //\\TESTSERVER12\DEVELOPMENT DATA\DEV1_ON_S1\IMAGES
       
    }
}
