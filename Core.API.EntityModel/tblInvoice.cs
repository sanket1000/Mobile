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
    public partial class tblInvoice
    {
        public int InvoiceID { get; set; }
        public Nullable<int> ConnectionID { get; set; }
        public Nullable<int> UserGroupID { get; set; }
        public string CurrentActionType { get; set; }
        public Nullable<short> CurrentActionLevel { get; set; }
        public string InvoiceType { get; set; }
        public string InvoiceCode { get; set; }
        public Nullable<System.DateTime> ScanDate { get; set; }
        public Nullable<int> ApprovalGroupID { get; set; }
        public string ScannedBy { get; set; }
        public string Source { get; set; }
        public string Operator { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DateDeleted { get; set; }
        public Nullable<bool> OnHold { get; set; }
        public Nullable<System.DateTime> DateOnHold { get; set; }
        public string OnHoldOperator { get; set; }
        public Nullable<bool> Rejected { get; set; }
        public string RejectedBy { get; set; }
        public Nullable<System.DateTime> DateRejected { get; set; }
        public Nullable<bool> Skipped { get; set; }
        public Nullable<bool> Processed { get; set; }
        public Nullable<System.DateTime> DateProcessed { get; set; }
        public string ProcessedBy { get; set; }
        public Nullable<bool> InsertedInAP { get; set; }
        public Nullable<System.DateTime> DateInserted { get; set; }
        public string InsertedBy { get; set; }
        public Nullable<bool> POAttached { get; set; }
        public string POAttachedBy { get; set; }
        public Nullable<System.DateTime> DatePOAttached { get; set; }
        public Nullable<bool> ReceivingTicketAttached { get; set; }
        public string ReceivingTicketAttachedBy { get; set; }
        public string DateReceivingTicketAttached { get; set; }
        public Nullable<bool> CheckAttached { get; set; }
        public Nullable<System.DateTime> DateCheckAttached { get; set; }
        public string CheckAttachedBy { get; set; }
        public string ImagePath { get; set; }
        public Nullable<System.DateTime> CurrentActionDate { get; set; }
        public string MatchType { get; set; }
        public Nullable<int> Batch { get; set; }
        public Nullable<int> ExportBatch { get; set; }
        public Nullable<System.DateTime> LastAccrueDate { get; set; }
        public Nullable<int> LastAccrueBatch { get; set; }
        public string APInsertMethod { get; set; }
        public Nullable<System.DateTime> OperatorDateTime { get; set; }
        public Nullable<bool> ImageAttached { get; set; }
        public bool HoldInAP { get; set; }
        public bool SentToDM { get; set; }
        public Nullable<System.DateTime> DateSentToDM { get; set; }
        public string SentToDMBy { get; set; }
        public Nullable<bool> SentToAP { get; set; }
        public Nullable<bool> OCRInvoice { get; set; }
        public string OCRMachineDestination { get; set; }
        public Nullable<System.DateTime> SentToAPIDate { get; set; }
    }
    
}