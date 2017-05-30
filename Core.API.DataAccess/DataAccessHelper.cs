using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;

namespace Core.API.DataAccess
{
    public enum DataAccessType
    {
        SqlServer,
        Odbc,
        OleDb
    }

    public enum ConnectionType
    {
        TimberScan,
        TimberSync,
        TimberLine
    }
    public class DataAcessHelper : IDataAcessHelper
    {

        private System.Data.Common.DbDataAdapter _adptr;
        private System.Data.Common.DbCommand _cmd;
        private System.Data.Common.DbConnection _conn;
        private System.String _connStr;
        private DataAccessType accessType = DataAccessType.SqlServer;
        private bool _keepConnectionOpen = false;



        public DataAcessHelper(string connectionString, DataAccessType aType)
            : this(connectionString, aType, false)
        {



        }
        public DataAcessHelper(string connectionString, DataAccessType aType, bool keepConnectionOpen)
        {
            this._keepConnectionOpen = keepConnectionOpen;
            this.accessType = aType;
            this._connStr = connectionString;
            this.init();


        }


        public virtual string ConnectionString
        {
            get
            {
                return this._connStr;
            }
        }

        public int ConnectionTimeout
        {
            get
            {
                return this._conn.ConnectionTimeout;
            }
        }

        public bool KeepConnectionOpen
        {
            get
            {
                return this._keepConnectionOpen;
            }
        }


        private void init()
        {


            switch (accessType)
            {
                case DataAccessType.SqlServer:
                    this._conn = new SqlConnection(this._connStr);
                    this._cmd = new SqlCommand("", (SqlConnection)this._conn);
                    this._adptr = new SqlDataAdapter((SqlCommand)this._cmd);

                    break;
                case DataAccessType.Odbc:
                    this._conn = new OdbcConnection(this._connStr);
                    OdbcConnection con = (OdbcConnection)this._conn;
                    con.ConnectionTimeout = 300;
                    this._cmd = new OdbcCommand("", (OdbcConnection)this._conn);
                    this._adptr = new OdbcDataAdapter((OdbcCommand)this._cmd);
                    break;
                case DataAccessType.OleDb:
                    this._conn = new OleDbConnection(this._connStr);
                    this._cmd = new OleDbCommand("", (OleDbConnection)this._conn);
                    this._adptr = new OleDbDataAdapter((OleDbCommand)this._cmd);
                    break;
            }

        }

        private void FillSchema(System.Data.DataTable dt, System.Data.SchemaType st)
        {
            //this._cmd.Parameters.Clear();

            ((this._adptr)).FillSchema(dt, st);


        }

        private void FillDataTable(System.Data.DataTable dt)
        {

            ((this._adptr)).Fill(dt);




        }

        public void FillDataTableWithSP(System.Data.DataTable dt, string StoredProcedureName, System.Collections.ArrayList parameters, bool AcceptChanges, bool FillSchema)
        {
            lock (this._conn)
            {
                this._cmd.Parameters.Clear();
                try
                {

                    //System.Object[] typedParams = this.GetTypedParameters(parameters);
                    if (!this._keepConnectionOpen || (this._keepConnectionOpen && this._conn.State != ConnectionState.Open))
                        this._conn.Open();

                    if (parameters == null)
                    {

                    }
                    else
                    {
                        //typedParams = this.GetTypedParameters(parameters);
                        this._cmd.Parameters.Clear();
                        this._cmd.Parameters.AddRange(parameters.ToArray());
                    }


                    this._cmd.CommandText = StoredProcedureName;
                    this._cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    this._adptr.AcceptChangesDuringFill = AcceptChanges;

                    if (FillSchema)
                    {
                        this.FillSchema(dt, System.Data.SchemaType.Source);
                    }

                    this.FillDataTable(dt);


                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {

                    if (!this._keepConnectionOpen)
                        this._conn.Close();
                }
            }
        }


        public void FillDataTable(System.Data.DataTable dt, string sql, System.Collections.ArrayList parameters, bool AcceptChanges, bool FillSchema)
        {
            lock (this._conn)
            {
                this._cmd.Parameters.Clear();
                try
                {
                    if (!this._keepConnectionOpen || (this._keepConnectionOpen && this._conn.State != ConnectionState.Open))
                        this._conn.Open();

                    if (parameters == null)
                    {
                    }
                    else
                    {
                        this._cmd.Parameters.Clear();
                        this._cmd.Parameters.AddRange(parameters.ToArray());

                    }

                    this._cmd.CommandText = sql;
                    this._cmd.CommandType = System.Data.CommandType.Text;

                    this._adptr.AcceptChangesDuringFill = AcceptChanges;

                    this.FillDataTable(dt);
                    if (FillSchema)
                    {
                        this.FillSchema(dt, System.Data.SchemaType.Source);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (!this._keepConnectionOpen)
                        this._conn.Close();
                }
            }
        }

        public void ExecuteStoredProcedure(string StoredProcedureName, System.Collections.ArrayList parameters)
        {
            lock (this._conn)
            {
                this._cmd.Parameters.Clear();
                try
                {
                    System.Data.SqlClient.SqlCommand cmd;

                    this._cmd.CommandText = StoredProcedureName;
                    this._cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    if (parameters == null)
                    {

                    }
                    else
                    {
                        this._cmd.Parameters.Clear();
                        this._cmd.Parameters.AddRange(parameters.ToArray());
                    }

                    if (!this._keepConnectionOpen || (this._keepConnectionOpen && this._conn.State != ConnectionState.Open))
                        this._conn.Open();

                    this._cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    throw;

                }
                finally
                {
                    if (!this._keepConnectionOpen)
                        this._conn.Close();

                }
            }
        }

        public bool TestConnection()
        {
            lock (this._conn)
            {

                try
                {
                    if (!this._keepConnectionOpen || (this._keepConnectionOpen && this._conn.State != ConnectionState.Open))
                        this._conn.Open();
                    return true;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (!this._keepConnectionOpen)
                        this._conn.Close();

                }
            }
        }

        public object NonQuery(string sql)
        {
            lock (this._conn)
            {
                this._cmd.Parameters.Clear();
                object identity = null;

                try
                {
                    if (!this._keepConnectionOpen || (this._keepConnectionOpen && this._conn.State != ConnectionState.Open))
                        this._conn.Open();

                    this._cmd.CommandText = sql;
                    this._cmd.CommandType = System.Data.CommandType.Text;

                    identity = this._cmd.ExecuteNonQuery();

                    return identity;

                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (!this._keepConnectionOpen)
                        this._conn.Close();
                }
            }
        }

        /// <summary>
        /// Executes the insertSql and returns the identity value for the newly created row. Use with DataAccessType.SqlServer ONLY.
        /// </summary>
        /// <param name="insertSql"></param>
        /// <returns></returns>
        public object ExecuteInsert(string insertSql)
        {
            lock (this._conn)
            {

                this._cmd.Parameters.Clear();
                object identity = null;

                try
                {

                    if (this.accessType == DataAccessType.SqlServer)
                    {
                        if (!this._keepConnectionOpen || (this._keepConnectionOpen && this._conn.State != ConnectionState.Open))
                            this._conn.Open();

                        this._cmd.CommandText = insertSql + ";SELECT SCOPE_IDENTITY();";
                        this._cmd.CommandType = System.Data.CommandType.Text;

                        identity = this._cmd.ExecuteScalar();
                    }

                    return identity;

                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (!this._keepConnectionOpen)
                        this._conn.Close();
                }
            }
        }

        public object GetCell(string sql)
        {
            lock (this._conn)
            {
                this._cmd.Parameters.Clear();
                try
                {

                    object obj;
                    this._cmd.CommandType = CommandType.Text;
                    this._cmd.CommandText = sql;

                    if (!this._keepConnectionOpen || (this._keepConnectionOpen && this._conn.State != ConnectionState.Open))
                        this._conn.Open();

                    obj = this._cmd.ExecuteScalar();

                    return obj;

                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (!this._keepConnectionOpen)
                        this._conn.Close();
                }
            }

        }

        internal void FillDataSet(DataSet ds, string sql, System.Collections.ArrayList parameters, bool AcceptChanges, bool FillSchema)
        {
            lock (this._conn)
            {
                this._cmd.Parameters.Clear();
                try
                {

                    //System.Object[] typedParams = this.GetTypedParameters(parameters);

                    if (!this._keepConnectionOpen || (this._keepConnectionOpen && this._conn.State != ConnectionState.Open))
                        this._conn.Open();

                    if (parameters == null)
                    {

                    }
                    else
                    {
                        //typedParams = this.GetTypedParameters(parameters);
                        this._cmd.Parameters.Clear();
                        this._cmd.Parameters.AddRange(parameters.ToArray());
                    }


                    this._cmd.CommandText = sql;
                    this._cmd.CommandType = System.Data.CommandType.Text;

                    this._adptr.AcceptChangesDuringFill = AcceptChanges;

                    this.FillDataSet(ds);


                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {

                    if (!this._keepConnectionOpen)
                        this._conn.Close();
                }
            }

        }

        internal void FillDataSetWithSP(DataSet ds, string StoredProcedureName, System.Collections.ArrayList parameters, bool AcceptChanges, bool FillSchema)
        {
            lock (this._conn)
            {
                this._cmd.Parameters.Clear();
                try
                {

                    //System.Object[] typedParams = this.GetTypedParameters(parameters);
                    if (!this._keepConnectionOpen || (this._keepConnectionOpen && this._conn.State != ConnectionState.Open))
                        this._conn.Open();

                    if (parameters == null)
                    {

                    }
                    else
                    {
                        //typedParams = this.GetTypedParameters(parameters);
                        this._cmd.Parameters.Clear();
                        this._cmd.Parameters.AddRange(parameters.ToArray());
                    }


                    this._cmd.CommandText = StoredProcedureName;
                    this._cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    this._adptr.AcceptChangesDuringFill = AcceptChanges;

                    this.FillDataSet(ds);


                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (!this._keepConnectionOpen)
                        this._conn.Close();
                }
            }

        }

        private void FillDataSet(System.Data.DataSet ds)
        {


            ((this._adptr)).Fill(ds);


        }

        /// <summary>
        /// Creates and executes an insert statement for the given data row.
        /// </summary>
        /// <returns>If the DataAccessType is SqlServer InsertDataRow Returns the identity value for the newly created row.</returns>
        public object InsertDataRow(System.Data.DataRow dr, string tableName, List<string> ColsToInsert)
        {
            lock (this._conn)
            {
                DataTable idt;
                string sql;
                StringBuilder iCols;
                StringBuilder iVals;
                System.Data.Common.DbParameter param;
                List<object> identities = new List<object>();
                object identity = null;

                try
                {

                    if (ColsToInsert != null && dr != null)
                    {
                        if (!this._keepConnectionOpen || (this._keepConnectionOpen && this._conn.State != ConnectionState.Open))
                            this._conn.Open();

                        this._cmd.Parameters.Clear();
                        iCols = new StringBuilder();
                        iVals = new StringBuilder();

                        iCols.Append("(");
                        iVals.Append("(");

                        this.GetValuesAndColumns(dr, iCols, iVals, ColsToInsert);

                        if (iCols.Length > 0 && (iCols[iCols.Length - 1] == ','))
                        {

                            iCols[iCols.Length - 1] = ')';//iCols.Substring(0, iCols.LastIndexOf(",")) + ")";
                            iVals[iVals.Length - 1] = ')';
                        }

                        iCols.Insert(0, string.Format("Insert into {0} ", tableName));
                        iCols.Append(string.Format(" Values {0}", iVals.ToString()));

                        this._cmd.CommandText = iCols.ToString();

                        if (this.accessType == DataAccessType.SqlServer)
                        {
                            this._cmd.CommandText += ";SELECT SCOPE_IDENTITY();";
                        }

                        this._cmd.CommandType = CommandType.Text;

                        identity = this._cmd.ExecuteScalar();

                        identities.Add(identity);

                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (!this._keepConnectionOpen)
                        this._conn.Close();
                }

                return identity;
            }


        }

        /// <summary>
        /// Creates and executes an insert statement for each data row of the given datatable.
        /// </summary>
        /// <returns>If the DataAccessType is SqlServer InsertDataRow Returns a list of identity values for the newly created row.</returns>
        public List<object> InsertDataTable(System.Data.DataTable dt, string tableName, System.Collections.Generic.List<string> ColsToInsert)
        {
            lock (this._conn)
            {
                DataTable idt;
                string sql;
                StringBuilder iCols;
                StringBuilder iVals;
                System.Data.Common.DbParameter param;
                List<object> identities = new List<object>();
                object identity = null;

                try
                {


                    idt = dt;//dt.GetChanges(DataRowState.Added);

                    if (!(idt == null))
                    {
                        if (!this._keepConnectionOpen || (this._keepConnectionOpen && this._conn.State != ConnectionState.Open))
                            this._conn.Open();

                        foreach (DataRow idr in idt.Rows)
                        {
                            this._cmd.Parameters.Clear();
                            iCols = new StringBuilder();
                            iVals = new StringBuilder();

                            iCols.Append("(");
                            iVals.Append("(");

                            this.GetValuesAndColumns(idr, iCols, iVals, ColsToInsert);

                            if (iCols.Length > 0 && (iCols[iCols.Length - 1] == ','))
                            {

                                iCols[iCols.Length - 1] = ')';//iCols.Substring(0, iCols.LastIndexOf(",")) + ")";
                                iVals[iVals.Length - 1] = ')';
                            }

                            iCols.Insert(0, string.Format("Insert into {0} ", tableName));
                            iCols.Append(string.Format(" Values {0}", iVals.ToString()));

                            this._cmd.CommandText = iCols.ToString();

                            if (this.accessType == DataAccessType.SqlServer)
                            {
                                this._cmd.CommandText += ";SELECT SCOPE_IDENTITY();";
                            }

                            this._cmd.CommandType = CommandType.Text;

                            identity = this._cmd.ExecuteScalar();

                            identities.Add(identity);
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (!this._keepConnectionOpen)
                        this._conn.Close();
                }

                return identities;
            }
        }

        private void GetValuesAndColumns(DataRow idr, StringBuilder iCols, StringBuilder iVals, List<string> ColsToInsert)
        {
            DataTable idt = null;
            System.Data.Common.DbParameter param;


            idt = idr.Table;

            foreach (string col in ColsToInsert)
            {
                param = this.GetParameter(col, idr[col]);
                iCols.Append("[" + col + "],");
                iVals.Append(param.ParameterName + ",");
                this._cmd.Parameters.Add(param);
            }

        }

        private System.Data.Common.DbParameter GetUniqueParameter(string parameterName, object parameterValue)
        {

            System.Data.Common.DbParameter param;
            int i = 1;

            switch (this.accessType)
            {
                case DataAccessType.SqlServer:
                    param = new System.Data.SqlClient.SqlParameter("@" + parameterName, parameterValue);
                    break;
                case DataAccessType.OleDb:
                    param = new System.Data.OleDb.OleDbParameter("@" + parameterName, parameterValue);

                    break;
                case DataAccessType.Odbc:
                    param = new System.Data.Odbc.OdbcParameter("@" + parameterName, parameterValue);

                    break;
                default:
                    param = null;
                    break;

            }

            if (!(param == null))
            {
                while (this._cmd.Parameters.Contains(param.ParameterName))
                {
                    param.ParameterName = param.ParameterName + i.ToString();
                    ++i;
                }
            }

            return param;


        }

        /// <summary>
        /// Creates and executes an update query based on the provided parameters.
        /// </summary>
        /// <param name="dt">The DataTable containing the columns to update and the data to update the columns with</param>
        /// <param name="tableName">The table this method will try to update</param>
        /// <param name="UpdCols">The columns to update. These columns must exist in the dt(DataTable) paramater</param>
        /// <param name="keys">The keys/identifiers to add to the where clause so the correct row is updated.</param>
        public void UpdateDataTable(System.Data.DataTable dt, string tableName, System.Collections.Generic.List<string> UpdCols, System.Collections.Generic.List<string> keys)
        {
            lock (this._conn)
            {
                try
                {
                    if (dt == null)
                    {
                    }
                    else
                    {
                        DataTable udt;
                        string cols = "";
                        string updKeys = "";
                        string sql = "";
                        System.Data.Common.DbParameter param;

                        udt = dt;//dt.GetChanges(DataRowState.Modified);

                        if (!(udt == null))
                        {

                            if (UpdCols == null || UpdCols.Count <= 0)
                            {
                                UpdCols = (from updCol in dt.Columns.OfType<DataColumn>()
                                           select updCol.ColumnName).ToList<string>();
                            }

                            if (!this._keepConnectionOpen || (this._keepConnectionOpen && this._conn.State != ConnectionState.Open))
                                this._conn.Open();

                            foreach (DataRow udr in udt.Rows)
                            {
                                this._cmd.Parameters.Clear();
                                cols = "";
                                updKeys = "";

                                foreach (string col in UpdCols)
                                {
                                    param = this.GetParameter(col, udr[col]);
                                    cols += string.Format("[{0}]={1},", col, param.ParameterName); //col.ColumnName + "=" + param.ParameterName + ",";
                                    this._cmd.Parameters.Add(param);
                                }

                                if (keys != null)
                                {
                                    foreach (string colKey in keys)
                                    {

                                        param = this.GetUniqueParameter(colKey, udr[colKey]);
                                        updKeys += string.Format("[{0}]={1} and ", colKey, param.ParameterName);//col.ColumnName + "=" + param.ParameterName + " and ";
                                        this._cmd.Parameters.Add(param);

                                    }
                                }

                                if (cols.EndsWith(","))
                                {
                                    cols = cols.Substring(0, cols.LastIndexOf(","));
                                }

                                if (string.IsNullOrEmpty(updKeys))
                                {
                                    if ((dt.PrimaryKey == null) || (dt.PrimaryKey.Length == 0))
                                    {
                                        throw new System.Exception("Cannot complete update. There are no keys.");

                                    }
                                    else
                                    {
                                        foreach (DataColumn col in dt.PrimaryKey)
                                        {
                                            param = this.GetUniqueParameter(col.ColumnName, udr[col.ColumnName, DataRowVersion.Original]);
                                            updKeys += col.ColumnName + "=" + param.ParameterName + " and ";
                                            this._cmd.Parameters.Add(param);

                                        }
                                    }
                                }

                                if (updKeys.EndsWith(" and "))
                                {
                                    updKeys = updKeys.Substring(0, updKeys.LastIndexOf(" and "));
                                }

                                sql = "Update " + tableName + " Set " + cols + " Where " + updKeys;

                                this._cmd.CommandText = sql;
                                this._cmd.CommandType = CommandType.Text;

                                this._cmd.ExecuteNonQuery();

                                dt.AcceptChanges();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (!this._keepConnectionOpen)
                        this._conn.Close();
                }
            }
        }

        private System.Data.Common.DbParameter GetParameter(string parameterName, object parameterValue)
        {

            try
            {
                System.Data.Common.DbParameter param;


                switch (this.accessType)
                {
                    case DataAccessType.SqlServer:
                        param = new System.Data.SqlClient.SqlParameter("@" + parameterName, parameterValue);
                        break;
                    case DataAccessType.OleDb:
                        param = new System.Data.OleDb.OleDbParameter("@" + parameterName, parameterValue);
                        break;
                    case DataAccessType.Odbc:
                        param = new System.Data.Odbc.OdbcParameter("@" + parameterName, parameterValue);
                        break;
                    default:
                        param = null;
                        break;

                }

                return param;

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
            }

        }

        public void CloseConnection()
        {
            if (this._conn.State != ConnectionState.Closed)
            {
                try
                {
                    this._conn.Close();
                    this._conn.Dispose();
                }
                catch (Exception ex)
                {
                    //this method is only called when the app is closing to close open timberline connections, so we just swallow the exceptions
                }
            }
        }
    }
}
