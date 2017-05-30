using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Core.API.DocumentSvc.Entity
{
    //[Serializable]
    //[XmlRoot("Header")]
    [DataContract]
    public class DocHeader
    {

        public DocHeader()
        {
            Vendor = new DocVendor();
        }


        private DocVendor vendorField;

        private Int32 documentIdField;

        private string documentNameField;

        private string documentDescField;

        private decimal amountField;

        private decimal? taxAmountField;

        private decimal? preTaxAmountField;

        private decimal? discountAmountField;

        private decimal? miscAmountField;

        private DateTime? receivedDateField;

        private DateTime? discountDateField;

        private DateTime? paymentDateField;

        private DateTime invoiceDateField;

        private DateTime accountingDateField;

        private string invoiceCode1Field;

        private string invoiceCode2Field;

        private string smry_Payee_NameField;

        private string smry_Payee_Address_1Field;

        private string smry_Payee_Address_2Field;

        private string smry_Payee_CityField;

        private string smry_Payee_StateField;

        private string smry_Payee_ZIPField;

        private string operator_StampField;

        private DateTime? date_StampField;

        private string currentActionTypeField;

        private Int32 currentActionLevelField;

        private string invoiceTypeField;

        private string invoiceCodeField;

        private string matchTypeField;

        private string headerStatus;


        /// <remarks/>
        [DataMember]
        public DocVendor Vendor
        {
            get
            {
                return this.vendorField;
            }
            set
            {
                this.vendorField = value;
            }
        }
        [DataMember]
        public Int32 DocumentId
        {
            get
            {
                return this.documentIdField;
            }
            set
            {
                this.documentIdField = value;
            }
        }
        /// <remarks/>
        [DataMember]
        public string DocumentName
        {
            get
            {
                return this.documentNameField;
            }
            set
            {
                this.documentNameField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public string DocumentDesc
        {
            get
            {
                return this.documentDescField;
            }
            set
            {
                this.documentDescField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public decimal Amount
        {
            get
            {
                return this.amountField;
            }
            set
            {
                this.amountField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public decimal? TaxAmount
        {
            get
            {
                return this.taxAmountField;
            }
            set
            {
                this.taxAmountField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public decimal? PreTaxAmount
        {
            get
            {
                return this.preTaxAmountField;
            }
            set
            {
                this.preTaxAmountField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public decimal? DiscountAmount
        {
            get
            {
                return this.discountAmountField;
            }
            set
            {
                this.discountAmountField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public decimal? MiscAmount
        {
            get
            {
                return this.miscAmountField;
            }
            set
            {
                this.miscAmountField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public DateTime? ReceivedDate
        {
            get
            {
                return this.receivedDateField;
            }
            set
            {
                this.receivedDateField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public DateTime? DiscountDate
        {
            get
            {
                return this.discountDateField;
            }
            set
            {
                this.discountDateField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public DateTime? PaymentDate
        {
            get
            {
                return this.paymentDateField;
            }
            set
            {
                this.paymentDateField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public DateTime InvoiceDate
        {
            get
            {
                return this.invoiceDateField;
            }
            set
            {
                this.invoiceDateField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public DateTime AccountingDate
        {
            get
            {
                return this.accountingDateField;
            }
            set
            {
                this.accountingDateField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public string InvoiceCode1
        {
            get
            {
                return this.invoiceCode1Field;
            }
            set
            {
                this.invoiceCode1Field = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public string InvoiceCode2
        {
            get
            {
                return this.invoiceCode2Field;
            }
            set
            {
                this.invoiceCode2Field = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public string Smry_Payee_Name
        {
            get
            {
                return this.smry_Payee_NameField;
            }
            set
            {
                this.smry_Payee_NameField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public string Smry_Payee_Address_1
        {
            get
            {
                return this.smry_Payee_Address_1Field;
            }
            set
            {
                this.smry_Payee_Address_1Field = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public string Smry_Payee_Address_2
        {
            get
            {
                return this.smry_Payee_Address_2Field;
            }
            set
            {
                this.smry_Payee_Address_2Field = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public string Smry_Payee_City
        {
            get
            {
                return this.smry_Payee_CityField;
            }
            set
            {
                this.smry_Payee_CityField = value;
            }
        }

        /// <remarks/>
        public string Smry_Payee_State
        {
            get
            {
                return this.smry_Payee_StateField;
            }
            set
            {
                this.smry_Payee_StateField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public string Smry_Payee_ZIP
        {
            get
            {
                return this.smry_Payee_ZIPField;
            }
            set
            {
                this.smry_Payee_ZIPField = value;
            }
        }

        /// <remarks/>
        public string Operator_Stamp
        {
            get
            {
                return this.operator_StampField;
            }
            set
            {
                this.operator_StampField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public DateTime? Date_Stamp
        {
            get
            {
                return this.date_StampField;
            }
            set
            {
                this.date_StampField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public string CurrentActionType
        {
            get
            {
                return this.currentActionTypeField;
            }
            set
            {
                this.currentActionTypeField = value;
            }
        }

        /// <remarks/>
        public Int32 CurrentActionLevel
        {
            get
            {
                return this.currentActionLevelField;
            }
            set
            {
                this.currentActionLevelField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public string InvoiceType
        {
            get
            {
                return this.invoiceTypeField;
            }
            set
            {
                this.invoiceTypeField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public string InvoiceCode
        {
            get
            {
                return this.invoiceCodeField;
            }
            set
            {
                this.invoiceCodeField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public string MatchType
        {
            get
            {
                return this.matchTypeField;
            }
            set
            {
                this.matchTypeField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public string HeaderStatus
        {
            get
            {
                return this.headerStatus;
            }
            set
            {
                this.headerStatus = value;
            }
        }
    }

}
