using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Data.EntityClient;
using System.Data.SqlClient;

namespace Core.API.Utilities.Helpers
{
    public class APIUtilityHelper
    {
        const string XFSFileName = "TimberScan.xfs";
        const string LFSFileName = "TimberScan.lfs";
        const string DecryptPassword = "Pelka1968";

        static string _TimberScanEntityConnectionString = null;
        public static string TimberScanEntityConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_TimberScanEntityConnectionString))
                    LoadTimberScanSettings();

                return _TimberScanEntityConnectionString;
            }
        }

        static string _TimberScanConnectionString = "";
        public static string TimberScanConnectionString
        {
            get 
            {
                if (string.IsNullOrEmpty(_TimberScanConnectionString))
                    LoadTimberScanSettings();

                return _TimberScanConnectionString; 
            }
        }

        //static string _TimberlineConnectionString = "";
        //public static string TimberlineConnectionString
        //{
        //    get {

        //        if (string.IsNullOrEmpty(_TimberlineConnectionString))
        //            LoadTimberlineSettings();

        //        return _TimberlineConnectionString; 
        //    }
        //}

        //static string _TimberlineCustomConnectionString = "";
        //internal static string TimberlineCustomConnectionString
        //{
        //    get
        //    {

        //        if (string.IsNullOrEmpty(_TimberlineCustomConnectionString))
        //            LoadTimberlineSettings();

        //        return _TimberlineCustomConnectionString;
        //    }
        //}

        //static string _TimberlineDictConnectionString = "";
        //internal static string TimberlineDictConnectionString
        //{
        //    get
        //    {

        //        if (string.IsNullOrEmpty(_TimberlineDictConnectionString))
        //            LoadTimberlineSettings();

        //        return _TimberlineDictConnectionString;
        //    }
        //}

        static string _TimberSyncConnectionString = "";
        public static string TimberSyncConnectionString
        {
            get 
            {
                if (string.IsNullOrEmpty(_TimberSyncConnectionString))
                    LoadTimberScanSettings();

                return _TimberSyncConnectionString; 
            }
        }

        static string _TimberSyncEntityConnectionString = "";
        public static string TimberSyncEntityConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_TimberSyncEntityConnectionString))
                    LoadTimberScanSettings();

                return _TimberSyncEntityConnectionString;
            }
        }


        internal static string AppPathAPI()
        {
            string applicationDirectory = string.Empty;

            {

                //we are not click once deployed then everything should be in the folder local to the executable
                applicationDirectory = System.Environment.GetEnvironmentVariable("ExePath");


                if (!string.IsNullOrEmpty(applicationDirectory) && applicationDirectory.EndsWith(":"))
                    applicationDirectory += @"\";

                if (string.IsNullOrEmpty(applicationDirectory))
                {
                    applicationDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                }


            }
            return applicationDirectory;
        }

        private static void LoadTimberScanSettings()
        {
            string XmlSS = GetSystemSettingsXML();
            TimberScanSettings ts = new TimberScanSettings();
            XmlSerializer xs = new XmlSerializer(typeof(TimberScanSettings));
            using (StringReader ms = new StringReader(XmlSS))
            {
                ts = (TimberScanSettings)xs.Deserialize(ms);

                if (ts != null)
                {
                    _TimberScanConnectionString = GetSQLServerConnectionString(ts.SQLAuthentication, ts.APInvoicesServer, ts.APDatabase, ts.APInvoiceUserID, ts.APInvoicePassword);

                    _TimberSyncConnectionString = GetSQLServerConnectionString(ts.SQLAuthentication, ts.TSyncServer, ts.TSyncDatabase, ts.TSyncUserID, ts.TSyncPassword);

                    _TimberScanEntityConnectionString = GetTimberScanEntityConnectionString(_TimberScanConnectionString);

                    _TimberSyncEntityConnectionString = GetTimberScanEntityConnectionString(_TimberSyncConnectionString);
                }
            }

         
        }

        //public static void LoadTimberlineSettings()
        //{
        //    string XmlSS = GetSystemSettingsXML();
        //    TimberScanSettings ts = new TimberScanSettings();
        //    XmlSerializer xs = new XmlSerializer(typeof(TimberScanSettings));
        //    using (StringReader ms = new StringReader(XmlSS))
        //    {
        //        ts = (TimberScanSettings)xs.Deserialize(ms);
        //        if (ts != null)
        //        {
        //            _TimberlineConnectionString = GetTimberlineConnectionStrings("Standard", ts.TLUserID, ts.TLPassword, modSystemProcedures.clsTimberlineConnection.Path);

        //            _TimberlineCustomConnectionString = GetTimberlineConnectionStrings("Custom", ts.TLUserID, ts.TLPassword, modSystemProcedures.clsTimberlineConnection.Path);

        //            _TimberlineDictConnectionString = GetTimberlineConnectionStrings("Dictionary", ts.TLUserID, ts.TLPassword, modSystemProcedures.clsTimberlineConnection.Path);
        //        }
        //    }

        //}

        private static string GetSystemSettingsXML()
        {
            return GetEncryptedSystemSettingsXMLDecrypted();
        }
       
        private static string GetEncryptedSystemSettingsXMLDecrypted() // done 
        {
            string sEncryptedText = null;
            string sDecryptedText = null;
            string filename = AppPathAPI() + "\\" + XFSFileName;
            //clsCryption encryptor = new clsCryption();
            Encryption encryptor = new Encryption();

            try
            {
                if (File.Exists(filename))
                {
                    sEncryptedText = File.ReadAllText(filename, Encoding.Default);
                    sEncryptedText = sEncryptedText.ToString().Replace("\r\n", "");
                    //sDecryptedText = encryptor.DecryptString(sEncryptedText, string.Empty);
                    sDecryptedText = encryptor.Decrypt(sEncryptedText, DecryptPassword);

                    return sDecryptedText;
                }
                else
                {
                   
                    //throw new Exception(filename.ToString() +
                    //    " does not exist! \r\n \r\n If you continue and it will not be created");
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static string GetTimberScanEntityConnectionString(string connectionString)
        {
            List<string> strLst = connectionString.Split(';').ToList<string>();
            string datasource = strLst[0].Split('=')[1];
            string database = strLst[1].Split('=')[1];
            string userId = strLst[3].Split('=')[1];
            string password = strLst[4].Split('=')[1];

            var connString = string.Format("metadata=res://*/CoreAPI.csdl|res://*/CoreAPI.ssdl|res://*/CoreAPI.msl;"
            + "provider=System.Data.SqlClient; provider connection string='Data Source={0};"
            + "Initial Catalog={1};User ID={2}; Password={3};MultipleActiveResultSets=True'",
            datasource, database, userId, password);

            //SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder(connectionString);
            //EntityConnectionStringBuilder ecb = new EntityConnectionStringBuilder();
            //ecb.Metadata = "res://*/EntityModel.TimberScanPO.csdl|res://*/EntityModel.TimberScanPO.ssdl|res://*/EntityModel.TimberScanPO.msl";
            //ecb.Provider = "System.Data.SqlClient";
            //ecb.ProviderConnectionString = scsb.ConnectionString;

            //return ecb;
            return connString;
        }

        private static string GetTimberSyncEntityConnectionString(string connectionString)
        {
            List<string> strLst = connectionString.Split(';').ToList<string>();
            string datasource = strLst[0].Split('=')[1];
            string database = strLst[1].Split('=')[1];
            string userId = strLst[3].Split('=')[1];
            string password = strLst[4].Split('=')[1];

            var connString = string.Format("metadata=res://*/CoreAPI.csdl|res://*/CoreAPI.ssdl|res://*/CoreAPI.msl;"
            + "provider=System.Data.SqlClient; provider connection string='Data Source={0};"
            + "Initial Catalog={1};User ID={2}; Password={3};MultipleActiveResultSets=True'",
            datasource, database, userId, password);
           
            //return ecb;
            return connString;
        }

        private static string GetSQLServerConnectionString(string SQLAuth, string Server, string Database, string UserID, string Password)
        {
            string connectionString = string.Empty;

            if (SQLAuth == "Windows") // "Windows" or "SQL Server"
            {

                connectionString =
                    string.Format("Data Source={0}; Initial Catalog={1}; Integrated Security=True;",
                    new object[] 
                    { 
                        Server, 
                        Database 
                    });
            }
            else
            {
                connectionString =
                    string.Format("Data Source={0}; Initial Catalog={1}; Integrated Security=False; User Id={2}; Password={3};",
                    new object[] 
                    { 
                        Server, 
                        Database,
                        UserID,
                        Password
                    });
            }


            return connectionString;

        }

        private static string GetTimberlineConnectionStrings(string conType, string sUserID, string sPassword, string dataFolderPath) //OK
        {

            /*standard
             * 
             * "DRIVER=Timberline Data;
             * UID=TimberApp;
             * PWD=TimberApp;
             * DBQ=\\gds_server\TSApps\Construction Sample Data\;
             * CODEPAGE=1252;
             * MaxColSupport=1536;
             * StandardMode=1;
             * MaxColSupport=1536;
             * ShortenNames=0;"
             *
             * */

            /*Dictionary
             * 
             * "DRIVER=Timberline Data;
             * UID=TimberApp;
             * PWD=TimberApp;
             * DBQ=\\gds_server\TSApps\Construction Sample Data\;
             * CODEPAGE=1252;
             * MaxColSupport=1536;
             * DictionaryMode=1;
             * MaxColSupport=1536;"
             * 
             * */


            string sTLConnectionString = string.Empty;

            switch (conType)
            {
                case "Standard": // standard
                    {
                        sTLConnectionString =
                        "Driver=Timberline Data;" +
                        "uid=" +
                        sUserID.ToString() + ";" +
                        "pwd=" +
                        sPassword.ToString() + ";" +
                        "dbq=" +
                        dataFolderPath + ";" +//.Substring(0, dataFolderPath.Length - 1) + ";" +
                        "codepage=1252;" +
                        "maxcolsupport=1536;" +
                        "standardmode=1;" +
                        "shortennames=0;";
                        break;
                    }
                case "Dictionary": // dictionary
                    {
                        sTLConnectionString =
                        "Driver=Timberline Data;" +
                        "uid=" +
                        sUserID.ToString() + ";" +
                        "pwd=" +
                        sPassword.ToString() + ";" +
                        "dbq=" +
                        dataFolderPath + ";" +//.Substring(0, dataFolderPath.Length - 1) + ";" +
                        "codepage=1252;" +
                        "maxcolsupport=1536;" +
                        "dictionarymode=1;";
                        break;
                    }
                case "Custom": // custom
                    {

                        /*Custom
           * 
           * 
           * DRIVER=Timberline Data;
           * UID=TimberApp;
           * PWD=TimberApp;
           * DBQ=\\gds_server\TSApps\Construction Sample Data\;
           * CODEPAGE=1252;
           * MaxColSupport=255;
           * StandardMode=0;
           * MaxColSupport=255;
           * ShortenNames=0;
           * 
           * */

                        sTLConnectionString =
                        "Driver=Timberline Data;" +
                        "uid=" +
                        sUserID.ToString() + ";" +
                        "pwd=" +
                        sPassword.ToString() + ";" +
                        "dbq=" +
                        dataFolderPath + ";" +//.Substring(0, dataFolderPath.Length - 1) + ";" +
                        "maxcolsupport=255;" +
                        "standardmode=0;" +
                        "shortennames=0;";
                        break;
                    }
                default:
                    {
                        sTLConnectionString =
                         "Driver=Timberline Data;" +
                         "uid=" +
                         sUserID.ToString() + ";" +
                         "pwd=" +
                         sPassword.ToString() + ";" +
                         "dbq=" +
                         dataFolderPath + ";" +//.Substring(0, dataFolderPath.Length - 1) + ";" +
                         "codepage=1252;" +
                         "maxcolsupport=1536;" +
                         "standardmode=1;" +
                         "shortennames=0;";
                        break;
                    }

            }
            return (sTLConnectionString);
        } 
    }
}
