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
    //[XmlRoot("HeaderConfiguration")]
    public class HeaderConfig
    {
        public List<Header> Headers { get; set; }

        public HeaderConfig()
        {
        }
        public HeaderConfig(int ConnectionID)
        {
            Headers = new List<Header>();
            Init(ConnectionID);
        }

        public void Init(int ConnectionID)
        {
            TimberlineSettings ts = new TimberlineSettings();
            APInvoiceSettings apSettings = ts.GetAPInvoiceSettings(ConnectionID);
         

            if (apSettings != null)
            {
                // vendor
                Headers.Add(SetHeader("Vendor", "True", "", "0", "string", "10", ""));
                Headers.Add(SetHeader("Invoice", "True", "", "1", "string", "100", ""));

                if(apSettings.Invoice_Code_1)
                    Headers.Add(SetHeader("Invoice_Code_1", "False", apSettings.Invoice_Code_1_Desc, "2", "string", "10", ""));

                Headers.Add(SetHeader("Invoice_Date", "True", "Inv Date", "3", "date", "", "MM-dd-yyyy"));

                Headers.Add(SetHeader("Amount", "True",apSettings.Amount_Desc, "4", "string", "100", ""));

                if(apSettings.Tax)
                    Headers.Add(SetHeader("Tax", "False", "Tax", "5", "float", "65", ""));

                if (apSettings.Discount)
                    Headers.Add(SetHeader("Discount_Offered", "False", "Discount Offered", "6", "float", "76", ""));

                if (apSettings.Misc_Deduction)
                    Headers.Add(SetHeader("Misc_Deduction", "False", apSettings.Misc_Deduction_Desc, "7", "float", "75", ""));

                Headers.Add(SetHeader("Description", "False", "Description", "8", "string", "200", ""));

                if(apSettings.Invoice_Code_2)
                    Headers.Add(SetHeader("Invoice_Code_2", "False", apSettings.Invoice_Code_2_Desc, "9", "string", "75", ""));

                if (apSettings.Date_Received_Visible)
                {
                    string drRequired = apSettings.Date_Received_Required ? "True" : "False";
                    Headers.Add(SetHeader("Date_Received", drRequired, "Received", "10", "date", "", "MM-dd-yyyy"));
                }

                if (apSettings.Payment_Date_Visible)
                {
                    string pdRequired = apSettings.Payment_Date_Required ? "True" : "False";
                    Headers.Add(SetHeader("Payment_Date", pdRequired, "Pmt Date", "11", "date", "", "MM-dd-yyyy"));
                }

                if(apSettings.Discount_Date_Visible)
                    Headers.Add(SetHeader("Discount_Date", "False", "Disc Date", "12", "date", "", "MM-dd-yyyy"));

                Headers.Add(SetHeader("Accounting_Date", "True", "Acct Date", "13", "date", "", "MM-dd-yyyy"));

                
            }
        }


        internal Header SetHeader(string Name, string isRequired, string CustomLabel, string Index, string Type, string Length, string Format)
        {

            Header hd = new Header();
            hd.Name = Name;
            hd.IsRequired = isRequired;
            hd.CustomLable = CustomLabel;
            hd.Index = Index;
            hd.Type = Type;
            hd.Length = Length;
            hd.Format = Format;

            return hd;
        }
    }
}
