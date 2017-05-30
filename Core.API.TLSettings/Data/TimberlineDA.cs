using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Core.API.Utilities;
using Core.API.Utilities.Helpers;

namespace Core.API.TLSettings.Data
{
    internal class TimberlineDA
    {
        internal static DataTable GetAPDistributionSettings(int ConnectionID)
        {
            DataTable dt = new DataTable();

            APIUtilDataAccessHelper.TimberSynchDataDynamic(ConnectionID).FillDataTable(dt, "SELECT * FROM APM_MASTER__AP_CONTROLS", null, true, false);
            return dt;
        }

        internal static DataTable GetAPDistStdDesc(int ConnectionID )
        {
            DataTable dt = new DataTable();
            APIUtilDataAccessHelper.TimberSynchDataDynamic(ConnectionID).FillDataTable(dt, "SELECT * FROM APM_MASTER__DISTRIBUTION where vendor='.~' and invoice='.~'", null, true, false);
            //APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(dt, "SELECT * FROM APM_MASTER__DISTRIBUTION where vendor='.~' and invoice='.~'", null, true, false);
            return dt;
        }

        internal static DataTable GetAPDistCustDesc(int ConnectionID )
        {
            DataTable dt = new DataTable();

            APIUtilDataAccessHelper.TimberSynchDataDynamic(ConnectionID).FillDataTable(dt, "SELECT * FROM MASTER_APM_DISTRIBUTION where vendor='.~' and invoice='.~'", null, true, false);
            //APIUtilDataAccessHelper.TimberlineCustomData.FillDataTable(dt, "SELECT * FROM MASTER_APM_DISTRIBUTION where vendor='.~' and invoice='.~'", null, true, false);
            return dt;
        }

        internal static DataTable GetAPInvoiceStdDesc(int ConnectionID )
        {
            DataTable dt = new DataTable();

            APIUtilDataAccessHelper.TimberSynchDataDynamic(ConnectionID).FillDataTable(dt, "SELECT * FROM APM_MASTER__INVOICE where vendor='.~' and invoice='.~'", null, true, false);
            //APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(dt, "SELECT * FROM APM_MASTER__INVOICE where vendor='.~' and invoice='.~'", null, true, false);
            return dt;
        }

        internal static DataTable GetAPInvoiceCustDesc(int ConnectionID )
        {
            DataTable dt = new DataTable();

            APIUtilDataAccessHelper.TimberSynchDataDynamic(ConnectionID).FillDataTable(dt, "SELECT * FROM MASTER_APM_INVOICE WHERE vendor='.~' and invoice='.~'", null, true, false);
            //APIUtilDataAccessHelper.TimberlineCustomData.FillDataTable(dt, "SELECT * FROM MASTER_APM_INVOICE WHERE vendor='.~' and invoice='.~'", null, true, false);
            return dt;
        }

        internal static DataTable GetCM_MASTER__JC_COST_CONTROLS(int ConnectionID )
        {
            DataTable dt = new DataTable();

            APIUtilDataAccessHelper.TimberSynchDataDynamic(ConnectionID).FillDataTable(dt, "SELECT * FROM JCM_MASTER__JC_COST_CONTROLS", null, true, false);
            //APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(dt, "SELECT * FROM JCM_MASTER__JC_COST_CONTROLS", null, true, false);
            return dt;
        }
        internal static DataTable GetEQM_MASTER__EQ_CONTROLS(int ConnectionID )
        {
            DataTable dt = new DataTable();

            try
            {
                APIUtilDataAccessHelper.TimberSynchDataDynamic(ConnectionID).FillDataTable(dt, "SELECT Default_Prefix, Cost_Debit_Account, Cost_DR_Use_Acct_Table, Cost_DR_Allow_Override, Cost_DR_Use_JC_Acct, Cost_DR_Table_Columns FROM EQM_MASTER__EQ_CONTROLS", null, true, false);
                //APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(dt, "SELECT Default_Prefix, Cost_Debit_Account, Cost_DR_Use_Acct_Table, Cost_DR_Allow_Override, Cost_DR_Use_JC_Acct, Cost_DR_Table_Columns FROM EQM_MASTER__EQ_CONTROLS", null, true, false);
            }
            catch (Exception ex)
            {
            }
            return dt;
        }
        internal static DataTable GetEQS_STANDARD__EQ_RETRIEVAL_CONTROLS(int ConnectionID )
        {
            DataTable dt = new DataTable();

            try
            {
                APIUtilDataAccessHelper.TimberSynchDataDynamic(ConnectionID).FillDataTable(dt, "SELECT Retrieve_prefix_from_Equipment FROM EQS_STANDARD__EQ_RETRIEVAL_CONTROLS", null, true, false);
                //APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(dt, "SELECT Retrieve_prefix_from_Equipment FROM EQS_STANDARD__EQ_RETRIEVAL_CONTROLS", null, true, false);
            }
            catch (Exception ex)
            {
            }
            return dt;
        }
        internal static DataTable GetJobAuthorization(string job, int ConnectionID )
        {
            DataTable dt = new DataTable();
            APIUtilDataAccessHelper.TimberSynchDataDynamic(ConnectionID).FillDataTable(dt,
                                                             "SELECT  \"Authorization\" FROM JCM_MASTER__JOB WHERE Job='" +
                                                             job.Replace("'", "''") + "'", null, true, false);
            //APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(dt,
            //                                                "SELECT  \"Authorization\" FROM JCM_MASTER__JOB WHERE Job='" +
            //                                                job.Replace("'", "''") + "'", null, true, false);

            return dt;
        }

        internal static DataTable GetTSCTLSettings(int ConnectionID )
        {
            DataTable dt = new DataTable();
            //APIUtilDataAccessHelper.TimberlineDictData.FillDataTable(dt, "SELECT Section,CSLEN,CNDESC,CNPUNCT,FLDCODE,CNTCAP FROM TS_CTL_RECORD_3 WHERE FLDCODE in (9,16,18,69,187,20,50,51,52,500) ORDER BY FLDCODE", null, true, false);
            APIUtilDataAccessHelper.TimberSynchDataDynamic(ConnectionID).FillDataTable(dt, "SELECT Section,CSLEN,CNDESC,CNPUNCT,FLDCODE,CNTCAP FROM TS_CTL_RECORD_3 WHERE FLDCODE in (9,16,18,69,187,20,50,51,52,500) ORDER BY FLDCODE", null, true, false);
            return dt;
        }
    }

}
