using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.API.EntityModel;
using Core.API.Utilities;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Data.Objects;
using Core.API.Utilities;
using Core.API.Utilities.Helpers;
using System.Data;

namespace Core.API.ConfigSvc
{
    internal class ConfigDA
    {
        static string _TimberScanConnString = null;

       public ConfigDA()
       {
           
       }
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

        public static void LogError(string ScreenName, string FunctionName, string Exception, string InnserException, string ErrMessage, string UserID)
        {
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                var tblErrorLog = new EntityModel.TSErrorLog();
                tblErrorLog.FunctionName = FunctionName;
                tblErrorLog.ScreenName = ScreenName;
                tblErrorLog.Exception = Exception;
                tblErrorLog.InnerException = InnserException;
                tblErrorLog.ErrMessage = ErrMessage;
                tblErrorLog.CreatedDate = DateTime.Now;
                tblErrorLog.CreatedUserID = UserID;
                ctx.TSErrorLogs.AddObject(tblErrorLog);
                ctx.SaveChanges();

            }

        }

        public static void LogAPIMessage(int DocumentID, string MessageType, string Message, string ScreenName, string Function, Int32 UserID)
        {
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                var apiMessageLog = new EntityModel.APIMessageLog();
                apiMessageLog.DocumentID = DocumentID;
                apiMessageLog.MessageType = MessageType;
                apiMessageLog.Message = Message;
                apiMessageLog.UserID = UserID;
                apiMessageLog.CreatedDate = DateTime.Now;
                apiMessageLog.Application = "API";
                apiMessageLog.ScreenName = ScreenName;
                apiMessageLog.Function = Function;
                ctx.APIMessageLogs.AddObject(apiMessageLog);
                ctx.SaveChanges();

            }
        }
    }
}
