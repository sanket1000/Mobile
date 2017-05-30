using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.API.Utilities.Code
{
    internal interface IAPDistributionSettings
    {
        string Amount_Desc
        {
            get;
        }
        bool Authorization
        {
            get;
        }
        string Authorization_Desc
        {
            get;
        }
        bool Category
        {
            get;
        }
        bool Commitment
        {
            get;
        }
        string Commitment_Desc
        {
            get;
        }
        bool Cost_Code
        {
            get;
        }
        string Cost_Code_Desc
        {
            get;
        }
        bool Description
        {
            get;
        }
        bool Discount
        {
            get;
        }
        bool Dist_Code
        {
            get;
        }
        string Dist_Code_Desc
        {
            get;
        }
        bool Draw
        {
            get;
        }
        string Eq_Cost_Code_Desc
        {
            get;
        }
        bool Eq_Cst_Cd
        {
            get;
        }
        string EquipCostCodeFieldName
        {
            get;
        }
        bool Equipment
        {
            get;
        }
        string Equipment_Desc
        {
            get;
        }
        bool Extra
        {
            get;
        }
        string Extra_Desc
        {
            get;
        }
        bool GL_Prefix
        {
            get;
        }
        bool Job
        {
            get;
        }
        bool Meter
        {
            get;
        }
        bool Misc_Ded_1
        {
            get;
        }
        bool Misc_Ded_2
        {
            get;
        }
        string Misc_Ded_Desc
        {
            get;
        }
        string Misc_Ded2_Pct_Desc
        {
            get;
        }
        bool Misc_Entry_1
        {
            get;
        }
        string Misc_Entry_1_Desc
        {
            get;
        }
        bool Misc_Entry_2
        {
            get;
        }
        string Misc_Entry_2_Desc
        {
            get;
        }
        bool Payable_Account
        {
            get;
        }
        bool PM_Chargeback
        {
            get;
        }
        bool PreTax
        {
            get;
        }
        bool Retainage
        {
            get;
        }
        string Retainage_Desc
        {
            get;
        }
        bool Standard_Item
        {
            get;
        }
        bool Tax
        {
            get;
        }
        bool Tax_Group
        {
            get;
        }
        bool Tax_Liability
        {
            get;
        }
        bool TestForJobCostTables();
        bool Unit_Cost
        {
            get;
        }
        bool Units
        {
            get;
        }
        string VendorTypeLevel
        {
            set;
        }

       // void SetVendorTypeLevel(string vendor);

        //bool SetApprovalFlag(string vendor, string invoice, bool posted);

       // bool UpdateFileLinks(string vendor, string invoice, string fileLinks, bool posted);

        //DataTable GetVendor(string vendor);

        //bool EquipmentMemoCostCode(string sEqCstCd);
    }
}
