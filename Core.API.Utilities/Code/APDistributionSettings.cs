using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Core.API.Utilities.Helpers;

namespace Core.API.Utilities.Code
{
    internal class APDistributionSettings : IAPDistributionSettings
    {

        #region Private

        DataTable adoContRs;

        string mvarAmount_Desc;
        string mvarRetainage_Desc;
        string mvarMisc_Ded_Desc;
        string mvarMisc_Ded2_Pct_Desc;
        string mvarAuthorization_Desc;
        string mvarDist_Code_Desc;
        string mvarExtra_Desc;
        string mvarCost_Code_Desc;
        string mvarCommitment_Desc;
        string mvarEquipment_Desc;
        string mvarEq_Cost_Code_Desc;
        string mvarMisc_Entry_1_Desc;
        string mvarMisc_Entry_2_Desc;
        string mvarEq_Cost_Code_Field_Name; // this can vary depending on the installation

        string mvarVendorTypeLevel;
        bool mvarPreTax;
        bool mvarTaxLiability;

        bool mvarJobCostTables;

        string mvarEquipCodeCodeField = string.Empty;

        #endregion 

        #region Properties

        public DataTable APM_MASTER__AP_Controls
        {
            get
            {
                return this.adoContRs;
            }
        }

        public string VendorTypeLevel
        {
            set
            {
                //pass full vendor type to vNewValue
                mvarVendorTypeLevel = string.IsNullOrEmpty(CommonFunctions.TestNullString(value).ToString()) ? string.Empty : value.Substring(value.Length - 1, 1);
            }
        }

        public string Amount_Desc
        {
            get
            {
                string returnValue = null;
                GetAmountDescription();
                returnValue = mvarAmount_Desc;
                return returnValue;
            }
        }

        public string Retainage_Desc
        {
            get
            {
                string returnValue = null;
                returnValue = mvarRetainage_Desc;
                return returnValue;
            }
        }

        public string Misc_Ded_Desc
        {
            get
            {
                string returnValue = null;
                returnValue = mvarMisc_Ded_Desc;
                return returnValue;
            }
        }

        public string Misc_Ded2_Pct_Desc
        {
            get
            {
                string returnValue = null;
                returnValue = mvarMisc_Ded2_Pct_Desc;
                return returnValue;
            }
        }

        public string Authorization_Desc
        {
            get
            {
                string returnValue = null;
                returnValue = mvarAuthorization_Desc;
                return returnValue;
            }
        }

        public string Dist_Code_Desc
        {
            get
            {
                string returnValue = null;
                returnValue = mvarDist_Code_Desc;
                return returnValue;
            }
        }

        public string Extra_Desc
        {
            get
            {
                string returnValue = null;
                returnValue = mvarExtra_Desc;
                return returnValue;
            }
        }

        public string Cost_Code_Desc
        {
            get
            {
                string returnValue = null;
                returnValue = mvarCost_Code_Desc;
                return returnValue;
            }
        }

        public string Commitment_Desc
        {
            get
            {
                string returnValue = null;
                returnValue = mvarCommitment_Desc;
                return returnValue;
            }
        }

        public string Equipment_Desc
        {
            get
            {
                string returnValue = null;
                returnValue = mvarEquipment_Desc;
                return returnValue;
            }
        }

        public string Eq_Cost_Code_Desc
        {
            get
            {
                string returnValue = null;
                returnValue = mvarEq_Cost_Code_Desc;
                return returnValue;
            }
        }

        public string Misc_Entry_1_Desc
        {
            get
            {
                string returnValue = null;
                returnValue = mvarMisc_Entry_1_Desc;
                return returnValue;
            }
        }

        public string Misc_Entry_2_Desc
        {
            get
            {
                string returnValue = null;
                returnValue = mvarMisc_Entry_2_Desc;
                return returnValue;
            }
        }

        public bool Commitment
        {
            get
            {
                if (mvarJobCostTables == false || string.IsNullOrEmpty(mvarVendorTypeLevel))
                    return false;
                return (bool)adoContRs.Rows[0]["Use_Commitment_Col_type_" + mvarVendorTypeLevel];
            }
        }

        public bool Equipment
        {
            get
            {
                if (mvarJobCostTables == false || string.IsNullOrEmpty(mvarVendorTypeLevel))
                    return false;
                return (bool)adoContRs.Rows[0]["Use_EQ_Info_Cols_type_" + mvarVendorTypeLevel];
            }
        }

        public bool Eq_Cst_Cd
        {
            get
            {
                if (mvarJobCostTables == false || string.IsNullOrEmpty(mvarVendorTypeLevel))
                    return false;
                return (bool)adoContRs.Rows[0]["Use_EQ_Info_Cols_type_" + mvarVendorTypeLevel];
            }
        }

        public bool Job
        {
            get
            {
                if (mvarJobCostTables == false || string.IsNullOrEmpty(mvarVendorTypeLevel))
                    return false;
                return System.Convert.ToBoolean(adoContRs.Rows[0]["Use_JC_Info_Cols_type_" + mvarVendorTypeLevel]) ||
                    System.Convert.ToBoolean(adoContRs.Rows[0]["Use_EQ_Info_Cols_type_" + mvarVendorTypeLevel]);
            }
        }

        public bool Extra
        {
            get
            {
                if (mvarJobCostTables == false || string.IsNullOrEmpty(mvarVendorTypeLevel))
                    return false;
                return System.Convert.ToBoolean(adoContRs.Rows[0]["Use_JC_Info_Cols_type_" + mvarVendorTypeLevel]) &&
                    modImage.ActualAPDistributionRoutines.TrackExtras;//CommonFunctions.TestNullString(adoJCContRs.Rows[0]["Track_Extras"]).ToUpper() == "TRACKING";
            }
        }

        public bool Cost_Code
        {
            get
            {
                if (mvarJobCostTables == false || string.IsNullOrEmpty(mvarVendorTypeLevel))
                    return false;
                return (bool)adoContRs.Rows[0]["Use_JC_Info_Cols_type_" + mvarVendorTypeLevel];
            }
        }

        public bool Category
        {
            get
            {
                if (mvarJobCostTables == false || string.IsNullOrEmpty(mvarVendorTypeLevel))
                    return false;
                return System.Convert.ToBoolean(adoContRs.Rows[0]["Use_JC_Info_Cols_type_" + mvarVendorTypeLevel]) &&
                    modImage.ActualAPDistributionRoutines.UseCategories;//CommonFunctions.TestNullString(adoJCContRs.Rows[0]["Category_Usage"]).ToUpper() == "ALWAYS USE";
            }
        }

        public bool Standard_Item
        {
            get
            {
                if (string.IsNullOrEmpty(mvarVendorTypeLevel))
                    return false;

                return (bool)adoContRs.Rows[0]["Use_BL_Info_Cols_type_" + mvarVendorTypeLevel];
            }
        }

        public bool PM_Chargeback
        {
            get
            {
                if (string.IsNullOrEmpty(mvarVendorTypeLevel))
                    return false;

                return (bool)adoContRs.Rows[0]["Use_PM_Chgbck_Col_type_" + mvarVendorTypeLevel];
            }
        }

        public bool GL_Prefix
        {
            get
            {
                if (string.IsNullOrEmpty(mvarVendorTypeLevel))
                    return false;

                return (bool)adoContRs.Rows[0]["Use_GL_Prefix_Col_type_" + mvarVendorTypeLevel];
            }
        }

        public bool Payable_Account
        {
            get
            {
                if (string.IsNullOrEmpty(mvarVendorTypeLevel))
                    return false;

                return (bool)adoContRs.Rows[0]["Use_Payable_Col_type_" + mvarVendorTypeLevel];
            }
        }

        public bool Units
        {
            get
            {
                if (string.IsNullOrEmpty(mvarVendorTypeLevel))
                    return false;

                return (bool)adoContRs.Rows[0]["Use_Units_Col_type_" + mvarVendorTypeLevel]; // ERF
            }
        }

        public bool Unit_Cost
        {
            get
            {
                if (string.IsNullOrEmpty(mvarVendorTypeLevel))
                    return false;

                return (bool)adoContRs.Rows[0]["Use_Units_Col_type_" + mvarVendorTypeLevel]; // ERF
            }
        }

        public bool PreTax
        {
            get
            {
                bool returnValue = true;
                GetAmountDescription();
                returnValue = mvarPreTax;
                return returnValue;
            }
        }

        public bool Tax_Group
        {
            get
            {
                bool returnValue = true;
                returnValue = (adoContRs.Rows[0]["Tax_Usage"].ToString().ToUpper() == "Not used".ToUpper()) ? false : true;
                return returnValue;
            }
        }

        public bool Tax
        {
            get
            {
                bool returnValue = true;
                returnValue = (adoContRs.Rows[0]["Tax_Usage"].ToString().ToUpper() == "Not used".ToUpper()) ? false : true;
                return returnValue;
            }
        }

        public bool Tax_Liability
        {
            get
            {
                return adoContRs.Rows[0]["Tax_Usage"].ToString().ToUpper() == "ACTUAL AND LIABILITY" ? true : false;
            }
        }

        public bool Discount
        {
            get
            {
                return (adoContRs.Rows[0]["Discount_Usage"].ToString().ToUpper() == "Distribution level".ToUpper()) ? true : false;
            }
        }

        public bool Retainage
        {
            get
            {
                if (string.IsNullOrEmpty(mvarVendorTypeLevel))
                    return false;

                return (bool)adoContRs.Rows[0]["Use_Retainage_Col_type_" + mvarVendorTypeLevel];
            }
        }

        public bool Misc_Ded_1
        {
            get
            {
                bool returnValue = false;
                string sMisc = string.Empty;

                if (adoContRs.Columns.Contains("Misc_Deduction_Level"))
                {
                    sMisc = adoContRs.Rows[0]["Misc_Deduction_Level"].ToString();

                    if (string.IsNullOrWhiteSpace(sMisc))
                    {
                        returnValue = false;
                    }
                    else if (adoContRs.Rows[0]["Misc_Deduction_Level"].ToString().ToUpper() == "Distribution level".ToUpper())
                    {
                        returnValue = true;
                    }
                    else
                    {
                        returnValue = false;
                    }
                }

                return returnValue;
            }
        }

        public bool Misc_Ded_2
        {
            get
            {
                bool use = false;

                if (this.adoContRs.Columns.Contains("Use_Misc_Deduction2_feature"))
                {
                    use = (bool)adoContRs.Rows[0]["Use_Misc_Deduction2_feature"];
                }
                return use;
            }
        }

        public bool Dist_Code
        {
            get
            {
                if (string.IsNullOrEmpty(mvarVendorTypeLevel))
                    return false;

                return (bool)adoContRs.Rows[0]["Use_Dist_Code_Col_type_" + mvarVendorTypeLevel];
            }
        }

        public bool Draw
        {
            get
            {
                if (string.IsNullOrEmpty(mvarVendorTypeLevel))
                    return false;

                return (bool)adoContRs.Rows[0]["Use_Draw_Col_type_" + mvarVendorTypeLevel];
            }
        }

        public bool Misc_Entry_1
        {
            get
            {
                if (string.IsNullOrEmpty(mvarVendorTypeLevel))
                    return false;

                return (bool)adoContRs.Rows[0]["Use_Misc_Entry_1_Col_type_" + mvarVendorTypeLevel];
            }
        }

        public bool Misc_Entry_2
        {
            get
            {
                if (string.IsNullOrEmpty(mvarVendorTypeLevel))
                    return false;

                return (bool)adoContRs.Rows[0]["Use_Misc_Entry_2_Col_type_" + mvarVendorTypeLevel];
            }
        }

        public bool Meter
        {
            get
            {
                if (string.IsNullOrEmpty(mvarVendorTypeLevel))
                    return false;

                return (bool)adoContRs.Rows[0]["Use_Meter_Col_type_" + mvarVendorTypeLevel];
            }
        }

        public bool Description
        {
            get
            {
                if (string.IsNullOrEmpty(mvarVendorTypeLevel))
                    return false;

                return (bool)adoContRs.Rows[0]["Use_Desc_Col_type_" + mvarVendorTypeLevel];
            }
        }

        public bool Authorization
        {
            get
            {
                if (string.IsNullOrEmpty(mvarVendorTypeLevel))
                    return false;

                return (bool)adoContRs.Rows[0]["Use_Authorization_Col_type_" + mvarVendorTypeLevel]; // ERF
            }
        }

        public string EquipCostCodeFieldName
        {
            get
            {
                return mvarEq_Cost_Code_Field_Name;
            }
        }

        public bool WarnLWAtInvoiceEntry
        {
            get
            {
                if (adoContRs.Columns.Contains("Warn_LW_AT_Invoice_Entry"))
                    return (bool)adoContRs.Rows[0]["Warn_LW_AT_Invoice_Entry"];
                else
                    return false;
            }
        }

        public bool WarnCRAtInvoiceEntry
        {
            get
            {
                if (adoContRs.Columns.Contains("Warn_CR_AT_Invoice_Entry"))
                    return (bool)adoContRs.Rows[0]["Warn_CR_AT_Invoice_Entry"];
                else
                    return false;
            }
        }

        public bool WarnInsAtInvoiceEntry
        {
            get
            {
                if (adoContRs.Columns.Contains("Warn_Ins_AT_Invoice_Entry"))
                    return (bool)adoContRs.Rows[0]["Warn_Ins_AT_Invoice_Entry"];
                else
                    return false;
            }
        }

        public bool WarnMiscAtInvoiceEntry
        {
            get
            {
                if (adoContRs.Columns.Contains("Warn_Misc_AT_Inv_Entry"))
                    return (bool)adoContRs.Rows[0]["Warn_Misc_AT_Inv_Entry"];
                else
                    return false;
            }
        }

        public string EquipCodeCodeField
        {
            get
            {
                return this.mvarEquipCodeCodeField;
            }
        }

       

        #endregion

        #region Constructor

        public APDistributionSettings()
        {
            Init();
        }


        #endregion


        #region Methods

        private void Init()
        {
            //mvarJobCostTables = modImage.ActualAPDistributionRoutines.JCSystemActive;
            mvarAmount_Desc = "Amount";
            mvarTaxLiability = false;
            mvarPreTax = false;

            adoContRs = new DataTable();
            APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(adoContRs, "SELECT * FROM APM_MASTER__AP_CONTROLS", null, true, false);
            GetCustomDescriptions();

        }

        private void GetAmountDescription()
        {

            if (System.Convert.ToInt32(adoContRs.Rows[0]["Pretax_at_invoice_level"]) != 0)
            {
                mvarPreTax = true;
                mvarAmount_Desc = "Pre-tax";
                return;
            }

            if (adoContRs.Rows[0]["Tax_Usage"].ToString() == "Not used")
            {
                mvarPreTax = false;
                mvarAmount_Desc = "Amount";
                return;
            }

            if (!(string.IsNullOrEmpty(mvarVendorTypeLevel)) && System.Convert.ToInt32(adoContRs.Rows[0]["Use_Pretax_Col_type_" + mvarVendorTypeLevel]) != 0)
            {
                mvarPreTax = true;
                mvarAmount_Desc = "Pre-tax";
            }
            else
            {
                mvarAmount_Desc = "Amount";
                mvarPreTax = false;
            }
        }

        private void GetCustomDescriptions()
        {
            string sql = string.Empty;
            DataTable standardDescriptions = new DataTable(), customDescriptions = new DataTable();


            mvarMisc_Entry_1_Desc = string.Empty;
            mvarMisc_Entry_2_Desc = string.Empty;

            try
            {
                mvarMisc_Entry_1_Desc = CommonFunctions.TestNullString(this.adoContRs.Rows[0]["Default_Misc_Entry_1"].ToString().Trim());

            }
            catch
            {
            }

            try
            {
                mvarMisc_Entry_2_Desc = CommonFunctions.TestNullString(this.adoContRs.Rows[0]["Default_Misc_Entry_2"].ToString().Trim());

            }
            catch
            {
            }


            APIUtilDataAccessHelper.TimberlineStandardData.FillDataTable(standardDescriptions, "SELECT * FROM APM_MASTER__DISTRIBUTION where vendor='.~' and invoice='.~'", null, true, false);

            APIUtilDataAccessHelper.TimberlineCustomData.FillDataTable(customDescriptions, "SELECT * FROM MASTER_APM_DISTRIBUTION where vendor='.~' and invoice='.~'", null, true, false);
            
            
            int colNum = 0;
            foreach (DataColumn stdCol in standardDescriptions.Columns)
            {

                if (stdCol.ColumnName == "Retainage")
                {
                    mvarRetainage_Desc = customDescriptions.Columns[colNum].ColumnName.Replace("_", " ");
                }
                else if (stdCol.ColumnName == "Misc_Deduction")
                {
                    mvarMisc_Ded_Desc = customDescriptions.Columns[colNum].ColumnName.Replace("_", " ");
                }
                else if (stdCol.ColumnName == "Misc_Deduction2_Percent")
                {
                    mvarMisc_Ded2_Pct_Desc = customDescriptions.Columns[colNum].ColumnName.Replace("_", " ");
                }
                else if (stdCol.ColumnName == "Authorization")
                {
                    mvarAuthorization_Desc = customDescriptions.Columns[colNum].ColumnName.Replace("_", " ");
                }
                else if (stdCol.ColumnName == "Dist_Code")
                {
                    mvarDist_Code_Desc = customDescriptions.Columns[colNum].ColumnName.Replace("_", " ");
                }
                else if (stdCol.ColumnName == "Extra")
                {
                    this.mvarExtra_Desc = customDescriptions.Columns[colNum].ColumnName.Replace("_", " ");
                }
                else if (stdCol.ColumnName == "Cost_Code")
                {
                    this.mvarCost_Code_Desc = customDescriptions.Columns[colNum].ColumnName.Replace("_", " ");
                }
                else if (stdCol.ColumnName == "Commitment")
                {
                    this.mvarCommitment_Desc = customDescriptions.Columns[colNum].ColumnName.Replace("_", " ");
                }
                else if (stdCol.ColumnName == "Equipment")
                {
                    this.mvarEquipment_Desc = customDescriptions.Columns[colNum].ColumnName.Replace("_", " ");
                    this.mvarEq_Cost_Code_Field_Name = standardDescriptions.Columns[colNum + 1].ColumnName;

                    if (standardDescriptions.Columns[colNum + 1].ColumnName == customDescriptions.Columns[colNum + 1].ColumnName)
                    {
                        mvarEq_Cost_Code_Desc = "EQ Cst Cd";
                    }
                    else
                    {
                        mvarEq_Cost_Code_Desc = customDescriptions.Columns[colNum + 1].ColumnName;
                    }
                    mvarEq_Cost_Code_Desc = mvarEq_Cost_Code_Desc.Replace("_", " ");
                }

                if (stdCol.ColumnName.Length > 10)
                {
                    if (stdCol.ColumnName.Substring(0, 10).ToUpper() == "COST_CODE_")
                    {

                        this.mvarEquipCodeCodeField = stdCol.ColumnName;
                    }
                }

                ++colNum;

            }

        }

        public bool TestForJobCostTables()
        {
            return this.mvarJobCostTables;
        }

        //public void SetVendorTypeLevel(string vendor)
        //{
        //    var vendorInfo = modImage.GetVendor(vendor);

        //    if (vendorInfo == null)
        //        return;

        //    this.VendorTypeLevel = vendorInfo.VendorTypeLevel;
        //}

        

        

        //public bool EquipmentMemoCostCode(string sEqCstCd)
        //{

        //    string sQry;
        //    DataTable aRs = new DataTable();
        //    TimberScanDataAccess.DataAcessHelper tLineData = modImage.TimberlineStandardData;

        //    bool bMemoCost;

        //    try
        //    {

        //        sQry = "SELECT Memo_Cost FROM EQS_STANDARD__STANDARD_COST_CODE WHERE Cost_Code=" + CommonFunctions.QNApos(sEqCstCd);

        //        if (TimberScan.Properties.Extended.Default.TSyncActive)
        //        {
        //            sQry += " AND ConnectionID=" + modSystemProcedures.clsTimberlineConnection.ConnectionID.ToString();
        //            tLineData = new TimberScanDataAccess.DataAcessHelper(TimberScan.Properties.Extended.Default.TSyncConnectionString, TimberScanDataAccess.DataAccessType.SqlServer);
        //        }
        //        tLineData.FillDataTable(aRs, sQry, null, true, false);

        //        if (aRs == null || aRs.Rows.Count <= 0)
        //        {
        //            bMemoCost = false;
        //        }
        //        else
        //        {
        //            bMemoCost = Convert.ToBoolean(aRs.Rows[0]["Memo_Cost"].ToString().Trim());
        //        }


        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    return bMemoCost;
        //}

        //public DataTable GetVendor(string vendor)
        //{

        //    DataTable vendorInfoDataTable = new DataTable();
        //    vendorInfoDataTable.Columns.Add(new DataColumn("Vendor", typeof(string)));
        //    vendorInfoDataTable.Columns.Add(new DataColumn("VndName", typeof(string)));
        //    vendorInfoDataTable.Columns.Add(new DataColumn("Type", typeof(string)));

        //    var vndInfoObj = modImage.GetVendor(vendor);

        //    if (vndInfoObj != null)
        //    {
        //        var vendorDataRow = vendorInfoDataTable.NewRow();
        //        vendorDataRow["Vendor"] = vndInfoObj.Vendor;
        //        vendorDataRow["VndName"] = vndInfoObj.Name;
        //        vendorDataRow["Type"] = vndInfoObj.VendorType;

        //        vendorInfoDataTable.Rows.Add(vendorDataRow);
        //    }

        //    return vendorInfoDataTable;
        //}


        #endregion
    }
}
