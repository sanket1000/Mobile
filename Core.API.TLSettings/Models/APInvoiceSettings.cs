using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Core.API.TLSettings.Models
{
    public class APInvoiceSettings
    {
      
        public DataTable adoContRs = null;

        string mvarAmount_Desc;
        string mvarMisc_Deduction_Desc;
        string mvarInvoice_Code_1_Desc;
        string mvarInvoice_Code_2_Desc;

        object mvarAccountingDate;
        object mvarDateReceived;

        ActualAPInvoiceSettings _ActualAPInvoiceSettings = null;

        public APInvoiceSettings(int ConnectionID)
        {
            _ActualAPInvoiceSettings = new ActualAPInvoiceSettings(ConnectionID);
            if(_ActualAPInvoiceSettings != null && _ActualAPInvoiceSettings.ADOContRs != null)
                adoContRs = _ActualAPInvoiceSettings.ADOContRs;
        }

        public object AccountingDate
        {
            set
            {
                _ActualAPInvoiceSettings.AccountingDate = value;
            }
        }

        public object DateReceived
        {
            set
            {
                _ActualAPInvoiceSettings.DateReceived = value;
            }
        }

        public string Amount_Desc
        {
            get
            {
                return _ActualAPInvoiceSettings.Amount_Desc;
            }
        }

        public string Misc_Deduction_Desc
        {
            get
            {
                return _ActualAPInvoiceSettings.Misc_Deduction_Desc;
            }
        }

        public string Invoice_Code_1_Desc
        {
            get
            {
                return _ActualAPInvoiceSettings.Invoice_Code_1_Desc;
            }
        }

        public string Invoice_Code_2_Desc
        {
            get
            {
                return _ActualAPInvoiceSettings.Invoice_Code_2_Desc;
            }
        }

        public bool Invoice_Code_1
        {
            get
            {
                return _ActualAPInvoiceSettings.Invoice_Code_1;
            }
        }

        public bool Invoice_Code_2
        {
            get
            {
                return _ActualAPInvoiceSettings.Invoice_Code_2;
            }
        }

        public bool Pre_Tax_Invoice
        {
            get
            {
                return _ActualAPInvoiceSettings.Pre_Tax_Invoice;
            }
        }

        public bool Tax
        {
            get
            {
                return _ActualAPInvoiceSettings.Tax;
            }
        }

        public bool Discount
        {
            get
            {
                return _ActualAPInvoiceSettings.Discount;
            }
        }

        public bool Misc_Deduction
        {
            get
            {
                return _ActualAPInvoiceSettings.Misc_Deduction;
            }
        }

        public bool Date_Received_Visible
        {
            get
            {
                return _ActualAPInvoiceSettings.Date_Received_Visible;
            }
        }

        public bool Accounting_Date_Visible
        {
            get
            {
                return _ActualAPInvoiceSettings.Accounting_Date_Visible;
            }
        }

        public bool Payment_Date_Visible
        {
            get
            {
                return _ActualAPInvoiceSettings.Payment_Date_Visible;
            }
        }

        public bool Discount_Date_Visible
        {
            get
            {
                return _ActualAPInvoiceSettings.Discount_Date_Visible;
            }
        }

        public bool Payment_Date_Required
        {
            get
            {
                return adoContRs.Rows[0]["Payment_Date_Usage"].ToString().ToUpper() == "REQUIRED"? true : false;
            }
        }
        public bool Date_Received_Required
        {
            get
            {
                return adoContRs.Rows[0]["Date_Received_Usage"].ToString().ToUpper() == "REQUIRED" ? true : false;
            }
        }
         
    }
}
