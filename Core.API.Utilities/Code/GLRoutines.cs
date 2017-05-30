using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Core.API.Utilities.Code
{
    internal class clsGLRoutines
    {
        string strMsg;
        string mvarPrefixADesc;
        string mvarPrefixABDesc;
        string mvarPrefixABCDesc;
        short mvarPrefixALength;
        short mvarPrefixABLength;
        short mvarPrefixABCLength;
        string mvarSepChar1;
        string mvarSepChar2;
        string mvarSepChar3;
        string mvarSepChar4;
        short mvarBaseAcctLength;
        short mvarSuffixLength;
        short mvarLevels;

        private void Class_Initialize_Renamed()
        {
            mvarPrefixADesc = "";

            mvarPrefixABDesc = "";
            mvarPrefixABCDesc = "";
            mvarPrefixALength = 0;

            mvarPrefixABLength = 0;
            mvarPrefixABCLength = 0;
            mvarSepChar1 = "";

            mvarSepChar2 = "";
            mvarSepChar3 = "";
            mvarSepChar4 = "";
            mvarBaseAcctLength = 0;

            mvarSuffixLength = 0;
            mvarLevels = 0;
        }
        public clsGLRoutines()
        {
            Class_Initialize_Renamed();
        }

        public string DetermineBalanceSheetLevel(string sPrefixA)
        {
            // TODO: WB - change DataTable to ODBCDataReader

            string returnValue = null;
            string sBalLevel;
            string sFiscEntLvl;
            string sQry = "";
            string sErrDesc;
            DataTable aRs = new DataTable();
            TimberScanDataAccess.DataAcessHelper tLineData = modImage.TimberlineStandardData;/*A new timberline dataaccesshelper should almost never be created. use the exising from modImage.
                                                                                              * new TimberScanDataAccess.DataAcessHelper(modImage.SystemConnections.TimberlineStdConnectionString,
                 TimberScanDataAccess.DataAccessType.Odbc);*/

            int lErrNo = 0;

            //sPrefixA = Me.GetEntityIDFromPrefix(sAcct, "PrefixA")

            //modIniFiles.LogFile"DetermineBalanceSheetLevel sPrefixA=" & sPrefixA

            sQry = "SELECT Balance_Sheet_Level, Period_End_1__Current FROM GLM_MASTER__FISCAL_CONTROLS";
            //aRs = Main.OdbcDR(sQry, TimberScan.Properties.Extended.Default.TimberlineConnectionString);
            tLineData.FillDataTable(aRs, sQry, null, true, false);

            if (aRs != null && aRs.Rows.Count > 0 && Information.IsDate(aRs.Rows[0]["Period_End_1__Current"]) == true)
            {
                returnValue = System.Convert.ToString(aRs.Rows[0]["Balance_Sheet_Level"]); // JBR to string
                //modIniFiles.LogFile"Determine Balance Sheet Level from GLM_MASTER__FISCAL_CONTROLS = " & aRs("Balance_Sheet_Level")
                aRs = null;
                return returnValue;
            }

            if (modImage.GLMasterDescriptions.Levels == 1)
            {
                return modImage.GLMasterDescriptions.PrefixADesc;
            }

            sQry = "SELECT Fiscal_Entity_Level, Balance_Sheet_Level FROM GLM_MASTER__ACCOUNT_PREFIX_A WHERE Account_Prefix_A=" + CommonFunctions.QNApos(sPrefixA);
            aRs = new DataTable();
            tLineData.FillDataTable(aRs, sQry, null, true, false);

            sFiscEntLvl = System.Convert.ToString(aRs != null && aRs.Rows.Count > 0 ? aRs.Rows[0]["Fiscal_Entity_Level"] : string.Empty); // JBR to string

            //modIniFiles.LogFile"Fiscal Entity Level=" & sFiscEntLvl
            //modIniFiles.LogFile"GLMasterDescriptions.PrefixADesc=" & GLMasterDescriptions.PrefixADesc
            //modIniFiles.LogFile"GLMasterDescriptions.PrefixABDesc=" & GLMasterDescriptions.PrefixABDesc
            //modIniFiles.LogFile"GLMasterDescriptions.PrefixABCDesc=" & GLMasterDescriptions.PrefixABCDesc
            if (sFiscEntLvl.ToUpper() == modImage.GLMasterDescriptions.PrefixADesc.ToUpper())
            {
                returnValue = System.Convert.ToString(aRs.Rows[0]["Balance_Sheet_Level"]); // JBR
                aRs = null;
                return returnValue;
                //Exit Function
            }

            if (sFiscEntLvl.ToUpper() == modImage.GLMasterDescriptions.PrefixABDesc.ToUpper())
            {
                sQry = "SELECT COUNT(Account_Prefix_AB) AS TotRecs FROM GLM_MASTER__ACCOUNT_PREFIX_AB WHERE Alternate_Account_Prefix_A=" + CommonFunctions.QNApos(sPrefixA);
            }
            else
            {
                sQry = "SELECT COUNT(Account_Prefix_ABC) AS TotRecs FROM GLM_MASTER__ACCOUNT_PREFIX_ABC WHERE Alternate_Account_Prefix_A=" + CommonFunctions.QNApos(sPrefixA);
            }
            //On Error Resume Next
            aRs = new DataTable();
            tLineData.FillDataTable(aRs, sQry, null, true, false);


            if (sFiscEntLvl.ToUpper() == modImage.GLMasterDescriptions.PrefixABDesc.ToUpper())
            {
                sQry = "SELECT Balance_Sheet_Level FROM GLM_MASTER__ACCOUNT_PREFIX_AB WHERE Alternate_Account_Prefix_A=" + CommonFunctions.QNApos(sPrefixA);
            }
            else
            {
                sQry = "SELECT Balance_Sheet_Level FROM GLM_MASTER__ACCOUNT_PREFIX_ABC WHERE Alternate_Account_Prefix_A=" + CommonFunctions.QNApos(sPrefixA);
            }

            sBalLevel = "";

            //On Error Resume Next
            aRs = new DataTable();// Main.OdbcDR(sQry, TimberScan.Properties.Extended.Default.TimberlineConnectionString);
            tLineData.FillDataTable(aRs, sQry, null, true, false);

            if (aRs != null && aRs.Rows.Count > 0)
            {
                //On Error Resume Next
                sBalLevel = System.Convert.ToString(aRs.Rows[0]["Balance_Sheet_Level"]); // JBR to string
            }
            returnValue = sBalLevel;
            return returnValue;
        }

        public string GetEntityIDFromPrefix(string sAcct, string sPrefix)
        {
            string returnValue = string.Empty;
            //sPrefix can either be PrefixA, PrefixB or PrefixC
            short iLoop;
            short iCnt;
            short iNum;
            string sChr;
            string sEntID;

            if (!string.IsNullOrEmpty(sAcct))
            {

                if (sPrefix == "PrefixA")
                {
                    iLoop = 1;
                }
                else if (sPrefix == "PrefixB")
                {
                    iLoop = 2;
                }
                else
                {
                    iLoop = 3;
                }

                iCnt = 0;
                sEntID = "";

                for (iNum = 0; iNum < sAcct.Length; iNum++)
                {
                    sChr = sAcct.Substring(iNum, 1).ToUpper();
                    //if a character is not alphanumeric, then it must be an account separator
                    if (!Information.IsNumeric(sChr) && (Convert.ToChar(sChr) < 'A' || Convert.ToChar(sChr) > 'Z'))
                    {
                        iCnt++;
                        if (iCnt == iLoop)
                        {
                            break;
                        }
                        else
                        {
                            sEntID = sEntID + sChr;
                        }
                    }
                    else
                    {
                        sEntID = sEntID + sChr;
                    }
                }
                returnValue = sEntID;
            }

            return returnValue;

        }

        public string GetBaseAccountFromPrefix(string sAcct)
        {
            string returnValue = null;
            //sAcct is full account including GL Prefix (GL Prefix not mandatory)
            //returns Base Account& Suffix
            //sPrefix can either be PrefixA, PrefixB or PrefixC
            short iCnt;
            short iLoop;
            short iPos;
            string sBase;
            string sChr;
            string sSuffix;

            sSuffix = "";

            iCnt = 0;
            if (modImage.GLMasterDescriptions.SuffixLength > 0)
            {
                for (iPos = (short)sAcct.Length; iPos >= 1; iPos--) // JBR
                {
                    sChr = sAcct.Substring(iPos - 1, 1).ToUpper();
                    //if a character is not alphanumeric, then it must be an account separator
                    // original: if (! Information.IsNumeric(sChr) && (sChr < "A" || sChr > "Z"))
                    if (!Information.IsNumeric(sChr) && (System.Convert.ToChar(sChr) < 'A' || System.Convert.ToChar(sChr) > 'Z')) // JBR
                    {
                        iCnt = iPos;
                        //Exit For
                    }
                }
                if (iCnt > 0)
                {
                    sSuffix = "." + sAcct.Substring(iCnt + 1 - 1);
                    sBase = sAcct.Substring(0, iCnt - 1);
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
            for (iPos = (short)sBase.Length; iPos >= 1; iPos--) // JBR to short
            {
                sChr = sBase.Substring(iPos - 1, 1).ToUpper();
                //if a character is not alphanumeric, then it must be an account separator
                // original: if (! Information.IsNumeric(sChr) && (sChr < "A" || sChr > "Z"))
                if (!Information.IsNumeric(sChr) && (System.Convert.ToChar(sChr) < 'A' || System.Convert.ToChar(sChr) > 'Z')) // JBR
                {
                    iCnt = iPos;
                    //Exit For
                }
            }

            if (iCnt > 0)
            {
                sBase = sBase.Substring(iCnt + 1 - 1);
            }
            returnValue = sBase + sSuffix;
            return returnValue;
        }

        public string GetEntityDescription(string sEntity)
        {
            // TODO: WB - change DataTable to ODBCDataReader

            string returnValue = null;
            string sQry = ""; // JBR
            OdbcDataReader adoRs;

            //If sEntity = "" Then GetEntityDescription = "" : Exit Function

            switch (TimberScan.Properties.Basic.Default.GroupPrefix)
            {
                case "PrefixA":
                    sQry = "SELECT PADESC AS AcctDesc FROM MASTER_GLM_RECORD_3" + " WHERE PAPRFX=" + CommonFunctions.QNApos(sEntity);
                    break;
                case "PrefixB":
                    sQry = "SELECT PBDESC AS AcctDesc FROM MASTER_GLM_RECORD_4" + " WHERE PBPRFX=" + CommonFunctions.QNApos(sEntity);
                    break;
                case "PrefixC":
                    sQry = "SELECT PCDESC AS AcctDesc FROM MASTER_GLM_RECORD_5" + " WHERE PCPRFX=" + CommonFunctions.QNApos(sEntity);
                    break;
            }
            TimberScanDataAccess.DataAcessHelper timberlineData = modImage.TimberlineDictionaryData;//new TimberScanDataAccess.DataAcessHelper(
            //    modImage.SystemConnections.TimberlineDictConnectionString, TimberScanDataAccess.DataAccessType.Odbc);
            DataTable dt = new DataTable();

            modIniFiles.LogFile("Querying timberline");
            timberlineData.FillDataTable(dt, sQry, null, false, true);
            modIniFiles.LogFile("Querying timberline done");

            if (dt != null && dt.Rows.Count > 0)
                returnValue = dt.Rows[0]["AcctDesc"].ToString();

            ////adoRs = Main.OdbcDR(sQry, TimberScan.Properties.Extended.Default.TimberlineConnectionString);
            //if (adoRs.Read() == false)
            //{
            //    returnValue = "";
            //}
            //else
            //{
            //    returnValue = System.Convert.ToString(adoRs["AcctDesc"]);
            //}
            //adoRs = null;
            return returnValue;
        }
    }
}
