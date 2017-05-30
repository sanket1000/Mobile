using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.EntityClient;
using Core.API.DataAccess;

namespace Core.API.DataAccess
{
    public class DataAccessBase
    {
        public static EntityConnectionStringBuilder ECB { get; set; }

        public static string DBConnectionString { get; set; }

        public static string TLineConnString { get; set; }

        public static ConnectionType DBConnectionType { get; set; }

        private static Core.API.DataAccess.DataAcessHelper _dataAccess = null;

        private static object objTSyncDataSync = new object();
        private static volatile Core.API.DataAccess.DataAcessHelper _tSyncData = null;
        private static Core.API.DataAccess.DataAcessHelper TimberSyncData
        {
            get
            {
                if (_tSyncData == null)
                {
                    lock (objTSyncDataSync)
                    {
                        if (_tSyncData == null)
                        {
                            _tSyncData = new Core.API.DataAccess.DataAcessHelper(DBConnectionString, Core.API.DataAccess.DataAccessType.SqlServer, true);
                        }
                    }
                }
                return _tSyncData;
            }
        }


        private static object objTLineStdDataSync = new object();
        private static volatile Core.API.DataAccess.DataAcessHelper _tLineStdData = null;
        public static Core.API.DataAccess.DataAcessHelper TimberlineStandardData
        {
            get
            {
                if (_tLineStdData == null)
                {
                    lock (objTLineStdDataSync)
                    {
                        if (_tLineStdData == null)
                        {
                            _tLineStdData = new Core.API.DataAccess.DataAcessHelper(TLineConnString, Core.API.DataAccess.DataAccessType.Odbc, true);
                        }
                    }
                }
                return _tLineStdData;
            }
        }

        private static object objTLineDictData = new object();
        //private static CoreAssociates.API.DataAccess.DataAcessHelper _tLineDictData = null;
        //public static CoreAssociates.API.DataAccess.DataAcessHelper TimberlineDictionaryData
        //{
        //    get
        //    {
        //        if (_tLineDictData == null)
        //        {
        //            lock (objTLineDictData)
        //            {
        //                if (_tLineDictData == null)
        //                {
        //                    _tLineDictData = new CoreAssociates.API.DataAccess.DataAcessHelper(TimberlineDictionaryDataConnectionString, CoreAssociates.API.DataAccess.DataAccessType.Odbc, true);
        //                }
        //            }
        //        }
        //        return _tLineDictData;
        //    }
        //}
        public static Core.API.DataAccess.DataAcessHelper DataAccess
        {
            get
            {
                switch (DBConnectionType)
                {
                    case ConnectionType.TimberLine:
                        _dataAccess = TimberlineStandardData;
                        break;
                    case ConnectionType.TimberSync:
                        _dataAccess = TimberSyncData;
                        break;
                    default:
                        _dataAccess = TimberlineStandardData;
                        break;
                }

                return _dataAccess;
            }

        }
    }
}
