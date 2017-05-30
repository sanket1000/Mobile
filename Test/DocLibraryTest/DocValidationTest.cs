using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.API.DocumentSvc;
using Core.API.DocumentSvc.Entity;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace DocLibraryTest
{
    [TestClass]
    public class DocValidationTest
    {
        //[TestMethod]
        //public void TestInvHeaderAmount()
        //{
        //    string expected = string.Empty;
        //    string actual;
        //    DocumentEnt ent = GetDocument();
        //    ent.Header.Amount = 0;

        //    actual = ValidateDocument(ent);
        //    Assert.AreEqual(expected, actual);
        //}

        #region Invoice Basic Validation

        [TestMethod]
        public void Doc_ValidDocID_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();
            ent.Header.DocumentId = 2025;

            actual = ValidateDocumentForBasicValidations(ent);
            Assert.AreEqual<string>(expected, actual, actual);
            Console.WriteLine(actual);
        }

        [TestMethod]
        public void Doc_InValidDocID_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();
            ent.Header.DocumentId = 20555;

            actual = ValidateDocumentForBasicValidations(ent);
            Assert.AreNotEqual(expected, actual, actual);
            Console.WriteLine(actual);
        }

        //[TestMethod]
        //public void Doc_NotExists_Pass()
        //{
        //    string expected = string.Empty;
        //    string actual;

        //    DocumentEnt ent = GetDocument();
        //    ent.Header.DocumentId = 20555;

        //    actual = ValidateDocumentForBasicValidations(ent);
            
        //    Assert.AreNotEqual(expected, actual);
        //}

        [TestMethod]
        public void Doc_Deleted_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();
            RunTScanSql("update dbo.tblInvoices set Deleted = 1 where InvoiceID = " + ent.Header.DocumentId.ToString());

            actual = ValidateDocumentForBasicValidations(ent);
            RunTScanSql("update dbo.tblInvoices set Deleted = 0 where InvoiceID = " + ent.Header.DocumentId.ToString());
            Assert.AreNotEqual(expected, actual, actual);
            Console.WriteLine(actual);
        }
        
        [TestMethod]
        public void Doc_OnHold_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();
            RunTScanSql("update dbo.tblInvoices set OnHold = 1 where InvoiceID = " + ent.Header.DocumentId.ToString());

            actual = ValidateDocumentForBasicValidations(ent);
            RunTScanSql("update dbo.tblInvoices set OnHold = 0 where InvoiceID = " + ent.Header.DocumentId.ToString());
            Assert.AreNotEqual(expected, actual, actual);
            Console.WriteLine(actual);
        }

        [TestMethod]
        public void Doc_HoldInAP_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();
            RunTScanSql("update dbo.tblInvoices set HoldInAP = 1 where InvoiceID = " + ent.Header.DocumentId.ToString());

            actual = ValidateDocumentForBasicValidations(ent);
            RunTScanSql("update dbo.tblInvoices set HoldInAP = 0 where InvoiceID = " + ent.Header.DocumentId.ToString());
            Assert.AreNotEqual(expected, actual, actual);
            Console.WriteLine(actual);
        }
        
        [TestMethod]
        public void Doc_CurrentActionFinalReview_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();
            RunTScanSql("update dbo.tblInvoices set CurrentActionType = 'Final Review' where InvoiceID = " + ent.Header.DocumentId.ToString());

            actual = ValidateDocumentForBasicValidations(ent);
            RunTScanSql("update dbo.tblInvoices set CurrentActionType = 'Approve' where InvoiceID = " + ent.Header.DocumentId.ToString());
            Assert.AreNotEqual(expected, actual, actual);
            Console.WriteLine(actual);
        }

        #endregion 

        #region Invoice Header Validations

        [TestMethod]
        public void Doc_HeaderLineRequired_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();
            ent.Header = null;
            actual = ValidateDocumentForHeaderValidations(ent);
            Assert.AreNotEqual(expected, actual);
            Console.WriteLine(actual);
        }

        [TestMethod]
        public void Doc_HeaderValidUserIDRequired_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();
            ent.UserID = 6;
            actual = ValidateDocumentForHeaderValidations(ent);
            Assert.AreNotEqual(expected, actual);
            Console.WriteLine(actual);
        }

        [TestMethod]
        public void Doc_HeaderVendorRequired_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();
            ent.Header.Vendor.Id = null;
            ent.Header.HeaderStatus = "U";
            actual = ValidateDocumentForHeaderValidations(ent);
            Assert.AreNotEqual(expected, actual);
            Console.WriteLine(actual);
        }

        [TestMethod]
        public void Doc_HeaderVendorMustBeActive_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();
            ent.Header.Vendor.Id = "1336"; // Advanced Therapeutic Massage
            ent.Header.HeaderStatus = "U";
            actual = ValidateDocumentForHeaderValidations(ent);
            Assert.AreNotEqual(expected, actual);
            Console.WriteLine(actual);
        }

        [TestMethod]
        public void Doc_HeaderVendorMustNotBeCreditCardVendor_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();
            ent.Header.Vendor.Id = "3717";
            ent.Header.HeaderStatus = "U";
            actual = ValidateDocumentForHeaderValidations(ent);
            Assert.AreNotEqual(expected, actual);
            Console.WriteLine(actual);
        }

        [TestMethod]
        public void Doc_HeaderInvoiceNumberRequired_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();
            ent.Header.DocumentName = null;
            ent.Header.HeaderStatus = "U";
            actual = ValidateDocumentForHeaderValidations(ent);
            Assert.AreNotEqual(expected, actual);
            Console.WriteLine(actual);
        }

        [TestMethod]
        public void Doc_HeaderInvoiceDateRequired_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();
            ent.Header.InvoiceDate = DateTime.MinValue;
            ent.Header.HeaderStatus = "U";
            actual = ValidateDocumentForHeaderValidations(ent);
            Assert.AreNotEqual(expected, actual);
            Console.WriteLine(actual);
        }

        [TestMethod]
        public void Doc_HeaderPaymentDateRequired_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();
            ent.Header.PaymentDate = DateTime.MinValue;
            ent.Header.HeaderStatus = "U";
            actual = ValidateDocumentForHeaderValidations(ent);
            Assert.AreNotEqual(expected, actual);
            Console.WriteLine(actual);
        }

        [TestMethod]
        public void Doc_HeaderReceivedDateRequired_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();
            ent.Header.ReceivedDate = DateTime.MinValue;
            ent.Header.HeaderStatus = "U";
            actual = ValidateDocumentForHeaderValidations(ent);
            Assert.AreNotEqual(expected, actual);
            Console.WriteLine(actual);
        }

        [TestMethod]
        public void Doc_HeaderValidTaxAmount_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();
            ent.Header.TaxAmount = 10.0M;
            ent.Distributions.FirstOrDefault().Tax = 20.0M;
            ent.Header.HeaderStatus = "U";
            actual = ValidateDocumentForHeaderValidations(ent);
            Assert.AreNotEqual(expected, actual);
            Console.WriteLine(actual);
        }

        [TestMethod]
        public void Doc_HeaderExistingInvoice_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();
            ent.Header.DocumentName = "4608174407";
            ent.Header.HeaderStatus = "U";
            actual = ValidateDocumentForHeaderValidations(ent);
            Assert.AreNotEqual(expected, actual);
            Console.WriteLine(actual);
        }


        #endregion

        #region Invoice Dist Basic Validations

        [TestMethod]
        public void Doc_DistValidCommitment_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();
            ent.Distributions.FirstOrDefault().Commitment ="1234";
            actual = ValidateDocumentForDistValidations(ent);
            Assert.AreNotEqual(expected, actual);
            Console.WriteLine(actual);
        }

        [TestMethod]
        public void Doc_DistValidEquipment_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();
            ent.Distributions.FirstOrDefault().Equipment = "AC.XXX";
            actual = ValidateDocumentForDistValidations(ent);
            Assert.AreNotEqual(expected, actual);
            Console.WriteLine(actual);
        }

        [TestMethod]
        public void Doc_DistValidEquipmentCostCodeRequired_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();
            ent.Distributions.FirstOrDefault().Equipment = "AC.002";
            ent.Distributions.FirstOrDefault().Cost_Code_295 = "";
            actual = ValidateDocumentForDistValidations(ent);
            Assert.AreNotEqual(expected, actual);
            Console.WriteLine(actual);
        }

        [TestMethod]
        public void Doc_DistValidEquipmentCostCodeFormat_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();
            ent.Distributions.FirstOrDefault().Equipment = "AC.002";
            ent.Distributions.FirstOrDefault().Cost_Code_295 = "1234";
            actual = ValidateDocumentForDistValidations(ent);
            Assert.AreNotEqual(expected, actual);
            Console.WriteLine(actual);
        }

        [TestMethod]
        public void Doc_DistValidJobCorrectFormat_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();
            ent.Distributions.FirstOrDefault().Job = "1111";
            actual = ValidateDocumentForDistValidations(ent);
            Assert.AreNotEqual(expected, actual);
            Console.WriteLine(actual);
        }

        [TestMethod]
        public void Doc_DistValidJobNotSetup_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();
            ent.Distributions.FirstOrDefault().Job = "00.11.022";
            actual = ValidateDocumentForDistValidations(ent);
            Assert.AreNotEqual(expected, actual);
            Console.WriteLine(actual);
        }

        [TestMethod]
        public void Doc_DistValidCostCodeCorrectFormat_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();
            ent.Distributions.FirstOrDefault().CostCode = "ASDDF";
            actual = ValidateDocumentForDistValidations(ent);
            Assert.AreNotEqual(expected, actual);
            Console.WriteLine(actual);
        }

        [TestMethod]
        public void Doc_DistValidCostCodeNotSetup_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();
            ent.Distributions.FirstOrDefault().CostCode = "01.999";
            actual = ValidateDocumentForDistValidations(ent);
            Assert.AreNotEqual(expected, actual);
            Console.WriteLine(actual);
        }

        [TestMethod]
        public void Doc_DistValidCategoryNotSetup_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();
            ent.Distributions.FirstOrDefault().Job = "01.06.261";
            ent.Distributions.FirstOrDefault().CostCode = "01.004";
            ent.Distributions.FirstOrDefault().Category = "ASD";
            actual = ValidateDocumentForDistValidations(ent);
            Assert.AreNotEqual(expected, actual);
            Console.WriteLine(actual);
        }

        [TestMethod]
        public void Doc_DistValidUnitsMaxDecimal_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();
            
            ent.Distributions.FirstOrDefault().Units = 1.55555M;
            actual = ValidateDocumentForDistValidations(ent);
            Assert.AreNotEqual(expected, actual);
            Console.WriteLine(actual);
        }

        [TestMethod]
        public void Doc_DistRetainageOverAmount_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();
            ent.Distributions.FirstOrDefault().Amount = 23.33M;
            ent.Distributions.FirstOrDefault().Retainage = 5000M;
            actual = ValidateDocumentForDistValidations(ent);
            Assert.AreNotEqual(expected, actual);
            Console.WriteLine(actual);
        }

        [TestMethod]
        public void Doc_DistValidMiscDeductionPercent_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();

            ent.Distributions.FirstOrDefault().MiscDeduction2Percent = 110M;
            actual = ValidateDocumentForDistValidations(ent);
            Assert.AreNotEqual(expected, actual);
            Console.WriteLine(actual);
        }

        [TestMethod]
        public void Doc_DistValidTaxGroupLength_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();

            ent.Distributions.FirstOrDefault().TaxGroup = "abcd123";
            actual = ValidateDocumentForDistValidations(ent);
            Assert.AreNotEqual(expected, actual);
            Console.WriteLine(actual);
        }

        [TestMethod]
        public void Doc_DistValidDrawLength_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();

            ent.Distributions.FirstOrDefault().Draw = "abcdabcdabcdabcd";
            actual = ValidateDocumentForDistValidations(ent);
            Assert.AreNotEqual(expected, actual);
            Console.WriteLine(actual);
        }

        [TestMethod]
        public void Doc_DistValidAuthLength_Pass()
        {
            string expected = string.Empty;
            string actual;

            DocumentEnt ent = GetDocument();

            ent.Distributions.FirstOrDefault().Authorization = "abcdabcdabcdabcd";
            actual = ValidateDocumentForDistValidations(ent);
            Assert.AreNotEqual(expected, actual);
            Console.WriteLine(actual);
        }

        #endregion


        private DocumentEnt GetDocument()
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
            ent.Header.DocumentDesc = description;
            ent.Header.Amount = 27833.390625M;
            ent.Header.TaxAmount = 0M;
            ent.Header.PreTaxAmount = null;
            ent.Header.DiscountAmount = 0;
            ent.Header.MiscAmount = 0;
            ent.Header.PaymentDate = null;
            ent.Header.InvoiceDate = Convert.ToDateTime("2016-11-08 07:00:00.000");
            ent.Header.ReceivedDate = Convert.ToDateTime("2016-11-09 07:00:00.000");
            ent.Header.DiscountDate = (DateTime?)null;
            ent.Header.PaymentDate = Convert.ToDateTime("9999-12-31 23:59:59.997");
            ent.Header.AccountingDate = Convert.ToDateTime("2016-11-08 07:00:00.000");
            ent.Header.InvoiceCode1 = null;
            ent.Header.InvoiceCode2 = null;
            ent.Header.Date_Stamp = null;
            ent.Header.CurrentActionLevel = 0;
            ent.Header.HeaderStatus = "U";

            DocDistribution dist = new DocDistribution();
            dist.DistStatus = "U";
            dist.InvoiceID = 2025;
            dist.DistSeq = 1983;
            dist.Deleted = false;
            dist.Description = description;
            dist.Amount = 27833.390625M;
            dist.TaxGroup = "";
            dist.Tax = 0M;
            dist.TaxLiability = 0M;
            dist.DiscountOffered = 0M;
            dist.Retainage = 0M;
            dist.MiscDeduction = 0M;
            dist.MiscDeduction2Percent = 0;
            dist.Exempt_1099 = false;
            dist.Authorization = "";
            dist.JointPayee = "";
            dist.DistCode = "";
            dist.ExpenseAccount = "01-00-15350"; // "01-00-15350";
            dist.AccountsPayableAccount = "01-00-20100";
            dist.Job = "";
            dist.Extra = "";
            dist.CostCode = "";
            dist.Category = "";
            dist.Commitment = "";
            dist.Commitment_Line_Item = 0;
            dist.Draw = "";
            dist.Standard_Item = "";
            dist.Equipment = ""; //dist.Equipment = "EX.430";
            dist.Cost_Code_295 = "";  //dist.Cost_Code_295 = "AW.1700";
            dist.Misc_Entry_1 = "";
            dist.Misc_Entry_2 = "";
            dist.Misc_Entry_Units_1 = 0;
            dist.Misc_Entry_Units_2 = 0;

            ent.Distributions.Add(dist);

            return ent;
        }

        private string ValidateDocumentForBasicValidations(DocumentEnt doc)
        {
            doc.Distributions.ForEach(x => x.DistStatus = "");
            doc.Header.HeaderStatus = "";
            return new Document().ProcessDocument(doc);
        }

        private string ValidateDocumentForHeaderValidations(DocumentEnt doc)
        {
           
            doc.Distributions.ForEach(x => x.DistStatus = "");
            return new Document().ProcessDocument(doc);
        }

        private string ValidateDocumentForDistValidations(DocumentEnt doc)
        {
            doc.Header.HeaderStatus = "";
            doc.Distributions.ForEach(x => x.DistStatus = "U");
            return new Document().ProcessDocument(doc);
        }

        private void RunTScanSql(string sql)
        {
            new Document().RunTScanSql(sql);
        }
    }
}
