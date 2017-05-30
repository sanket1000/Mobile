using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.API.DataAccess;

namespace Core.API.Utilities.Helpers
{
    public static class APIUtilDataAccessHelper
    {

        //public int ConnectionID = 1;
        public static bool TimberScanConnectionStringChanged = false;
        private static volatile DataAcessHelper _tScanData = null;
        public static DataAcessHelper TimberScanData
        {
            get
            {
                if (_tScanData == null || TimberScanConnectionStringChanged)
                {
                    _tScanData = new DataAcessHelper(APIUtilityHelper.TimberScanConnectionString, DataAccessType.SqlServer);
                }
                return _tScanData;
            }
        }

        //private static object objTLineStdDataSync = new object();
        //private static volatile DataAcessHelper _tLineStdData = null;
        //public static DataAcessHelper TimberlineStandardData
        //{
        //    get
        //    {
        //        if (_tLineStdData == null)
        //        {
        //            lock (objTLineStdDataSync)
        //            {
        //                if (_tLineStdData == null)
        //                {
        //                    _tLineStdData = new DataAcessHelper(APIUtilityHelper.TimberlineConnectionString, DataAccessType.Odbc, true);
        //                }
        //            }
        //        }
        //        return _tLineStdData;
        //    }
        //}

        //private static object objTLineCustDataSync = new object();
        //private static volatile DataAcessHelper _tLineCustData = null;
        //public static DataAcessHelper TimberlineCustomData
        //{
        //    get
        //    {
        //        if (_tLineCustData == null)
        //        {
        //            lock (objTLineCustDataSync)
        //            {
        //                if (_tLineCustData == null)
        //                {
        //                    _tLineCustData = new DataAcessHelper(APIUtilityHelper.TimberlineCustomConnectionString, DataAccessType.Odbc, true);
        //                }
        //            }
        //        }
        //        return _tLineCustData;
        //    }
        //}

        //private static object objTLineDictDataSync = new object();
        //private static volatile DataAcessHelper _tLineDictData = null;
        //public static DataAcessHelper TimberlineDictData
        //{
        //    get
        //    {
        //        if (_tLineDictData == null)
        //        {
        //            lock (objTLineDictDataSync)
        //            {
        //                if (_tLineDictData == null)
        //                {
        //                    _tLineDictData = new DataAcessHelper(APIUtilityHelper.TimberlineDictConnectionString, DataAccessType.Odbc, true);
        //                }
        //            }
        //        }
        //        return _tLineDictData;
        //    }
        //}

        private static volatile DataAcessHelper _tSyncData = null;
        public static DataAcessHelper TimberSyncData
        {
            get
            {
                if (_tSyncData == null)
                {
                    _tSyncData = new DataAcessHelper(APIUtilityHelper.TimberSyncConnectionString, DataAccessType.SqlServer);
                }
                return _tSyncData;
            }
        }
        private static volatile DataAcessHelper _tSyncDataDynamic = null;
        public static DataAcessHelper TimberSynchDataDynamic(int ConnectionID)
        {
            if (_tSyncDataDynamic == null)
           {
               string connString = APIUtilityHelper.TimberSyncConnectionString.Replace("TimberScan", "TimberSync");
               //connString = APIUtilityHelper.TimberSyncConnectionString.Replace("TimberSync", "TimberSync_" + ConnectionID.ToString());
               connString = connString.Replace("TimberSync", "TimberSync_" + ConnectionID.ToString());
               _tSyncDataDynamic = new DataAcessHelper(connString, DataAccessType.SqlServer);
           }
            return _tSyncDataDynamic;
            
        }
    }
}
