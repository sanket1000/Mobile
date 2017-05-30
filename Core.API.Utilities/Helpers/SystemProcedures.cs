using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Core.API.Utilities.Helpers
{
    sealed class modSystemProcedures // TSTimberlineSettingsHelper
    {
        internal static colConnections colTimberlineConnections;

        private static clsConnection _clsTimberlineConnection;
        internal static clsConnection clsTimberlineConnection
        {

            get
            {
                if (_clsTimberlineConnection == null)
                    SetConnectionsCollection();
                return _clsTimberlineConnection;
            }
            set
            {
                bool changed = (value != _clsTimberlineConnection);


                _clsTimberlineConnection = value;


                if (changed)
                {
                    //when the current connection has changed we need to reset some of the singleton classes that are dependent on the current timberline connection
                    //modImage.ReinstantiateTLineConnectionDependentStaticClasses();
                }
            }
        }

        private static clsConnection _clsCurrentTimberlineConnection;
        internal static clsConnection clsCurrentTimberlineConnection
        {

            get
            {
                if (_clsCurrentTimberlineConnection == null)
                    SetConnectionsCollection();

                return _clsCurrentTimberlineConnection;
            }
            set
            {
                _clsCurrentTimberlineConnection = value;
            }
        }

        private static string GetQuery()
        {
            DataTable dt = new DataTable();
            string configValue = "";
            bool tblExits = true;
            try
            {
                string configQuery = @"select a.UserID, a.ConfigurationID, a.ConfigurationValue from tblUsersConfiguration a 
                                        inner join tblConfiguration b on a.ConfigurationID = b.ConfigurationID
                                    where b.ConfigurationName = 'CompanyDisplayOrder' and a.UserID = 'Sauron' ";


                APIUtilDataAccessHelper.TimberScanData.FillDataTable(dt, configQuery, null, true, false);
                if (dt != null && dt.Rows.Count > 0)
                {
                    configValue = dt.Rows[0]["ConfigurationValue"].ToString();
                }
            }
            catch (Exception ex)
            {
                tblExits = false;
            }

            if (string.IsNullOrEmpty(configValue))
                configValue = "";
            string sqry = "";
            if (configValue.Equals("Alphabetical by Alias"))
            {

                sqry = @"SELECT [ConnectionID]
                          , CASE WHEN ISNULL(ltrim(rtrim(Alias)), '') = '' then [Description]
                            ELSE ltrim(rtrim(Alias)) end as Description
                          ,[Path]
                          ,[SystemType]
                          ,[DefaultConnection]
                          ,[PrefixADesc]
                          ,[PrefixABDesc]
                          ,[PrefixABCDesc]
                          ,[JobSec1Size]
                          ,[JobPunct1]
                          ,[JobSec2Size]
                          ,[JobPunct2]
                          ,[JobSec3Size]
                          ,[CostCodeSec1Size]
                          ,[CostCodePunct1]
                          ,[CostCodeSec2Size]
                          ,[CostCodePunct2]
                          ,[CostCodeSec3Size]
                          ,[CostCodePunct3]
                          ,[CostCodeSec4Size]
                          ,[LastApproveImportedDate]
                          ,[LastApproveRMDate]
                          ,[LastApproveRecurringDate]
                          ,[LastApproveRegularDate]
                          , CASE WHEN ISNULL(ltrim(rtrim(Alias)), '') = '' then [Description]
                            ELSE ltrim(rtrim(Alias)) end as Alias
                          ,[DisplayOrder]
                          ,[IsActive]
                           FROM [tblConnections] WITH(NOLOCK) WHERE IsActive = 1 AND DefaultConnection = 1";
            }
            else
            {
                if (tblExits)
                {
                    sqry = @"SELECT [ConnectionID]
                          ,[Description]
                          ,[Path]
                          ,[SystemType]
                          ,[DefaultConnection]
                          ,[PrefixADesc]
                          ,[PrefixABDesc]
                          ,[PrefixABCDesc]
                          ,[JobSec1Size]
                          ,[JobPunct1]
                          ,[JobSec2Size]
                          ,[JobPunct2]
                          ,[JobSec3Size]
                          ,[CostCodeSec1Size]
                          ,[CostCodePunct1]
                          ,[CostCodeSec2Size]
                          ,[CostCodePunct2]
                          ,[CostCodeSec3Size]
                          ,[CostCodePunct3]
                          ,[CostCodeSec4Size]
                          ,[LastApproveImportedDate]
                          ,[LastApproveRMDate]
                          ,[LastApproveRecurringDate]
                          ,[LastApproveRegularDate]
                          , CASE WHEN ISNULL(ltrim(rtrim(Alias)), '') = '' then [Description]
                            ELSE ltrim(rtrim(Alias)) end as Alias
                          ,[DisplayOrder]
                          ,[IsActive]
                           FROM [tblConnections] WITH(NOLOCK) WHERE IsActive = 1 AND DefaultConnection = 1";
                }
                else
                {
                    sqry = @"SELECT [ConnectionID]
                          ,[Description]
                          ,[Path]
                          ,[SystemType]
                          ,[DefaultConnection]
                          ,[PrefixADesc]
                          ,[PrefixABDesc]
                          ,[PrefixABCDesc]
                          ,[JobSec1Size]
                          ,[JobPunct1]
                          ,[JobSec2Size]
                          ,[JobPunct2]
                          ,[JobSec3Size]
                          ,[CostCodeSec1Size]
                          ,[CostCodePunct1]
                          ,[CostCodeSec2Size]
                          ,[CostCodePunct2]
                          ,[CostCodeSec3Size]
                          ,[CostCodePunct3]
                          ,[CostCodeSec4Size]
                          ,[LastApproveImportedDate]
                          ,[LastApproveRMDate]
                          ,[LastApproveRecurringDate]
                          ,[LastApproveRegularDate]
                           FROM [tblConnections] WITH(NOLOCK) WHERE IsActive = 1 AND DefaultConnection = 1 ";
                }
            }


            if (tblExits)
            {
                if (configValue.Equals("Custom Display Order"))
                {
                    sqry = sqry + " order by DisplayOrder";
                }
                else if (configValue.Equals("Alphabetical by Display Name"))
                {
                    sqry = sqry + " order by Description";
                }
                else if (configValue.Equals("Alphabetical by Alias"))
                {
                    sqry = sqry + " order by Alias";
                }
            }
            return sqry;
        }

        private static void SetConnectionsCollection()
        {
            string sPath;
            colTimberlineConnections = null;
            colTimberlineConnections = new colConnections();
            clsConnection conn = null;

            DataTable tblConnections = new DataTable();

            APIUtilDataAccessHelper.TimberScanData.FillDataTable(tblConnections, GetQuery(), null, true, false);

            if (tblConnections != null && tblConnections.Rows.Count > 0)
            {
                foreach (DataRow connection in tblConnections.Rows)
                {
                    sPath = connection["Path"].ToString();

                    if (!string.IsNullOrWhiteSpace(sPath))
                    {
                        if (!sPath.EndsWith("\\"))
                        {
                            sPath = sPath + "\\";
                        }

                        if (TestForValidTimberlinePath(sPath) == true) //2.1.3 FSG 5/31/08
                        {

                            conn = colTimberlineConnections.Add(
                                sPath == null ? " " : sPath,
                                connection["SystemType"].ToString(),
                                connection["Description"].ToString(),
                                Convert.ToInt32(connection["ConnectionID"]),
                                connection["DefaultConnection"] == DBNull.Value ? false : System.Convert.ToBoolean(connection["DefaultConnection"]),
                                connection["PrefixADesc"].ToString(),
                                connection["PrefixABDesc"].ToString(),
                                connection["PrefixABCDesc"].ToString(),
                                System.Convert.ToInt16(connection["JobSec1Size"] == DBNull.Value ? 0 : connection["JobSec1Size"]),
                                System.Convert.ToInt16(connection["JobSec2Size"] == DBNull.Value ? 0 : connection["JobSec2Size"]),
                                System.Convert.ToInt16(connection["JobSec3Size"] == DBNull.Value ? 0 : connection["JobSec3Size"]),
                                connection["JobPunct1"].ToString(),
                                connection["JobPunct2"].ToString(),
                                System.Convert.ToInt16(connection["CostCodeSec1Size"] == DBNull.Value ? 0 : connection["CostCodeSec1Size"]),
                                System.Convert.ToInt16(connection["CostCodeSec2Size"] == DBNull.Value ? 0 : connection["CostCodeSec2Size"]),
                                System.Convert.ToInt16(connection["CostCodeSec3Size"] == DBNull.Value ? 0 : connection["CostCodeSec3Size"]),
                                System.Convert.ToInt16(connection["CostCodeSec4Size"] == DBNull.Value ? 0 : connection["CostCodeSec4Size"]),
                                connection["CostCodePunct1"].ToString(),
                                connection["CostCodePunct2"].ToString(),
                                connection["CostCodePunct3"].ToString(),
                                connection["LastApproveImportedDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(connection["LastApproveImportedDate"]),
                                connection["LastApproveRecurringDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(connection["LastApproveRecurringDate"]),
                                connection["LastApproveRMDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(connection["LastApproveRMDate"]),
                                connection["ConnectionID"].ToString());

                            if (connection["DefaultConnection"] != DBNull.Value && Convert.ToBoolean(connection["DefaultConnection"]))
                            {
                                _clsTimberlineConnection = conn;
                                break;
                            }

                        }
                    }
                }
            }
        }

        internal static bool TestForValidTimberlinePath(string sPth)
        {
           // bool returnValue = false;

            //2.1.3 FSG 5/31/08 if a user's windows permissions don't allow them access to a particular
            //data folder or the data folder has been disabled be renaming or removing ts.ctl
            //then we have to make the data folder unavailable to the user.

            //returnValue = System.IO.File.Exists(System.IO.Path.Combine(sPth, "ts.ctl"));

            //return returnValue;

            return true; // sanket - check with frank, how to handle this for API
        }
    }
}
