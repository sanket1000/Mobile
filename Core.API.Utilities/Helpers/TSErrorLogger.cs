using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Core.API.Entities;
using Core.API.EntityModel;
namespace Core.API.Utilities.Helpers
{
    public static class TSErrorLogger
    {
        static string _TimberScanConnString = null;
        internal static string TimberScanConnString
        {
            get
            {
                if (string.IsNullOrEmpty(_TimberScanConnString))
                {
                    _TimberScanConnString = Core.API.Utilities.Helpers.APIUtilityHelper.TimberScanEntityConnectionString;
                }
                return _TimberScanConnString;
            }
        }

        public static void LogError(string ScreenName, string FunctionName, string Exception, string InnerException, string ErrorMessage, string UserID)
        {
        
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                var tblErrorLog = new EntityModel.TSErrorLog();
                tblErrorLog.FunctionName = FunctionName;
                tblErrorLog.ScreenName = ScreenName;
                tblErrorLog.Exception = Exception;
                tblErrorLog.InnerException = InnerException;
                tblErrorLog.ErrMessage = ErrorMessage;
                tblErrorLog.CreatedDate = DateTime.Now;
                tblErrorLog.CreatedUserID = UserID;
                ctx.TSErrorLogs.AddObject(tblErrorLog);
                ctx.SaveChanges();
            }
        }
    }
}
