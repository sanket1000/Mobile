using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Core.API.Utilities.Helpers;
using Microsoft.VisualBasic;
namespace Core.API.TLSettings.Models
{
    public class ActualTSCTL
    {
        int mvarGLPrefixALength;
        string mvarGLPrefixADesc;
        string mvarGLPrefixAPunct;
        int mvarGLPrefixABLength;
        string mvarGLPrefixABDesc;
        string mvarGLPrefixABPunct;
        int mvarGLPrefixABCLength;
        string mvarGLPrefixABCDesc;
        string mvarGLPrefixABCPunct;
        int mvarGLBaseLength;
        string mvarGLBasePunct;
        int mvarGLSuffixLength;

        string mvarVendorType1;
        string mvarVendorType2;
        string mvarVendorType3;
        string mvarVendorType4;
        string mvarVendorType5;

        int mvarJobSec1Length;
        string mvarJobSec1Desc;
        string mvarJobSec1Punct;
        int mvarJobSec2Length;
        string mvarJobSec2Desc;
        string mvarJobSec2Punct;
        int mvarJobSec3Length;
        string mvarJobSec3Desc;

        int mvarCostCodeSec1Length;
        string mvarCostCodeSec1Desc;
        string mvarCostCodeSec1Punct;
        int mvarCostCodeSec2Length;
        string mvarCostCodeSec2Desc;
        string mvarCostCodeSec2Punct;
        int mvarCostCodeSec3Length;
        string mvarCostCodeSec3Desc;
        string mvarCostCodeSec3Punct;
        int mvarCostCodeSec4Length;
        string mvarCostCodeSec4Desc;

        int mvarEQSec1Length;
        string mvarEQSec1Desc;
        string mvarEQSec1Punct;
        int mvarEQSec2Length;
        string mvarEQSec2Desc;
        string mvarEQSec2Punct;
        int mvarEQSec3Length;
        string mvarEQSec3Desc;

        int mvarEQCostCodeSec1Length;
        string mvarEQCostCodeSec1Desc;
        string mvarEQCostCodeSec1Punct;
        int mvarEQCostCodeSec2Length;
        string mvarEQCostCodeSec2Desc;
        string mvarEQCostCodeSec2Punct;
        int mvarEQCostCodeSec3Length;
        string mvarEQCostCodeSec3Desc;
        string mvarEQCostCodeSec3Punct;
        int mvarEQCostCodeSec4Length;
        string mvarEQCostCodeSec4Desc;

        string strJobFormat;

        public string JobFormat
        {
            get
            {
                return strJobFormat;
            }
        }

        /*string mvarEquipCodeCodeField = string.Empty;
        public string EquipCodeCodeField
        {
            get
            {
                return modImage.APD
            }
        }

        private string DetermineEquipCostCodeField()
        {
            DataTable dt = new DataTable();
            TimberScanDataAccess.DataAcessHelper tLineData = modImage.TimberlineStandardData;
            string fld = string.Empty;

            try
            {

                tLineData.FillDataTable(dt , "Select * from APM_MASTER__DISTRIBUTION where Dist_Seq<0" , null , true , false);

                foreach (DataColumn dc in dt.Columns)
                {

                    if (dc.ColumnName.Length > 10)
                    {
                        if (dc.ColumnName.Substring(0 , 10).ToUpper() == "COST_CODE_")
                        {
                            fld = dc.ColumnName;
                            break;
                        }
                    }
                }
            }
            catch
            {
                throw;
            }

            return fld;
        }*/

        string strCostCodeFormat;
        public string CostCodeFormat
        {
            get
            {
                return this.strCostCodeFormat;
            }
        }


        string strEQFormat;
        public string EQFormat
        {
            get
            {
                return this.strEQFormat;
            }
        }


        string strEQCostCodeFormat;
        public string EQCostCodeFormat
        {
            get
            {
                return this.strEQCostCodeFormat;
            }
        }

        string strGLFullAccountFormat;
        public string GLFullAccountFormat
        {
            get
            {
                return this.strGLFullAccountFormat;
            }
        }

        string strGLBaseAccountFormat;
        public string GLBaseAccountFormat
        {
            get
            {
                return this.strGLBaseAccountFormat;
            }
        }

        string strGLPrefixFormat;
        public string GLPrefixFormat
        {
            get
            {
                return this.strGLPrefixFormat;
            }
        }

        int intJobSections;
        int intCostCodeSections;
        int intEQSections;
        int intEQCostCodeSections;
        int intGLSections;
        int intGLPrefixCnt;

        string _CommitmentDesc = string.Empty;
        public string CommitmentDesc
        {
            get
            {
                return _CommitmentDesc;
            }
        }

        int _CommitmentLen = 0;
        public int CommitmentLen
        {
            get
            {
                return _CommitmentLen;
            }
        }

        bool _CommitmentCaps = false;
        public bool CommitmentCaps
        {
            get
            {
                return _CommitmentCaps;
            }
        }

        bool _VendorCaps = false;
        public bool VendorCaps
        {
            get
            {
                return _VendorCaps;
            }
        }

        bool _InvoiceCaps = false;
        public bool InvoiceCaps
        {
            get
            {
                return _InvoiceCaps;
            }
        }

        bool _AuthorizationCaps = false;
        public bool AuthorizationCaps
        {
            get
            {
                return _AuthorizationCaps;
            }
        }

        bool mvarNoSuchTableError;
        bool mvarStandardCostCodeTable;

        int[] intSecLen = new int[5];
        string[] strSecPunct = new string[4];
        //string strTLVersion;

        int GLMasterPrefixALength;
        int GLMasterPrefixABLength;
        int GLMasterPrefixABCLength;
        int GLMasterBaseAcctLength;
        int GLMasterSuffixLength;
        int GLMasterLevels;
        string GLMasterPrefixADesc;
        string GLMasterPrefixABDesc;
        string GLMasterPrefixABCDesc;
        public string GLPrefixType;

        public string strJobIn = "";
        public string strPrefixIn = "";

        private void Class_Initialize_Renamed(int ConnectionID)
        {
            //modImage.WriteInitStaticClassMessage(this.ToString(), "Initializing actualTS_CTL");
            string[] sTlVer;

            mvarNoSuchTableError = false;
            mvarGLPrefixALength = 0;
            mvarGLPrefixADesc = "";
            mvarGLPrefixAPunct = "";
            mvarGLPrefixABLength = 0;
            mvarGLPrefixABDesc = "";
            mvarGLPrefixABPunct = "";
            mvarGLPrefixABCLength = 0;
            mvarGLPrefixABCDesc = "";
            mvarGLPrefixABCPunct = "";
            mvarGLBaseLength = 0;
            mvarGLBasePunct = "";
            mvarGLSuffixLength = 0;

            mvarVendorType1 = "vendor type 1";
            mvarVendorType2 = "vendor type 2";
            mvarVendorType3 = "vendor type 3";
            mvarVendorType4 = "summary";
            mvarVendorType5 = "vendor type 4";

            mvarJobSec1Length = 0;
            mvarJobSec1Desc = "";
            mvarJobSec1Punct = "";
            mvarJobSec2Length = 0;
            mvarJobSec2Desc = "";
            mvarJobSec2Punct = "";
            mvarJobSec3Length = 0;
            mvarJobSec3Desc = "";

            mvarCostCodeSec1Length = 0;
            mvarCostCodeSec1Desc = "";
            mvarCostCodeSec1Punct = "";
            mvarCostCodeSec2Length = 0;
            mvarCostCodeSec2Desc = "";
            mvarCostCodeSec2Punct = "";
            mvarCostCodeSec3Length = 0;
            mvarCostCodeSec3Desc = "";
            mvarCostCodeSec3Punct = "";
            mvarCostCodeSec4Length = 0;
            mvarCostCodeSec4Desc = "";

            mvarEQSec1Length = 0;
            mvarEQSec1Desc = "";
            mvarEQSec1Punct = "";
            mvarEQSec2Length = 0;
            mvarEQSec2Desc = "";
            mvarEQSec2Punct = "";

            intJobSections = 0;
            intCostCodeSections = 0;
            intEQSections = 0;
            intEQCostCodeSections = 0;
            intGLSections = 0;


            //GLMasterPrefixALength = "";
            //GLMasterPrefixABLength = "";
            //GLMasterPrefixABCLength = "";
            //GLMasterBaseAcctLength = "";
            //GLMasterSuffixLength = "";
            //GLMasterLevels = "";
            GLMasterPrefixADesc = "";
            GLMasterPrefixABDesc = "";
            GLMasterPrefixABCDesc = "";
            GLPrefixType = "";
            //sTlVer = modImage.strTimberlineVersion.Split('.');
            //strTLVersion = Strings.Format(Conversion.Val(sTlVer[0]), "00000") + "." + Strings.Format(Conversion.Val(sTlVer[1]), "00000") + "." + Strings.Format(Conversion.Val(sTlVer[2]), "00000");

            DataTable dtAllSettings = new DataTable();
            DataRow[] specficSettingsRows = null;


            //modImage.WriteInitStaticClassMessage(this.ToString(), "querying TS_CTL_RECORD_3. SQL = SELECT Section,CSLEN,CNDESC,CNPUNCT,FLDCODE,CNTCAP FROM TS_CTL_RECORD_3 WHERE FLDCODE in (9,16,18,69,187,20,50,51,52,500) ORDER BY FLDCODE");
            //APIUtilDataAccessHelper.TimberlineDictData.FillDataTable(dtAllSettings, "SELECT Section,CSLEN,CNDESC,CNPUNCT,FLDCODE,CNTCAP FROM TS_CTL_RECORD_3 WHERE FLDCODE in (9,16,18,69,187,20,50,51,52,500) ORDER BY FLDCODE", null, true, false);
            APIUtilDataAccessHelper.TimberSynchDataDynamic(ConnectionID).FillDataTable(dtAllSettings, "SELECT Section,CSLEN,CNDESC,CNPUNCT,FLDCODE,CNTCAP FROM TS_CTL_RECORD_3 WHERE FLDCODE in (9,16,18,69,187,20,50,51,52,500) ORDER BY FLDCODE", null, true, false);
            //modImage.WriteInitStaticClassMessage(this.ToString(), "querying TS_CTL_RECORD_3 done");

            //modImage.WriteInitStaticClassMessage(this.ToString(), "Getting GL Settings");
            specficSettingsRows = dtAllSettings.Select("FLDCODE = 9", "Section");
            mvarNoSuchTableError = !GetGLSettings(specficSettingsRows);
            //modImage.WriteInitStaticClassMessage(this.ToString(), "Getting GL Settings done");

            if (mvarNoSuchTableError == false)
            {

                //These are the rows for the vendor types
                specficSettingsRows = dtAllSettings.Select("FLDCODE In (50,51,52,500)");

                if (specficSettingsRows != null && specficSettingsRows.Length > 0)
                {
                    //modImage.WriteInitStaticClassMessage(this.ToString(), "Getting vendor types");
                    GetVendorTypes(specficSettingsRows);
                    //modImage.WriteInitStaticClassMessage(this.ToString(), "Getting vendor types done");
                }

                //These are the rows for the job settings
                specficSettingsRows = dtAllSettings.Select("FLDCODE = 16", "Section");

                if (specficSettingsRows != null && specficSettingsRows.Length > 0)
                {
                    //modImage.WriteInitStaticClassMessage(this.ToString(), "Getting job settings");
                    GetJobSettings(specficSettingsRows);
                    //modImage.WriteInitStaticClassMessage(this.ToString(), "Getting job settings done");
                }

                //These are the rows for the Cost Code Settings
                specficSettingsRows = dtAllSettings.Select("FLDCODE = 18", "Section");

                if (specficSettingsRows != null && specficSettingsRows.Length > 0)
                {
                    //modImage.WriteInitStaticClassMessage(this.ToString(), "Getting cost code settings");
                    GetCostCodeSettings(specficSettingsRows);
                    //modImage.WriteInitStaticClassMessage(this.ToString(), "Getting cost code settings done");
                }

                //These are the rows for the Equipment Settings
                specficSettingsRows = dtAllSettings.Select("FLDCODE = 69", "Section");

                if (specficSettingsRows != null && specficSettingsRows.Length > 0)
                {
                    //modImage.WriteInitStaticClassMessage(this.ToString(), "Getting equipment settings");
                    GetEquipmentSettings(specficSettingsRows);
                    //modImage.WriteInitStaticClassMessage(this.ToString(), "Getting equipment settings done");
                }

                //These are the rows for the Equipment Cost Code Settings
                specficSettingsRows = dtAllSettings.Select("FLDCODE = 187", "Section");

                if (specficSettingsRows != null && specficSettingsRows.Length > 0)
                {
                    //modImage.WriteInitStaticClassMessage(this.ToString(), "Getting equipment cost code settings");
                    GetEquipmentCostCodeSettings(specficSettingsRows);
                    //modImage.WriteInitStaticClassMessage(this.ToString(), "Getting equipment cost code settings done");
                }

                //modImage.WriteInitStaticClassMessage(this.ToString(), "testing for statndard cost code table");
                this.mvarStandardCostCodeTable = TestForStandardCostCodeTable(ConnectionID);
                //modImage.WriteInitStaticClassMessage(this.ToString(), "testing for statndard cost code table done");

                //These are the rows for the Commitment Settings
                specficSettingsRows = dtAllSettings.Select("FLDCODE = 20", "Section");

                if (specficSettingsRows != null && specficSettingsRows.Length > 0)
                {
                    //modImage.WriteInitStaticClassMessage(this.ToString(), "getting commitment settings");
                    GetCommitmentSettings(specficSettingsRows);
                    //modImage.WriteInitStaticClassMessage(this.ToString(), "getting commitment settings done");
                }

                GetCapsSettings(ConnectionID);

                SetGLMasterInfo();
            }

        }

        private void SetGLMasterInfo()
        {
            GLPrefixType = this.GLPrefixABCLength > 0 ? "PrefixC" : (this.GLPrefixABLength > 0 ? "PrefixB" : "PrefixA");
            GLMasterPrefixALength = this.GLPrefixALength;
            GLMasterPrefixABLength = this.GLPrefixABLength == 0 ? 0 : this.GLPrefixALength + this.GLPrefixABLength + 1;
            GLMasterPrefixABCLength = this.GLPrefixABCLength == 0 ? 0 : this.GLPrefixALength + this.GLPrefixABLength + this.GLPrefixABCLength + 2; ;
            GLMasterBaseAcctLength = this.GLBaseLength;
            GLMasterSuffixLength = this.GLSuffixLength;
            GLMasterLevels = System.Convert.ToInt16(GLMasterPrefixABCLength > 0 ? 3 : (GLMasterPrefixABLength > 0 ? 2 : 1));
            GLMasterPrefixADesc = this.GLPrefixADesc;
            GLMasterPrefixABDesc = this.GLPrefixABDesc;
            GLMasterPrefixABCDesc = this.GLPrefixABCDesc;
        }

        private void GetCommitmentSettings(DataRow[] commitmentSettings)
        {
            bool gotit = true;

            //if (string.Compare(strTLVersion, "00009.00004.00000") >= 0)
            //{

            if (commitmentSettings != null && commitmentSettings.Length > 0)
            {
                this._CommitmentLen = Convert.ToInt32(CommonFunctions.TestNullNumber(commitmentSettings[0]["CSLEN"]));

                if (this._CommitmentLen > 0)
                {
                    this._CommitmentDesc = commitmentSettings[0]["CNDESC"].ToString();
                }

                if (Convert.ToInt32(CommonFunctions.TestNullNumber(commitmentSettings[0]["CNTCAP"])) == 0)
                {
                    _CommitmentCaps = false;
                }
                else
                {
                    _CommitmentCaps = true;
                }

            }
            //}

        }

        private void GetCapsSettings(int ConnectionID)
        {

            DataTable dtAllCapsSettings = new DataTable();
            //APIUtilDataAccessHelper.TimberlineDictData.FillDataTable(dtAllCapsSettings, "SELECT Section,FLDCODE,CNTCAP FROM TS_CTL_RECORD_3 WHERE FLDCODE in (6,3,38) ORDER BY FLDCODE", null, true, false);

            APIUtilDataAccessHelper.TimberSynchDataDynamic(ConnectionID).FillDataTable(dtAllCapsSettings, "SELECT Section,FLDCODE,CNTCAP FROM TS_CTL_RECORD_3 WHERE FLDCODE in (6,3,38) ORDER BY FLDCODE", null, true, false);

            GetCapsSetting(dtAllCapsSettings, ref _VendorCaps, 6);

            GetCapsSetting(dtAllCapsSettings, ref _InvoiceCaps, 3);

            GetCapsSetting(dtAllCapsSettings, ref _AuthorizationCaps, 38);

        }

        private void GetCapsSetting(DataTable dtAllCapsSettings, ref bool setng, int fldCode)
        {
            DataRow[] specficCapsSettingsRows = null;

            specficCapsSettingsRows = dtAllCapsSettings.Select("FLDCODE = " + fldCode.ToString().Trim(), "Section");

            if (specficCapsSettingsRows != null && specficCapsSettingsRows.Length > 0)
            {
                setng = GetCapsSetting(specficCapsSettingsRows);
            }
        }

        private bool GetCapsSetting(DataRow[] capsSettings)
        {
            bool CapsS = false;

            //if (string.Compare(strTLVersion, "00009.00004.00000") >= 0)
            //{

            if (capsSettings != null && capsSettings.Length > 0)
            {

                if (Convert.ToInt32(CommonFunctions.TestNullNumber(capsSettings[0]["CNTCAP"])) == 0)
                {
                    CapsS = false;
                }
                else
                {
                    CapsS = true;
                }

            }
            //}

            return CapsS;

        }

        public bool StandardCostCodeTable
        {
            get
            {
                bool returnValue = true;
                returnValue = mvarStandardCostCodeTable;
                return returnValue;
            }
        }

        public bool TestForStandardCostCodeTable(int ConnectionID)
        {
            bool returnValue = false;
            string sQry = "";

            try
            {
                int costCodeCnt = 0;
                object objectReturnedFromQuery = null;
                sQry = "SELECT count(*) FROM JCM_MASTER__STANDARD_COST_CODE";

                objectReturnedFromQuery = APIUtilDataAccessHelper.TimberSynchDataDynamic(ConnectionID).GetCell(sQry);

                returnValue = objectReturnedFromQuery != null && int.TryParse(objectReturnedFromQuery.ToString(), out costCodeCnt) && costCodeCnt > 0;
            }
            catch
            {

            }
            return returnValue;
        }

        public ActualTSCTL(int ConnectionID)
        {
            Class_Initialize_Renamed(ConnectionID);
        }

        public bool LoadGLSettings(DataRow[] glSettings)
        {
            bool returnValue = true;
            string sSepChar = "";

            //if (String.Compare(strTLVersion, "00009.00004.00000") >= 0)
            //{

            try
            {
                strGLPrefixFormat = "";

                if (glSettings != null && glSettings.Length > 0)
                {
                    foreach (DataRow dr in glSettings)
                    {
                        if (System.Convert.ToInt32(dr["Section"]) == 1)
                        {
                            mvarGLPrefixALength = System.Convert.ToInt16(dr["CSLEN"]);
                            if (mvarGLPrefixALength > 0)
                            {
                                mvarGLPrefixADesc = dr["CNDESC"].ToString();
                                mvarGLPrefixAPunct = dr["CNPUNCT"].ToString();
                                strGLPrefixFormat = string.Empty.PadLeft(mvarGLPrefixALength, 'x');// new string('x', mvarGLPrefixALength);
                                sSepChar = mvarGLPrefixAPunct;
                                intGLPrefixCnt = 1;
                            }
                        }
                        else if (System.Convert.ToInt32(dr["Section"]) == 2)
                        {
                            mvarGLPrefixABLength = System.Convert.ToInt16(dr["CSLEN"]);
                            if (mvarGLPrefixABLength > 0)
                            {
                                mvarGLPrefixABDesc = dr["CNDESC"].ToString();
                                mvarGLPrefixABPunct = dr["CNPUNCT"].ToString();
                                strGLPrefixFormat = strGLPrefixFormat + mvarGLPrefixAPunct + string.Empty.PadLeft(mvarGLPrefixABLength, 'x');//new string('x', mvarGLPrefixABLength);
                                sSepChar = mvarGLPrefixABPunct;
                                intGLPrefixCnt = 2;
                            }
                        }
                        else if (System.Convert.ToInt32(dr["Section"]) == 3)
                        {
                            mvarGLPrefixABCLength = System.Convert.ToInt16((dr["CSLEN"]));
                            if (mvarGLPrefixABCLength > 0)
                            {
                                mvarGLPrefixABCDesc = dr["CNDESC"].ToString();
                                mvarGLPrefixABCPunct = dr["CNPUNCT"].ToString();
                                strGLPrefixFormat = strGLPrefixFormat + mvarGLPrefixABPunct + string.Empty.PadLeft(mvarGLPrefixABCLength, 'x');//new string('x', mvarGLPrefixABCLength);
                                sSepChar = mvarGLPrefixABCPunct;
                                intGLPrefixCnt = 3;
                            }
                        }
                        else if (System.Convert.ToInt32(dr["Section"]) == 4)
                        {



                            mvarGLBaseLength = dr["CSLEN"] == null || dr["CSLEN"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CSLEN"]);
                            mvarGLBasePunct = dr["CNPUNCT"].ToString();
                            strGLBaseAccountFormat = string.Empty.PadLeft(mvarGLBaseLength, 'x') + mvarGLBasePunct;
                        }
                        else if (System.Convert.ToInt32(dr["Section"]) == 5)
                        {
                            mvarGLSuffixLength = System.Convert.ToInt32(dr["CSLEN"]);
                            strGLBaseAccountFormat = strGLBaseAccountFormat + string.Empty.PadLeft(mvarGLSuffixLength, 'x');//new string('x', mvarGLSuffixLength);
                        }
                    }
                }
                strGLFullAccountFormat = strGLPrefixFormat + sSepChar + strGLBaseAccountFormat;
            }
            catch (Exception e)
            {
                throw;
            }


            // }

            return returnValue;
        }

        public bool NoSuchTableError
        {
            get
            {
                bool returnValue = true;
                returnValue = mvarNoSuchTableError;
                return returnValue;
            }
        }

        public int GLPrefixALength
        {
            get
            {
                int returnValue = 0;
                returnValue = mvarGLPrefixALength;
                return returnValue;
            }
        }

        public string GLPrefixADesc
        {
            get
            {
                string returnValue = null;
                returnValue = mvarGLPrefixADesc;
                return returnValue;
            }
        }

        public string GLPrefixAPunct
        {
            get
            {
                string returnValue = null;
                returnValue = mvarGLPrefixAPunct;
                return returnValue;
            }
        }

        public int GLPrefixABLength
        {
            get
            {
                int returnValue = 0;
                returnValue = mvarGLPrefixABLength;
                return returnValue;
            }
        }

        public string GLPrefixABDesc
        {
            get
            {
                string returnValue = null;
                returnValue = mvarGLPrefixABDesc;
                return returnValue;
            }
        }

        public string GLPrefixABPunct
        {
            get
            {
                string returnValue = null;
                returnValue = mvarGLPrefixABPunct;
                return returnValue;
            }
        }

        public int GLPrefixABCLength
        {
            get
            {
                int returnValue = 0;
                returnValue = mvarGLPrefixABCLength;
                return returnValue;
            }
        }

        public string GLPrefixABCDesc
        {
            get
            {
                string returnValue = null;
                returnValue = mvarGLPrefixABCDesc;
                return returnValue;
            }
        }

        public string GLPrefixABCPunct
        {
            get
            {
                string returnValue = null;
                returnValue = mvarGLPrefixABCPunct;
                return returnValue;
            }
        }

        public int GLBaseLength
        {
            get
            {
                int returnValue = 0;
                returnValue = mvarGLBaseLength;
                return returnValue;
            }
        }

        public string GLBasePunct
        {
            get
            {
                string returnValue = null;
                returnValue = mvarGLBasePunct;
                return returnValue;
            }
        }

        public int GLSuffixLength
        {
            get
            {
                int returnValue = 0;
                returnValue = mvarGLSuffixLength;
                return returnValue;
            }
        }

        public string VendorType1
        {
            get
            {
                string returnValue = null;
                returnValue = mvarVendorType1;
                return returnValue;
            }
        }

        public string VendorType2
        {
            get
            {
                string returnValue = null;
                returnValue = mvarVendorType2;
                return returnValue;
            }
        }

        public string VendorType3
        {
            get
            {
                string returnValue = null;
                returnValue = mvarVendorType3;
                return returnValue;
            }
        }

        public string VendorType4
        {
            get
            {
                string returnValue = null;
                returnValue = mvarVendorType4;
                return returnValue;
            }
        }

        public string VendorType5
        {
            get
            {
                string returnValue = null;
                returnValue = mvarVendorType5;
                return returnValue;
            }
        }

        public int JobSec1Length
        {
            get
            {
                int returnValue = 0;
                returnValue = mvarJobSec1Length;
                return returnValue;
            }
        }

        public string JobSec1Desc
        {
            get
            {
                string returnValue = null;
                returnValue = mvarJobSec1Desc;
                return returnValue;
            }
        }

        public string JobSec1Punct
        {
            get
            {
                string returnValue = null;
                returnValue = mvarJobSec1Punct;
                return returnValue;
            }
        }

        public int JobSec2Length
        {
            get
            {
                int returnValue = 0;
                returnValue = mvarJobSec2Length;
                return returnValue;
            }
        }

        public string JobSec2Desc
        {
            get
            {
                string returnValue = null;
                returnValue = mvarJobSec2Desc;
                return returnValue;
            }
        }

        public string JobSec2Punct
        {
            get
            {
                string returnValue = null;
                returnValue = mvarJobSec2Punct;
                return returnValue;
            }
        }

        public int JobSec3Length
        {
            get
            {
                int returnValue = 0;
                returnValue = mvarJobSec3Length;
                return returnValue;
            }
        }

        public string JobSec3Desc
        {
            get
            {
                string returnValue = null;
                returnValue = mvarJobSec3Desc;
                return returnValue;
            }
        }

        public int CostCodeSec1Length
        {
            get
            {
                int returnValue = 0;
                returnValue = mvarCostCodeSec1Length;
                return returnValue;
            }
        }

        public string CostCodeSec1Desc
        {
            get
            {
                string returnValue = null;
                returnValue = mvarCostCodeSec1Desc;
                return returnValue;
            }
        }

        public string CostCodeSec1Punct
        {
            get
            {
                string returnValue = null;
                returnValue = mvarCostCodeSec1Punct;
                return returnValue;
            }
        }

        public int CostCodeSec2Length
        {
            get
            {
                int returnValue = 0;
                returnValue = mvarCostCodeSec2Length;
                return returnValue;
            }
        }

        public string CostCodeSec2Desc
        {
            get
            {
                string returnValue = null;
                returnValue = mvarCostCodeSec2Desc;
                return returnValue;
            }
        }

        public string CostCodeSec2Punct
        {
            get
            {
                string returnValue = null;
                returnValue = mvarCostCodeSec2Punct;
                return returnValue;
            }
        }

        public int CostCodeSec3Length
        {
            get
            {
                int returnValue = 0;
                returnValue = mvarCostCodeSec3Length;
                return returnValue;
            }
        }

        public string CostCodeSec3Desc
        {
            get
            {
                string returnValue = null;
                returnValue = mvarCostCodeSec3Desc;
                return returnValue;
            }
        }

        public string CostCodeSec3Punct
        {
            get
            {
                string returnValue = null;
                returnValue = mvarCostCodeSec3Punct;
                return returnValue;
            }
        }

        public int CostCodeSec4Length
        {
            get
            {
                int returnValue = 0;
                returnValue = mvarCostCodeSec4Length;
                return returnValue;
            }
        }

        public string CostCodeSec4Desc
        {
            get
            {
                string returnValue = null;
                returnValue = mvarCostCodeSec4Desc;
                return returnValue;
            }
        }

        public int EQSec1Length
        {
            get
            {
                int returnValue = 0;
                returnValue = mvarEQSec1Length;
                return returnValue;
            }
        }

        public string EQSec1Desc
        {
            get
            {
                string returnValue = null;
                returnValue = mvarEQSec1Desc;
                return returnValue;
            }
        }

        public string EQSec1Punct
        {
            get
            {
                string returnValue = null;
                returnValue = mvarEQSec1Punct;
                return returnValue;
            }
        }

        public int EQSec2Length
        {
            get
            {
                int returnValue = 0;
                returnValue = mvarEQSec2Length;
                return returnValue;
            }
        }

        public string EQSec2Desc
        {
            get
            {
                string returnValue = null;
                returnValue = mvarEQSec2Desc;
                return returnValue;
            }
        }

        public string EQSec2Punct
        {
            get
            {
                string returnValue = null;
                returnValue = mvarEQSec2Punct;
                return returnValue;
            }
        }

        public int GLPrefixCnt
        {
            get
            {
                int returnValue = 0;
                returnValue = intGLPrefixCnt;
                return returnValue;
            }
        }

        public string GLPrefixDescription
        {
            get
            {
                string desc = string.Empty;
                if (this.mvarGLPrefixABCLength > 0)
                {
                    desc = this.mvarGLPrefixABCDesc;
                }
                else if (this.mvarGLPrefixABLength > 0)
                {
                    desc = this.mvarGLPrefixABDesc;
                }
                else if (this.mvarGLPrefixALength > 0)
                {
                    desc = this.mvarGLPrefixADesc;
                }

                return desc;
            }
        }

        public string DetermineBaseAccount(string sAcct, bool bIncludeSuffix, int ConnectionID)
        {


            return this.DetermineBaseAccount(sAcct, bIncludeSuffix, true, ConnectionID);


        }



        //public string DetermineBaseAccount(ref string sAcct, bool bIncludeSuffix)
        public string DetermineBaseAccount(string sAcct, bool bIncludeSuffix, bool makeSureBaseAccountIsValid, int ConnectionID)
        {

            if (string.IsNullOrWhiteSpace(sAcct))
            {
                return string.Empty;
            }
            string returnValue = null;
            string sSuffix;
            string sNewAcct;
            string sBaseAcct;
            string sReturnAcct;

            //modIniFiles.LogFile("DetermineBaseAccount");
            sNewAcct = StripPunctuation(sAcct, false);
            sBaseAcct = "";

            sSuffix = "";
            if (mvarGLPrefixALength == 0) //no prefix
            {
                if (mvarGLSuffixLength > 0)
                {
                    sSuffix = Strings.Right(sNewAcct, mvarGLSuffixLength);
                    sBaseAcct = Strings.Left(sNewAcct, sNewAcct.Length - mvarGLSuffixLength); //sNewAcct.Substring(0, sNewAcct.Length - mvarGLSuffixLength);
                }
                else
                {
                    sBaseAcct = sNewAcct;
                }
            }
            else
                if (mvarGLPrefixALength > 0)
                {
                    sNewAcct = Strings.Right(sNewAcct, mvarGLBaseLength + mvarGLSuffixLength);
                    sBaseAcct = Strings.Left(sNewAcct, mvarGLBaseLength);//sNewAcct.Substring(0, mvarGLBaseLength);
                    if (mvarGLSuffixLength > 0)
                    {
                        sSuffix = Strings.Right(sNewAcct, mvarGLSuffixLength);
                    }
                }
                else
                {
                    if (mvarGLSuffixLength > 0)
                    {
                        sSuffix = Strings.Right(sNewAcct, mvarGLSuffixLength);
                        sBaseAcct = Strings.Left(sNewAcct, sNewAcct.Length - mvarGLBaseLength);
                    }
                    else
                    {
                        sBaseAcct = sNewAcct;
                    }
                }

            if (sSuffix.Length > 0 && bIncludeSuffix == true)
            {
                sReturnAcct = sBaseAcct + mvarGLBasePunct + sSuffix;
            }
            else
            {
                sReturnAcct = sBaseAcct;
            }
            //only if a suffix is supposed to be included can we test for a valid base accouny
            if (bIncludeSuffix == true && makeSureBaseAccountIsValid)
            {
                if (this.TestForValidBaseAccount(sReturnAcct, ConnectionID) == false)
                {
                    sReturnAcct = "";
                }
            }

            returnValue = sReturnAcct;
            return returnValue;
        }

        public string DetermineSuffix(ref string sAcct)
        {
            string returnValue = null;
            string sNewAcct;
            string sSuffix;

            sNewAcct = StripPunctuation(sAcct, false);
            if (mvarGLSuffixLength > 0)
            {
                sSuffix = sNewAcct.Substring(sNewAcct.Length - mvarGLSuffixLength, mvarGLSuffixLength);
            }
            else
            {
                sSuffix = "";
            }
            returnValue = sSuffix;
            return returnValue;
        }

        public bool TestForValidBaseAccount(string sAccount, int ConnectionID)
        {
            bool returnValue = true;
            string sQry = "";
            DataTable aRs = null;

            //If sAccount = "" Then TestForValidBaseAccount = False : Exit Function
            if (mvarGLPrefixALength == 0)
            {
                sQry = "SELECT Account FROM GLM_MASTER__ACCOUNT" + " WHERE Account=" + CommonFunctions.QNApos(sAccount);
            }
            else
            {
                sQry = "SELECT Base_Account FROM GLM_MASTER__BASE_ACCOUNT" + " WHERE Base_Account=" + CommonFunctions.QNApos(sAccount);
            }
            //try
            //{
            aRs = new DataTable();
            //APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(aRs, sQry, null, true, false);
            APIUtilDataAccessHelper.TimberSynchDataDynamic(ConnectionID).FillDataTable(aRs, sQry, null, true, false);


            //Main.OdbcDR(sQry , TimberScan.Properties.Extended.Default.TimberlineConnectionString);
            //}
            //catch (Exception e)
            //{
            //    throw;
            //}

            //if (aRs.Read() == false)
            //{
            //    returnValue = false;
            //}
            //else
            //{
            //    returnValue = true;
            //}
            //return returnValue;

            return aRs != null && aRs.Rows != null && aRs.Rows.Count > 0;
        }

        //public string DetermineGLPrefix(ref string sAcct, string sPrfxLvl)
        public string DetermineGLPrefix(string sAcct, int ConnectionID)
        {
            return DetermineGLPrefix(sAcct, this.GLPrefixType, true, ConnectionID);
        }

        public string DetermineGLPrefix(string sAcct, string sPrfxLvl, bool makeSurePrefixIsValid, int ConnectionID)
        {
            string returnValue = String.Empty;
            //sAcct can either be a full Account including G/L Prefix or just the G/L Prefix or just the base account
            //sPrfxLvl is the prefix level you wan to determine, "PrefixA", "PrefixB" or "PrefixC"

            string sPrefixAB = String.Empty;
            string sNewAcct = String.Empty;
            string sPrefixA = String.Empty;
            string sPrefixABC = String.Empty;


            int iMinFullAcctLen = 0;
            int iMinPrefixLen = 0;
            int iMaxPrefixLen = 0;

            if (string.IsNullOrEmpty(sAcct))
            {
                return string.Empty;
            }

            if (mvarGLPrefixALength == 0)
            {
                return string.Empty;
            }

            sPrefixA = "";
            sPrefixAB = "";
            sPrefixABC = "";
            sNewAcct = StripPunctuation(sAcct, false);

            if (string.IsNullOrEmpty(sNewAcct))
            {
                return string.Empty;
            }

            iMinFullAcctLen = 1 + mvarGLPrefixABLength + mvarGLPrefixABCLength + mvarGLBaseLength + mvarGLSuffixLength;

            iMinPrefixLen = 1 + mvarGLPrefixABLength + mvarGLPrefixABCLength;
            iMaxPrefixLen = mvarGLPrefixALength + mvarGLPrefixABLength + mvarGLPrefixABCLength;

            if (sNewAcct.Length > iMaxPrefixLen || sNewAcct.Length >= iMinFullAcctLen)
            {
                sNewAcct = sNewAcct.Substring(0, sNewAcct.Length - mvarGLBaseLength - mvarGLSuffixLength);
            }

            if (sNewAcct.Length < iMinPrefixLen)
            {
                return string.Empty;
            }

            if (intGLPrefixCnt == 1)
            {
                sPrefixA = sNewAcct;
            }
            else
                if (intGLPrefixCnt == 2)
                {
                    sPrefixA = sNewAcct.Substring(0, sNewAcct.Length - mvarGLPrefixABLength);
                    sPrefixAB = sNewAcct.Substring(sNewAcct.Length - mvarGLPrefixABLength, mvarGLPrefixABLength);
                }
                else
                {
                    sPrefixA = sNewAcct.Substring(0, sNewAcct.Length - mvarGLPrefixABLength - mvarGLPrefixABCLength);
                    sPrefixABC = sNewAcct.Substring(sNewAcct.Length - mvarGLPrefixABCLength, mvarGLPrefixABCLength);
                    sNewAcct = sNewAcct.Substring(sPrefixA.Length + 1 - 1);
                    sPrefixAB = sNewAcct.Substring(0, mvarGLPrefixABLength);
                }

            if (sPrfxLvl == "PrefixA" || intGLPrefixCnt == 1)
            {
                sNewAcct = sPrefixA;

                if (makeSurePrefixIsValid)
                {
                    if (this.TestForValidGLPrefix(sNewAcct, "PrefixA", ConnectionID) == false)
                    {
                        sNewAcct = "";
                    }
                }
            }
            else
                if (sPrfxLvl == "PrefixB" || intGLPrefixCnt == 2)
                {
                    sNewAcct = sPrefixA + mvarGLPrefixAPunct + sPrefixAB;

                    if (makeSurePrefixIsValid)
                    {
                        if (this.TestForValidGLPrefix(sNewAcct, "PrefixB", ConnectionID) == false)
                        {
                            sNewAcct = "";
                        }
                    }
                }
                else
                {

                    sNewAcct = sPrefixA + mvarGLPrefixAPunct + sPrefixAB + mvarGLPrefixABPunct + sPrefixABC;

                    if (makeSurePrefixIsValid)
                    {
                        if (this.TestForValidGLPrefix(sNewAcct, "PrefixC", ConnectionID) == false)
                        {
                            sNewAcct = "";
                        }
                    }
                }


            returnValue = sNewAcct;

            return returnValue;
        }

        public bool TestForValidGLPrefix(string sPrefix, string sPrefixLevel, int ConnectionID)
        {
            bool returnValue = true;
            //sPrefixLevel - PrefixA, PrefixB or PrefixC
            string sQry = "";
            DataTable aRs = new DataTable();

            int lErrNo = 0;

            if (sPrefixLevel == "PrefixA")
            {
                sQry = "SELECT Account_Prefix_A FROM GLM_MASTER__ACCOUNT_PREFIX_A" + " WHERE Account_Prefix_A=" + CommonFunctions.QNApos(sPrefix);
            }
            else
                if (sPrefixLevel == "PrefixB")
                {
                    sQry = "SELECT Account_Prefix_AB FROM GLM_MASTER__ACCOUNT_PREFIX_AB" + " WHERE Account_Prefix_AB=" + CommonFunctions.QNApos(sPrefix);
                }
                else
                {
                    sQry = "SELECT Account_Prefix_ABC FROM GLM_MASTER__ACCOUNT_PREFIX_ABC" + " WHERE Account_Prefix_ABC=" + CommonFunctions.QNApos(sPrefix);
                }

            try
            {

                //APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(aRs, sQry, null, true, false);
                APIUtilDataAccessHelper.TimberSynchDataDynamic(ConnectionID).FillDataTable(aRs, sQry, null, true, false);
            }
            catch
            {
                //frank swallows these exceptions, i didnt want to do that but we have not choice
            }

            return aRs != null && aRs.Rows.Count > 0;

        }

        //Signature chaged to use optional parameters - VishA, 12/15/2014
        public string DetermineBalanceSheetLevel(int ConnectionID, string sPrefixA, string sPrefixB = "", string sPrefixC = "")
        {
            string returnValue = null;
            string sBalLevel;
            string sFiscEntLvl;
            string sQry = "";
            DataTable dt = null;
            if (this.GLPrefixCnt == 0)
            {
                return string.Empty;

            }

            sQry = "SELECT Balance_Sheet_Level, Period_End_1__Current FROM GLM_MASTER__FISCAL_CONTROLS";
            dt = new DataTable();
            APIUtilDataAccessHelper.TimberSynchDataDynamic(ConnectionID).FillDataTable(dt, sQry, null, true, false);

            DateTime dtTime = DateTime.MinValue;
            DateTime.TryParse(dt.Rows[0]["Period_End_1__Current"].ToString(), out dtTime);
            //if (Microsoft.VisualBasic.Information.IsDate(dt.Rows[0]["Period_End_1__Current"]) == true)
            if (dtTime != DateTime.MinValue)
            {
                returnValue = System.Convert.ToString(dt.Rows[0]["Balance_Sheet_Level"]);
                return returnValue;
            }

            if (this.GLPrefixCnt == 1)
            {
                returnValue = this.GLPrefixADesc;
                return returnValue;
            }

            sQry = "SELECT Fiscal_Entity_Level, Balance_Sheet_Level FROM GLM_MASTER__ACCOUNT_PREFIX_A WHERE Account_Prefix_A=" + CommonFunctions.QNApos(sPrefixA);
            dt = new DataTable();
            APIUtilDataAccessHelper.TimberSynchDataDynamic(ConnectionID).FillDataTable(dt, sQry, null, true, false);

            if (!(dt != null && dt.Rows.Count > 0))
                return string.Empty;

            sFiscEntLvl = System.Convert.ToString(dt.Rows[0]["Fiscal_Entity_Level"]);

            if (sFiscEntLvl.ToUpper() == this.GLPrefixADesc.ToUpper())
            {
                returnValue = System.Convert.ToString(dt.Rows[0]["Balance_Sheet_Level"]);
                return returnValue;
            }

            if (sFiscEntLvl.ToUpper() == this.GLPrefixABDesc.ToUpper())
            {
                sQry = "SELECT Balance_Sheet_Level FROM GLM_MASTER__ACCOUNT_PREFIX_AB WHERE Alternate_Account_Prefix_A=" + CommonFunctions.QNApos(sPrefixA);
                if (!string.IsNullOrEmpty(sPrefixB))
                {
                    sQry += "And Alternate_Account_Prefix_B =" + CommonFunctions.QNApos(sPrefixB);
                }
            }
            else
            {
                sQry = "SELECT Balance_Sheet_Level FROM GLM_MASTER__ACCOUNT_PREFIX_ABC WHERE Alternate_Account_Prefix_A=" + CommonFunctions.QNApos(sPrefixA);
                if (!string.IsNullOrEmpty(sPrefixB))
                {
                    sQry += "And Alternate_Account_Prefix_B =" + CommonFunctions.QNApos(sPrefixB);
                }
                if (!string.IsNullOrEmpty(sPrefixC))
                {
                    sQry += "And Alternate_Account_Prefix_C=" + CommonFunctions.QNApos(sPrefixC);
                }
            }

            dt = new DataTable();
            APIUtilDataAccessHelper.TimberSynchDataDynamic(ConnectionID).FillDataTable(dt, sQry, null, true, false);

            if (!(dt != null && dt.Rows.Count > 0))
                return string.Empty;

            if (dt.Rows.Count == 1)
                returnValue = System.Convert.ToString(dt.Rows[0]["Balance_Sheet_Level"]);
            else if (dt.Rows.Count > 1)
            {
                DataRow[] dr = dt.Select("Balance_Sheet_Level = '" + sFiscEntLvl + "'");
                if (dr != null && dr.Length > 0)
                    returnValue = System.Convert.ToString(dr[0]["Balance_Sheet_Level"]);
                else
                    returnValue = System.Convert.ToString(dt.Rows[0]["Balance_Sheet_Level"]);
            }

            return returnValue;
        }

        public string DeterminePunctuation()
        {
            string punctuation = string.Empty;

            if (GLPrefixABCLength > 0)
            {
                punctuation = GLPrefixABCPunct;
            }
            else if (GLPrefixABLength > 0)
            {
                punctuation = GLPrefixABPunct;
            }
            else if (GLPrefixALength > 0)
            {
                punctuation = GLPrefixAPunct;
            }
            return punctuation;
        }

        public string DetermineAPGLPrefix(string sGLPrefix, int ConnectionID)
        {
            string returnValue = null;
            //sGlPrefix is the prefix from an expense account, we need this to determine the prefix for the Accounts Payable or
            //Accrued Invoices payable account

            string sAPPrefix = "";
            string sEntity = "";
            string sBalLvl = "";
            string sPrfxA = "";
            string sPrfxB = string.Empty;
            string sPrfxC = string.Empty;

            if (this.GLPrefixALength == 0)
            {
                return string.Empty;
            }
            if (string.IsNullOrEmpty(sGLPrefix))
            {
                return string.Empty;
            }

            //if (this.GLPrefixCnt == 1) // Commented out as part of fix for C#38109 VishA 9/22/15
            //{
            //    sPrfxA = sGLPrefix;
            //}
            if (this.GLPrefixCnt == 1)
            {
                sPrfxA = sGLPrefix.Contains(GLPrefixAPunct) ? Strings.Left(sGLPrefix, Strings.InStr(sGLPrefix, this.GLPrefixAPunct, CompareMethod.Binary) - 1) : sGLPrefix;
            }

            if (this.GLPrefixCnt == 2)
            {
                sPrfxA = Strings.Left(sGLPrefix, Strings.InStr(sGLPrefix, this.GLPrefixAPunct, CompareMethod.Binary) - 1);
                sPrfxB = Strings.Mid(sGLPrefix, Strings.InStrRev(sGLPrefix, this.GLPrefixABPunct, sGLPrefix.Length, CompareMethod.Binary) + 1);
            }
            else if (this.GLPrefixCnt == 3)
            {
                sPrfxA = Strings.Left(sGLPrefix, Strings.InStr(sGLPrefix, this.GLPrefixAPunct, CompareMethod.Binary) - 1);
                sPrfxB = Strings.Left(sGLPrefix, Strings.InStrRev(sGLPrefix, this.GLPrefixABPunct, sGLPrefix.Length, CompareMethod.Binary) - 1);
                sPrfxB = Strings.Mid(sPrfxB, Strings.InStr(sPrfxB, this.GLPrefixAPunct, CompareMethod.Binary) + 1);
                if (sPrfxB.Contains(this.GLPrefixABCPunct) || sPrfxB.Contains(this.GLPrefixABPunct) || sPrfxB.Contains(this.GLPrefixAPunct))
                    sPrfxB = Strings.Left(sPrfxB, Strings.InStrRev(sPrfxB, this.GLPrefixAPunct, sPrfxB.Length, CompareMethod.Binary) - 1);
                sPrfxC = Strings.Left(sGLPrefix, Strings.InStrRev(sGLPrefix, this.GLPrefixABCPunct, sGLPrefix.Length, CompareMethod.Binary) - 1);
                sPrfxC = Strings.Mid(sPrfxC, Strings.InStr(sPrfxC, this.GLPrefixABPunct, CompareMethod.Binary) + 1);
                if (sPrfxC.Contains(this.GLPrefixABCPunct) || sPrfxC.Contains(this.GLPrefixABPunct) || sPrfxC.Contains(this.GLPrefixAPunct))
                    sPrfxC = Strings.Mid(sPrfxC, Strings.InStrRev(sPrfxC, this.GLPrefixABPunct, sPrfxC.Length, CompareMethod.Binary) + 1);
            }

            sBalLvl = this.DetermineBalanceSheetLevel(ConnectionID, sPrfxA, sPrfxB, sPrfxC);

            if (this.GLPrefixCnt == 3)
            {
                switch (sBalLvl)
                {
                    case "Company":
                        sAPPrefix = sPrfxA + this.GLPrefixAPunct + new string('0', this.GLPrefixABLength) + this.GLPrefixABPunct + new string('0', this.GLPrefixABCLength);
                        return sAPPrefix;

                    case "Region":
                        sAPPrefix = sPrfxA + this.GLPrefixAPunct + sPrfxB + this.GLPrefixABPunct + new string('0', this.GLPrefixABCLength);
                        return sAPPrefix;

                    case "Prop/Div":
                    case "Property":
                        sAPPrefix = sPrfxA + this.GLPrefixAPunct + sPrfxB + this.GLPrefixABPunct + sPrfxC;
                        return sAPPrefix;
                }

            }

            if (sBalLvl == this.GLPrefixADesc)
            {
                if (this.GLPrefixCnt == 1)
                {
                    sAPPrefix = sGLPrefix.Contains(GLPrefixAPunct) ? Strings.Left(sGLPrefix, Strings.InStr(sGLPrefix, this.GLPrefixAPunct, CompareMethod.Binary) - 1) : sGLPrefix;
                }
                else
                    if (this.GLPrefixCnt == 2)
                    {
                        sAPPrefix = sPrfxA + this.GLPrefixAPunct + new string('0', System.Convert.ToInt32(this.GLPrefixABLength));
                    }
                    else
                    {
                        sAPPrefix = sPrfxA + this.GLPrefixAPunct + new string('0', this.GLPrefixABLength) + this.GLPrefixABPunct + new string('0', this.GLPrefixABCLength);
                    }
            }
            else
                if (sBalLvl == this.GLPrefixABDesc)
                {
                    if (this.GLPrefixCnt == 2)
                    {
                        sAPPrefix = sGLPrefix;
                    }
                    else
                    {
                        //sAPPrefix = sGLPrefix + this.GLPrefixABPunct + new string('0', this.GLPrefixABCLength);
                        sAPPrefix = sPrfxA + this.GLPrefixAPunct + sPrfxB + this.GLPrefixABPunct + new string('0', this.GLPrefixABCLength);
                    }
                }
                else
                {
                    sAPPrefix = sGLPrefix;
                }
            returnValue = sAPPrefix;
            return returnValue;
        }

        public string FormatFullGLAccount(ref string sAct, Boolean bImportedRec)
        {
            string returnValue = String.Empty;
            string sNewAcct = String.Empty;
            string sNewItem = String.Empty;
            string sSuffix = String.Empty;
            string sAcct = String.Empty;
            string[] sSections;
            int iMaxLen = 0;
            int iLoop = 0;
            int iMinLen = 0;
            int iMinLenSfx = 0;
            int iMaxLenSfx = 0;
            int iPos = 0;
            int iDecPnts = 0;

            sAcct = sAct.Trim();

            if (sAcct == "")
            {
                return returnValue;
            }

            if (this.GLSuffixLength > 0)
            {
                iDecPnts = CountAccountDecimalPoints(sAcct);
                if (iDecPnts > 1)
                {
                    returnValue = "Incorrect account format format. Only one decimal point is allowed. Correct format is " + strGLFullAccountFormat + ". Reenter account.";
                    return returnValue;
                }

                iPos = sAcct.IndexOf(this.GLBasePunct.ToString());
                if ((iPos + 1) == sAcct.Length)//if the GL base punctuation is the last character in the account
                {
                    sNewItem = sAcct.Substring(0, iPos);
                    sSuffix = string.Empty.PadLeft(this.GLSuffixLength, Convert.ToChar("0"));
                }
                else if (iPos > 0)
                {
                    sNewItem = sAcct.Substring(0, iPos);
                    sSuffix = (sAcct.Substring(iPos + 1) + "".PadLeft(this.GLSuffixLength, '0')).Substring(0, GLSuffixLength); // Strings.Left(Strings.Mid(sAcct , iPos + 1) + "".PadLeft(this.GLSuffixLength , '0') , this.GLSuffixLength);

                }
                else
                {
                    if (bImportedRec == true)
                    {
                        sNewItem = this.StripGLAccountPunctuation(sAcct, false);
                        sSuffix = Strings.Right(sNewItem, mvarGLSuffixLength);
                        sNewItem = Strings.Left(sNewItem, sNewItem.Length - mvarGLSuffixLength);
                    }
                    else
                    {
                        if (TestValidCharacters(sAcct, false) == false)
                        {
                            returnValue = "Incorrect account format. Invalid character. Correct format is " + strGLFullAccountFormat + ". Renter account.";
                            return returnValue;
                        }
                        sNewItem = StripGLAccountPunctuation(sAcct, false);

                        if (this.GLPrefixALength > 0)
                        {
                            iMaxLen = this.GLPrefixALength + this.GLPrefixABLength + this.GLPrefixABCLength + this.GLBaseLength;
                            iMinLen = 1 + this.GLPrefixABLength + this.GLPrefixABCLength + this.GLBaseLength;
                            iMaxLenSfx = iMaxLen + this.GLSuffixLength;
                            iMinLenSfx = iMinLen + this.GLSuffixLength;
                        }
                        else
                        {
                            iMaxLen = this.GLBaseLength;
                            iMinLen = 1;
                            iMaxLenSfx = iMaxLen + this.GLSuffixLength;
                            iMinLenSfx = iMinLen + this.GLSuffixLength;
                        }

                        if (sNewItem.Length < iMinLen)
                        {
                            returnValue = "Incorrect account format format. Insufficient digits entered. Correct format is " + strGLFullAccountFormat + ". Reenter account.";
                            return returnValue;
                        }

                        if (sNewItem.Length > iMaxLenSfx)
                        {
                            returnValue = "Incorrect account format. Too many digits before decimal point. Correct format is " + strGLFullAccountFormat + ". Renter account.";
                            return returnValue;
                        }

                        if (sNewItem.Length <= iMaxLen)
                        {
                            sSuffix = string.Empty.PadRight(this.GLSuffixLength, Convert.ToChar("0"));
                        }
                        else
                        {
                            sSuffix = Strings.Right(sNewItem, mvarGLSuffixLength);
                            sNewItem = Strings.Left(sNewItem, sNewItem.Length - mvarGLSuffixLength);
                        }
                    }
                }
            }
            else
            {
                sNewItem = sAcct;
            }

            if (this.GLPrefixALength > 0)
            {
                sNewItem = StripGLAccountPunctuation(sNewItem, true);
            }
            else
            {
                if (TestValidCharacters(sNewItem, true) == false)
                {
                    returnValue = "Incorrect account format. Invalid character. Correct format is " + strGLFullAccountFormat + ". Renter account.";
                    return returnValue;
                }
            }

            if (sNewItem == "Invalid character")
            {
                returnValue = "Incorrect account format. Invalid character. Correct format is " + strGLFullAccountFormat + ". Renter account.";
                return returnValue;
            }

            if (!string.IsNullOrEmpty(sSuffix))
            {
                if (!TestValidCharacters(sSuffix, true))
                {
                    returnValue = "Incorrect account format. Invalid character. Correct format is " + strGLFullAccountFormat + ". Renter account.";
                    return returnValue;
                }
                sNewItem += "|" + sSuffix;
            }

            sSections = sNewItem.Split(Convert.ToChar("|"));
            sNewAcct = string.Empty;

            if (sSections != null && (sSections.Length - 1) > 0) //muliple prefix levels formatted
            {
                sNewItem = string.Empty;
                //reminder - we don't decrement intGLPrefixCnt becuase sSections() is zero based and this allows for base account
                if (mvarGLSuffixLength > 0)
                {
                    if ((sSections.Length - 1) < (intGLPrefixCnt + 1))
                    {
                        sNewItem = "Incorrect account format format. Insufficient digits entered. Correct format is " + strGLFullAccountFormat + ". Reenter account.";
                    }
                    else
                        if ((sSections.Length - 1) > (intGLPrefixCnt + 1))
                        {
                            sNewItem = "Incorrect account format. Invalid character. Correct format is " + strGLFullAccountFormat + ". Renter account.";
                        }
                }
                else
                {
                    if ((sSections.Length - 1) < (intGLPrefixCnt))
                    {
                        sNewItem = "Incorrect account format format. Insufficient digits entered. Correct format is " + strGLFullAccountFormat + ". Reenter account.";
                    }
                    else
                        if ((sSections.Length - 1) > (intGLPrefixCnt))
                        {
                            sNewItem = "Incorrect account format. Invalid character. Correct format is " + strGLFullAccountFormat + ". Renter account.";
                        }
                }

                if (!string.IsNullOrEmpty(sNewItem))
                {
                    returnValue = sNewItem;
                    return returnValue;
                }

                //we assemble full acount w/o punctuation
                sNewAcct = "";
                sNewItem = "";
                for (iLoop = 0; iLoop < sSections.Length; iLoop++)
                {
                    sNewAcct = sNewAcct + sSections[iLoop];
                }
            }
            else
            {
                sNewAcct = sNewItem;
            }

            iMaxLen = this.GLPrefixALength + this.GLPrefixABLength + this.GLPrefixABCLength + this.GLBaseLength + this.GLSuffixLength;

            if (mvarGLPrefixALength > 0)
            {
                iMinLen = 1 + mvarGLPrefixABLength + mvarGLPrefixABCLength + mvarGLBaseLength + this.GLSuffixLength;
            }
            else
            {
                //if there are no GL prefixes defined, the base account length can be less than the defined length
                //otherwise it must be the defined length. If this is the case, we just make certain that the suffix
                //is formatted correctly then pass back the account and let the calling routine check if the account is valid
                iMinLen = 1 + this.GLSuffixLength;
                if (sNewAcct.Length >= iMinLen && sNewAcct.Length <= iMaxLen)
                {
                    if (this.GLSuffixLength > 0)
                    {
                        sNewAcct = sNewAcct.Substring(0, sNewAcct.Length - this.GLSuffixLength) + this.GLBasePunct + sNewAcct.Substring(sNewAcct.Length - this.GLSuffixLength, this.GLSuffixLength);
                    }
                    returnValue = sNewAcct;
                    return returnValue;
                }
            }
            if (sNewAcct.Length < iMinLen)
            {
                returnValue = "Incorrect account format. Insufficient digits entered. Correct format is " + strGLFullAccountFormat + ". Reenter account.";
                return returnValue;
            }
            else if (sNewAcct.Length > iMaxLen)
            {
                returnValue = "Incorrect account format. Too many digits before decimal point. Correct format is " + strGLFullAccountFormat + ". Renter account.";
                return returnValue;
            }

            returnValue = FormatItem(sNewAcct, strGLFullAccountFormat);
            return returnValue;
        }

        private bool TestValidCharacters(string sAcct, bool bIgnorePunctuation)
        {
            int iPos = 0;
            string sChr = String.Empty;
            string sTestPunct = String.Empty;
            bool returnValue = false;

            if (bIgnorePunctuation == false)
            {
                if (this.GLPrefixALength > 0)
                {
                    sTestPunct = this.GLPrefixAPunct;
                    if (this.GLPrefixABLength > 0 && Strings.InStr(sTestPunct, this.GLPrefixABCPunct, CompareMethod.Binary) == 0)
                    {
                        sTestPunct += this.GLPrefixABPunct;
                    }
                    if (this.GLPrefixABCLength > 0 && Strings.InStr(sTestPunct, this.GLPrefixABCPunct, CompareMethod.Binary) == 0)
                    {
                        sTestPunct += this.GLPrefixABCPunct;
                    }
                }
                if (this.GLSuffixLength > 0)
                {
                    sTestPunct += this.GLBasePunct;   //GLBasePunct is always "."
                }
            }

            for (iPos = 1; iPos <= sAcct.Length; iPos++)
            {
                sChr = Strings.Mid(sAcct, iPos, 1);

                if (sTestPunct.Length > 0)
                {
                    if (!((String.Compare(sChr, "A") >= 0 && String.Compare(sChr, "Z") <= 0) || (String.Compare(sChr, "0") >= 0 && String.Compare(sChr, "9") <= 0) || String.Compare(sChr, " ") == 0 || sTestPunct.IndexOf(sChr) > 0))
                    {
                        returnValue = false;
                        return returnValue;
                    }
                }
                else
                {
                    if (!((String.Compare(sChr, "A") >= 0 && String.Compare(sChr, "Z") <= 0) || (String.Compare(sChr, "0") >= 0 && String.Compare(sChr, "9") <= 0) || String.Compare(sChr, " ") == 0))
                    {
                        returnValue = false;
                        return returnValue;
                    }
                }
            }
            returnValue = true;
            return returnValue;
        }

        private bool TestForGLAccountPunctuation(string sAcct)
        {
            int iPos = 1;
            int iCnt = 0;
            string sChr = String.Empty;
            string[] sTestChars;
            bool returnValue = false;

            if (this.GLPrefixALength == 0)
            {
                return returnValue;
            }

            if (this.GLPrefixABCLength > 0)
            {
                sTestChars = new string[3];
                sTestChars[0] = this.GLPrefixAPunct;
                sTestChars[1] = this.GLPrefixABPunct;
                sTestChars[2] = this.GLPrefixABCPunct;
            }
            else if (this.GLPrefixABLength > 0)
            {
                sTestChars = new string[2];
                sTestChars[0] = this.GLPrefixAPunct;
                sTestChars[1] = this.GLPrefixABPunct;
            }
            else
            {
                sTestChars = new string[1];
                sTestChars[0] = this.GLPrefixAPunct;
            }

            for (iCnt = 0; iCnt < sTestChars.Length; iCnt++)
            {
                iPos = sAcct.IndexOf(sTestChars[iCnt]);
                if (iPos == 0)
                {
                    return returnValue;
                }
                iPos++;
            }
            returnValue = true;
            return returnValue;
        }

        
        public string TestForValidCode(string sFldNme, string sKey, bool bUseToDoList, string sJob, string sExtra, string sCostCode, string sCategory, string sEquip, string sEqCostCode, string sExpAcct, string sVendor, TLSettings.Models.APDistributionSettings apDistSettings, int ConnectionID)
        {
            string sRetVal = "";
            string sQry = "";
            string sTScanQry = "";
            string sEQCostCode = "";
            int iLoop = 0;
            string sPrfAB = "";
            string sGLAcct = "";
            string sGlPreFix = "";
            string sPrfA = "";
            string sPrfABC = "";
            string sFullAcct = "";
            string sPunct = "";
            string sBaseAcctDesc = "";
            string sGlPrexDesc = "";
            string strAPGLPrefix = "";
            if (this.GLPrefixABCLength > 0)
            {
                sPunct = this.GLPrefixABCPunct;
            }
            else if (this.GLPrefixABLength > 0)
            {
                sPunct = this.GLPrefixABPunct;
            }
            else if (this.GLPrefixALength > 0)
            {
                sPunct = this.GLPrefixAPunct;
            }

            switch (sFldNme)
            {
                case "Commitment":
                    sQry = "SELECT Commitment, Vendor, Job FROM JCM_MASTER__COMMITMENT" + " WHERE Commitment=" + CommonFunctions.QNApos(sKey) + " AND Closed=0";
                    break;

                case "Job":
                    if (!string.IsNullOrEmpty(strJobIn))
                    {
                        if (strJobIn.Contains("\'" + sKey + "\'") == false)
                        {
                            sRetVal = "Your TimberScan permissions do not allow you access to this job.";
                            return sRetVal;
                        }
                    }
                    sQry = "SELECT Cost_Account_Group, Cost_Account_Prefix, Status FROM JCM_MASTER__JOB WHERE Job=" + CommonFunctions.QNApos(sKey);
                    if (bUseToDoList == true)
                    {
                        sTScanQry = "SELECT Job FROM tblToDoList WHERE ConnectionID=" + ConnectionID + " AND (JobDescription IS NOT NULL) AND (AddDelete IS NULL) AND Job=" + CommonFunctions.QNApos(sKey);
                    }
                    break;

                case "Extra":
                    sQry = "SELECT Extra FROM JCM_MASTER__EXTRA WHERE Job=" + CommonFunctions.QNApos(sJob) + " AND Extra=" + CommonFunctions.QNApos(sKey);
                    break;

                case "Cost_Code":
                    sQry = "SELECT Cost_Code FROM JCM_MASTER__COST_CODE" + " WHERE Job=" + CommonFunctions.QNApos(sJob) + " AND Cost_Code=" + CommonFunctions.QNApos(sKey);
                    if (this.StandardCostCodeTable == true && this.CostCodeSec2Length > 0)
                    {
                        sQry = sQry + " AND Group_Cost_Code=0";
                    }
                    if (bUseToDoList == true)
                    {
                        sTScanQry = "SELECT CostCode FROM tblToDoList WHERE ConnectionID=" + ConnectionID + " AND (Category IS NULL) AND (AddDelete IS NULL)" + " AND Job=" + CommonFunctions.QNApos(sJob) + " AND CostCode=" + CommonFunctions.QNApos(sKey);
                    }
                    if (!string.IsNullOrEmpty(sExtra))
                    {
                        sQry = sQry + " AND Extra=" + CommonFunctions.QNApos(sExtra);
                        if (!string.IsNullOrEmpty(sTScanQry))
                        {
                            sTScanQry = sTScanQry + " AND Extra=" + CommonFunctions.QNApos(sExtra);
                        }
                    }
                    else
                    {
                        sQry = sQry + " AND Extra=\'\'";
                        if (!string.IsNullOrEmpty(sTScanQry))
                        {
                            sTScanQry = sTScanQry + " AND Extra IS NULL";
                        }
                    }
                    break;

                case "Category":
                    sQry = "SELECT Cost_Account FROM JCM_MASTER__CATEGORY" + " WHERE Job=" + CommonFunctions.QNApos(sJob) + " AND Cost_Code=" + CommonFunctions.QNAposSt(sCostCode) + " AND Category=" + CommonFunctions.QNApos(sKey);
                    if (bUseToDoList == true)
                    {
                        sTScanQry = "SELECT Category FROM tblToDoList WHERE ConnectionID=" + ConnectionID + " AND (AddDelete IS NULL) AND Job=" + CommonFunctions.QNApos(sJob) + " AND CostCode=" + CommonFunctions.QNApos(sCostCode) + " AND Category=" + CommonFunctions.QNApos(sKey);
                    }
                    if (!string.IsNullOrEmpty(sExtra))
                    {
                        sQry = sQry + " AND Extra=" + CommonFunctions.QNApos(sExtra);
                        if (!string.IsNullOrEmpty(sTScanQry))
                        {
                            sTScanQry = sTScanQry + " AND Extra=" + CommonFunctions.QNApos(sExtra);
                        }
                    }
                    else
                    {
                        sQry = sQry + " AND Extra=\'\'";
                        if (!string.IsNullOrEmpty(sTScanQry))
                        {
                            sTScanQry = sTScanQry + " AND Extra IS NULL";
                        }
                    }
                    break;

                case "Equipment":
                    sQry = "SELECT Equipment, Status FROM EQM_MASTER__EQUIPMENT WHERE Equipment=" + CommonFunctions.QNApos(sKey);
                    break;

                case "Cost_Code_295":
                    sQry = "SELECT Cost_Code FROM EQS_STANDARD__STANDARD_COST_CODE WHERE Cost_Code=" + CommonFunctions.QNApos(sKey);
                    break;

                case "Standard_Item":
                    sQry = "SELECT Standard_Item FROM BLS_STANDARD__STANDARD_ITEM WHERE Standard_Item=" + CommonFunctions.QNApos(sKey);
                    break;

                case "Company":
                    if (!string.IsNullOrEmpty(strPrefixIn))
                    {
                        if (strPrefixIn.Contains("\'" + sKey + "\'") == false)
                        {
                            sGlPrexDesc = this.GLMasterLevels == 1 ? this.GLMasterPrefixADesc : (this.GLMasterLevels == 2 ? this.GLMasterPrefixABDesc : this.GLMasterPrefixABCDesc);
                            sRetVal = "Your TimberScan permissions do not allow you access to this " + sGlPrexDesc + ".";
                            return sRetVal;
                        }
                    }
                    switch (this.GLPrefixType)
                    {
                        case "PrefixA":
                            sQry = "SELECT Account_Prefix_A FROM GLM_MASTER__ACCOUNT_PREFIX_A" + " WHERE Account_Prefix_A=" + CommonFunctions.QNApos(sKey);
                            break;
                        case "PrefixB":
                            sQry = "SELECT Account_Prefix_AB FROM GLM_MASTER__ACCOUNT_PREFIX_AB" + " WHERE Account_Prefix_AB=" + CommonFunctions.QNApos(sKey);
                            break;
                        case "PrefixC":
                            sQry = "SELECT Account_Prefix_ABC FROM GLM_MASTER__ACCOUNT_PREFIX_ABC" + " WHERE Account_Prefix_ABC=" + CommonFunctions.QNApos(sKey);
                            break;
                    }

                    break;

                case "Expense_Account":
                    //if (this.grdDetail.FieldLayouts[0].Fields["Company"].Visibility == Visibility.Visible)
                    //if(isCompanyVisible)
                    //{
                    //    sGlPreFix = string.IsNullOrEmpty(sCompany) ? "" : sCompany.Trim();
                    //    if (!string.IsNullOrEmpty(sGlPreFix))
                    //    {
                    //        sGlPreFix = sGlPreFix + sPunct;
                    //    }
                    //    sGLAcct = sGlPreFix + sKey;
                    //}
                    //else
                    //{
                    sGLAcct = sKey;
                    if (!string.IsNullOrEmpty(strPrefixIn))
                    {
                        sGlPreFix = this.DetermineGLPrefix(sGLAcct, ConnectionID);
                        if (!strPrefixIn.Contains("\'" + sGlPreFix + "\'"))
                        {
                            sGlPrexDesc = this.GLMasterLevels == 1 ? this.GLMasterPrefixADesc : (this.GLMasterLevels == 2 ? this.GLMasterPrefixABDesc : this.GLMasterPrefixABCDesc);
                            sRetVal = "Your TimberScan permissions do not allow you access to G/L Accounts for this " + sGlPrexDesc + ".";
                            return sRetVal;
                        }
                    }
                    //}
                    //if (TimberScan.Properties.Extended.Default.UseTscanGLAccounts == true && TimberScan.Properties.Extended.Default.TSyncActive == false)
                    //{
                    //    sQry = "SELECT Account FROM tblGLAccounts" + " WHERE Account=" + CommonFunctions.QNApos(sGLAcct.ToUpper()) + " AND ConnectionID=" + ConnectionID;
                    //}
                    //else
                    //{
                    sQry = "SELECT Account FROM GLM_MASTER__ACCOUNT WHERE Account=" + CommonFunctions.QNApos(sGLAcct);
                    //}
                    if (bUseToDoList == true)
                    {
                        sTScanQry = "SELECT Account FROM tblToDoList WHERE ConnectionID=" + ConnectionID + " AND (AddDelete IS NULL) AND Account=" + CommonFunctions.QNApos(sGLAcct);
                    }
                    break;

                case "Accounts_Payable_Account":
                    //strAPGLPrefix = DetermineAPGLPrefix(isCompanyVisible ? sCompany : sExpAcct, ConnectionID);
                    strAPGLPrefix = DetermineAPGLPrefix(sExpAcct, ConnectionID);
                    sGLAcct = strAPGLPrefix + sPunct + sKey;
                    //if (TimberScan.Properties.Extended.Default.UseTscanGLAccounts == true && TimberScan.Properties.Extended.Default.TSyncActive == false)
                    //{
                    //    sQry = "SELECT Account FROM tblGLAccounts" + " WHERE Account=" + CommonFunctions.QNApos(sGLAcct.ToUpper()) + " AND ConnectionID=" + ConnectionID;
                    //}
                    //else
                    //{
                    sQry = "SELECT Account FROM GLM_MASTER__ACCOUNT WHERE Account=" + CommonFunctions.QNApos(sGLAcct);
                    //}
                    if (bUseToDoList == true)
                    {
                        sTScanQry = "SELECT Account FROM tblToDoList WHERE ConnectionID=" + ConnectionID + " AND (AddDelete IS NULL) AND Account=" + CommonFunctions.QNApos(sGLAcct);
                    }
                    break;

                case "Tax_Group":
                    sQry = "SELECT Tax_Group FROM TXM_MASTER__TAX_GROUP WHERE Tax_Group=" + CommonFunctions.QNApos(sKey);
                    break;

                case "Authorization":
                    sQry = "SELECT Authorization FROM APM_MASTER__AUTHORIZATION WHERE Authorization=" + CommonFunctions.QNApos(sKey);
                    break;
            }

            DataTable dtRec = new DataTable();
            DataTable dtTScan = new DataTable();

            if ("Expense_Account|Accounts_Payable_Account".Contains(sFldNme) == true)
            {
                //if (TimberScan.Properties.Extended.Default.TSyncActive == false && TimberScan.Properties.Extended.Default.UseTscanGLAccounts == false)
                //{
                //    TimberScanDataAccess.DataAcessHelper timberlineDataAccess = modImage.TimberlineStandardData;//new TimberScanDataAccess.DataAcessHelper(modImage.SystemConnections.TimberlineStdConnectionString , TimberScanDataAccess.DataAccessType.Odbc);
                //    timberlineDataAccess.FillDataTable(dtRec, sQry, null, true, false);
                //}
                //else if (TimberScan.Properties.Extended.Default.TSyncActive == true)
                //{
                sQry = sQry + " AND ConnectionID=" + ConnectionID;
                //TimberScanDataAccess.DataAcessHelper timberSyncDataAccess = new TimberScanDataAccess.DataAcessHelper(TimberScan.Properties.Extended.Default.TSyncConnectionString, TimberScanDataAccess.DataAccessType.SqlServer);
                //timberSyncDataAccess.FillDataTable(dtRec, sQry, null, true, false);
                //}
                //else
                //{
                //    TimberScanDataAccess.DataAcessHelper timberScanDataAccess = new TimberScanDataAccess.DataAcessHelper(TimberScan.Properties.Extended.Default.TimberScanConnectionString, TimberScanDataAccess.DataAccessType.SqlServer);
                //    timberScanDataAccess.FillDataTable(dtRec, sQry, null, true, false);
                //}

                APIUtilDataAccessHelper.TimberSynchDataDynamic(ConnectionID).FillDataTable(dtRec, sQry, null, true, false);
            }
            else
            {
                //if (TimberScan.Properties.Extended.Default.TSyncActive == true)
                //{
                sQry = sQry + " AND ConnectionID=" + ConnectionID;
                //TimberScanDataAccess.DataAcessHelper timberSyncDataAccess = new TimberScanDataAccess.DataAcessHelper(TimberScan.Properties.Extended.Default.TSyncConnectionString, TimberScanDataAccess.DataAccessType.SqlServer);
                //timberSyncDataAccess.FillDataTable(dtRec, sQry, null, true, false);
                //}
                //else
                //{
                //    TimberScanDataAccess.DataAcessHelper timberlineDataAccess = modImage.TimberlineStandardData;//new TimberScanDataAccess.DataAcessHelper(modImage.SystemConnections.TimberlineStdConnectionString , TimberScanDataAccess.DataAccessType.Odbc);
                //    timberlineDataAccess.FillDataTable(dtRec, sQry, null, true, false);
                //}
                APIUtilDataAccessHelper.TimberSynchDataDynamic(ConnectionID).FillDataTable(dtRec, sQry, null, true, false);
            }
            if (dtRec.Rows.Count == 0)
            {
                //if there are no GL prefixes defined then we cannot use the to-do list since you can't add GL accounts "on the fly" in Timberline
                //therefore, we just return an invalid account message
                if ("Expense_Account|Accounts_Payable_Account".Contains(sFldNme) && this.GLMasterPrefixALength == 0)
                {
                    sRetVal = "Account " + sGLAcct + " not set up.";
                    return sRetVal;
                }
                if (!string.IsNullOrEmpty(sTScanQry))
                {
                    //TimberScanDataAccess.DataAcessHelper timberScanDataAccess = new TimberScanDataAccess.DataAcessHelper(TimberScan.Properties.Extended.Default.TimberScanConnectionString, TimberScanDataAccess.DataAccessType.SqlServer);
                    //timberScanDataAccess.FillDataTable(dtTScan, sTScanQry, null, true, false);
                    APIUtilDataAccessHelper.TimberScanData.FillDataTable(dtTScan, sTScanQry, null, true, false);
                    if (dtTScan.Rows.Count == 0)
                    {
                        sRetVal = "NoCodeMatch";
                        //if we are here, the account is not in G/L or ToDoList
                        //we check to see if it is a valid base account and if the GLPrefix is valid if the company field
                        //is not displayed
                        if ("Expense_Account|Accounts_Payable_Account".Contains(sFldNme) == true)
                        {
                            sFullAcct = sGLAcct;
                            if (this.GLPrefixALength > 0)
                            {
                                int rtLen = this.GLBaseLength + this.GLSuffixLength + (this.GLSuffixLength > 0 ? 1 : 0);
                                sGLAcct = sGLAcct.Substring(rtLen > sGLAcct.Length ? 0 : sGLAcct.Length - rtLen);
                            }
                            if (sFldNme == "Accounts_Payable_Account")
                            {
                                if (string.IsNullOrEmpty(strAPGLPrefix))
                                {
                                    //strAPGLPrefix = DetermineAPGLPrefix(isCompanyVisible ? sCompany : sExpAcct, ConnectionID);
                                    strAPGLPrefix = DetermineAPGLPrefix(sExpAcct, ConnectionID);
                                }

                                sGlPreFix = strAPGLPrefix;
                            }
                            //else if (this.grdDetail.FieldLayouts[0].Fields["Company"].Visibility != Visibility.Visible && sFldNme == "Expense_Account")
                            //else if (!isCompanyVisible && sFldNme == "Expense_Account")
                            else if (!apDistSettings.GL_Prefix && sFldNme == "Expense_Account")
                            {
                                if (sFullAcct.Length - this.GLBaseLength - this.GLSuffixLength - 1 > 0)
                                {
                                    int ltLen = sFullAcct.Length - this.GLBaseLength - this.GLSuffixLength - 1;
                                    sGlPreFix = sFullAcct.Substring(0, ltLen);
                                }
                                else
                                {
                                    sGlPreFix = "";
                                    sRetVal = "Invalid Code";
                                    return sRetVal;
                                }
                                sPrfA = "";
                                sPrfAB = "";
                                sPrfABC = "";
                                if (this.GLPrefixABCLength > 0)
                                {
                                    sPrfABC = sGlPreFix;
                                    if (sPrfABC.Substring(sPrfABC.Length - 1 > 0 ? sPrfABC.Length - 1 : 0) == this.GLPrefixABCPunct)
                                    {
                                        sPrfABC = sPrfABC.Substring(0, sPrfABC.Length - 1);
                                    }
                                    sPrfAB = sPrfABC.Substring(0, sPrfABC.LastIndexOf(this.GLPrefixABPunct));
                                    sPrfA = sPrfAB.Substring(0, sPrfAB.LastIndexOf(this.GLPrefixAPunct));
                                }
                                else if (this.GLPrefixABLength > 0)
                                {
                                    sPrfAB = sGlPreFix;
                                    if (sPrfAB.Substring(sPrfAB.Length - 1 > 0 ? sPrfAB.Length - 1 : 0) == this.GLPrefixABPunct)
                                    {
                                        sPrfAB = sPrfAB.Substring(0, sPrfAB.Length - 1);
                                    }
                                    sPrfA = sPrfAB.Substring(0, sPrfAB.LastIndexOf(this.GLPrefixAPunct));
                                }
                                else if (this.GLPrefixALength > 0)
                                {
                                    sPrfA = sGlPreFix;
                                    if (sPrfA.Substring(sPrfA.Length - 1 > 0 ? sPrfA.Length - 1 : 0) == this.GLPrefixAPunct)
                                    {
                                        sPrfA = sPrfA.Substring(0, sPrfA.Length - 1);
                                    }
                                }
                                sQry = "SELECT Account_Prefix_A FROM GLM_MASTER__ACCOUNT_PREFIX_A" + " WHERE Account_Prefix_A=" + CommonFunctions.QNApos(sPrfA);
                                //if (TimberScan.Properties.Extended.Default.TSyncActive)
                                //{
                                sQry = sQry + " AND ConnectionID=" + ConnectionID;
                                //TimberScanDataAccess.DataAcessHelper timberSyncDataAccess = new TimberScanDataAccess.DataAcessHelper(TimberScan.Properties.Extended.Default.TSyncConnectionString, TimberScanDataAccess.DataAccessType.SqlServer);
                                //timberSyncDataAccess.FillDataTable(dtRec, sQry, null, true, false);
                                APIUtilDataAccessHelper.TimberSynchDataDynamic(ConnectionID).FillDataTable(dtRec, sQry, null, true, false);
                                //}
                                //else
                                //{
                                //    TimberScanDataAccess.DataAcessHelper timberlineDataAccess = modImage.TimberlineStandardData;//new TimberScanDataAccess.DataAcessHelper(modImage.SystemConnections.TimberlineStdConnectionString, TimberScanDataAccess.DataAccessType.Odbc);
                                //    timberlineDataAccess.FillDataTable(dtRec, sQry, null, true, false);
                                //}

                                if (dtRec.Rows.Count == 0)
                                {
                                    sRetVal = this.GLMasterPrefixADesc + " " + sPrfA + " is not set up.";
                                    return sRetVal;
                                }
                                if (!string.IsNullOrEmpty(sPrfAB))
                                {
                                    sQry = "SELECT Account_Prefix_AB FROM GLM_MASTER__ACCOUNT_PREFIX_AB" + " WHERE Account_Prefix_AB=" + CommonFunctions.QNApos(sPrfAB);
                                    //if (TimberScan.Properties.Extended.Default.TSyncActive)
                                    //{
                                    sQry = sQry + " AND ConnectionID=" + ConnectionID;
                                    //    TimberScanDataAccess.DataAcessHelper timberSyncDataAccess = new TimberScanDataAccess.DataAcessHelper(TimberScan.Properties.Extended.Default.TSyncConnectionString, TimberScanDataAccess.DataAccessType.SqlServer);
                                    //    timberSyncDataAccess.FillDataTable(dtRec, sQry, null, true, false);
                                    APIUtilDataAccessHelper.TimberSynchDataDynamic(ConnectionID).FillDataTable(dtRec, sQry, null, true, false);
                                    //}
                                    //else
                                    //{
                                    //    TimberScanDataAccess.DataAcessHelper timberlineDataAccess = modImage.TimberlineStandardData;//new TimberScanDataAccess.DataAcessHelper(modImage.SystemConnections.TimberlineStdConnectionString, TimberScanDataAccess.DataAccessType.Odbc);
                                    //    timberlineDataAccess.FillDataTable(dtRec, sQry, null, true, false);
                                    //}
                                    if (dtRec.Rows.Count == 0)
                                    {
                                        sRetVal = this.GLMasterPrefixABDesc + " " + sPrfAB + " is not set up.";
                                        return sRetVal;
                                    }
                                }
                                if (!string.IsNullOrEmpty(sPrfABC))
                                {
                                    sQry = "SELECT Account_Prefix_ABC FROM GLM_MASTER__ACCOUNT_PREFIX_ABC" + " WHERE Account_Prefix_ABC=" + CommonFunctions.QNApos(sPrfABC);
                                    //if (TimberScan.Properties.Extended.Default.TSyncActive)
                                    //{
                                    sQry = sQry + " AND ConnectionID=" + ConnectionID;
                                    //    TimberScanDataAccess.DataAcessHelper timberSyncDataAccess = new TimberScanDataAccess.DataAcessHelper(TimberScan.Properties.Extended.Default.TSyncConnectionString, TimberScanDataAccess.DataAccessType.SqlServer);
                                    //    timberSyncDataAccess.FillDataTable(dtRec, sQry, null, true, false);
                                    APIUtilDataAccessHelper.TimberSynchDataDynamic(ConnectionID).FillDataTable(dtRec, sQry, null, true, false);
                                    //}
                                    //else
                                    //{
                                    //    TimberScanDataAccess.DataAcessHelper timberlineDataAccess = modImage.TimberlineStandardData;//new TimberScanDataAccess.DataAcessHelper(modImage.SystemConnections.TimberlineStdConnectionString, TimberScanDataAccess.DataAccessType.Odbc);
                                    //    timberlineDataAccess.FillDataTable(dtRec, sQry, null, true, false);
                                    //}
                                    if (dtRec.Rows.Count == 0)
                                    {
                                        sRetVal = this.GLMasterPrefixABCDesc + " " + sPrfABC + " is not set up.";
                                        return sRetVal;
                                    }
                                }
                            }
                            //now look for base account
                            sQry = "SELECT Base_Account_Title FROM GLM_MASTER__BASE_ACCOUNT WHERE Base_Account=" + CommonFunctions.QNApos(sGLAcct);
                            //if (TimberScan.Properties.Extended.Default.TSyncActive)
                            //{
                            sQry = sQry + " AND ConnectionID=" + ConnectionID;
                            //TimberScanDataAccess.DataAcessHelper timberSyncDataAccess = new TimberScanDataAccess.DataAcessHelper(TimberScan.Properties.Extended.Default.TSyncConnectionString, TimberScanDataAccess.DataAccessType.SqlServer);
                            //timberSyncDataAccess.FillDataTable(dtRec, sQry, null, true, false);
                            APIUtilDataAccessHelper.TimberSynchDataDynamic(ConnectionID).FillDataTable(dtRec, sQry, null, true, false);
                            //}
                            //else
                            //{
                            //    TimberScanDataAccess.DataAcessHelper timberlineDataAccess = modImage.TimberlineStandardData;//new TimberScanDataAccess.DataAcessHelper(modImage.SystemConnections.TimberlineStdConnectionString, TimberScanDataAccess.DataAccessType.Odbc);
                            //    timberlineDataAccess.FillDataTable(dtRec, sQry, null, true, false);
                            //}
                            if (dtRec.Rows.Count == 0)
                            {
                                sRetVal = "Base account " + sGLAcct + " is not set up.";
                                return sRetVal;
                            }
                            else
                            {
                                sBaseAcctDesc = dtRec.Rows[0]["Base_Account_Title"].ToString().Trim();
                            }
                            //if we are here, the prefix is OK and the Base account is OK - we have to see if the account is set up for this company
                            if (!string.IsNullOrEmpty(sGlPreFix))
                            {
                                dtRec.Clear(); //08-26-2013 - Sanket - clear out table before we use it next time -- fixed Convest GL Account not found issue
                                if (sGlPreFix.Substring(sGlPreFix.Length - 1, 1) == sPunct) //if (sGlPreFix.LastIndexOf(sPunct) > 0)
                                {
                                    sGlPreFix = sGlPreFix.Substring(0, sGlPreFix.Length - 1);
                                }
                                sQry = "SELECT Account FROM GLM_MASTER__ACCOUNT WHERE Account=" + CommonFunctions.QNApos(sGlPreFix + sPunct + sGLAcct);
                                //if (TimberScan.Properties.Extended.Default.TSyncActive)
                                //{
                                sQry = sQry + " AND ConnectionID=" + ConnectionID;
                                //TimberScanDataAccess.DataAcessHelper timberSyncDataAccess = new TimberScanDataAccess.DataAcessHelper(TimberScan.Properties.Extended.Default.TSyncConnectionString, TimberScanDataAccess.DataAccessType.SqlServer);
                                //timberSyncDataAccess.FillDataTable(dtRec, sQry, null, true, false);
                                APIUtilDataAccessHelper.TimberSynchDataDynamic(ConnectionID).FillDataTable(dtRec, sQry, null, true, false);
                                //}
                                //else
                                //{
                                //    TimberScanDataAccess.DataAcessHelper timberlineDataAccess = modImage.TimberlineStandardData;//new TimberScanDataAccess.DataAcessHelper(modImage.SystemConnections.TimberlineStdConnectionString, TimberScanDataAccess.DataAccessType.Odbc);
                                //    timberlineDataAccess.FillDataTable(dtRec, sQry, null, true, false);
                                //}
                                if (dtRec.Rows.Count == 0)
                                {
                                    sRetVal = "Account " + sGlPreFix + "-" + sGLAcct + " not set up.";
                                }
                                else
                                {
                                    if (dtRec.Rows[0]["Account"].ToString().Trim() == "")
                                    {
                                        sRetVal = "Account " + sGlPreFix + "-" + sGLAcct + " not set up.";
                                    }
                                    else
                                    {
                                        sRetVal = "";
                                    }
                                }
                            }
                            else
                            {
                                sRetVal = "";
                            }
                        }
                    }
                    else
                    {
                        sRetVal = "";
                    }
                }
                else
                {
                    sRetVal = sFldNme + " not found.";
                }
            }
            else
            {
                sRetVal = "";
                if (sFldNme == "Job")
                {
                    if (dtRec.Rows[0]["Status"].ToString().ToUpper() == "CLOSED")
                    {
                        sRetVal = "JobClosed";
                    }
                }
                else if (sFldNme == "Equipment")
                {
                    if (dtRec.Rows[0]["Status"].ToString().ToUpper() == "OUT OF SERVICE")
                    {
                        sRetVal = "Equipment " + dtRec.Rows[0]["Equipment"].ToString() + " is inactive. Contact your Timberscan administrator.";
                        //if (!(modImage.UserPermission.IEOverride))
                        //{
                        //    sRetVal = "Equipment " + dtRec.Rows[0]["Equipment"].ToString() + " is inactive. Contact your Timberscan administrator.";
                        //    //sRetVal = "Equipment " + dtRec.Rows[0]["Equipment"].ToString() + " is inactive. You do not have permission to override inactive equipment. Contact your Timberscan administrator.";
                        //}
                        //else
                        //{
                        //    sRetVal = string.Format("Equipment {0} is inactive?  Do you want to continue and code this distribution with inactive equipment? ", dtRec.Rows[0]["Equipment"].ToString());
                        //    if (result == MessageBoxResult.No)
                        //    {
                        //        // get back to list of equipments
                        //        sRetVal = "refocus";
                        //    }
                        //}
                    }
                }
                else if (sFldNme == "Commitment")
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(strJobIn))
                        {
                            sJob = (String.IsNullOrEmpty(dtRec.Rows[0]["Job"].ToString()) ? "" : dtRec.Rows[0]["Job"].ToString());
                            if (!string.IsNullOrEmpty(sJob) && Strings.InStr(strJobIn, "\'" + sJob + "\'", 0) == 0)
                            {
                                sRetVal = "Your TimberScan permissions do not allow you access to the job associated with this " + apDistSettings.Commitment_Desc + ".";
                                return sRetVal;
                            }
                        }
                        if (dtRec.Rows[0]["Vendor"].ToString().ToUpper().Trim() != CommonFunctions.TestNullString(sVendor).ToString().ToUpper().Trim())
                        {
                            sRetVal = "WrongVendor";
                        }
                    }
                    catch
                    {
                        sRetVal = "WrongVendor";
                    }
                }
                else if (sFldNme == "Company" && !string.IsNullOrEmpty(sEquip))
                {
                    sRetVal = "";
                    for (iLoop = 0; iLoop <= dtRec.Columns.Count - 1; iLoop++)
                    {
                        if (iLoop > 0)
                        {
                            sRetVal = sRetVal + "|" + dtRec.Rows[0][iLoop].ToString().Trim();
                        }
                        else
                        {
                            sRetVal = sRetVal + dtRec.Rows[0][iLoop].ToString().Trim();
                        }
                    }
                    //bool equipVisible = (this.grdDetail.FieldLayouts[0].Fields["Equipment"].Visibility == Visibility.Visible) ? true : false;
                    //sGlPreFix = objAPDistRoutines.DetermineAPDistGLPrefix(sJob, sExtra, sCostCode, sCategory, ref sEquip, sEQCostCode, equipVisible, adoApVendorRs);

                    //VERY IMPORTANT CHANGE! - taking away a fiscal settings check
                    ////if (sRetVal != sGlPreFix)     2.3.74 
                    //clsTS_CTL ctl = new clsTS_CTL();
                    //if (ctl.CalculateFiscalEntityPrefix(sRetVal, false) != ctl.CalculateFiscalEntityPrefix(sGlPreFix, false))
                    //{
                    //    TimberScan.Controls.TMessageBox.Show("Prefix must be: " + sGlPreFix, "TimberScan", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    //    sRetVal = "Bad Prefix";
                    //}
                    //else
                    //{
                    //    sRetVal = "";
                    //}

                    //ctl = null;
                    sRetVal = ""; //the commented code above for fiscal checks was just replaced by this line
                    //VERY IMPORTANT CHANGE! - taking away a fiscal settings check - 12/11/2012 Irene -----------------------------------------------

                    //sRetVal = sRetVal;
                }
            }

            return sRetVal;
        }

       

        private string StripGLAccountPunctuation(string sAcct, bool bIncludePunctuation)
        //sAcct is GL Account w/o suffix
        {
            int iPos = 0;
            int iCnt = 0;
            string sChr = String.Empty;
            string sItm = String.Empty;
            string sNewItem = String.Empty;
            string returnValue = String.Empty;
            string[] sTestChars;
            int testInt = 0;

            if (this.GLPrefixALength == 0)
            {
                return returnValue;
            }

            if (this.GLPrefixABCLength > 0)
            {
                sTestChars = new string[3];
                sTestChars[0] = this.GLPrefixAPunct;
                sTestChars[1] = this.GLPrefixABPunct;
                sTestChars[2] = this.GLPrefixABCPunct;
            }
            else if (this.GLPrefixABLength > 0)
            {
                sTestChars = new string[2];
                sTestChars[0] = this.GLPrefixAPunct;
                sTestChars[1] = this.GLPrefixABPunct;
            }
            else
            {
                sTestChars = new string[1];
                sTestChars[0] = this.GLPrefixAPunct;
            }

            sItm = sAcct.Trim();

            for (iPos = 1; iPos <= sItm.Length; iPos++)
            {
                sChr = Strings.Mid(sItm, iPos, 1);//sItm.Substring(iPos, 1);
                if (iCnt <= (sTestChars.Length - 1))
                {
                    if (sChr != sTestChars[iCnt])
                    {
                        if (((char.IsUpper(sChr, 0) && char.IsLetter(sChr, 0)) || int.TryParse(sChr, out testInt) || sChr == " "))
                        {
                            sNewItem += sChr;
                        }
                        else
                        {
                            returnValue = "Invalid character";
                            return returnValue;
                        }
                    }
                    else
                    {
                        if (bIncludePunctuation == true)
                        {
                            sNewItem += "|";
                        }
                        iCnt++;
                    }
                }
                else
                {
                    sNewItem += sChr;
                }
            }
            if (iCnt == 0 && bIncludePunctuation == true)  //this means the account was not punctuated, but we have to return it punctuated
            {
                if (sNewItem.Length > this.GLBaseLength)
                {
                    returnValue = Strings.Right(sNewItem, this.GLBaseLength);//sNewItem.Substring(sNewItem.Length - this.GLBaseLength + 1, this.GLBaseLength);
                    sNewItem = Strings.Left(sNewItem, sNewItem.Length - GLBaseLength);//sNewItem.Substring(1, sNewItem.Length - this.GLBaseLength);
                }
                else
                {
                    returnValue = sNewItem;
                    return returnValue;
                }

                if (this.GLPrefixABCLength > 0)
                {
                    if (sNewItem.Length > this.GLPrefixABCLength)
                    {
                        returnValue = Strings.Right(sNewItem, this.GLPrefixABCLength) + "|" + returnValue;//sNewItem.Substring(sNewItem.Length - this.GLPrefixABCLength + 1, this.GLPrefixABCLength) + "|" + returnValue;
                        sNewItem = Strings.Left(sNewItem, sNewItem.Length - this.GLPrefixABCLength);//sNewItem.Substring(1, sNewItem.Length - this.GLPrefixABCLength);
                    }
                    else
                    {
                        sNewItem += "|" + returnValue;
                        returnValue = sNewItem;
                        return returnValue;
                    }
                }
                if (this.GLPrefixABLength > 0)
                {
                    if (sNewItem.Length > this.GLPrefixABLength)
                    {
                        returnValue = Strings.Right(sNewItem, this.GLPrefixABLength) + "|" + returnValue;//sNewItem.Substring(sNewItem.Length - this.GLPrefixABLength + 1, this.GLPrefixABLength) + "|" + returnValue;
                        sNewItem = Strings.Left(sNewItem, sNewItem.Length - this.GLPrefixABLength);
                    }
                    else
                    {
                        sNewItem += "|" + returnValue;
                        returnValue = sNewItem;
                        return returnValue;
                    }
                }
                sNewItem += "|" + returnValue;
                returnValue = sNewItem;
            }
            return sNewItem;
        }

        private int CountAccountDecimalPoints(String sAcct)
        {
            int iCnt = 0;
            int iPos = 1;

            while (sAcct.IndexOf(this.GLBasePunct, iPos) > 0)
            {
                iCnt++;
                iPos = sAcct.IndexOf(this.GLBasePunct, iPos) + 1;

                if (iPos >= sAcct.Length)
                {
                    break;
                }
            }
            return iCnt;
        }

        public string FormatGLPrefix(ref string sPrfx)
        {
            string returnValue = null;
            string sNewItem;
            string[] sSections;
            int i;
            string sNewPrfx;

            //If Trim(sPrfx) = "" Then FormatGLPrefix = "" : Exit Function

            sNewItem = StripPunctuation(sPrfx, true);
            sSections = sNewItem.Split('|');

            sNewItem = "";

            sNewPrfx = "";
            if ((sSections.Length - 1) > 0) //muliple prefix levels formatted
            {
                if ((sSections.Length - 1) < (intGLPrefixCnt - 1))
                {
                    sNewItem = "Incorrect Full Prefix format.|Insufficient digits entered.|Correct format is " + strGLPrefixFormat + ".|Reenter Full Prefix.";
                }
                else
                    if ((sSections.Length - 1) > (intGLPrefixCnt - 1))
                    {
                        sNewItem = "Incorrect Full Prefix format.|Invalid character.|Correct format is " + strGLPrefixFormat + ".|Reenter Full Prefix.";
                    }
                    else
                    {
                        if (sSections[0].Length > mvarGLPrefixALength)
                        {
                            sNewItem = "Incorrect Full Prefix format.|Too many digits entered.|Correct format is " + strGLPrefixFormat + ".|Reenter Full Prefix.";
                        }
                        else
                            if (sSections[0].Length == 0)
                            {
                                sNewItem = "Incorrect Full Prefix format.|Insufficient digits entered.|Correct format is " + strGLPrefixFormat + ".|Reenter Full Prefix.";
                            }
                        if (sNewItem == "")
                        {
                            if (sSections[1].Length > mvarGLPrefixABLength)
                            {
                                sNewItem = "Incorrect Full Prefix format.|Too many digits entered.|Correct format is " + strGLPrefixFormat + ".|Reenter Full Prefix.";
                            }
                            else
                                if (sSections[1].Length < mvarGLPrefixABLength)
                                {
                                    sNewItem = "Incorrect Full Prefix format.|Insufficient digits entered.|Correct format is " + strGLPrefixFormat + ".|Reenter Full Prefix.";
                                }
                        }
                        if (sNewItem == "" && intGLPrefixCnt == 3)
                        {
                            if (sSections[2].Length > mvarGLPrefixABCLength)
                            {
                                sNewItem = "Incorrect Full Prefix format.|Too many digits entered.|Correct format is " + strGLPrefixFormat + ".|Reenter Full Prefix.";
                            }
                            else
                                if (sSections[2].Length < mvarGLPrefixABCLength)
                                {
                                    sNewItem = "Incorrect Full Prefix format.|Insufficient digits entered.|Correct format is " + strGLPrefixFormat + ".|Reenter Full Prefix.";
                                }
                        }
                    }
                if (sNewItem.Length > 0)
                {
                    returnValue = sNewItem;
                    //Exit Function
                }
            }
            for (i = 0; i <= (sSections.Length - 1); i++)
            {
                sNewPrfx = sNewPrfx + sSections[i];
            }

            switch (intGLPrefixCnt)
            {
                case 1:
                    //length can't be less for prefixa otherwise it would have been rejected above
                    if (sNewPrfx.Length > mvarGLPrefixALength)
                    {
                        sNewItem = "Incorrect Full Prefix format.|Too many digits entered.|Correct format is " + strGLPrefixFormat + ".|Reenter Full Prefix.";
                    }
                    break;
                case 2:
                    if (sNewPrfx.Length > (mvarGLPrefixALength + mvarGLPrefixABLength))
                    {
                        sNewItem = "Incorrect Full Prefix format.|Too many digits entered.|Correct format is " + strGLPrefixFormat + ".|Reenter Full Prefix.";
                    }
                    else
                        if (sNewPrfx.Length < (1 + mvarGLPrefixABLength))
                        {
                            sNewItem = "Incorrect Full Prefix format.|Insufficient digits entered.|Correct format is " + strGLPrefixFormat + ".|Reenter Full Prefix.";
                        }
                    break;
                case 3:
                    if (sNewPrfx.Length > (mvarGLPrefixALength + mvarGLPrefixABLength + mvarGLPrefixABCLength))
                    {
                        sNewItem = "Incorrect Full Prefix format.|Too many digits entered.|Correct format is " + strGLPrefixFormat + ".|Reenter Full Prefix.";
                    }
                    else
                        if (sNewPrfx.Length < (1 + mvarGLPrefixABLength + mvarGLPrefixABCLength))
                        {
                            sNewItem = "Incorrect Full Prefix format.|Insufficient digits entered.|Correct format is " + strGLPrefixFormat + ".|Reenter Full Prefix.";
                        }
                    break;
            }

            if (sNewItem.Length > 0)
            {
                returnValue = sNewItem;
            }
            else
            {
                returnValue = FormatItem(sNewPrfx, strGLPrefixFormat);
            }
            return returnValue;
        }

        public string FormatGLPrefix(string sPrfx)
        {
            string returnValue = null;
            string sNewItem = string.Empty;
            string[] sSections;
            int i;
            string sNewPrfx;

            //If Trim(sPrfx) = "" Then FormatGLPrefix = "" : Exit Function

            sNewItem = StripPunctuation(sPrfx, true);
            sSections = sNewItem.Split('|');

            sNewItem = "";

            sNewPrfx = "";
            if ((sSections.Length - 1) > 0) //muliple prefix levels formatted
            {
                if ((sSections.Length - 1) < (intGLPrefixCnt - 1))
                {
                    sNewItem = "Incorrect Full Prefix format.|Insufficient digits entered.|Correct format is " + strGLPrefixFormat + ".|Reenter Full Prefix.";
                }
                else
                    if ((sSections.Length - 1) > (intGLPrefixCnt - 1))
                    {
                        sNewItem = "Incorrect Full Prefix format.|Invalid character.|Correct format is " + strGLPrefixFormat + ".|Reenter Full Prefix.";
                    }
                    else
                    {
                        if (sSections[0].Length > mvarGLPrefixALength)
                        {
                            sNewItem = "Incorrect Full Prefix format.|Too many digits entered.|Correct format is " + strGLPrefixFormat + ".|Reenter Full Prefix.";
                        }
                        else
                            if (sSections[0].Length == 0)
                            {
                                sNewItem = "Incorrect Full Prefix format.|Insufficient digits entered.|Correct format is " + strGLPrefixFormat + ".|Reenter Full Prefix.";
                            }
                        if (sNewItem == "")
                        {
                            if (sSections[1].Length > mvarGLPrefixABLength)
                            {
                                sNewItem = "Incorrect Full Prefix format.|Too many digits entered.|Correct format is " + strGLPrefixFormat + ".|Reenter Full Prefix.";
                            }
                            else
                                if (sSections[1].Length < mvarGLPrefixABLength)
                                {
                                    sNewItem = "Incorrect Full Prefix format.|Insufficient digits entered.|Correct format is " + strGLPrefixFormat + ".|Reenter Full Prefix.";
                                }
                        }
                        if (sNewItem == "" && intGLPrefixCnt == 3)
                        {
                            if (sSections[2].Length > mvarGLPrefixABCLength)
                            {
                                sNewItem = "Incorrect Full Prefix format.|Too many digits entered.|Correct format is " + strGLPrefixFormat + ".|Reenter Full Prefix.";
                            }
                            else
                                if (sSections[2].Length < mvarGLPrefixABCLength)
                                {
                                    sNewItem = "Incorrect Full Prefix format.|Insufficient digits entered.|Correct format is " + strGLPrefixFormat + ".|Reenter Full Prefix.";
                                }
                        }
                    }
                if (!string.IsNullOrWhiteSpace(sNewItem))
                {
                    returnValue = sNewItem;

                }
            }

            if (string.IsNullOrWhiteSpace(sNewItem))
            {
                for (i = 0; i <= (sSections.Length - 1); i++)
                {
                    sNewPrfx = sNewPrfx + sSections[i];
                }

                switch (intGLPrefixCnt)
                {
                    case 1:
                        //length can't be less for prefixa otherwise it would have been rejected above
                        if (sNewPrfx.Length > mvarGLPrefixALength)
                        {
                            sNewItem = "Incorrect Full Prefix format.|Too many digits entered.|Correct format is " + strGLPrefixFormat + ".|Reenter Full Prefix.";
                        }
                        break;
                    case 2:
                        if (sNewPrfx.Length > (mvarGLPrefixALength + mvarGLPrefixABLength))
                        {
                            sNewItem = "Incorrect Full Prefix format.|Too many digits entered.|Correct format is " + strGLPrefixFormat + ".|Reenter Full Prefix.";
                        }
                        else
                            if (sNewPrfx.Length < (1 + mvarGLPrefixABLength))
                            {
                                sNewItem = "Incorrect Full Prefix format.|Insufficient digits entered.|Correct format is " + strGLPrefixFormat + ".|Reenter Full Prefix.";
                            }
                        break;
                    case 3:
                        if (sNewPrfx.Length > (mvarGLPrefixALength + mvarGLPrefixABLength + mvarGLPrefixABCLength))
                        {
                            sNewItem = "Incorrect Full Prefix format.|Too many digits entered.|Correct format is " + strGLPrefixFormat + ".|Reenter Full Prefix.";
                        }
                        else
                            if (sNewPrfx.Length < (1 + mvarGLPrefixABLength + mvarGLPrefixABCLength))
                            {
                                sNewItem = "Incorrect Full Prefix format.|Insufficient digits entered.|Correct format is " + strGLPrefixFormat + ".|Reenter Full Prefix.";
                            }
                        break;
                }

                if (!string.IsNullOrEmpty(sNewItem))
                {
                    returnValue = sNewItem;
                }
                else
                {
                    returnValue = FormatItem(sNewPrfx, strGLPrefixFormat);
                }
            }
            return returnValue;
        }

        public string FormatGLBaseAccount(ref string sAcct)
        {
            string returnValue = null;
            string sNewItem;
            string[] sSections;

            //If sAcct = "" Then FormatGLBaseAccount = "" : Exit Function

            sNewItem = StripPunctuation(sAcct, true);
            sSections = sNewItem.Split('|');

            sNewItem = sSections[0];
            if ((sSections.Length - 1) == 1) //there was a suffix punctuation in the string
            {
                if (sSections[0].Length < mvarGLBaseLength)
                {
                    sNewItem = "Incorrect account format.|Insufficient digits entered.|Correct format is " + strGLBaseAccountFormat + ".|Renter Base and Suffix.";
                }
                else
                {
                    sSections[1] = (sSections[1] + "".PadLeft(mvarGLSuffixLength, '0')).Substring(0, mvarGLSuffixLength);

                    if (Strings.Len(sSections[0] + sSections[1]) == (mvarGLBaseLength + mvarGLSuffixLength))
                    {
                        sNewItem = FormatItem(sSections[0] + sSections[1], strGLBaseAccountFormat);
                    }
                    else //it can't be less because we took care of that in the first part of this if-then-else stmt, so it must be greater
                    {
                        sNewItem = "Incorrect account format.|Too many digits before decimal point.|Correct format is " + strGLBaseAccountFormat + ".|Renter Base and Suffix.";
                    }
                }
            }
            else
            {
                if (sSections[0].Length == mvarGLBaseLength)
                {
                    sNewItem = sSections[0];
                    if (mvarGLSuffixLength > 0)
                    {
                        sNewItem = sNewItem + mvarGLBasePunct + new string('0', mvarGLSuffixLength);
                    }
                }
                else
                    if (sSections[0].Length < mvarGLBaseLength)
                    {
                        sNewItem = "Incorrect account format.|Insufficient digits entered.|Correct format is " + strGLBaseAccountFormat + ".|Renter account.";
                    }
                    else
                    {
                        sNewItem = "Incorrect Base format.|Too many digits entered.|Correct format is " + strGLBaseAccountFormat + ".";
                    }
            }
            returnValue = sNewItem;
            return returnValue;
        }

        public string FormatJob(ref string sJob)
        {
            string returnValue = null;
            string sJobFmt;

            if (mvarJobSec1Length == 0 || mvarJobSec2Length == 0) return sJob;
            if (string.IsNullOrWhiteSpace(sJob)) return string.Empty;

            //If mvarJobSec1Length = 0 Or mvarJobSec2Length = 0 Then FormatJob = sJob : Exit Function
            //If Trim(sJob) = "" Then FormatJob = "" : Exit Function

            intSecLen[1] = mvarJobSec1Length;
            strSecPunct[1] = mvarJobSec1Punct;
            intSecLen[2] = mvarJobSec2Length;
            strSecPunct[2] = mvarJobSec2Punct;
            intSecLen[3] = mvarJobSec3Length;
            strSecPunct[3] = "";
            intSecLen[4] = 0;

            sJobFmt = MasterFormat(ref sJob, ref strJobFormat);
            if (sJobFmt == "TooMany")
            {
                returnValue = "Incorrect job format. Too many characters entered. Correct format is " + strJobFormat + ".";
            }
            else
                if (sJobFmt == "TooFew")
                {
                    returnValue = "Incorrect job format. Insufficient digits entered. Correct format is " + strJobFormat + ".";
                }
                
            return returnValue;
        }

        private string MasterFormat(string sItem, string sFormat)
        {
            string returnValue = null;
            int iMaxLen;
            int iMinLen;
            int iLoop;
            string sNewItem;
            string[] sSections;

            //If sItem = "" Then MasterFormat = "" : Exit Function

            sNewItem = StripPunctuation(sItem, true);
            //If sNewItem = "" Then MasterFormat = "" : Exit Function

            sSections = sNewItem.Split('|');

            iMinLen = 1 + intSecLen[2] + intSecLen[3] + intSecLen[4];
            iMaxLen = intSecLen[1] + intSecLen[2] + intSecLen[3] + intSecLen[4];

            //SANKET - 04-08-2013 - Issue # 16168P/17573 - Error received when trying to code equipment to a supporting doc
            if (iMaxLen == 0)
                iMaxLen = sItem.Length;

            //if an item is punctuated in Timberline, Timberline will left pad it with 0's AFTER the first section
            sNewItem = sSections[0];
            if ((sSections.Length - 1) > 0) //item is formatted
            {
                for (iLoop = 1; iLoop <= (sSections.Length - 1); iLoop++)
                {
                    //reminder sSections() is 0 based, intSecLen() is 1 based
                    sNewItem = sNewItem + Strings.Right(new string('0', intSecLen[iLoop + 1]) + sSections[iLoop], intSecLen[iLoop + 1]);
                }
            }

            if (sNewItem.Length >= iMinLen && sNewItem.Length <= iMaxLen)
            {
                if (sFormat != null)
                    returnValue = FormatItem(sNewItem, sFormat);
                else
                    returnValue = sNewItem;
            }
            else
                if (sItem.Length > iMaxLen)
                {
                    returnValue = "TooMany";
                }
                else
                {
                    returnValue = "TooFew";
                }
            return returnValue;
        }

        public string FormatJob(string sJob)
        {
            string returnValue = null;
            string sJobFmt;

            //If mvarJobSec1Length = 0 Or mvarJobSec2Length = 0 Then FormatJob = sJob : Exit Function
            //If Trim(sJob) = "" Then FormatJob = "" : Exit Function

            sJob = sJob.PadLeft(mvarJobSec1Length + mvarJobSec2Length + mvarJobSec3Length, '0');

            intSecLen[1] = mvarJobSec1Length;
            strSecPunct[1] = mvarJobSec1Punct;
            intSecLen[2] = mvarJobSec2Length;
            strSecPunct[2] = mvarJobSec2Punct;
            intSecLen[3] = mvarJobSec3Length;
            strSecPunct[3] = "";
            intSecLen[4] = 0;

            sJobFmt = MasterFormat(ref sJob, ref strJobFormat);
            if (sJobFmt == "TooMany")
            {
                returnValue = "Incorrect job format. Too many characters entered. Correct format is " + strJobFormat + ".";
            }
            else
                if (sJobFmt == "TooFew")
                {
                    returnValue = "Incorrect job format. Insufficient digits entered. Correct format is " + strJobFormat + ".";
                }
                else
                {
                    returnValue = sJobFmt;
                }
            return returnValue;
        }

        public string FormatCostCode(ref string sCostCode)
        {
            string returnValue = null;
            string sCostCodeFmt;
            //If mvarCostCodeSec1Length = 0 Or mvarCostCodeSec2Length = 0 Then FormatCostCode = sCostCode : Exit Function

            intSecLen[1] = mvarCostCodeSec1Length;
            strSecPunct[1] = mvarCostCodeSec1Punct;
            intSecLen[2] = mvarCostCodeSec2Length;
            strSecPunct[2] = mvarCostCodeSec2Punct;
            intSecLen[3] = mvarCostCodeSec3Length;
            strSecPunct[3] = mvarCostCodeSec3Punct;
            intSecLen[4] = mvarCostCodeSec4Length;

            sCostCodeFmt = MasterFormat(ref sCostCode, ref strCostCodeFormat);
            if (sCostCodeFmt == "TooMany")
            {
                returnValue = "Incorrect cost code format. Too many characters entered. Correct format is " + strCostCodeFormat + ". Reenter cost code.";
            }
            else
                if (sCostCodeFmt == "TooFew")
                {
                    returnValue = "Incorrect cost code format. Insufficient digits entered. Correct format is " + strCostCodeFormat + ". Reenter cost code.";
                }
                else
                {
                    returnValue = sCostCodeFmt;
                }
            return returnValue;
        }

        public string FormatEquipment(ref string sEquip)
        {
            string returnValue = null;
            string sEquipFmt;
            //If mvarEQSec1Length = 0 Or mvarEQSec2Length = 0 Then FormatEquipment = sEquip : Exit Function

            intSecLen[1] = mvarEQSec1Length;
            strSecPunct[1] = mvarEQSec1Punct;
            intSecLen[2] = mvarEQSec2Length;
            strSecPunct[2] = mvarEQSec2Punct;
            intSecLen[3] = mvarEQSec3Length;
            strSecPunct[3] = "";
            intSecLen[4] = 0;

            sEquipFmt = MasterFormat(ref sEquip, ref strEQFormat);
            if (sEquipFmt == "TooMany")
            {
                returnValue = "Incorrect Equipment format. Too many characters entered. Correct format is " + strEQFormat + ". Reenter equipment.";
            }
            else
                if (sEquipFmt == "TooFew")
                {
                    returnValue = "Incorrect Equipment format. Insufficient digits entered. Correct format is " + strEQFormat + ". Reenter equipment.";
                }
                else
                {
                    returnValue = sEquipFmt;
                }
            return returnValue;
        }

        public string FormatEquipment(string sEquip)
        {
            string returnValue = null;
            string sEquipFmt;
            //If mvarEQSec1Length = 0 Or mvarEQSec2Length = 0 Then FormatEquipment = sEquip : Exit Function

            intSecLen[1] = mvarEQSec1Length;
            strSecPunct[1] = mvarEQSec1Punct;
            intSecLen[2] = mvarEQSec2Length;
            strSecPunct[2] = mvarEQSec2Punct;
            intSecLen[3] = mvarEQSec3Length;
            strSecPunct[3] = "";
            intSecLen[4] = 0;

            sEquipFmt = MasterFormat(sEquip, strEQFormat);
            if (sEquipFmt == "TooMany")
            {
                returnValue = "Incorrect Equipment format. Too many characters entered. Correct format is " + strEQFormat + ". Reenter equipment.";
            }
            else
                if (sEquipFmt == "TooFew")
                {
                    returnValue = "Incorrect Equipment format. Insufficient digits entered. Correct format is " + strEQFormat + ". Reenter equipment.";
                }
                else
                {
                    returnValue = sEquipFmt;
                }
            return returnValue;
        }

        public string FormatEQCostCode(ref string sEQCostCode)
        {
            string returnValue = null;
            string sEQCostCodeFmt;
            //If mvarEQCostCodeSec1Length = 0 Or mvarEQCostCodeSec2Length = 0 Then FormatEQCostCode = sEQCostCode : Exit Function

            intSecLen[1] = mvarEQCostCodeSec1Length;
            strSecPunct[1] = mvarEQCostCodeSec1Punct;
            intSecLen[2] = mvarEQCostCodeSec2Length;
            strSecPunct[2] = mvarEQCostCodeSec2Punct;
            intSecLen[3] = mvarEQCostCodeSec3Length;
            strSecPunct[3] = mvarEQCostCodeSec3Punct;
            intSecLen[4] = mvarEQCostCodeSec4Length;

            sEQCostCodeFmt = MasterFormat(ref sEQCostCode, ref strEQCostCodeFormat);
            if (sEQCostCodeFmt == "TooMany")
            {
                returnValue = "Incorrect equipment cost code format. Too many characters entered. Correct format is " + strEQCostCodeFormat + ". Reenter cost code.";
            }
            else
                if (sEQCostCodeFmt == "TooFew")
                {
                    returnValue = "Incorrect Equipment cost code format. Insufficient digits entered. Correct format is " + strEQCostCodeFormat + ". Reenter cost code.";
                }
                else
                {
                    returnValue = sEQCostCodeFmt;
                }
            return returnValue;
        }

        public string StripPunctuation(string sItm, bool bUncludePunctuation)
        {
            string returnValue = null;
            int iPos;
            char sChr;
            string sNewItem;

            sNewItem = "";

            for (iPos = 1; iPos <= sItm.Length; iPos++)
            {
                sChr = System.Convert.ToChar(sItm.ToUpper().Substring(iPos - 1, 1));
                if ((sChr >= 'A' && sChr <= 'Z') || (sChr >= '0' && sChr <= '9'))
                {
                    sNewItem = sNewItem + sChr;
                }
                else
                {
                    if (bUncludePunctuation == true && sChr > ' ')
                    {
                        sNewItem = sNewItem + "|";
                    }
                }
            }
            returnValue = sNewItem;
            return returnValue;
        }

        public string StripPunctuationButNotSpaces(string sItm, bool bUncludePunctuation)
        {
            string returnValue = null;
            int iPos;
            char sChr;
            string sNewItem;

            sNewItem = "";

            sItm = sItm.Trim();
            for (iPos = 1; iPos <= sItm.Length; iPos++)
            {
                sChr = System.Convert.ToChar(sItm.ToUpper().Substring(iPos - 1, 1));
                if ((sChr >= 'A' && sChr <= 'Z') || (sChr >= '0' && sChr <= '9') || (sChr == ' '))
                {
                    sNewItem = sNewItem + sChr;
                }
                else
                {
                    if (bUncludePunctuation == true && sChr > ' ')
                    {
                        sNewItem = sNewItem + "|";
                    }
                }
            }
            returnValue = sNewItem;
            return returnValue;
        }

        private string MasterFormat(ref string sItem, ref string sFormat)
        {
            string returnValue = null;
            int iMaxLen;
            int iMinLen;
            int iLoop;
            string sNewItem;
            string[] sSections;

            if (string.IsNullOrWhiteSpace(sItem)) return string.Empty;
            //If sItem = "" Then MasterFormat = "" : Exit Function

            sNewItem = StripPunctuationButNotSpaces(sItem, true);

            if (string.IsNullOrWhiteSpace(sNewItem)) return string.Empty;

            sSections = sNewItem.Split('|');

            iMinLen = 1 + intSecLen[2] + intSecLen[3] + intSecLen[4];
            iMaxLen = intSecLen[1] + intSecLen[2] + intSecLen[3] + intSecLen[4];

            if (iMaxLen == 0)               //13443
                iMaxLen = sItem.Length;


            //if an item is punctuated in Timberline, Timberline will left pad it with 0's AFTER the first section
            sNewItem = sSections[0];
            if ((sSections.Length - 1) > 0) //item is formatted
            {
                for (iLoop = 1; iLoop <= (sSections.Length - 1); iLoop++)
                {
                    //reminder sSections() is 0 based, intSecLen() is 1 based
                    sNewItem = sNewItem + Strings.Right(new string('0', intSecLen[iLoop + 1]) + sSections[iLoop], intSecLen[iLoop + 1]);
                }
            }

            if (sNewItem.Length >= iMinLen && sNewItem.Length <= iMaxLen)
            {
                if (sFormat != null)        //13443
                    returnValue = FormatItem(sNewItem, sFormat);
                else
                    returnValue = sNewItem;
            }
            else
                if (sItem.Length > iMaxLen)
                {
                    returnValue = "TooMany";
                }
                else
                {
                    returnValue = "TooFew";
                }
            return returnValue;
        }

        private string FormatItem(string sItm, string sFmt)
        {
            string returnValue = null;
            string sNewItm;
            int iPos;
            int iFmtPos;

            sNewItm = "";
            iFmtPos = sFmt.Length - 1;
            for (iPos = sItm.Length - 1; iPos >= 0; iPos--)
            {
                if (iFmtPos > 0)
                {
                    if (sFmt.Substring(iFmtPos, 1) != "x")
                    {
                        sNewItm = sFmt.Substring(iFmtPos, 1) + sNewItm;
                        iFmtPos--;
                    }
                    iFmtPos--;
                }
                sNewItm = sItm.Substring(iPos, 1) + sNewItm;
            }

            //if there are still characters left on the format string, we test to see if there is any punctuation
            //if there is we add it so MasterFormat will return an error message
            if (iFmtPos > 0)
            {
                for (iPos = iFmtPos; iPos >= 0; iPos--)
                {
                    if (sFmt.Substring(iPos, 1) != "x")
                    {
                        sNewItm = sFmt.Substring(iPos, 1) + sNewItm;
                    }
                }
            }

            returnValue = sNewItm;
            return returnValue;
        }

        private void GetCostCodeSettings(DataRow[] costCodeSettings)
        {

            //if (string.Compare(strTLVersion, "00009.00004.00000") >= 0)
            //{
            strCostCodeFormat = "";

            if (costCodeSettings != null && costCodeSettings.Length > 0)
            {
                foreach (DataRow dr in costCodeSettings)
                {
                    if (dr["Section"].ToString().Trim() == "1")
                    {

                        mvarCostCodeSec1Length = (dr["CSLEN"]) == DBNull.Value ? 0 : Convert.ToInt32(dr["CSLEN"]);
                        if (mvarCostCodeSec1Length > 0)
                        {
                            mvarCostCodeSec1Desc = dr["CNDESC"].ToString();
                            mvarCostCodeSec1Punct = dr["CNPUNCT"].ToString();
                            strCostCodeFormat = new string('x', mvarCostCodeSec1Length) + mvarCostCodeSec1Punct;
                        }
                    }
                    else if (dr["Section"].ToString().Trim() == "2")
                    {

                        mvarCostCodeSec2Length = (dr["CSLEN"]) == DBNull.Value ? 0 : Convert.ToInt32(dr["CSLEN"]);
                        if (mvarCostCodeSec2Length > 0)
                        {
                            mvarCostCodeSec2Desc = dr["CNDESC"].ToString();
                            mvarCostCodeSec2Punct = dr["CNPUNCT"].ToString();
                            strCostCodeFormat = strCostCodeFormat + new string('x', mvarCostCodeSec2Length) + mvarCostCodeSec2Punct;
                        }
                    }
                    else if (dr["Section"].ToString().Trim() == "3")
                    {

                        mvarCostCodeSec3Length = (dr["CSLEN"]) == DBNull.Value ? 0 : Convert.ToInt32(dr["CSLEN"]);
                        if (mvarCostCodeSec3Length > 0)
                        {
                            mvarCostCodeSec3Desc = dr["CNDESC"].ToString();
                            mvarCostCodeSec3Punct = dr["CNPUNCT"].ToString();
                            strCostCodeFormat = strCostCodeFormat + new string('x', mvarCostCodeSec3Length) + mvarCostCodeSec3Punct;
                        }
                    }
                    else if (dr["Section"].ToString().Trim() == "4")
                    {

                        mvarCostCodeSec4Length = (dr["CSLEN"]) == DBNull.Value ? 0 : Convert.ToInt32(dr["CSLEN"]);
                        if (mvarCostCodeSec4Length > 0)
                        {
                            mvarCostCodeSec4Desc = dr["CNDESC"].ToString();
                            strCostCodeFormat = strCostCodeFormat + new string('x', mvarCostCodeSec4Length);
                        }
                    }
                    if (dr["Section"] != DBNull.Value && Convert.ToInt32(dr["Section"]) > intCostCodeSections)
                    {
                        intCostCodeSections = Convert.ToInt32(dr["Section"]);
                    }
                }
            }
            //}

        }

        private void GetJobSettings(DataRow[] jobSettings)
        {
            //if (string.Compare(strTLVersion, "00009.00004.00000") >= 0)//(strTLVersion >= "00009.00004.00000")
            //{
            strJobFormat = "";

            if (jobSettings != null && jobSettings.Length > 0)
            {
                foreach (DataRow dr in jobSettings)
                {
                    if (dr["Section"].ToString().Trim() == "1")
                    {

                        mvarJobSec1Length = dr["CSLEN"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CSLEN"]);
                        if (mvarJobSec1Length > 0)
                        {
                            mvarJobSec1Desc = dr["CNDESC"].ToString();
                            mvarJobSec1Punct = dr["CNPUNCT"].ToString();
                            strJobFormat = new string('x', mvarJobSec1Length) + mvarJobSec1Punct;
                        }
                    }
                    else if (dr["Section"].ToString().Trim() == "2")
                    {

                        mvarJobSec2Length = dr["CSLEN"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CSLEN"]);
                        if (mvarJobSec2Length > 0)
                        {
                            mvarJobSec2Desc = dr["CNDESC"].ToString();
                            mvarJobSec2Punct = dr["CNPUNCT"].ToString();
                            strJobFormat = strJobFormat + new string('x', mvarJobSec2Length) + mvarJobSec2Punct;
                        }
                    }
                    else if (dr["Section"].ToString().Trim() == "3")
                    {

                        mvarJobSec3Length = dr["CSLEN"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CSLEN"].ToString());
                        if (mvarJobSec3Length > 0)
                        {
                            mvarJobSec3Desc = CommonFunctions.TestNullString(dr["CNDESC"]);
                            strJobFormat = strJobFormat + new string('x', mvarJobSec3Length);
                        }
                    }
                    if (dr["Section"] != DBNull.Value && Convert.ToInt32(dr["Section"]) > intJobSections)
                    {
                        intJobSections = Convert.ToInt32(dr["Section"]);
                    }
                }
            }
            //}

        }

        private void GetEquipmentSettings(DataRow[] equipmentSettings)
        {
            strEQFormat = "";

            if (equipmentSettings != null && equipmentSettings.Length > 0)
            {
                foreach (DataRow dr in equipmentSettings)
                {
                    if (dr["Section"].ToString().Trim() == "1")
                    {

                        mvarEQSec1Length = dr["CSLEN"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CSLEN"]);
                        if (mvarEQSec1Length > 0)
                        {
                            mvarEQSec1Desc = dr["CNDESC"].ToString();
                            mvarEQSec1Punct = dr["CNPUNCT"].ToString();
                            strEQFormat = new string('x', mvarEQSec1Length) + mvarEQSec1Punct;
                        }
                    }
                    else if (dr["Section"].ToString().Trim() == "2")
                    {

                        mvarEQSec2Length = dr["CSLEN"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CSLEN"]);
                        if (mvarEQSec2Length > 0)
                        {
                            mvarEQSec2Desc = dr["CNDESC"].ToString();
                            mvarEQSec2Punct = dr["CNPUNCT"].ToString();
                            strEQFormat = strEQFormat + new string('x', mvarEQSec2Length) + mvarEQSec2Punct;
                        }
                    }
                    else if (dr["Section"].ToString().Trim() == "3")
                    {

                        mvarEQSec3Length = dr["CSLEN"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CSLEN"]);
                        if (mvarEQSec3Length > 0)
                        {
                            mvarEQSec3Desc = dr["CNDESC"].ToString();
                            strEQFormat = strEQFormat + new string('x', mvarEQSec3Length);
                        }
                    }
                    if (dr["Section"] != DBNull.Value && Convert.ToInt32(dr["Section"]) > intEQSections)
                    {
                        intEQSections = Convert.ToInt32(dr["Section"]);
                    }
                }
            }

        }

        private void GetEquipmentCostCodeSettings(DataRow[] eqCostCodeSettings)
        {

            //if (string.Compare(strTLVersion, "00009.00004.00000") >= 0)
            //{
            strEQCostCodeFormat = "";

            if (eqCostCodeSettings != null && eqCostCodeSettings.Length > 0)
            {
                foreach (DataRow dr in eqCostCodeSettings)
                {
                    if (dr["Section"].ToString().Trim() == "1")
                    {
                        //UPGRADE_WARNING: Couldn't resolve default property of object TestNullNumber(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        mvarEQCostCodeSec1Length = dr["CSLEN"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CSLEN"]);
                        if (mvarEQCostCodeSec1Length > 0)
                        {
                            mvarEQCostCodeSec1Desc = dr["CNDESC"].ToString();
                            mvarEQCostCodeSec1Punct = dr["CNPUNCT"].ToString();
                            strEQCostCodeFormat = new string('x', mvarEQCostCodeSec1Length) + mvarEQCostCodeSec1Punct;
                        }
                    }
                    else if (dr["Section"].ToString().Trim() == "2")
                    {
                        //UPGRADE_WARNING: Couldn't resolve default property of object TestNullNumber(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        mvarEQCostCodeSec2Length = dr["CSLEN"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CSLEN"]);
                        if (mvarEQCostCodeSec2Length > 0)
                        {
                            mvarEQCostCodeSec2Desc = dr["CNDESC"].ToString();
                            mvarEQCostCodeSec2Punct = dr["CNPUNCT"].ToString();
                            strEQCostCodeFormat = strEQCostCodeFormat + new string('x', mvarEQCostCodeSec2Length) + mvarEQCostCodeSec2Punct;
                        }
                    }
                    else if (dr["Section"].ToString().Trim() == "3")
                    {
                        //UPGRADE_WARNING: Couldn't resolve default property of object TestNullNumber(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        mvarEQCostCodeSec3Length = dr["CSLEN"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CSLEN"]);
                        if (mvarEQCostCodeSec3Length > 0)
                        {
                            mvarEQCostCodeSec3Desc = dr["CNDESC"].ToString();
                            mvarEQCostCodeSec3Punct = dr["CNPUNCT"].ToString();
                            strEQCostCodeFormat = strEQCostCodeFormat + new string('x', mvarEQCostCodeSec3Length) + mvarEQCostCodeSec3Punct;
                        }
                    }
                    else if (dr["Section"].ToString().Trim() == "4")
                    {
                        //UPGRADE_WARNING: Couldn't resolve default property of object TestNullNumber(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        mvarEQCostCodeSec4Length = dr["CSLEN"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CSLEN"]);
                        if (mvarEQCostCodeSec4Length > 0)
                        {
                            mvarEQCostCodeSec4Desc = dr["CNDESC"].ToString();
                        }
                        strEQCostCodeFormat = strEQCostCodeFormat + new string('x', mvarEQCostCodeSec4Length);
                    }
                    if (dr["Section"] != DBNull.Value && Convert.ToInt32(dr["Section"]) > intEQCostCodeSections)
                    {
                        intEQCostCodeSections = Convert.ToInt32(dr["Section"]);
                    }
                }
            }
            //}
        }

        private void GetVendorTypes(DataRow[] vendorTypes) // done
        {

            List<DataRow> vendorTypesNotNull = new List<DataRow>();

            //Pete      Remove any Vendor types that are spaces
            foreach (DataRow row in vendorTypes)
            {
                if (!String.IsNullOrEmpty(row["CNDESC"].ToString()))
                    vendorTypesNotNull.Add(row);
            }
            Array.Resize<DataRow>(ref vendorTypes, vendorTypesNotNull.Count);
            vendorTypes = vendorTypesNotNull.ToArray();
            //Pete


            //if (string.Compare(strTLVersion, "00009.00004.00000") >= 0)
            //{

            if (vendorTypes != null)
            {
                for (int ix = 1; ix <= vendorTypes.Length; ix++)
                {

                    if (vendorTypes[ix - 1]["FLDCODE"].ToString().Trim() == "50")
                    {
                        mvarVendorType1 = vendorTypes[ix - 1]["CNDESC"].ToString();
                    }

                    if (vendorTypes[ix - 1]["FLDCODE"].ToString().Trim() == "51")
                    {
                        mvarVendorType2 = vendorTypes[ix - 1]["CNDESC"].ToString();
                    }

                    if (vendorTypes[ix - 1]["FLDCODE"].ToString().Trim() == "52")
                    {
                        mvarVendorType3 = vendorTypes[ix - 1]["CNDESC"].ToString();
                    }

                    if (vendorTypes[ix - 1]["FLDCODE"].ToString().Trim() == "500")
                    {
                        mvarVendorType5 = vendorTypes[ix - 1]["CNDESC"].ToString();
                    }

                }
            }

            //}
        }

        private bool GetGLSettings(DataRow[] glSettings)
        {
            bool returnValue = true;
            string sSepChar = "";

            //if (String.Compare(strTLVersion, "00009.00004.00000") >= 0)
            //{

            try
            {
                strGLPrefixFormat = "";

                if (glSettings != null && glSettings.Length > 0)
                {
                    foreach (DataRow dr in glSettings)
                    {
                        if (System.Convert.ToInt32(dr["Section"]) == 1)
                        {
                            mvarGLPrefixALength = System.Convert.ToInt16(dr["CSLEN"]);
                            if (mvarGLPrefixALength > 0)
                            {
                                mvarGLPrefixADesc = dr["CNDESC"].ToString();
                                mvarGLPrefixAPunct = dr["CNPUNCT"].ToString();
                                strGLPrefixFormat = string.Empty.PadLeft(mvarGLPrefixALength, 'x');// new string('x', mvarGLPrefixALength);
                                sSepChar = mvarGLPrefixAPunct;
                                intGLPrefixCnt = 1;
                            }
                        }
                        else if (System.Convert.ToInt32(dr["Section"]) == 2)
                        {
                            mvarGLPrefixABLength = System.Convert.ToInt16(dr["CSLEN"]);
                            if (mvarGLPrefixABLength > 0)
                            {
                                mvarGLPrefixABDesc = dr["CNDESC"].ToString();
                                mvarGLPrefixABPunct = dr["CNPUNCT"].ToString();
                                strGLPrefixFormat = strGLPrefixFormat + mvarGLPrefixAPunct + string.Empty.PadLeft(mvarGLPrefixABLength, 'x');//new string('x', mvarGLPrefixABLength);
                                sSepChar = mvarGLPrefixABPunct;
                                intGLPrefixCnt = 2;
                            }
                        }
                        else if (System.Convert.ToInt32(dr["Section"]) == 3)
                        {
                            mvarGLPrefixABCLength = System.Convert.ToInt16((dr["CSLEN"]));
                            if (mvarGLPrefixABCLength > 0)
                            {
                                mvarGLPrefixABCDesc = dr["CNDESC"].ToString();
                                mvarGLPrefixABCPunct = dr["CNPUNCT"].ToString();
                                strGLPrefixFormat = strGLPrefixFormat + mvarGLPrefixABPunct + string.Empty.PadLeft(mvarGLPrefixABCLength, 'x');//new string('x', mvarGLPrefixABCLength);
                                sSepChar = mvarGLPrefixABCPunct;
                                intGLPrefixCnt = 3;
                            }
                        }
                        else if (System.Convert.ToInt32(dr["Section"]) == 4)
                        {
                            mvarGLBaseLength = dr["CSLEN"] == null || dr["CSLEN"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CSLEN"]);
                            mvarGLBasePunct = dr["CNPUNCT"].ToString();
                            strGLBaseAccountFormat = string.Empty.PadLeft(mvarGLBaseLength, 'x') + mvarGLBasePunct;
                        }
                        else if (System.Convert.ToInt32(dr["Section"]) == 5)
                        {
                            mvarGLSuffixLength = System.Convert.ToInt32(dr["CSLEN"]);
                            strGLBaseAccountFormat = strGLBaseAccountFormat + string.Empty.PadLeft(mvarGLSuffixLength, 'x');//new string('x', mvarGLSuffixLength);
                        }
                    }
                }
                strGLFullAccountFormat = strGLPrefixFormat + sSepChar + strGLBaseAccountFormat;
            }
            catch (Exception e)
            {
                throw;
            }


            // }

            return returnValue;
        }

        //public string DetermineProjMgrFieldName()
        //{
        //    string name = string.Empty;
        //    DataTable dt = new DataTable();
        //    int i = -1;

        //    try
        //    {


        //        APIUtilDataAccessHelper.TimberlineDictData.FillDataTable(dt, "Select * from MASTER_JCM_RECORD_1 WHERE 1=0", null, true, false);

        //        if (dt != null)
        //        {
        //            for (i = 0; i < dt.Columns.Count; i++)
        //            {

        //                if (dt.Columns[i].ColumnName == "JPRJMGR")
        //                    break;

        //            }

        //            dt = new DataTable();
        //            APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(dt, "SELECT * FROM JCM_MASTER__JOB WHERE 1=0", null, true, false);

        //            if (dt != null && i >= 0 && i < dt.Columns.Count)
        //            {
        //                name = dt.Columns[i].ColumnName;
        //            }

        //        }


        //    }
        //    catch
        //    {
        //        name = string.Empty;
        //    }

        //    return name;
        //}

        //2.3.6=====================================================
        public bool TestForBaseAccounts(int ConnectionID)
        {
            string sQry = null;
            DataTable dt = new DataTable();
            bool exist = false;
            sQry = "SELECT * FROM GLM_MASTER__BASE_ACCOUNT";

            try
            {
                //we are checking to see if the table exists
                APIUtilDataAccessHelper.TimberSynchDataDynamic(ConnectionID).FillDataTable(dt, sQry, null, true, false);
                exist = true;
            }
            catch
            {
                //its ok to swallow the exception here, if we get here then we know the table does not exist in the timberline database    
            }

            return exist;
        }

        bool? _equipmentTablesExist = null;
        public bool TestForEquipment(int ConnectionID)
        {
            if (this._equipmentTablesExist == null)
            {
                string sQry = null;
                DataTable dt = new DataTable();
                bool exist = false;
                sQry = "SELECT * FROM EQM_MASTER__EQUIPMENT";

                try
                {
                    //we are checking to see if the table exists
                    APIUtilDataAccessHelper.TimberSynchDataDynamic(ConnectionID).FillDataTable(dt, sQry, null, true, false);
                    this._equipmentTablesExist = true;
                }
                catch
                {
                    this._equipmentTablesExist = false;
                    //its ok to swallow the exception here, if we get here then we know the table does not exist in the timberline database    
                }
            }


            return this._equipmentTablesExist.Value;
        }

        /*public bool TestForJobCostTables()
        {
            string sQry = null;
            DataTable dt = new DataTable();
            bool exist = false;

            sQry = "SELECT * FROM JCM_MASTER__JOB WHERE 1=0";

            try
            {
                //we are checking to see if the table exists
                this._tLineStdData.FillDataTable(dt , sQry , null , true , false);
                exist = true;
            }
            catch
            {
                //its ok to swallow the exception here, if we get here then we know the table does not exist in the timberline database    
            }

            return exist;
        }*/

        bool? _commitmentTablesExist = null;
        public bool TestForCommitmentTables(int ConnectionID)
        {
            if (this._commitmentTablesExist == null)
            {
                string sQry = null;
                DataTable dt = new DataTable();

                sQry = "SELECT * FROM JCM_MASTER__COMMITMENT WHERE 1=0";

                try
                {
                    //we are checking to see if the table exists
                    APIUtilDataAccessHelper.TimberSynchDataDynamic(ConnectionID).FillDataTable(dt, sQry, null, true, false);
                    this._commitmentTablesExist = true;
                }
                catch
                {
                    this._commitmentTablesExist = false;
                    //its ok to swallow the exception here, if we get here then we know the table does not exist in the timberline database    
                }
            }

            return this._commitmentTablesExist.Value;
        }

        internal object GLMinAccount(string prefix)
        {

            string acct = string.Empty, punct = string.Empty;

            try
            {



                if (this.GLPrefixABCLength > 0)
                {
                    punct = this.GLPrefixABCPunct;
                }
                else if (this.GLPrefixABLength > 0)
                {
                    punct = this.GLPrefixABPunct;
                }
                else if (this.GLPrefixALength > 0)
                {
                    punct = this.GLPrefixAPunct;
                }

                acct = string.Format("{0}{1}{2}", prefix, punct, "".PadLeft(this.GLBaseLength, '0'));

                if (this.GLSuffixLength > 0)
                {
                    acct = acct + this.GLBasePunct + "".PadLeft(this.GLSuffixLength, '0');
                }

            }
            catch
            {
                throw;
            }

            return acct;
        }

        internal object GLMaxAccount(string prefix)
        {
            string acct = string.Empty, punct = string.Empty;

            try
            {
                if (this.GLPrefixABCLength > 0)
                {
                    punct = this.GLPrefixABCPunct;
                }
                else if (this.GLPrefixABLength > 0)
                {
                    punct = this.GLPrefixABPunct;
                }
                else if (this.GLPrefixALength > 0)
                {
                    punct = this.GLPrefixAPunct;
                }

                acct = string.Format("{0}{1}{2}", prefix, punct, "".PadLeft(this.GLBaseLength, '9'));

                if (this.GLSuffixLength > 0)
                {
                    acct = acct + this.GLBasePunct + "".PadLeft(this.GLSuffixLength, '9');
                }
            }
            catch
            {
                throw;
            }

            return acct;
        }
    }
}
