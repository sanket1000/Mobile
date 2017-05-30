using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.API.DocumentSvc;
using Core.API.DocumentSvc.Entity;
using Core.API.ConfigSvc;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
namespace Core.API.DocSvc.Test
{
    class Program
    {
        static int docid = 50;
        static int actionlvl = 3;
        
        static int userid = 1;
        static DateTime currentDate = DateTime.Now;

        static void Main(string[] args)
        {
            //GetDistConfig();
            //GetHeaderConfig();
            //
            //GetDocumentNotes();
           // GetDocument();
           //SaveNotes();
            //APHoldDocument(true);
            //APHoldDocument(false);
           // RejectDocument();
            //DeleteDocument();
            //GetAnnotation();
            ApproveDocument();
           //SaveDocument();
        }

        #region Document Validation Tests
        public static void ApproveDocument()
        {
            new Core.API.DocumentSvc.Document().ApproveDocument(102, 5);
        }
        public static void SaveDocument()
        {
            try
            {
                DocumentEnt ent = new DocumentEnt();

                string description = "InvAmountTest";
                ent.UserID = 1;
                ent.ConnectionID = 1;
                ent.DocumentID = 2025;
                ent.ApproveLevel = 0;
                ent.OnHold = false;
                ent.OnAPHold = false;
                ent.GroupID = 0;
                ent.Header.Vendor.Id = "8700";
                ent.Header.DocumentId = 2025;
                ent.Header.DocumentName = "4608575842";
                //ent.Header.DocumentDesc = description;
                ent.Header.Amount = 13.65M;
                //ent.Header.TaxAmount = 0M;
                //ent.Header.PreTaxAmount = null;
                //ent.Header.DiscountAmount = 0;
                //ent.Header.MiscAmount = 0;
                ent.Header.PaymentDate = Convert.ToDateTime("2016-11-08 07:00:00.000"); ;
                ent.Header.InvoiceDate = Convert.ToDateTime("2016-11-08 07:00:00.000");
                ent.Header.ReceivedDate = Convert.ToDateTime("2016-11-09 07:00:00.000");
                ent.Header.DiscountDate = (DateTime?)null;
                ent.Header.PaymentDate = Convert.ToDateTime("9999-12-31 23:59:59.997");
                ent.Header.AccountingDate = Convert.ToDateTime("2016-11-08 07:00:00.000");
                //ent.Header.InvoiceCode1 = null;
                //ent.Header.InvoiceCode2 = null;
                ent.Header.Date_Stamp = null;
                ent.Header.CurrentActionLevel = 0;
                ent.Header.HeaderStatus = "U";


                DocDistribution dist1 = new DocDistribution();
                dist1.DistStatus = "I";
                dist1.InvoiceID = 2025;
                dist1.DistSeq = 0;
                dist1.Deleted = false;
                //dist1.Description = description;
                dist1.Amount = 0.6M;
                //dist1.TaxGroup = "";
                dist1.PreTax = 0.0M;
                dist1.Tax = 0M;
                dist1.TaxLiability = 0M;
                dist1.DiscountOffered = 0M;
                dist1.Retainage = 0M;
                dist1.MiscDeduction = 0M;
                dist1.MiscDeduction2Percent = 0;
                dist1.Exempt_1099 = false;
                //dist1.Authorization = null;
                //dist1.JointPayee = null;
                //dist1.DistCode = null;
                //dist1.ExpenseAccount = "01-01-80400"; // "01-01-80400", "01-00-15350";
                //dist1.AccountsPayableAccount = "01-01-20100"; // "01-00-20100";
                dist1.Commitment_Line_Item = 0;
                dist1.Units = 0;
                dist1.Unit_Cost = 0.0M;
                dist1.Job = "00.06.261";
                dist1.Extra = null;
                dist1.CostCode = "01.002";
                dist1.Category = "L";
                //dist1.Category = "ASD";
                //dist1.Commitment = null;
                //dist1.Commitment_Line_Item = 0;
                //dist1.Draw = null;
                //dist1.Standard_Item = null;
                //dist1.Equipment = null; //"AC.001"; //dist.Equipment = "EX.430";
                //dist1.Cost_Code_295 = null; // "AW.1000";  //dist.Cost_Code_295 = "AW.1700";
                //dist1.Misc_Entry_1 = null;
                //dist1.Misc_Entry_2 = null;
                dist1.Misc_Entry_Units_1 = 0;
                dist1.Misc_Entry_Units_2 = 0;
                ent.Distributions.Add(dist1);


                DocDistribution dist = new DocDistribution();
                dist.DistStatus = "U";
                dist.InvoiceID = 2025;
                dist.DistSeq = 1983;
                dist.Deleted = false;
                //dist.Description = description;
                dist.Amount = 13.05M;
                dist.TaxGroup = "";
                dist.Tax = 0M;
                dist.TaxLiability = 0M;
                dist.DiscountOffered = 0M;
                dist.Retainage = 0M;
                dist.MiscDeduction = 0M;
                dist.MiscDeduction2Percent = 0;
                dist.Exempt_1099 = false;
                dist.Authorization = null;
                dist.JointPayee = null;
                dist.DistCode = null;
                //dist.ExpenseAccount = "01-01-80400"; // "01-01-80400", "01-00-15350";
                //dist.AccountsPayableAccount = "01-01-20100"; // "01-00-20100";
                dist.Job = "00.06.261";
                dist.Extra = null;
                dist.CostCode = "01.002";
                dist.Category = "L";
                //dist.Category = "ASD";
                dist.Commitment = null;
                dist.Commitment_Line_Item = 0;
                dist.Draw = null;
                dist.Standard_Item = null;
                dist.Equipment = null; //"AC.001"; //dist.Equipment = "EX.430";
                dist.Cost_Code_295 = null; // "AW.1000";  //dist.Cost_Code_295 = "AW.1700";
                dist.Misc_Entry_1 = null;
                dist.Misc_Entry_2 = null;
                dist.Misc_Entry_Units_1 = 0;
                dist.Misc_Entry_Units_2 = 0;



                ent.Distributions.Add(dist);
                new Core.API.DocumentSvc.Document().SaveDocument(ent);
                //string docxml = new Core.API.DocumentSvc.Document().GetDocument(21);

                //XmlSerializer serializer = new XmlSerializer(typeof(DocumentEnt), new XmlRootAttribute("Document"));

                //FileStream fs = new FileStream(@"C:\sanket\CoreAPIHH2\doc.xml", FileMode.Open);
                //XmlReader reader = XmlReader.Create(fs);
                //ent = (DocumentEnt)serializer.Deserialize(reader);

                //ent.Header.CurrentActionLevel = 1;
                //ent.Header.CurrentActionType = "Approve";
                //ent.UserID = 5;
                //ent.OnHold = true;
                //ent.GroupID = 1;

                // new Core.API.DocumentSvc.Document().SaveDocument(ent);
            }
            catch (Exception ex)
            {
                string msg = "";
                msg = ex.Message;
            }
        }

        #endregion



        public static void GetHeaderConfig()
        {
            string headerconfig = "";
            headerconfig = new Core.API.ConfigSvc.Config().headerconfiguration();
        }
        public static void GetDistConfig()
        {
            string distconfig = "";
            distconfig = new Core.API.ConfigSvc.Config().distributionconfiguration();
        }

        
     
        public static void GetAnnotation()
        {
            string retmsg = "";
          var ent = new Core.API.DocumentSvc.Document().GetDocumentAnnotation(70);
            //AnnotationEnt ent1 = ent;
            string s = retmsg;
        }

        public static string GetDocument()
        {
            string docxml = new Core.API.DocumentSvc.Document().GetDocument(docid);
            return docxml;

        }
        public static void GetDocumentNotes()
        {
            string notes = new Core.API.DocumentSvc.Document().GetDocumentNotes(docid);
            int i = 0;
        }

        public static void SaveNotes()
        {

            string noteText = "San Test Note - " + DateTime.Now.ToString();
            RetMessage retMsg = new Core.API.DocumentSvc.Document().SaveComment(docid, noteText, currentDate, userid);
            int i = 0;
        }

        public static void RejectDocument()
        {
            int rejectlvl = 1;
            string noteText = "San Reject Document Note - " + DateTime.Now.ToString();
            string retMsg = new Core.API.DocumentSvc.Document().RejectDocument(59, 2, 1, "san reject 1", currentDate, 5);
            int i = 0;
            int x = 1;
            x = i;
        }

        public static void DeleteDocument()
        {
            
            string noteText = "San Delete Document Note - " + DateTime.Now.ToString();
            string retMsg = new Core.API.DocumentSvc.Document().DeleteDocument(docid, actionlvl, noteText, userid);
            int i = 0;
            int x = 1;
            x = i;
        }
        public static void HoldDocument()
        {
            
            string noteText = "San Hold Document Note - " + DateTime.Now.ToString();
            string retMsg = new Core.API.DocumentSvc.Document().HoldDocument(docid, actionlvl, noteText, currentDate, userid);
            int i = 0;
            int x = 1;
            x = i;
        }
        public static void APHoldDocument(bool apHold)
        {

            string noteText = "San AP Hold Document Note - " + DateTime.Now.ToString();
            string retMsg = new Core.API.DocumentSvc.Document().APHoldDocument(docid, actionlvl, noteText, apHold, currentDate, userid);
            int i = 0;
            int x = 1;
            x = i;
        }
    }
}
