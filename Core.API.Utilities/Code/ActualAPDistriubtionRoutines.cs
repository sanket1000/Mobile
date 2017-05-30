using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Core.API.Utilities.Helpers;
using Core.API.Utilities.Code;

using Core.API.DataAccess;

namespace Core.API.Utilities.Code
{
    public class actualAPDistriubtionRoutines
    {

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

        private void Init()
        {
            //////TLSettingsHelper.WriteInitStaticClassMessage(this.ToString(), "Class_Initialized_Renamed");
            string sQry = null;
          

            ////TLSettingsHelper.WriteInitStaticClassMessage(this.ToString(), "instantiating clsGLRoutines");
            clsGLRoutines GLRoutinesObject = new clsGLRoutines();
            ////TLSettingsHelper.WriteInitStaticClassMessage(this.ToString(), "instantiating clsGLRoutines done");

            ////TLSettingsHelper.WriteInitStaticClassMessage(this.ToString(), "instantiating objTS_CTL");
            objTS_CTL = new clsTS_CTL();
            ////TLSettingsHelper.WriteInitStaticClassMessage(this.ToString(), "instantiating objTS_CTL done");

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
                ////TLSettingsHelper.WriteInitStaticClassMessage(this.ToString(), "querying JCM_MASTER__JC_COST_CONTROLS. SQL=" + sQry);
                APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(adoJCCntrlsRs, sQry, null, true, false);

            }
            catch
            {
            }
            finally
            {
                ////TLSettingsHelper.WriteInitStaticClassMessage(this.ToString(), "querying JCM_MASTER__JC_COST_CONTROLS done");
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
                ////TLSettingsHelper.WriteInitStaticClassMessage(this.ToString(), "querying EQM_MASTER__EQ_CONTROLS. SQL=" + sQry);
                APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(adoEQCntrlsRs, sQry, null, true, false);

            }
            catch
            {
            }
            finally
            {

                //TLSettingsHelper.WriteInitStaticClassMessage(this.ToString(), "querying EQM_MASTER__EQ_CONTROLS done");
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
                //TLSettingsHelper.WriteInitStaticClassMessage(this.ToString(), "querying EQS_STANDARD__EQ_RETRIEVAL_CONTROLS");
                APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(adoEQCntrlsRs, sQry, null, true, false);

            }
            catch
            {
            }
            finally
            {
                //TLSettingsHelper.WriteInitStaticClassMessage(this.ToString(), "querying EQS_STANDARD__EQ_RETRIEVAL_CONTROLS done");
            }

            if (adoEQCntrlsRs != null && adoEQCntrlsRs.Rows.Count > 0)
            {

                bolEq_RetrievePrefixFromEq = (bool)(adoEQCntrlsRs.Rows[0]["Retrieve_prefix_from_Equipment"]);

            }

            //TLSettingsHelper.WriteInitStaticClassMessage(this.ToString(), "Class_Initialized_Renamed - DONE");

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

        /*private TimberScanDataAccess.DataAcessHelper _timberlineStandardData = null;
        private TimberScanDataAccess.DataAcessHelper TimberlineStandardData
        {
            get
            {

                return this._timberlineStandardData;
            }
        }
         * */
        public actualAPDistriubtionRoutines()
        {


            //TLSettingsHelper.WriteInitStaticClassMessage(this.ToString(), "constructor");
            Init();

            this.bolTaxTables = this.TestForTaxTables();
            //TLSettingsHelper.WriteInitStaticClassMessage(this.ToString(), "constructor - Done");
        }

        private bool TestForTaxTables()
        {
            bool exists = false;
            //TimberScanDataAccess.DataAcessHelper data = null;

            string sql = "Select Tax_Group from TXM_MASTER__TAX_GROUP";

            //--- REVISIT--SANKET
            //if (TimberScan.Properties.Extended.Default.TSyncActive)
            //{
            //    sql = sql + " WHERE ConnectionId = " + modSystemProcedures.clsTimberlineConnection.ConnectionID.ToString();
            //    data = new TimberScanDataAccess.DataAcessHelper(TimberScan.Properties.Extended.Default.TSyncConnectionString, TimberScanDataAccess.DataAccessType.SqlServer);
            //}
            //else
            //{
            //    data = TLSettingsHelper.TimberlineStandardData;

            //}

            DataTable dt = new DataTable();



            try
            {
                //TLSettingsHelper.WriteInitStaticClassMessage(this.ToString(), "Querying TXM_MASTER__TAX_GROUP - SQL = " + sql);
                APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(dt, sql, null, true, false);
                exists = true;
            }
            catch
            {
            }
            finally
            {
                //TLSettingsHelper.WriteInitStaticClassMessage(this.ToString(), "Querying TXM_MASTER__TAX_GROUP - SQL = " + sql);
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


        public string RetrieveGLPrefixFromEquipment(string sEquip)
        {

            clsGLRoutines GLRoutinesObject = new clsGLRoutines();
            string sEquipPrefix = "";
            string sQry = "";
            DataTable aRs = null;
            //TimberScanDataAccess.DataAcessHelper tLineData = null;

            //if (sEquip > "" && sEqCstCode > "") // ERF original
            if (!string.IsNullOrEmpty(sEquip)) // ERF
            {
                sQry = "SELECT GL_Prefix FROM EQM_MASTER__EQUIPMENT WHERE Equipment=" + CommonFunctions.QNApos(sEquip);


                aRs = new DataTable();

                // REVISIT-SANKET
                //if (TimberScan.Properties.Extended.Default.TSyncActive)
                //{
                //    sQry += " AND ConnectionID = " + modSystemProcedures.clsTimberlineConnection.ConnectionID.ToString();
                //    tLineData = new TimberScanDataAccess.DataAcessHelper(TimberScan.Properties.Extended.Default.TSyncConnectionString, TimberScanDataAccess.DataAccessType.SqlServer);
                //}
                //else
                //{
                //    tLineData = TLSettingsHelper.TimberlineStandardData;
                //}

                APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(aRs, sQry, null, true, false);



                if (aRs != null && aRs.Rows.Count > 0)
                {

                    sEquipPrefix = string.IsNullOrEmpty(aRs.Rows[0]["GL_Prefix"].ToString().Trim()) ? string.Empty : aRs.Rows[0]["GL_Prefix"].ToString();
                }

                if (sEquipPrefix == "")
                {
                    sQry = "SELECT Default_Prefix FROM EQM_MASTER__EQ_CONTROLS";

                    aRs = new DataTable();



                    APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(aRs, sQry, null, true, false);

                    if (aRs != null && aRs.Rows.Count > 0)
                    {
                        sEquipPrefix = string.IsNullOrEmpty(aRs.Rows[0]["Default_Prefix"].ToString()) ? string.Empty : aRs.Rows[0]["Default_Prefix"].ToString();
                    }
                }

            }

            return sEquipPrefix;

        }

        public string DetermineAPDistGLPrefix(string sJb, string sExt, string sCstCd, string sCat, ref string sEquip, string sEqCstCode, bool bEquipColVis, DataTable aVndRs)
        {

            clsGLRoutines GLRoutinesObject = new clsGLRoutines();
            string sGLPrefix;
            string sQry = "";
            string sGLAccount = "";
            string sDebitAccount;
            string sEquipment;
            int iLen;
            int iPrefixLen;
            string sPrefix;
            DataTable aRs = null;
            string sCostAcctGrp;
            int iSuffixLen = 0;
            int iMinPrefixLen;
            string sPrxLvl = "";
            object objAffected = null;
            //TimberScanDataAccess.DataAcessHelper tLineData = null;

            //1st from Job Cost, then from EQ Cost Debit Account, then from Vendor record
            sGLPrefix = "";

            sDebitAccount = "";
            sEquipment = "";

            bool bMemoCost = false;
            if (!string.IsNullOrEmpty(sEquip) && !string.IsNullOrEmpty(sEqCstCode))
            {
                bMemoCost = EquipmentMemoCost(sEqCstCode);
            }

            if (objTS_CTL.GLPrefixABCLength > 0)
            {
                sPrxLvl = "PrefixC";
            }
            else if (objTS_CTL.GLPrefixABLength > 0) // ERF
            {
                sPrxLvl = "PrefixB";
            }
            else if (objTS_CTL.GLPrefixALength > 0) // ERF
            {
                sPrxLvl = "PrefixA";
            }
            else
            {
                return "";
            }

            //modIniFiles.LogFile"DetermineAPDistGLPrefix"

            sPrefix = "";
            //if (sEquip > "" && sEqCstCode > "") // ERF original
            if (!string.IsNullOrEmpty(sEquip) && !string.IsNullOrEmpty(sEqCstCode) && !bMemoCost) // ERF
            {
                if (!bolEq_RetrievePrefixFromEq)
                {
                    sPrefix = GetExpenseAccountFromEquip(ref sEquip, sEqCstCode);
                    sPrefix = objTS_CTL.DetermineGLPrefix(sPrefix, sPrxLvl);
                    return sPrefix;
                }

                sQry = "SELECT GL_Prefix FROM EQM_MASTER__EQUIPMENT WHERE Equipment=" + CommonFunctions.QNApos(sEquip);


                aRs = new DataTable();

                //REVISIT-SANKET
                //if (TimberScan.Properties.Extended.Default.TSyncActive)
                //{
                //    sQry += " AND ConnectionID = " + modSystemProcedures.clsTimberlineConnection.ConnectionID.ToString();
                //    tLineData = new TimberScanDataAccess.DataAcessHelper(TimberScan.Properties.Extended.Default.TSyncConnectionString, TimberScanDataAccess.DataAccessType.SqlServer);
                //}
                //else
                //{
                //    tLineData = TLSettingsHelper.TimberlineStandardData;
                //}

                APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(aRs, sQry, null, true, false);



                if (aRs != null && aRs.Rows.Count > 0)
                {

                    sPrefix = string.IsNullOrEmpty(aRs.Rows[0]["GL_Prefix"].ToString().Trim()) ? string.Empty : aRs.Rows[0]["GL_Prefix"].ToString();
                }
                if (string.IsNullOrEmpty(sPrefix))
                {
                    sPrefix = strEq_DefaultPrefix;
                }
                if (!string.IsNullOrEmpty(sPrefix)) // ERF
                {
                    PrefixWasGottenFromEquipment = true;
                    return sPrefix;
                }
            }

            if (bolJCSystemActive == true)
            {
                if (bolRetrieveAcctPrfxFrmJob == true && !string.IsNullOrEmpty(sJb)) // ERF
                {
                    sQry = "SELECT Cost_Account_Prefix FROM JCM_MASTER__JOB WHERE Job=" + CommonFunctions.QNApos(sJb);
                    aRs = new DataTable();

                    //REVISIT-SANKET
                    //if (TimberScan.Properties.Extended.Default.TSyncActive)
                    //{
                    //    sQry += " AND ConnectionID = " + modSystemProcedures.clsTimberlineConnection.ConnectionID.ToString();
                    //    tLineData = new TimberScanDataAccess.DataAcessHelper(TimberScan.Properties.Extended.Default.TSyncConnectionString, TimberScanDataAccess.DataAccessType.SqlServer);
                    //}
                    //else
                    //{
                    //    tLineData = TLSettingsHelper.TimberlineStandardData;
                    //}

                    APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(aRs, sQry, null, true, false);

                    if (aRs == null || aRs.Rows.Count <= 0)
                    {
                        return "";
                    }
                    else
                    {

                        return string.IsNullOrEmpty(aRs.Rows[0]["Cost_Account_Prefix"].ToString().Trim()) ? string.Empty : aRs.Rows[0]["Cost_Account_Prefix"].ToString();
                    }
                }

                //if (sJb > "" && sCat > "" && sCstCd > "" && strGlRetrieveMethod.ToUpper() == "RETRIEVE BY CATEGORY") // ERF original
                if (!string.IsNullOrEmpty(sJb) && !string.IsNullOrEmpty(sCat) && !string.IsNullOrEmpty(sCstCd)
                    && strGlRetrieveMethod.ToUpper() == "RETRIEVE BY CATEGORY") // ERF
                {
                    //If sJb > "" And sCat > "" And sCstCd > "" Then

                    sQry = "SELECT Cost_Account FROM JCM_MASTER__CATEGORY WHERE Job=" + CommonFunctions.QNApos(sJb) + " AND Cost_Code=" + CommonFunctions.QNApos(sCstCd) + " AND Category=" + CommonFunctions.QNApos(sCat);

                    if (!string.IsNullOrEmpty(sExt))
                    {
                        sQry = sQry + " AND Extra=" + CommonFunctions.QNApos(sExt);
                    }
                    else
                    {
                        sQry = sQry + " AND Extra=\'\'";
                    }

                    aRs = new DataTable();

                    //REVISIT-SANKET
                    //if (TimberScan.Properties.Extended.Default.TSyncActive)
                    //{
                    //    sQry += " AND ConnectionID = " + modSystemProcedures.clsTimberlineConnection.ConnectionID.ToString();
                    //    tLineData = new TimberScanDataAccess.DataAcessHelper(TimberScan.Properties.Extended.Default.TSyncConnectionString, TimberScanDataAccess.DataAccessType.SqlServer);
                    //}
                    //else
                    //{
                    //    tLineData = TLSettingsHelper.TimberlineStandardData;
                    //}

                    if (aRs != null && aRs.Rows.Count > 0)
                    {
                        sGLAccount = string.IsNullOrEmpty(aRs.Rows[0]["Cost_Account"].ToString().Trim()) ? string.Empty : aRs.Rows[0]["Cost_Account"].ToString();
                    }

                    iSuffixLen = TLSettingsHelper.GLMasterDescriptions.SuffixLength > 0 ? TLSettingsHelper.GLMasterDescriptions.SuffixLength + 1 : 0; // ERF
                    if (!string.IsNullOrEmpty(sGLAccount) &&
                        sGLAccount.Length > TLSettingsHelper.GLMasterDescriptions.BaseAcctLength +
                        iSuffixLen) // ERF
                    {
                        return GLRoutinesObject.GetEntityIDFromPrefix(sGLAccount, strPrefixType);
                    }
                }

                if (!string.IsNullOrEmpty(sJb) && !string.IsNullOrEmpty(sCstCd) &&
                    (strGlRetrieveMethod.ToUpper() == "RETRIEVE COST CODE" || strGlRetrieveMethod.ToUpper().Contains(TLSettingsHelper.APDistributionSettingsStatic.Cost_Code_Desc.ToUpper())))
                {
                    sQry = "SELECT Cost_Account FROM JCM_MASTER__COST_CODE WHERE Job=" +
                        CommonFunctions.QNApos(sJb) +
                        " AND Cost_Code=" +
                        CommonFunctions.QNApos(sCstCd);

                    if (!string.IsNullOrEmpty(sExt))
                    {
                        sQry = sQry + " AND Extra=" + CommonFunctions.QNApos(sExt);
                    }

                    aRs = new DataTable();

                    //REVISIT-SANKET
                    //if (TimberScan.Properties.Extended.Default.TSyncActive)
                    //{
                    //    sQry += " AND ConnectionID = " + modSystemProcedures.clsTimberlineConnection.ConnectionID.ToString();
                    //    tLineData = new TimberScanDataAccess.DataAcessHelper(TimberScan.Properties.Extended.Default.TSyncConnectionString, TimberScanDataAccess.DataAccessType.SqlServer);
                    //}
                    //else
                    //{
                    //    tLineData = TLSettingsHelper.TimberlineStandardData;
                    //}

                    if (aRs != null && aRs.Rows.Count > 0)
                    {
                        sGLAccount = string.IsNullOrEmpty(aRs.Rows[0]["Cost_Account"].ToString().Trim()) ? string.Empty : aRs.Rows[0]["Cost_Account"].ToString();
                    }

                    if (!string.IsNullOrEmpty(sGLAccount))
                    {
                        sGLPrefix = GLRoutinesObject.GetEntityIDFromPrefix(sGLAccount, strPrefixType);
                        if (!string.IsNullOrEmpty(sGLPrefix))
                        {
                            return sGLPrefix;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(sJb) && strGlRetrieveMethod.ToUpper() == "RETRIEVE BY JOB")
                        {
                            sQry = "SELECT Cost_Account_Group, Cost_Account FROM JCM_MASTER__JOB WHERE Job=" + CommonFunctions.QNApos(sJb);

                            aRs = new DataTable();

                            //REVISIT-SANKET
                            //if (TimberScan.Properties.Extended.Default.TSyncActive)
                            //{
                            //    sQry += " AND ConnectionID = " + modSystemProcedures.clsTimberlineConnection.ConnectionID.ToString();
                            //    tLineData = new TimberScanDataAccess.DataAcessHelper(TimberScan.Properties.Extended.Default.TSyncConnectionString, TimberScanDataAccess.DataAccessType.SqlServer);
                            //}
                            //else
                            //{
                            //    tLineData = TLSettingsHelper.TimberlineStandardData;
                            //}

                            if (aRs == null || aRs.Rows.Count <= 0)
                                return string.Empty;

                            sGLPrefix = string.IsNullOrEmpty(aRs.Rows[0]["Cost_Account"].ToString().Trim()) ? string.Empty : aRs.Rows[0]["Cost_Account"].ToString();

                            if (!string.IsNullOrEmpty(sGLPrefix))
                            {
                                return objTS_CTL.DetermineGLPrefix(sGLPrefix, sPrxLvl);
                            }
                            else
                            {
                                sCostAcctGrp = string.IsNullOrEmpty(aRs.Rows[0]["Cost_Account_Group"].ToString().Trim()) ? string.Empty : aRs.Rows[0]["Cost_Account_Group"].ToString();
                                if (string.IsNullOrEmpty(sCostAcctGrp))
                                {
                                    return string.Empty;

                                }

                                sQry = "SELECT Cost_Account_" + sCostAcctGrp.Substring(sCostAcctGrp.Length - 1, 1) + " FROM JCM_MASTER__STANDARD_COST_CODE WHERE Cost_Code=" + CommonFunctions.QNApos(sCstCd);

                                aRs = new DataTable();

                                // SANKET-REVISIT
                                //if (TimberScan.Properties.Extended.Default.TSyncActive)
                                //{
                                //    sQry += " AND ConnectionID = " + modSystemProcedures.clsTimberlineConnection.ConnectionID.ToString();
                                //    tLineData = new TimberScanDataAccess.DataAcessHelper(TimberScan.Properties.Extended.Default.TSyncConnectionString, TimberScanDataAccess.DataAccessType.SqlServer);
                                //}
                                //else
                                //{
                                //    tLineData = TLSettingsHelper.TimberlineStandardData;
                                //}

                                //If aRs.EOF Then DetermineAPDistGLPrefix = "" : Exit Function
                                sGLAccount = string.IsNullOrEmpty(aRs.Rows[0][0].ToString().Trim()) ? string.Empty : aRs.Rows[0][0].ToString();

                                if (!string.IsNullOrEmpty(sGLAccount))
                                {
                                    sGLPrefix = GLRoutinesObject.GetEntityIDFromPrefix(sGLAccount, strPrefixType);
                                    return sGLPrefix;
                                }
                                else
                                {
                                    return string.Empty;
                                }
                            }
                        }
                    }
                }

                //if (sJb > "") // ERF original
                if (!string.IsNullOrEmpty(sJb)) // ERF
                {
                    sQry = "SELECT Cost_Account_Group, Cost_Account, Cost_Account_Prefix FROM JCM_MASTER__JOB WHERE Job=" + CommonFunctions.QNApos(sJb);

                    aRs = new DataTable();

                    //REVISIT-SANKET
                    //if (TimberScan.Properties.Extended.Default.TSyncActive)
                    //{
                    //    sQry += " AND ConnectionID = " + modSystemProcedures.clsTimberlineConnection.ConnectionID.ToString();
                    //    tLineData = new TimberScanDataAccess.DataAcessHelper(TimberScan.Properties.Extended.Default.TSyncConnectionString, TimberScanDataAccess.DataAccessType.SqlServer);
                    //}
                    //else
                    //{
                    //    tLineData = TLSettingsHelper.TimberlineStandardData;
                    //}
                    //If aRs.EOF Then DetermineAPDistGLPrefix = "" : Exit Function

                    APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(aRs, sQry, null, true, false);

                    if (aRs == null || aRs.Rows.Count <= 0)
                    {
                        return string.Empty;
                    }

                    if (bolRetrieveAcctPrfxFrmJob == true)
                    {
                        return string.IsNullOrEmpty(aRs.Rows[0]["Cost_Account_Prefix"].ToString().Trim()) ? string.Empty : aRs.Rows[0]["Cost_Account_Prefix"].ToString().Trim();
                    }
                    else
                    {
                        sGLPrefix = string.IsNullOrEmpty(aRs.Rows[0]["Cost_Account"].ToString().Trim()) ? string.Empty : aRs.Rows[0]["Cost_Account"].ToString().Trim();
                    }

                    if (!string.IsNullOrEmpty(sGLPrefix))
                    {
                        return objTS_CTL.DetermineGLPrefix(sGLPrefix, sPrxLvl, false);
                    }
                    else
                    {
                        sCostAcctGrp = string.IsNullOrEmpty(aRs.Rows[0]["Cost_Account_Group"].ToString().Trim()) ? string.Empty : aRs.Rows[0]["Cost_Account_Group"].ToString().Trim();

                        if (string.IsNullOrEmpty(CommonFunctions.TestNullString(sCostAcctGrp).Trim()))
                        {
                            return string.Empty;
                        }

                        if (string.IsNullOrEmpty(CommonFunctions.TestNullString(sCat).Trim()))
                        {
                            return string.Empty;
                        }

                        sQry = "SELECT Cost_Account_" + sCostAcctGrp.Substring(sCostAcctGrp.Length - 1, 1) + " FROM JCM_MASTER__STANDARD_CATEGORY WHERE Category=" + CommonFunctions.QNApos(sCat);

                        aRs = new DataTable();

                        //REVISIT-SANKET
                        //if (TimberScan.Properties.Extended.Default.TSyncActive)
                        //{
                        //    sQry += " AND ConnectionID = " + modSystemProcedures.clsTimberlineConnection.ConnectionID.ToString();
                        //    tLineData = new TimberScanDataAccess.DataAcessHelper(TimberScan.Properties.Extended.Default.TSyncConnectionString, TimberScanDataAccess.DataAccessType.SqlServer);
                        //}
                        //else
                        //{
                        //    tLineData = TLSettingsHelper.TimberlineStandardData;
                        //}

                        if (aRs == null || aRs.Rows.Count <= 0)
                        {
                            return string.Empty;
                        }

                        sGLAccount = string.IsNullOrEmpty(aRs.Rows[0][0].ToString().Trim()) ? string.Empty : aRs.Rows[0][0].ToString();

                        if (!string.IsNullOrEmpty(sGLAccount) &&
                            sGLAccount.Length > TLSettingsHelper.GLMasterDescriptions.BaseAcctLength +
                            iSuffixLen)
                        {
                            sGLPrefix = GLRoutinesObject.GetEntityIDFromPrefix(sGLAccount, strPrefixType);
                            return sGLPrefix;
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                }

                if (bEquipColVis == true)
                {

                    sEquipment = CommonFunctions.TestNullString(sEquip);

                    if (!string.IsNullOrEmpty(sEquipment)) // ERF
                    {
                        sQry = "SELECT Default_Prefix FROM EQM_MASTER__EQ_CONTROLS";

                        aRs = new DataTable();

                        //tLineData = TLSettingsHelper.TimberlineStandardData;

                        APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(aRs, sQry, null, true, false);

                        if (aRs != null && aRs.Rows.Count > 0)
                        {
                            sGLPrefix = string.IsNullOrEmpty(aRs.Rows[0]["Default_Prefix"].ToString()) ? string.Empty : aRs.Rows[0]["Default_Prefix"].ToString();
                        }
                        if (!string.IsNullOrEmpty(sGLPrefix))
                        {
                            return sGLPrefix;
                        }
                    }
                }
            }

            sDebitAccount = CommonFunctions.TestNullString(aVndRs.Rows[0]["Expense_Account"]);
            iPrefixLen = (TLSettingsHelper.GLMasterDescriptions.Levels == 1 ? TLSettingsHelper.GLMasterDescriptions.PrefixALength : (TLSettingsHelper.GLMasterDescriptions.Levels == 2 ? TLSettingsHelper.GLMasterDescriptions.PrefixABLength : TLSettingsHelper.GLMasterDescriptions.PrefixABCLength)) + 1; //add final -
            iSuffixLen = TLSettingsHelper.GLMasterDescriptions.SuffixLength > 0 ? TLSettingsHelper.GLMasterDescriptions.SuffixLength + 1 : 0; // ERF original

            iLen = string.IsNullOrEmpty(sDebitAccount) ? 0 : sDebitAccount.Length;

            if (iLen > TLSettingsHelper.GLMasterDescriptions.BaseAcctLength + iSuffixLen)
            {
                sGLPrefix = GLRoutinesObject.GetEntityIDFromPrefix(sDebitAccount, strPrefixType);
            }
            return sGLPrefix;
        }


        public string DetermineGLExpenseAccount(ref string sJob, ref string sCostCode, ref string sExtra, ref string sCategory, ref string sEquip, ref string sEqCstCd, bool bEquipColVis, string sVndExpAccount, ref bool bIncludePrefix)
        {
            //we return full account in cluding G/L Prefix, base account & suffix
            string returnValue = String.Empty;
            string sExpAccount = "";
            string sEquipment;
            bool bEq_RetrievePrefixFromEq = false;
            bool bMemoCost = false;
            string GLPrefix = string.Empty;

            GLAccRtvMessageString = "";

            string svEquipCostCode = sEqCstCd;

            if (!string.IsNullOrEmpty(sEquip) && !string.IsNullOrEmpty(sEqCstCd))
            {
                bMemoCost = this.EquipmentMemoCost(sEqCstCd);
            }

            sEquipment = bEquipColVis == true ? (CommonFunctions.TestNullString(sEquip)) : "";
            if (bolJCSystemActive == false || (CommonFunctions.IsNullOrEmpty(sJob) && CommonFunctions.IsNullOrEmpty(sEquipment)))
            {
                returnValue = sVndExpAccount;
                if (!CommonFunctions.IsNullOrEmpty(sVndExpAccount))
                    GLAccRtvMessageString = GLAccRtvFromVndMessageString;
                return returnValue;
            }

            //This lines are added so it would be possible to test
            //for the equipment cost by equipment right
            //after the equipment is added.
            if (string.IsNullOrEmpty(CommonFunctions.TestNullString(sEqCstCd).Trim()))
                sEqCstCd = "00-000";
            //------------------------------------ Irene 11/13/2012

            if (!string.IsNullOrEmpty(CommonFunctions.TestNullString(sEquipment).Trim()) &&
                !string.IsNullOrEmpty(CommonFunctions.TestNullString(sEqCstCd).Trim())
                )   //VERY IMPORTANT CHANGE && bMemoCost == false Jan 5 2013
            {
                //In Equipment Setting-->GL Entry Settings if Table Columns are used for the cost debit account, any combination
                //of the three listed tables can be selected - the setting for this is in the Cost_DR_Table column in the EQM_MASTER__EQ_CONTROLS table
                //The 3 table columns listed are Equipment type (ET), Equipment (EQ) and Cost code Eq (CC)
                //If only ET is selected, the field value is 1
                //If only EQ is selected, the field value is 2
                //If only CC is selected, the field value is 4
                //If ET and EQ are selected, the field value is 3
                //If ET and CC are selected, the field value is 5
                //If EQ and CC are selected, the field value is 6
                //If ET, EQ and CC are selected, the field value is 7

                //VERY IMPORTANT CHANGE - lines taken out for now.
                //if (bMemoCost == true && (CommonFunctions.IsNullOrEmpty(sCostCode) || CommonFunctions.IsNullOrEmpty(sCategory)))
                //{
                //    return string.Empty;
                //}
                //---------------------

                //if (bolEq_UseJCCostAcct == true)  //2.3.74
                if ((bolEq_UseJCCostAcct == true) && (!string.IsNullOrEmpty(sJob)) && (bMemoCost == true)) //VERY IMPORTANT CHANGE && (bMemoCost == true) Jan 5 2013
                {

                    bool notEqRetrievePrefixFromEq = !bEq_RetrievePrefixFromEq;

                    //we use "Not bolEq_RetrievePrefixFromEq" because if we are retrieving the GL Prefix from the equipment record, we do not want it here
                    sExpAccount = GetExpenseAccountFromJob(ref sJob, ref sCostCode, ref sExtra, ref sCategory, ref notEqRetrievePrefixFromEq); // ERF

                    if (!CommonFunctions.IsNullOrEmpty(sExpAccount)) // ERF
                    {
                        returnValue = sExpAccount;

                        if (!CommonFunctions.IsNullOrEmpty(returnValue))
                            GLAccRtvMessageString = GLAccRtvFromJobCostMessageString;

                        return returnValue;
                    }
                }

                if (bMemoCost == false) //VERY IMPORTANT CHANGE - addition jan 5 2013
                {                       //VERY IMPORTANT CHANGE - addition jan 5 2013 

                    if (bolEq_UseAccountTable == false)
                    {
                        if (CommonFunctions.IsNullOrEmpty(sExpAccount))
                        {
                            sExpAccount = strEq_CostDebitAcct;
                        }
                        if (bEq_RetrievePrefixFromEq == false)
                        {
                            returnValue = sExpAccount;
                        }
                        else
                        {
                            returnValue = objTS_CTL.DetermineBaseAccount(sExpAccount, true);
                        }
                        //VERY IMPORTANT CHANGE - retrieving the gl prefix from equipment - 12/13/2012
                        if (!TLSettingsHelper.APDistributionSettingsStatic.GL_Prefix && bolEq_RetrievePrefixFromEq)
                        {
                            if (returnValue != "")
                            {
                                string basAcct = objTS_CTL.DetermineBaseAccount(returnValue, true);
                                string glPref = DetermineAPDistGLPrefix(sJob, sExtra, sCostCode, sCategory, ref sEquip, sEqCstCd,
                                    TLSettingsHelper.APDistributionSettingsStatic.Equipment, adoApVendorRs);
                                if (basAcct != "" && glPref != "")
                                {
                                    string sSepChar = "";
                                    if (this.objTS_CTL.GLPrefixABCLength > 0)
                                    {
                                        sSepChar = this.objTS_CTL.GLPrefixABCPunct;
                                    }
                                    else if (objTS_CTL.GLPrefixABLength > 0)
                                    {
                                        sSepChar = this.objTS_CTL.GLPrefixABPunct;
                                    }
                                    else
                                    {
                                        sSepChar = this.objTS_CTL.GLPrefixAPunct;
                                    }
                                    returnValue = glPref + sSepChar + basAcct;
                                }
                            }
                        }
                        //-----------------------------

                        if (!CommonFunctions.IsNullOrEmpty(returnValue))
                            GLAccRtvMessageString = GLAccRtvFromEquipCostMessageString;

                        return returnValue;
                    }

                    sExpAccount = GetExpenseAccountFromEquip(ref sEquip, sEqCstCd);

                    if (bolEq_RetrievePrefixFromEq)
                    {
                        GLPrefix = GetGLPrefixFromEquipment(sEquip);
                        if (!CommonFunctions.IsNullOrEmpty(GLPrefix))
                        {
                            if (!sExpAccount.Contains(GLPrefix)) //add 1/10/2013
                            {
                                //add 1/10/2013
                                if (TLSettingsHelper.APDistributionSettingsStatic.GL_Prefix == false && strEq_CostDebitAcct == sExpAccount)
                                {
                                    sExpAccount = objTS_CTL.DetermineBaseAccount(sExpAccount, true);
                                }
                                //add 1/10/2013-------------
                                sExpAccount = GLPrefix + sExpAccount;
                            } //add 1/10/2013
                        }
                        //VERY IMPORTANT CHANGE! - NOT USING BASE ACCOUNT
                        //The following added code makes sure that even when the prefix is returned from equipment
                        //and the equipment account is returned,
                        //the full equipment is returned and not just the base one.
                        else
                        {
                            if (!CommonFunctions.IsNullOrEmpty(sExpAccount))
                            {
                                string Pnct = DeterminePunctuation();
                                string eqPrf = strEq_DefaultPrefix + Pnct;
                                if (!sExpAccount.Contains(eqPrf))
                                    sExpAccount = eqPrf + sExpAccount;
                            }
                        }
                        //VERY IMPORTANT CHANGE!         //Irene 11/8/2012
                    }


                    if (!CommonFunctions.IsNullOrEmpty(returnValue))
                        GLAccRtvMessageString = GLAccRtvFromEquipCostMessageString;

                    returnValue = sExpAccount;
                    return returnValue;

                } //VERY IMPORTANT CHANGE - addition jan 5 2013

            }

            returnValue = GetExpenseAccountFromJob(ref sJob, ref sCostCode, ref sExtra, ref sCategory, ref bIncludePrefix);

            if (!CommonFunctions.IsNullOrEmpty(returnValue))
                GLAccRtvMessageString = GLAccRtvFromJobCostMessageString;

            return returnValue;
        }
        

        private string GetGLPrefixFromEquipment(string sEquip)
        {
            string prefix = string.Empty, sql = string.Empty, sepChar = string.Empty;
            DataTable dt = new DataTable();
            //TimberScanDataAccess.DataAcessHelper TimberlineStandardData = TLSettingsHelper.TimberlineStandardData;

            if (this.objTS_CTL.GLPrefixALength == 0)
            {
                return string.Empty;
            }

            if (objTS_CTL.GLPrefixABCLength > 0)
            {
                sepChar = objTS_CTL.GLPrefixABCPunct;
            }
            else if (objTS_CTL.GLPrefixABLength > 0)
            {
                sepChar = objTS_CTL.GLPrefixABPunct;
            }
            else
            {
                sepChar = objTS_CTL.GLPrefixAPunct;
            }

            sql = "SELECT GL_Prefix FROM EQM_MASTER__EQUIPMENT WHERE Equipment=" + CommonFunctions.QNApos(sEquip);
            APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(dt, sql, null, true, false);

            if (dt != null && dt.Rows.Count > 0)
            {
                prefix = CommonFunctions.TestNullString(dt.Rows[0]["GL_Prefix"]);
                if (!string.IsNullOrEmpty(prefix))
                {
                    prefix = prefix + sepChar;
                }
            }

            return prefix;
        }

        private string GetExpenseAccountFromEquip(ref string sEquip, string sEqCstCode)
        {
            string sQry = "";
            string sEqType;
            string sExpAccount;
            DataTable aRs = null;

            //In Equipment Setting-->GL Entry Settings if Table Columns are used for the cost debit account, any combination
            //of the three listed tables can be selected - the setting for this is in the Cost_DR_Table column in the EQM_MASTER__EQ_CONTROLS table
            //The 3 table columns listed are Equipment type (ET), Equipment (EQ) and Cost code Eq (CC)
            //If only ET is selected, the field value is 1
            //If only EQ is selected, the field value is 2
            //If only CC is selected, the field value is 4
            //If ET and EQ are selected, the field value is 3
            //If ET and CC are selected, the field value is 5
            //If EQ and CC are selected, the field value is 6
            //If ET, EQ and CC are selected, the field value is 7

            //This is used when an an account table has been set up
            //Per Timberline help this is the heirarchy:

            //Order | Eq Type-ET | Equip- EQ | Code - CC
            //1    | See Note 1 |           |
            //2    |  X         |  X        | X
            //3    |            |  X        | X
            //4    |  X         |           | X
            //5    |  X         |  X        |
            //6    |            |  X        |
            //7    |  X         |           |
            //8    |            |           | X
            //9    | See Note 2 |           |

            //Note 1 - If JC info is enetered and bolEq_UseJCCostAcct is true, the JC Cost account is used
            //Note 2 - If account is not found in steps 1 - 8 then the defaul account is used
            //NOTE - step 1 is done before we call this function

            sExpAccount = "";
            sEqType = "";

            //First we see is we need to Determine Equipment Type
            if (!string.IsNullOrEmpty(System.Convert.ToString(intEQ_TableColumns)) && System.Convert.ToString(intEQ_TableColumns).Contains("1357"))
            {
                sEqType = GetEquipmentTypeFromEquip(sEquip);
            }

            if (intEQ_TableColumns == 0)
            {
                return strEq_CostDebitAcct;
            }

            string EqTpCond = "";
            if (CommonFunctions.IsNullOrEmpty(sEqType))
            {
                EqTpCond = "";
            }
            else
            {
                EqTpCond = "Equipment_Type=" + CommonFunctions.QNApos(sEqType) + " AND ";
            }


            switch (intEQ_TableColumns)
            {
                case 1:
                    sQry = "SELECT Account FROM EQS_STANDARD__COST_DEBIT_ACCOUNT_ENTRY WHERE Equipment_Type=" + CommonFunctions.QNApos(sEqType);
                    break;
                case 2:
                    sQry = "SELECT Account FROM EQS_STANDARD__COST_DEBIT_ACCOUNT_ENTRY WHERE Equipment=" + CommonFunctions.QNApos(sEquip);
                    break;
                case 4:
                    sQry = "SELECT Account FROM EQS_STANDARD__COST_DEBIT_ACCOUNT_ENTRY WHERE Cost_Code=" + CommonFunctions.QNApos(sEqCstCode);
                    break;
                case 6:
                    sQry = "SELECT Account FROM EQS_STANDARD__COST_DEBIT_ACCOUNT_ENTRY WHERE Equipment=" + CommonFunctions.QNApos(sEquip) + " AND Cost_Code=" + CommonFunctions.QNApos(sEqCstCode);
                    break;
                case 3:
                    sQry = "SELECT Account FROM EQS_STANDARD__COST_DEBIT_ACCOUNT_ENTRY WHERE " + EqTpCond + " Equipment=" + CommonFunctions.QNApos(sEquip);
                    break;
                case 5:
                    sQry = "SELECT Account FROM EQS_STANDARD__COST_DEBIT_ACCOUNT_ENTRY WHERE " + EqTpCond + " Cost_Code=" + CommonFunctions.QNApos(sEqCstCode);
                    break;
                case 7:
                    sQry = "SELECT Account FROM EQS_STANDARD__COST_DEBIT_ACCOUNT_ENTRY WHERE " + EqTpCond + " Equipment=" + CommonFunctions.QNApos(sEquip) + " AND Cost_Code=" + CommonFunctions.QNApos(sEqCstCode);
                    break;
            }

            if (intEQ_TableColumns == 1 && CommonFunctions.IsNullOrEmpty(sEqType))
            {
                sExpAccount = "";
            }
            else
            {
                aRs = new DataTable();
                //TimberScanDataAccess.DataAcessHelper tLineData = TLSettingsHelper.TimberlineStandardData;

                APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(aRs, sQry, null, true, false);
            }
            if (aRs != null && aRs.Rows.Count > 0)
            {
                sExpAccount = aRs.Rows[0]["Account"].ToString();
                if (bolEq_RetrievePrefixFromEq == true)
                {
                    sExpAccount = objTS_CTL.DetermineBaseAccount(sExpAccount, true);
                }
            }
            if (string.IsNullOrEmpty(sExpAccount))
            {
                sExpAccount = strEq_CostDebitAcct;
            }
            return sExpAccount;
        }

        private string GetEquipmentTypeFromEquip(string sEq)
        {
            string sQry = "";
            string sEqTyp;
            DataTable aRs = new DataTable();
            
            //TimberScanDataAccess.DataAcessHelper tLineData = TimberScan.Properties.Extended.Default.TSyncActive ?
            //    new TimberScanDataAccess.DataAcessHelper(TimberScan.Properties.Extended.Default.TSyncConnectionString, TimberScanDataAccess.DataAccessType.SqlServer) :
            //    TLSettingsHelper.TimberlineStandardData;


            sEqTyp = "";
            sQry = "SELECT Equipment_Type FROM EQM_MASTER__EQUIPMENT WHERE Equipment=" + CommonFunctions.QNApos(sEq);

            //sQry += TimberScan.Properties.Extended.Default.TSyncActive ? " AND ConnectionID = " + modSystemProcedures.clsTimberlineConnection.ConnectionID.ToString() : string.Empty;


            APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(aRs, sQry, null, true, false);

            if (aRs != null && aRs.Rows.Count > 0)
            {
                sEqTyp = aRs.Rows[0]["Equipment_Type"].ToString();
            }
            aRs = null;
            return sEqTyp;
        }

        private string GetExpenseAccountFromJob(ref string sJob, ref string sCostCode, ref string sExtra, ref string sCategory, ref bool bIncludePrefix)
        {
            string returnValue = null;
            string sExpAccount;
            string extraA = ""; // ERF added
            string extraB = ""; // ERF added

            sExpAccount = "";
            switch (strGlRetrieveMethod.ToUpper())
            {
                case "RETRIEVE BY JOB":

                    if (!string.IsNullOrEmpty(sJob)) // ERF
                    {
                        sExpAccount = GetCostAccount("Job", ref sJob, "", ref extraA, ref extraB, bIncludePrefix);
                    }
                    break;
                case "RETRIEVE COST CODE":
                    if (!string.IsNullOrEmpty(sCostCode)) // ERF
                    {
                        sExpAccount = GetCostAccount("CostCode", ref sJob, sExtra, ref sCostCode, ref extraB, bIncludePrefix); // ERF switched extraB in for the necessary assignment
                        //add 1/10/2013
                        if (sExpAccount == strDefaultCostAcct)
                            IgnoreRetriveJobPrefixSetting = true;
                        //add 1/10/2013-----------
                    }
                    break;
                case "RETRIEVE BY CATEGORY":
                    if (!string.IsNullOrEmpty(sCategory)) // ERF
                    {
                        sExpAccount = GetCostAccount("Category", ref sJob, sExtra, ref sCostCode, ref sCategory, bIncludePrefix);
                    }
                    break;
                case "RETRIEVE DEFAULT":
                    sExpAccount = strDefaultCostAcct;
                    IgnoreRetriveJobPrefixSetting = true;
                    break;
                case "USE HIERARCHY":

                    if (strCategoryUsage.ToUpper() == "ALWAYS USE" && !string.IsNullOrEmpty(sCategory)) // ERF
                    {
                        sExpAccount = GetCostAccount("Category", ref sJob, sExtra, ref sCostCode, ref sCategory, bIncludePrefix);
                    }

                    if (string.IsNullOrEmpty(sExpAccount) && !string.IsNullOrEmpty(sCostCode)) // ERF
                    {
                        sExpAccount = GetCostAccount("CostCode", ref sJob, sExtra, ref sCostCode, ref extraB, bIncludePrefix); // ERF switched extraB in for necessary assignment
                    }

                    if (string.IsNullOrEmpty(sExpAccount) && !string.IsNullOrEmpty(sJob)) // ERF
                    {
                        sExpAccount = GetCostAccount("Job", ref sJob, "", ref extraA, ref extraB, bIncludePrefix); // ERF switched extraA & extraB in for necessary assignments
                    }

                    if (string.IsNullOrEmpty(sExpAccount))
                    {
                        sExpAccount = strDefaultCostAcct;
                    }

                    if (sExpAccount == strDefaultCostAcct)
                        IgnoreRetriveJobPrefixSetting = true;

                    break;
            }

            //same as cost code retrieval
            if (sExpAccount == "" && strGlRetrieveMethod.ToUpper() != "RETRIEVE COST CODE" && strGlRetrieveMethod.ToUpper().Contains(TLSettingsHelper.APDistributionSettingsStatic.Cost_Code_Desc.ToUpper()))
            {
                if (!string.IsNullOrEmpty(sCostCode)) // ERF
                {
                    sExpAccount = GetCostAccount("CostCode", ref sJob, sExtra, ref sCostCode, ref extraB, bIncludePrefix); // ERF switched extraB in for the necessary assignment
                    //add 1/10/2013
                    if (sExpAccount == strDefaultCostAcct)
                        IgnoreRetriveJobPrefixSetting = true;
                    //add 1/10/2013-----------
                }
            }

            returnValue = sExpAccount;
            return returnValue;
        }


        private string GetCostAccount(string sTable, ref string sJob, string sExtra, ref string sCostCode, ref string sCategory, bool bIncludePrfx)
        {
            string returnValue = null;
            string sQry = "";
            string sCostAccount = "";
            string sBaseAcct = "";
            string sGLPrefix = "";
            string sGLAccount = "";
            string sPunct = "";
            string sPrefixLevel = string.Empty;

            //REVISIT-SANKET
            //TimberScanDataAccess.DataAcessHelper tLineData;

            //if (TimberScan.Properties.Extended.Default.TSyncActive)
            //{

            //    tLineData = new TimberScanDataAccess.DataAcessHelper(TimberScan.Properties.Extended.Default.TSyncConnectionString, TimberScanDataAccess.DataAccessType.SqlServer);
            //}
            //else
            //{
            //    tLineData = TLSettingsHelper.TimberlineStandardData;
            //}

            sPunct = "";
            if (objTS_CTL.GLPrefixABCLength > 0)
            {
                sPunct = objTS_CTL.GLPrefixABCPunct;
                sPrefixLevel = "PrefixC";
            }
            else if (objTS_CTL.GLPrefixABLength > 0) // ERF
            {
                sPunct = objTS_CTL.GLPrefixABPunct;
                sPrefixLevel = "PrefixB";
            }
            else if (objTS_CTL.GLPrefixALength > 0) // ERF
            {
                sPunct = objTS_CTL.GLPrefixAPunct;
                sPrefixLevel = "PrefixA";
            }

            sCostAccount = "";

            if (string.IsNullOrEmpty(CommonFunctions.TestNullString(sJob).Trim()))
                return "";

            switch (sTable)
            {
                case "Job":
                    {
                        sQry = "SELECT Cost_Account FROM JCM_MASTER__JOB WHERE Job=" + CommonFunctions.QNApos(sJob);
                        break;
                    }
                case "Category":
                    {
                        if (string.IsNullOrEmpty(CommonFunctions.TestNullString(sCategory).Trim()))
                            return "";

                        if (string.IsNullOrEmpty(CommonFunctions.TestNullString(sCostCode).Trim()))
                        {
                            sBaseAcct = GetDefaultCostAccount("Category", sJob, "", sCategory, bIncludePrfx);
                            return sBaseAcct;
                        }
                        else
                        {
                            sQry = "SELECT Cost_Account FROM JCM_MASTER__CATEGORY WHERE Job=" + CommonFunctions.QNApos(sJob) + " AND Cost_Code=" + CommonFunctions.QNApos(sCostCode) + " AND Category=" + CommonFunctions.QNApos(sCategory);

                            if (!string.IsNullOrEmpty(CommonFunctions.TestNullString(sExtra).Trim())) // ERF
                            {
                                sQry = sQry + " AND Extra=" + CommonFunctions.QNApos(sExtra);
                            }
                            else
                            {
                                sQry = sQry + " AND Extra=\'\'";
                            }
                        }
                        break;
                    }
                case "CostCode":
                    {
                        if (string.IsNullOrEmpty(CommonFunctions.TestNullString(sCostCode).Trim()))
                            return "";

                        sQry = "SELECT Cost_Account FROM JCM_MASTER__COST_CODE WHERE Job=" + CommonFunctions.QNApos(sJob) + " AND Cost_Code=" + CommonFunctions.QNApos(sCostCode);

                        if (!string.IsNullOrEmpty(CommonFunctions.TestNullString(sExtra).Trim())) // ERF
                        {
                            sQry = sQry + " AND Extra=" + CommonFunctions.QNApos(sExtra);
                        }
                        else
                        {
                            sQry = sQry + " AND Extra=\'\'";
                        }
                        break;
                    }
            }

            DataTable reader1 = new DataTable();

            //REVISIT-SANKET
            //if (TimberScan.Properties.Extended.Default.TSyncActive)
            //{
            //    sQry += " AND ConnectionID=" + modSystemProcedures.clsTimberlineConnection.ConnectionID.ToString();
            //}

            APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(reader1, sQry, null, true, false);

            if ((reader1 != null) && (reader1.Rows.Count > 0))
            {
                sCostAccount = reader1.Rows[0]["Cost_Account"].ToString();

                //If sCostAccount is empty 
                // Check for "Use Hierarchy" and the table
                if (string.IsNullOrEmpty(sCostAccount) && (strGlRetrieveMethod).ToUpper() == "USE HIERARCHY" && sTable == "Job")
                {
                    sQry = "SELECT Cost_Account FROM JCM_MASTER__JOB WHERE Job=" + CommonFunctions.QNApos(sJob);
                    DataTable reader2 = new DataTable();

                    //REVISIT-SANKET
                    //if (TimberScan.Properties.Extended.Default.TSyncActive)
                    //{
                    //    sQry += " AND ConnectionID=" + modSystemProcedures.clsTimberlineConnection.ConnectionID.ToString();
                    //}
                    APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(reader2, sQry, null, true, false);

                    if ((reader2 != null) && (reader2.Rows.Count > 0))
                    {
                        sGLAccount = reader2.Rows[0]["Cost_Account"].ToString().Trim();
                        if (!string.IsNullOrEmpty(sGLAccount))
                        {
                            sCostAccount = sGLAccount;
                        }
                        else
                            sCostAccount = strDefaultCostAcct;
                    }
                    else
                        sCostAccount = strDefaultCostAcct;
                }
                else if (string.IsNullOrEmpty(sCostAccount) && (strGlRetrieveMethod).ToUpper() == "USE HIERARCHY" && sTable == "Category")
                {
                    //if table is Category and CostAccount is empty 
                    //step1 search CostAccount based on job and costcode 
                    //step 2 if step1 fails search CostAccount based on job
                    //step 3 if step2 fails return default
                    if (string.IsNullOrEmpty(CommonFunctions.TestNullString(sCostCode).Trim()))
                        return "";

                    sQry = "SELECT Cost_Account FROM JCM_MASTER__COST_CODE WHERE Job=" + CommonFunctions.QNApos(sJob) + " AND Cost_Code=" + CommonFunctions.QNApos(sCostCode);
                    if (!string.IsNullOrEmpty(CommonFunctions.TestNullString(sExtra).Trim())) // ERF
                    {
                        sQry = sQry + " AND Extra=" + CommonFunctions.QNApos(sExtra);
                    }
                    else
                    {
                        sQry = sQry + " AND Extra=\'\'";
                    }
                    DataTable reader3 = new DataTable();

                    //REVISIT
                    //if (TimberScan.Properties.Extended.Default.TSyncActive)
                    //{
                    //    sQry += " AND ConnectionID=" + modSystemProcedures.clsTimberlineConnection.ConnectionID.ToString();
                    //}

                    APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(reader3, sQry, null, true, false);

                    if ((reader3 != null) && (reader3.Rows.Count > 0))
                    {
                        sCostAccount = reader3.Rows[0]["Cost_Account"].ToString();
                    }
                    if (string.IsNullOrEmpty(sCostAccount))
                    {
                        sQry = "SELECT Cost_Account FROM JCM_MASTER__JOB WHERE Job=" + CommonFunctions.QNApos(sJob);
                        DataTable reader2 = new DataTable();

                        //SANKET-REVISIT
                        //if (TimberScan.Properties.Extended.Default.TSyncActive)
                        //{
                        //    sQry += " AND ConnectionID=" + modSystemProcedures.clsTimberlineConnection.ConnectionID.ToString();
                        //}

                        APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(reader2, sQry, null, true, false);

                        if ((reader2 != null) && (reader2.Rows.Count > 0))
                        {
                            sGLAccount = reader2.Rows[0]["Cost_Account"].ToString().Trim();
                            if (!string.IsNullOrEmpty(sGLAccount))
                            {
                                sCostAccount = sGLAccount;
                            }
                            else
                                sCostAccount = strDefaultCostAcct;
                        }
                        else
                            sCostAccount = strDefaultCostAcct;
                    }
                }
                else if (string.IsNullOrEmpty(sCostAccount))
                {
                    if (!string.IsNullOrEmpty(sCostCode))
                        sCostAccount = GetDefaultCostAccount("CostCode", sJob, sCostCode, "", bIncludePrfx);
                    if (string.IsNullOrEmpty(sCostAccount))
                    {
                        sCostAccount = strDefaultCostAcct;
                    }
                }
            }
            else
            {
                if (sTable == "Category")
                {
                    sCostAccount = GetDefaultCostAccount("Category", sJob, "", sCategory, bIncludePrfx);
                }
                else
                {
                    if (sTable == "CostCode" && !string.IsNullOrEmpty(sCostCode))
                    {
                        sCostAccount = GetDefaultCostAccount("CostCode", sJob, sCostCode, "", bIncludePrfx);
                    }
                    else
                    {
                        sCostAccount = strDefaultCostAcct;
                    }
                }
            }

            sGLPrefix = "";
            if (!string.IsNullOrEmpty(sCostAccount))
            {
                if (bIncludePrfx == true && !string.IsNullOrEmpty(sPunct))
                {
                    //if (bolRetrieveAcctPrfxFrmJob == true && !string.IsNullOrEmpty(sJob)) this line had been replaced by the line before //VERY IMPORTANT CHANGE - do not add on prefix when a full account is pulled - the case happens when the account and prefix are in separate columns
                    if (bolRetrieveAcctPrfxFrmJob == true && !string.IsNullOrEmpty(sJob) && !(TLSettingsHelper.APDistributionSettingsStatic.GL_Prefix && sCostAccount == strDefaultCostAcct))
                    {
                        sQry = "SELECT Cost_Account_Prefix, Cost_Account FROM JCM_MASTER__JOB WHERE Job=" + CommonFunctions.QNApos(sJob);

                        //REVISIT
                        //if (TimberScan.Properties.Extended.Default.TSyncActive)
                        //{
                        //    sQry += " AND ConnectionID = " + modSystemProcedures.clsTimberlineConnection.ConnectionID.ToString();
                        //}

                        DataTable reader3 = new DataTable();
                        APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(reader3, sQry, null, true, false);

                        if ((reader3 != null) && (reader3.Rows.Count > 0))
                        {
                            sGLPrefix = reader3.Rows[0]["Cost_Account_Prefix"].ToString();
                        }
                        else
                        {
                            sGLPrefix = string.Empty;
                        }
                    }
                    else
                    {
                        //VERY IMPORTANT CHANGE! - NOT USING BASE ACCOUNT
                        //The following 2 statements were commented out because if the prefixes are not retrieved, still the account should be returned full, NOT just a base account!!!!!
                        //sGLPrefix = objTS_CTL.DetermineGLPrefix(sCostAccount, sPrefixLevel);
                        //sCostAccount = objTS_CTL.DetermineBaseAccount(sCostAccount, true);
                        //VERY IMPORTANT CHANGE!         //Irene 11/8/2012
                    }

                    if (!string.IsNullOrEmpty(sGLPrefix))
                    {
                        //1/14/2013 addidion
                        returnValue = sCostAccount;
                        string PrefPlusPunct = sGLPrefix + sPunct;
                        if (!sCostAccount.Contains(PrefPlusPunct))
                        {
                            if (TLSettingsHelper.APDistributionSettingsStatic.GL_Prefix == false && strDefaultCostAcct == sCostAccount)
                            {
                                sCostAccount = objTS_CTL.DetermineBaseAccount(sCostAccount, true);
                            }
                            //1/14/2013 addidion ---------------------
                            returnValue = sGLPrefix + sPunct + sCostAccount;
                        }//1/14/2013 addidion
                    }
                    else
                    {
                        returnValue = sCostAccount;
                    }
                }
                else
                {
                    //VERY IMPORTANT CHANGE! - NOT USING BASE ACCOUNT
                    //The following statement was commented out and the one after added because if the prefixes are not retrieved, still the account should be returned full, NOT just a base account!!!!!
                    //    returnValue = objTS_CTL.DetermineBaseAccount(sCostAccount , true);
                    returnValue = sCostAccount;
                    //VERY IMPORTANT CHANGE!         //Irene 11/8/2012
                }
            }
            else
            {
                returnValue = "";
            }
            return returnValue;
        }

        private string GetDefaultCostAccount(string sTable, string sJob, string sCostCode, string sCategory, bool includePrefix)
        {
            string sQry = "";
            DataTable aRs = null;
            string sGLAccount;
            string sCostAcctGrp;
            //TimberScanDataAccess.DataAcessHelper tLineData;
            bool CostAcctFromStandardCategory = false;


            if (string.IsNullOrEmpty(sJob))
                return string.Empty;

            sQry = "SELECT Cost_Account_Group FROM JCM_MASTER__JOB WHERE Job=" + CommonFunctions.QNApos(sJob);

            //REVISIT -SANKET
            //if (TimberScan.Properties.Extended.Default.TSyncActive)
            //{
            //    sQry += " AND ConnectionId = " + modSystemProcedures.clsTimberlineConnection.ConnectionID.ToString();
            //    tLineData = new TimberScanDataAccess.DataAcessHelper(TimberScan.Properties.Extended.Default.TimberSyncConnectionString, TimberScanDataAccess.DataAccessType.SqlServer);
            //}
            //else
            //{
            //    tLineData = TLSettingsHelper.TimberlineStandardData;
            //}

            DataTable reader1 = new DataTable();

            APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(reader1, sQry, null, true, false);

            if ((reader1 != null) && (reader1.Rows.Count > 0))
            {
                sCostAcctGrp = reader1.Rows[0]["Cost_Account_Group"].ToString();
                if (string.IsNullOrEmpty(sCostAcctGrp))
                    return string.Empty;
            }
            else
            {
                return string.Empty;
            }

            if (sTable == "Category")
            {
                sQry = "SELECT Cost_Account_" + Strings.Right(sCostAcctGrp, 1) + " FROM JCM_MASTER__STANDARD_CATEGORY WHERE Category=" + CommonFunctions.QNApos(sCategory);
                CostAcctFromStandardCategory = true;
            }
            else
            {
                sQry = "SELECT Cost_Account_" + Strings.Right(sCostAcctGrp, 1) + " FROM JCM_MASTER__STANDARD_COST_CODE WHERE Cost_Code=" + CommonFunctions.QNApos(sCostCode);
            }

            //REVISIT
            //if (TimberScan.Properties.Extended.Default.TSyncActive)
            //{
            //    sQry += " AND ConnectionID = " + modSystemProcedures.clsTimberlineConnection.ConnectionID.ToString();
            //}

            DataTable reader2 = new DataTable();
            APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(reader2, sQry, null, true, false);

            if ((reader2 != null) && (reader2.Rows.Count > 0))
            {
                sGLAccount = reader2.Rows[0][0].ToString();
                if (!string.IsNullOrEmpty(sGLAccount))
                {
                    if (includePrefix || CostAcctFromStandardCategory)
                    {
                        sGLAccount = sGLAccount;
                    }
                    else
                    {
                        sGLAccount = this.GetBaseAccount(sGLAccount);
                    }
                    return sGLAccount;
                }
            }
            return string.Empty;
        }

        public string GetBaseAccount(string sAcct)
        {
            string returnValue = null;
            //sAcct is full account including GL Prefix (GL Prefix not mandatory)
            //returns Base Account& Suffix
            //sPrefix can either be PrefixA, PrefixB or PrefixC
            int iCnt; // ERF short to int
            int iLoop; // ERF short to int
            int iPos; // ERF shor to int
            string sBase;
            string sChr;
            string sSuffix;

            if (string.IsNullOrEmpty(sAcct))
            {
                return string.Empty;
            }

            if (TLSettingsHelper.GLMasterDescriptions.PrefixALength == 0)
            {
                return sAcct;
            }

            sSuffix = "";

            iCnt = 0;
            if (TLSettingsHelper.GLMasterDescriptions.SuffixLength > 0)
            {
                for (iPos = sAcct.Length - 1; iPos >= 0; iPos--)
                {
                    sChr = sAcct[iPos].ToString();
                    //if a character is not alphanumeric, then it must be an account separator
                    if (!char.IsLetterOrDigit(sChr, 0)) // ERF
                    {

                        iCnt = iPos;
                        break;
                    }
                }
                if (iCnt > 0)
                {
                    sSuffix = "." + sAcct.Substring(iCnt + 1);
                    sBase = sAcct.Substring(0, iCnt);
                }
                else
                {
                    sBase = sAcct;
                }
            }
            else
            {
                sBase = sAcct;
            }

            iCnt = 0;
            int testInt = 0;

            for (int i = sBase.Length - 1; i >= 0; --i)
            {
                sChr = sBase[i].ToString();
                //if ( ((char.IsUpper(sChr,0) && char.IsLetter(sChr,0)) || int.TryParse(sChr,out testInt) || sChr == " ") )
                if (!char.IsLetterOrDigit(sChr, 0))
                {
                    iCnt = i;
                    break;
                }
            }

            if (iCnt > 0)
            {

                sBase = sBase.Substring(iCnt + 1);
            }
            returnValue = sBase + sSuffix;
            return returnValue;
        }

        internal bool EquipmentMemoCost(string sEqCstCd)
        {

            string sQry;
            DataTable aRs = new DataTable();
            //TimberScanDataAccess.DataAcessHelper tLineData = TLSettingsHelper.TimberlineStandardData;

            bool bMemoCost;

            try
            {

                sQry = "SELECT Memo_Cost FROM EQS_STANDARD__STANDARD_COST_CODE WHERE Cost_Code=" + CommonFunctions.QNApos(sEqCstCd);

                //REVISIT
                //if (TimberScan.Properties.Extended.Default.TSyncActive)
                //{
                //    sQry += " AND ConnectionID=" + modSystemProcedures.clsTimberlineConnection.ConnectionID.ToString();
                //    tLineData = new TimberScanDataAccess.DataAcessHelper(TimberScan.Properties.Extended.Default.TSyncConnectionString, TimberScanDataAccess.DataAccessType.SqlServer);
                //}


                APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(aRs, sQry, null, true, false);

                if (aRs == null || aRs.Rows.Count <= 0)
                {
                    bMemoCost = false;
                }
                else
                {
                    bMemoCost = Convert.ToBoolean(aRs.Rows[0]["Memo_Cost"].ToString().Trim());
                }


            }
            catch
            {
                throw;
            }
            return bMemoCost;
        }



        internal string DeterminePayableAccount(string expenseAccount, string commitment)
        {
            //Commitment_Payable_Acct
            //PO_Payable_Account
            //if(po_payable_account==string.empty)-->po_payable_account = payableacct
            string glAcct = string.Empty;
            string glPrefix = string.Empty;
            string sql = string.Empty;
            DataTable dt = null;
            
            //TimberScanDataAccess.DataAcessHelper tLineData = null;

            if (string.IsNullOrWhiteSpace(commitment))
            {
                glAcct = TLSettingsHelper.APDistributionSettingsStatic.APM_MASTER__AP_Controls.Rows[0]["AP_Payable_Account"].ToString();
            }
            else
            {
                sql = "select Commitment_Type from JCM_MASTER__COMMITMENT WHERE Commitment = " + CommonFunctions.QNApos(commitment);


                //REVISIT
                //if (TimberScan.Properties.Extended.Default.TSyncActive)
                //{
                //    sql += " AND ConnectionId=" + modSystemProcedures.clsTimberlineConnection.ConnectionID;
                //    tLineData = new TimberScanDataAccess.DataAcessHelper(TimberScan.Properties.Extended.Default.TimberSyncConnectionString, TimberScanDataAccess.DataAccessType.SqlServer);
                //}
                //else
                //{
                //    tLineData = TLSettingsHelper.TimberlineStandardData;
                //}

                dt = new DataTable();
                APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(dt, sql, null, true, false);

                if (dt == null || dt.Rows.Count <= 0)
                {
                    if (!string.IsNullOrWhiteSpace(TLSettingsHelper.APDistributionSettingsStatic.APM_MASTER__AP_Controls.Rows[0]["Commitment_Payable_Acct"].ToString()))
                    {
                        glAcct = TLSettingsHelper.APDistributionSettingsStatic.APM_MASTER__AP_Controls.Rows[0]["Commitment_Payable_Acct"].ToString();
                    }
                    else if (!string.IsNullOrWhiteSpace(TLSettingsHelper.APDistributionSettingsStatic.APM_MASTER__AP_Controls.Rows[0]["PO_Payable_Account"].ToString()))
                    {
                        glAcct = TLSettingsHelper.APDistributionSettingsStatic.APM_MASTER__AP_Controls.Rows[0]["PO_Payable_Account"].ToString();
                    }
                    else
                    {
                        glAcct = TLSettingsHelper.APDistributionSettingsStatic.APM_MASTER__AP_Controls.Rows[0]["AP_Payable_Account"].ToString();
                    }
                }
                else
                {

                    if (dt.Rows[0]["Commitment_Type"].ToString().ToUpper() == "SUBCONTRACT")
                    {
                        glAcct = glAcct = TLSettingsHelper.APDistributionSettingsStatic.APM_MASTER__AP_Controls.Rows[0]["Commitment_Payable_Acct"].ToString();
                    }
                    else
                    {
                        glAcct = TLSettingsHelper.APDistributionSettingsStatic.APM_MASTER__AP_Controls.Rows[0]["PO_Payable_Account"].ToString();
                    }

                    if (string.IsNullOrWhiteSpace(glAcct))
                    {
                        glAcct = TLSettingsHelper.APDistributionSettingsStatic.APM_MASTER__AP_Controls.Rows[0]["AP_Payable_Account"].ToString();
                    }

                }
            }

            if (!string.IsNullOrWhiteSpace(glAcct))
            {
                glPrefix = DetermineAPDistGLPrefix(expenseAccount);
                if (!string.IsNullOrWhiteSpace(glPrefix))
                {
                    glAcct = glPrefix + DeterminePunctuation() + glAcct;
                }
            }

            return glAcct;
        }

        public string DetermineAPDistGLPrefixFromExpenseAccount(string expenseAccount)
        {
            //return DetermineAPDistGLPrefix(expenseAccount);
            return TLSettingsHelper.TS_CTL.DetermineGLPrefix(expenseAccount, "", false);
        }

        private string DetermineAPDistGLPrefix(string expenseAccount)
        {
            string entity = string.Empty;
            string balLvl = string.Empty;
            string apPrefix = string.Empty;
            clsGLRoutines glRoutines = new clsGLRoutines();

            if (TLSettingsHelper.TS_CTL.GLPrefixALength > 0)
            {

                entity = TLSettingsHelper.TS_CTL.DetermineGLPrefix(expenseAccount, "PrefixA", true);

                if (!string.IsNullOrWhiteSpace(entity))
                {
                    balLvl = glRoutines.DetermineBalanceSheetLevel(entity);

                    if (balLvl == TLSettingsHelper.TS_CTL.GLPrefixADesc)
                    {
                        if (objTS_CTL.GLPrefixABCLength > 0)
                        {
                            apPrefix = entity + TLSettingsHelper.TS_CTL.GLPrefixAPunct + "".PadLeft(TLSettingsHelper.TS_CTL.GLPrefixABLength, '0') + TLSettingsHelper.TS_CTL.GLPrefixABPunct + "".PadLeft(TLSettingsHelper.TS_CTL.GLPrefixABCLength, '0');
                        }
                        else if (objTS_CTL.GLPrefixABLength > 0)
                        {
                            apPrefix = entity + TLSettingsHelper.TS_CTL.GLPrefixAPunct + "".PadLeft(TLSettingsHelper.TS_CTL.GLPrefixABLength, '0');
                        }
                        else
                        {
                            apPrefix = entity;
                        }
                    }
                    else if (balLvl == TLSettingsHelper.TS_CTL.GLPrefixABDesc)
                    {

                        entity = objTS_CTL.DetermineGLPrefix(expenseAccount, "PrefixB", true);

                        if (TLSettingsHelper.TS_CTL.GLPrefixABCLength > 0)
                        {
                            apPrefix = entity + TLSettingsHelper.TS_CTL.GLPrefixABPunct + "".PadLeft(TLSettingsHelper.TS_CTL.GLPrefixABCLength, '0');
                        }
                        else
                        {
                            apPrefix = entity;
                        }


                    }
                    else
                    {
                        apPrefix = entity;
                    }
                }

            }


            return apPrefix;
        }

        private string DeterminePunctuation()
        {
            string punctuation = string.Empty;

            if (TLSettingsHelper.TS_CTL.GLPrefixABCLength > 0)
            {
                punctuation = TLSettingsHelper.TS_CTL.GLPrefixABCPunct;
            }
            else if (TLSettingsHelper.TS_CTL.GLPrefixABLength > 0)
            {
                punctuation = TLSettingsHelper.TS_CTL.GLPrefixABPunct;
            }
            else if (TLSettingsHelper.TS_CTL.GLPrefixALength > 0)
            {
                punctuation = TLSettingsHelper.TS_CTL.GLPrefixAPunct;
            }


            return punctuation;
        }
    }
}
