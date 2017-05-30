using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Core.API.TLSettings.Models
{
    public class APDistributionSettings
    {
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

        ActualAPDistributionSettings APDistributionSettingsStatic = null;
        public APDistributionSettings(int ConnectionID)
        {
            APDistributionSettingsStatic = new ActualAPDistributionSettings(ConnectionID);
        }

        public string VendorTypeLevel
        {
            set
            {
                //pass full vendor type to vNewValue
                APDistributionSettingsStatic.VendorTypeLevel = value;
            }
        }

        public string Amount_Desc
        {
            get
            {
                return APDistributionSettingsStatic.Amount_Desc;
            }
        }

        public string Retainage_Desc
        {
            get
            {
                return APDistributionSettingsStatic.Retainage_Desc;
            }
        }

        public string Misc_Ded_Desc
        {
            get
            {
                return APDistributionSettingsStatic.Misc_Ded_Desc;
            }
        }

        public string Misc_Ded2_Pct_Desc
        {
            get
            {
                return APDistributionSettingsStatic.Misc_Ded2_Pct_Desc;
            }
        }

        public string Authorization_Desc
        {
            get
            {
                return APDistributionSettingsStatic.Authorization_Desc;
            }
        }

        public string Dist_Code_Desc
        {
            get
            {
                return APDistributionSettingsStatic.Dist_Code_Desc;
            }
        }

        public string Extra_Desc
        {
            get
            {
                return APDistributionSettingsStatic.Extra_Desc;
            }
        }

        public string Cost_Code_Desc
        {
            get
            {
                return APDistributionSettingsStatic.Cost_Code_Desc;
            }
        }

        public string Commitment_Desc
        {
            get
            {
                return APDistributionSettingsStatic.Commitment_Desc;
            }
        }

        public string Equipment_Desc
        {
            get
            {
                return APDistributionSettingsStatic.Equipment_Desc;
            }
        }

        public string Eq_Cost_Code_Desc
        {
            get
            {
                return APDistributionSettingsStatic.Eq_Cost_Code_Desc;
            }
        }

        public string Misc_Entry_1_Desc
        {
            get
            {
                return APDistributionSettingsStatic.Misc_Entry_1_Desc;
            }
        }

        public string Misc_Entry_2_Desc
        {
            get
            {
                return APDistributionSettingsStatic.Misc_Entry_2_Desc;
            }
        }

        public bool Commitment
        {
            get
            {
                return APDistributionSettingsStatic.Commitment;
            }
        }

        public bool Equipment
        {
            get
            {
                return APDistributionSettingsStatic.Equipment;
            }
        }

        public bool Eq_Cst_Cd
        {
            get
            {
                return APDistributionSettingsStatic.Eq_Cst_Cd;
            }
        }

        public bool Job
        {
            get
            {
                return APDistributionSettingsStatic.Job;
            }
        }

        public bool Extra
        {
            get
            {
                return APDistributionSettingsStatic.Extra;
            }
        }

        public bool Cost_Code
        {
            get
            {
                return APDistributionSettingsStatic.Cost_Code;
            }
        }

        public bool Category
        {
            get
            {
                return APDistributionSettingsStatic.Category;
            }
        }

        public bool Standard_Item
        {
            get
            {
                return APDistributionSettingsStatic.Standard_Item;
            }
        }

        public bool PM_Chargeback
        {
            get
            {
                return APDistributionSettingsStatic.PM_Chargeback;
            }
        }

        public bool GL_Prefix
        {
            get
            {
                return APDistributionSettingsStatic.GL_Prefix;
            }
        }

        public bool Payable_Account
        {
            get
            {
                return APDistributionSettingsStatic.Payable_Account;
            }
        }

        public bool Units
        {
            get
            {
                return APDistributionSettingsStatic.Units;
            }
        }

        public bool Unit_Cost
        {
            get
            {
                return APDistributionSettingsStatic.Unit_Cost;
            }
        }

        public bool PreTax
        {
            get
            {
                return APDistributionSettingsStatic.PreTax;
            }
        }

        public bool Tax_Group
        {
            get
            {
                return APDistributionSettingsStatic.Tax_Group;
            }
        }

        public bool Tax
        {
            get
            {
                return APDistributionSettingsStatic.Tax;
            }
        }

        public bool Tax_Liability
        {
            get
            {
                return APDistributionSettingsStatic.Tax_Liability;
            }
        }

        public bool Discount
        {
            get
            {
                return APDistributionSettingsStatic.Discount;
            }
        }

        public bool Retainage
        {
            get
            {
                return APDistributionSettingsStatic.Retainage;
            }
        }

        public bool Misc_Ded_1
        {
            get
            {
                return APDistributionSettingsStatic.Misc_Ded_1;
            }
        }

        public bool Misc_Ded_2
        {
            get
            {
                return APDistributionSettingsStatic.Misc_Ded_2;
            }
        }

        public bool Dist_Code
        {
            get
            {
                return APDistributionSettingsStatic.Dist_Code;
            }
        }

        public bool Draw
        {
            get
            {
                return APDistributionSettingsStatic.Draw;
            }
        }

        public bool Misc_Entry_1
        {
            get
            {
                return APDistributionSettingsStatic.Misc_Entry_1;
            }
        }

        public bool Misc_Entry_2
        {
            get
            {
                return APDistributionSettingsStatic.Misc_Entry_2;
            }
        }

        public bool Meter
        {
            get
            {
                return APDistributionSettingsStatic.Meter;
            }
        }

        public bool Description
        {
            get
            {
                return APDistributionSettingsStatic.Description;
            }
        }

        public bool Authorization
        {
            get
            {
                return APDistributionSettingsStatic.Authorization;
            }
        }

        public string EquipCostCodeFieldName
        {
            get
            {
                return APDistributionSettingsStatic.EquipCostCodeFieldName;
            }
        }

        public bool WarnLWAtInvoiceEntry
        {
            get
            {
                return APDistributionSettingsStatic.WarnLWAtInvoiceEntry;
            }
        }

        public bool WarnCRAtInvoiceEntry
        {
            get
            {
                return APDistributionSettingsStatic.WarnCRAtInvoiceEntry;
            }
        }

        public bool WarnInsAtInvoiceEntry
        {
            get
            {
                return APDistributionSettingsStatic.WarnInsAtInvoiceEntry;
            }
        }

        public bool WarnMiscAtInvoiceEntry
        {
            get
            {
                return APDistributionSettingsStatic.WarnMiscAtInvoiceEntry;
            }
        }

        public JCCostControls JCControls
        {
            get
            {
                return APDistributionSettingsStatic.jcCTLs;
            }
        }

        internal DataTable APM_MASTER__AP_Controls
        {
            get
            {
                return APDistributionSettingsStatic.APM_MASTER__AP_Controls;
            }
        }

        public string AP_Payable_Account
        {
            get
            {
                if (APDistributionSettingsStatic.APM_MASTER__AP_Controls != null && APDistributionSettingsStatic.APM_MASTER__AP_Controls.Rows.Count > 0)
                    return APDistributionSettingsStatic.APM_MASTER__AP_Controls.Rows[0]["AP_Payable_Account"].ToString();

                return "";
            }
        }

        public string Commitment_Payable_Acct
        {
            get
            {
                if (APDistributionSettingsStatic.APM_MASTER__AP_Controls != null && APDistributionSettingsStatic.APM_MASTER__AP_Controls.Rows.Count > 0)
                    return APDistributionSettingsStatic.APM_MASTER__AP_Controls.Rows[0]["Commitment_Payable_Acct"].ToString();

                return "";
            }
        }

        public string PO_Payable_Account
        {
            get
            {
                if (APDistributionSettingsStatic.APM_MASTER__AP_Controls != null && APDistributionSettingsStatic.APM_MASTER__AP_Controls.Rows.Count > 0)
                    return APDistributionSettingsStatic.APM_MASTER__AP_Controls.Rows[0]["PO_Payable_Account"].ToString();

                return "";
            }
        }
    }
}
