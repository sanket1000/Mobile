using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.API.Utilities.Code
{
    internal class TLSettings // originally modImage class
    {
            static bool firstStaticClassMessage = true;
            public static string AdditionalVendorInfoSqlSubSt = ", (Address_1 + ' ' +  Address_2 + ' ' +  City + ' ' +  State + ' ' +  ZIP) as Address ";
            public static string AdditionalVendorInfoSqlSubSt_A = ", (a.Address_1 + ' ' +  a.Address_2 + ' ' +  a.City + ' ' +  a.State + ' ' +  a.ZIP) as Address ";

            public static List<string[]> tsApprovalFields = new List<string[]>(){
                new string[] { "M", "Commitment", "Commitment"},
                new string[] { "O", "Vendor-Job", "Vendor", "Job"},                
                new string[] { "J", "Job", "Job"},
                new string[] { "U", "Job Authorization", "Authorization"},                                                
                new string[] { "V", "Vendor", "Vendor"},                
                new string[] { "A", "Category", "Category"},
                new string[] { "E", "Equipment", "Equipment"},
                new string[] { "R", "Job-Extra", "Job", "Extra"},
                new string[] { "T", "Job-Category", "Job", "Category"},
                new string[] { "S", "Job-Cost Code", "Job", "Cost_Code"},
                new string[] { "D", "Cost Code", "Cost_Code"},
                new string[] { "Y", "Job-Cost Code-Category", "Job", "Cost_Code", "Category"},
                new string[] { "G", "Full G/L Account", "GL_Prefix"}                
            };//new string[] { "C", "Invoice Type", "GL_Prefix" },new string[] { "B", "Base G/L Account", "GL_Prefix"}         

            public enum ProcessGroupTypes
            {
                PMProcessGroup = 0,
                JCProcessGroup = 1
            }

            public static ProcessGroupTypes ProcessGroupType;

            public static int ActiveDataEntryGroup = -1;    //save the last datantry group used in acquiiring

            public enum ReportOptions
            {
                InvoiceReportOnly = 0,
                InvoiceImagesOnly = 1,
                BothReportAndImages = 2
            }

            public static ReportOptions SelectedReportOption;

            public enum ReviewResults
            {
                tsAcceptButtonPressed = 0,
                tsRejectButtonPressed = 1,
                tsSkipButtonPressed = 2,
                tsExitButtonPressed = 3,
                tsRefreshButtonPressed = 4
            }

            public static ReviewResults ReviewResult;

            public enum ApproveResults
            {
                tsApprovePrintButton = 0,
                tsApproveApproveButton = 2,
                tsApproveReturnButton = 4,
                tsApproveSkipButton = 1,
                tsApproveCancelButton = 3,
                tsApproveDetailButton = 5,
                tsApproveCommentButton = 6,
                tsApprovalProcessing = 7,
                tsApproveRouteButton = 8
            }

            public static ApproveResults ApproveResult;

            public enum ReportDestinations
            {
                tsPrintToPrinter = 0,
                tsPrintToViewer = 1,
                tsPrintToPDF = 2
            }

            public static ReportDestinations ReportDestination;

            public enum InvoiceOrder
            {
                VendorNameOrder = 0,
                VendorIDOrder = 1,
                InvoiceDateOrder = 2,
                CompanyOrder = 3,
                JobOrder = 4
            }

            public static InvoiceOrder InvoiceDisplayOrder;

            public enum InvoiceSelections
            {
                AllInvoices = 0,
                NewInvoices = 1,
                RejectedInvoices = 2
            }

            public static InvoiceSelections InvoiceSelection;

            public enum InvoiceSelectionTypes
            {
                AllInvoiceTypes = 0,
                CompanyInvoices = 1,
                JobCostInvoices = 2,
                Code1Invoices = 3,
                VendorInvoices = 4,
                HeldInvoices = 5,
                ByInvoiceType = 6,
                DiscountDate = 7,
                PaymentDate = 8,
                RejectedInvoices = 9,
                InvoiceDate = 10, // "Export" only
                AccountingDate = 11 // "Export" only
            }

            public static InvoiceSelectionTypes InvoiceSelectionType;

            public enum CheckReviewTypes
            {
                CheckRuns = 0,
                CheckBatches = 1,
                SpecificChecks = 2,
                StartingCheckByAccount = 3,
                StartingCheckAllAccounts = 4
            }

            public static CheckReviewTypes CheckReviewType;

            public const string strFileType = "tif";

            public struct DateTimeRangeType
            {
                public DateTime StartDateTime;
                public DateTime EndDateTime;
            }

            public static DateTimeRangeType DateTimeRange;
            public static string HelpFilePath = string.Empty;

            public static string ImgLogsPath = "";

            public static string TempFilesPath = "";

            public struct GLMasterDescriptionsType
            {
                public string PrefixADesc;
                public string PrefixABDesc;
                public string PrefixABCDesc;
                public int PrefixALength;
                public int PrefixABLength;
                public int PrefixABCLength;
                public int BaseAcctLength;
                public int SuffixLength;
                public int Levels;
            }

            public static GLMasterDescriptionsType GLMasterDescriptions;

            public struct InvoiceEntryFormType
            {
                public string DataEntryFormMode; //"Data Entry", "Approve", "Review", "Timberline Interface"
                public int ActionLevel; //0 for Data Entry, Action ID for everything else
                public string DataFolderToUse; //Active or All
                public string ActionGroupIDString; //A string of action group ID's separated by | for usse if a user belongs to more than 1 user group at a particular action level
                public string ApproveType; //from main menu so we can get other approver info for criteria dropdown - keyAllApproveInvsCurFldr", "keyPriApproveInvsCurFldr", "keyAltApproveInvsCurFldr", etc
                public string ActionUserID; //Blank for "Data Entry" otherwise UserID Action is assigned to if individual user (not User Group)
                //mutually exclusive with ActionGroupIDString
                public bool HeldInvoice;
                public bool RejectedInvoice;
                public string SpecialCriteria;
                public string SpecialCriteriaValue;
                //2.1.36=========================
                public string SpecialCriteriaValue2; //for invoice number if you only want a specific invoice when selecting invoices by vendor only
                //===============================
                public string OtherApproverID; //this is for when an administrator is approving for another user - it can alse be <All_Users>
                public bool MarkFinalAproval; //if this is for <All_Users> should invoice be marked as final approval
                //2.3.19=========================
                public bool AltApproverForGroup; //this is for when a user an alternate member of an approval user group is approving invoices for a primary member of that group
                //===============================
            }

            public static InvoiceEntryFormType InvoiceEntryForm;

            // 5/13/2013
            public static string InvoiceEntryFormTempTable = "";
            //----------

            public struct ViewerInfoType
            {
                public string ViewType; //Accrue or Inquiry
                public string ImagePath;
                public int InvoiceID;
                public bool UseExtras;
                public DateTime AccrualDate;
            }

            public static ViewerInfoType ViewerInfo;

            public static bool bolInquiryComplete;

            //static clsTimberlineUpdate objTLUpate;

            //static clsFileLinksAndNotes objFileLinksAndNotes;

            //2.1.33=========================================
            //static clsTimberline objTimberline;
            //===============================================
      
            public static string globalAppPath;

           
            
            /// <summary>
            /// This should be called async to load some of the stuff that will be used later in the application. Must be called after timberline connection have been established.
            /// </summary>
            public void LoadImagingAssemblies()
            {



                try
                {

                   //modImage.WriteInitStaticClassMessage"LoadingImagingAssemblies", "TaxTables");
                    /*these classes are static and lazy loaded the first time they are needed. We hit them here so they are loaded*/
                    bool taxTables = TLSettings.ActualAPDistributionRoutines.TaxTables;
                   //modImage.WriteInitStaticClassMessage"LoadingImagingAssemblies", "TaxTables - DONE");
                    //string str = modImage.APDistributionSettingsStatic.Amount_Desc;
                    //str = modImage.APInvoiceSettings.Invoice_Code_1_Desc;

                }
                catch (Exception ex)
                {
                    //modIniFiles.LogFile("Error occurred while trying to preload some classes. " + ex.ToString());
                }

                try
                {
                    //REVISIT
                    //if (TimberScan.Properties.Basic.Default.JCSystem)
                    //{
                       //modImage.WriteInitStaticClassMessage"LoadingImagingAssemblies", "Set job format settings");
                        modTimberline.SetJobFormatSettings();
                       //modImage.WriteInitStaticClassMessage"LoadingImagingAssemblies", "set job format setting - DONE");
                    //}

                    if (TimberScan.Properties.Basic.Default.InvoiceCode1Select)
                    {
                       //modImage.WriteInitStaticClassMessage"LoadingImagingAssemblies", "invoice code 1 description");
                        //TimberScan.Properties.Extended.Default.InvoiceCode1Descr = DetermineInvoiceCode1Description();
                       //modImage.WriteInitStaticClassMessage"LoadingImagingAssemblies", "invoice code 1 description - DONE");
                    }


                   //modImage.WriteInitStaticClassMessage"LoadingImagingAssemblies", "init GLAccountFomrat");
                    GLAccountFormatObject = new clsGLAccountFormat();
                   //modImage.WriteInitStaticClassMessage"LoadingImagingAssemblies", "init GLAccountFomrat - DONE");

                   //modImage.WriteInitStaticClassMessage"LoadingImagingAssemblies", "init GLAccountFormat.DetermineAccountStructure");
                    GLAccountFormatObject.DetermineAccountStructure();
                   //modImage.WriteInitStaticClassMessage"LoadingImagingAssemblies", "init GLAccountFormat.DetermineAccountStructure - DONE");


                    if (DetermineGLMasterInfo())
                    {


                       //modImage.WriteInitStaticClassMessage"LoadingImagingAssemblies", "setting group prefix level");
                        if (GLMasterDescriptions.PrefixABCLength > 0)
                        {
                            TimberScan.Properties.Basic.Default.GroupPrefix = "PrefixC";
                        }
                        else if (GLMasterDescriptions.PrefixABLength > 0)
                        {
                            TimberScan.Properties.Basic.Default.GroupPrefix = "PrefixB";
                        }
                        else
                        {
                            TimberScan.Properties.Basic.Default.GroupPrefix = "PrefixA";
                        }
                       //modImage.WriteInitStaticClassMessage"LoadingImagingAssemblies", "setting group prefix level - DONE");

                       //modImage.WriteInitStaticClassMessage"LoadingImagingAssemblies", "init file links and notes");
                        objFileLinksAndNotes = new clsFileLinksAndNotes();
                       //modImage.WriteInitStaticClassMessage"LoadingImagingAssemblies", "init file links and notes");

                       //modImage.WriteInitStaticClassMessage"LoadingImagingAssemblies", "set JobCostTAbles");
                        TimberScan.Properties.Extended.Default.JobCostTables = modImage.ActualAPDistributionRoutines.JCSystemActive;
                       //modImage.WriteInitStaticClassMessage"LoadingImagingAssemblies", "set JobCostTAbles");

                       //modImage.WriteInitStaticClassMessage"LoadingImagingAssemblies", "set PMTables");
                        TimberScan.Properties.Extended.Default.PMTables = objFileLinksAndNotes.TestForPropertyManagementTables();
                       //modImage.WriteInitStaticClassMessage"LoadingImagingAssemblies", "set PMTables done");

                       //modImage.WriteInitStaticClassMessage"LoadingImagingAssemblies", "init clsTimberline");
                        objTimberline = new clsTimberline();
                       //modImage.WriteInitStaticClassMessage"LoadingImagingAssemblies", "init clsTimberline");

                       //modImage.WriteInitStaticClassMessage"LoadingImagingAssemblies", "allocation tables");
                        TimberScan.Properties.Extended.Default.AllocationTables = objTimberline.AllocationTables();
                       //modImage.WriteInitStaticClassMessage"LoadingImagingAssemblies", "allocation tables");

                       //modImage.WriteInitStaticClassMessage"LoadingImagingAssemblies", "apapproval system");
                        TimberScan.Properties.Extended.Default.APApprovalActive = objTimberline.APApprovalSystemActive();
                       //modImage.WriteInitStaticClassMessage"LoadingImagingAssemblies", "apapproval system");

                        lazyLoaded = true;

                    }
                    else
                    {
                        lazyLoaded = false;
                    }
                }
                catch (Exception ex)
                {
                    lazyLoaded = false;
                    //modIniFiles.LogFile("While initializing settings in the background. " + ex.ToString());
                }
            }

            enum VerifyVersionsResults
            {
                FailedToConnectToDatabase,
                Success
            }

                
         
           

        
         
           
           
            private bool GetApprovalFlagValue()
            {
                object obj = null;

                obj = TimberScanData.GetCell("SELECT SetApprovalFlagOnExport FROM tblRouting WHERE ID=1");

                if (obj != null && obj != DBNull.Value)
                {
                    return Convert.ToBoolean(obj);
                }
                else
                {
                    return false;
                }
            }

          

           
         
           
            //public static string GetDateMask()
            //{
            //    string sepChar = TimberScan.Properties.Extended.Default.DateFormat.Contains('-') ? "-" : "/";

            //    return TimberScan.Properties.Extended.Default.DateEntryFormatMDY.Insert(2, sepChar).Insert(5, sepChar);

            //}
           
            //private static void GetSQLSystemSettings() 
            //{
            //    string sLocalIniDir = null;
            //    string sMsg = null;

            //    DataTable dt = new DataTable();

            //    modImage.TimberScanData.FillDataTable(dt, "Select * from tblSystemSettings where ID=1", null, true, false);

            //    if (dt != null && dt.Rows.Count > 0)
            //    {
            //        //MasterSystemSettings20 with_1 = SystemSettings20;
            //        TimberScan.Properties.Basic.Default.BlankPageTolerance = 4000;
            //        TimberScan.Properties.Extended.Default.AllocateTaxes = false;
            //        TimberScan.Properties.Extended.Default.AlwaysExport = false;
            //        TimberScan.Properties.Extended.Default.ExportFormat = "";
            //        TimberScan.Properties.Extended.Default.DateFormat = !string.IsNullOrWhiteSpace(TimberScan.Properties.Extended.Default.DateFormat) ? TimberScan.Properties.Extended.Default.DateFormat : string.IsNullOrWhiteSpace(dt.Rows[0]["DateFormat"].ToString()) ? "MM-dd-yyyy" : dt.Rows[0]["DateFormat"].ToString();
            //        TimberScan.Properties.Extended.Default.RegionalDateFormat = !string.IsNullOrWhiteSpace(TimberScan.Properties.Extended.Default.RegionalDateFormat) ? TimberScan.Properties.Extended.Default.RegionalDateFormat : string.IsNullOrWhiteSpace(dt.Rows[0]["RegionalDateFormat"].ToString()) ? "MM/dd/yyyy" : dt.Rows[0]["RegionalDateFormat"].ToString();
            //        TimberScan.Properties.Extended.Default.DateEntryFormatMD = !string.IsNullOrWhiteSpace(TimberScan.Properties.Extended.Default.DateEntryFormatMD) ? TimberScan.Properties.Extended.Default.DateEntryFormatMD : string.IsNullOrWhiteSpace(dt.Rows[0]["DateEntryFormatMD"].ToString()) ? "mmdd" : dt.Rows[0]["DateEntryFormatMD"].ToString(); //"MMdd";
            //        TimberScan.Properties.Extended.Default.DateEntryFormatMDY = !string.IsNullOrWhiteSpace(TimberScan.Properties.Extended.Default.DateEntryFormatMDY) ? TimberScan.Properties.Extended.Default.DateEntryFormatMDY : string.IsNullOrWhiteSpace(dt.Rows[0]["DateEntryFormatMDY"].ToString()) ? "mmddyy" : dt.Rows[0]["DateEntryFormatMDY"].ToString(); //"MMddyy";
            //        TimberScan.Properties.Extended.Default.VendorJobRecordLimit = 100000;
            //        TimberScan.Properties.Basic.Default.BlankPageTolerance = System.Convert.ToInt32(CommonFunctions.TestNullNumber(dt.Rows[0]["BlankPageTolerance"])) == 0 ? 4000 :
            //            System.Convert.ToInt32(dt.Rows[0]["BlankPageTolerance"]);
            //        TimberScan.Properties.Extended.Default.AllocateTaxes = System.Convert.ToBoolean(dt.Rows[0]["AllocateTaxes"]);
            //        TimberScan.Properties.Extended.Default.AlwaysExport = Convert.ToBoolean(dt.Rows[0]["AlwaysExport"]);
            //        TimberScan.Properties.Extended.Default.ExportFormat = dt.Rows[0]["ExportFormat"].ToString().Trim();
            //        TimberScan.Properties.Extended.Default.RestrictGLJobAccess = false;
            //        TimberScan.Properties.Extended.Default.AllowPOAsAppGrpMember = Convert.ToBoolean(dt.Rows[0]["AllowPOAsAppGrpMember"]);
            //        TimberScan.Properties.Extended.Default.VendorJobRecordLimit = Convert.ToInt32(CommonFunctions.TestNullNumber(dt.Rows[0]["VendorJobLimit"])) == 0 ? 100000 :
            //            Convert.ToInt32(CommonFunctions.TestNullNumber(dt.Rows[0]["VendorJobLimit"]));
            //        int docTypeId = 0;
            //        int.TryParse(dt.Rows[0]["AimCheckDocTypeId"].ToString(), out docTypeId);

            //        TimberScan.Properties.Extended.Default.AimCheckDocTypeId = docTypeId;

            //        int.TryParse(dt.Rows[0]["AimInvoiceDocTypeId"].ToString(), out docTypeId);

            //        TimberScan.Properties.Extended.Default.AimInvoiceDocTypeId = docTypeId;


            //        bool bolVal = false;
            //        bool.TryParse(dt.Rows[0]["RestrictGLJobAccess"].ToString().Trim(),
            //            out bolVal);
            //        TimberScan.Properties.Extended.Default.RestrictGLJobAccess = bolVal;

            //        bolVal = false;
            //        TimberScan.Properties.Extended.Default.ApproveImportedInvoices = false;
            //        bool.TryParse(dt.Rows[0]["ApproveImportedInvoices"].ToString(),
            //            out bolVal);
            //        TimberScan.Properties.Extended.Default.ApproveImportedInvoices = bolVal;

            //        TimberScan.Properties.Extended.Default.ApproveRecurringInvoices = false;
            //        bolVal = false;
            //        bool.TryParse(dt.Rows[0]["ApproveRecurringInvoices"].ToString().Trim(), out bolVal);
            //        TimberScan.Properties.Extended.Default.ApproveRecurringInvoices = bolVal;

            //        TimberScan.Properties.Extended.Default.ApproveRMInvoices = false;
            //        TimberScan.Properties.Extended.Default.ApproveRMInvoices = Convert.ToBoolean(dt.Rows[0]["ApproveRMInvoices"]);
            //        TimberScan.Properties.Extended.Default.ApproveRegularInvoices = false;
            //        TimberScan.Properties.Extended.Default.ApproveRegularInvoices = Convert.ToBoolean(dt.Rows[0]["ApproveRegularInvoices"]);
            //        TimberScan.Properties.Extended.Default.UseTimberSync = true;
            //        TimberScan.Properties.Extended.Default.UseTimberSync = Convert.ToBoolean(dt.Rows[0]["UseTimberSync"]);
            //        TimberScan.Properties.Extended.Default.AllowNoJobOrGLEntry = Convert.ToBoolean(dt.Rows[0]["AllowNoJobOrGLEntry"]);
            //        TimberScan.Properties.Extended.Default.IgnoreThresholdOnReRoute = Convert.ToBoolean(dt.Rows[0]["IgnoreThresholdOnReRoute"]);
            //        //TimberScan.Properties.Extended.Default.UsePOInterface = Convert.ToBoolean(dt.Rows[0]["UsePOInterface"]);
            //        //TimberScan.Properties.Extended.Default.POInvoiceTolerance = Convert.ToDecimal(dt.Rows[0]["POInvoiceTolerance"]);

            //        if ((TimberScan.Properties.Extended.Default.AllocateTaxes || TimberScan.Properties.Extended.Default.AlwaysExport) && TimberScan.Properties.Extended.Default.ExportFormat == "")
            //        {
            //            TimberScan.Properties.Extended.Default.ExportFormat = "Timberline";
            //        }

            //        modImage.strPaperClipViewerPath = System.IO.Path.Combine(TimberScan.SystemSettings.AppPathAPI(), "Viewer.exe");

            //        if (dt.Rows[0]["TSLitePath"] != DBNull.Value)
            //        {
            //            Uri tmpUri;
            //            if (!string.IsNullOrEmpty(dt.Rows[0]["TSLitePath"].ToString()))
            //            {
            //                if (Uri.TryCreate(dt.Rows[0]["TSLitePath"].ToString(), UriKind.RelativeOrAbsolute, out tmpUri))
            //                {
            //                    modImage.strPaperClipViewerPath = dt.Rows[0]["TSLitePath"].ToString();
            //                }
            //            }
            //        }


            //        bolVal = false;
            //        bool.TryParse(dt.Rows[0]["AutoDisplayApprovals"].ToString().Trim(), out bolVal);
            //        TimberScan.Properties.Extended.Default.AutoDisplayApprovals = bolVal;

            //        bolVal = false;
            //        bool.TryParse(dt.Rows[0]["OmitTaxFromCommit"].ToString().Trim(), out bolVal);
            //        TimberScan.Properties.Extended.Default.OmitTaxFromCommit = bolVal;

            //        bolVal = false;
            //        TimberScan.Properties.Extended.Default.DocAssemblyOrder = dt.Rows[0]["DocAssemblyOrder"].ToString();

            //        TimberScan.Properties.Basic.Default.UseCommitmentItemDesc = Convert.ToBoolean(dt.Rows[0]["UseCommitItemDescr"]);

            //        TimberScan.Properties.Extended.Default.QPDFCoversionResolution = 300;
            //        try
            //        {
            //            int testConvPdfRes = Convert.ToInt32(CommonFunctions.TestNullNumber(dt.Rows[0]["QPDFConvRes"]));
            //            if (testConvPdfRes > 0)
            //                TimberScan.Properties.Extended.Default.QPDFCoversionResolution = testConvPdfRes;
            //        }
            //        catch { }

            //        TimberScan.Properties.Extended.Default.AllowExportBatchNaming = false;
            //        try
            //        {
            //            TimberScan.Properties.Extended.Default.AllowExportBatchNaming = Convert.ToBoolean(CommonFunctions.TestNullNumber(dt.Rows[0]["AllowExportBatchNaming"]));
            //        }
            //        catch { }

            //        App.CaptureResolution = Convert.ToInt32(dt.Rows[0]["CaptureResolution"]);
            //        App.FinalResolution = Convert.ToInt32(dt.Rows[0]["FinalResolution"]);


            //        return;


            //    }
            //    else
            //    {
            //        TimberScan.Controls.TMessageBox.Show("Critical error. SQL System settings not defined. Contact support.");
            //    }

            //}

            //public static bool SaveSystemSettingsXML(string sXMLSettingsFile, string sPassword)
            //{
            //    if (TimberScan.Properties.Extended.Default.bUseVB6Settings == false)
            //    {
            //        return false;
            //    }

            //    //origCTimberScanSettings settings = new origCTimberScanSettings();
            //    origCTimberScanSettings settings = Client.Singleton<origCTimberScanSettings>.GetInstance();

            //    #region comment out
            //    /*
            //    settings.AdminPassword = TimberScan.Properties.Basic.Default.AdminPassword;
            //    settings.AlwaysBatchExport = TimberScan.Properties.Basic.Default.AlwaysBatchExport;
            //    settings.APDatabase = TimberScan.Properties.Basic.Default.APDatabase;
            //    settings.APInvoicePassword = TimberScan.Properties.Basic.Default.ApInvoicePassword;
            //    settings.APInvoicesDriver = TimberScan.Properties.Basic.Default.ApInvoicesDriver;
            //    settings.APInvoicesServer = TimberScan.Properties.Basic.Default.ApInvoicesServer;
            //    settings.APInvoiceUserID = TimberScan.Properties.Basic.Default.ApInvoiceUserID;
            //    settings.ApprovalDate = TimberScan.Properties.Basic.Default.ApprovalDate;
            //    settings.ApprovalSignature = TimberScan.Properties.Basic.Default.ApprovalSignature;
            //    settings.ApprovalStamp = TimberScan.Properties.Basic.Default.ApprovalStamp;
            //    settings.AttachChecks = TimberScan.Properties.Basic.Default.AttachChecks;
            //    settings.AttachPaidChecks = TimberScan.Properties.Basic.Default.AttachPaidChecks;
            //    settings.AutoApprovalAccounts = TimberScan.Properties.Basic.Default.AutoApprovalAccounts;
            //    settings.AutoApprovalVendors = TimberScan.Properties.Basic.Default.AutoApprovalVendors;
            //    settings.AutoApproveAllInvoices = TimberScan.Properties.Basic.Default.AutoApproveAllInvoices;
            //    settings.AutoStampApproval = TimberScan.Properties.Basic.Default.AutoStampApproval;
            //    settings.AutoStampLocation = TimberScan.Properties.Basic.Default.AutoStampLocation;
            //    settings.BccEMailAddress1 = TimberScan.Properties.Basic.Default.BccEmailAddress1;
            //    settings.BccEMailAddress2 = TimberScan.Properties.Basic.Default.BccEmailAddress2;
            //    settings.BlankPageTolerance = TimberScan.Properties.Basic.Default.BlankPageTolerance;
            //    settings.BudgetVarianceCheck = TimberScan.Properties.Basic.Default.BudgetVarianceCheck;
            //    settings.CheckImageFolder = TimberScan.Properties.Basic.Default.CheckImageFolder;
            //    settings.CheckSubFolderType1 = TimberScan.Properties.Basic.Default.CheckSubFolderType1;
            //    settings.CheckSubFolderType2 = TimberScan.Properties.Basic.Default.CheckSubFolderType2;
            //    settings.DateReceivedUsage = TimberScan.Properties.Basic.Default.DateReceivedUsage;
            //    settings.ESMTPAuthMode = TimberScan.Properties.Basic.Default.ESMTPAuthMode;
            //    settings.GroupPrefix = TimberScan.Properties.Basic.Default.GroupPrefix;
            //    settings.ImageQuality = TimberScan.Properties.Basic.Default.ImageQuality;
            //    settings.ImportedInvoices = TimberScan.Properties.Basic.Default.ImportedInvoices;
            //    settings.InterfaceTransInvoices = TimberScan.Properties.Basic.Default.InterfaceTransInvoices;
            //    settings.InvoiceCode1Select = TimberScan.Properties.Basic.Default.InvoiceCode1Select;
            //    settings.InvoiceFolder = TimberScan.Properties.Basic.Default.InvoiceFolder;
            //    settings.InvoiceRouting = TimberScan.Properties.Basic.Default.InvoiceRouting;
            //    settings.InvoicesWithData = TimberScan.Properties.Basic.Default.InvoicesWithData;
            //    settings.JCSystem = TimberScan.Properties.Basic.Default.JCSystem;
            //    settings.LastOtherInvoiceProcessDate = TimberScan.Properties.Basic.Default.LastOtherInvoiceProcessDate;
            //    settings.Login = TimberScan.Properties.Basic.Default.Login;
            //    settings.MailPort = TimberScan.Properties.Basic.Default.MailPort;
            //    settings.MasterEMailAddress = TimberScan.Properties.Basic.Default.MasterEmailAddress;
            //    settings.MasterEMailFrom = TimberScan.Properties.Basic.Default.MasterEmailFrom;
            //    settings.MDBVersion = TimberScan.Properties.Basic.Default.MDBVersion;
            //    settings.MultiDataFolderEntry = TimberScan.Properties.Basic.Default.MultiDataFolderEntry;
            //    settings.NotifyUserOfBcc = TimberScan.Properties.Basic.Default.NotifyUserOfBcc;
            //    settings.OmitAcctName = TimberScan.Properties.Basic.Default.OmitAcctName;
            //    settings.Password = TimberScan.Properties.Basic.Default.Password;
            //    settings.PMSystem = TimberScan.Properties.Basic.Default.PMSystem;
            //    settings.RecurringAutoApprovalAccounts = TimberScan.Properties.Basic.Default.RecurringAutoApprovalAccounts;
            //    settings.RecurringAutoApprovalVendors = TimberScan.Properties.Basic.Default.RecurringAutoApprovalVendors;
            //    settings.RecurringInvoiceAttachments = TimberScan.Properties.Basic.Default.RecurringInvoiceAttachments;
            //    settings.RecurringInvoices = TimberScan.Properties.Basic.Default.RecurringInvoices;
            //    settings.RejectMethod = TimberScan.Properties.Basic.Default.RejectMethod;
            //    settings.ReviewEntryMandatory = TimberScan.Properties.Basic.Default.ReviewEntryMandatory;
            //    settings.ReviewMandatoryOnImport = TimberScan.Properties.Basic.Default.ReviewMandatory;
            //    settings.ServerRequiresAuthentication = TimberScan.Properties.Basic.Default.ServerRequiresAuthentication;
            //    settings.SetupComplete = TimberScan.Properties.Basic.Default.SetupComplete;
            //    settings.SignatureType = TimberScan.Properties.Basic.Default.SignatureType;
            //    settings.SMTPServer = TimberScan.Properties.Basic.Default.SMTPServer;
            //    settings.SourceDirectory = TimberScan.Properties.Basic.Default.SourceDirectory;
            //    settings.SQLAuthentication = TimberScan.Properties.Basic.Default.SQLAuthentication;
            //    settings.SQLConnectMethod = TimberScan.Properties.Basic.Default.SQLConnectMethod;
            //    settings.StoreCheckImages = TimberScan.Properties.Basic.Default.StoreCheckImages;
            //    settings.SubFolderType1 = TimberScan.Properties.Basic.Default.SubFolderType1;
            //    settings.SubFolderType1Desc = TimberScan.Properties.Basic.Default.SubFolderType1Desc;
            //    settings.SubFolderType2 = TimberScan.Properties.Basic.Default.SubFolderType2;
            //    settings.SubFolderType2Desc = TimberScan.Properties.Basic.Default.SubFolderType2Desc;
            //    settings.TLPassword = TimberScan.Properties.Basic.Default.TLPassword;
            //    settings.TLUserID = TimberScan.Properties.Basic.Default.TLUserID;
            //    settings.TotalActions = TimberScan.Properties.Basic.Default.TotalActions;
            //    settings.TScanMobileDatabase = TimberScan.Properties.Basic.Default.TScanMobileDatabase;
            //    settings.TScanMobileDriver = TimberScan.Properties.Basic.Default.TScanMobileDriver;
            //    settings.TScanMobilePassword = TimberScan.Properties.Basic.Default.TScanMobilePassword;
            //    settings.TScanMobileServer = TimberScan.Properties.Basic.Default.TScanMobileServer;
            //    settings.TScanMobileServer64 = TimberScan.Properties.Basic.Default.TScanMobileServer64;
            //    settings.TScanMobileUserID = TimberScan.Properties.Basic.Default.TScanMobileUserID;
            //    settings.TSyncDatabase = TimberScan.Properties.Basic.Default.TSyncDatabase;
            //    settings.TSyncDriver = TimberScan.Properties.Basic.Default.TSyncDriver;
            //    settings.TSyncPassword = TimberScan.Properties.Basic.Default.TSyncPassword;
            //    settings.TSyncServer = TimberScan.Properties.Basic.Default.TSyncServer;
            //    settings.TSyncUserID = TimberScan.Properties.Basic.Default.TSyncUserID;
            //    settings.UpdateTimberline = TimberScan.Properties.Basic.Default.UpdateTimberline;
            //    settings.UploadDirectory = TimberScan.Properties.Basic.Default.UploadDirectory;
            //    settings.UserMessage = TimberScan.Properties.Basic.Default.UserMessage;
            //     */
            //    #endregion

            //    return settings.SaveXML(sXMLSettingsFile, sPassword);
            //}

            

            private static string GetEnvironmentVariable(string sVar)
            {
                string returnValue = null;
                string sEnvString;
                string sMsg;
                int iIndx;
                iIndx = 1; // Initialize index to 1.

                do
                {
                    sEnvString = Interaction.Environ(iIndx); // Get environment variable.
                    if (Strings.InStr(sEnvString, sVar, 0) > 0) // Check entry.
                    {
                        returnValue = sEnvString.Substring(Strings.InStr(sEnvString, "=", 0) + 1 - 1);
                        return returnValue;
                    }
                    else
                    {
                        iIndx++; // Not PATH entry,
                    } // so increment.
                } while (!(sEnvString == ""));
                returnValue = "";
                return returnValue;
            }

            public static string GetNameFromUsers(string sUsrID)
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                modImage.TimberScanData.FillDataTable(dt, "Select FirstName, LastName from tblUsers where UserID = '" + sUsrID + "'", null, true, false);
                string passWordDecrypted = string.Empty;

                if (dt != null && dt.Rows.Count > 0)
                {
                    string firstName = string.Empty + dt.Rows[0]["FirstName"].ToString();
                    string lastName = string.Empty + dt.Rows[0]["Lastname"].ToString();
                    return firstName.Trim() + " " + lastName.Trim();
                }

                else
                    return string.Empty;
            }

            public static string GetGroupDescription(int lGrpID)
            {
                DalTsdbLts.tsdbDataContext dc = new DalTsdbLts.tsdbDataContext(TimberScan.Properties.Extended.Default.TimberScanConnectionString);

                var tbl = from a in dc.tblUserGroups where a.UserGroupID == lGrpID select a;

                foreach (var rec in tbl)
                {
                    return (rec.GroupDescription == null ? null : rec.GroupDescription.ToString());
                }

                return null;
            }

            public static int GetTimberScanPermissions()
            {
                // -1 - New User | 0 - Unauthorized user | 1 - Valid user
                short returnValue = 0;
                string strNme;

                DataTable tblUsers = new DataTable();
                //"Select Password from tblUsers where UserID = '" + modImage.strLogin.Replace("'", "''") + "'"

                modImage.TimberScanData.FillDataTable(tblUsers, "Select * from tblUsers where UPPER(UserID) = '" + strLogin.ToString().Replace("'", "''").ToUpper() + "'", null, true, false);


                if (tblUsers != null && tblUsers.Rows.Count > 0)
                {

                    if (tblUsers.Rows[0]["UserLoggedOn"] == DBNull.Value || Convert.ToBoolean(tblUsers.Rows[0]["UserLoggedOn"]))
                    {
                        returnValue = -2;
                        return returnValue;
                    }

                    //this case was moved to the bottom after the modImage.UserPermission is populated because we need to add some logic
                    //to this when the user is an aim user only. AIM user only users are allowed to log in even when the account is not enabled
                    //if ( (tblUsers.Rows[0]["AccountEnabled"] == DBNull.Value || !Convert.ToBoolean(tblUsers.Rows[0]["AccountEnabled"]))
                    //{
                    //    returnValue = 0;
                    //    return returnValue;
                    //}
                    if (string.IsNullOrEmpty(tblUsers.Rows[0]["FirstName"].ToString().Trim()) && string.IsNullOrEmpty(tblUsers.Rows[0]["LastName"].ToString().Trim()))
                    {
                        strNme = strLogin;
                    }
                    else
                    {
                        strNme = CommonFunctions.TestNullString(tblUsers.Rows[0]["FirstName"]).Trim() +
                            " " + CommonFunctions.TestNullString(tblUsers.Rows[0]["LastName"]).Trim();
                    }

                    UserPermission.UserID = tblUsers.Rows[0]["UserID"].ToString();
                    TimberScan.Global.CurrentWUserID = UserPermission.UserID;
                    //2.3.40===============================
                    UserPermission.UserIDNo = Convert.ToInt32(tblUsers.Rows[0]["UserIDNo"].ToString());
                    //======================================
                    bool.TryParse(tblUsers.Rows[0]["AccountEnabled"].ToString(), out UserPermission.AccountEnabled);
                    UserPermission.UserName = strNme;
                    UserPermission.email = tblUsers.Rows[0]["Email"].ToString();
                    UserPermission.NotificationMethod = tblUsers.Rows[0]["NotificationMethod"].ToString();
                    UserPermission.EmailSystem = Convert.ToBoolean(tblUsers.Rows[0]["EmailSystem"].ToString());
                    UserPermission.Administrator = Convert.ToBoolean(tblUsers.Rows[0]["Administrator"].ToString());
                    UserPermission.Supervisor = Convert.ToBoolean(tblUsers.Rows[0]["Supervisor"].ToString());
                    UserPermission.EnterVendors = Convert.ToBoolean(tblUsers.Rows[0]["EnterVendors"].ToString());
                    UserPermission.ApproveInvoices = Convert.ToBoolean(tblUsers.Rows[0]["ApproveInvoices"].ToString());
                    UserPermission.ApproveLowerInvoices = Convert.ToBoolean(tblUsers.Rows[0]["ApproveLowerInvoices"].ToString());
                    UserPermission.EnterInvoices = Convert.ToBoolean(tblUsers.Rows[0]["EnterInvoices"].ToString());
                    //On Error Resume Next 'this is a new field w/2.0.9, we get user permissions before we verify the database so we need this
                    UserPermission.ToDoList = Convert.ToBoolean(tblUsers.Rows[0]["ToDoList"].ToString());
                    //this is a new field w/2.0.19, we get user permissions before we verify the database so we need this
                    UserPermission.AddNewCodes = Convert.ToBoolean(tblUsers.Rows[0]["AddNewCodes"].ToString());
                    //these are new fields w/2.0.22, we get user permissions before we verify the database so we need this
                    UserPermission.ChangeType = Convert.ToBoolean(tblUsers.Rows[0]["ChangeType"].ToString());
                    UserPermission.Inquiries = Convert.ToBoolean(tblUsers.Rows[0]["Inquiries"].ToString());
                    UserPermission.ViewAll = Convert.ToBoolean(tblUsers.Rows[0]["ViewAll"].ToString());
                    UserPermission.ImportInvoices = Convert.ToBoolean(tblUsers.Rows[0]["ImportInvoices"].ToString());
                    UserPermission.AddImages = Convert.ToBoolean(tblUsers.Rows[0]["AddImages"].ToString());
                    UserPermission.RemoveImages = Convert.ToBoolean(tblUsers.Rows[0]["RemoveImages"].ToString());
                    UserPermission.ChangeDistributions = Convert.ToBoolean(tblUsers.Rows[0]["ChangeDistributions"].ToString());
                    UserPermission.RegularInquiries = Convert.ToBoolean(tblUsers.Rows[0]["RegularInquiries"].ToString());
                    UserPermission.JobCostInquiries = Convert.ToBoolean(tblUsers.Rows[0]["JobCostInquiries"].ToString());
                    UserPermission.VendorHistory = Convert.ToBoolean(tblUsers.Rows[0]["VendorHistory"].ToString());
                    UserPermission.Annotations = Convert.ToBoolean(tblUsers.Rows[0]["Annotations"].ToString());
                    //AIM
                    UserPermission.AIMAcquire = Convert.ToBoolean(tblUsers.Rows[0]["AIMAcquire"].ToString());
                    UserPermission.AIMEntry = Convert.ToBoolean(tblUsers.Rows[0]["AIMEntry"].ToString());
                    UserPermission.AIMSearch = Convert.ToBoolean(tblUsers.Rows[0]["AIMSearch"].ToString());
                    UserPermission.AIMEditDocument = Convert.ToBoolean(tblUsers.Rows[0]["AIMEditDocument"].ToString());
                    UserPermission.AIMViewAll = Convert.ToBoolean(tblUsers.Rows[0]["AIMViewAll"].ToString());
                    UserPermission.AIMApproveDocuments = Convert.ToBoolean(tblUsers.Rows[0]["ApproveDocuments"].ToString());
                    UserPermission.AIMRouteOnFly = Convert.ToBoolean(tblUsers.Rows[0]["AIMRouteOnFly"].ToString());
                    UserPermission.AIMOverrideDefaultRoute = Convert.ToBoolean(tblUsers.Rows[0]["AIMOverrideRoute"].ToString());
                    UserPermission.AIMAddTiffPages = Convert.ToBoolean(tblUsers.Rows[0]["AIMAddTiffPages"].ToString());
                    UserPermission.AIMDeleteDoc = Convert.ToBoolean(tblUsers.Rows[0]["AIMDeleteDoc"].ToString());
                    //On Error GoTo 0
                    UserPermission.PrintInvoices = Convert.ToBoolean(tblUsers.Rows[0]["PrintInvoices"].ToString());
                    UserPermission.RouteInvoicePreAprv = Convert.ToBoolean(tblUsers.Rows[0]["RouteInvoicePreAprv"].ToString());
                    UserPermission.RouteInvoicePostAprv = Convert.ToBoolean(tblUsers.Rows[0]["RouteInvoicePostAprv"].ToString());
                    UserPermission.ChangeInvoices = Convert.ToBoolean(tblUsers.Rows[0]["ChangeInvoices"].ToString());
                    UserPermission.JointChecks = Convert.ToBoolean(tblUsers.Rows[0]["JointChecks"].ToString());
                    UserPermission.Exempt1099 = Convert.ToBoolean(tblUsers.Rows[0]["Exempt1099"].ToString());
                    UserPermission.ReviewInvoices = Convert.ToBoolean(tblUsers.Rows[0]["ReviewInvoices"].ToString());
                    UserPermission.FinalReview = Convert.ToBoolean(tblUsers.Rows[0]["FinalReview"].ToString());
                    UserPermission.RecurringInvoices = Convert.ToBoolean(tblUsers.Rows[0]["RecurringInvoices"].ToString());
                    UserPermission.RescannedInvoices = Convert.ToBoolean(tblUsers.Rows[0]["RescannedInvoices"].ToString());
                    UserPermission.UploadInvoices = Convert.ToBoolean(tblUsers.Rows[0]["UploadInvoices"].ToString());
                    UserPermission.ReviewPaidInvoices = Convert.ToBoolean(tblUsers.Rows[0]["ReviewPaidInvoices"].ToString());
                    UserPermission.AttachChecks = Convert.ToBoolean(tblUsers.Rows[0]["AttachChecks"].ToString());
                    UserPermission.DeleteInvoices = Convert.ToBoolean(tblUsers.Rows[0]["DeleteInvoices"].ToString());
                    UserPermission.ProcessRejects = Convert.ToBoolean(tblUsers.Rows[0]["ProcessRejects"].ToString());
                    UserPermission.SignatureSize = System.Convert.ToInt32(tblUsers.Rows[0]["SignatureSize"] == DBNull.Value ? 0 : tblUsers.Rows[0]["SignatureSize"]);
                    UserPermission.AllowHoldInAP = Convert.ToBoolean(tblUsers.Rows[0]["AllowHoldInAP"].ToString());
                    UserPermission.VendorInquiries = Convert.ToBoolean(tblUsers.Rows[0]["VendorInquiries"].ToString());
                    UserPermission.ChangeInvoiceAmount = Convert.ToBoolean(tblUsers.Rows[0]["ChangeInvoiceAmount"].ToString());
                    UserPermission.DeviceId = tblUsers.Rows[0]["DeviceId"].ToString();
                    UserPermission.BlackberryActive = Convert.ToBoolean(tblUsers.Rows[0]["BlackBerryActive"].ToString());
                    UserPermission.JobIn = string.Empty;
                    UserPermission.PrefixIn = string.Empty;
                    UserPermission.TypeIn = string.Empty;
                    UserPermission.InvoiceOnHold = Convert.ToBoolean(tblUsers.Rows[0]["InvoiceOnHold"].ToString());
                    UserPermission.AttachSDInApproval = Convert.ToBoolean(tblUsers.Rows[0]["AttachSDInApproval"].ToString());
                    UserPermission.AttachSDInFinalReview = Convert.ToBoolean(tblUsers.Rows[0]["AttachSDInFinalReview"].ToString());
                    UserPermission.ImportPO = Convert.ToBoolean(tblUsers.Rows[0]["ImportPO"].ToString());
                    UserPermission.FirstName = tblUsers.Rows[0]["FirstName"].ToString();
                    UserPermission.LastName = tblUsers.Rows[0]["LastName"].ToString();
                    UserPermission.AttachCWInApproval = Convert.ToBoolean(tblUsers.Rows[0]["AttachCWInApproval"].ToString());
                    UserPermission.AttachCWInFinalReview = Convert.ToBoolean(tblUsers.Rows[0]["AttachCWInFinalReview"].ToString());

                    int.TryParse(tblUsers.Rows[0]["SebringUserId"].ToString(), out UserPermission.UserSebringID);
                    //this case was previously at the top of this method but it was moved here because we need to add some logic
                    //to this when the user is an aim user only. AIM user only users are allowed to log in even when the account is not enabled
                    if ((tblUsers.Rows[0]["AccountEnabled"] == DBNull.Value || !Convert.ToBoolean(tblUsers.Rows[0]["AccountEnabled"])) && (!modImage.IsAimOnly(UserPermission)))
                    {
                        returnValue = 0;
                        return returnValue;
                    }

                    returnValue = 1;

                }
                return returnValue;
            }

            public static bool DetermineGLMasterInfo()
            {
               //modImage.WriteInitStaticClassMessage"LoadingImagingAssemblies", "determine GLMASTERInfo");
                bool returnValue = false;

                clsTS_CTL objTS_CTL = new clsTS_CTL();

                if (objTS_CTL.NoSuchTableError)
                {
                   //modImage.WriteInitStaticClassMessage"LoadingImagingAssemblies", "determine GLMASTERInfo - DONE");
                    return false;

                }

                returnValue = true;
                GLMasterDescriptions.PrefixALength = objTS_CTL.GLPrefixALength;
                GLMasterDescriptions.PrefixABLength = objTS_CTL.GLPrefixABLength == 0 ? 0 : objTS_CTL.GLPrefixALength + objTS_CTL.GLPrefixABLength + 1;
                GLMasterDescriptions.PrefixABCLength = objTS_CTL.GLPrefixABCLength == 0 ? 0 : objTS_CTL.GLPrefixALength + objTS_CTL.GLPrefixABLength + objTS_CTL.GLPrefixABCLength + 2; ;
                GLMasterDescriptions.BaseAcctLength = objTS_CTL.GLBaseLength;
                GLMasterDescriptions.SuffixLength = objTS_CTL.GLSuffixLength;
                GLMasterDescriptions.Levels = System.Convert.ToInt16(GLMasterDescriptions.PrefixABCLength > 0 ? 3 : (GLMasterDescriptions.PrefixABLength > 0 ? 2 : 1));
                GLMasterDescriptions.PrefixADesc = objTS_CTL.GLPrefixADesc;
                GLMasterDescriptions.PrefixABDesc = objTS_CTL.GLPrefixABDesc;
                GLMasterDescriptions.PrefixABCDesc = objTS_CTL.GLPrefixABCDesc;
                if (!string.IsNullOrEmpty(modSystemProcedures.clsTimberlineConnection.PrefixADesc))
                {
                   //modImage.WriteInitStaticClassMessage"LoadingImagingAssemblies", "UpdateGLPrefixDescriptions");
                    UpdateGLPrefixDescriptions(GLMasterDescriptions.PrefixADesc, GLMasterDescriptions.PrefixABDesc, GLMasterDescriptions.PrefixABCDesc);
                   //modImage.WriteInitStaticClassMessage"LoadingImagingAssemblies", "UpdateGLPrefixDescriptions - DONE");
                }

               //modImage.WriteInitStaticClassMessage"LoadingImagingAssemblies", "determine GLMASTERInfo - DONE");
                return returnValue;
            }

            private static bool UpdateGLPrefixDescriptions(string sPrfxA, string sPrfxB, string sPrfxC)
            {
                string sQry = string.Empty;

                sQry = "Update tblConnections set PrefixADesc=" + CommonFunctions.QNApos(sPrfxA);

                if (!string.IsNullOrEmpty(sPrfxB))
                {
                    sQry += ",PrefixABDesc=" + CommonFunctions.QNApos(sPrfxB);
                }
                if (!string.IsNullOrEmpty(sPrfxC))
                {
                    sQry += ",PrefixABCDesc=" + CommonFunctions.QNApos(sPrfxC);
                }

                sQry += " WHERE ConnectionID=" + modSystemProcedures.clsTimberlineConnection.ConnectionID.ToString();

                modImage.TimberScanData.NonQuery(sQry);


                return true;
            }

            private static int GetSessionID()
            {
                Random random = new Random();
                return random.Next(1, 9999);
            }

            enum TestForValidCompanyOutComes
            {
                CompanyNotValid = 1,
                NoDefaultDataFolder = 2,
                ValidCompany = 3
            }

            TestForValidCompanyOutComes _validCompany = TestForValidCompanyOutComes.CompanyNotValid;

            private void TestForValidCompany()
            {
                string sQry = string.Empty, sNme = string.Empty, sMsg = string.Empty;
                DataTable dt = null;
                bool bValidComp = false;
                clsConnection defaultConnection = modSystemProcedures.clsTimberlineConnection;
                TimberScanDataAccess.DataAcessHelper tLineDicData = null;//new TimberScanDataAccess.DataAcessHelper( SystemConnections.TimberlineDictConnectionString, TimberScanDataAccess.DataAccessType.Odbc );

                defaultConnection = GetDefaultConnection();


                if (defaultConnection != null)
                {
                    sQry = "SELECT CNAME FROM COMPANY_INFORMATION";
                    bValidComp = false;

                    dt = new DataTable();

                    tLineDicData = new TimberScanDataAccess.DataAcessHelper(modTimberline.GetTimberlineConnectionStrings("Dictionary", TimberScan.Properties.Basic.Default.TLUserID, TimberScan.Properties.Basic.Default.TLPassword, defaultConnection.Path), TimberScanDataAccess.DataAccessType.Odbc);
                    tLineDicData.FillDataTable(dt, sQry, null, true, false);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        sNme = dt.Rows[0]["CNAME"].ToString().Trim();

                        if (sNme.ToUpper() == TimberScan.Properties.License.Default.Licensee.ToUpper())
                        {
                            bValidComp = true;
                        }
                    }

                    if (bValidComp)
                    {
                        this._validCompany = TestForValidCompanyOutComes.ValidCompany;
                        //sMsg = "An invalid license key was detected.\r\n\r\nThis program will now terminate.\r\n\r\nIf you think this is an error the contact your TimberScan dealer.";
                        //TimberScan.Controls.TMessageBox.Show(sMsg, "TimberScan", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }


                    strCompanyName = sNme;
                }
                else
                {
                    _validCompany = TestForValidCompanyOutComes.NoDefaultDataFolder;
                }
            }

            private clsConnection GetDefaultConnection()
            {
                clsConnection defaultConnection = null;

                TimberScanDataAccess.DataAcessHelper tScanData = new TimberScanDataAccess.DataAcessHelper(
                    TimberScan.Properties.Extended.Default.TimberScanConnectionString, TimberScanDataAccess.DataAccessType.SqlServer);

                DataTable dt = new DataTable();

                tScanData.FillDataTable(dt, "Select * from tblConnections where DefaultConnection = 1", null, true, false);

                if (dt != null && dt.Rows.Count > 0)
                {

                    defaultConnection = new clsConnection()
                    {
                        ConnectionID = Convert.ToInt32(dt.Rows[0]["ConnectionID"]),
                        Path = dt.Rows[0]["Path"].ToString()
                    };


                }

                return defaultConnection;
            }

            public static string DetermineInvoiceCode1Description()
            {

                string sQry = null;
                string sIC1Name = null;
                DataTable dtCust = null;
                DataTable dtStd = null;




                int iLoop = 0;

                sIC1Name = "";
                sQry = "SELECT * FROM APM_MASTER__INVOICE WHERE 1=0";
                dtStd = new DataTable();

                try
                {
                    modImage.TimberlineStandardData.FillDataTable(dtStd, sQry, null, true, false);

                    sQry = "SELECT * FROM MASTER_APM_INVOICE WHERE 1=0";
                    dtCust = new DataTable();

                    try
                    {
                        modImage.TimberlineCustomData.FillDataTable(dtCust, sQry, null, true, false);

                        for (iLoop = 0; iLoop < dtStd.Columns.Count; iLoop++)
                        {
                            if (dtStd.Columns[iLoop].ColumnName.ToLower() == "invoice_code_1")
                            {
                                sIC1Name = dtCust.Columns[iLoop].ColumnName;
                                break;
                            }
                        }

                    }
                    catch { }

                }
                catch { }

                if (string.IsNullOrEmpty(sIC1Name))
                {
                    sIC1Name = "Invoice_Code_1";
                }
                sIC1Name = sIC1Name.Replace("_", " ");
                return sIC1Name;
            }

       
           
            private static void CheckTempFilesDir()
            {
                TempFilesPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + "TempFiles";
                if (!Directory.Exists(TempFilesPath))
                {
                    System.IO.Directory.CreateDirectory(TempFilesPath);
                }

                try
                {
                    foreach (string file in System.IO.Directory.GetFiles(TempFilesPath))
                    {
                        System.IO.File.Delete(file);
                    }
                }
                catch { }
            }

            private static void CheckImagesLoggingDir()
            {
                //ImgLogsPath = System.Environment.CurrentDirectory + "\\" + "ImageLogging";
                ImgLogsPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + "ImageLogging";
                if (!Directory.Exists(ImgLogsPath))
                {
                    System.IO.Directory.CreateDirectory(ImgLogsPath);
                }
            }

            public static string GetImgLogFileName()
            {
                return ImgLogsPath + "\\" + "ImageLog_" + modImage.UserPermission.UserID + "_" + DateAndTime.Now.ToString("yyyyMMddhhmmss") + ".txt";
            }
        

            private static bool TestForDefaultApprovalGroup()
            {
                bool returnValue = true;

                DataTable dtDefaultApprovalGroup = new DataTable();
                modImage.TimberScanData.FillDataTable(dtDefaultApprovalGroup, "Select * from tblApprovalGroups where ProcessingGroupID= 'DEFAULT'", null, true, false);

                return dtDefaultApprovalGroup != null && dtDefaultApprovalGroup.Rows.Count > 0;
            }

           

            #region static settings classes

            private static bool _initializing = true;
            public static bool Initializing { get { return _initializing; } }

           

            #region vendor info management

            public static void SetVendorTypeLevel(string vendor, out string vendorType, out string vendorTypeLevel)
            {

                //CACHE THE VENDOR TYPES????? MAKE SURE TO CLEAR IF THE DATAFOLDER CHANGES
                vendorType = vendorTypeLevel = string.Empty;

                var vendorInfo = GetVendor(vendor);
                if (vendorInfo == null)
                    return;

                vendorTypeLevel = vendorInfo.VendorTypeLevel;

                vendorType = vendorInfo.VendorType;


                modImage.APDistributionSettingsStatic.VendorTypeLevel = vendorTypeLevel;

            }

            public class VendorInfo
            {
                public VendorInfo(string vendor, string name, string vendorType, string vendorTypeLevel)
                {
                    Vendor = vendor;
                    Name = name;
                    VendorType = vendorType;
                    VendorTypeLevel = vendorTypeLevel;
                }

                public string Vendor { get; private set; }
                public string Name { get; private set; }
                public string VendorType { get; private set; }
                public string VendorTypeLevel { get; private set; }
            }

            static object _vendorInfoLock = new object();
            static volatile Dictionary<string, VendorInfo> _vendors = null;

            /// <summary>
            /// Keeps a list of all vendors requested by calling GetVendor(vendor). 
            /// If the vendor is not already in Vendors then GetVendor looks up the vendor. If the vendor exists it is added to the Vendors
            /// </summary>
            private static Dictionary<string, VendorInfo> Vendors
            {
                get
                {

                    if (_vendors == null)
                    {
                        lock (_vendorInfoLock)
                        {
                            if (_vendors == null)
                            {
                                _vendors = new Dictionary<string, VendorInfo>();
                            }
                        }
                    }

                    return _vendors;
                }
            }

            public static VendorInfo GetVendor(string vendor)
            {
                if (string.IsNullOrWhiteSpace(vendor))
                    return null;

                vendor = vendor.Trim();

                if (Vendors.ContainsKey(vendor))
                    return Vendors[vendor];

                DataTable dtVendor = GetVendorDataTable(vendor);

                if (dtVendor == null || dtVendor.Rows.Count <= 0)
                    return null;

                VendorInfo vendorInfo = GetVendorInfoFromDataTable(dtVendor);

                Vendors.Add(vendor, vendorInfo);

                return vendorInfo;
            }

            private static VendorInfo GetVendorInfoFromDataTable(DataTable dtVendor)
            {
                if (dtVendor == null || dtVendor.Rows.Count <= 0)
                    return null;

                VendorInfo vendor = new VendorInfo(dtVendor.Rows[0]["Vendor"].ToString(),
                    dtVendor.Rows[0]["Name"].ToString(),
                    dtVendor.Rows[0]["Type"].ToString(),
                    frmInvoiceEntryDocks.GetConvertedVendorType(dtVendor.Rows[0]["Type"].ToString(), dtVendor.Rows[0]["Vendor"].ToString()));

                return vendor;
            }

            private static DataTable GetVendorDataTable(string vendor)
            {
                if (string.IsNullOrWhiteSpace(vendor))
                    return null;


                TimberScanDataAccess.DataAcessHelper dataAccess = null;
                short iLoop;
                string strVendorTypeDesc = string.Empty, strVendorType = string.Empty, sQry = string.Empty;
                DataTable adoApVendorRs = new DataTable();

                sQry = "SELECT * FROM APM_MASTER__VENDOR WHERE Vendor=" + CommonFunctions.QNApos(vendor);

                if (TimberScan.Properties.Extended.Default.TSyncActive)
                {

                    dataAccess = new TimberScanDataAccess.DataAcessHelper(TimberScan.Properties.Extended.Default.TSyncConnectionString, TimberScanDataAccess.DataAccessType.SqlServer);

                    sQry = sQry + " AND ConnectionID=" + modSystemProcedures.clsTimberlineConnection.ConnectionID;

                    adoApVendorRs = new DataTable();
                    dataAccess.FillDataTable(adoApVendorRs, sQry, null, true, false);

                }
                else
                {
                    dataAccess = modImage.TimberlineStandardData;
                    adoApVendorRs = new DataTable();
                    dataAccess.FillDataTable(adoApVendorRs, sQry, null, true, false);
                }

                if (adoApVendorRs == null || adoApVendorRs.Rows.Count <= 0)
                {

                    sQry = "SELECT * FROM APM_MASTER__VENDOR WHERE Ucase(Vendor)=" + CommonFunctions.QNApos(vendor).ToString().ToUpper();
                    dataAccess = modImage.TimberlineStandardData; //new TimberScanDataAccess.DataAcessHelper(modImage.SystemConnections.TimberlineStdConnectionString, TimberScanDataAccess.DataAccessType.Odbc);

                    adoApVendorRs = new DataTable();
                    dataAccess.FillDataTable(adoApVendorRs, sQry, null, true, false);
                }

                return adoApVendorRs;
            }

            #endregion


            //since all the singleton classes are interdependent we will have a single sync object for all. this is so
            //all the singletons below are instantiated one time by a single thread.
            private static object syncRoot = new object();

            //this static instance is instantiated one time throughout the program and used by clsAPDistributionRoutines
            private static volatile actualAPDistriubtionRoutines _actualAPDistributionRoutines = null;
            public static actualAPDistriubtionRoutines ActualAPDistributionRoutines
            {
                get
                {
                    if (_actualAPDistributionRoutines == null)
                    {
                        lock (syncRoot)
                        {
                            if (_actualAPDistributionRoutines == null)
                            {
                                _actualAPDistributionRoutines = new actualAPDistriubtionRoutines();
                            }
                        }
                    }
                    return _actualAPDistributionRoutines;

                }
            }

            static volatile actualAPDistributionSettings _apDistributionSettings;
            public static actualAPDistributionSettings APDistributionSettingsStatic
            {
                get
                {
                    if (_apDistributionSettings == null)
                    {
                        lock (syncRoot)
                        {
                            if (_apDistributionSettings == null)
                            {
                                _apDistributionSettings = new actualAPDistributionSettings();
                            }
                        }
                    }

                    return _apDistributionSettings;
                }
            }

            static volatile actualAPInvoiceSettings _actualAPInvoiceSettings;
            public static actualAPInvoiceSettings APInvoiceSettings
            {
                get
                {
                    if (_actualAPInvoiceSettings == null)
                    {
                        lock (syncRoot)
                        {
                            if (_actualAPInvoiceSettings == null)
                            {
                                _actualAPInvoiceSettings = new actualAPInvoiceSettings();
                            }
                        }
                    }
                    return _actualAPInvoiceSettings;
                }
            }

            static volatile actualTS_CTL _actualTS_CTL;
            public static actualTS_CTL TS_CTL
            {
                get
                {
                    if (_actualTS_CTL == null)
                    {
                        lock (syncRoot)
                        {
                            if (_actualTS_CTL == null)
                            {
                                _actualTS_CTL = new actualTS_CTL();
                            }
                        }
                    }
                    return _actualTS_CTL;
                }
            }

           
            #endregion
        
    }
}
