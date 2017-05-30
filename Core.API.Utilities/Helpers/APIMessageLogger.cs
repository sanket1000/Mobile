using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.API.EntityModel;

namespace Core.API.Utilities.Helpers
{
    public static class APIMessageLogger
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

        public static void LogAPIMessage(int DocumentID, string MessageType, string Message, string ScreenName, string Function, int UserID)
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
