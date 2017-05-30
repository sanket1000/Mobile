using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.Entity;
using Core.API.EntityModel;

namespace Core.API.DataAccess
{
    public class ErrorLogDA : DataAccessBase
    {

        public void LogError(string ScreenName, string FunctionName, string Exception, string InnserException, string ErrMessage, string UserID)
        {
            using (var ctx = new CoreAPIEntities(ECB.ConnectionString))
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
    }
}
