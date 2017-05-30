using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.Odbc;

namespace Core.API.Utilities.Code
{
    internal class clsTS_CTL
    {

        public string CommitmentDesc
        {
            get
            {
                return modImage.TS_CTL.CommitmentDesc;
            }
        }

        public int CommitmentLen
        {
            get
            {
                return modImage.TS_CTL.CommitmentLen;
            }
        }

        public bool CommitmentCaps
        {
            get
            {
                return modImage.TS_CTL.CommitmentCaps;
            }
        }

        public bool VendorCaps
        {
            get
            {
                return modImage.TS_CTL.VendorCaps;
            }
        }

        public bool InvoiceCaps
        {
            get
            {
                return modImage.TS_CTL.InvoiceCaps;
            }
        }

        public bool AuthorizationCaps
        {
            get
            {
                return modImage.TS_CTL.AuthorizationCaps;
            }
        }

        public string JobFormat
        {
            get { return modImage.TS_CTL.JobFormat; }
        }

        /*This field has been moved to APDistributionSettings class
         * public string EquipCodeCodeField
        {
            get
            {
                return modImage.TS_CTL.EquipCodeCodeField;
            }
        }
        */
        public string CostCodeFormat
        {
            get { return modImage.TS_CTL.CostCodeFormat; }
        }

        public string EQFormat
        {
            get
            {
                return modImage.TS_CTL.EQFormat;
            }
        }

        public string EQCostCodeFormat
        {
            get { return modImage.TS_CTL.EQCostCodeFormat; }
        }

        public string GLFullAccountFormat
        {
            get { return modImage.TS_CTL.GLFullAccountFormat; }
        }

        public string GLBaseAccountFormat
        {
            get
            {
                return modImage.TS_CTL.GLBaseAccountFormat;
            }
        }

        public string GLPrefixFormat
        {
            get { return modImage.TS_CTL.GLPrefixFormat; }
        }

        private void Class_Initialize_Renamed()
        {


        }

        public bool StandardCostCodeTable
        {
            get
            {
                return modImage.TS_CTL.StandardCostCodeTable;
            }
        }

        public bool TestForStandardCostCodeTable()
        {
            return modImage.TS_CTL.TestForStandardCostCodeTable();
        }

        public clsTS_CTL()
        {
        }

        public bool NoSuchTableError
        {
            get
            {
                return modImage.TS_CTL.NoSuchTableError;
            }
        }

        public int GLPrefixALength
        {
            get
            {
                return modImage.TS_CTL.GLPrefixALength;
            }
        }

        public string GLPrefixADesc
        {
            get
            {
                return modImage.TS_CTL.GLPrefixADesc;
            }
        }

        public string GLPrefixAPunct
        {
            get
            {
                return modImage.TS_CTL.GLPrefixAPunct;
            }
        }

        public int GLPrefixABLength
        {
            get
            {
                return modImage.TS_CTL.GLPrefixABLength;
            }
        }

        public string GLPrefixABDesc
        {
            get
            {
                return modImage.TS_CTL.GLPrefixABDesc;
            }
        }

        public string GLPrefixABPunct
        {
            get
            {
                return modImage.TS_CTL.GLPrefixABPunct;
            }
        }

        public int GLPrefixABCLength
        {
            get
            {
                return modImage.TS_CTL.GLPrefixABCLength;
            }
        }

        public string GLPrefixABCDesc
        {
            get
            {
                return modImage.TS_CTL.GLPrefixABCDesc;
            }
        }

        public string GLPrefixABCPunct
        {
            get
            {
                return modImage.TS_CTL.GLPrefixABCPunct;
            }
        }

        public int GLBaseLength
        {
            get
            {
                return modImage.TS_CTL.GLBaseLength;
            }
        }

        public string GLBasePunct
        {
            get
            {
                return modImage.TS_CTL.GLBasePunct;
            }
        }

        public int GLSuffixLength
        {
            get
            {
                return modImage.TS_CTL.GLSuffixLength;
            }
        }

        public string VendorType1
        {
            get
            {
                return modImage.TS_CTL.VendorType1;
            }
        }

        public string VendorType2
        {
            get
            {
                return modImage.TS_CTL.VendorType2;
            }
        }

        public string VendorType3
        {
            get
            {
                return modImage.TS_CTL.VendorType3;
            }
        }

        public string VendorType4
        {
            get
            {
                return modImage.TS_CTL.VendorType4;
            }
        }

        public string VendorType5
        {
            get
            {
                return modImage.TS_CTL.VendorType5;
            }
        }

        public int JobSec1Length
        {
            get
            {
                return modImage.TS_CTL.JobSec1Length;
            }
        }

        public string JobSec1Desc
        {
            get
            {
                return modImage.TS_CTL.JobSec1Desc;
            }
        }

        public string JobSec1Punct
        {
            get
            {
                return modImage.TS_CTL.JobSec1Punct;
            }
        }

        public int JobSec2Length
        {
            get
            {
                return modImage.TS_CTL.JobSec2Length;
            }
        }

        public string JobSec2Desc
        {
            get
            {
                return modImage.TS_CTL.JobSec2Desc;
            }
        }

        public string JobSec2Punct
        {
            get
            {
                return modImage.TS_CTL.JobSec2Punct;
            }
        }

        public int JobSec3Length
        {
            get
            {
                return modImage.TS_CTL.JobSec3Length;
            }
        }

        public string JobSec3Desc
        {
            get
            {
                return modImage.TS_CTL.JobSec3Desc;
            }
        }

        public int CostCodeSec1Length
        {
            get
            {
                return modImage.TS_CTL.CostCodeSec1Length;
            }
        }

        public string CostCodeSec1Desc
        {
            get
            {
                return modImage.TS_CTL.CostCodeSec1Desc;
            }
        }

        public string CostCodeSec1Punct
        {
            get
            {
                return modImage.TS_CTL.CostCodeSec1Punct;
            }
        }

        public int CostCodeSec2Length
        {
            get
            {
                return modImage.TS_CTL.CostCodeSec2Length;
            }
        }

        public string CostCodeSec2Desc
        {
            get
            {
                return modImage.TS_CTL.CostCodeSec2Desc;
            }
        }

        public string CostCodeSec2Punct
        {
            get
            {
                return modImage.TS_CTL.CostCodeSec2Punct;
            }
        }

        public int CostCodeSec3Length
        {
            get
            {
                return modImage.TS_CTL.CostCodeSec3Length;
            }
        }

        public string CostCodeSec3Desc
        {
            get
            {
                return modImage.TS_CTL.CostCodeSec3Desc;
            }
        }

        public string CostCodeSec3Punct
        {
            get
            {
                return modImage.TS_CTL.CostCodeSec3Punct;
            }
        }

        public int CostCodeSec4Length
        {
            get
            {
                return modImage.TS_CTL.CostCodeSec4Length;
            }
        }

        public string CostCodeSec4Desc
        {
            get
            {
                return modImage.TS_CTL.CostCodeSec4Desc;
            }
        }

        public int EQSec1Length
        {
            get
            {
                return modImage.TS_CTL.EQSec1Length;
            }
        }

        public string EQSec1Desc
        {
            get
            {
                return modImage.TS_CTL.EQSec1Desc;
            }
        }

        public string EQSec1Punct
        {
            get
            {
                return modImage.TS_CTL.EQSec1Punct;
            }
        }

        public int EQSec2Length
        {
            get
            {
                return modImage.TS_CTL.EQSec2Length;
            }
        }

        public string EQSec2Desc
        {
            get
            {
                return modImage.TS_CTL.EQSec2Desc;
            }
        }

        public string EQSec2Punct
        {
            get
            {
                return modImage.TS_CTL.EQSec2Punct;
            }
        }

        public int GLPrefixCnt
        {
            get
            {
                return modImage.TS_CTL.GLPrefixCnt;
            }
        }

        public string GLPrefixDescription
        {
            get
            {
                return modImage.TS_CTL.GLPrefixDescription;
            }
        }

        //public string DetermineBaseAccount(ref string sAcct, bool bIncludeSuffix)
        public string DetermineBaseAccount(string sAcct, bool bIncludeSuffix)
        {
            return modImage.TS_CTL.DetermineBaseAccount(sAcct, bIncludeSuffix);
        }

        public string DetermineBaseAccount(string sAcct, bool bIncludeSuffix, bool makeSureBaseAccountIsValid)
        {
            return modImage.TS_CTL.DetermineBaseAccount(sAcct, bIncludeSuffix, makeSureBaseAccountIsValid);
        }

        public string DetermineSuffix(ref string sAcct)
        {
            return modImage.TS_CTL.DetermineSuffix(ref sAcct);
        }

        public bool TestForValidBaseAccount(string sAccount)
        {
            return modImage.TS_CTL.TestForValidBaseAccount(sAccount);
        }

        //public string DetermineGLPrefix(ref string sAcct, string sPrfxLvl)
        public string DetermineGLPrefix(string sAcct, string sPrfxLvl)
        {
            return modImage.TS_CTL.DetermineGLPrefix(sAcct, sPrfxLvl);
        }

        public string DetermineGLPrefix(string sAcct, string sPrfxLvl, bool makeSurePrefixIsValid)
        {
            return modImage.TS_CTL.DetermineGLPrefix(sAcct, sPrfxLvl, makeSurePrefixIsValid);
        }

        public bool TestForValidGLPrefix(string sPrefix, string sPrefixLevel)
        {
            return modImage.TS_CTL.TestForValidGLPrefix(sPrefix, sPrefixLevel);
        }

        public string DetermineBalanceSheetLevel(string sPrefixA)
        {
            return modImage.TS_CTL.DetermineBalanceSheetLevel(sPrefixA);
        }

        public string DetermineAPGLPrefix(string sGLPrefix)
        {
            return modImage.TS_CTL.DetermineAPGLPrefix(sGLPrefix);
        }

        public string FormatFullGLAccount(ref string sAct, Boolean bImportedRec)
        {
            return modImage.TS_CTL.FormatFullGLAccount(ref sAct, bImportedRec);
        }

        public string FormatGLPrefix(ref string sPrfx)
        {
            return modImage.TS_CTL.FormatGLPrefix(ref sPrfx);
        }

        public string FormatGLPrefix(string sPrfx)
        {
            return modImage.TS_CTL.FormatGLPrefix(sPrfx);
        }

        public string FormatGLBaseAccount(ref string sAcct)
        {
            return modImage.TS_CTL.FormatGLBaseAccount(ref sAcct);
        }

        public string FormatJob(ref string sJob)
        {
            return modImage.TS_CTL.FormatJob(ref sJob);
        }

        public string FormatJob(string sJob)
        {
            return modImage.TS_CTL.FormatJob(sJob);
        }

        public string FormatCostCode(ref string sCostCode)
        {
            return modImage.TS_CTL.FormatCostCode(ref sCostCode);
        }

        public string FormatEquipment(ref string sEquip)
        {
            return modImage.TS_CTL.FormatEquipment(ref sEquip);
        }

        public string FormatEquipment(string sEquip)
        {
            return modImage.TS_CTL.FormatEquipment(sEquip);
        }

        public string FormatEQCostCode(ref string sEQCostCode)
        {
            return modImage.TS_CTL.FormatEQCostCode(ref sEQCostCode);
        }

        public string StripPunctuation(string sItm, bool bUncludePunctuation)
        {
            return modImage.TS_CTL.StripPunctuation(sItm, bUncludePunctuation);
        }


        public string DetermineProjMgrFieldName()
        {
            return modImage.TS_CTL.DetermineProjMgrFieldName();
        }

        //2.3.6=====================================================
        public bool TestForBaseAccounts()
        {
            return modImage.TS_CTL.TestForBaseAccounts();
        }

        public bool TestForEquipment()
        {
            return modImage.TS_CTL.TestForEquipment();
        }

        private TimberScanDataAccess.DataAcessHelper _tLineStdData = new TimberScanDataAccess.DataAcessHelper(
            modImage.SystemConnections.TimberlineStdConnectionString, TimberScanDataAccess.DataAccessType.Odbc);

        public bool TestForCommitmentTables()
        {
            return modImage.TS_CTL.TestForCommitmentTables();
        }

        internal object GLMinAccount(string prefix)
        {

            return modImage.TS_CTL.GLMinAccount(prefix);
        }

        internal object GLMaxAccount(string prefix)
        {
            return modImage.TS_CTL.GLMaxAccount(prefix);
        }

        public string CalculateFiscalEntityPrefix(string prefix, bool makeSurePrefixIsValid)
        {
            string fiscalEntityLevel = string.Empty;
            string fiscalPrefix = string.Empty;

            string prefixA = DetermineGLPrefix(prefix, "PrefixA");

            //Get EntityLevel from database
            TimberScanDataAccess.DataAcessHelper tLineData = modImage.TimberlineStandardData;//new TimberScanDataAccess.DataAcessHelper(modImage.SystemConnections.TimberlineStdConnectionString, TimberScanDataAccess.DataAccessType.Odbc);
            string sql = "SELECT Fiscal_Entity_Level FROM GLM_MASTER__ACCOUNT_PREFIX_A WHERE Account_Prefix_A=" + CommonFunctions.QNApos(prefixA);
            DataTable dataTable = new DataTable();
            tLineData.FillDataTable(dataTable, sql, null, true, false);
            fiscalEntityLevel = dataTable.Rows[0]["Fiscal_Entity_Level"].ToString();


            if (fiscalEntityLevel == GLPrefixADesc)
                return DetermineGLPrefix(prefix, "PrefixA", makeSurePrefixIsValid);

            else if (fiscalEntityLevel == GLPrefixABDesc)
                return DetermineGLPrefix(prefix, "PrefixB", makeSurePrefixIsValid);

            else
                return DetermineGLPrefix(prefix, "PrefixC", makeSurePrefixIsValid);
        }

    }
}
