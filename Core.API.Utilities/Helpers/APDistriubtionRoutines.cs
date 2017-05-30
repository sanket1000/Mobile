using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Core.API.Utilities;

namespace Core.API.Utilities.Helpers
{
    internal class APDistriubtionRoutines
    {
        #region Variables-Properties

        bool bolJCSystemActive;
        bool bolDontAllowOverride;
        bool bolRetrieveAcctPrfxFrmJob;

        string strPrefixType;
        string strCategoryUsage;
        string strGlRetrieveMethod;
        string strDefaultCostAcct;
        string strTrackExtras;
        
        clsTS_CTL objTS_CTL;

        bool bolEquipTablesExist;
        bool bolEq_RetrievePrefixFromEq;
        string strEq_DefaultPrefix;
        string strEq_CostDebitAcct;
        bool bolEq_UseAccountTable;
        bool bolEq_AllowOverride;
        bool bolEq_UseJCCostAcct;
        int intEQ_TableColumns;

        public string GLAccRtvMessageString = "";
        public string GLAccRtvFromVndMessageString = "GL Account retrieved from Vendor";
        public string GLAccRtvFromJobCostMessageString = "GL Account retrieved from Job Cost";
        public string GLAccRtvFromEquipCostMessageString = "GL Account retrieved from Equipment Cost";

        public bool PrefixWasGottenFromEquipment = false;

        bool _IgnoreRetriveJobPrefixSetting = false;

        public bool IgnoreRetriveJobPrefixSetting
        {
            get
            {
                return _IgnoreRetriveJobPrefixSetting;
            }
            set
            {
                _IgnoreRetriveJobPrefixSetting = value;
            }
        }


        public bool RetrieveAcctPrfxFrmJob
        {
            get
            {
                return this.bolRetrieveAcctPrfxFrmJob;
            }
        }

        public DataTable adoApVendorRs = null;

        public bool RetrievePrefixFromEquipment
        {
            get
            {
                return bolEq_RetrievePrefixFromEq;
            }
        }

        public string RetrieveDefaultCostAccount
        {
            get
            {
                return strDefaultCostAcct;
            }

        }

        DataTable adoJCCntrlsRs = null;

        public DataTable JCM_MASTER__JC_COST_CONTROLS
        {
            get
            {
                return this.adoJCCntrlsRs;
            }
        }

        private bool TestForTaxTables()
        {
            bool exists = false;
            TimberScanDataAccess.DataAcessHelper data = null;

            string sql = "Select Tax_Group from TXM_MASTER__TAX_GROUP";

            if (TimberScan.Properties.Extended.Default.TSyncActive)
            {
                sql = sql + " WHERE ConnectionId = " + modSystemProcedures.clsTimberlineConnection.ConnectionID.ToString();
                data = new TimberScanDataAccess.DataAcessHelper(TimberScan.Properties.Extended.Default.TSyncConnectionString, TimberScanDataAccess.DataAccessType.SqlServer);
            }
            else
            {
                data = modImage.TimberlineStandardData;

            }

            DataTable dt = new DataTable();



            try
            {
                modImage.WriteInitStaticClassMessage(this.ToString(), "Querying TXM_MASTER__TAX_GROUP - SQL = " + sql);
                data.FillDataTable(dt, sql, null, true, false);
                exists = true;
            }
            catch
            {
            }
            finally
            {
                modImage.WriteInitStaticClassMessage(this.ToString(), "Querying TXM_MASTER__TAX_GROUP - SQL = " + sql);
            }
            return exists;

        }

        public string GLPrefixType
        {
            get
            {
                string returnValue = null;
                returnValue = strPrefixType;
                return returnValue;
            }
        }

        public string GlRetrieveMethod
        {
            get
            {
                string returnValue = null;
                returnValue = strGlRetrieveMethod;
                return returnValue;
            }
        }

        public bool UseCategories
        {
            get
            {
                bool returnValue = true;
                returnValue = (strCategoryUsage.ToUpper() == "ALWAYS USE");
                return returnValue;
            }
        }

        public bool JCSystemActive
        {
            get
            {
                return this.bolJCSystemActive;
            }
        }

        public bool TrackExtras
        {
            get
            {
                bool returnValue = true;
                returnValue = strTrackExtras.ToUpper() == "TRACKING";
                return returnValue;
            }
        }

        public bool DontAllowOverride
        {
            get
            {
                bool returnValue = true;
                returnValue = bolDontAllowOverride;
                return returnValue;
            }
        }

        public bool EquipTablesExist
        {
            get
            {
                bool returnValue = true;
                returnValue = bolEquipTablesExist;
                return returnValue;
            }
        }

        public bool Eq_AllowOverride
        {
            get
            {
                bool returnValue = true;
                returnValue = bolEq_AllowOverride;
                return returnValue;
            }
        }

        private bool bolTaxTables = false;

        public bool TaxTables
        {
            get
            {
                return this.bolTaxTables;
            }
        }

        #endregion 

        public APDistriubtionRoutines()
        {
            Class_Initialize_Renamed();

            this.bolTaxTables = this.TestForTaxTables();
        }


        private void Class_Initialize_Renamed()
        {
            
            string sQry = null;
            TimberScanDataAccess.DataAcessHelper TimberlineStandardData = modImage.TimberlineStandardData;

            
            clsGLRoutines GLRoutinesObject = new clsGLRoutines();
            
            objTS_CTL = new clsTS_CTL();
            

            strPrefixType = objTS_CTL.GLPrefixABCLength > 0 ? "PrefixC" : (objTS_CTL.GLPrefixABLength > 0 ? "PrefixB" : "PrefixA");

            strGlRetrieveMethod = "";

            strCategoryUsage = "";
            strDefaultCostAcct = "";
            strTrackExtras = "";
            bolRetrieveAcctPrfxFrmJob = false;
            bolJCSystemActive = false;

            strEq_DefaultPrefix = "";
            bolEq_RetrievePrefixFromEq = false;
            bolEquipTablesExist = false;
            strEq_CostDebitAcct = "";
            bolEq_UseAccountTable = false;
            bolEq_AllowOverride = false;
            bolEq_UseJCCostAcct = false;
            intEQ_TableColumns = 0;

            adoJCCntrlsRs = new DataTable();
            sQry = "SELECT * FROM JCM_MASTER__JC_COST_CONTROLS";

            try
            {
                modImage.WriteInitStaticClassMessage(this.ToString(), "querying JCM_MASTER__JC_COST_CONTROLS. SQL=" + sQry);
                TimberlineStandardData.FillDataTable(adoJCCntrlsRs, sQry, null, true, false);

            }
            catch
            {
            }
            finally
            {
                modImage.WriteInitStaticClassMessage(this.ToString(), "querying JCM_MASTER__JC_COST_CONTROLS done");
            }
            if (adoJCCntrlsRs != null && adoJCCntrlsRs.Rows.Count > 0)
            {

                bolJCSystemActive = true;
                strGlRetrieveMethod = adoJCCntrlsRs.Rows[0]["GL_Retrieval_Method"].ToString();
                strCategoryUsage = adoJCCntrlsRs.Rows[0]["Category_Usage"].ToString();
                strDefaultCostAcct = adoJCCntrlsRs.Rows[0]["Cost_Account"].ToString();
                strTrackExtras = adoJCCntrlsRs.Rows[0]["Track_Extras"].ToString();
                strGlRetrieveMethod = adoJCCntrlsRs.Rows[0]["GL_Retrieval_Method"].ToString();
                bolDontAllowOverride = System.Convert.ToBoolean(adoJCCntrlsRs.Rows[0]["Automatic_GL"]);
                bolDontAllowOverride = System.Convert.ToBoolean(adoJCCntrlsRs.Rows[0]["Automatic_GL"]);
                bolRetrieveAcctPrfxFrmJob = System.Convert.ToBoolean(adoJCCntrlsRs.Rows[0]["Use_Prefix"]);

            }


            sQry = "SELECT Default_Prefix, Cost_Debit_Account, Cost_DR_Use_Acct_Table, Cost_DR_Allow_Override, Cost_DR_Use_JC_Acct, Cost_DR_Table_Columns FROM EQM_MASTER__EQ_CONTROLS";


            DataTable adoEQCntrlsRs = new DataTable();


            try
            {
                modImage.WriteInitStaticClassMessage(this.ToString(), "querying EQM_MASTER__EQ_CONTROLS. SQL=" + sQry);
                TimberlineStandardData.FillDataTable(adoEQCntrlsRs, sQry, null, true, false);

            }
            catch
            {
            }
            finally
            {

                modImage.WriteInitStaticClassMessage(this.ToString(), "querying EQM_MASTER__EQ_CONTROLS done");
            }


            if (adoEQCntrlsRs != null && adoEQCntrlsRs.Rows.Count > 0)
            {
                bolEquipTablesExist = true;
                strEq_DefaultPrefix = adoEQCntrlsRs.Rows[0]["Default_Prefix"].ToString();
                strEq_CostDebitAcct = CommonFunctions.TestNullString(adoEQCntrlsRs.Rows[0]["Cost_Debit_Account"].ToString());
                bolEq_UseAccountTable = Convert.ToBoolean(CommonFunctions.TestNullNumber(adoEQCntrlsRs.Rows[0]["Cost_DR_Use_Acct_Table"]));
                bolEq_AllowOverride = Convert.ToBoolean(CommonFunctions.TestNullNumber(adoEQCntrlsRs.Rows[0]["Cost_DR_Allow_Override"]));
                bolEq_UseJCCostAcct = Convert.ToBoolean(CommonFunctions.TestNullNumber(adoEQCntrlsRs.Rows[0]["Cost_DR_Use_JC_Acct"]));
                intEQ_TableColumns = System.Convert.ToInt32(CommonFunctions.TestNullNumber(adoEQCntrlsRs.Rows[0]["Cost_DR_Table_Columns"].ToString()));

            }

            sQry = "SELECT Retrieve_prefix_from_Equipment FROM EQS_STANDARD__EQ_RETRIEVAL_CONTROLS";
            adoEQCntrlsRs = new DataTable();

            try
            {
                modImage.WriteInitStaticClassMessage(this.ToString(), "querying EQS_STANDARD__EQ_RETRIEVAL_CONTROLS");
                TimberlineStandardData.FillDataTable(adoEQCntrlsRs, sQry, null, true, false);

            }
            catch
            {
            }
            finally
            {
                modImage.WriteInitStaticClassMessage(this.ToString(), "querying EQS_STANDARD__EQ_RETRIEVAL_CONTROLS done");
            }

            if (adoEQCntrlsRs != null && adoEQCntrlsRs.Rows.Count > 0)
            {

                bolEq_RetrievePrefixFromEq = (bool)(adoEQCntrlsRs.Rows[0]["Retrieve_prefix_from_Equipment"]);

            }

            modImage.WriteInitStaticClassMessage(this.ToString(), "Class_Initialized_Renamed - DONE");

            //VERY IMPORTANT CHANGE! - NOT USING BASE ACCOUNT
            //The following added code makes sure that even when the prefix is returned from equipment
            //and the situation is such that the default cost debit account is returned,
            //the full cost debit account is returned and not just the base one.
            if (bolEq_RetrievePrefixFromEq)
            {
                string Pnct = DeterminePunctuation();
                string eqPrf = strEq_DefaultPrefix + Pnct;
                if (!strEq_CostDebitAcct.Contains(eqPrf))
                    strEq_CostDebitAcct = eqPrf + strEq_CostDebitAcct;
            }
            //VERY IMPORTANT CHANGE!         //Irene 11/8/2012

        }
    }
}
