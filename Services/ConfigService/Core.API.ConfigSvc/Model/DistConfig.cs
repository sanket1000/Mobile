using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Core.API.TLSettings;
using Core.API.TLSettings.Models;

namespace Core.API.ConfigSvc.Model
{
    [Serializable]
    public class DistConfig
    {
        [XmlElement("VendorType")]
        public string VendorType { get; set; }

        //[XmlElement("Distributions")]
        public List<Distribution> Distributions { get; set; }

        [XmlIgnore]
        private APDistributionSettings apSettings = null;

        public DistConfig()
        {
            VendorType = "";
            Distributions = new List<Distribution>();
        }
        public DistConfig(string VendorTypeDesc, APDistributionSettings apsettings)
        {
            VendorType = VendorTypeDesc;
            apSettings = apsettings;
            Distributions = new List<Distribution>();
            Init();
        }

        internal void Init()
        {
            if (apSettings != null)
            {
                //REVISIT-SANKET - get format from invoice entry
                if(apSettings.Commitment)
                    Distributions.Add(new Distribution("Commitment", apSettings.Commitment_Desc, "string", "12", "", "false", "0"));

                //REVISIT-SANKET - get format from invoice entry
                //string.Format(">{0}", "".PadLeft(12, '&')),
                if(apSettings.Equipment)
                    Distributions.Add(new Distribution("Equipment", apSettings.Equipment_Desc, "string", "15", "",  "false", "1"));
                //string.Format(">{0}", "".PadLeft(12, '&'))
                if(apSettings.Eq_Cst_Cd)
                    Distributions.Add(new Distribution("Cost_Code_295", apSettings.Eq_Cost_Code_Desc, "string", "15", "", "false", "2"));

                //REVISIT-SANKET - get format from invoice entry
                if(apSettings.Job)
                    Distributions.Add(new Distribution("Job", "Job", "string", "13", "", "false", "3"));

                //string.Format(">{0}", "".PadLeft(10, '&'))
                if(apSettings.Extra)
                    Distributions.Add(new Distribution("Extra", apSettings.Extra_Desc, "string", "10", "", "false", "4"));

                //REVISIT-SANKET - get format from invoice entry
                if(apSettings.Cost_Code)
                    Distributions.Add(new Distribution("Cost_Code", apSettings.Cost_Code_Desc, "string", "15", "", "false", "5"));

                if(apSettings.Category)
                    Distributions.Add(new Distribution("Category", "Category", "string", "3", "", "false", "6"));

                if(apSettings.Standard_Item)
                    Distributions.Add(new Distribution("Standard_Item", "Standard_Item", "string", "10", "", "false", "7"));

                //REVISIT-SANKET - get sGLPrefix description instead of hardcoded custom label
                if(apSettings.GL_Prefix)
                    Distributions.Add(new Distribution("Company", "Company", "string", "", "", "false", "8"));

                Distributions.Add(new Distribution("Expense_Account", "Account", "string", "30", "", "false", "9"));

                if(apSettings.Payable_Account)
                    Distributions.Add(new Distribution("Accounts_Payable_Account", "Pay Acct", "string", "30", "", "false", "10"));

                if (apSettings.Tax_Group) 
                    Distributions.Add(new Distribution("Tax_Group", "Tax Grp", "string", "6", "", "false", "11"));

                if (apSettings.Units)
                    Distributions.Add(new Distribution("Units", "Units", "float", "", "", "false", "12"));

                if (apSettings.Unit_Cost)
                    Distributions.Add(new Distribution("Unit_Cost", "UnitCost", "float", "", "", "false", "13"));

                
                Distributions.Add(new Distribution("Amount", apSettings.Amount_Desc, "float", "", "", "false", "15"));

                if(apSettings.Tax)
                    Distributions.Add(new Distribution("Tax", "Tax", "float", "", "", "false", "16"));

                if (apSettings.Tax_Liability)
                    Distributions.Add(new Distribution("Tax_Liability", "Tax_Liability", "float", "", "", "false", "17"));

                if (apSettings.Discount)
                Distributions.Add(new Distribution("Discount_Offered", "Discount", "float", "", "", "false", "18"));

                //REVISIT-SANKET - there is a calculation to detrmine visibility of retainage.
                if (apSettings.Retainage)
                    Distributions.Add(new Distribution("Retainage", "Retainage", "float", "", "", "false", "19"));

                if (apSettings.Misc_Ded_1)
                    Distributions.Add(new Distribution("Misc_Deduction", apSettings.Misc_Ded_Desc, "float", "", "", "false", "20"));

                if (apSettings.Misc_Ded_2)
                    Distributions.Add(new Distribution("Misc_Deduction2_Percent", apSettings.Misc_Ded2_Pct_Desc, "float", "", "", "false", "21"));

                 if (apSettings.Dist_Code)
                    Distributions.Add(new Distribution("Dist_Code", apSettings.Dist_Code_Desc, "string", "10", "",  "false", "22"));

                 if (apSettings.Draw)
                    Distributions.Add(new Distribution("Draw","Draw", "string", "15", "", "false", "23"));


                if(apSettings.Misc_Entry_1)
                {
                    string desc1 = string.IsNullOrEmpty(apSettings.Misc_Entry_1_Desc) ? "Misc Entry 1" : this.apSettings.Misc_Entry_1_Desc.Trim();
                    Distributions.Add(new Distribution("Misc_Entry_1", desc1, "string", "10", "", "false", "24"));
                }

                if(apSettings.Misc_Entry_2)
                {
                string desc2 = string.IsNullOrEmpty(apSettings.Misc_Entry_2_Desc) ? "Misc Entry 2" : this.apSettings.Misc_Entry_2_Desc.Trim();
                Distributions.Add(new Distribution("Misc_Entry_2", desc2, "string", "10", "", "false", "25"));
                }

                if(apSettings.Meter)
                    Distributions.Add(new Distribution("MeterOdometer", "Meter", "float", "", "", "false", "26"));

                if(apSettings.Description)
                    Distributions.Add(new Distribution("Description", "Description", "string", "30", "", "false", "27"));

                if(apSettings.Authorization)
                    Distributions.Add(new Distribution("Authorization", apSettings.Authorization_Desc, "string", "10", "", "false", "28"));

                //Distributions.Add(new Distribution("Exempt_1099", "Exempt 1099", "bit", "", "", "false", "29"));

                //Distributions.Add(new Distribution("Joint_Payee", "Joint Payee", "string", "30", "", "false", "30"));

                //Distributions.Add(new Distribution("Commitment_Line_Item", "Commitment Item", "int", "", "", "false", "31"));
             

            }
        }
    }
}
