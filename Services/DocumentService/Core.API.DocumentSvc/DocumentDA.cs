using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.API.EntityModel;
using Core.API.Utilities;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Data.Objects;
using Core.API.Utilities;
using Core.API.Utilities.Helpers;
using Core.API.DocumentSvc.Entity;
using System.Data;
using System.Configuration;
using Microsoft.VisualBasic;

namespace Core.API.DocumentSvc
{
    internal class DocumentDA
    {

        //int ConnectionID = 0;
        string _TimberScanConnString = null;
        string _TimberSyncEntityConnString = null;
        bool? _APIMessageLogFlag = null;
        public DocumentDA()
        {
           //this.ConnectionID =  GetDefaultConnection();
        }
        internal string TimberScanConnString
        {
            get
            {
                if (string.IsNullOrEmpty(_TimberScanConnString))
                {
                    _TimberScanConnString = Core.API.Utilities.Helpers.APIUtilityHelper.TimberScanEntityConnectionString;
                }
                return _TimberScanConnString;
            }
        }
        internal string TimberSyncEntityConnString
        {
            get
            {
                if (string.IsNullOrEmpty(_TimberSyncEntityConnString))
                {
                    _TimberSyncEntityConnString = Core.API.Utilities.Helpers.APIUtilityHelper.TimberSyncEntityConnectionString;
                }
                return _TimberSyncEntityConnString;
            }
        }

        internal bool APIMessageLogFlag
        {
            get
            {
                if (_APIMessageLogFlag == null)
                {
                    bool flg = false;
                    try
                    {
                        bool.TryParse(System.Configuration.ConfigurationManager.AppSettings["APILogFlag"].ToString(), out flg);
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        _APIMessageLogFlag = flg;
                    }
                }
                return _APIMessageLogFlag.Value;
            }
        }

        //internal Int32 GetDefaultConnection()
        //{
        //    using (var ctx = new CoreAPIEntities(TimberScanConnString))
        //    {
        //        return ctx.GetDefaultConnection().AsQueryable().First().Value;

        //    }
        //}

        #region API Methods

        internal string GetDocument(int DocumentID)
        {
            string retMessage;
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                retMessage = ctx.GetDocument(DocumentID).AsQueryable().First().ToString();
            }
            return retMessage;
        }

        internal string GetDocumentList(int? ConnectionID)
        {
            string retMessage;
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                retMessage = ctx.GetDocumentListInApproval(ConnectionID).AsQueryable().First().ToString();

            }
            return retMessage;
        }

        internal string GetDocumentImagePath(int DocumentID)
        {
            string retMessage;
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                retMessage = ctx.GetDocumentImagePath(DocumentID).AsQueryable().First().ToString();

            }
            return retMessage;
        }

        internal string GetDocumentNotes(int DocumentID)
        {
            string retMessage;
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                retMessage = ctx.GetDocumentNotes(DocumentID).AsQueryable().First().ToString();

            }
            return retMessage;
        }

        internal RetMessage SaveComment(int DocumentID, string NoteText, DateTime NoteDate, int UserId)
        {
            RetMessage retMessage = new RetMessage();
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                ObjectParameter outRetMessage = new ObjectParameter("RetMessage", typeof(string));
                ObjectParameter outRetSequence = new ObjectParameter("RetSequence", typeof(int));
                //ctx.SaveDocumentNote(DocumentID, NoteText, NoteDate, UserId, outRetMessage).AsQueryable().First().ToString();
                ctx.SaveDocumentNote(DocumentID, NoteText, NoteDate, UserId, outRetMessage, outRetSequence).AsQueryable().First().ToString();
                if (outRetMessage != null)
                    retMessage.Message = outRetMessage.Value.ToString();
                if (outRetSequence != null)
                    retMessage.Id = Convert.ToInt32(outRetSequence.Value);


            }
            return retMessage;
        }

        internal string DeleteInvoice(int lInvID, string UserID, int ActionLevel, string DeleteReason) // done - update
        {

            string retMessage = ""; ;
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                ObjectParameter outRetMessage = new ObjectParameter("RetMessage", typeof(string));
                ctx.DeleteDocument(lInvID, UserID, ActionLevel, DeleteReason, outRetMessage).AsQueryable().First().ToString();
                if (outRetMessage != null)
                    retMessage = outRetMessage.Value.ToString();

            }
            return retMessage;

            //string sql = "UPDATE tblInvoices SET Deleted=1, DateDeleted=" + CommonFunctions.QCom(CommonFunctions.SQLDate(DateTime.Now))
            //    + " CurrentActionType=Null, CurrentActionLevel=0, OnHoldOperator=NULL,"
            //    + " CurrentActionDate=" + CommonFunctions.QCom(CommonFunctions.SQLDate(DateTime.Now))
            //    + " OnHold=0, Rejected=0, Operator=Null, DeletedBy=" + CommonFunctions.QNApos(UserID)
            //    + " WHERE InvoiceID = " + lInvID.ToString();


            //APIUtilDataAccessHelper.TimberScanData.NonQuery(sql);

            //sql = "UPDATE APM_MASTER__INVOICE SET Deleted=1, POHeaderID = 0 WHERE InvoiceID = " + lInvID;
            //APIUtilDataAccessHelper.TimberScanData.NonQuery(sql);

            //sql = "UPDATE APM_MASTER__INVOICE_Import SET Deleted=1 WHERE InvoiceID = " + lInvID;
            //APIUtilDataAccessHelper.TimberScanData.NonQuery(sql);

            ////sql = "SELECT POHeaderID FROM APM_MASTER__INVOICE" +
            ////        " WHERE InvoiceID = " + lInvID;

            ////DataTable dt = new DataTable();
            ////APIUtilDataAccessHelper.TimberScanData.FillDataTable(dt, sql, null, true, false);

            //return true;

        }

        internal string HoldInvoice(int DocumentID, int ActionLevel, string UserID, string HoldReason, DateTime HoldDate)
        {

            string retMessage = ""; ;
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                ObjectParameter outRetMessage = new ObjectParameter("RetMessage", typeof(string));
                ctx.HoldDocument(DocumentID, UserID, ActionLevel, HoldReason, HoldDate, outRetMessage).AsQueryable().First().ToString();
                if (outRetMessage != null)
                    retMessage = outRetMessage.Value.ToString();

            }
            return retMessage;
            //string sQry = "UPDATE tblInvoices SET OnHold=1, DateOnHold=" + CommonFunctions.QCom(CommonFunctions.SQLDate(DateTime.Now)) +
            //    " OnHoldOperator=" + CommonFunctions.QNAposCom(UserID) +
            //    " Operator=Null" + " WHERE InvoiceID = " + lInvID;

            //APIUtilDataAccessHelper.TimberScanData.NonQuery(sQry);

            //sQry = "UPDATE tblInvoiceActions SET Hold=1 WHERE ActionID = " + lActID.ToString();

            //APIUtilDataAccessHelper.TimberScanData.NonQuery(sQry);

            //return true;

        }

        internal string HoldInvoiceInAP(int DocumentID, int ActionLevel, string UserID, string APHoldReason, DateTime APHoldDate, bool OnAPHold)
        {
            string retMessage = ""; ;
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                ObjectParameter outRetMessage = new ObjectParameter("RetMessage", typeof(string));
                ctx.HoldDocumentInAP(DocumentID, UserID, ActionLevel, APHoldReason, APHoldDate, OnAPHold, outRetMessage).AsQueryable().First().ToString();
                if (outRetMessage != null)
                    retMessage = outRetMessage.Value.ToString();

            }
            return retMessage;

            //string sMsg = String.Empty;

            //string sql = "UPDATE tblInvoices SET HoldInAP=" + (bHold ? "1" : "0") + " WHERE InvoiceID=" + lInvID.ToString();

            //APIUtilDataAccessHelper.TimberScanData.NonQuery(sql);
            //if (bHold == true)
            //{
            //    sMsg = "Invoice placed on hold in A/P.";
            //}
            //else
            //{
            //    sMsg = "Invoice removed from hold in A/P.";
            //}
            //this.AddAPInvoiceNoteDB(lInvID, sMsg, "Log", sAction, iActlvl, sUserID, DateTime.Now, "Normal", -1);

        }

        internal string SaveDocument(DocumentEnt doc)
        {
            string retMessage = "";

            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                //get systemsettings
                //var sysSettings = ctx.tblSystemSettings.FirstOrDefault();

                //this.ConnectionID = this.GetDefaultConnection();
                int ConnectionID = this.GetConnectionIDByDocumentID(doc.Header.DocumentId);
                int documentID = doc.Header.DocumentId;
                string operatorstamp = this.GetUserId(doc.UserID);
                bool saveChanges = false;
                //save invocie header
                if (doc.Header.HeaderStatus.ToUpper().Equals("U"))
                {
                    APM_MASTER__INVOICE invHeader = ctx.APM_MASTER__INVOICE.Where(x => x.ConnectionID == ConnectionID && x.InvoiceID == doc.Header.DocumentId).FirstOrDefault();
                    if (invHeader != null)
                    {
                        invHeader.Invoice = doc.Header.DocumentName;
                        invHeader.Description = doc.Header.DocumentDesc;


                        invHeader.Amount = Convert.ToDouble(doc.Header.Amount);


                        double outTaxAmount = 0.0;
                        if (doc.Header.TaxAmount != null)
                            double.TryParse(doc.Header.TaxAmount.ToString(), out outTaxAmount);

                        if (doc.Header.TaxAmount.HasValue)
                            invHeader.Tax = Convert.ToDouble(doc.Header.TaxAmount);
                        else
                            invHeader.Tax = 0;

                        double outDiscAmount = 0.0;

                        invHeader.Discount_Offered = Convert.ToDouble(doc.Header.DiscountAmount);
                        invHeader.Misc_Deduction = Convert.ToDouble(doc.Header.MiscAmount);

                        //DateTime dt = DateTime.MinValue;
                        //DateTime.TryParse(doc.Header.InvoiceDate.ToString(), out dt);
                        //if(dt != DateTime.MinValue)
                        invHeader.Invoice_Date = doc.Header.InvoiceDate;

                        //dt = DateTime.MinValue;
                        //DateTime.TryParse(doc.Header.ReceivedDate.ToString(), out dt);
                        //if (dt != DateTime.MinValue)
                        invHeader.Date_Received = doc.Header.ReceivedDate;

                        //dt = DateTime.MinValue;
                        //DateTime.TryParse(doc.Header.DiscountDate.ToString(), out dt);
                        //if (dt != DateTime.MinValue)
                        invHeader.Discount_Date = doc.Header.DiscountDate;

                        //dt = DateTime.MinValue;
                        //DateTime.TryParse(doc.Header.PaymentDate.ToString(), out dt);
                        //if (dt != DateTime.MinValue)
                        invHeader.Payment_Date = doc.Header.PaymentDate;

                        //dt = DateTime.MinValue;
                        //DateTime.TryParse(doc.Header.AccountingDate.ToString(), out dt);
                        //if (dt != DateTime.MinValue)
                        invHeader.Accounting_Date = doc.Header.AccountingDate;

                        invHeader.Invoice_Code_1 = doc.Header.InvoiceCode1;
                        invHeader.Invoice_Code_2 = doc.Header.InvoiceCode2;
                        invHeader.Smry_Payee_Name = doc.Header.Smry_Payee_Name;
                        invHeader.Smry_Payee_Address_1 = doc.Header.Smry_Payee_Address_1;
                        invHeader.Smry_Payee_Address_2 = doc.Header.Smry_Payee_Address_2;
                        invHeader.Smry_Payee_City = doc.Header.Smry_Payee_City;
                        invHeader.Smry_Payee_State = doc.Header.Smry_Payee_State;
                        invHeader.Smry_Payee_ZIP = doc.Header.Smry_Payee_ZIP;
                        invHeader.Operator_Stamp = operatorstamp;
                        invHeader.Date_Stamp = DateTime.Now;
                        LogError("CoreAPIDocumentSvc", "SaveDocument", "BEGIN", "Saving header changes", documentID.ToString(), "");
                        //ctx.AcceptAllChanges();
                        saveChanges = true;
                        LogError("CoreAPIDocumentSvc", "SaveDocument", "END", "Saving header changes", documentID.ToString(), "");
                    }
                }

                foreach (var dist in doc.Distributions)
                {
                    ObjectParameter outRetMessage = new ObjectParameter("RetMessage", typeof(string));

                    if (dist.DistStatus.ToUpper().Equals("I")) //Insert
                    {
                        APM_MASTER__DISTRIBUTION newDist = new APM_MASTER__DISTRIBUTION();
                        newDist.ConnectionID = ConnectionID;
                        newDist.InvoiceID = documentID;
                        newDist.DistSeq = dist.DistSeq;// ask hh2 to update distsequence for new row
                        newDist.Deleted = false;
                        newDist.Description = dist.Description;
                        newDist.Amount = Convert.ToDouble(dist.Amount);
                        newDist.Tax_Group = dist.TaxGroup;
                        newDist.Tax = Convert.ToDouble(dist.Tax);
                        newDist.Tax_Liability = Convert.ToDouble(dist.TaxLiability);
                        newDist.Discount_Offered = Convert.ToDouble(dist.DiscountOffered);
                        newDist.Retainage = Convert.ToDouble(dist.Retainage);
                        newDist.Misc_Deduction = Convert.ToDouble(dist.MiscDeduction);
                        newDist.Misc_Deduction2_Percent = Convert.ToDouble(dist.MiscDeduction2Percent);
                        newDist.Exempt_1099 = dist.Exempt_1099;
                        newDist.Authorization = dist.Authorization;
                        newDist.Joint_Payee = dist.JointPayee;
                        newDist.Dist_Code = dist.DistCode;
                        newDist.Expense_Account = dist.ExpenseAccount;
                        newDist.Accounts_Payable_Account = dist.AccountsPayableAccount;
                        newDist.Job = dist.Job;
                        newDist.Extra = dist.Extra;
                        newDist.Cost_Code = dist.CostCode;
                        newDist.Category = dist.Category;
                        newDist.Commitment = dist.Commitment;
                        newDist.Commitment_Line_Item = dist.Commitment_Line_Item;
                        newDist.Units = Convert.ToDouble(dist.Units);
                        newDist.Unit_Cost = Convert.ToDouble(dist.Unit_Cost);
                        newDist.Draw = dist.Draw;
                        newDist.Standard_Item = dist.Standard_Item;
                        newDist.Equipment = dist.Equipment;
                        newDist.Cost_Code_295 = dist.Cost_Code_295;
                        newDist.Misc_Entry_1 = dist.Misc_Entry_1;
                        newDist.Misc_Entry_Units_1 = Convert.ToDouble(dist.Misc_Entry_Units_1);
                        newDist.Misc_Entry_2 = dist.Misc_Entry_2;
                        newDist.Misc_Entry_Units_2 = Convert.ToDouble(dist.Misc_Entry_Units_2);
                        newDist.MeterOdometer = Convert.ToDouble(dist.MeterOdometer);
                        newDist.Operator_Stamp = operatorstamp;
                        newDist.Date_Stamp = DateTime.Now;

                        ctx.APM_MASTER__DISTRIBUTION.AddObject(newDist);

                        LogError("CoreAPIDocumentSvc", "SaveDocument", "BEGIN", "Add Distribution", documentID.ToString(), dist.DistSeq.ToString());
                        //ctx.AcceptAllChanges();
                        saveChanges = true;
                        LogError("CoreAPIDocumentSvc", "SaveDocument", "END", "Add Distribution", documentID.ToString(), dist.DistSeq.ToString());

                    }
                    else if (dist.DistStatus.ToUpper().Equals("U")) //Updte
                    {
                        APM_MASTER__DISTRIBUTION curDist = ctx.APM_MASTER__DISTRIBUTION.Where(x => x.ConnectionID == ConnectionID && x.InvoiceID == documentID && x.DistSeq == dist.DistSeq).FirstOrDefault();

                        if (curDist != null)
                        {
                            curDist.InvoiceID = documentID;
                            curDist.Description = dist.Description;
                            curDist.Amount = Convert.ToDouble(dist.Amount);
                            curDist.Tax_Group = dist.TaxGroup;
                            curDist.Tax = Convert.ToDouble(dist.Tax);
                            curDist.Tax_Liability = Convert.ToDouble(dist.TaxLiability);
                            curDist.Discount_Offered = Convert.ToDouble(dist.DiscountOffered);
                            curDist.Retainage = Convert.ToDouble(dist.Retainage);
                            curDist.Misc_Deduction = Convert.ToDouble(dist.MiscDeduction);
                            curDist.Misc_Deduction2_Percent = Convert.ToDouble(dist.MiscDeduction2Percent);
                            curDist.Exempt_1099 = dist.Exempt_1099;
                            curDist.Authorization = dist.Authorization;
                            curDist.Joint_Payee = dist.JointPayee;
                            curDist.Dist_Code = dist.DistCode;
                            curDist.Expense_Account = dist.ExpenseAccount;
                            curDist.Accounts_Payable_Account = dist.AccountsPayableAccount;
                            curDist.Job = dist.Job;
                            curDist.Extra = dist.Extra;
                            curDist.Cost_Code = dist.CostCode;
                            curDist.Category = dist.Category;
                            curDist.Commitment = dist.Commitment;
                            curDist.Commitment_Line_Item = dist.Commitment_Line_Item;
                            curDist.Units = Convert.ToDouble(dist.Units);
                            curDist.Unit_Cost = Convert.ToDouble(dist.Unit_Cost);
                            curDist.Draw = dist.Draw;
                            curDist.Standard_Item = dist.Standard_Item;
                            curDist.Equipment = dist.Equipment;
                            curDist.Cost_Code_295 = dist.Cost_Code_295;
                            curDist.Misc_Entry_1 = dist.Misc_Entry_1;
                            curDist.Misc_Entry_Units_1 = Convert.ToDouble(dist.Misc_Entry_Units_1);
                            curDist.Misc_Entry_2 = dist.Misc_Entry_2;
                            curDist.Misc_Entry_Units_2 = Convert.ToDouble(dist.Misc_Entry_Units_2);
                            curDist.MeterOdometer = Convert.ToDouble(dist.MeterOdometer);
                            curDist.Operator_Stamp = operatorstamp;
                            curDist.Date_Stamp = DateTime.Now;
                            LogError("CoreAPIDocumentSvc", "SaveDocument", "BEGIN", "Update Distribution", documentID.ToString(), dist.DistSeq.ToString());
                            // ctx.AcceptAllChanges();
                            saveChanges = true;
                            LogError("CoreAPIDocumentSvc", "SaveDocument", "END", "Update Distribution", documentID.ToString(), dist.DistSeq.ToString());
                        }
                    }
                }
                //update document
                if (saveChanges)
                    ctx.SaveChanges();

                foreach (var dist in doc.Distributions)
                {
                    ObjectParameter outRetMessage = new ObjectParameter("RetMessage", typeof(string));
                    if (dist.DistStatus.ToUpper().Equals("D")) //Deleted
                    {
                        //if (sysSettings.AllocateTaxes.HasValue && sysSettings.AllocateTaxes.Value)
                        //{

                        ctx.API_DeleteTaxAllocation(documentID, dist.DistSeq, dist.Operator_Stamp, outRetMessage).AsQueryable().First().ToString();
                        if (outRetMessage != null)
                            retMessage = outRetMessage.Value.ToString();
                        //}
                        //insert shadow
                        //outRetMessage.Value = "";
                        //ctx.InsertShadowDistRecord(documentID, dist.DistSeq, outRetMessage).AsQueryable().First().ToString();
                        //if (outRetMessage != null)
                        //    retMessage = outRetMessage.Value.ToString();

                        // delete distribution - hard delete
                        outRetMessage.Value = "";
                        ctx.DeleteDistribution(documentID, dist.DistSeq, operatorstamp, outRetMessage);
                        if (outRetMessage != null)
                            retMessage = outRetMessage.Value.ToString();
                        LogError("CoreAPIDocumentSvc", "SaveDocument", "BEGIN", "Delete Distribution", documentID.ToString(), dist.DistSeq.ToString());
                        //ctx.AcceptAllChanges();
                        saveChanges = true;
                        LogError("CoreAPIDocumentSvc", "SaveDocument", "END", "Delete Distribution", documentID.ToString(), dist.DistSeq.ToString());
                    }
                }
            }

            return retMessage;
        }

        internal string ApproveDocument(DocumentEnt doc)
        {
            string retMessage = "";
            //int documentID = doc.Header.DocumentId;
            string strUserID = GetUserId(doc.UserID);
            //approve document
            //ApproveInvoiceAction(doc.DocumentID, strUserID, doc.ApproveType, strUserID);
            ApproveInvoiceAction(doc.DocumentID, "", "MOBILE", strUserID); //05/06/2017 -- sanket
            return retMessage;
        }

        internal string RouteDocumentManual(int DocumentID, int ActionLevel, int RoutingUserID, int RouteToID, string RouteToComment)
        {
            string retMessage = ""; ;
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                ObjectParameter outRetMessage = new ObjectParameter("RetMessage", typeof(string));
                ctx.RouteDocumentManual(DocumentID, ActionLevel, RoutingUserID, RouteToID, RouteToComment, outRetMessage);
                if (outRetMessage != null)
                    retMessage = outRetMessage.Value.ToString();

            }
            return retMessage;
        }
        #endregion

        #region Invoice Related

        internal DataSet GetInvoice(int InvoiceId)
        {
            //using (var ctx = new CoreAPIEntities(TimberScanConnString))
            //{
            //    return ctx.GetInvoice(InvoiceId).FirstOrDefault();
            //}

            DataSet retVal = new DataSet();
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {

                EntityConnection entityConn = (EntityConnection)ctx.Connection;
                SqlConnection sqlConn = (SqlConnection)entityConn.StoreConnection;
                SqlCommand cmd = new SqlCommand("API_GetInvoice", sqlConn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                using (cmd)
                {
                    SqlParameter prm = new SqlParameter("InvoiceId", InvoiceId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(prm);
                    da.Fill(retVal);
                }
            }
            return retVal;
        }

        internal DataSet GetInvoiceDistributions(int InvoiceId)
        {
            DataSet retVal = new DataSet();
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {

                EntityConnection entityConn = (EntityConnection)ctx.Connection;
                SqlConnection sqlConn = (SqlConnection)entityConn.StoreConnection;
                SqlCommand cmd = new SqlCommand("API_GetInvoiceDistributions", sqlConn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                using (cmd)
                {
                    SqlParameter prm = new SqlParameter("InvoiceId", InvoiceId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(prm);
                    da.Fill(retVal);
                }
            }
            return retVal;
        }

        internal GetInvoice_Result GetInvoiceHeaderEnt(int InvoiceId)
        {
            GetInvoice_Result retVal = new GetInvoice_Result();
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                retVal = ctx.GetInvoice(InvoiceId).FirstOrDefault();
            }

            return retVal;
        }

        internal List<GetInvoiceDistributions_Result> GetInvoiceDistributionEnt(int InvoiceId)
        {
            List<GetInvoiceDistributions_Result> retList = new List<GetInvoiceDistributions_Result>();
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                retList = ctx.GetInvoiceDistributions(InvoiceId).ToList();
            }

            return retList;
        }

        internal string ValidateInvoice(int DocumentID, int ActionLevel, int UserID)
        {
            string retMessage = ""; ;
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                ObjectParameter outRetMessage = new ObjectParameter("RetMessage", typeof(string));
                ctx.ValidateDocument(DocumentID, UserID, ActionLevel, outRetMessage);
                if (outRetMessage != null)
                    retMessage = outRetMessage.Value.ToString();
            }
            return retMessage;
        }

        internal string DocumentExists(int DocumentID)
        {
            string retMessage = "";
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                //LogError("DocumentExists", "ConnectionString", TimberScanConnString, "DocumentExists", "", "0");

                ObjectParameter outRetMessage = new ObjectParameter("RetMessage", typeof(string));
                ctx.DocumentExists(DocumentID, outRetMessage).AsQueryable().First().ToString();
                if (outRetMessage != null)
                    retMessage = outRetMessage.Value.ToString();

            }
            return retMessage;
        }

        internal Int32 RouteInvoice(Int32 connectionId, Int32 invoiceId, Int32 processGroupId, string userId, Int32? groupId, string ActionType, Int32 actionLevel, bool hold, bool rejected, DateTime dateAssigned, string assignedBy, string Operator)
        {

            Int32 actionId = -1;

            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                object obj = ctx.RouteInvoice(connectionId, invoiceId, userId, groupId, processGroupId, ActionType, actionLevel, hold, rejected, dateAssigned, assignedBy, Operator);
                Int32.TryParse(obj.ToString(), out actionId);
            }

            return actionId;

        }

        internal int AddAPInvoiceAction(int lInvID, string sUserID, object lProcGrpID, int lGroupID, string sAction, short iActionLevel, string sAssignedBy, bool bRejected, int lConnID)
        {
            int lConnectID = this.GetConnectionIDByDocumentID(lInvID);
            string sQry = string.Empty;
            int actionId = 0;

            //Note sUserID and lGroupID are mutually exclusive
            //we first test to see if the same action was assigned to this user as the result of another action being completed
            //ie we have an invoice that went to 2 or more level 1 approvers that goes to a common level 2 approver
            //we don't want to send it to the level 2 approver twice

            if (lGroupID > 0)
            {
                sQry = "INSERT INTO tblInvoiceActions (ConnectionID, InvoiceID, ProcessGroupID, GroupID, ActionType, ActionLevel, DateAssigned, AssignedBy, Rejected)" +
                    " VALUES (" + CommonFunctions.QCom(lConnectID) + CommonFunctions.QCom(lInvID) + CommonFunctions.QCom(lProcGrpID) + CommonFunctions.QCom(lGroupID) +
                    CommonFunctions.QNAposCom((sAction.Trim())) + CommonFunctions.QCom(iActionLevel) + CommonFunctions.QNAposCom(CommonFunctions.GetCurrentDateAndTimeOnSqlServer()) + CommonFunctions.QNAposCom(sAssignedBy) +
                    (bRejected ? "1" : "0") + ")";
            }
            else
            {
                sQry = "INSERT INTO tblInvoiceActions (ConnectionID, InvoiceID, ProcessGroupID, UserID, ActionType, ActionLevel, DateAssigned, AssignedBy, Rejected)" +
                    " VALUES (" + CommonFunctions.QCom(lConnectID) + CommonFunctions.QCom(lInvID) + CommonFunctions.QCom(lProcGrpID) + CommonFunctions.QNAposCom(sUserID) +
                    CommonFunctions.QNAposCom((sAction.Trim())) + CommonFunctions.QCom(iActionLevel) + CommonFunctions.QNAposCom(CommonFunctions.GetCurrentDateAndTimeOnSqlServer()) + CommonFunctions.QNAposCom(sAssignedBy) + (bRejected ? "1" : "0") + ")";
            }

            object identity = APIUtilDataAccessHelper.TimberScanData.ExecuteInsert(sQry);

            if (identity != null)
            {
                actionId = Convert.ToInt32(identity);
            }

            return actionId;

        }

        internal bool AddAPInvoiceNoteDB(int lInvID, string sNote, string sLogOrNote, string sAction, short iAvtLvl, string sOperator, object vNoteDate, string sPriority, int lConnID) // done - insert
        {
            //sLogOrNote - Log (System Log Entry) or Note (Operator Comment)
            //Priority: "Normal", "High", "Critical"
            tblInvoiceNote tIN;
            object vNDate;
            int lConnectID = this.GetConnectionIDByDocumentID(lInvID);
            string sNoteText;

            if (Information.IsDBNull(vNoteDate) == false)
            {
                vNDate = vNoteDate;
            }
            else
            {
                vNDate = DateTime.Now;
            }

            if (sNote.Length > 255)
            {
                sNoteText = sNote.Substring(0, 255);
            }
            else
            {
                sNoteText = sNote;
            }

            string sQry = "INSERT INTO tblInvoiceNotes (ConnectionID, InvoiceID, NoteType, ActionType, ActionLevel, Priority, NoteDate, Operator, NoteText)" +
                " VALUES (" + CommonFunctions.QCom(lConnectID) +
                CommonFunctions.QCom(lInvID) + CommonFunctions.QNAposCom(sLogOrNote) +
                CommonFunctions.QNAposCom(sAction) + CommonFunctions.QCom(iAvtLvl) + CommonFunctions.QNAposCom(sPriority) +
                CommonFunctions.QNValCom(CommonFunctions.SQLDate(vNDate)) + CommonFunctions.QNAposCom(sOperator).ToUpper() +
                CommonFunctions.QNApos(sNoteText) + ")";

            APIUtilDataAccessHelper.TimberScanData.NonQuery(sQry);
            return true;
        }

        private void ApproveInvoiceAction(int InvoiceId, string UserIdApprovalIsFor, string ApprovalType, string Approver)
        {
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                ctx.ApproveInvoiceAction(InvoiceId, UserIdApprovalIsFor, ApprovalType, Approver);
            }
        }

        internal bool SetNextInvoiceAction(int lInvID, string sNxtAct, short iNxtLvl, int lConnID) // done - update 
        {
            string sQry = string.Empty;

            if (lConnID < 0)
            {
                sQry = "UPDATE tblInvoices SET CurrentActionType=" + CommonFunctions.QNAposCom(sNxtAct) +
                    " CurrentActionLevel=" + CommonFunctions.QCom(iNxtLvl) +
                    " CurrentActionDate=" + CommonFunctions.QCom(CommonFunctions.SQLDate(DateTime.Now)) +
                    " OnHold=0, DateOnHold=NULL, OnHoldOperator=NULL, Operator=Null" +
                    " WHERE InvoiceID = " + lInvID;
            }
            else
            {
                sQry = "UPDATE tblInvoices SET CurrentActionType=" + CommonFunctions.QNAposCom(sNxtAct) +
                    " CurrentActionLevel=" + CommonFunctions.QCom(iNxtLvl) +
                    " CurrentActionDate=" + CommonFunctions.QCom(CommonFunctions.SQLDate(DateTime.Now)) +
                    " ConnectionID=" + CommonFunctions.QCom(lConnID) +
                    " OnHold=0, DateOnHold=NULL, OnHoldOperator=NULL, Operator=Null" +
                    " WHERE InvoiceID = " + lInvID;
            }

            APIUtilDataAccessHelper.TimberScanData.NonQuery(sQry);
            return true;

        }

        internal void UpdateInvoiceActionInfo(int InvoiceId, int Level, string ActionType)
        {

            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                ctx.UpdateInvoiceWithActionInfo(InvoiceId, Level, ActionType);
            }
        }

        internal bool CloseAllActions(int lInvID, string sUserID, string sComment, short iActLvl, object vTimeStamp) // done - update
        {
            //this closes all open actions for an invoice
            //it is used when an invoice is marked as deleted or when an adminstrator is approving invoices
            //for another approver and the invoice is being marked as final approval
            //or an administrator is approving invoices for <All Users>
            int lActionID;
            string sql = string.Empty;
            DateTime dTimeStamp = DateTime.Now;

            if (vTimeStamp != null && DateTime.TryParse(vTimeStamp.ToString(), out dTimeStamp))
            {
                dTimeStamp = Convert.ToDateTime(vTimeStamp);
            }
            else
            {
                dTimeStamp = DateTime.Now;
            }

            dTimeStamp = dTimeStamp.AddSeconds(-1);

            sql = "UPDATE tblInvoiceActions SET CompletedBy=" + CommonFunctions.QNAposCom(sUserID)
            + "Complete=1, DateComplete=" + CommonFunctions.SQLDate(dTimeStamp);

            if (!CommonFunctions.IsNullOrEmpty(sComment))
            {
                sql += ", Comment=" + CommonFunctions.QNApos(sComment);
            }

            sql += " WHERE InvoiceID = " + lInvID.ToString() + " AND Complete = 0";

            if (iActLvl > 0)
            {
                sql += " AND ActionLevel = " + iActLvl.ToString();
            }

            APIUtilDataAccessHelper.TimberScanData.NonQuery(sql);

            return true;

        }

        internal DataTable GetRoutingInfo()
        {
            //using (var ctx = new CoreAPIEntities(TimberScanConnString))
            //{
            //    return ctx.GetRoutingInfo().FirstOrDefault();
            //}
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {

                EntityConnection entityConn = (EntityConnection)ctx.Connection;
                SqlConnection sqlConn = (SqlConnection)entityConn.StoreConnection;
                SqlCommand cmd = new SqlCommand("TS_GetRoutingInfo", sqlConn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                using (cmd)
                {
                    da.Fill(ds);
                }

                if (ds != null && ds.Tables.Count > 0)
                    dt = ds.Tables[0];
            }
            return dt;
        }

        //internal DataTable GetCompletedDataEntryTasks(int invoiceId)
        internal string GetCompletedDataEntryTasks(int invoiceId)
        {
            //DataTable dt = new DataTable();
           
           
            //this.GetDataTable("SELECT CompletedBy FROM tblInvoiceActions WHERE ActionLevel=1 AND Complete=1 AND InvoiceID="
            //     + invoiceId.ToString() + " ORDER BY ActionID DESC");

            //return dt;

            string completedBy = "";

            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                completedBy = ctx.tblInvoiceActions.Where(x => x.InvoiceID == invoiceId && x.ActionLevel == 1 && x.Complete == true).OrderByDescending(x => x.ActionID).FirstOrDefault().CompletedBy;
            }
            return completedBy;
        }

        internal bool TestForExistingInvoice(int connectionId, Int32 invoiceId, string invoice, string vendor)
        {
            bool retVal = false;
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {

                ObjectParameter outRetMessage = new ObjectParameter("RetMessage", typeof(string));
                retVal = ctx.TestForExistingInvoice(connectionId, invoiceId, invoice, vendor).AsQueryable().First().Value;
            }
            return retVal;
        }
        #endregion

        #region Users-UserGroups Relates

        internal bool GetUserOrGroupIDFromApprovalGroup(int lApprvGrpID, int iActionLvl, ref string sUsrID, ref int lGrpID) // done
        {
            bool gotIt = false;
            string userIdColumn = "UserID" + iActionLvl.ToString().PadLeft(2, '0');
            string groupIdColumn = "GroupID" + iActionLvl.ToString().PadLeft(2, '0');
            string sql = "SELECT " + CommonFunctions.QCom(userIdColumn) + groupIdColumn + " FROM tblApprovalGroups WHERE GroupID=" + lApprvGrpID;
            DataTable dt = new DataTable();

            sUsrID = string.Empty;
            lGrpID = 0;

            if (iActionLvl > 0 && iActionLvl <= 10)
            {
                APIUtilDataAccessHelper.TimberScanData.FillDataTable(dt, sql, null, true, false);

                if (dt != null && dt.Rows.Count > 0)
                {

                    if (!string.IsNullOrWhiteSpace(dt.Rows[0][userIdColumn].ToString()))
                    {
                        sUsrID = dt.Rows[0][userIdColumn].ToString();
                    }
                    else if (int.TryParse(dt.Rows[0][groupIdColumn].ToString(), out lGrpID))
                    {
                        lGrpID = lGrpID;
                    }

                }
            }
            return !string.IsNullOrWhiteSpace(sUsrID) || lGrpID > 0;
        }

        internal string GetNameFromUsers(string sUserId)
        {

            string fName = string.Empty;
            string sQry = string.Empty;

            DataTable dt = new DataTable();

            try
            {

                sQry = "Select FirstName,LastName from tblUsers where UserId = '" + sUserId + "'";

                APIUtilDataAccessHelper.TimberScanData.FillDataTable(dt, sQry, null, true, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    fName = string.Format("{0} {1}", dt.Rows[0]["FirstName"], dt.Rows[0]["LastName"]);


                }


            }
            catch
            {
                throw;
            }
            return fName;

        }

        internal string GetUserId(int intUID) // done
        {
            string userid = string.Empty;
            string sQry = string.Empty;

            DataTable dt = new DataTable();

            try
            {

                sQry = "SELECT UserID From tblUsers WHERE UserIDNo=" + intUID.ToString();

                APIUtilDataAccessHelper.TimberScanData.FillDataTable(dt, sQry, null, true, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    userid = dt.Rows[0]["UserID"].ToString();


                }
            }
            catch
            {
                throw;
            }

            return userid;
        }

        internal string GetGroupDescription(int lUserGroupId)
        {
            string grpDesc = string.Empty;
            string sQry = string.Empty;

            DataTable dt = new DataTable();

            try
            {

                sQry = "SELECT GroupDescription From tblUserGroups WHERE UserGroupID=" + lUserGroupId.ToString();

                APIUtilDataAccessHelper.TimberScanData.FillDataTable(dt, sQry, null, true, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    grpDesc = dt.Rows[0]["GroupDescription"].ToString();


                }
            }
            catch
            {
                throw;
            }

            return grpDesc;

        }

        internal bool UserHasRole(string userID, string role)
        {
            DataTable dt = new DataTable();
            bool hasRole = false;

            dt = GetDataTable("Select " + role + " from tblUsers where userID = '" + userID + "'");

            if (dt != null && dt.Rows.Count > 0)
            {
                hasRole = (!string.IsNullOrEmpty(dt.Rows[0][role].ToString().Trim()) ?
                    Convert.ToBoolean(dt.Rows[0][role].ToString().Trim()) : false);

            }
                      
            return hasRole;
        }

        internal DataSet GetApprovalGroupByMemberTypeAndMemberValue(string routingType, string routingValue, int connectionId)
        {

            DataSet retVal = new DataSet();
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {

                EntityConnection entityConn = (EntityConnection)ctx.Connection;
                SqlConnection sqlConn = (SqlConnection)entityConn.StoreConnection;
                SqlCommand cmd = new SqlCommand("TS_GetApprovalGroupByMemberTypeAndMemberValue", sqlConn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                using (cmd)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter prm1 = new SqlParameter("@MemberType", routingType.Trim());
                    SqlParameter prm2 = new SqlParameter("@MemberValue", routingValue.Trim());
                    SqlParameter prm3 = new SqlParameter("@ConnectionId", connectionId);

                    cmd.Parameters.Add(prm1);
                    cmd.Parameters.Add(prm2);
                    cmd.Parameters.Add(prm3);
                    da.Fill(retVal);
                }
            }
            return retVal;
        }

        #endregion

        #region Vendor

        internal DataTable GetVendorInfoByVendorID(string Vendor, int ConnectionID)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {

                EntityConnection entityConn = (EntityConnection)ctx.Connection;
                SqlConnection sqlConn = (SqlConnection)entityConn.StoreConnection;
                SqlCommand cmd = new SqlCommand("TS_GetVendorInfo", sqlConn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                using (cmd)
                {
                    SqlParameter prm1 = new SqlParameter("@ConnectionID", ConnectionID);
                    cmd.Parameters.Add(prm1);
                    SqlParameter prm2 = new SqlParameter("@Vendor", Vendor);
                    cmd.Parameters.Add(prm2);

                    cmd.CommandType = CommandType.StoredProcedure;
                    da.Fill(ds);
                }
            }
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                dt = ds.Tables[0];
            return dt;
        }

        internal DataTable GetVendorInfoByDocumentID(int DocumentID)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {

                EntityConnection entityConn = (EntityConnection)ctx.Connection;
                SqlConnection sqlConn = (SqlConnection)entityConn.StoreConnection;
                SqlCommand cmd = new SqlCommand("API_GetVendorInfoDT", sqlConn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                using (cmd)
                {
                    SqlParameter prm1 = new SqlParameter("InvoiceId", DocumentID.ToString());
                    cmd.Parameters.Add(prm1);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.Fill(ds);
                }

                if (ds != null && ds.Tables.Count > 0)
                    dt = ds.Tables[0];
            }
            return dt;
        }

        #endregion

        #region Equipment

        internal bool GetEquipmentMemoCost(Int32 connectionId, string EquipmentCostCode)
        {
            bool memoCost = false;

            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                object obj = ctx.GetEquipmentMemoCost(connectionId, EquipmentCostCode).FirstOrDefault();
                if (obj != null)
                    bool.TryParse(obj.ToString(), out memoCost);
            }

            return memoCost;
        }

        #endregion

        #region Helpers

        internal int GetConnectionIDByDocumentID(int DocumentId)
        {
            DataSet dsInvoice = GetInvoice(DocumentId);
            int ConnectionID = 0;
            if (dsInvoice != null && dsInvoice.Tables.Count > 0)
            {
                DataTable dtInvoice = dsInvoice.Tables[0];
                if (dsInvoice.Tables[0].Rows.Count > 0)
                {
                    int.TryParse(dtInvoice.Rows[0]["ConnectionId"].ToString(), out ConnectionID);
                }
            }

            return ConnectionID;
        }

        internal DataTable GetSystemSettings()
        {
            string sql = "Select * from tblSystemSettings where ID=1";
            DataTable dt = GetDataTable(sql);
            return dt;
        }

        internal DateTime GetPeriodEndDate(int ConnectionID)
        {
            DateTime dAccrueDate = DateTime.Now;
            DataTable dtAccruralSettings = new DataTable();
            string sql = "SELECT LastAccrualDate FROM tblAccrualSettings";
            dtAccruralSettings = GetDataTable(sql);
            if (dtAccruralSettings == null || dtAccruralSettings.Rows.Count <= 0)
                dAccrueDate = DateTime.Parse("1/1/1900");
            else
                dAccrueDate = Convert.ToDateTime(dtAccruralSettings.Rows[0]["LastAccrualDate"] == DBNull.Value ? "1/1/1900" : dtAccruralSettings.Rows[0]["LastAccrualDate"]);

            return dAccrueDate;
        }

        internal List<API_GetCommitmentItems_Result> GetCommitmentItems(int connectionID, string commitment)
        {
            List<API_GetCommitmentItems_Result> CommitmentItemsList = new List<API_GetCommitmentItems_Result>();
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                CommitmentItemsList = ctx.GetCommitmentItems(connectionID, commitment).ToList();
            }

            return CommitmentItemsList;
        }

        #endregion

        #region DataAccess

        internal DataTable GetDataTable(string sql)
        {
            DataTable dt = new DataTable();

            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {

                EntityConnection entityConn = (EntityConnection)ctx.Connection;
                SqlConnection sqlConn = (SqlConnection)entityConn.StoreConnection;
                SqlCommand cmd = new SqlCommand("", sqlConn);
                cmd.CommandText = sql;
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                using (cmd)
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        internal DataTable GetDataTableTSync(string sql, int ConnectionID)
        {
            DataTable dt = new DataTable();
            string tSyncConnString = TimberSyncEntityConnString.Replace("TimberSync", "TimberSync_" + ConnectionID.ToString());
            using (var ctx = new CoreAPIEntities(tSyncConnString))
            {

                EntityConnection entityConn = (EntityConnection)ctx.Connection;
                SqlConnection sqlConn = (SqlConnection)entityConn.StoreConnection;
                SqlCommand cmd = new SqlCommand("", sqlConn);
                cmd.CommandText = sql;
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                using (cmd)
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        internal DateTime GetCurrentDateAndTimeOnSqlServerDate()
        {
            string dateFormat = string.Empty;
            DataTable dt = new DataTable();

            dt = this.GetDataTable("SELECT GETDATE()");
            DateTime CurDateTime = Convert.ToDateTime(dt.Rows[0][0]);

            dt.Dispose();

            return CurDateTime;
        }

        internal void RunTScanSQL(string sql)
        {
            APIUtilDataAccessHelper.TimberScanData.NonQuery(sql);
        }

        #endregion

        #region Error-Message Logging

        internal void LogError(string ScreenName, string FunctionName, string Exception, string InnserException, string ErrMessage, string UserID)
        {
            using (var ctx = new CoreAPIEntities(TimberScanConnString))
            {
                var tblErrorLog = new EntityModel.TSErrorLog();
                tblErrorLog.FunctionName = FunctionName;
                tblErrorLog.ScreenName = ScreenName;
                tblErrorLog.Exception = Exception;
                tblErrorLog.InnerException = InnserException;
                tblErrorLog.ErrMessage = ErrMessage;
                tblErrorLog.CreatedDate = DateTime.Now;
                tblErrorLog.CreatedUserID = UserID;
                ctx.TSErrorLogs.AddObject(tblErrorLog);
                ctx.SaveChanges();

            }

        }

        internal void LogAPIMessage(int DocumentID, string MessageType, string Message, string ScreenName, string Function, Int32 UserID)
        {
            if (APIMessageLogFlag)
            {
                using (var ctx = new CoreAPIEntities(TimberScanConnString))
                {
                    var apiMessageLog = new EntityModel.APIMessageLog();
                    apiMessageLog.DocumentID = DocumentID;
                    apiMessageLog.MessageType = MessageType;
                    apiMessageLog.Message = Message;
                    apiMessageLog.UserID = UserID;
                    apiMessageLog.CreatedDate = DateTime.Now;
                    apiMessageLog.Application = "API";
                    apiMessageLog.ScreenName = ScreenName;
                    apiMessageLog.Function = Function;
                    ctx.APIMessageLogs.AddObject(apiMessageLog);
                    ctx.SaveChanges();

                }
            }
        }

        #endregion

        #region Untilized

        //public List<GetInvoiceDistributions_Result> GetInvoiceDistributions(int InvoiceId)
        //{
        //    using (var ctx = new CoreAPIEntities(TimberScanConnString))
        //    {
        //        return ctx.GetInvoiceDistributions(InvoiceId).ToList();
        //    }
        //}

        //public GetRoutingInfo_Result GetRoutingInfo()


       

        #endregion
    }
}
