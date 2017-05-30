using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

//using Core.API.Utilities.Helpers;
using Core.API.TLSettings.Data;

namespace Core.API.TLSettings.Models
{
    internal class ActualAPInvoiceSettings
    {

        DataTable adoContRs = null;

        string mvarAmount_Desc;
        string mvarMisc_Deduction_Desc;
        string mvarInvoice_Code_1_Desc;
        string mvarInvoice_Code_2_Desc;

        object mvarAccountingDate;
        object mvarDateReceived;
       
        #region Properties


        public DataTable ADOContRs
        {
            get { return adoContRs; }
        }
        public object AccountingDate
        {
            set
            {
                mvarAccountingDate = value;
            }
        }

        public object DateReceived
        {
            set
            {
                mvarDateReceived = value;
            }
        }

        public string Amount_Desc
        {
            get
            {
                string returnValue = null;
                returnValue = mvarAmount_Desc;
                return returnValue;
            }
        }

        public string Misc_Deduction_Desc
        {
            get
            {
                string returnValue = null;
                returnValue = mvarMisc_Deduction_Desc;
                return returnValue;
            }
        }

        public string Invoice_Code_1_Desc
        {
            get
            {
                string returnValue = null;
                returnValue = mvarInvoice_Code_1_Desc;
                return returnValue;
            }
        }

        public string Invoice_Code_2_Desc
        {
            get
            {
                string returnValue = null;
                returnValue = mvarInvoice_Code_2_Desc;
                return returnValue;
            }
        }

        public bool Invoice_Code_1
        {
            get
            {
                bool returnValue = true;
                try
                {
                    returnValue = (bool)adoContRs.Rows[0]["Use_Invoice_Code_1"]; // ERF
                }
                catch
                {
                    returnValue = false;
                }

                return returnValue;
            }
        }

        public bool Invoice_Code_2
        {
            get
            {
                bool returnValue = true;

                try
                {
                    returnValue = (bool)adoContRs.Rows[0]["Use_Invoice_Code_2"]; // ERF
                }
                catch
                {
                    returnValue = false;
                }

                return returnValue;
            }
        }

        public bool Pre_Tax_Invoice
        {
            get
            {
                bool returnValue = true;
                //returnValue = adoContRs.Rows[0]["Pretax_at_invoice_level"]; // ERF original
                returnValue = (bool)adoContRs.Rows[0]["Pretax_at_invoice_level"]; // ERF
                return returnValue;
            }
        }

        public bool Tax
        {
            get
            {
                bool returnValue = true;
                bool bVisible;
                if (adoContRs.Rows[0]["Tax_Usage"].ToString() == "Not used")
                {
                    bVisible = false;
                }
                else if (adoContRs.Rows[0]["Tax_Entry_Method"].ToString() == "Distributions only") // ERF
                {
                    bVisible = false;
                }
                else
                {
                    bVisible = true;
                }
                returnValue = bVisible;
                return returnValue;
            }
        }

        public bool Discount
        {
            get
            {
                bool returnValue = true;
                returnValue = (adoContRs.Rows[0]["Discount_Usage"] != DBNull.Value && adoContRs.Rows[0]["Discount_Usage"].ToString() == "Invoice level") ? true : false;
                return returnValue;
            }
        }

        public bool Misc_Deduction
        {
            get
            {
                bool returnValue = true;
                if (adoContRs.Columns.Contains("Misc_Deduction_Level"))
                {
                    returnValue = (adoContRs.Rows[0]["Misc_Deduction_Level"] != DBNull.Value && adoContRs.Rows[0]["Misc_Deduction_Level"].ToString() == "Invoice level") ? true : false;
                }
                else
                {
                    returnValue = false;
                }
                return returnValue;
            }
        }

        public bool Date_Received_Visible
        {
            get
            {
                bool returnValue = true;
                returnValue = (adoContRs.Rows[0]["Date_Received_Usage"] != DBNull.Value && adoContRs.Rows[0]["Date_Received_Usage"].ToString() != "Not used") ? true : false;
                return returnValue;
            }
        }

        public bool Accounting_Date_Visible
        {
            get
            {
                if (adoContRs.Rows[0]["Accounting_Date_Usage"] != DBNull.Value && adoContRs.Rows[0]["Accounting_Date_Usage"].ToString().ToUpper() == "INVOICE DATE")
                    return false;
                else
                    return true;
            }
        }

        public bool Payment_Date_Visible
        {
            get
            {
                bool returnValue = true;
                returnValue = (adoContRs.Rows[0]["Payment_Date_Usage"] == DBNull.Value || adoContRs.Rows[0]["Payment_Date_Usage"].ToString() == "Not used") ? false : true;
                return returnValue;
            }
        }

        public bool Discount_Date_Visible
        {
            get
            {
                bool returnValue = true;
                returnValue = (adoContRs.Rows[0]["Discount_Usage"] == DBNull.Value || adoContRs.Rows[0]["Discount_Usage"].ToString() == "Not used") ? false : true;
                return returnValue;
            }
        }

        #endregion

        public ActualAPInvoiceSettings(int ConnectionID)
        {
            Init(ConnectionID);
        }

        #region Methods

        private void Init(int ConnectionID)
        {

            adoContRs = TimberlineDA.GetAPDistributionSettings(ConnectionID);

            GetCustomDescriptions(ConnectionID);

            bool pretaxAtInvoiceLevel = false;

            if (adoContRs != null && adoContRs.Rows.Count > 0)
            {
                if (bool.TryParse(adoContRs.Rows[0]["Pretax_at_invoice_level"].ToString(), out pretaxAtInvoiceLevel))
                {
                    pretaxAtInvoiceLevel = pretaxAtInvoiceLevel;
                }
                else
                {
                    pretaxAtInvoiceLevel = false;
                }
            }


            mvarAmount_Desc = pretaxAtInvoiceLevel ? "Pre-tax" : "Amount";


        }

        private void GetCustomDescriptions(int ConnectionID)
        {
            

            int iFieldNo;
            string sQry = "";
            DataTable aStdRs, aCusRs;
            bool miscDed = false, invoiceCode1 = false, invoiceCode2 = false;

            miscDed = this.adoContRs != null && adoContRs.Rows.Count > 0 && adoContRs.Columns.Contains("Misc_Deduction_Level") && adoContRs.Rows[0]["Misc_Deduction_Level"].ToString().ToUpper() == "INVOICE LEVEL";
            invoiceCode1 = this.adoContRs != null && adoContRs.Rows.Count > 0 && adoContRs.Columns.Contains("Use_Invoice_Code_1") && bool.TryParse(adoContRs.Rows[0]["Use_Invoice_Code_1"].ToString(), out invoiceCode1) && invoiceCode1;
            invoiceCode2 = this.adoContRs != null && adoContRs.Rows.Count > 0 && adoContRs.Columns.Contains("Use_Invoice_Code_2") && bool.TryParse(adoContRs.Rows[0]["Use_Invoice_Code_2"].ToString(), out invoiceCode2) && invoiceCode2;

            if (miscDed || invoiceCode1 || invoiceCode2)
            {

                aStdRs = TimberlineDA.GetAPInvoiceStdDesc(ConnectionID);
                aCusRs = TimberlineDA.GetAPInvoiceCustDesc(ConnectionID);

                for (iFieldNo = 0; iFieldNo < aStdRs.Columns.Count; iFieldNo++)
                {
                    if (aStdRs.Columns[iFieldNo].ColumnName == "Misc_Deduction")
                    {
                        mvarMisc_Deduction_Desc = aCusRs.Columns[(iFieldNo)].ColumnName.Replace("_", " ");
                    }
                    else if (aStdRs.Columns[iFieldNo].ColumnName == "Invoice_Code_1")
                    {
                        mvarInvoice_Code_1_Desc = aCusRs.Columns[(iFieldNo)].ColumnName.Replace("_", " ");
                    }
                    else if (aStdRs.Columns[iFieldNo].ColumnName == "Invoice_Code_2")
                    {
                        mvarInvoice_Code_2_Desc = aCusRs.Columns[(iFieldNo)].ColumnName.Replace("_", " ");
                    }
                }
                aStdRs = null;
                aCusRs = null;
            }


        }

        #endregion
    }
    
    
}
