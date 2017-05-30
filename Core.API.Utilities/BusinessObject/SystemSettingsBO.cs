using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.API.Utilities.Model;
using Core.API.Utilities.Helpers;

namespace Core.API.Utilities.BusinessObject
{
    public class SystemSettingsBO
    {
        public static SystemSettings GetSystemSettings()
        {
            SystemSettings ss = new SystemSettings();
           // ss.TimberlineConnectionString = APIUtilityHelper.TimberlineConnectionString;
            ss.TimberScanConnectionString = APIUtilityHelper.TimberScanConnectionString;
            ss.TimberSyncConnectionString = APIUtilityHelper.TimberSyncConnectionString;

            //TimberScanSettings ts = APIUtilityHelper.GetTimberScanSettings();
            //if (ts != null)
            //{
            //}
            //System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
            return ss;
        }
    }
}
