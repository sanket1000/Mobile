using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Core.API.DocumentSvc.Entity
{
    //[Serializable]
    //[XmlRoot("Distribution")]
       [DataContract]
    public class DocDistribution 
    {
        [DataMember]
        public Int32 InvoiceID {get;set;}
        [DataMember]
        public Int32 DistSeq { get; set; }
        [DataMember]
        public bool Deleted {get;set;}
        [DataMember]
        public string Description {get;set;}
        [DataMember]
        public decimal Amount { get; set; }
        [DataMember]
        public string TaxGroup {get;set;}
        [DataMember]
        public decimal PreTax { get; set; }
        [DataMember]
        public decimal Tax { get; set; }
        [DataMember]
        public decimal TaxLiability { get; set; }
        [DataMember]
        public decimal DiscountOffered { get; set; }
        [DataMember]
        public decimal Retainage { get; set; }
        [DataMember]
        public decimal MiscDeduction { get; set; }
        [DataMember]
        public decimal MiscDeduction2Percent { get; set; }
        [DataMember]
        public bool Exempt_1099 {get;set;}
        [DataMember]
        public string Authorization {get;set;}
        [DataMember]
        public string JointPayee {get;set;}
        [DataMember]
        public string DistCode {get;set;}
        [DataMember]
        public string ExpenseAccount {get;set;}
        [DataMember]
        public string AccountsPayableAccount {get;set;}
        [DataMember]
        public string Job {get;set;}
        [DataMember]
        public string Extra {get;set;}
        [DataMember]
        public string CostCode {get;set;}
        [DataMember]
        public string Category {get;set;}
        [DataMember]
        public string Commitment {get;set;}
        [DataMember]
        public Int32 Commitment_Line_Item {get;set;}
        [DataMember]
        public decimal Units { get; set; }
        [DataMember]
        public decimal Unit_Cost { get; set; }
        [DataMember]
        public string Draw {get;set;}
        [DataMember]
        public string Standard_Item {get;set;}
        [DataMember]
        public string Equipment {get;set;}
        [DataMember]
        public string Cost_Code_295 {get;set;}
        [DataMember]
        public string Misc_Entry_1 {get;set;}
        [DataMember]
        public decimal Misc_Entry_Units_1 { get; set; }
        [DataMember]
        public string Misc_Entry_2 {get;set;}
        [DataMember]
        public decimal Misc_Entry_Units_2 { get; set; }
        [DataMember]
        public decimal MeterOdometer { get; set; }
        [DataMember]
        public string PM_Tenant {get;set;}
        [DataMember]
        public string PM_Lease {get;set;}
        [DataMember]
        public short PM_Lease_Revision_Num {get;set;}
        [DataMember]
        public string PM_Property {get;set;}
        [DataMember]
        public string PM_Unit {get;set;}
        [DataMember]
        public string PM_Charge_Type {get;set;}
        [DataMember]
        public DateTime PM_Charge_Date {get;set;}
        [DataMember]
        public string PM_Item_ID {get;set;}
        [DataMember]
        public string PM_Chargeback_Description {get;set;}
        [DataMember]
        public decimal PM_Markup_Percent { get; set; }
        [DataMember]
        public decimal PM_Markup_Amount { get; set; }
        [DataMember]
        public string PM_Markup_Charge_Type {get;set;}
        [DataMember]
        public string Operator_Stamp {get;set;}
        [DataMember]
        public DateTime Date_Stamp{get;set;}
        [DataMember]
        public string DistStatus { get; set; }
    }
}
