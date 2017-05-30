//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Core.API.EntityModel
{
    public partial class tblAccrual
    {
        public int ID { get; set; }
        public Nullable<int> ConnectionID { get; set; }
        public Nullable<int> AccrualBatchID { get; set; }
        public Nullable<System.DateTime> AccrualPeriod { get; set; }
        public int InvoiceID { get; set; }
        public Nullable<bool> Selected { get; set; }
        public string Vendor { get; set; }
        public string VendorName { get; set; }
        public string Invoice { get; set; }
        public string Description { get; set; }
        public Nullable<double> Amount { get; set; }
        public Nullable<System.DateTime> Invoice_Date { get; set; }
        public Nullable<System.DateTime> Accounting_Date { get; set; }
        public string Expense_Account { get; set; }
        public string Accounts_Payable_Account { get; set; }
        public string APGLPrefix { get; set; }
        public string Commitment { get; set; }
        public Nullable<int> Commitment_Line_Item { get; set; }
        public string Job { get; set; }
        public string Extra { get; set; }
        public string Cost_Code { get; set; }
        public string Category { get; set; }
        public string ImagePath { get; set; }
    }
    
}
