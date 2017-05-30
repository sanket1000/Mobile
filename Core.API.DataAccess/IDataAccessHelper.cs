using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.API.DataAccess
{
    public interface IDataAcessHelper
    {
        void CloseConnection();
        string ConnectionString { get; }
        int ConnectionTimeout { get; }
        object ExecuteInsert(string insertSql);
        void ExecuteStoredProcedure(string StoredProcedureName, System.Collections.ArrayList parameters);
        void FillDataTable(System.Data.DataTable dt, string sql, System.Collections.ArrayList parameters, bool AcceptChanges, bool FillSchema);
        void FillDataTableWithSP(System.Data.DataTable dt, string StoredProcedureName, System.Collections.ArrayList parameters, bool AcceptChanges, bool FillSchema);
        object GetCell(string sql);
        object InsertDataRow(System.Data.DataRow dr, string tableName, System.Collections.Generic.List<string> ColsToInsert);
        System.Collections.Generic.List<object> InsertDataTable(System.Data.DataTable dt, string tableName, System.Collections.Generic.List<string> ColsToInsert);
        bool KeepConnectionOpen { get; }
        object NonQuery(string sql);
        bool TestConnection();
        void UpdateDataTable(System.Data.DataTable dt, string tableName, System.Collections.Generic.List<string> UpdCols, System.Collections.Generic.List<string> keys);
    }
}

