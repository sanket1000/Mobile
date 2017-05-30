using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.VisualBasic;
using Core.API.Utilities.Helpers;


namespace Core.API.Utilities.Code
{
    public class actualTS_CTL 
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
        string strTLVersion;

        private void Class_Initialize_Renamed()
        {
            //TLSettings.WriteInitStaticClassMessage(this.ToString(), "Initializing actualTS_CTL");
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

            sTlVer = TLSettings.strTimberlineVersion.Split('.');
            strTLVersion = Strings.Format(Conversion.Val(sTlVer[0]), "00000") + "." + Strings.Format(Conversion.Val(sTlVer[1]), "00000") + "." + Strings.Format(Conversion.Val(sTlVer[2]), "00000");

            DataTable dtAllSettings = new DataTable();
            DataRow[] specficSettingsRows = null;


            //TLSettings.WriteInitStaticClassMessage(this.ToString(), "querying TS_CTL_RECORD_3. SQL = SELECT Section,CSLEN,CNDESC,CNPUNCT,FLDCODE,CNTCAP FROM TS_CTL_RECORD_3 WHERE FLDCODE in (9,16,18,69,187,20,50,51,52,500) ORDER BY FLDCODE");
            TLSettings.TimberlineDictionaryData.FillDataTable(dtAllSettings, "SELECT Section,CSLEN,CNDESC,CNPUNCT,FLDCODE,CNTCAP FROM TS_CTL_RECORD_3 WHERE FLDCODE in (9,16,18,69,187,20,50,51,52,500) ORDER BY FLDCODE", null, true, false);
            //TLSettings.WriteInitStaticClassMessage(this.ToString(), "querying TS_CTL_RECORD_3 done");

            //TLSettings.WriteInitStaticClassMessage(this.ToString(), "Getting GL Settings");
            specficSettingsRows = dtAllSettings.Select("FLDCODE = 9", "Section");
            mvarNoSuchTableError = !GetGLSettings(specficSettingsRows);
            //TLSettings.WriteInitStaticClassMessage(this.ToString(), "Getting GL Settings done");

            if (mvarNoSuchTableError == false)
            {

                //These are the rows for the vendor types
                specficSettingsRows = dtAllSettings.Select("FLDCODE In (50,51,52,500)");

                if (specficSettingsRows != null && specficSettingsRows.Length > 0)
                {
                    //TLSettings.WriteInitStaticClassMessage(this.ToString(), "Getting vendor types");
                    GetVendorTypes(specficSettingsRows);
                    //TLSettings.WriteInitStaticClassMessage(this.ToString(), "Getting vendor types done");
                }

                //These are the rows for the job settings
                specficSettingsRows = dtAllSettings.Select("FLDCODE = 16", "Section");

                if (specficSettingsRows != null && specficSettingsRows.Length > 0)
                {
                    //TLSettings.WriteInitStaticClassMessage(this.ToString(), "Getting job settings");
                    GetJobSettings(specficSettingsRows);
                    //TLSettings.WriteInitStaticClassMessage(this.ToString(), "Getting job settings done");
                }

                //These are the rows for the Cost Code Settings
                specficSettingsRows = dtAllSettings.Select("FLDCODE = 18", "Section");

                if (specficSettingsRows != null && specficSettingsRows.Length > 0)
                {
                    //TLSettings.WriteInitStaticClassMessage(this.ToString(), "Getting cost code settings");
                    GetCostCodeSettings(specficSettingsRows);
                    //TLSettings.WriteInitStaticClassMessage(this.ToString(), "Getting cost code settings done");
                }

                //These are the rows for the Equipment Settings
                specficSettingsRows = dtAllSettings.Select("FLDCODE = 69", "Section");

                if (specficSettingsRows != null && specficSettingsRows.Length > 0)
                {
                    //TLSettings.WriteInitStaticClassMessage(this.ToString(), "Getting equipment settings");
                    GetEquipmentSettings(specficSettingsRows);
                    //TLSettings.WriteInitStaticClassMessage(this.ToString(), "Getting equipment settings done");
                }

                //These are the rows for the Equipment Cost Code Settings
                specficSettingsRows = dtAllSettings.Select("FLDCODE = 187", "Section");

                if (specficSettingsRows != null && specficSettingsRows.Length > 0)
                {
                    //TLSettings.WriteInitStaticClassMessage(this.ToString(), "Getting equipment cost code settings");
                    GetEquipmentCostCodeSettings(specficSettingsRows);
                    //TLSettings.WriteInitStaticClassMessage(this.ToString(), "Getting equipment cost code settings done");
                }

                //TLSettings.WriteInitStaticClassMessage(this.ToString(), "testing for statndard cost code table");
                this.mvarStandardCostCodeTable = TestForStandardCostCodeTable();
                //TLSettings.WriteInitStaticClassMessage(this.ToString(), "testing for statndard cost code table done");

                //These are the rows for the Commitment Settings
                specficSettingsRows = dtAllSettings.Select("FLDCODE = 20", "Section");

                if (specficSettingsRows != null && specficSettingsRows.Length > 0)
                {
                    //TLSettings.WriteInitStaticClassMessage(this.ToString(), "getting commitment settings");
                    GetCommitmentSettings(specficSettingsRows);
                    //TLSettings.WriteInitStaticClassMessage(this.ToString(), "getting commitment settings done");
                }

                GetCapsSettings();
            }

        }

        private void GetCommitmentSettings(DataRow[] commitmentSettings)
        {
            bool gotit = true;

            if (string.Compare(strTLVersion, "00009.00004.00000") >= 0)
            {

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
            }

        }

        private void GetCapsSettings()
        {

            DataTable dtAllCapsSettings = new DataTable();
            APIUtilDataAccessHelper.TimberlineDictData.FillDataTable(dtAllCapsSettings, "SELECT Section,FLDCODE,CNTCAP FROM TS_CTL_RECORD_3 WHERE FLDCODE in (6,3,38) ORDER BY FLDCODE", null, true, false);

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

            if (string.Compare(strTLVersion, "00009.00004.00000") >= 0)
            {

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
            }

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

        public bool TestForStandardCostCodeTable()
        {
            bool returnValue = false;
            string sQry = "";

            try
            {
                int costCodeCnt = 0;
                object objectReturnedFromQuery = null;
                sQry = "SELECT count(*) FROM JCM_MASTER__STANDARD_COST_CODE";

                objectReturnedFromQuery = APIUtilDataAccessHelper.TimberlineStandardData.GetCell(sQry);

                returnValue = objectReturnedFromQuery != null && int.TryParse(objectReturnedFromQuery.ToString(), out costCodeCnt) && costCodeCnt > 0;
            }
            catch
            {

            }
            return returnValue;
        }

        public actualTS_CTL()
        {
            Class_Initialize_Renamed();
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

        public string DetermineBaseAccount(string sAcct, bool bIncludeSuffix)
        {


            return this.DetermineBaseAccount(sAcct, bIncludeSuffix, true);


        }



        //public string DetermineBaseAccount(ref string sAcct, bool bIncludeSuffix)
        public string DetermineBaseAccount(string sAcct, bool bIncludeSuffix, bool makeSureBaseAccountIsValid)
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
                if (this.TestForValidBaseAccount(sReturnAcct) == false)
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

        public bool TestForValidBaseAccount(string sAccount)
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
            TLSettings.TimberlineStandardData.FillDataTable(aRs, sQry, null, true, false);
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
        public string DetermineGLPrefix(string sAcct, string sPrfxLvl)
        {
            return DetermineGLPrefix(sAcct, sPrfxLvl, true);
        }

        public string DetermineGLPrefix(string sAcct, string sPrfxLvl, bool makeSurePrefixIsValid)
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
                    if (this.TestForValidGLPrefix(sNewAcct, "PrefixA") == false)
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
                        if (this.TestForValidGLPrefix(sNewAcct, "PrefixB") == false)
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
                        if (this.TestForValidGLPrefix(sNewAcct, "PrefixC") == false)
                        {
                            sNewAcct = "";
                        }
                    }
                }


            returnValue = sNewAcct;

            return returnValue;
        }

        public bool TestForValidGLPrefix(string sPrefix, string sPrefixLevel)
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

                APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(aRs, sQry, null, true, false);
            }
            catch
            {
                //frank swallows these exceptions, i didnt want to do that but we have not choice
            }

            return aRs != null && aRs.Rows.Count > 0;

        }

        //Signature chaged to use optional parameters - VishA, 12/15/2014
        public string DetermineBalanceSheetLevel(string sPrefixA, string sPrefixB = "", string sPrefixC = "")
        {
            string returnValue = null;
            string sBalLevel;
            string sFiscEntLvl;
            string sQry = "";
            DataTable dt = null;
            //TimberScanDataAccess.DataAcessHelper tLineStdData = TLSettings.TimberlineStandardData;

            if (this.GLPrefixCnt == 0)
            {
                return string.Empty;

            }

            sQry = "SELECT Balance_Sheet_Level, Period_End_1__Current FROM GLM_MASTER__FISCAL_CONTROLS";
            dt = new DataTable();
            APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(dt, sQry, null, true, false);

            if (Microsoft.VisualBasic.Information.IsDate(dt.Rows[0]["Period_End_1__Current"]) == true)
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
            APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(dt, sQry, null, true, false);

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
            APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(dt, sQry, null, true, false);

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

        public string DetermineAPGLPrefix(string sGLPrefix)
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

            if (this.GLPrefixCnt == 1)
            {
                sPrfxA = sGLPrefix;
            }
            else
            {
                sPrfxA = Strings.Left(sGLPrefix, Strings.InStr(sGLPrefix, this.GLPrefixAPunct, CompareMethod.Binary) - 1);
            }

            if (this.GLPrefixCnt == 2)
            {
                sPrfxB = Strings.Mid(sGLPrefix, Strings.InStrRev(sGLPrefix, this.GLPrefixABPunct, sGLPrefix.Length, CompareMethod.Binary) + 1);
            }
            else if (this.GLPrefixCnt == 3)
            {
                sPrfxB = Strings.Left(sGLPrefix, Strings.InStrRev(sGLPrefix, this.GLPrefixABPunct, sGLPrefix.Length, CompareMethod.Binary) - 1);
                sPrfxB = Strings.Mid(sPrfxB, Strings.InStr(sPrfxB, this.GLPrefixAPunct, CompareMethod.Binary) + 1);
                if (sPrfxB.Contains(this.GLPrefixABCPunct) || sPrfxB.Contains(this.GLPrefixABPunct) || sPrfxB.Contains(this.GLPrefixAPunct))
                    sPrfxB = Strings.Left(sPrfxB, Strings.InStrRev(sPrfxB, this.GLPrefixAPunct, sPrfxB.Length, CompareMethod.Binary) - 1);
                sPrfxC = Strings.Mid(sGLPrefix, Strings.InStrRev(sGLPrefix, this.GLPrefixABCPunct, sGLPrefix.Length, CompareMethod.Binary) + 1);
            }

            sBalLvl = this.DetermineBalanceSheetLevel(sPrfxA, sPrfxB, sPrfxC);

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
                        sAPPrefix = sPrfxA + this.GLPrefixAPunct + sPrfxB + this.GLPrefixABPunct + sPrfxC;
                        return sAPPrefix;
                }

            }

            if (sBalLvl == this.GLPrefixADesc)
            {
                if (this.GLPrefixCnt == 1)
                {
                    sAPPrefix = sGLPrefix;
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
                    returnValue = "Incorrect account format format.|Only one decimal point is allowed.|Correct format is " + strGLFullAccountFormat + ".|Reenter account.";
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
                            returnValue = "Incorrect account format.|Invalid character.|Correct format is " + strGLFullAccountFormat + ".|Renter account.";
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
                            returnValue = "Incorrect account format format.|Insufficient digits entered.|Correct format is " + strGLFullAccountFormat + ".|Reenter account.";
                            return returnValue;
                        }

                        if (sNewItem.Length > iMaxLenSfx)
                        {
                            returnValue = "Incorrect account format.|Too many digits before decimal point.|Correct format is " + strGLFullAccountFormat + ".|Renter account.";
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
                    returnValue = "Incorrect account format.|Invalid character.|Correct format is " + strGLFullAccountFormat + ".|Renter account.";
                    return returnValue;
                }
            }

            if (sNewItem == "Invalid character")
            {
                returnValue = "Incorrect account format.|Invalid character.|Correct format is " + strGLFullAccountFormat + ".|Renter account.";
                return returnValue;
            }

            if (!string.IsNullOrEmpty(sSuffix))
            {
                if (!TestValidCharacters(sSuffix, true))
                {
                    returnValue = "Incorrect account format.|Invalid character.|Correct format is " + strGLFullAccountFormat + ".|Renter account.";
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
                        sNewItem = "Incorrect account format format.|Insufficient digits entered.|Correct format is " + strGLFullAccountFormat + ".|Reenter account.";
                    }
                    else
                        if ((sSections.Length - 1) > (intGLPrefixCnt + 1))
                        {
                            sNewItem = "Incorrect account format.|Invalid character.|Correct format is " + strGLFullAccountFormat + ".|Renter account.";
                        }
                }
                else
                {
                    if ((sSections.Length - 1) < (intGLPrefixCnt))
                    {
                        sNewItem = "Incorrect account format format.|Insufficient digits entered.|Correct format is " + strGLFullAccountFormat + ".|Reenter account.";
                    }
                    else
                        if ((sSections.Length - 1) > (intGLPrefixCnt))
                        {
                            sNewItem = "Incorrect account format.|Invalid character.|Correct format is " + strGLFullAccountFormat + ".|Renter account.";
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
                returnValue = "Incorrect account format format.|Insufficient digits entered.|Correct format is " + strGLFullAccountFormat + ".|Reenter account.";
                return returnValue;
            }
            else if (sNewAcct.Length > iMaxLen)
            {
                returnValue = "Incorrect account format.|Too many digits before decimal point.|Correct format is " + strGLFullAccountFormat + ".|Renter account.";
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
                returnValue = "Incorrect job format.|Too many characters entered.|Correct format is " + strJobFormat + ".";
            }
            else
                if (sJobFmt == "TooFew")
                {
                    returnValue = "Incorrect job format.|Insufficient digits entered.|Correct format is " + strJobFormat + ".";
                }
                else
                {
                    returnValue = sJobFmt;
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
                returnValue = "Incorrect job format.|Too many characters entered.|Correct format is " + strJobFormat + ".";
            }
            else
                if (sJobFmt == "TooFew")
                {
                    returnValue = "Incorrect job format.|Insufficient digits entered.|Correct format is " + strJobFormat + ".";
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
                returnValue = "Incorrect cost code format.|Too many characters entered.|Correct format is " + strCostCodeFormat + ".|Reenter cost code.";
            }
            else
                if (sCostCodeFmt == "TooFew")
                {
                    returnValue = "Incorrect cost code format.|Insufficient digits entered.|Correct format is " + strCostCodeFormat + ".|Reenter cost code.";
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
                returnValue = "Incorrect Equipment format.|Too many characters entered.|Correct format is " + strEQFormat + ".|Reenter equipment.";
            }
            else
                if (sEquipFmt == "TooFew")
                {
                    returnValue = "Incorrect Equipment format.|Insufficient digits entered.|Correct format is " + strEQFormat + ".|Reenter equipment.";
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
                returnValue = "Incorrect Equipment format.|Too many characters entered.|Correct format is " + strEQFormat + ".|Reenter equipment.";
            }
            else
                if (sEquipFmt == "TooFew")
                {
                    returnValue = "Incorrect Equipment format.|Insufficient digits entered.|Correct format is " + strEQFormat + ".|Reenter equipment.";
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
                returnValue = "Incorrect equipment cost code format.|Too many characters entered.|Correct format is " + strEQCostCodeFormat + ".|Reenter cost code.";
            }
            else
                if (sEQCostCodeFmt == "TooFew")
                {
                    returnValue = "Incorrect Equipment cost code format.|Insufficient digits entered.|Correct format is " + strEQCostCodeFormat + ".|Reenter cost code.";
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

            if (string.Compare(strTLVersion, "00009.00004.00000") >= 0)
            {
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
            }
            else
            {
                clsConnection with_1 = modSystemProcedures.clsTimberlineConnection;
                mvarCostCodeSec1Length = modSystemProcedures.clsTimberlineConnection.CostCodeSec1Size;
                mvarCostCodeSec1Desc = "";
                mvarCostCodeSec1Punct = modSystemProcedures.clsTimberlineConnection.CostCodePunct1;
                mvarCostCodeSec2Length = modSystemProcedures.clsTimberlineConnection.CostCodeSec2Size;
                mvarCostCodeSec2Desc = "";
                mvarCostCodeSec2Punct = modSystemProcedures.clsTimberlineConnection.CostCodePunct2;
                mvarCostCodeSec3Length = modSystemProcedures.clsTimberlineConnection.CostCodeSec3Size;
                mvarCostCodeSec3Punct = modSystemProcedures.clsTimberlineConnection.CostCodePunct3;
                mvarCostCodeSec4Length = modSystemProcedures.clsTimberlineConnection.CostCodeSec4Size;
                mvarCostCodeSec4Desc = "";
                if (mvarCostCodeSec1Length > 0)
                {
                    strCostCodeFormat = new string('x', mvarCostCodeSec1Length) + mvarCostCodeSec1Punct;
                }
                if (mvarCostCodeSec2Length > 0)
                {
                    strCostCodeFormat = strCostCodeFormat + new string('x', mvarCostCodeSec2Length) + mvarCostCodeSec2Punct;
                }
                if (mvarCostCodeSec3Length > 0)
                {
                    strCostCodeFormat = strCostCodeFormat + new string('x', mvarCostCodeSec3Length) + mvarCostCodeSec3Punct;
                }
                if (mvarCostCodeSec4Length > 0)
                {
                    strCostCodeFormat = strCostCodeFormat + new string('x', mvarCostCodeSec4Length);
                }
            }
        }

        private void GetJobSettings(DataRow[] jobSettings)
        {
            if (string.Compare(strTLVersion, "00009.00004.00000") >= 0)//(strTLVersion >= "00009.00004.00000")
            {
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
            }
            else
            {
                clsConnection with_1 = modSystemProcedures.clsTimberlineConnection;
                mvarJobSec1Length = modSystemProcedures.clsTimberlineConnection.JobSec1Size;//modSystemProcedures.with_1.JobSec1Size;
                mvarJobSec1Desc = "";
                mvarJobSec1Punct = modSystemProcedures.clsTimberlineConnection.JobPunct1;
                mvarJobSec2Length = modSystemProcedures.clsTimberlineConnection.JobSec2Size;
                mvarJobSec2Desc = "";
                mvarJobSec2Punct = modSystemProcedures.clsTimberlineConnection.JobPunct2;
                mvarJobSec3Length = modSystemProcedures.clsTimberlineConnection.JobSec3Size;
                mvarJobSec3Desc = "";
                if (mvarJobSec1Length > 0)
                {
                    strJobFormat = new string('x', mvarJobSec1Length) + mvarJobSec1Punct;
                }
                if (mvarJobSec2Length > 0)
                {
                    strJobFormat = strJobFormat + new string('x', mvarJobSec2Length) + mvarJobSec2Punct;
                }
                if (mvarJobSec3Length > 0)
                {
                    strJobFormat = strJobFormat + new string('x', mvarJobSec3Length);
                }
            }
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

            if (string.Compare(strTLVersion, "00009.00004.00000") >= 0)
            {
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
            }
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


            if (string.Compare(strTLVersion, "00009.00004.00000") >= 0)
            {
                //if (vendorTypes != null && vendorTypes.Length > 0)
                //{
                //    mvarVendorType1 = vendorTypes[0]["CNDESC"].ToString();
                //}

                //if (vendorTypes != null && vendorTypes.Length > 1)
                //{
                //    mvarVendorType2 = vendorTypes[1]["CNDESC"].ToString();
                //}

                //if (vendorTypes != null && vendorTypes.Length > 2)
                //{
                //    mvarVendorType3 = vendorTypes[2]["CNDESC"].ToString();
                //}

                //if (vendorTypes != null && vendorTypes.Length > 3)
                //{
                //    mvarVendorType5 = vendorTypes[3]["CNDESC"].ToString();
                //}

                //irene 6/2013
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

            }
            else
            {
                DataTable dt = new DataTable();

                //TimberScanDataAccess.DataAcessHelper timberScanDataAccess = new TimberScanDataAccess.DataAcessHelper(
                //    TimberScan.Properties.Extended.Default.TimberScanConnectionString, TimberScanDataAccess.DataAccessType.SqlServer);

                string sQry = "SELECT Vendor_Type_1, Vendor_Type_2, Vendor_Type_3, Vendor_Type_4, Vendor_Type_5 FROM tblAP_VendorTypes WHERE ConnectionID=" + modSystemProcedures.clsTimberlineConnection.ConnectionID;

                APIUtilDataAccessHelper.TimberScanData.FillDataTable(dt, sQry, null, true, false);//aRs = TLSettings.SystemConnections.AP_DatabaseConnection.Execute(sQry, null, -1);

                if (dt != null && dt.Rows.Count > 0)
                {
                    mvarVendorType1 = dt.Rows[0]["Vendor_Type_1"].ToString();
                    mvarVendorType2 = dt.Rows[0]["Vendor_Type_2"].ToString();
                    mvarVendorType3 = dt.Rows[0]["Vendor_Type_3"].ToString();
                    mvarVendorType5 = dt.Rows[0]["Vendor_Type_4"].ToString();
                }
            }
        }

        private bool GetGLSettings(DataRow[] glSettings)
        {
            bool returnValue = true;
            string sSepChar = "";

            if (String.Compare(strTLVersion, "00009.00004.00000") >= 0)
            {

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


            }
            else
            {
                mvarGLPrefixALength = TLSettings.GLMasterDescriptions.PrefixALength;
                mvarGLPrefixADesc = TLSettings.GLMasterDescriptions.PrefixADesc;
                mvarGLPrefixAPunct = "-"; //we hope
                mvarGLPrefixABLength = System.Convert.ToInt16(TLSettings.GLMasterDescriptions.PrefixABLength > 0 ? TLSettings.GLMasterDescriptions.PrefixABLength - TLSettings.GLMasterDescriptions.PrefixALength - 1 : 0);
                mvarGLPrefixABDesc = TLSettings.GLMasterDescriptions.PrefixABDesc;
                mvarGLPrefixABPunct = TLSettings.GLMasterDescriptions.PrefixABLength > 0 ? "-" : "";
                mvarGLPrefixABCDesc = TLSettings.GLMasterDescriptions.PrefixABCDesc;
                mvarGLPrefixABCLength = System.Convert.ToInt16(TLSettings.GLMasterDescriptions.PrefixABCLength > 0 ? TLSettings.GLMasterDescriptions.PrefixABCLength - TLSettings.GLMasterDescriptions.PrefixABLength - 1 : 0);
                mvarGLBaseLength = TLSettings.GLMasterDescriptions.BaseAcctLength;
                mvarGLBasePunct = TLSettings.GLMasterDescriptions.SuffixLength > 0 ? "." : "";
                mvarGLSuffixLength = TLSettings.GLMasterDescriptions.SuffixLength;
                if (mvarGLPrefixABCLength > 0)
                {
                    intGLPrefixCnt = 3;
                }
                else
                    if (mvarGLPrefixABLength > 0)
                    {
                        intGLPrefixCnt = 2;
                    }
                    else
                    {
                        intGLPrefixCnt = 1;
                    }
                strGLPrefixFormat = new string('x', mvarGLPrefixALength);
                if (mvarGLPrefixABLength > 0)
                {
                    strGLPrefixFormat = strGLPrefixFormat + "-" + new string('x', mvarGLPrefixABLength);
                }
                if (mvarGLPrefixABCLength > 0)
                {
                    strGLPrefixFormat = strGLPrefixFormat + "-" + new string('x', mvarGLPrefixABCLength);
                }
                strGLBaseAccountFormat = new string('x', mvarGLBaseLength) + "." + new string('x', mvarGLSuffixLength);
                strGLFullAccountFormat = strGLPrefixFormat + "-" + strGLBaseAccountFormat;
            }
            return returnValue;
        }

        public string DetermineProjMgrFieldName()
        {
            string name = string.Empty;

          
            DataTable dt = new DataTable();
            int i = -1;

            try
            {


                APIUtilDataAccessHelper.TimberlineDictData.FillDataTable(dt, "Select * from MASTER_JCM_RECORD_1 WHERE 1=0", null, true, false);

                if (dt != null)
                {
                    for (i = 0; i < dt.Columns.Count; i++)
                    {

                        if (dt.Columns[i].ColumnName == "JPRJMGR")
                            break;

                    }

                    dt = new DataTable();
                    APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(dt, "SELECT * FROM JCM_MASTER__JOB WHERE 1=0", null, true, false);

                    if (dt != null && i >= 0 && i < dt.Columns.Count)
                    {
                        name = dt.Columns[i].ColumnName;
                    }

                }


            }
            catch
            {
                name = string.Empty;
            }

            return name;
        }

        //2.3.6=====================================================
        public bool TestForBaseAccounts()
        {
            string sQry = null;
            DataTable dt = new DataTable();
            bool exist = false;
            //TimberScanDataAccess.DataAcessHelper _tLineStdData = TLSettings.TimberlineStandardData;
            sQry = "SELECT * FROM GLM_MASTER__BASE_ACCOUNT";

            try
            {
                //we are checking to see if the table exists
                APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(dt, sQry, null, true, false);
                exist = true;
            }
            catch
            {
                //its ok to swallow the exception here, if we get here then we know the table does not exist in the timberline database    
            }

            return exist;
        }

        bool? _equipmentTablesExist = null;
        public bool TestForEquipment()
        {
            if (this._equipmentTablesExist == null)
            {
                string sQry = null;
                DataTable dt = new DataTable();
                bool exist = false;
               // TimberScanDataAccess.DataAcessHelper _tLineStdData = TLSettings.TimberlineStandardData;
                sQry = "SELECT * FROM EQM_MASTER__EQUIPMENT";

                try
                {
                    //we are checking to see if the table exists
                    APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(dt, sQry, null, true, false);
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
        public bool TestForCommitmentTables()
        {
            if (this._commitmentTablesExist == null)
            {
                string sQry = null;
                DataTable dt = new DataTable();
                //TimberScanDataAccess.DataAcessHelper _tLineStdData = TLSettings.TimberlineStandardData;

                sQry = "SELECT * FROM JCM_MASTER__COMMITMENT WHERE 1=0";

                try
                {
                    //we are checking to see if the table exists
                    APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(dt, sQry, null, true, false);
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
