using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Core.API.Utilities;
using Core.API.Utilities.Helpers;
using Core.API.DocumentSvc.Entity;
using Core.API.EntityModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using CA.Imaging.Annotations;
using CA.Imaging.Annotations.Serialization;
using CA.Imaging.Models;
using System.Drawing;
using TimberScanServices;
using Core.API.TLSettings;
using Core.API.TLSettings.Models;

using Microsoft.VisualBasic;

namespace Core.API.DocumentSvc
{
    internal class DocumentBO
    {
        //static int ConnectionID = 1;
        #region Members

        int ConnectionID = 0;
        DocumentDA _docDA = null;
        TLSettings.TimberlineSettings tlSettings = null;
        ActualTSCTL __actualTSCTL = null;
        APInvoiceSettings __apInvoiceSettings = null;
        APDistributionSettings __apDistSettings = null;
        Dictionary<string, clsTaxGroup> colTaxItems;
        SystemSettings __systemSettings = null;
        bool noJobOrGLEntry = false;
        DataTable adoApVendorRs = null;
        DataTable dtUserPermissions = null;
        DataTable dtUsers = null;
        int UserID = 0;
        string LoginID = "";
        string UserJobIn = "";
        string UserPrefixIn = "";

        #endregion

        internal DocumentBO()
        {
            _docDA = new DocumentDA();
            tlSettings = new TimberlineSettings();
        }

        #region Properties

        private ActualTSCTL _actualTSCTL
        {
            get { return __actualTSCTL; }
        }

        private APInvoiceSettings apInvSettings
        {
            get { return __apInvoiceSettings; }
        }

        private APDistributionSettings apDistSettings
        {
            get { return __apDistSettings; }
        }

        private SystemSettings TSSysSettings
        {
            get
            {
                if (__systemSettings == null)
                    __systemSettings = new SystemSettings();

                return __systemSettings;
            }
        }

        private string VendorName
        {
            get
            {
                if (adoApVendorRs != null && adoApVendorRs.Rows.Count > 0)
                    return adoApVendorRs.Rows[0]["Name"].ToString();
                else
                    return string.Empty;
            }
        }
        #endregion

        #region API Methods

        public string GetDocument(int DocumentID)
        {
            string retMsg = "";
            try
            {
                TimberlineSettings ts = new TimberlineSettings();
                retMsg = _docDA.GetDocument(DocumentID);
                // append file section info
                if (!string.IsNullOrEmpty(retMsg))
                {
                    string xmlFileSection = new FileSectionFormatter(DocumentID).GetFileSection();
                    if (!string.IsNullOrEmpty(xmlFileSection))
                    {
                        retMsg = retMsg.Replace("<File></File>", xmlFileSection);
                    }
                }
            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                _docDA.LogError("CoreAPIDocumentSvc", "GetDocument", ex.Message, innerExMsg, "", "");

                retMsg = ex.Message + "  --- " + innerExMsg;
                retMsg = new RetMessageFormatter("EXCEPTION", retMsg).GetReturnMessage();
            }
            return retMsg;
        }

        public string GetDocumentList(int? ConnectionID)
        {
            string retMsg = "";
            try
            {
                retMsg = _docDA.GetDocumentList(ConnectionID);
            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                _docDA.LogError("CoreAPIDocumentSvc", "GetDocumentList", ex.Message, innerExMsg, "", "");

                retMsg = ex.Message + "  --- " + innerExMsg;
                retMsg = new RetMessageFormatter("EXCEPTION", retMsg).GetReturnMessage();
            }
            return retMsg;
        }

        public string GetDocumentNotes(int DocumentID)
        {
            string retMsg = "";
            try
            {
                retMsg = _docDA.GetDocumentNotes(DocumentID);
            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                _docDA.LogError("CoreAPIDocumentSvc", "GetDocumentNotes", ex.Message, innerExMsg, "", "");

                retMsg = ex.Message + "  --- " + innerExMsg;
                retMsg = new RetMessageFormatter("EXCEPTION", retMsg).GetReturnMessage();
            }
            return retMsg;
        }

        public RetMessage SaveComment(int DocumentID, string NoteText, DateTime NoteDate, int UserId)
        {
            RetMessage retMsg = new RetMessage();
            string title = "SUCCESS";
            try
            {
                retMsg = _docDA.SaveComment(DocumentID, NoteText, NoteDate, UserId);
                if (retMsg != null)
                {
                    if (retMsg.Message.Contains("SUCCESS"))
                        retMsg.Message = new RetMessageFormatter("SUCCESS", "Comment saved successfully").GetReturnMessage();
                    else
                        retMsg.Message = new RetMessageFormatter("ERROR", retMsg.Message).GetReturnMessage();
                }
            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                _docDA.LogError("CoreAPIDocumentSvc", "SaveComment", ex.Message, innerExMsg, "", "");
                title = "EXCEPTION";
                if (retMsg == null)
                    retMsg = new RetMessage();
                retMsg.Message = ex.Message + "  --- " + innerExMsg;
                retMsg.Message = new RetMessageFormatter(title, retMsg.Message).GetReturnMessage();
            }
            return retMsg;
        }

        public string RejectDocument(int DocumentID, int ActionLevel, int RejectLevel, string RejectReason, DateTime RejectDate, int UserId)
        {
            string retMsg = "";
            string title = "SUCCESS";
            string sQry = string.Empty, sActionType = string.Empty,
               sUserId = string.Empty, sActionComment = string.Empty, sCurUser = string.Empty;
            int lUserGroupId = 0, lActionId = 0, lLoop = 0;
            bool bIDFound, bRejectOk = false, bActionsClosed;
            string rejectUserId = "";
            try
            {
                retMsg = this.DocumentExists(DocumentID);

                if (!retMsg.Contains("SUCCESS"))
                    return new RetMessageFormatter("ERROR", retMsg).GetReturnMessage();


                rejectUserId = _docDA.GetUserId(UserId);
                Int32 connId = _docDA.GetConnectionIDByDocumentID(DocumentID);
                DataTable dt = null;
                bActionsClosed = false;
                if (RejectLevel > 1)
                {

                    sQry = string.Format("select ProcessGroupId,UserID,GroupID,ActionType,ActionLevel,Comment from tblInvoiceActions where ConnectionId={0} AND InvoiceID={1} AND ActionLevel<={2} ORDER BY ActionId desc", connId, DocumentID, RejectLevel);

                }
                else
                {

                    sQry = string.Format("select ProcessGroupId,UserID,GroupID,ActionType,ActionLevel,Comment from tblInvoiceActions where ConnectionId={0} AND InvoiceID={1} AND ActionLevel={2} ORDER BY ActionId desc", connId, DocumentID, RejectLevel);

                }

                dt = new DataTable();

                APIUtilDataAccessHelper.TimberScanData.FillDataTable(dt, sQry, null, true, false);
                bRejectOk = false;
                sActionType = dt.Rows[0]["ActionType"].ToString();

                if (sActionType.ToUpper() == "MATCH" || sActionType.ToUpper() == "IMPORT")
                    sActionType = "Data Entry";


                foreach (DataRow drAction in dt.Rows)
                {

                    if (Convert.ToInt32(drAction["ActionLevel"]) != RejectLevel)
                        break;

                    if (RejectLevel > 1)
                    {
                        sActionComment = drAction["Comment"].ToString();
                    }
                    else
                    {
                        sActionComment = string.Empty;
                    }

                    if (RejectLevel == 1 ||
                        (RejectLevel > 1 && (string.IsNullOrEmpty(sActionComment) || sActionComment.Contains("Invoice marked as approved") || sActionComment.Contains("Invoice status set as completely approved"))))
                    {

                        if (sActionType == "Data Entry")
                        {
                            bIDFound = true;
                            lUserGroupId = Convert.ToInt32(CommonFunctions.TestNullNumber(drAction["GroupId"]));
                        }
                        else
                        {
                            bIDFound = _docDA.GetUserOrGroupIDFromApprovalGroup(Convert.ToInt32(drAction["ProcessGroupID"]),
                                RejectLevel, ref sUserId, ref lUserGroupId);
                        }

                        if (bIDFound)
                        {
                            if (!bActionsClosed)
                            {
                                _docDA.CloseAllActions(DocumentID, sUserId, "Invoice Rejected", 0, null);
                                bActionsClosed = true;
                            }
                            if (!string.IsNullOrEmpty(sUserId))
                            {
                                _docDA.AddAPInvoiceAction(DocumentID, sUserId, drAction["ProcessGroupID"], 0, sActionType, (short)RejectLevel, rejectUserId, true, -1);
                                retMsg = "Invoice rejected to " + _docDA.GetNameFromUsers(sUserId) + " at " + sActionType + " level " + RejectLevel.ToString();
                                bRejectOk = true;
                            }
                            else if (lUserGroupId > 0)
                            {

                                _docDA.AddAPInvoiceAction(DocumentID, "", drAction["ProcessGroupID"], lUserGroupId, sActionType, (short)RejectLevel, rejectUserId, true, -1);
                                retMsg = "Invoice rejected to " + _docDA.GetGroupDescription(lUserGroupId) + " at " + sActionType + " level " + RejectLevel;
                                bRejectOk = true;

                            }

                            if (!string.IsNullOrEmpty(sUserId) || lUserGroupId > 0)
                            {
                                _docDA.AddAPInvoiceNoteDB(DocumentID, retMsg, "Log", "Approve", (short)ActionLevel, "System", DateTime.Now, "Normal", -1);

                            }

                            if (sActionType == "Data Entry")
                                break;

                        }
                    }
                }

                if (bRejectOk)
                {
                    _docDA.SetNextInvoiceAction(DocumentID, sActionType, (short)RejectLevel, -1);
                    _docDA.AddAPInvoiceNoteDB(DocumentID, RejectReason, "Note", "Reject", (short)ActionLevel, rejectUserId, DateTime.Now, "Critical", -1);

                }
                else
                {
                    title = "ERROR";
                    retMsg = "Unable to reject to a user or group at this level.\r\n\r\n All users and groups associated with this invoice\r\nat level " + RejectLevel.ToString() + " have been removed.";

                }
                if (string.IsNullOrEmpty(retMsg))
                    retMsg = "Document rejected successfully";
                retMsg = new RetMessageFormatter(title, retMsg).GetReturnMessage();

            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                _docDA.LogError("CoreAPIDocumentSvc", "RejectDocument", ex.Message, innerExMsg, "", "");
                title = "EXCEPTION";
                retMsg = ex.Message + "  --- " + innerExMsg;
                retMsg = new RetMessageFormatter(title, retMsg).GetReturnMessage();

            }

            return retMsg;
        }

        public string DeleteDocument(int DocumentID, int ApproveLevel, string DeleteReason, int UserId)
        {
            string retMsg = "";
            string title = "";
            try
            {
                string sUserID = _docDA.GetUserId(UserId);

                retMsg = _docDA.DeleteInvoice(DocumentID, sUserID, ApproveLevel, DeleteReason);

                //_docDA.AddAPInvoiceNoteDB(DocumentID, DeleteReason, "Log", "Delete", (short)ApproveLevel, sUserID, DateTime.Now, "Critical", -1);

                //_docDA.CloseAllActions(DocumentID, sUserID, "Invoice Deleted", 0, DateTime.Now);
                //retMsg = new RetMessageFormatter(title, retMsg).GetReturnMessage();

                if (retMsg.Contains("SUCCESS"))
                    retMsg = new RetMessageFormatter("SUCCESS", "Document deleted successfully").GetReturnMessage();
                else
                    retMsg = new RetMessageFormatter("ERROR", retMsg).GetReturnMessage();
            }
            catch (Exception ex)
            {
                retMsg = "ERROR--- Error occured while deleting document";
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                _docDA.LogError("CoreAPIDocumentSvc", "DeleteDocument", ex.Message, innerExMsg, "", "");
                title = "EXCEPTION";
                retMsg = ex.Message + "  --- " + innerExMsg;
                retMsg = new RetMessageFormatter(title, retMsg).GetReturnMessage();
            }


            return retMsg;
        }

        public string APHoldDocument(int DocumentID, int ApproveLevel, string APHoldReason, DateTime APHoldDate, bool APHold, int UserId)
        {
            string retMsg = "";
            string title = "SUCCESS";
            try
            {
                string sUserID = _docDA.GetUserId(UserId);
                retMsg = _docDA.HoldInvoiceInAP(DocumentID, ApproveLevel, sUserID, APHoldReason, APHoldDate, APHold);

                if (retMsg.Contains("SUCCESS"))
                {
                    if (APHold)
                        retMsg = new RetMessageFormatter("SUCCESS", "Document put on AP hold successfully").GetReturnMessage();
                    else
                        retMsg = new RetMessageFormatter("SUCCESS", "Document removed from AP hold successfully").GetReturnMessage();
                }
                else
                    retMsg = new RetMessageFormatter("ERROR", retMsg).GetReturnMessage();

            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                _docDA.LogError("CoreAPIDocumentSvc", "APHoldDocument", ex.Message, innerExMsg, "", "");
                title = "EXCEPTION";
                retMsg = ex.Message + "  --- " + innerExMsg;
                retMsg = new RetMessageFormatter(title, retMsg).GetReturnMessage();
            }


            return retMsg;
        }

        public string HoldDocument(int DocumentID, int ApproveLevel, string HoldReason, DateTime HoldDate, int UserId)
        {
            string retMsg = "";
            string title = "SUCCESS";
            try
            {
                string sUserID = _docDA.GetUserId(UserId);

                retMsg = this._docDA.HoldInvoice(DocumentID, ApproveLevel, sUserID, HoldReason, HoldDate);


                if (retMsg.Contains("SUCCESS"))
                    retMsg = new RetMessageFormatter("SUCCESS", "Document put on Hold successfully").GetReturnMessage();
                else
                    retMsg = new RetMessageFormatter("ERROR", retMsg).GetReturnMessage();
            }
            catch (Exception ex)
            {
                retMsg = "ERROR--- Can not put document on hold";
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                _docDA.LogError("CoreAPIDocumentSvc", "HoldDocument", ex.Message, innerExMsg, "", "");
                title = "EXCEPTION";
                retMsg = ex.Message + "  --- " + innerExMsg;
                retMsg = new RetMessageFormatter(title, retMsg).GetReturnMessage();
            }

            return retMsg;
        }

        public byte[] GetDocumentImagePath(int DocumentID)
        {

            try
            {
                // get document image path
                string imagePath = _docDA.GetDocumentImagePath(DocumentID);
                if (!string.IsNullOrEmpty(imagePath))
                {
                    // check if file exists
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.FileStream fs = new System.IO.FileStream(imagePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                        byte[] img = new byte[fs.Length];
                        fs.Read(img, 0, (int)fs.Length);
                        fs.Close();
                        return img;
                    }
                }
            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                _docDA.LogError("CoreAPIDocumentSvc", "GetDocumentImagePath for document = " + DocumentID.ToString(), ex.Message, innerExMsg, "", "");
            }
            return null;
        }

        public string SaveDocumentBinary(int DocumentID, byte[] DocumentBinary)
        {
            string retMsg = "";
            string title = "SUCCESS";
            try
            {
                retMsg = this.DocumentExists(DocumentID);

                if (!retMsg.Contains("SUCCESS"))
                    return new RetMessageFormatter("ERROR", retMsg).GetReturnMessage();

                // get document image path
                string imagePath = _docDA.GetDocumentImagePath(DocumentID);
                if (DocumentBinary != null && DocumentBinary.Length > 0)
                {
                    title = "ERROR";
                    retMsg = "Document binary is empty";
                }
                else if (!string.IsNullOrEmpty(imagePath))
                {
                    System.IO.File.WriteAllBytes(imagePath, DocumentBinary);
                }
                else
                {
                    title = "ERROR";
                    retMsg = "Original document image does not exists.";
                }
                retMsg = new RetMessageFormatter(title, retMsg).GetReturnMessage();
            }
            catch (Exception ex)
            {
                retMsg = "Error in saving document binary";
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                _docDA.LogError("CoreAPIDocumentSvc", "SaveDocumentBinary", ex.Message, innerExMsg, "", "");
                title = "EXCEPTION";
                retMsg = ex.Message + "  --- " + innerExMsg;
                retMsg = new RetMessageFormatter(title, retMsg).GetReturnMessage();
            }

            return retMsg;
        }

        public Core.API.DocumentSvc.Entity.AnnotationDataDto GetDocumentAnnotation(int DocumentID)
        {
            string retMsg = "";
            string errMsg = "";
            CA.Imaging.Annotations.Serialization.AnnotationDataDto adDTO = null;
            Core.API.DocumentSvc.Entity.AnnotationDataDto apiAnnotationData = new Core.API.DocumentSvc.Entity.AnnotationDataDto();
            try
            {
                retMsg = this.DocumentExists(DocumentID);

                if (!retMsg.Contains("SUCCESS"))
                {
                    errMsg = "DocumentID = " + DocumentID.ToString() + " - Document not exists";
                    _docDA.LogError("CoreAPIDocumentSvc", "GetDocumentAnnotation", "", "", errMsg, "");
                    return apiAnnotationData;

                }

                string imagePath = _docDA.GetDocumentImagePath(DocumentID);
                if (string.IsNullOrEmpty(imagePath))
                {
                    errMsg = "DocumentID = " + DocumentID.ToString() + " - Empty Image Path";
                    _docDA.LogError("CoreAPIDocumentSvc", "GetDocumentAnnotation", "", "", errMsg, "");
                    return apiAnnotationData;
                }

                // string imagePath = @"C:\sanket\temp.tiff";
                IAnnotationProvider annotationProvider = new AtalasoftAnnotationProvider();

                using (var input = File.OpenRead(imagePath))
                    adDTO = annotationProvider.GetAnnotationData(input);

                if (adDTO != null)
                {
                    foreach (var page in adDTO.Pages)
                    {
                        Core.API.DocumentSvc.Entity.AnnotationPageDto apiPage = new Core.API.DocumentSvc.Entity.AnnotationPageDto();

                        apiPage.PageIndex = page.PageIndex;

                        foreach (var layer in page.Layers)
                        {
                            Core.API.DocumentSvc.Entity.AnnotationLayerDto apiLayer = new Entity.AnnotationLayerDto();

                            foreach (var annotation in layer.Annotations)
                            {
                                //text annotation
                                if (annotation.GetType().Equals(typeof(CA.Imaging.Annotations.Serialization.TextAnnotationDto)))
                                {
                                    var tiffTextAnnotation = (CA.Imaging.Annotations.Serialization.TextAnnotationDto)annotation;
                                    var apiTxtAnnot = new Core.API.DocumentSvc.Entity.TextAnnotationDto();

                                    if (apiTxtAnnot.Location == null) apiTxtAnnot.Location = new Entity.PointDto();

                                    if (annotation.Location != null)
                                    {
                                        apiTxtAnnot.Location.X = annotation.Location.X;
                                        apiTxtAnnot.Location.Y = annotation.Location.Y;
                                    }

                                    if (apiTxtAnnot.Size == null) apiTxtAnnot.Size = new Entity.SizeDto();

                                    if (annotation.Size != null)
                                    {
                                        apiTxtAnnot.Size.Height = annotation.Size.Height;
                                        apiTxtAnnot.Size.Width = annotation.Size.Width;
                                    }

                                    apiTxtAnnot.Rotation = annotation.Rotation;
                                    apiTxtAnnot.CanMirror = annotation.CanMirror;
                                    apiTxtAnnot.CanMove = annotation.CanMove;
                                    apiTxtAnnot.CanResize = annotation.CanResize;
                                    apiTxtAnnot.CanRotate = annotation.CanRotate;
                                    apiTxtAnnot.CanSelect = annotation.CanSelect;
                                    apiTxtAnnot.CreationTime = annotation.CreationTime;
                                    apiTxtAnnot.ModifiedTime = annotation.ModifiedTime;
                                    apiTxtAnnot.Visible = annotation.Visible;
                                    apiTxtAnnot.ToolTip = annotation.ToolTip;

                                    // -----------------------------------------------------------------------------
                                    apiTxtAnnot.Text = tiffTextAnnotation.Text;

                                    if (apiTxtAnnot.Fill == null)
                                        apiTxtAnnot.Fill = new Entity.BrushDto();
                                    if (tiffTextAnnotation.Fill != null)
                                        apiTxtAnnot.Fill.Color = tiffTextAnnotation.Fill.Color;

                                    apiTxtAnnot.LineAlignment = tiffTextAnnotation.LineAlignment;
                                    apiTxtAnnot.Alignment = tiffTextAnnotation.Alignment;
                                    apiTxtAnnot.Minimized = tiffTextAnnotation.Minimized;
                                    apiTxtAnnot.AutoSize = tiffTextAnnotation.AutoSize;
                                    apiTxtAnnot.AllowEditing = tiffTextAnnotation.AllowEditing;
                                    apiTxtAnnot.Padding = tiffTextAnnotation.Padding;

                                    if (apiTxtAnnot.Font == null)
                                        apiTxtAnnot.Font = new Entity.FontDto();

                                    if (tiffTextAnnotation.Font != null)
                                    {
                                        apiTxtAnnot.Font.Size = tiffTextAnnotation.Font.Size;
                                        apiTxtAnnot.Font.Italic = tiffTextAnnotation.Font.Italic;
                                        apiTxtAnnot.Font.Underline = tiffTextAnnotation.Font.Underline;
                                        apiTxtAnnot.Font.Bold = tiffTextAnnotation.Font.Bold;
                                        apiTxtAnnot.Font.Name = tiffTextAnnotation.Font.Name;
                                        apiTxtAnnot.Font.Type = tiffTextAnnotation.Font.Type;
                                    }

                                    if (apiTxtAnnot.FontBrush == null)
                                        apiTxtAnnot.FontBrush = new Entity.BrushDto();

                                    if (tiffTextAnnotation.FontBrush != null)
                                    {
                                        apiTxtAnnot.FontBrush.Color = tiffTextAnnotation.FontBrush.Color;
                                    }

                                    if (apiTxtAnnot.Outline == null)
                                        apiTxtAnnot.Outline = new Entity.PenDto();

                                    if (tiffTextAnnotation.Outline != null)
                                    {
                                        apiTxtAnnot.Outline.Color = tiffTextAnnotation.Outline.Color;
                                        apiTxtAnnot.Outline.Width = tiffTextAnnotation.Outline.Width;
                                    }

                                    apiTxtAnnot.Trimming = tiffTextAnnotation.Trimming;

                                    if (apiLayer.TextAnnotations == null)
                                        apiLayer.TextAnnotations = new List<Entity.TextAnnotationDto>();

                                    apiLayer.TextAnnotations.Add(apiTxtAnnot);

                                }// end of Text Annotation
                                else if (annotation.GetType().Equals(typeof(CA.Imaging.Annotations.Serialization.PenAnnotationDto)))
                                {
                                    var tiffPenAnnotation = (CA.Imaging.Annotations.Serialization.PenAnnotationDto)annotation;
                                    var apiPenAnnot = new Core.API.DocumentSvc.Entity.PenAnnotationDto();

                                    if (apiPenAnnot.Location == null) apiPenAnnot.Location = new Entity.PointDto();

                                    if (apiPenAnnot.Location != null)
                                    {
                                        apiPenAnnot.Location.X = annotation.Location.X;
                                        apiPenAnnot.Location.Y = annotation.Location.Y;
                                    }

                                    if (apiPenAnnot.Size == null) apiPenAnnot.Size = new Entity.SizeDto();

                                    if (annotation.Size != null)
                                    {
                                        apiPenAnnot.Size.Height = annotation.Size.Height;
                                        apiPenAnnot.Size.Width = annotation.Size.Width;
                                    }

                                    apiPenAnnot.Rotation = annotation.Rotation;
                                    apiPenAnnot.CanMirror = annotation.CanMirror;
                                    apiPenAnnot.CanMove = annotation.CanMove;
                                    apiPenAnnot.CanResize = annotation.CanResize;
                                    apiPenAnnot.CanRotate = annotation.CanRotate;
                                    apiPenAnnot.CanSelect = annotation.CanSelect;
                                    apiPenAnnot.CreationTime = annotation.CreationTime;
                                    apiPenAnnot.ModifiedTime = annotation.ModifiedTime;
                                    apiPenAnnot.Visible = annotation.Visible;
                                    apiPenAnnot.ToolTip = annotation.ToolTip;

                                    //-----------------------------------------------------------------------------

                                    if (apiPenAnnot.Pen == null) apiPenAnnot.Pen = new Entity.PenDto();

                                    if (tiffPenAnnotation.Pen != null)
                                    {
                                        apiPenAnnot.Pen.Width = tiffPenAnnotation.Pen.Width;
                                        apiPenAnnot.Pen.Color = tiffPenAnnotation.Pen.Color;
                                    }

                                    if (apiPenAnnot.Points == null) apiPenAnnot.Points = new List<Entity.PointDto>();
                                    foreach (var tiffPoint in tiffPenAnnotation.Points)
                                    {
                                        var apiPoint = new Entity.PointDto();
                                        apiPoint.X = tiffPoint.X;
                                        apiPoint.Y = tiffPoint.Y;
                                        apiPenAnnot.Points.Add(apiPoint);
                                    }

                                    if (apiLayer.PenAnnotations == null)
                                        apiLayer.PenAnnotations = new List<Entity.PenAnnotationDto>();

                                    apiLayer.PenAnnotations.Add(apiPenAnnot);

                                }//end of Pen Annotation
                                else if (annotation.GetType().Equals(typeof(CA.Imaging.Annotations.Serialization.RectangleAnnotationDto)))
                                {
                                    var tiffRectAnnotation = (CA.Imaging.Annotations.Serialization.RectangleAnnotationDto)annotation;
                                    var apiRectAnnot = new Core.API.DocumentSvc.Entity.RectangleAnnotationDto();

                                    if (apiRectAnnot.Location == null) apiRectAnnot.Location = new Entity.PointDto();

                                    if (annotation.Location != null)
                                    {
                                        apiRectAnnot.Location.X = annotation.Location.X;
                                        apiRectAnnot.Location.Y = annotation.Location.Y;
                                    }

                                    if (apiRectAnnot.Size == null) apiRectAnnot.Size = new Entity.SizeDto();

                                    if (apiRectAnnot.Size != null)
                                    {
                                        apiRectAnnot.Size.Height = annotation.Size.Height;
                                        apiRectAnnot.Size.Width = annotation.Size.Width;
                                    }

                                    apiRectAnnot.Rotation = annotation.Rotation;
                                    apiRectAnnot.CanMirror = annotation.CanMirror;
                                    apiRectAnnot.CanMove = annotation.CanMove;
                                    apiRectAnnot.CanResize = annotation.CanResize;
                                    apiRectAnnot.CanRotate = annotation.CanRotate;
                                    apiRectAnnot.CanSelect = annotation.CanSelect;
                                    apiRectAnnot.CreationTime = annotation.CreationTime;
                                    apiRectAnnot.ModifiedTime = annotation.ModifiedTime;
                                    apiRectAnnot.Visible = annotation.Visible;
                                    apiRectAnnot.ToolTip = annotation.ToolTip;

                                    // ------------------------------------
                                    if (apiRectAnnot.Fill == null) apiRectAnnot.Fill = new Entity.BrushDto();

                                    if (tiffRectAnnotation.Fill != null)
                                    {
                                        apiRectAnnot.Fill.Color = tiffRectAnnotation.Fill.Color;
                                    }

                                    if (apiRectAnnot.Outline == null) apiRectAnnot.Outline = new Entity.PenDto();

                                    if (tiffRectAnnotation.Outline != null)
                                    {
                                        apiRectAnnot.Outline.Color = tiffRectAnnotation.Outline.Color;
                                        apiRectAnnot.Outline.Width = tiffRectAnnotation.Outline.Width;
                                    }

                                    if (apiLayer.RectanngleAnnotations == null)
                                        apiLayer.RectanngleAnnotations = new List<Entity.RectangleAnnotationDto>();

                                    apiLayer.RectanngleAnnotations.Add(apiRectAnnot);

                                }// end of Rectangle Annotation

                            }// end for each - layer.annotation


                            // add layer to pages
                            if (apiPage.Layers == null)
                                apiPage.Layers = new List<Entity.AnnotationLayerDto>();

                            apiPage.Layers.Add(apiLayer);

                        }// end for each - layers

                        // add page to annotation data
                        if (apiAnnotationData.Pages == null)
                            apiAnnotationData.Pages = new List<Entity.AnnotationPageDto>();

                        apiAnnotationData.Pages.Add(apiPage);

                    }// end for each pages

                } // end annotation

                //ent.AnnotationData = adDTO;

                //var serializer = new XmlSerializer(typeof(AnnotationDataDto));
                //using (var stringwriter = new System.IO.StringWriter())
                //{

                //    serializer.Serialize(stringwriter, adDTO);
                //    retMsg = stringwriter.ToString();
                //}

            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                _docDA.LogError("CoreAPIDocumentSvc", "GetDocumentAnnotation", ex.Message, innerExMsg, "", "");
                retMsg = ex.Message + "  --- " + innerExMsg;
            }
            //return ent;
            return apiAnnotationData;
            //  return retMsg;
        }

        public string SaveDocumentAnnotation(int DocumentID, Core.API.DocumentSvc.Entity.AnnotationDataDto Annotation)
        {
            string retMsg = "";
            string errMsg = "";

            CA.Imaging.Annotations.Serialization.AnnotationDataDto tiffAnnotDTO = null;

            try
            {
                retMsg = this.DocumentExists(DocumentID);

                if (!retMsg.Contains("SUCCESS"))
                {
                    errMsg = "DocumentID = " + DocumentID.ToString() + " - Document not exists";
                    _docDA.LogError("CoreAPIDocumentSvc", "GetDocumentAnnotation", "", "", errMsg, "");
                    return errMsg;

                }

                string imagePath = _docDA.GetDocumentImagePath(DocumentID);
                if (string.IsNullOrEmpty(imagePath))
                {
                    errMsg = "DocumentID = " + DocumentID.ToString() + " - Empty Image Path";
                    _docDA.LogError("CoreAPIDocumentSvc", "GetDocumentAnnotation", "", "", errMsg, "");
                    return errMsg;
                }

                if (Annotation != null)
                {
                    foreach (var apipage in Annotation.Pages)
                    {
                        CA.Imaging.Annotations.Serialization.AnnotationPageDto tiffPage = new CA.Imaging.Annotations.Serialization.AnnotationPageDto();

                        tiffPage.PageIndex = apipage.PageIndex;

                        foreach (var layer in apipage.Layers)
                        {
                            CA.Imaging.Annotations.Serialization.AnnotationLayerDto tiffLayer = new CA.Imaging.Annotations.Serialization.AnnotationLayerDto();

                            if (layer.TextAnnotations != null)
                            {
                                foreach (var annotation in layer.TextAnnotations)
                                {
                                    CA.Imaging.Annotations.Serialization.TextAnnotationDto tiffTextDto = new CA.Imaging.Annotations.Serialization.TextAnnotationDto();
                                    if (tiffTextDto.Location == null) tiffTextDto.Location = new CA.Imaging.Annotations.Serialization.PointDto();

                                    if (annotation.Location != null)
                                    {
                                        tiffTextDto.Location.X = annotation.Location.X;
                                        tiffTextDto.Location.Y = annotation.Location.Y;
                                    }

                                    if (tiffTextDto.Size == null) tiffTextDto.Size = new CA.Imaging.Annotations.Serialization.SizeDto();

                                    if (annotation.Size != null)
                                    {
                                        tiffTextDto.Size.Height = annotation.Size.Height;
                                        tiffTextDto.Size.Width = annotation.Size.Width;
                                    }

                                    tiffTextDto.Rotation = annotation.Rotation;
                                    tiffTextDto.CanMirror = annotation.CanMirror;
                                    tiffTextDto.CanMove = annotation.CanMove;
                                    tiffTextDto.CanResize = annotation.CanResize;
                                    tiffTextDto.CanRotate = annotation.CanRotate;
                                    tiffTextDto.CanSelect = annotation.CanSelect;
                                    tiffTextDto.CreationTime = annotation.CreationTime;
                                    tiffTextDto.ModifiedTime = annotation.ModifiedTime;
                                    tiffTextDto.Visible = annotation.Visible;
                                    tiffTextDto.ToolTip = annotation.ToolTip;

                                    // -----------------------------------------------------------------------------
                                    tiffTextDto.Text = annotation.Text;

                                    if (tiffTextDto.Fill == null)
                                        tiffTextDto.Fill = new CA.Imaging.Annotations.Serialization.BrushDto();
                                    if (annotation.Fill != null)
                                        tiffTextDto.Fill.Color = annotation.Fill.Color;

                                    tiffTextDto.LineAlignment = annotation.LineAlignment;
                                    tiffTextDto.Alignment = annotation.Alignment;
                                    tiffTextDto.Minimized = annotation.Minimized;
                                    tiffTextDto.AutoSize = annotation.AutoSize;
                                    tiffTextDto.AllowEditing = annotation.AllowEditing;
                                    tiffTextDto.Padding = annotation.Padding;

                                    if (tiffTextDto.Font == null)
                                        tiffTextDto.Font = new CA.Imaging.Annotations.Serialization.FontDto();

                                    if (annotation.Font != null)
                                    {
                                        tiffTextDto.Font.Size = annotation.Font.Size;
                                        tiffTextDto.Font.Italic = annotation.Font.Italic;
                                        tiffTextDto.Font.Underline = annotation.Font.Underline;
                                        tiffTextDto.Font.Bold = annotation.Font.Bold;
                                        tiffTextDto.Font.Name = annotation.Font.Name;
                                        tiffTextDto.Font.Type = annotation.Font.Type;
                                    }

                                    if (tiffTextDto.FontBrush == null)
                                        tiffTextDto.FontBrush = new CA.Imaging.Annotations.Serialization.BrushDto();

                                    if (annotation.FontBrush != null)
                                    {
                                        tiffTextDto.FontBrush.Color = annotation.FontBrush.Color;
                                    }

                                    if (tiffTextDto.Outline == null)
                                        tiffTextDto.Outline = new CA.Imaging.Annotations.Serialization.PenDto();

                                    if (annotation.Outline != null)
                                    {
                                        tiffTextDto.Outline.Color = annotation.Outline.Color;
                                        tiffTextDto.Outline.Width = annotation.Outline.Width;
                                    }

                                    tiffTextDto.Trimming = annotation.Trimming;

                                    if (tiffLayer.Annotations == null)
                                        tiffLayer.Annotations = new List<CA.Imaging.Annotations.Serialization.AnnotationDto>();

                                    tiffLayer.Annotations.Add(tiffTextDto);
                                }
                            }

                            if (layer.PenAnnotations != null)
                            {
                                foreach (var annotation in layer.PenAnnotations)
                                {
                                    CA.Imaging.Annotations.Serialization.PenAnnotationDto tiffPenDto = new CA.Imaging.Annotations.Serialization.PenAnnotationDto();
                                    if (tiffPenDto.Location == null) tiffPenDto.Location = new CA.Imaging.Annotations.Serialization.PointDto();

                                    if (annotation.Location != null)
                                    {
                                        tiffPenDto.Location.X = annotation.Location.X;
                                        tiffPenDto.Location.Y = annotation.Location.Y;
                                    }

                                    if (tiffPenDto.Size == null) tiffPenDto.Size = new CA.Imaging.Annotations.Serialization.SizeDto();

                                    if (annotation.Size != null)
                                    {
                                        tiffPenDto.Size.Height = annotation.Size.Height;
                                        tiffPenDto.Size.Width = annotation.Size.Width;
                                    }

                                    tiffPenDto.Rotation = annotation.Rotation;
                                    tiffPenDto.CanMirror = annotation.CanMirror;
                                    tiffPenDto.CanMove = annotation.CanMove;
                                    tiffPenDto.CanResize = annotation.CanResize;
                                    tiffPenDto.CanRotate = annotation.CanRotate;
                                    tiffPenDto.CanSelect = annotation.CanSelect;
                                    tiffPenDto.CreationTime = annotation.CreationTime;
                                    tiffPenDto.ModifiedTime = annotation.ModifiedTime;
                                    tiffPenDto.Visible = annotation.Visible;
                                    tiffPenDto.ToolTip = annotation.ToolTip;

                                    //-----------------------------------------------------------------------------

                                    if (tiffPenDto.Pen == null) tiffPenDto.Pen = new CA.Imaging.Annotations.Serialization.PenDto();

                                    if (annotation.Pen != null)
                                    {
                                        tiffPenDto.Pen.Width = annotation.Pen.Width;
                                        tiffPenDto.Pen.Color = annotation.Pen.Color;
                                    }

                                    if (tiffPenDto.Points == null) tiffPenDto.Points = new List<CA.Imaging.Annotations.Serialization.PointDto>();
                                    foreach (var apiPoint in annotation.Points)
                                    {
                                        var tiffPoint = new CA.Imaging.Annotations.Serialization.PointDto();
                                        tiffPoint.X = apiPoint.X;
                                        tiffPoint.Y = apiPoint.Y;
                                        tiffPenDto.Points.Add(tiffPoint);
                                    }

                                    if (tiffLayer.Annotations == null)
                                        tiffLayer.Annotations = new List<CA.Imaging.Annotations.Serialization.AnnotationDto>();

                                    tiffLayer.Annotations.Add(tiffPenDto);
                                }
                            }
                            if (layer.RectanngleAnnotations != null)
                            {
                                foreach (var annotation in layer.RectanngleAnnotations)
                                {
                                    CA.Imaging.Annotations.Serialization.RectangleAnnotationDto tiffRectDto = new CA.Imaging.Annotations.Serialization.RectangleAnnotationDto();
                                    if (tiffRectDto.Location == null) tiffRectDto.Location = new CA.Imaging.Annotations.Serialization.PointDto();

                                    if (annotation.Location != null)
                                    {
                                        tiffRectDto.Location.X = annotation.Location.X;
                                        tiffRectDto.Location.Y = annotation.Location.Y;
                                    }

                                    if (tiffRectDto.Size == null) tiffRectDto.Size = new CA.Imaging.Annotations.Serialization.SizeDto();

                                    if (annotation.Size != null)
                                    {
                                        tiffRectDto.Size.Height = annotation.Size.Height;
                                        tiffRectDto.Size.Width = annotation.Size.Width;
                                    }

                                    tiffRectDto.Rotation = annotation.Rotation;
                                    tiffRectDto.CanMirror = annotation.CanMirror;
                                    tiffRectDto.CanMove = annotation.CanMove;
                                    tiffRectDto.CanResize = annotation.CanResize;
                                    tiffRectDto.CanRotate = annotation.CanRotate;
                                    tiffRectDto.CanSelect = annotation.CanSelect;
                                    tiffRectDto.CreationTime = annotation.CreationTime;
                                    tiffRectDto.ModifiedTime = annotation.ModifiedTime;
                                    tiffRectDto.Visible = annotation.Visible;
                                    tiffRectDto.ToolTip = annotation.ToolTip;

                                    // ------------------------------------
                                    if (tiffRectDto.Fill == null) tiffRectDto.Fill = new CA.Imaging.Annotations.Serialization.BrushDto();

                                    if (annotation.Fill != null)
                                    {
                                        tiffRectDto.Fill.Color = annotation.Fill.Color;
                                    }

                                    if (tiffRectDto.Outline == null) tiffRectDto.Outline = new CA.Imaging.Annotations.Serialization.PenDto();

                                    if (annotation.Outline != null)
                                    {
                                        tiffRectDto.Outline.Color = annotation.Outline.Color;
                                        tiffRectDto.Outline.Width = annotation.Outline.Width;
                                    }

                                    if (tiffLayer.Annotations == null)
                                        tiffLayer.Annotations = new List<CA.Imaging.Annotations.Serialization.AnnotationDto>();

                                    tiffLayer.Annotations.Add(tiffRectDto);
                                }
                            }
                            // add layer to pages
                            if (tiffPage.Layers == null)
                                tiffPage.Layers = new List<CA.Imaging.Annotations.Serialization.AnnotationLayerDto>();

                            tiffPage.Layers.Add(tiffLayer);

                        }// end for each - layers

                        // add page to annotation data
                        if (tiffAnnotDTO.Pages == null)
                            tiffAnnotDTO.Pages = new List<CA.Imaging.Annotations.Serialization.AnnotationPageDto>();

                        if (tiffAnnotDTO == null)
                            tiffAnnotDTO = new CA.Imaging.Annotations.Serialization.AnnotationDataDto();
                        tiffAnnotDTO.Pages.Add(tiffPage);

                    }// end for each pages

                } // end annotation

                if (tiffAnnotDTO != null)
                {
                    IAnnotationProvider annotationProvider = new AtalasoftAnnotationProvider();
                    var annotationPath = imagePath;

                    using (var output = File.OpenWrite(imagePath))
                    using (var writer = XmlWriter.Create(output, new XmlWriterSettings { Encoding = Encoding.ASCII, Indent = true }))
                    {
                        var serializer = new XmlSerializer(typeof(CA.Imaging.Annotations.Serialization.AnnotationDataDto));
                        serializer.Serialize(writer, tiffAnnotDTO);
                        writer.Flush();
                        output.Flush(true);
                    }
                }

            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                _docDA.LogError("CoreAPIDocumentSvc", "SaveDocumentAnnotation", ex.Message, innerExMsg, "", "");
                retMsg = ex.Message + "  --- " + innerExMsg;
            }
            return retMsg;
        }

        public string ApproveDocument(int DocumentID, int UserID)
        {
            //add validation for DocumentExists and also If User is valid and MobileUser then proccessed with approval
            RetMessage retMsg = new RetMessage();
            DocumentEnt doc = new DocumentEnt();
            doc.DocumentID = DocumentID;
            doc.Header.DocumentId = DocumentID;
            doc.UserID = UserID;
            doc.ApproveType = "Approve";
            string strErrMsg = "";
            string title = "SUCCESS";

            try
            {
                // validate document and prepare it for approve 
                retMsg.Message = ValidateInvoiceToApprove(DocumentID, 0, UserID);
                retMsg.Id = DocumentID;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(retMsg.Message))
                    _docDA.LogError("CoreAPIDocumentSvc", "ApproveDocument", retMsg.Message, "", "Exception while processing documents", LoginID);

                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                _docDA.LogError("CoreAPIDocumentSvc", "ApproveDocument", ex.Message, innerExMsg, ex.StackTrace, LoginID);

                retMsg.Id = doc.DocumentID;
                retMsg.Message = ex.Message + "  --- " + innerExMsg;
                retMsg.Message = new RetMessageFormatter("EXCEPTION", retMsg.Message).GetReturnMessage();
            }

            try
            {
                if (string.IsNullOrEmpty(retMsg.Message))
                {
                    DataTable dtInvoice = new DataTable();
                    DataTable dtDistributions = new DataTable();
                    int CurrentActionLevel = 0;
                    int ConnectionID = 0;
                    string approverUserId = _docDA.GetUserId(UserID);
                    DataSet dsInvoice = _docDA.GetInvoice(DocumentID);
                    if (dsInvoice != null && dsInvoice.Tables.Count > 0)
                    {
                        dtInvoice = dsInvoice.Tables[0];
                        int.TryParse(dtInvoice.Rows[0]["CurrentActionLevel"].ToString(), out CurrentActionLevel);
                        int.TryParse(dtInvoice.Rows[0]["ConnectionId"].ToString(), out ConnectionID);
                    }
                    DataSet dsDistributions = _docDA.GetInvoiceDistributions(DocumentID);
                    if (dsDistributions != null && dsDistributions.Tables.Count > 0)
                    {
                        dtDistributions = dsDistributions.Tables[0];
                    }
                    DataTable dtNextActions = this.GetNextLevelActions(dtInvoice, dtDistributions, CurrentActionLevel, UserID, out strErrMsg, ConnectionID);
                    retMsg.Message = strErrMsg;
                    if (dtNextActions != null && dtNextActions.Rows.Count > 0)
                    {
                        retMsg.Message = _docDA.ApproveDocument(doc);
                        if (string.IsNullOrEmpty(retMsg.Message))
                        {
                            System.Collections.Hashtable htDupActions = new System.Collections.Hashtable();
                            foreach (DataRow action in dtNextActions.Rows)
                            {
                                if (!htDupActions.Contains(action["UserId"] == DBNull.Value || action["UserId"] == null || string.IsNullOrEmpty(action["UserId"].ToString()) ? action["GroupId"] : action["UserId"]))
                                {

                                    htDupActions.Add(action["UserId"] == DBNull.Value || action["UserId"] == null || string.IsNullOrEmpty(action["UserId"].ToString()) ? action["GroupId"] : action["UserId"], action);

                                    //if ("Recurring|Upload|UploadRM".Contains(invType) && (action["ActionType"].ToString() == "Final Review" || action["ActionType"].ToString() == "Export"))
                                    //{

                                    //    finalize = true;
                                    //    break;
                                    //}
                                    //else
                                    //{
                                    //route invoice will add the invoice routed note and create the invoice action

                                    Int32 intgrpid = -1;
                                    Int32.TryParse(action["GroupId"].ToString(), out intgrpid);
                                    _docDA.RouteInvoice(ConnectionID, DocumentID, Convert.ToInt32(action["ProcessGroupId"]), action["UserId"].ToString(), intgrpid, action["ActionType"].ToString(),
                                            Convert.ToInt32(action["ActionLevel"]), false, false, _docDA.GetCurrentDateAndTimeOnSqlServerDate(), "MOBILE", approverUserId);

                                    //}
                                }
                            }
                            _docDA.UpdateInvoiceActionInfo(DocumentID, Convert.ToInt32(dtNextActions.Rows[0]["ActionLevel"]), dtNextActions.Rows[0]["ActionType"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                _docDA.LogError("CoreAPIDocumentSvc", "ApproveDocument", ex.Message, innerExMsg, "", "");
                title = "EXCEPTION";
                if (retMsg == null)
                    retMsg = new RetMessage();
                retMsg.Id = DocumentID;
                retMsg.Message = ex.Message + "  --- " + innerExMsg;
                retMsg.Message = new RetMessageFormatter(title, retMsg.Message).GetReturnMessage();
            }
            if (string.IsNullOrEmpty(retMsg.Message))
            {
                retMsg.Id = doc.DocumentID;
                retMsg.Message = new RetMessageFormatter("SUCCESS", "Document approved successfully.").GetReturnMessage();
            }
            
            return retMsg.Message;
        }

        public string SaveDocument(DocumentEnt doc)
        {
            RetMessage retMsg = new RetMessage();

            try
            {
                // prepare document to save. 
                retMsg = ProcessDocument(doc);

                // save document
                if (string.IsNullOrEmpty(retMsg.Message))
                    retMsg.Message = _docDA.SaveDocument(doc);
                else
                    return retMsg.Message;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(retMsg.Message))
                    _docDA.LogError("CoreAPIDocumentSvc", "SaveDocument", retMsg.Message, "", "Exception while saving documents", LoginID);

                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                _docDA.LogError("CoreAPIDocumentSvc", "SaveDocument", ex.Message, innerExMsg, ex.StackTrace, LoginID);

                retMsg.Id = doc.DocumentID;
                retMsg.Message = ex.Message + "  --- " + innerExMsg;
                retMsg.Message = new RetMessageFormatter("EXCEPTION", retMsg.Message).GetReturnMessage();
            }

            if (string.IsNullOrEmpty(retMsg.Message))
                retMsg.Message = new RetMessageFormatter("SUCCESS", "Document saved successfully.").GetReturnMessage();
            else if (retMsg.Message.ToUpper().Contains("EXCEPTION"))
                retMsg.Message = new RetMessageFormatter("EXCEPTION", retMsg.Message).GetReturnMessage();
            else
                retMsg.Message = new RetMessageFormatter("ERROR", retMsg.Message).GetReturnMessage();

            return retMsg.Message;
        }

        public string RouteDocumentManual(int DocumentID, int ActionLevel, int RoutingUserID, int RouteToID, string RouteToComment)
        {
            string retMsg = "";
            string title = "";
            try
            {
                retMsg = _docDA.RouteDocumentManual(DocumentID, ActionLevel, RoutingUserID, RouteToID, RouteToComment);
                if (string.IsNullOrEmpty(retMsg))
                    retMsg = new RetMessageFormatter("SUCCESS", "Document routed successfully").GetReturnMessage();
                else
                    retMsg = new RetMessageFormatter("ERROR", retMsg).GetReturnMessage();
            }
            catch (Exception ex)
            {
                retMsg = "ERROR--- Error occured in routing document";
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                _docDA.LogError("CoreAPIDocumentSvc", "RouteDocumentManual", ex.Message, innerExMsg, "", "");
                title = "EXCEPTION";
                retMsg = ex.Message + "  --- " + innerExMsg;
                retMsg = new RetMessageFormatter(title, retMsg).GetReturnMessage();
            }

            return retMsg;
        }

        #endregion

        #region Document Processing

        internal RetMessage ProcessDocument(DocumentEnt doc)
        {
            RetMessage retMsg = new RetMessage();
            bool blnContinue = true;

            try
            {
                // TODO - Route Invoice and Threshorder

                //Initialization
                retMsg.Message = this.Init(doc);

                // validate invoice to save
                if (string.IsNullOrEmpty(retMsg.Message))
                {
                    retMsg.Message = ValidateInvoiceToSave(doc);
                    if (!string.IsNullOrEmpty(retMsg.Message))
                        return retMsg;
                }
                else
                    return retMsg;

                if (string.IsNullOrEmpty(retMsg.Message))
                {
                    //get current distributions by invoice id
                    List<GetInvoiceDistributions_Result> lstDistributions = _docDA.GetInvoiceDistributionEnt(doc.Header.DocumentId);

                    // Determine Amount vs Pretax -- ask HH2 to supply Either Amount or PreTax based on Config, to avoid API to determine 
                    this.SetDocumentAmounts(doc);

                    foreach (var dist in doc.Distributions)
                    {
                        if (!blnContinue) break; // stop processing in case of exception

                        if (!string.IsNullOrEmpty(retMsg.Message))
                            break;
                        if (!string.IsNullOrEmpty(dist.DistStatus) && !dist.DistStatus.ToUpper().Equals("D")) // loop through non deleted dist rows to determin expense account
                        {
                            string dtExpAccount = "";
                            string dtAccountsPayableAccount = "";
                            
                            if (!dist.DistStatus.ToUpper().Equals("I") && lstDistributions != null && lstDistributions.Count() > 0)// retreive only for existing distribution
                            {
                                dtExpAccount = lstDistributions.Where(x => x.DistSeq == dist.DistSeq).FirstOrDefault().Expense_Account;
                                dtAccountsPayableAccount = lstDistributions.Where(x => x.DistSeq == dist.DistSeq).FirstOrDefault().Accounts_Payable_Account;
                            }

                            try
                            {
                                
                                if(string.IsNullOrEmpty(dist.ExpenseAccount) && string.IsNullOrEmpty(dtExpAccount)) // both are empty then set it to default
                                {
                                    string expAcct = "";
                                    retMsg.Message = GetAPIExpenseAccount(dist.Job, dist.Extra, dist.CostCode, dist.Category, dist.Equipment,
                                          dist.Cost_Code_295, dist.ExpenseAccount, doc.Header.Vendor.Id, ref expAcct);

                                    //update expense account
                                    if (string.IsNullOrEmpty(retMsg.Message))
                                        dist.ExpenseAccount = expAcct;
                                }
                                else if (string.IsNullOrEmpty(dist.ExpenseAccount) && !string.IsNullOrEmpty(dtExpAccount)) // if exp account passed as empty and db exp acct is not empty, then set it with db exp acct
                                {
                                    dist.ExpenseAccount = dtExpAccount;
                                }
                                //else if (!string.IsNullOrEmpty(dist.ExpenseAccount) && !string.IsNullOrEmpty(dtExpAccount) && !dist.ExpenseAccount.Equals(dtExpAccount)) // passed exp acct and db exp acct not empty and passed exp act don't match with db exp account
                                //{
                                //    string expAcct = "";
                                //    retMsg.Message = GetAPIExpenseAccount(dist.Job, dist.Extra, dist.CostCode, dist.Category, dist.Equipment,
                                //          dist.Cost_Code_295, dist.ExpenseAccount, doc.Header.Vendor.Id, ref expAcct);

                                //    //update expense account
                                //    if (string.IsNullOrEmpty(retMsg.Message))
                                //        dist.ExpenseAccount = expAcct;
                                //}
                            }
                            catch (Exception ex)
                            {
                                blnContinue = false; // stop furhter processing of distribution records and return error message

                                if (!string.IsNullOrEmpty(retMsg.Message))
                                    _docDA.LogError("CoreAPIDocumentSvc", "SaveDocument", retMsg.Message, "", "Expense Account Validation Error", LoginID);

                                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                                _docDA.LogError("CoreAPIDocumentSvc", "SaveDocument", ex.Message, innerExMsg, ex.StackTrace, LoginID);
                            }

                            try
                            {
                                if (string.IsNullOrEmpty(retMsg.Message))
                                {
                                    if (string.IsNullOrEmpty(dist.AccountsPayableAccount) && !string.IsNullOrEmpty(dtAccountsPayableAccount)) // if AccountsPayableAccount passed as empty and db AccountsPayableAccount is not empty, then set it with db AccountsPayableAccount
                                    {
                                        dist.AccountsPayableAccount = dtAccountsPayableAccount;
                                    }
                                    else if (string.IsNullOrEmpty(dist.AccountsPayableAccount) && string.IsNullOrEmpty(dtAccountsPayableAccount)) // both are empty then set it to default
                                    {
                                        //if (!apDistSettings.Payable_Account)
                                        //{
                                            string glPrefix = DetermineAPDistGLPrefix(dist.Job, dist.Extra, dist.CostCode, dist.Category, dist.Equipment,
                                                   dist.Cost_Code_295, apDistSettings.Equipment, adoApVendorRs, ConnectionID);
                                            //_docDA.LogError("CoreAPIDocumentSvc", "DetermineAPDistGLPrefix", "glPrefix - " + glPrefix, "", "", "");

                                            string sSepChar = _actualTSCTL.DeterminePunctuation();
                                            //_docDA.LogError("CoreAPIDocumentSvc", "GetGLPrefixPunct", "sSepChar - " + sSepChar, "", "", "");

                                            string acctPayableAccount = GetAccountsPayableAccount(dist.Commitment, ConnectionID);
                                            //_docDA.LogError("CoreAPIDocumentSvc", "GetAccountsPayableAccount", "acctPayableAccount - " + acctPayableAccount, "", "", "");

                                            dist.AccountsPayableAccount = glPrefix + sSepChar + acctPayableAccount;
                                        //}
                                    }
                                    //else if (!string.IsNullOrEmpty(dist.AccountsPayableAccount) &&  !string.IsNullOrEmpty(dtAccountsPayableAccount) && !dist.AccountsPayableAccount.Equals(dtAccountsPayableAccount) ) 
                                    //{
                                    //    if (!apDistSettings.Payable_Account) // Accounts_Payable_Account column is not visible, then get default
                                    //    {

                                    //        string glPrefix = DetermineAPDistGLPrefix(dist.Job, dist.Extra, dist.CostCode, dist.Category, dist.Equipment,
                                    //           dist.Cost_Code_295, apDistSettings.Equipment, adoApVendorRs, ConnectionID);
                                    //        //_docDA.LogError("CoreAPIDocumentSvc", "DetermineAPDistGLPrefix", "glPrefix - " + glPrefix, "", "", "");

                                    //        string sSepChar = _actualTSCTL.DeterminePunctuation();
                                    //        //_docDA.LogError("CoreAPIDocumentSvc", "GetGLPrefixPunct", "sSepChar - " + sSepChar, "", "", "");

                                    //        string acctPayableAccount = GetAccountsPayableAccount(dist.Commitment, ConnectionID);
                                    //        //_docDA.LogError("CoreAPIDocumentSvc", "GetAccountsPayableAccount", "acctPayableAccount - " + acctPayableAccount, "", "", "");

                                    //        dist.AccountsPayableAccount = glPrefix + sSepChar + acctPayableAccount;
                                    //    }
                                    //}
                                }
                            }
                            catch (Exception ex)
                            {
                                blnContinue = false; // stop furhter processing of distribution records and return error message

                                if (!string.IsNullOrEmpty(retMsg.Message))
                                    _docDA.LogError("CoreAPIDocumentSvc", "ProcessDocument", retMsg.Message, "", "Account Payable Account Validation Error", LoginID);

                                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                                _docDA.LogError("CoreAPIDocumentSvc", "ProcessDocument", ex.Message, innerExMsg, ex.StackTrace, LoginID);
                            }
                        }
                    }// Foor loop distribution
                }
            }// END TRY
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                _docDA.LogError("CoreAPIDocumentSvc", "SaveDocument", ex.Message, innerExMsg, ex.StackTrace, "");

                if (retMsg == null)
                    retMsg = new RetMessage();
                retMsg.Message = ex.Message + "  --- " + innerExMsg;
                retMsg.Message = new RetMessageFormatter("EXCEPTION", retMsg.Message).GetReturnMessage();
            }
            if (doc != null && doc.Header != null)
                retMsg.Id = doc.DocumentID;

            return retMsg;
        }

        private string GetAPIExpenseAccount(string Job, string Extra, string CostCode, string Category, string Equipment, string Cost_Code_295, string ExpenseAccount, string VendorID, ref string APIExpenseAccount)
        {
            string retMsg = "";
            //if expAccount is null, then clear out expense account and save it.. 
            // Do expense account required validation in ApproveDocument (12/14/2016)
            //dist.ExpenseAccount = "";
            if (string.IsNullOrEmpty(ExpenseAccount))
            {
                string expAccount = GetGLExpenseAccount(Job, Extra, CostCode, Category, Equipment, Cost_Code_295, ExpenseAccount, adoApVendorRs, ConnectionID);
                APIExpenseAccount = expAccount;
            }
            else
            {
                //validate account
                string sFieldName = "Expense_Account";
                string sAcct = ExpenseAccount;
                string msg = "";
                bool isPaybleAccount = (sFieldName == "Expense_Account" ? false : true);
                if (apDistSettings.GL_Prefix || isPaybleAccount) // apDistSettings.GL_Prefix - to determine if Compnay is visible or not
                {
                    msg = _actualTSCTL.FormatGLBaseAccount(ref sAcct);
                }
                else
                {
                    msg = _actualTSCTL.FormatFullGLAccount(ref sAcct, false);
                }

                if (msg.Contains("Incorrect"))// TRY TO GET DEFAULT ACCOUNT //02/08/2017
                {
                    //retMsg = msg;
                    string expAccount = GetGLExpenseAccount(Job, Extra, CostCode, Category, Equipment, Cost_Code_295, ExpenseAccount, adoApVendorRs, ConnectionID);
                    APIExpenseAccount = expAccount;
                }
                else
                {
                    //if (isPaybleAccount && !string.IsNullOrWhiteSpace(sAcct))
                    //{
                    //    dist.ExpenseAccount = sAcct;
                    //}

                    retMsg = _actualTSCTL.TestForValidCode("Expense_Account", sAcct, true, Job, Extra, CostCode, Category, Equipment,
                    Cost_Code_295, ExpenseAccount, VendorID, apDistSettings, ConnectionID);

                    if (string.IsNullOrEmpty(retMsg) && apDistSettings.JCControls.bolEq_RetrievePrefixFromEq)
                    {
                        if (!string.IsNullOrEmpty(Equipment) && !string.IsNullOrEmpty(Cost_Code_295))
                            retMsg = RetrievePrefixFromEquipmentAndPrefixIsWrong(sAcct, Equipment, Cost_Code_295, ConnectionID);
                    }

                    //update expense account
                    if (string.IsNullOrEmpty(retMsg))
                        APIExpenseAccount = sAcct;

                }
            }

            return retMsg;
        }

        private DocumentEnt GetDocumentEnt(int InvoiceId)
        {
            DocumentEnt doc = new DocumentEnt();

            //get invoice header 
            GetInvoice_Result invHeader = _docDA.GetInvoiceHeaderEnt(InvoiceId);
            if (invHeader != null)
            {

                //get vendor info
                adoApVendorRs = _docDA.GetVendorInfoByDocumentID(invHeader.InvoiceId);
                if (adoApVendorRs != null && adoApVendorRs.Rows.Count > 0)
                {
                    doc.Header.Vendor.Id = adoApVendorRs.Rows[0]["Id"].ToString();
                    doc.Header.DocumentId = invHeader.InvoiceId;
                    doc.Header.DocumentName = invHeader.Invoice;
                    doc.Header.DocumentDesc = invHeader.Description;
                    doc.Header.Amount = invHeader.Amount.HasValue ? Convert.ToDecimal(invHeader.Amount.Value) : 0;
                    doc.Header.TaxAmount = invHeader.Tax.HasValue ? Convert.ToDecimal(invHeader.Tax.Value) : 0;
                    doc.Header.DiscountAmount = invHeader.Discount_Offered.HasValue ? Convert.ToDecimal(invHeader.Discount_Offered.Value) : 0;
                    doc.Header.MiscAmount = invHeader.Misc_Deduction.HasValue ? Convert.ToDecimal(invHeader.Misc_Deduction.Value) : 0;
                    doc.Header.ReceivedDate = invHeader.Date_Received.HasValue ? invHeader.Date_Received.Value : (DateTime?)null;
                    doc.Header.DiscountDate = invHeader.Discount_Date.HasValue ? invHeader.Discount_Date.Value : (DateTime?)null;
                    doc.Header.PaymentDate = invHeader.Payment_Date.HasValue ? invHeader.Payment_Date.Value : (DateTime?)null;
                    doc.Header.InvoiceDate = invHeader.Invoice_Date.Value;
                    doc.Header.AccountingDate = invHeader.Accounting_Date.Value;
                    doc.Header.CurrentActionLevel = invHeader.CurrentActionLevel.HasValue ? invHeader.CurrentActionLevel.Value : 0;
                    doc.Header.HeaderStatus = "U";
                }

                //get distribution info
                List<GetInvoiceDistributions_Result> invDistEntList = _docDA.GetInvoiceDistributionEnt(InvoiceId);
                foreach (var invDist in invDistEntList)
                {
                    DocDistribution dist = new DocDistribution();
                    dist.InvoiceID = invDist.InvoiceId;
                    dist.DistSeq = invDist.DistSeq;
                    //dist.Deleted =
                    dist.Description = invDist.Description;
                    dist.Amount = invDist.Amount.HasValue ? Convert.ToDecimal(invDist.Amount.Value) : 0;
                    dist.Tax = invDist.Tax.HasValue ? Convert.ToDecimal(invDist.Tax.Value) : 0;
                    //dist.PreTax = invDist.Amount.HasValue ? Convert.ToDecimal(invDist.Amount.Value) : 0;
                    dist.Amount = invDist.Amount.HasValue ? Convert.ToDecimal(invDist.Amount.Value) : 0;
                    dist.TaxLiability = invDist.Tax_Liability.HasValue ? Convert.ToDecimal(invDist.Tax_Liability.Value) : 0;
                    dist.DiscountOffered = invDist.Discount_Offered.HasValue ? Convert.ToDecimal(invDist.Discount_Offered.Value) : 0;
                    dist.Retainage = invDist.Retainage.HasValue ? Convert.ToDecimal(invDist.Retainage.Value) : 0;
                    dist.MiscDeduction = invDist.Misc_Deduction.HasValue ? Convert.ToDecimal(invDist.Misc_Deduction.Value) : 0;
                    dist.MiscDeduction2Percent = invDist.Misc_Deduction2_Percent.HasValue ? Convert.ToDecimal(invDist.Misc_Deduction2_Percent.Value) : 0;
                    dist.Exempt_1099 = invDist.Exempt_1099.HasValue ? invDist.Exempt_1099.Value : false;
                    dist.ExpenseAccount = invDist.Expense_Account;
                    dist.AccountsPayableAccount = invDist.Accounts_Payable_Account;
                    dist.Commitment_Line_Item = invDist.Commitment_Line_Item.HasValue ? invDist.Commitment_Line_Item.Value : 0;
                    //dist.Commitment --- HH2 not passing commitment when saves document
                    dist.Units = invDist.Units.HasValue ? Convert.ToDecimal(invDist.Units.Value) : 0;
                    dist.Unit_Cost = invDist.Unit_Cost.HasValue ? Convert.ToDecimal(invDist.Unit_Cost.Value) : 0;
                    dist.Misc_Entry_Units_1 = invDist.Misc_Entry_Units_1.HasValue ? Convert.ToDecimal(invDist.Misc_Entry_Units_1.Value) : 0;
                    dist.Misc_Entry_Units_2 = invDist.Misc_Entry_Units_2.HasValue ? Convert.ToDecimal(invDist.Misc_Entry_Units_2.Value) : 0;


                    dist.MeterOdometer = invDist.MeterOdometer.HasValue ? Convert.ToDecimal(invDist.MeterOdometer.Value) : 0;
                    dist.PM_Lease_Revision_Num = invDist.PM_Lease_Revision_Num.HasValue ? invDist.PM_Lease_Revision_Num.Value : dist.PM_Lease_Revision_Num;
                    dist.PM_Charge_Date = invDist.PM_Charge_Date.HasValue ? invDist.PM_Charge_Date.Value : dist.PM_Charge_Date;
                    dist.PM_Markup_Percent = invDist.PM_Markup_Percent.HasValue ? Convert.ToDecimal(invDist.PM_Markup_Percent.Value) : 0;
                    dist.PM_Markup_Amount = invDist.PM_Markup_Amount.HasValue ? Convert.ToDecimal(invDist.PM_Markup_Amount.Value) : 0;
                    dist.Date_Stamp = invDist.Date_Stamp.HasValue ? invDist.Date_Stamp.Value : dist.Date_Stamp;
                    dist.DistStatus = "U";

                    doc.Distributions.Add(dist);

                }
            }

            return doc;
        }

        private void SetDocumentAmounts(DocumentEnt doc)
        {
            decimal dDetTaxTot = 0;

            VerifyDiscountAmounts(doc);

            if (apDistSettings.Tax && apDistSettings.Amount_Desc.ToUpper().Equals("PRE-TAX"))
            {
                decimal outPreTaxAmount = 0;
                decimal outTaxAmount = 0;
               
                foreach (var dist in doc.Distributions)
                {
                    if (!string.IsNullOrEmpty(dist.DistStatus) && !dist.DistStatus.ToUpper().Equals("D")) // loop through non deleted dist rows to determin expense account
                    {
                        outPreTaxAmount = 0;
                        outTaxAmount = 0;
                        if (dist.Amount != null)
                            decimal.TryParse(dist.Amount.ToString(), out outPreTaxAmount);

                        if (dist.Tax!= null)
                            decimal.TryParse(dist.Tax.ToString(), out outTaxAmount);
                        dist.Amount = Convert.ToDecimal(outPreTaxAmount + outTaxAmount);
                        dDetTaxTot += outTaxAmount;
                    }
                }
            }

            if( apInvSettings.Amount_Desc.ToUpper().Equals("AMOUNT") && apInvSettings.Tax && doc.Header.TaxAmount == 0)
                doc.Header.TaxAmount = dDetTaxTot;
            else if (apInvSettings.Amount_Desc.ToUpper().Equals("PRE-TAX"))
            {
                doc.Header.TaxAmount = dDetTaxTot;
                doc.Header.Amount += dDetTaxTot;
            }
        }

        private bool VerifyDiscountAmounts(DocumentEnt doc)
        {
            decimal dDistDiscTot = 0;
            decimal dDiscPct = 0;
            decimal dDiscAmt = 0;

            //if a discount is being calculated, it is entered both for
            //Invoices and distributions regardless of whether the column is visible
            //we have to reconcile the amounts here to make certain the all amounts agree
            dDistDiscTot = 0;
            if (!apInvSettings.Discount && !apDistSettings.Discount)
                return false;


            foreach (var dist in doc.Distributions)
            {
                dDistDiscTot = dDistDiscTot + Convert.ToDecimal(CommonFunctions.TestNullNumber(dist.DiscountOffered));
            }

            if (dDistDiscTot == Convert.ToDecimal(CommonFunctions.TestNullNumber(doc.Header.DiscountAmount)))
                return true;
            

            if (Convert.ToDecimal(CommonFunctions.TestNullNumber((doc.Header.Amount))) == 0)
            {
                doc.Header.DiscountAmount = 0;
                foreach (var dist in doc.Distributions)
                {
                    dist.DiscountOffered = 0;
                }
                return true;
            }

            //the choices are either discounts visible just on detail grids or just on invoice grid
            if (apInvSettings.Discount)
            {
                dDistDiscTot = Convert.ToDecimal(CommonFunctions.TestNullNumber(doc.Header.DiscountAmount));
                dDiscPct = dDistDiscTot / Convert.ToDecimal(CommonFunctions.TestNullNumber(doc.Header.Amount));

                foreach (var dist in doc.Distributions)
                {
                    dDiscAmt = System.Math.Round(dDiscPct * Convert.ToDecimal(CommonFunctions.TestNullNumber(dist.Amount)), 2);
                    if (Convert.ToDecimal(CommonFunctions.TestNullNumber((dist.DiscountOffered))) != dDiscAmt)
                        dist.DiscountOffered = dDiscAmt;
                    dDistDiscTot = dDistDiscTot - System.Math.Round(Convert.ToDecimal(CommonFunctions.TestNullNumber(dist.DiscountOffered)), 2);
                }
            }
            else
            {
                dDistDiscTot = 0;
                foreach (var dist in doc.Distributions)
                {
                    dDistDiscTot = dDistDiscTot + Convert.ToDecimal(CommonFunctions.TestNullNumber(dist.DiscountOffered));
                }
                if (Convert.ToDecimal(CommonFunctions.TestNullNumber((doc.Header.Amount))) != dDistDiscTot)
                    doc.Header.DiscountAmount = dDistDiscTot;

            }
            return true;
        }
        #endregion

        #region Document Validataion

        private string Init(DocumentEnt doc)
        {
            string retMsg = "";
            try
            {
                if (doc.Header == null)
                    retMsg = FormatReturnMessage("ERROR", "Document must have at least one invoice line", 0).Message;

                if (string.IsNullOrEmpty(retMsg))
                {
                    // log document
                    LogDocToXML(doc);

                    ConnectionID = _docDA.GetConnectionIDByDocumentID(doc.Header.DocumentId);
                    if (ConnectionID == 0)
                        retMsg = FormatReturnMessage("ERROR", "Invalid Document. Cannot find TimberScan ConnectionId", doc.Header.DocumentId).Message;

                    //set user info
                    if (string.IsNullOrEmpty(retMsg))
                    {
                        this.SetUserInfo(doc.UserID);
                        if (string.IsNullOrEmpty(this.LoginID))
                            retMsg = FormatReturnMessage("ERROR", "Invalid UserID. Cannon find Timberscan user", doc.Header.DocumentId).Message;
                    }

                    if (string.IsNullOrEmpty(retMsg))
                    {
                        if (string.IsNullOrEmpty(doc.Header.Vendor.Id))
                        {
                            retMsg = FormatReturnMessage("ERROR", "Vendor ID is required.", doc.Header.DocumentId).Message;
                        }
                        else
                        {
                            adoApVendorRs = _docDA.GetVendorInfoByVendorID(doc.Header.Vendor.Id, ConnectionID);
                            if (adoApVendorRs == null || adoApVendorRs.Rows.Count == 0)
                                retMsg = FormatReturnMessage("ERROR", "Invalid VendorID. Cannot find valid vendor.", doc.Header.DocumentId).Message;
                        }
                    }

                    if (string.IsNullOrEmpty(retMsg))
                        retMsg = InitTimberLineSettings(ConnectionID).Message; // init timberline stuff
                }
            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                _docDA.LogError("CoreAPIDocumentSvc", "Init", ex.Message, innerExMsg, ex.StackTrace, "");
                retMsg = FormatReturnMessage("EXCEPTION", ex.Message + "  --- " + innerExMsg, doc.Header.DocumentId).Message;
            }
            return retMsg;
        }
               

        /// <summary>
        /// To make sure document is in approval and not deleted, put on hold
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private string ValidateInvoiceStatus(DocumentEnt doc)
        {
            string retMsg = "";
            try
            {
                retMsg = _docDA.ValidateInvoice(doc.Header.DocumentId, doc.ApproveLevel, doc.UserID); //  validate invoice status
            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                _docDA.LogError("CoreAPIDocumentSvc", "ValidateInvoiceStatus", ex.Message, innerExMsg, ex.StackTrace, "");
                retMsg = FormatReturnMessage("EXCEPTION", ex.Message + "  --- " + innerExMsg, doc.Header.DocumentId).Message;
            }
            if (!string.IsNullOrEmpty(retMsg))
                retMsg = FormatReturnMessage("ERROR", retMsg, doc.Header.DocumentId).Message;
            return retMsg;
        }

        private string ValidateInvoiceToSave(DocumentEnt doc)
        {
            string retMsg = "", msg = "";
            
            //basic validation
            retMsg = ValidateInvoiceStatus(doc); // to make sure document is still in approval mode. (has not been deleted, put on hold etc)
            
            //if (string.IsNullOrEmpty(retMsg) && doc.Header.HeaderStatus.Equals("U"))
            //{
            //    msg = ValidateInvoiceHeader(doc); // validate invoice header
            //    if (!string.IsNullOrEmpty(msg))
            //        retMsg += msg;
            //}

            //msg = "";
            //msg = ValidateInvoiceDistribution(doc); // validate invoice distribution
            //if (!string.IsNullOrEmpty(msg))
            //    retMsg += msg;

            //if (!string.IsNullOrEmpty(retMsg))
            //{
            //    if (retMsg.ToUpper().Contains("EXCEPTION"))
            //        retMsg = FormatReturnMessage("EXCEPTION", retMsg, doc.Header.DocumentId).Message;
            //    else
            //        retMsg = FormatReturnMessage("ERROR", retMsg, doc.Header.DocumentId).Message;
            //}
            return retMsg;
        }

        private string ValidateInvoiceToApprove(int InvoiceId, int ApprovalLevel, int UserID)
        {
            string retMsg = "";

            //DocumentEnt doc = new DocumentEnt();

            //if (string.IsNullOrEmpty(retMsg)) // validate invoice header
            //    retMsg = ValidateInvoiceHeader(doc);

            if (string.IsNullOrEmpty(retMsg)) //  validate invoice status
                retMsg = _docDA.ValidateInvoice(InvoiceId,ApprovalLevel, UserID);


            //if (string.IsNullOrEmpty(retMsg)) // validate invoice distribution
            //    retMsg = ValidateInvoiceDistribution(doc);

            if (!string.IsNullOrEmpty(retMsg))
            {
                if (retMsg.ToUpper().Contains("EXCEPTION"))
                    retMsg = FormatReturnMessage("EXCEPTION", retMsg, InvoiceId).Message;
                else
                    retMsg = FormatReturnMessage("ERROR", retMsg, InvoiceId).Message;
            }
            return retMsg;
        }

        private string ValidateInvoiceHeader(DocumentEnt doc)
        {
            string retMsg = "";
            try
            {
                if (string.IsNullOrEmpty(retMsg))
                {
                    DateTime dtTime = DateTime.MinValue;

                    if (adoApVendorRs.Columns.Contains("Inactive"))
                    {
                        if (Convert.ToBoolean(adoApVendorRs.Rows[0]["Inactive"].ToString()))
                            retMsg += "Vendor is inactive." + "#";
                        else if (adoApVendorRs.Rows[0]["Type"].ToString().ToUpper().Equals("CREDIT CARD VENDOR"))
                            retMsg += "Credit Card vendors cannot be entered." + "#";
                    }

                    if (string.IsNullOrEmpty(doc.Header.DocumentName))
                        retMsg += "An Invoice Number is required." + "#";

                    if (!apInvSettings.Accounting_Date_Visible)
                        doc.Header.AccountingDate = doc.Header.InvoiceDate;
                    else if (!IsValiDate(doc.Header.AccountingDate))
                        retMsg += "Accounting date is required." + "#";

                    if (!IsValiDate(doc.Header.InvoiceDate))
                        retMsg += "Invoice date is required." + "#";
                    if (apInvSettings.Payment_Date_Required)
                    {
                        if (!doc.Header.PaymentDate.HasValue || !IsValiDate(doc.Header.PaymentDate.Value))
                            retMsg += "Payment Date is required." + "#";
                    }
                    if (apInvSettings.Date_Received_Required)
                    {
                        if (!doc.Header.ReceivedDate.HasValue || !IsValiDate(doc.Header.ReceivedDate.Value))
                            retMsg += "Date Received is required." + "#";
                    }

                    if (ValidateTaxAmounts(doc) == false)
                    {
                        retMsg += "The total tax distributed is not equal to the invoice tax amount." + "#";
                    }

                    // test for existing invoice

                    if (string.IsNullOrEmpty(retMsg) && _docDA.TestForExistingInvoice(ConnectionID, doc.Header.DocumentId, doc.Header.DocumentName, doc.Header.Vendor.Id))
                    {
                        retMsg += "Duplicate Invoice. Invoice " + doc.Header.DocumentName + " already exists for vendor " + VendorName + "#";
                    }
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(retMsg))
                    _docDA.LogError("DocumentSvc", "ValidateInvoiceHeader", "Invoice Header Validation Error", retMsg, "", this.LoginID);

                retMsg = "Exception occurred during invoice header validation";

                _docDA.LogError("DocumentSvc", "ValidateInvoiceHeader", ex.Message, string.IsNullOrEmpty(ex.InnerException.Message) ? "" : ex.InnerException.Message, ex.StackTrace, this.LoginID);

            }

            return retMsg;
        }

        private string ValidateInvoiceDistribution(DocumentEnt doc)
        {
            string retMsg = "";
            bool bCommitOK = true, taxAllocationsOk = true;
            //bool detailLinesOk = false, detailLineDeleted = false;
            int lRow = 0, noJobOrGLCount = 0, noJobOrGLCount2 = 0;
            double dDetTot = 0, dDetTaxTot = 0, dDistTot = 0, dRetainTot = 0;
            double chkRetainDetAmt = 0, chkTaxAmt = 0;

            try
            {
                if (doc != null && doc.Distributions.Count() == 0)
                    retMsg = retMsg += "Invoice must have at least 1 distribution." + "#";
                else
                {
                    retMsg = ValidateInvoiceDistBasic(doc);
                    if (string.IsNullOrEmpty(retMsg))
                    {
                        foreach (var dist in doc.Distributions)
                        {

                            if (apDistSettings.Unit_Cost)
                            {
                                decimal commitUnitCost = 0M;

                                if (!this.ValidateUnitCost(dist.Unit_Cost, dist.Commitment_Line_Item, dist.Commitment, ref commitUnitCost))
                                {
                                    retMsg = retMsg += string.Format("Incorrect Unit Price. Commitment Unit Price : {0} Distribution Unit Price : {1}", commitUnitCost, dist.Unit_Cost) + "#";
                                    break;
                                }
                            }
                            if (apDistSettings.Commitment && apDistSettings.Retainage)
                            {
                                dDistTot += Convert.ToDouble(CommonFunctions.TestNullNumber(dist.Amount));
                                dRetainTot += Convert.ToDouble(CommonFunctions.TestNullNumber(dist.Retainage));
                            }
                            if (apDistSettings.Retainage)
                            {
                                chkRetainDetAmt += Convert.ToDouble(CommonFunctions.TestNullNumber(dist.Retainage));
                                if (apDistSettings.Tax)
                                    chkTaxAmt += Convert.ToDouble(CommonFunctions.TestNullNumber(dist.Tax));
                            }

                            if (!string.IsNullOrEmpty(dist.DistStatus) && !dist.DistStatus.ToUpper().Equals("D")) // only updated or new distribution
                            {
                                string msg = "";

                                if (apDistSettings.Commitment && !ValidateCommitment(dist, ref msg))  //TODO: bCommitOK Validation
                                {
                                    bCommitOK = false;
                                    break;
                                }
                                else if (!ValidateTaxAllocation(dist, ref msg)) //TODO: Tax Allocation Validation
                                {
                                    taxAllocationsOk = false;
                                    break;
                                }
                                else if (ValidateJobAndEntry(dist)) // JOB and GL Entry Validation
                                {
                                    ++noJobOrGLCount;
                                    dDetTot = Math.Round(dDetTot + Math.Round(Convert.ToDouble(dist.Amount), 4), 4);
                                    dDetTaxTot = Math.Round(dDetTaxTot + Math.Round(Convert.ToDouble(dist.Tax), 2), 2);
                                }
                                else
                                {
                                    dDetTot = Math.Round(dDetTot + Math.Round(Convert.ToDouble(dist.Amount), 4), 4);
                                    dDetTaxTot = Math.Round(dDetTaxTot + Math.Round(Convert.ToDouble(dist.Tax), 2), 2);
                                }
                            }
                            if (this.NoJobAndNoGLEntry(dist))
                                ++noJobOrGLCount2;      //without any other conditions, count the number of rows witout a job or gl entry
                        }


                        dDetTot = Math.Round(dDetTot, 2);
                        if (noJobOrGLCount2 == doc.Distributions.Count())
                            noJobOrGLEntry = true;//we will use this in the accept routine to know if we need to use basic routing

                        //if (!bCommitOK)
                        //{
                        //    detailLinesOk = false;
                        //}
                        if (apDistSettings.Retainage && Math.Sign(Math.Round(dDistTot, 2)) != Math.Sign(Math.Round(dRetainTot, 2)) && Math.Sign(Math.Round(dRetainTot, 2)) != 0)
                        {
                            if (dRetainTot < 0)
                                retMsg = retMsg += "Amount fields must have consitent signage. Amount is positive and retainage is negative" + "#";
                            else
                                retMsg = retMsg += "Amount fields must have consitent signage. Amount is Negative and retainage is positive" + "#";
                            //detailLinesOk = false;
                        }
                        if (noJobOrGLCount == doc.Distributions.Count())
                        {
                            if (apDistSettings.Job && apDistSettings.GL_Prefix)
                                retMsg = retMsg += "Either a Job or a company (GL Prefix) must be entered" + "#";
                            else if (apDistSettings.Job)
                                retMsg = retMsg += "A Job or an Account must be entered" + "#";
                            else
                                retMsg = retMsg += "An Account must be entered" + "#";
                        }
                        if (taxAllocationsOk)
                        {
                            string sMsg = "";
                            if (!ValidateInvoiceInBalance(doc, dDetTot, dDetTaxTot, ref sMsg))
                            {
                                retMsg += sMsg;
                                //detailLinesOk = false;
                            }

                        }
                    } // end DistributionBasicValidation
                }// End Else
                //if (string.IsNullOrEmpty(retMsg))
                //    detailLinesOk = true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(retMsg))
                    _docDA.LogError("DocumentSvc", "ValidateInvoiceDistribution", "Invoice Distribution Validation Error", retMsg, "", this.LoginID);

                retMsg = "Exception occurred during invoice distribution validation";

                _docDA.LogError("DocumentSvc", "ValidateInvoiceDistribution", ex.Message, string.IsNullOrEmpty(ex.InnerException.Message) ? "" : ex.InnerException.Message, ex.StackTrace, this.LoginID);

                //detailLinesOk = false;
            }
            return retMsg;
        }

        private string ValidateInvoiceDistBasic(DocumentEnt doc)
        {
            string retDistValidationMsg = "", retMsg = "", msg = "";
            bool isValidDist = true;

            foreach (var dist in doc.Distributions)
            {
                retMsg = ""; msg = ""; isValidDist = true;
                if (apDistSettings.Commitment && !string.IsNullOrEmpty(dist.Commitment))
                {
                    msg = "";
                    msg = this._actualTSCTL.TestForValidCode("Commitment", dist.Commitment, false, dist.Job, dist.Extra, dist.CostCode,
                        dist.Category, dist.Equipment, dist.Cost_Code_295, dist.ExpenseAccount, doc.Header.Vendor.Id, this.apDistSettings, this.ConnectionID);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        retMsg += msg + "#";
                        isValidDist = false;
                    }
                }
                if (apDistSettings.Extra && !string.IsNullOrEmpty(dist.Extra))
                {
                    msg = "";
                    msg = this._actualTSCTL.TestForValidCode("Extra", dist.Commitment, false, dist.Job, dist.Extra, dist.CostCode,
                        dist.Category, dist.Equipment, dist.Cost_Code_295, dist.ExpenseAccount, doc.Header.Vendor.Id, this.apDistSettings, this.ConnectionID);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        retMsg += msg + "#";
                        isValidDist = false;
                    }
                }
                if (apDistSettings.Standard_Item && !string.IsNullOrEmpty(dist.Standard_Item))
                {
                    msg = "";
                    msg = this._actualTSCTL.TestForValidCode("Standard_Item", dist.Commitment, false, dist.Job, dist.Extra, dist.CostCode,
                        dist.Category, dist.Equipment, dist.Cost_Code_295, dist.ExpenseAccount, doc.Header.Vendor.Id, this.apDistSettings, this.ConnectionID);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        retMsg += msg + "#";
                        isValidDist = false;
                    }
                    
                }
                if (apDistSettings.Job && !string.IsNullOrEmpty(dist.Job))
                {
                    msg = "";
                    string strJob = dist.Job;
                    msg = _actualTSCTL.FormatJob(ref strJob);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        retMsg += msg + "#";
                        isValidDist = false;
                    }
                    else
                    {
                        msg = this._actualTSCTL.TestForValidCode("Job", dist.Commitment, false, dist.Job, dist.Extra, dist.CostCode,
                        dist.Category, dist.Equipment, dist.Cost_Code_295, dist.ExpenseAccount, doc.Header.Vendor.Id, this.apDistSettings, this.ConnectionID);
                        if (!string.IsNullOrEmpty(msg) && msg.Contains("not found"))
                        {
                            msg = "Job " + dist.Job + " is not set up.";
                            retMsg += msg + "#";
                            isValidDist = false;
                        }
                    }
                }
                if (apDistSettings.Cost_Code && !string.IsNullOrEmpty(dist.CostCode))
                {
                    msg = "";
                    string sNewCostCode = dist.CostCode;
                    msg = _actualTSCTL.FormatCostCode(ref sNewCostCode);

                    if (!string.IsNullOrEmpty(msg) && !msg.Contains("Incorrect"))
                    {
                        if (IsNewCostCodeAGroupCostCode(sNewCostCode))
                        {
                            msg = "Creation of group cost codes is not allowed.";
                        }
                        else
                        {
                            msg = this._actualTSCTL.TestForValidCode("Cost_Code", dist.Commitment, false, dist.Job, dist.Extra, dist.CostCode,
                            dist.Category, dist.Equipment, dist.Cost_Code_295, dist.ExpenseAccount, doc.Header.Vendor.Id, this.apDistSettings, this.ConnectionID);
                            if (!string.IsNullOrEmpty(msg))
                                msg = "CostCode " + dist.CostCode + " is not set up.";
                        }
                    }
                    if (!string.IsNullOrEmpty(msg))
                    {
                        retMsg += msg + "#";
                        isValidDist = false;
                    }
                }
                if (apDistSettings.Category && !string.IsNullOrEmpty(dist.Job) && !string.IsNullOrEmpty(dist.CostCode))
                {
                    msg = "";
                    string sNewCategory = dist.Category;

                    msg = this._actualTSCTL.TestForValidCode("Category", dist.Commitment, false, dist.Job, dist.Extra, dist.CostCode,
                        dist.Category, dist.Equipment, dist.Cost_Code_295, dist.ExpenseAccount, doc.Header.Vendor.Id, this.apDistSettings, this.ConnectionID);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        retMsg += "Category " + dist.Category + " is not set up for Job " + dist.Job + ", Cost Code " + dist.CostCode + "." + "#";
                        isValidDist = false;
                    }
                }

                if (apDistSettings.Units && System.Text.RegularExpressions.Regex.IsMatch(dist.Units.ToString(), @"\.\d\d\d\d\d"))
                {
                    retMsg += "Only 4 digits to the right of the decimal place are allowed." + "#";
                    isValidDist = false;
                }

                if (apDistSettings.Unit_Cost)
                {
                    msg = "";
                    decimal commitUnitCost = 0M;
                    if (!this.ValidateUnitCost(dist.Unit_Cost, dist.Commitment_Line_Item, dist.Commitment, ref commitUnitCost))
                    {
                        retMsg += string.Format("Incorrect Unit Price. Commitment Unit Price : {0} Distribution Unit Price : {1}", commitUnitCost, dist.Unit_Cost) + "#";
                        isValidDist = false;
                    }
                }
                if (apDistSettings.Retainage)
                {
                    msg = "";
                    double dblNewDistAmt = Convert.ToDouble(dist.Retainage);
                    if (dblNewDistAmt != 0)
                    {
                        double compToRtgDistAmount = Convert.ToDouble(CommonFunctions.TestNullNumber(dist.Amount));
                        if (this.apDistSettings.Amount_Desc.ToUpper() == "Pre-Tax".ToUpper())
                            compToRtgDistAmount = compToRtgDistAmount + Convert.ToDouble(CommonFunctions.TestNullNumber(dist.Tax));

                        if ((dblNewDistAmt < 0 && compToRtgDistAmount > 0) || (dblNewDistAmt < 0 && compToRtgDistAmount == 0))
                            msg = "The retainage must be positive.";
                        else if (dblNewDistAmt > 0 && compToRtgDistAmount < 0)
                            msg = "The retainage must be negative.";
                        else if (dblNewDistAmt > 0 && compToRtgDistAmount == 0)
                            msg = "The retainage amount ($" + dblNewDistAmt.ToString().Trim() + ") cannot be more than the amount over which retainage is being spread ($" + compToRtgDistAmount.ToString().Trim() + ").";
                        else if ((dblNewDistAmt > 0 && compToRtgDistAmount > 0) && (dblNewDistAmt > compToRtgDistAmount))
                            msg = "The retainage amount ($" + dblNewDistAmt.ToString().Trim() + ") cannot be more than the amount over which retainage is being spread ($" + compToRtgDistAmount.ToString().Trim() + ").";
                        else if ((dblNewDistAmt < 0 && compToRtgDistAmount < 0) && (Math.Abs(dblNewDistAmt) > Math.Abs(compToRtgDistAmount)))
                            msg = "The retainage amount ($" + dblNewDistAmt.ToString().Trim() + ") must be more than the amount over which retainage is being spread ($" + compToRtgDistAmount.ToString().Trim() + ").";
                    }
                    if (!string.IsNullOrEmpty(msg))
                    {
                        retMsg += msg + "#";
                        isValidDist = false;
                    }
                }
                if (apDistSettings.Misc_Ded_2)
                {
                    double dblNewDistAmt = Convert.ToDouble(dist.MiscDeduction2Percent);
                    if (dblNewDistAmt > 100)
                    {
                        retMsg += "Misc Deduction2 Percentage cannot be greater than or equal to 100." + " #";
                        isValidDist = false;
                    }
                }

                if (apDistSettings.Tax_Group && !string.IsNullOrEmpty(dist.TaxGroup) && dist.TaxGroup.Length > 6)
                {
                    retMsg += "Tax Group is too long (max 6 characters)" + "#";
                    isValidDist = false;
                }

                if (apDistSettings.Draw && !string.IsNullOrEmpty(dist.Draw) && dist.Draw.Length > 15)
                {
                    retMsg += "Draw is too long (max 15 characters)" + "#";
                    isValidDist = false;
                }

                if (apDistSettings.Authorization && !string.IsNullOrEmpty(dist.Authorization) && dist.Authorization.Length > 10)
                {
                    retMsg += "Authorization is too long (max 10 characters)" + "#";
                    isValidDist = false;
                }

                if (apDistSettings.Equipment && !string.IsNullOrEmpty(dist.Equipment))
                {
                    string sNewText = dist.Equipment;
                    msg = "";
                    msg = _actualTSCTL.FormatEquipment(ref sNewText);
                    if (!string.IsNullOrEmpty(msg) && msg.Contains("Incorrect"))
                    {
                        retMsg += msg + "#";
                        isValidDist = false;
                    }
                    else
                    {
                        msg = this._actualTSCTL.TestForValidCode("Equipment", dist.Commitment, false, dist.Job, dist.Extra, dist.CostCode,
                               dist.Category, dist.Equipment, dist.Cost_Code_295, dist.ExpenseAccount, doc.Header.Vendor.Id, this.apDistSettings, this.ConnectionID);
                        if (!string.IsNullOrEmpty(msg))
                        {
                            if (!msg.Contains("Inactive"))
                                msg = "Equipment " + dist.Equipment + " is not setup up";

                            retMsg += msg + "#";
                            isValidDist = false;
                        }
                    }
                }

                if (apDistSettings.Equipment && apDistSettings.Eq_Cst_Cd && !string.IsNullOrEmpty(dist.Equipment) && string.IsNullOrEmpty(dist.Cost_Code_295))
                {
                    retMsg += "Equipment Cost Code is required when Equipment is used." + "#";
                    isValidDist = false;
                }
                else if (apDistSettings.Equipment && apDistSettings.Eq_Cst_Cd && !string.IsNullOrEmpty(dist.Cost_Code_295))
                {
                    string sNewText = dist.Cost_Code_295;
                    msg = "";
                    msg = _actualTSCTL.FormatEquipment(ref sNewText);
                    if (!string.IsNullOrEmpty(msg) && msg.Contains("Incorrect"))
                    {
                        retMsg += msg + "#";
                        isValidDist = false;
                    }
                    else
                    {
                        msg = this._actualTSCTL.TestForValidCode("Cost_Code_295", dist.Commitment, false, dist.Job, dist.Extra, dist.CostCode,
                              dist.Category, dist.Equipment, dist.Cost_Code_295, dist.ExpenseAccount, doc.Header.Vendor.Id, this.apDistSettings, this.ConnectionID);
                        if (!string.IsNullOrEmpty(msg))
                        {
                            if (!msg.Contains("Inactive"))
                                msg = "Equipment Cost Code " + dist.Cost_Code_295 + " is not setup up";

                            retMsg += msg + "#";
                            isValidDist = false;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(retMsg))
                {
                    retMsg = "Dist Seq " + dist.DistSeq.ToString() + " - " + retMsg + "#";
                    retDistValidationMsg += retMsg;
                }

            }
            return retDistValidationMsg;
        }

        private bool ValidateUnitCost(decimal unitCost, int commitmentLineItem, string commitment, ref decimal commitUnitPrice)
        {
            bool isValid = true;
            try
            {
                string commitKey = commitment.ToString() + "|" + string.Format("00000", commitmentLineItem);

                if (!TSSysSettings.AllowOverrideUnitCost)
                {
                    var CommitmentItemsList = (from com in _docDA.GetCommitmentItems(ConnectionID, commitment)
                                               select new
                                               {
                                                   Key = com.Commitment + "|" + com.ItemNumber.Value.ToString(),
                                                   UnitCost = com.UnitCost
                                               }).ToList();


                    if (CommitmentItemsList.Any(x => x.Key.ToUpper().Equals(commitKey.ToUpper())))
                    {
                        decimal xUnitcost = CommitmentItemsList.Where(x => x.Key.ToUpper().Equals(commitKey.ToUpper())).FirstOrDefault().UnitCost.Value;
                        if (unitCost.CompareTo(xUnitcost) != 0)
                        {
                            commitUnitPrice = xUnitcost;
                            isValid = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _docDA.LogError("DocumentSvc", "ValidateUnitCost", ex.Message, string.IsNullOrEmpty(ex.InnerException.Message) ? "" : ex.InnerException.Message, ex.StackTrace, this.LoginID);
                isValid = false;
                throw ex;
            }

            return isValid;
        }

        private bool ValidateCommitment(DocDistribution dist, ref string msg)
        {
            bool isValid = true;

            //try
            //{

            //}
            //catch (Exception ex)
            //{
            //    _docDA.LogError("DocumentSvc", "ValidateCommitment", ex.Message, string.IsNullOrEmpty(ex.InnerException.Message) ? "" : ex.InnerException.Message, ex.StackTrace, this.LoginID);
            //    isValid = false;
            //}

            return isValid;
        }

        private bool ValidateTaxAllocation(DocDistribution dist, ref string msg)
        {
            bool isValid = true;

            //try
            //{

            //}
            //catch (Exception ex)
            //{
            //    _docDA.LogError("DocumentSvc", "ValidateTaxAllocation", ex.Message, string.IsNullOrEmpty(ex.InnerException.Message) ? "" : ex.InnerException.Message, ex.StackTrace, this.LoginID);
            //    isValid = false;
            //}

            return isValid;
        }

        private bool ValidateTaxAmounts(DocumentEnt doc)
        {

            try
            {
                if (!apInvSettings.Tax && !apDistSettings.Tax)
                    return true;

                if (!apInvSettings.Tax)
                    return true;

                double dInvTaxAmt = 0;
                double dDistTaxAmt = 0;

                if (doc.Header.TaxAmount.HasValue)
                    dInvTaxAmt = Convert.ToDouble(System.Math.Round(doc.Header.TaxAmount.Value, 2));

                if (apDistSettings.Tax)
                {
                    foreach (var dist in doc.Distributions)
                    {
                        dDistTaxAmt += Convert.ToDouble(System.Math.Round(dist.Tax, 2));
                    }
                }

                if (apInvSettings.Amount_Desc.ToUpper().Equals("AMOUNT") && apInvSettings.Tax && dInvTaxAmt == 0)
                    dInvTaxAmt = dDistTaxAmt;
                else if (apInvSettings.Amount_Desc.ToUpper().Equals("PRE-TAX"))
                    dInvTaxAmt = dDistTaxAmt;

                if (dInvTaxAmt.Equals(dDistTaxAmt))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                _docDA.LogError("DocumentSvc", "ValidateTaxAmounts", ex.Message, string.IsNullOrEmpty(ex.InnerException.Message) ? "" : ex.InnerException.Message, ex.StackTrace, this.LoginID);
                return false;
                throw ex;
            }

            //return true;
        }

        private bool ValidateInvoiceInBalance(DocumentEnt doc, double DetailsTotal, double DetailsTaxTotal, ref string msg)
        {
            bool isInBalance = true;
            double dInvTaxTot = 0, dInvAmt = 0;
            string sMsg = string.Empty;
            try
            {
                dInvAmt = Convert.ToDouble(doc.Header.Amount);
                dInvTaxTot = Convert.ToDouble(doc.Header.TaxAmount);

                if (!this.apDistSettings.Amount_Desc.ToUpper().Equals("AMOUNT"))
                    DetailsTotal = DetailsTotal + DetailsTaxTotal;
                else if (this.apDistSettings.Amount_Desc.ToUpper().Equals("PRE-TAX"))
                    dInvAmt = dInvAmt + DetailsTaxTotal;

                if (System.Math.Round(dInvAmt, 2) != System.Math.Round(DetailsTotal, 2))
                {
                    msg = "The invoice amount and distribution total amount are not in balance. The invoice amount is " + dInvAmt.ToString("C") + " and the distribution total is " + DetailsTotal.ToString("C") + "#";
                    isInBalance = false;
                }
            }
            catch (Exception ex)
            {
                _docDA.LogError("DocumentSvc", "ValidateInvoiceInBalance", ex.Message, string.IsNullOrEmpty(ex.InnerException.Message) ? "" : ex.InnerException.Message, ex.StackTrace, this.LoginID);
                isInBalance = false;
                throw ex;
            }

            return isInBalance;
        }

        private bool ValidateJobAndEntry(DocDistribution dist)
        {
            bool blnRet = true;
            //if (!this.TSSysSettings.AllowNoJobOrGLEntry)
            //{
            if (NoJobAndNoGLEntry(dist))
                blnRet = false;
            //}
            return blnRet;
        }

        private bool NoJobAndNoGLEntry(DocDistribution dist)
        {
            bool ok = false;
            if (string.IsNullOrEmpty(dist.ExpenseAccount.Trim()) && string.IsNullOrEmpty(dist.Job.Trim()) && NoGLPrefix(dist))
                ok = true;
            return ok;
        }

        private bool NoGLPrefix(DocDistribution dist)
        {
            string glPrefix = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(dist.ExpenseAccount) && _actualTSCTL.GLPrefixALength > 0)
                {
                    glPrefix = this._actualTSCTL.DetermineGLPrefix(dist.ExpenseAccount, ConnectionID);
                }
            }
            catch (Exception ex)
            {

                _docDA.LogError("DocumentSvc", "NoGLPrefix", ex.Message, string.IsNullOrEmpty(ex.InnerException.Message) ? "" : ex.InnerException.Message, ex.StackTrace, this.LoginID);
                throw ex;
            }
            return string.IsNullOrEmpty(glPrefix);
        }

        #endregion

        #region GLAccounts

        //private string GetAccountsPayableAccount(string Commitment, Core.API.TLSettings.Models.ActualTSCTL _actualTSCTL, TLSettings.Models.APDistributionSettings apDistSettings, Int32 ConnectionID)
        private string GetAccountsPayableAccount(string Commitment, Int32 ConnectionID)
        {
            // get payable account from ap controls
            string sGLAcct = "";
            if (string.IsNullOrEmpty(Commitment))
                return GetBaseAccount(apDistSettings.AP_Payable_Account, ConnectionID);
            else
            {
                string sQry = string.Format("SELECT Commitment_Type FROM JCM_MASTER__COMMITMENT WHERE Commitment='{0}'",
                 CommonFunctions.QNAValSt(Commitment.Trim()));

                DataTable aRs = new DataTable();
                aRs = _docDA.GetDataTableTSync(sQry, ConnectionID);
                if (aRs == null || aRs.Rows.Count <= 0)
                {
                    sGLAcct = apDistSettings.AP_Payable_Account;
                }
                else
                {
                    if (aRs.Rows[0]["Commitment_Type"].ToString().ToUpper() == "SUBCONTRACT")
                    {
                        sGLAcct = GetBaseAccount(apDistSettings.Commitment_Payable_Acct, ConnectionID);
                    }
                    else
                    {
                        sGLAcct = GetBaseAccount(apDistSettings.PO_Payable_Account, ConnectionID);
                    }
                }

            }

            return sGLAcct;
        }

        //public string DetermineAPDistGLPrefix(string sJb, string sExt, string sCstCd, string sCat, string sEquip, string sEqCstCode, bool bEquipColVis, DataTable aVndRs, Core.API.TLSettings.Models.ActualTSCTL _actualTSCTL, TLSettings.Models.APDistributionSettings apDistSettings, Int32 ConnectionID) 
        private string DetermineAPDistGLPrefix(string sJb, string sExt, string sCstCd, string sCat, string sEquip, string sEqCstCode, bool bEquipColVis, DataTable aVndRs, Int32 ConnectionID)
        {
            //clsGLRoutines GLRoutinesObject = new clsGLRoutines();
            string sGLPrefix;
            string sQry = "";
            string sGLAccount = "";
            string sDebitAccount;
            string sEquipment;
            int iLen;
            int iPrefixLen;
            string sPrefix;
            DataTable aRs = null;
            string sCostAcctGrp;
            int iSuffixLen = 0;
            int iMinPrefixLen;
            string sPrxLvl = "";
            object objAffected = null;

            string strGlRetrieveMethod = apDistSettings.JCControls.strGlRetrieveMethod;
            string strDefaultCostAcct = apDistSettings.JCControls.strDefaultCostAcct;
            string strCategoryUsage = apDistSettings.JCControls.strCategoryUsage;
            string strPrefixType = _actualTSCTL.GLPrefixABCLength > 0 ? "PrefixC" : (_actualTSCTL.GLPrefixABLength > 0 ? "PrefixB" : "PrefixA");
            //TimberScanDataAccess.DataAcessHelper tLineData = null;

            //1st from Job Cost, then from EQ Cost Debit Account, then from Vendor record
            sGLPrefix = "";

            sDebitAccount = "";
            sEquipment = "";

            bool bMemoCost = false;
            if (!string.IsNullOrEmpty(sEquip) && !string.IsNullOrEmpty(sEqCstCode))
            {
                //bMemoCost = EquipmentMemoCost(sEqCstCode);
                bMemoCost = _docDA.GetEquipmentMemoCost(ConnectionID, sEqCstCode);
            }

            //if (_actualTSCTL.GLPrefixABCLength > 0)
            //{
            //    sPrxLvl = "PrefixC";
            //}
            //else if (_actualTSCTL.GLPrefixABLength > 0) // ERF
            //{
            //    sPrxLvl = "PrefixB";
            //}
            //else if (_actualTSCTL.GLPrefixALength > 0) // ERF
            //{
            //    sPrxLvl = "PrefixA";
            //}
            //else
            //{
            //    return "";
            //}

            //modIniFiles.LogFile"DetermineAPDistGLPrefix"

            sPrefix = "";
            //if (sEquip > "" && sEqCstCode > "") // ERF original
            if (!string.IsNullOrEmpty(sEquip) && !string.IsNullOrEmpty(sEqCstCode) && !bMemoCost) // ERF
            {
                if (!apDistSettings.JCControls.bolEq_RetrievePrefixFromEq)
                {
                    sPrefix = GetExpenseAccountFromEquip(ref sEquip, sEqCstCode, ConnectionID);
                    sPrefix = _actualTSCTL.DetermineGLPrefix(sPrefix, ConnectionID);
                    return sPrefix;
                }

                sQry = "SELECT GL_Prefix FROM EQM_MASTER__EQUIPMENT WHERE Equipment=" + CommonFunctions.QNApos(sEquip);


                aRs = new DataTable();
                aRs = _docDA.GetDataTableTSync(sQry, ConnectionID);

                if (aRs != null && aRs.Rows.Count > 0)
                {

                    sPrefix = string.IsNullOrEmpty(aRs.Rows[0]["GL_Prefix"].ToString().Trim()) ? string.Empty : aRs.Rows[0]["GL_Prefix"].ToString();
                }
                if (string.IsNullOrEmpty(sPrefix))
                {
                    sPrefix = apDistSettings.JCControls.strEq_DefaultPrefix;
                }
                if (!string.IsNullOrEmpty(sPrefix)) // ERF
                {
                    //PrefixWasGottenFromEquipment = true;
                    return sPrefix;
                }
            }

            if (apDistSettings.JCControls.bolJCSystemActive == true)
            {
                if (apDistSettings.JCControls.bolRetrieveAcctPrfxFrmJob == true && !string.IsNullOrEmpty(sJb)) // ERF
                {
                    sQry = "SELECT Cost_Account_Prefix FROM JCM_MASTER__JOB WHERE Job=" + CommonFunctions.QNApos(sJb);
                    aRs = new DataTable();
                    aRs = _docDA.GetDataTableTSync(sQry, ConnectionID);

                    if (aRs == null || aRs.Rows.Count <= 0)
                    {
                        return "";
                    }
                    else
                    {

                        return string.IsNullOrEmpty(aRs.Rows[0]["Cost_Account_Prefix"].ToString().Trim()) ? string.Empty : aRs.Rows[0]["Cost_Account_Prefix"].ToString();
                    }
                }

                //if (sJb > "" && sCat > "" && sCstCd > "" && strGlRetrieveMethod.ToUpper() == "RETRIEVE BY CATEGORY") // ERF original
                if (!string.IsNullOrEmpty(sJb) && !string.IsNullOrEmpty(sCat) && !string.IsNullOrEmpty(sCstCd)
                    && apDistSettings.JCControls.strGlRetrieveMethod.ToUpper() == "RETRIEVE BY CATEGORY") // ERF
                {
                    //If sJb > "" And sCat > "" And sCstCd > "" Then

                    sQry = "SELECT Cost_Account FROM JCM_MASTER__CATEGORY WHERE Job=" + CommonFunctions.QNApos(sJb) + " AND Cost_Code=" + CommonFunctions.QNApos(sCstCd) + " AND Category=" + CommonFunctions.QNApos(sCat);

                    if (!string.IsNullOrEmpty(sExt))
                    {
                        sQry = sQry + " AND Extra=" + CommonFunctions.QNApos(sExt);
                    }
                    else
                    {
                        sQry = sQry + " AND Extra=\'\'";
                    }

                    aRs = new DataTable();
                    aRs = _docDA.GetDataTableTSync(sQry, ConnectionID);

                    if (aRs != null && aRs.Rows.Count > 0)
                    {
                        sGLAccount = string.IsNullOrEmpty(aRs.Rows[0]["Cost_Account"].ToString().Trim()) ? string.Empty : aRs.Rows[0]["Cost_Account"].ToString();
                    }

                    iSuffixLen = _actualTSCTL.GLSuffixLength > 0 ? _actualTSCTL.GLSuffixLength + 1 : 0; // ERF
                    if (!string.IsNullOrEmpty(sGLAccount) &&
                        sGLAccount.Length > _actualTSCTL.GLBaseLength + iSuffixLen) // ERF
                    {
                        return GetEntityIDFromPrefix(sGLAccount, strPrefixType);
                    }
                }

                if (!string.IsNullOrEmpty(sJb) && !string.IsNullOrEmpty(sCat) && !string.IsNullOrEmpty(sCstCd)
                   && apDistSettings.JCControls.strGlRetrieveMethod.ToUpper() == "USE HIERARCHY") // VishA  2/25/16 - retrieve Cost Account using Hierarchy 
                {
                    //search the Category record
                    sQry = "SELECT Cost_Account FROM JCM_MASTER__CATEGORY WHERE Job=" + CommonFunctions.QNApos(sJb) + " AND Cost_Code=" + CommonFunctions.QNApos(sCstCd) + " AND Category=" + CommonFunctions.QNApos(sCat);

                    if (!string.IsNullOrEmpty(sExt))
                    {
                        sQry = sQry + " AND Extra=" + CommonFunctions.QNApos(sExt);
                    }
                    else
                    {
                        sQry = sQry + " AND Extra=\'\'";
                    }

                    aRs = new DataTable();
                    aRs = _docDA.GetDataTableTSync(sQry, ConnectionID);

                    if (aRs != null && aRs.Rows.Count > 0)
                    {
                        sGLAccount = string.IsNullOrEmpty(aRs.Rows[0]["Cost_Account"].ToString().Trim()) ? string.Empty : aRs.Rows[0]["Cost_Account"].ToString();
                    }
                    else
                    {
                        //if record not found use Cost Code record
                        sQry = "SELECT Cost_Account FROM JCM_MASTER__COST_CODE WHERE Job=" + CommonFunctions.QNApos(sJb) + " AND Cost_Code=" +
                       CommonFunctions.QNApos(sCstCd);

                        if (!string.IsNullOrEmpty(sExt))
                        {
                            sQry = sQry + " AND Extra=" + CommonFunctions.QNApos(sExt);
                        }

                        aRs = new DataTable();
                        aRs = _docDA.GetDataTableTSync(sQry, ConnectionID);

                        if (aRs != null && aRs.Rows.Count > 0)
                        {
                            sGLAccount = string.IsNullOrEmpty(aRs.Rows[0]["Cost_Account"].ToString().Trim()) ? string.Empty : aRs.Rows[0]["Cost_Account"].ToString();
                        }
                        else
                        {
                            //if record not found search the Job record
                            sQry = "SELECT Cost_Account_Group, Cost_Account FROM JCM_MASTER__JOB WHERE Job=" + CommonFunctions.QNApos(sJb);
                            aRs = new DataTable();
                            aRs = _docDA.GetDataTableTSync(sQry, ConnectionID);

                            if (aRs != null && aRs.Rows.Count > 0)
                            {
                                sGLAccount = string.IsNullOrEmpty(aRs.Rows[0]["Cost_Account"].ToString().Trim()) ? string.Empty : aRs.Rows[0]["Cost_Account"].ToString();
                            }
                            else
                            {
                                //If record not found use default
                                sGLAccount = strDefaultCostAcct;
                            }
                        }
                    }
                    //End of USE HIERARCHY


                    iSuffixLen = _actualTSCTL.GLSuffixLength > 0 ? _actualTSCTL.GLSuffixLength + 1 : 0; // ERF
                    if (!string.IsNullOrEmpty(sGLAccount) &&
                        sGLAccount.Length > _actualTSCTL.GLBaseLength +
                        iSuffixLen) // ERF
                    {
                        return GetEntityIDFromPrefix(sGLAccount, strPrefixType);
                    }
                }


                if (!string.IsNullOrEmpty(sJb) && !string.IsNullOrEmpty(sCstCd) &&
                    (strGlRetrieveMethod.ToUpper() == "RETRIEVE COST CODE" || strGlRetrieveMethod.ToUpper().Contains(apDistSettings.Cost_Code_Desc.ToUpper())))
                {
                    sQry = "SELECT Cost_Account FROM JCM_MASTER__COST_CODE WHERE Job=" +
                        CommonFunctions.QNApos(sJb) +
                        " AND Cost_Code=" +
                        CommonFunctions.QNApos(sCstCd);

                    if (!string.IsNullOrEmpty(sExt))
                    {
                        sQry = sQry + " AND Extra=" + CommonFunctions.QNApos(sExt);
                    }

                    aRs = new DataTable();
                    aRs = _docDA.GetDataTableTSync(sQry, ConnectionID);

                    if (aRs != null && aRs.Rows.Count > 0)
                    {
                        sGLAccount = string.IsNullOrEmpty(aRs.Rows[0]["Cost_Account"].ToString().Trim()) ? string.Empty : aRs.Rows[0]["Cost_Account"].ToString();
                    }

                    if (!string.IsNullOrEmpty(sGLAccount))
                    {
                        sGLPrefix = GetEntityIDFromPrefix(sGLAccount, strPrefixType);
                        if (!string.IsNullOrEmpty(sGLPrefix))
                        {
                            return sGLPrefix;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(sJb) && strGlRetrieveMethod.ToUpper() == "RETRIEVE BY JOB")
                        {
                            sQry = "SELECT Cost_Account_Group, Cost_Account FROM JCM_MASTER__JOB WHERE Job=" + CommonFunctions.QNApos(sJb);

                            aRs = new DataTable();
                            aRs = _docDA.GetDataTableTSync(sQry, ConnectionID);

                            if (aRs == null || aRs.Rows.Count <= 0)
                                return string.Empty;

                            sGLPrefix = string.IsNullOrEmpty(aRs.Rows[0]["Cost_Account"].ToString().Trim()) ? string.Empty : aRs.Rows[0]["Cost_Account"].ToString();

                            if (!string.IsNullOrEmpty(sGLPrefix))
                            {
                                return _actualTSCTL.DetermineGLPrefix(sGLPrefix, ConnectionID);
                            }
                            else
                            {
                                sCostAcctGrp = string.IsNullOrEmpty(aRs.Rows[0]["Cost_Account_Group"].ToString().Trim()) ? string.Empty : aRs.Rows[0]["Cost_Account_Group"].ToString();
                                if (string.IsNullOrEmpty(sCostAcctGrp))
                                {
                                    return string.Empty;

                                }

                                sQry = "SELECT Cost_Account_" + sCostAcctGrp.Substring(sCostAcctGrp.Length - 1, 1) + " FROM JCM_MASTER__STANDARD_COST_CODE WHERE Cost_Code=" + CommonFunctions.QNApos(sCstCd);

                                aRs = new DataTable();
                                aRs = _docDA.GetDataTableTSync(sQry, ConnectionID);


                                //If aRs.EOF Then DetermineAPDistGLPrefix = "" : Exit Function
                                sGLAccount = string.IsNullOrEmpty(aRs.Rows[0][0].ToString().Trim()) ? string.Empty : aRs.Rows[0][0].ToString();

                                if (!string.IsNullOrEmpty(sGLAccount))
                                {
                                    sGLPrefix = GetEntityIDFromPrefix(sGLAccount, strPrefixType);
                                    return sGLPrefix;
                                }
                                else
                                {
                                    return string.Empty;
                                }
                            }
                        }
                    }
                }

                //if (sJb > "") // ERF original
                if (!string.IsNullOrEmpty(sJb)) // ERF
                {
                    sQry = "SELECT Cost_Account_Group, Cost_Account, Cost_Account_Prefix FROM JCM_MASTER__JOB WHERE Job=" + CommonFunctions.QNApos(sJb);

                    aRs = new DataTable();
                    aRs = _docDA.GetDataTableTSync(sQry, ConnectionID);

                    if (aRs == null || aRs.Rows.Count <= 0)
                    {
                        return string.Empty;
                    }

                    if (apDistSettings.JCControls.bolRetrieveAcctPrfxFrmJob == true)
                    {
                        return string.IsNullOrEmpty(aRs.Rows[0]["Cost_Account_Prefix"].ToString().Trim()) ? string.Empty : aRs.Rows[0]["Cost_Account_Prefix"].ToString().Trim();
                    }
                    else
                    {
                        sGLPrefix = string.IsNullOrEmpty(aRs.Rows[0]["Cost_Account"].ToString().Trim()) ? string.Empty : aRs.Rows[0]["Cost_Account"].ToString().Trim();
                    }

                    if (!string.IsNullOrEmpty(sGLPrefix))
                    {
                        return _actualTSCTL.DetermineGLPrefix(sGLPrefix, sPrxLvl, false, ConnectionID);
                    }
                    else
                    {
                        sCostAcctGrp = string.IsNullOrEmpty(aRs.Rows[0]["Cost_Account_Group"].ToString().Trim()) ? string.Empty : aRs.Rows[0]["Cost_Account_Group"].ToString().Trim();

                        if (string.IsNullOrEmpty(CommonFunctions.TestNullString(sCostAcctGrp).Trim()))
                        {
                            return string.Empty;
                        }

                        if (string.IsNullOrEmpty(CommonFunctions.TestNullString(sCat).Trim()))
                        {
                            return string.Empty;
                        }

                        sQry = "SELECT Cost_Account_" + sCostAcctGrp.Substring(sCostAcctGrp.Length - 1, 1) + " FROM JCM_MASTER__STANDARD_CATEGORY WHERE Category=" + CommonFunctions.QNApos(sCat);

                        aRs = new DataTable();
                        aRs = _docDA.GetDataTableTSync(sQry, ConnectionID);

                        if (aRs == null || aRs.Rows.Count <= 0)
                        {
                            return string.Empty;
                        }

                        sGLAccount = string.IsNullOrEmpty(aRs.Rows[0][0].ToString().Trim()) ? string.Empty : aRs.Rows[0][0].ToString();

                        if (!string.IsNullOrEmpty(sGLAccount) &&
                            sGLAccount.Length > _actualTSCTL.GLBaseLength +
                            iSuffixLen)
                        {
                            sGLPrefix = GetEntityIDFromPrefix(sGLAccount, strPrefixType);
                            return sGLPrefix;
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                }

                if (bEquipColVis == true)
                {

                    sEquipment = CommonFunctions.TestNullString(sEquip);

                    if (!string.IsNullOrEmpty(sEquipment)) // ERF
                    {
                        sQry = "SELECT Default_Prefix FROM EQM_MASTER__EQ_CONTROLS";
                        aRs = new DataTable();
                        aRs = _docDA.GetDataTableTSync(sQry, ConnectionID);
                        if (aRs != null && aRs.Rows.Count > 0)
                        {
                            sGLPrefix = string.IsNullOrEmpty(aRs.Rows[0]["Default_Prefix"].ToString()) ? string.Empty : aRs.Rows[0]["Default_Prefix"].ToString();
                        }
                        if (!string.IsNullOrEmpty(sGLPrefix))
                        {
                            return sGLPrefix;
                        }
                    }
                }
            }

            sDebitAccount = CommonFunctions.TestNullString(aVndRs.Rows[0]["Expense_Account"]);
            int levels = System.Convert.ToInt16(_actualTSCTL.GLPrefixABCLength > 0 ? 3 : (_actualTSCTL.GLPrefixABLength > 0 ? 2 : 1));
            iPrefixLen = (levels == 1 ? _actualTSCTL.GLPrefixALength : (levels == 2 ? _actualTSCTL.GLPrefixABLength : _actualTSCTL.GLPrefixABCLength)) + 1; //add final -
            iSuffixLen = _actualTSCTL.GLSuffixLength > 0 ? _actualTSCTL.GLSuffixLength + 1 : 0; // ERF original

            iLen = string.IsNullOrEmpty(sDebitAccount) ? 0 : sDebitAccount.Length;

            if (iLen > _actualTSCTL.GLBaseLength + iSuffixLen)
            {
                sGLPrefix = GetEntityIDFromPrefix(sDebitAccount, strPrefixType);
            }
            return sGLPrefix;
        }

        //private string GetGLExpenseAccount(string sJob, string sExtra, string sCostCode, string sCategory, string sEquip, string sEqCostCode, string sExpAcct, DataTable adoApVendorRs, Core.API.TLSettings.Models.ActualTSCTL _actualTSCTL, TLSettings.Models.APDistributionSettings apDistSettings, Int32 ConnectionID)
        private string GetGLExpenseAccount(string sJob, string sExtra, string sCostCode, string sCategory, string sEquip, string sEqCostCode, string sExpAcct, DataTable adoApVendorRs, Int32 ConnectionID)
        {
            string sGLAct = "";
            bool bUseEquip, bIncludePrefix, bReturnPrefix;
            bUseEquip = apDistSettings.Equipment;
            bool PrefixWasGottenFromEquipment = false;
            string company = "";
            string sGLPrefix = "";
            bool IgnoreRetriveJobPrefixSetting = false;
            if (apDistSettings.GL_Prefix)
            {
                bIncludePrefix = true;
                sGLAct = DetermineGLExpenseAccount(ref sJob, ref sCostCode, ref sExtra, ref sCategory, ref sEquip,
                         ref sEqCostCode, bUseEquip, adoApVendorRs.Rows[0]["Expense_Account"].ToString(), ref bIncludePrefix, ConnectionID, adoApVendorRs, ref IgnoreRetriveJobPrefixSetting);

                PrefixWasGottenFromEquipment = false;
                sGLPrefix = DetermineAPDistGLPrefix(sJob, sExtra, sCostCode, sCategory, sEquip, sEqCostCode, bUseEquip, adoApVendorRs, ConnectionID);

                if (!string.IsNullOrEmpty(sGLPrefix))
                    company = sGLPrefix;
                // D2209 - If sJob is not empty get the default else go to Vendor AP default
                else if (!string.IsNullOrEmpty(sJob))
                {
                    sGLAct = apDistSettings.JCControls.strDefaultCostAcct;
                }
                else
                {
                    sGLAct = adoApVendorRs.Rows[0]["Expense_Account"].ToString();
                }

            }
            else
            {

                if (!string.IsNullOrEmpty(sJob) && apDistSettings.JCControls.bolRetrieveAcctPrfxFrmJob)
                {
                    bReturnPrefix = false;
                }
                else
                {
                    bReturnPrefix = true;
                }

                bIncludePrefix = false;

                IgnoreRetriveJobPrefixSetting = false;

                if (adoApVendorRs != null && adoApVendorRs.Rows.Count > 0)
                {
                    sGLAct = DetermineGLExpenseAccount(ref sJob, ref sCostCode, ref sExtra, ref sCategory, ref sEquip,
                        ref sEqCostCode, bUseEquip, adoApVendorRs.Rows[0]["Expense_Account"].ToString(), ref bIncludePrefix, ConnectionID, adoApVendorRs, ref IgnoreRetriveJobPrefixSetting);
                }

                if (!bReturnPrefix && IgnoreRetriveJobPrefixSetting == false)
                {
                    sGLPrefix = DetermineAPDistGLPrefix(sJob, sExtra, sCostCode, sCategory, sEquip, sEqCostCode, bUseEquip, adoApVendorRs, ConnectionID);
                }
                else
                {
                    sGLPrefix = string.Empty;
                }

                if (!string.IsNullOrEmpty(sGLPrefix) && !string.IsNullOrEmpty(sGLAct))
                {
                    string sSepChar = _actualTSCTL.DeterminePunctuation();
                    string PreffPlusPunct = sGLPrefix + sSepChar;
                    if (!sGLAct.Contains(PreffPlusPunct))
                        sGLAct = sGLPrefix + sSepChar + sGLAct;
                }
                if (string.IsNullOrEmpty(sGLAct))
                {
                    sGLAct = sExpAcct;
                }
            }


            return sGLAct;

        }

        //private string DetermineGLExpenseAccount(ref string sJob, ref string sCostCode, ref string sExtra, ref string sCategory, ref string sEquip, ref string sEqCstCd, bool bEquipColVis, string sVndExpAccount, ref bool bIncludePrefix, Core.API.TLSettings.Models.ActualTSCTL _actualTSCTL, TLSettings.Models.APDistributionSettings apDistSettings, Int32 ConnectionID, DataTable adoApVendorRs, ref bool IgnoreRetriveJobPrefixSetting)
        private string DetermineGLExpenseAccount(ref string sJob, ref string sCostCode, ref string sExtra, ref string sCategory, ref string sEquip, ref string sEqCstCd, bool bEquipColVis, string sVndExpAccount, ref bool bIncludePrefix, Int32 ConnectionID, DataTable adoApVendorRs, ref bool IgnoreRetriveJobPrefixSetting)
        {
            //we return full account in cluding G/L Prefix, base account & suffix
            string returnValue = String.Empty;
            string sExpAccount = "";
            string sEquipment;
            bool bEq_RetrievePrefixFromEq = false;
            bool bMemoCost = false;
            string GLPrefix = string.Empty;

            //GLAccRtvMessageString = "";

            string svEquipCostCode = sEqCstCd;

            //string log = "Job - " + (string.IsNullOrEmpty(sJob) ? "" : sJob);
            //log += " Extra - " + (string.IsNullOrEmpty(sExtra) ? "" : sExtra);
            //log += " CostCode - " + (string.IsNullOrEmpty(sCostCode) ? "" : sCostCode);
            //log += " Category - " + (string.IsNullOrEmpty(sCategory) ? "" : sCategory);
            //log += " Equipment - " + (string.IsNullOrEmpty(sEquip) ? "" : sEquip);
            //log += " Cost_Code_295 - " + (string.IsNullOrEmpty(sEqCstCd) ? "" : sEqCstCd);
            //log += " Vendor_Expense_Account - " + (string.IsNullOrEmpty(sVndExpAccount) ? "" : sVndExpAccount); ;
            //log += " ConnectionID - " + ConnectionID.ToString();
            //log += " bEquipColVis - " + (bEquipColVis ? "True" : "False");
            //log += " bIncludePrefix - " + (bIncludePrefix ? "True" : "False");

            //_docDA.LogError("CoreAPIDocumentSvc", "DetermineGLExpenseAccount", log, "", "", "");

            if (!string.IsNullOrEmpty(sEquip) && !string.IsNullOrEmpty(sEqCstCd))
            {

                bMemoCost = _docDA.GetEquipmentMemoCost(ConnectionID, sEqCstCd);
            }

            sEquipment = bEquipColVis == true ? (CommonFunctions.TestNullString(sEquip)) : "";

            if (apDistSettings.JCControls.bolJCSystemActive == false || (CommonFunctions.IsNullOrEmpty(sJob) && CommonFunctions.IsNullOrEmpty(sEquipment)))
            {
                returnValue = sVndExpAccount;
                //if (!CommonFunctions.IsNullOrEmpty(sVndExpAccount))
                //    GLAccRtvMessageString = GLAccRtvFromVndMessageString;
                return returnValue;
            }

            //This lines are added so it would be possible to test
            //for the equipment cost by equipment right
            //after the equipment is added.
            if (string.IsNullOrEmpty(CommonFunctions.TestNullString(sEqCstCd).Trim()))
                sEqCstCd = "00-000";
            //------------------------------------ Irene 11/13/2012

            if (!string.IsNullOrEmpty(CommonFunctions.TestNullString(sEquipment).Trim()) &&
                !string.IsNullOrEmpty(CommonFunctions.TestNullString(sEqCstCd).Trim())
                )   //VERY IMPORTANT CHANGE && bMemoCost == false Jan 5 2013
            {
                //In Equipment Setting-->GL Entry Settings if Table Columns are used for the cost debit account, any combination
                //of the three listed tables can be selected - the setting for this is in the Cost_DR_Table column in the EQM_MASTER__EQ_CONTROLS table
                //The 3 table columns listed are Equipment type (ET), Equipment (EQ) and Cost code Eq (CC)
                //If only ET is selected, the field value is 1
                //If only EQ is selected, the field value is 2
                //If only CC is selected, the field value is 4
                //If ET and EQ are selected, the field value is 3
                //If ET and CC are selected, the field value is 5
                //If EQ and CC are selected, the field value is 6
                //If ET, EQ and CC are selected, the field value is 7

                //VERY IMPORTANT CHANGE - lines taken out for now.
                //if (bMemoCost == true && (CommonFunctions.IsNullOrEmpty(sCostCode) || CommonFunctions.IsNullOrEmpty(sCategory)))
                //{
                //    return string.Empty;
                //}
                //---------------------

                //if (bolEq_UseJCCostAcct == true)  //2.3.74
                if ((apDistSettings.JCControls.bolEq_UseJCCostAcct == true) && (!string.IsNullOrEmpty(sJob)) && (bMemoCost == true)) //VERY IMPORTANT CHANGE && (bMemoCost == true) Jan 5 2013
                {

                    bool notEqRetrievePrefixFromEq = !bEq_RetrievePrefixFromEq;

                    //we use "Not bolEq_RetrievePrefixFromEq" because if we are retrieving the GL Prefix from the equipment record, we do not want it here
                    sExpAccount = GetExpenseAccountFromJob(ref sJob, ref sCostCode, ref sExtra, ref sCategory, ref notEqRetrievePrefixFromEq, ConnectionID, ref IgnoreRetriveJobPrefixSetting); // ERF

                    if (!CommonFunctions.IsNullOrEmpty(sExpAccount)) // ERF
                    {
                        returnValue = sExpAccount;

                        //if (!CommonFunctions.IsNullOrEmpty(returnValue))
                        //    GLAccRtvMessageString = GLAccRtvFromJobCostMessageString;

                        return returnValue;
                    }
                }

                if (bMemoCost == false || (bMemoCost == true && !string.IsNullOrEmpty(sEqCstCd)))
                {

                    if (apDistSettings.JCControls.bolEq_UseAccountTable == false)
                    {
                        if (CommonFunctions.IsNullOrEmpty(sExpAccount))
                        {
                            sExpAccount = apDistSettings.JCControls.strEq_CostDebitAcct;
                        }
                        if (bEq_RetrievePrefixFromEq == false)
                        {
                            returnValue = sExpAccount;
                        }
                        else
                        {
                            returnValue = _actualTSCTL.DetermineBaseAccount(sExpAccount, true, ConnectionID);
                        }
                        //VERY IMPORTANT CHANGE - retrieving the gl prefix from equipment - 12/13/2012
                        if (!apDistSettings.GL_Prefix && apDistSettings.JCControls.bolEq_RetrievePrefixFromEq)
                        {
                            if (returnValue != "")
                            {
                                string basAcct = _actualTSCTL.DetermineBaseAccount(returnValue, true, ConnectionID);
                                string glPref = "";
                                glPref = DetermineAPDistGLPrefix(sJob, sExtra, sCostCode, sCategory, sEquip, sEqCstCd, apDistSettings.Equipment, adoApVendorRs, ConnectionID);
                                if (basAcct != "" && glPref != "")
                                {
                                    string sSepChar = "";
                                    if (_actualTSCTL.GLPrefixABCLength > 0)
                                    {
                                        sSepChar = _actualTSCTL.GLPrefixABCPunct;
                                    }
                                    else if (_actualTSCTL.GLPrefixABLength > 0)
                                    {
                                        sSepChar = _actualTSCTL.GLPrefixABPunct;
                                    }
                                    else
                                    {
                                        sSepChar = _actualTSCTL.GLPrefixAPunct;
                                    }
                                    returnValue = glPref + sSepChar + basAcct;
                                }
                            }
                        }
                        //-----------------------------

                        //if (!CommonFunctions.IsNullOrEmpty(returnValue))
                        //    GLAccRtvMessageString = GLAccRtvFromEquipCostMessageString;

                        return returnValue;
                    }

                    sExpAccount = GetExpenseAccountFromEquip(ref sEquip, sEqCstCd, ConnectionID);

                    if (apDistSettings.JCControls.bolEq_RetrievePrefixFromEq)
                    {

                        GLPrefix = GetGLPrefixFromEquipment(sEquip, ConnectionID);
                        //GLPrefix = RetrieveGLPrefixFromEquipment(sEquip, ConnectionID);

                        if (!CommonFunctions.IsNullOrEmpty(GLPrefix))
                        {
                            if (!sExpAccount.Contains(GLPrefix)) //add 1/10/2013
                            {
                                //add 1/10/2013
                                if (apDistSettings.GL_Prefix == false && apDistSettings.JCControls.strEq_CostDebitAcct == sExpAccount)
                                {
                                    sExpAccount = _actualTSCTL.DetermineBaseAccount(sExpAccount, true, ConnectionID);
                                }
                                //add 1/10/2013-------------
                                sExpAccount = GLPrefix + sExpAccount;
                            } //add 1/10/2013
                        }
                        //VERY IMPORTANT CHANGE! - NOT USING BASE ACCOUNT
                        //The following added code makes sure that even when the prefix is returned from equipment
                        //and the equipment account is returned,
                        //the full equipment is returned and not just the base one.
                        else
                        {
                            if (!CommonFunctions.IsNullOrEmpty(sExpAccount))
                            {
                                string Pnct = "";
                                Pnct = _actualTSCTL.DeterminePunctuation();
                                string eqPrf = apDistSettings.JCControls.strEq_DefaultPrefix + Pnct;
                                if (!sExpAccount.Contains(eqPrf))
                                    sExpAccount = eqPrf + sExpAccount;
                            }
                        }
                        //VERY IMPORTANT CHANGE!         //Irene 11/8/2012
                    }

                    returnValue = sExpAccount;
                    return returnValue;

                } //VERY IMPORTANT CHANGE - addition jan 5 2013

            }

            returnValue = GetExpenseAccountFromJob(ref sJob, ref sCostCode, ref sExtra, ref sCategory, ref bIncludePrefix, ConnectionID, ref IgnoreRetriveJobPrefixSetting);

            //if (!CommonFunctions.IsNullOrEmpty(returnValue))
            //    GLAccRtvMessageString = GLAccRtvFromJobCostMessageString;

            return returnValue;
        }

        //private string GetExpenseAccountFromJob(ref string sJob, ref string sCostCode, ref string sExtra, ref string sCategory, ref bool bIncludePrefix, Core.API.TLSettings.Models.ActualTSCTL _actualTSCTL, TLSettings.Models.APDistributionSettings apDistSettings, Int32 ConnectionID, ref bool IgnoreRetriveJobPrefixSetting)
        private string GetExpenseAccountFromJob(ref string sJob, ref string sCostCode, ref string sExtra, ref string sCategory, ref bool bIncludePrefix, Int32 ConnectionID, ref bool IgnoreRetriveJobPrefixSetting)
        {
            string returnValue = null;
            string sExpAccount;
            string extraA = ""; // ERF added
            string extraB = ""; // ERF added

            //added by sanket
            string strGlRetrieveMethod = apDistSettings.JCControls.strGlRetrieveMethod;
            string strDefaultCostAcct = apDistSettings.JCControls.strDefaultCostAcct;
            string strCategoryUsage = apDistSettings.JCControls.strCategoryUsage;
            //

            //string log = "Job - " + (string.IsNullOrEmpty(sJob) ? "" : sJob);
            //log += " Extra - " + (string.IsNullOrEmpty(sExtra) ? "" : sExtra);
            //log += " CostCode - " + (string.IsNullOrEmpty(sCostCode) ? "" : sCostCode);
            //log += " Category - " + (string.IsNullOrEmpty(sCategory) ? "" : sCategory);
            //log += " ConnectionID - " + ConnectionID.ToString();
            //log += " bIncludePrefix - " + (bIncludePrefix ? "True" : "False");
            //log += " strGlRetrieveMethod - " + (string.IsNullOrEmpty(strGlRetrieveMethod) ? "" : strGlRetrieveMethod);
            //log += " strDefaultCostAcct - " + (string.IsNullOrEmpty(strDefaultCostAcct) ? "" : strDefaultCostAcct);
            //log += " strCategoryUsage - " + (string.IsNullOrEmpty(strCategoryUsage) ? "" : strCategoryUsage);

            //_docDA.LogError("CoreAPIDocumentSvc", "GetExpenseAccountFromJob", log, "", "", "");



            sExpAccount = "";
            switch (strGlRetrieveMethod.ToUpper())
            {
                case "RETRIEVE BY JOB":

                    if (!string.IsNullOrEmpty(sJob)) // ERF
                    {
                        sExpAccount = GetCostAccount("Job", ref sJob, "", ref extraA, ref extraB, bIncludePrefix, ConnectionID);
                    }
                    break;
                case "RETRIEVE COST CODE":
                    if (!string.IsNullOrEmpty(sCostCode)) // ERF
                    {
                        sExpAccount = GetCostAccount("CostCode", ref sJob, sExtra, ref sCostCode, ref extraB, bIncludePrefix, ConnectionID); // ERF switched extraB in for the necessary assignment

                        if (sExpAccount == strDefaultCostAcct)
                            IgnoreRetriveJobPrefixSetting = true;

                    }
                    break;
                case "RETRIEVE BY CATEGORY":
                    if (!string.IsNullOrEmpty(sCategory)) // ERF
                    {
                        sExpAccount = GetCostAccount("Category", ref sJob, sExtra, ref sCostCode, ref sCategory, bIncludePrefix, ConnectionID);
                    }
                    break;
                case "RETRIEVE DEFAULT":
                    sExpAccount = strDefaultCostAcct;
                    IgnoreRetriveJobPrefixSetting = true;
                    break;
                case "USE HIERARCHY":

                    if (strCategoryUsage.ToUpper() == "ALWAYS USE" && !string.IsNullOrEmpty(sCategory)) // ERF
                    {
                        sExpAccount = GetCostAccount("Category", ref sJob, sExtra, ref sCostCode, ref sCategory, bIncludePrefix, ConnectionID);
                    }

                    if (string.IsNullOrEmpty(sExpAccount) && !string.IsNullOrEmpty(sCostCode)) // ERF
                    {
                        sExpAccount = GetCostAccount("CostCode", ref sJob, sExtra, ref sCostCode, ref extraB, bIncludePrefix, ConnectionID); // ERF switched extraB in for necessary assignment
                    }

                    if (string.IsNullOrEmpty(sExpAccount) && !string.IsNullOrEmpty(sJob)) // ERF
                    {
                        sExpAccount = GetCostAccount("Job", ref sJob, "", ref extraA, ref extraB, bIncludePrefix, ConnectionID); // ERF switched extraA & extraB in for necessary assignments
                    }

                    if (string.IsNullOrEmpty(sExpAccount))
                    {
                        sExpAccount = strDefaultCostAcct;
                    }

                    if (sExpAccount == strDefaultCostAcct)
                        IgnoreRetriveJobPrefixSetting = true;

                    break;
            }

            //same as cost code retrieval
            if (sExpAccount == "" && strGlRetrieveMethod.ToUpper() != "RETRIEVE COST CODE" && strGlRetrieveMethod.ToUpper().Contains(apDistSettings.Cost_Code_Desc.ToUpper()))
            {
                if (!string.IsNullOrEmpty(sCostCode)) // ERF
                {
                    sExpAccount = GetCostAccount("CostCode", ref sJob, sExtra, ref sCostCode, ref extraB, bIncludePrefix, ConnectionID); // ERF switched extraB in for the necessary assignment

                    //if (sExpAccount == strDefaultCostAcct)
                    //    IgnoreRetriveJobPrefixSetting = true;

                }
            }

            returnValue = sExpAccount;
            return returnValue;
        }

        //private string GetCostAccount(string sTable, ref string sJob, string sExtra, ref string sCostCode, ref string sCategory, bool bIncludePrfx, Core.API.TLSettings.Models.ActualTSCTL _actualTSCTL, TLSettings.Models.APDistributionSettings apDistSettings, Int32 ConnectionID)
        private string GetCostAccount(string sTable, ref string sJob, string sExtra, ref string sCostCode, ref string sCategory, bool bIncludePrfx, Int32 ConnectionID)
        {
            string returnValue = null;
            string sQry = "";
            string sCostAccount = "";
            string sBaseAcct = "";
            string sGLPrefix = "";
            string sGLAccount = "";
            string sPunct = "";
            string sPrefixLevel = string.Empty;

            //added by sanket
            string strGlRetrieveMethod = apDistSettings.JCControls.strGlRetrieveMethod;
            string strDefaultCostAcct = apDistSettings.JCControls.strDefaultCostAcct;
            string strCategoryUsage = apDistSettings.JCControls.strCategoryUsage;
            //

            sPunct = "";
            if (_actualTSCTL.GLPrefixABCLength > 0)
            {
                sPunct = _actualTSCTL.GLPrefixABCPunct;
                sPrefixLevel = "PrefixC";
            }
            else if (_actualTSCTL.GLPrefixABLength > 0) // ERF
            {
                sPunct = _actualTSCTL.GLPrefixABPunct;
                sPrefixLevel = "PrefixB";
            }
            else if (_actualTSCTL.GLPrefixALength > 0) // ERF
            {
                sPunct = _actualTSCTL.GLPrefixAPunct;
                sPrefixLevel = "PrefixA";
            }

            sCostAccount = "";

            if (string.IsNullOrEmpty(CommonFunctions.TestNullString(sJob).Trim()))
                return "";

            switch (sTable)
            {
                case "Job":
                    {
                        sQry = "SELECT Cost_Account FROM JCM_MASTER__JOB WHERE Job=" + CommonFunctions.QNApos(sJob);
                        break;
                    }
                case "Category":
                    {
                        if (string.IsNullOrEmpty(CommonFunctions.TestNullString(sCategory).Trim()))
                            return "";

                        if (string.IsNullOrEmpty(CommonFunctions.TestNullString(sCostCode).Trim()))
                        {
                            sBaseAcct = GetDefaultCostAccount("Category", sJob, "", sCategory, bIncludePrfx, ConnectionID);
                            return sBaseAcct;
                        }
                        else
                        {
                            sQry = "SELECT Cost_Account FROM JCM_MASTER__CATEGORY WHERE Job=" + CommonFunctions.QNApos(sJob) + " AND Cost_Code=" + CommonFunctions.QNApos(sCostCode) + " AND Category=" + CommonFunctions.QNApos(sCategory);

                            if (!string.IsNullOrEmpty(CommonFunctions.TestNullString(sExtra).Trim())) // ERF
                            {
                                sQry = sQry + " AND Extra=" + CommonFunctions.QNApos(sExtra);
                            }
                            else
                            {
                                sQry = sQry + " AND Extra=\'\'";
                            }
                        }
                        break;
                    }
                case "CostCode":
                    {
                        if (string.IsNullOrEmpty(CommonFunctions.TestNullString(sCostCode).Trim()))
                            return "";

                        sQry = "SELECT Cost_Account FROM JCM_MASTER__COST_CODE WHERE Job=" + CommonFunctions.QNApos(sJob) + " AND Cost_Code=" + CommonFunctions.QNApos(sCostCode);

                        if (!string.IsNullOrEmpty(CommonFunctions.TestNullString(sExtra).Trim())) // ERF
                        {
                            sQry = sQry + " AND Extra=" + CommonFunctions.QNApos(sExtra);
                        }
                        else
                        {
                            sQry = sQry + " AND Extra=\'\'";
                        }
                        break;
                    }
            }

            DataTable reader1 = new DataTable();

            //if (TimberScan.Properties.Extended.Default.TSyncActive)
            //{
            //    sQry += " AND ConnectionID=" + modSystemProcedures.clsTimberlineConnection.ConnectionID.ToString();
            //}
            //tLineData.FillDataTable(reader1, sQry, null, true, false);

            reader1 = _docDA.GetDataTableTSync(sQry, ConnectionID);

            if ((reader1 != null) && (reader1.Rows.Count > 0))
            {
                sCostAccount = reader1.Rows[0]["Cost_Account"].ToString();

                //If sCostAccount is empty 
                // Check for "Use Hierarchy" and the table
                if (string.IsNullOrEmpty(sCostAccount) && (strGlRetrieveMethod).ToUpper() == "USE HIERARCHY" && sTable == "Job")
                {
                    sQry = "SELECT Cost_Account FROM JCM_MASTER__JOB WHERE Job=" + CommonFunctions.QNApos(sJob);
                    DataTable reader2 = new DataTable();
                    //if (TimberScan.Properties.Extended.Default.TSyncActive)
                    //{
                    //    sQry += " AND ConnectionID=" + modSystemProcedures.clsTimberlineConnection.ConnectionID.ToString();
                    //}
                    //tLineData.FillDataTable(reader2, sQry, null, true, false);

                    reader2 = _docDA.GetDataTableTSync(sQry, ConnectionID);

                    if ((reader2 != null) && (reader2.Rows.Count > 0))
                    {
                        sGLAccount = reader2.Rows[0]["Cost_Account"].ToString().Trim();
                        if (!string.IsNullOrEmpty(sGLAccount))
                        {
                            sCostAccount = sGLAccount;
                        }
                        else
                            sCostAccount = strDefaultCostAcct;
                    }
                    else
                        sCostAccount = strDefaultCostAcct;
                }
                else if (string.IsNullOrEmpty(sCostAccount) && (strGlRetrieveMethod).ToUpper() == "USE HIERARCHY" && sTable == "Category")
                {
                    //if table is Category and CostAccount is empty 
                    //step1 search CostAccount based on job and costcode 
                    //step 2 if step1 fails search CostAccount based on job
                    //step 3 if step2 fails return default
                    if (string.IsNullOrEmpty(CommonFunctions.TestNullString(sCostCode).Trim()))
                        return "";

                    sQry = "SELECT Cost_Account FROM JCM_MASTER__COST_CODE WHERE Job=" + CommonFunctions.QNApos(sJob) + " AND Cost_Code=" + CommonFunctions.QNApos(sCostCode);
                    if (!string.IsNullOrEmpty(CommonFunctions.TestNullString(sExtra).Trim())) // ERF
                    {
                        sQry = sQry + " AND Extra=" + CommonFunctions.QNApos(sExtra);
                    }
                    else
                    {
                        sQry = sQry + " AND Extra=\'\'";
                    }
                    DataTable reader3 = new DataTable();
                    //if (TimberScan.Properties.Extended.Default.TSyncActive)
                    //{
                    //    sQry += " AND ConnectionID=" + modSystemProcedures.clsTimberlineConnection.ConnectionID.ToString();
                    //}

                    //tLineData.FillDataTable(reader3, sQry, null, true, false);

                    reader3 = _docDA.GetDataTableTSync(sQry, ConnectionID);

                    if ((reader3 != null) && (reader3.Rows.Count > 0))
                    {
                        sCostAccount = reader3.Rows[0]["Cost_Account"].ToString();
                    }
                    if (string.IsNullOrEmpty(sCostAccount))
                    {
                        sQry = "SELECT Cost_Account FROM JCM_MASTER__JOB WHERE Job=" + CommonFunctions.QNApos(sJob);
                        DataTable reader2 = new DataTable();
                        //if (TimberScan.Properties.Extended.Default.TSyncActive)
                        //{
                        //    sQry += " AND ConnectionID=" + modSystemProcedures.clsTimberlineConnection.ConnectionID.ToString();
                        //}
                        //tLineData.FillDataTable(reader2, sQry, null, true, false);
                        reader2 = _docDA.GetDataTableTSync(sQry, ConnectionID);
                        if ((reader2 != null) && (reader2.Rows.Count > 0))
                        {
                            sGLAccount = reader2.Rows[0]["Cost_Account"].ToString().Trim();
                            if (!string.IsNullOrEmpty(sGLAccount))
                            {
                                sCostAccount = sGLAccount;
                            }
                            else
                                sCostAccount = strDefaultCostAcct;
                        }
                        else
                            sCostAccount = strDefaultCostAcct;
                    }
                }
                else if (string.IsNullOrEmpty(sCostAccount))
                {
                    if (!string.IsNullOrEmpty(sCostCode))
                        sCostAccount = GetDefaultCostAccount("CostCode", sJob, sCostCode, "", bIncludePrfx, ConnectionID);
                    if (string.IsNullOrEmpty(sCostAccount))
                    {
                        sCostAccount = strDefaultCostAcct;
                    }
                }
            }
            else
            {
                if (sTable == "Category")
                {
                    sCostAccount = GetDefaultCostAccount("Category", sJob, "", sCategory, bIncludePrfx, ConnectionID);
                }
                else
                {
                    if (sTable == "CostCode" && !string.IsNullOrEmpty(sCostCode))
                    {
                        sCostAccount = GetDefaultCostAccount("CostCode", sJob, sCostCode, "", bIncludePrfx, ConnectionID);
                    }
                    else
                    {
                        sCostAccount = strDefaultCostAcct;
                    }
                }
            }

            sGLPrefix = "";
            if (!string.IsNullOrEmpty(sCostAccount))
            {
                if (bIncludePrfx == true && !string.IsNullOrEmpty(sPunct))
                {
                    //if (bolRetrieveAcctPrfxFrmJob == true && !string.IsNullOrEmpty(sJob)) this line had been replaced by the line before //VERY IMPORTANT CHANGE - do not add on prefix when a full account is pulled - the case happens when the account and prefix are in separate columns
                    if (apDistSettings.JCControls.bolRetrieveAcctPrfxFrmJob == true && !string.IsNullOrEmpty(sJob) && !(apDistSettings.GL_Prefix && sCostAccount == strDefaultCostAcct))
                    {
                        sQry = "SELECT Cost_Account_Prefix, Cost_Account FROM JCM_MASTER__JOB WHERE Job=" + CommonFunctions.QNApos(sJob);

                        //if (TimberScan.Properties.Extended.Default.TSyncActive)
                        //{
                        //    sQry += " AND ConnectionID = " + modSystemProcedures.clsTimberlineConnection.ConnectionID.ToString();
                        //}

                        DataTable reader3 = new DataTable();
                        //tLineData.FillDataTable(reader3, sQry, null, true, false);
                        reader3 = _docDA.GetDataTableTSync(sQry, ConnectionID);

                        if ((reader3 != null) && (reader3.Rows.Count > 0))
                        {
                            sGLPrefix = reader3.Rows[0]["Cost_Account_Prefix"].ToString();
                        }
                        else
                        {
                            sGLPrefix = string.Empty;
                        }
                    }
                    else
                    {
                        //VERY IMPORTANT CHANGE! - NOT USING BASE ACCOUNT
                        //The following 2 statements were commented out because if the prefixes are not retrieved, still the account should be returned full, NOT just a base account!!!!!
                        //sGLPrefix = objTS_CTL.DetermineGLPrefix(sCostAccount, sPrefixLevel);
                        //sCostAccount = objTS_CTL.DetermineBaseAccount(sCostAccount, true);
                        //VERY IMPORTANT CHANGE!         //Irene 11/8/2012
                    }

                    if (!string.IsNullOrEmpty(sGLPrefix))
                    {
                        //1/14/2013 addidion
                        returnValue = sCostAccount;
                        string PrefPlusPunct = sGLPrefix + sPunct;
                        if (!sCostAccount.Contains(PrefPlusPunct))
                        {
                            if (apDistSettings.GL_Prefix == false && strDefaultCostAcct == sCostAccount)
                            {
                                sCostAccount = _actualTSCTL.DetermineBaseAccount(sCostAccount, true, ConnectionID);
                            }
                            //1/14/2013 addidion ---------------------
                            returnValue = sGLPrefix + sPunct + sCostAccount;
                        }//1/14/2013 addidion
                    }
                    else
                    {
                        returnValue = sCostAccount;
                    }
                }
                else
                {
                    //VERY IMPORTANT CHANGE! - NOT USING BASE ACCOUNT
                    //The following statement was commented out and the one after added because if the prefixes are not retrieved, still the account should be returned full, NOT just a base account!!!!!
                    //    returnValue = objTS_CTL.DetermineBaseAccount(sCostAccount , true);
                    returnValue = sCostAccount;
                    //VERY IMPORTANT CHANGE!         //Irene 11/8/2012
                }
            }
            else
            {
                returnValue = "";
            }
            return returnValue;
        }

        //private string GetDefaultCostAccount(string sTable, string sJob, string sCostCode, string sCategory, bool includePrefix, Core.API.TLSettings.Models.ActualTSCTL _actualTSCTL, TLSettings.Models.APDistributionSettings apDistSettings, Int32 ConnectionID)
        private string GetDefaultCostAccount(string sTable, string sJob, string sCostCode, string sCategory, bool includePrefix, Int32 ConnectionID)
        {
            string sQry = "";
            DataTable aRs = null;
            string sGLAccount;
            string sCostAcctGrp;

            bool CostAcctFromStandardCategory = false;


            if (string.IsNullOrEmpty(sJob))
                return string.Empty;

            sQry = "SELECT Cost_Account_Group FROM JCM_MASTER__JOB WHERE Job=" + CommonFunctions.QNApos(sJob);

            //if (TimberScan.Properties.Extended.Default.TSyncActive)
            //{
            //    sQry += " AND ConnectionId = " + modSystemProcedures.clsTimberlineConnection.ConnectionID.ToString();
            //    tLineData = new TimberScanDataAccess.DataAcessHelper(TimberScan.Properties.Extended.Default.TimberSyncConnectionString, TimberScanDataAccess.DataAccessType.SqlServer);
            //}
            //else
            //{
            //    tLineData = modImage.TimberlineStandardData;
            //}

            DataTable reader1 = new DataTable();

            //tLineData.FillDataTable(reader1, sQry, null, true, false);
            reader1 = _docDA.GetDataTableTSync(sQry, ConnectionID);

            if ((reader1 != null) && (reader1.Rows.Count > 0))
            {
                sCostAcctGrp = reader1.Rows[0]["Cost_Account_Group"].ToString();
                if (string.IsNullOrEmpty(sCostAcctGrp))
                    return string.Empty;
            }
            else
            {
                return string.Empty;
            }

            if (sTable == "Category")
            {
                sQry = "SELECT Cost_Account_" + Strings.Right(sCostAcctGrp, 1) + " FROM JCM_MASTER__STANDARD_CATEGORY WHERE Category=" + CommonFunctions.QNApos(sCategory);
                CostAcctFromStandardCategory = true;
            }
            else
            {
                sQry = "SELECT Cost_Account_" + Strings.Right(sCostAcctGrp, 1) + " FROM JCM_MASTER__STANDARD_COST_CODE WHERE Cost_Code=" + CommonFunctions.QNApos(sCostCode);
            }

            //if (TimberScan.Properties.Extended.Default.TSyncActive)
            //{
            //    sQry += " AND ConnectionID = " + modSystemProcedures.clsTimberlineConnection.ConnectionID.ToString();
            //}

            DataTable reader2 = new DataTable();
            //tLineData.FillDataTable(reader2, sQry, null, true, false);
            reader2 = _docDA.GetDataTableTSync(sQry, ConnectionID);

            if ((reader2 != null) && (reader2.Rows.Count > 0))
            {
                sGLAccount = reader2.Rows[0][0].ToString();
                if (!string.IsNullOrEmpty(sGLAccount))
                {
                    if (includePrefix || CostAcctFromStandardCategory)
                    {
                        sGLAccount = sGLAccount;
                    }
                    else
                    {
                        sGLAccount = this.GetBaseAccount(sGLAccount, ConnectionID);
                    }
                    return sGLAccount;
                }
            }
            return string.Empty;
        }

        //public string GetBaseAccount(string sAcct, Core.API.TLSettings.Models.ActualTSCTL _actualTSCTL, TLSettings.Models.APDistributionSettings apDistSettings, Int32 ConnectionID)
        private string GetBaseAccount(string sAcct, Int32 ConnectionID)
        {
            string returnValue = null;
            //sAcct is full account including GL Prefix (GL Prefix not mandatory)
            //returns Base Account& Suffix
            //sPrefix can either be PrefixA, PrefixB or PrefixC
            int iCnt; // ERF short to int
            int iLoop; // ERF short to int
            int iPos; // ERF shor to int
            string sBase;
            string sChr;
            string sSuffix;

            if (string.IsNullOrEmpty(sAcct))
            {
                return string.Empty;
            }

            if (_actualTSCTL.GLPrefixALength == 0)
            {
                return sAcct;
            }

            sSuffix = "";

            iCnt = 0;
            if (_actualTSCTL.GLSuffixLength > 0)
            {
                for (iPos = sAcct.Length - 1; iPos >= 0; iPos--)
                {
                    sChr = sAcct[iPos].ToString();
                    //if a character is not alphanumeric, then it must be an account separator
                    if (!char.IsLetterOrDigit(sChr, 0)) // ERF
                    {

                        iCnt = iPos;
                        break;
                    }
                }
                if (iCnt > 0)
                {
                    sSuffix = "." + sAcct.Substring(iCnt + 1);
                    sBase = sAcct.Substring(0, iCnt);
                }
                else
                {
                    sBase = sAcct;
                }
            }
            else
            {
                sBase = sAcct;
            }

            iCnt = 0;
            int testInt = 0;

            for (int i = sBase.Length - 1; i >= 0; --i)
            {
                sChr = sBase[i].ToString();
                //if ( ((char.IsUpper(sChr,0) && char.IsLetter(sChr,0)) || int.TryParse(sChr,out testInt) || sChr == " ") )
                if (!char.IsLetterOrDigit(sChr, 0))
                {
                    iCnt = i;
                    break;
                }
            }

            if (iCnt > 0)
            {

                sBase = sBase.Substring(iCnt + 1);
            }
            returnValue = sBase + sSuffix;
            return returnValue;
        }

        #endregion

        #region Equipment Related

        private string GetEquipmentTypeFromEquip(string sEq, Int32 ConnectionID)
        {
            string sQry = "";
            string sEqTyp;
            DataTable aRs = new DataTable();
            //;
            //TimberScanDataAccess.DataAcessHelper tLineData = TimberScan.Properties.Extended.Default.TSyncActive ?
            //    new TimberScanDataAccess.DataAcessHelper(TimberScan.Properties.Extended.Default.TSyncConnectionString, TimberScanDataAccess.DataAccessType.SqlServer) :
            //    modImage.TimberlineStandardData;


            sEqTyp = "";
            sQry = "SELECT Equipment_Type FROM EQM_MASTER__EQUIPMENT WHERE Equipment=" + CommonFunctions.QNApos(sEq);

            //sQry += TimberScan.Properties.Extended.Default.TSyncActive ? " AND ConnectionID = " + modSystemProcedures.clsTimberlineConnection.ConnectionID.ToString() : string.Empty;
            //tLineData.FillDataTable(aRs, sQry, null, true, false);

            aRs = _docDA.GetDataTableTSync(sQry, ConnectionID);
            if (aRs != null && aRs.Rows.Count > 0)
            {
                sEqTyp = aRs.Rows[0]["Equipment_Type"].ToString();
            }
            aRs = null;
            return sEqTyp;
        }

        //private string GetExpenseAccountFromEquip(ref string sEquip, string sEqCstCode, Core.API.TLSettings.Models.ActualTSCTL _actualTSCTL, TLSettings.Models.APDistributionSettings apDistSettings, Int32 ConnectionID)
        private string GetExpenseAccountFromEquip(ref string sEquip, string sEqCstCode, Int32 ConnectionID)
        {
            string sQry = "";
            string sEqType;
            string sExpAccount;
            DataTable aRs = null;

            //In Equipment Setting-->GL Entry Settings if Table Columns are used for the cost debit account, any combination
            //of the three listed tables can be selected - the setting for this is in the Cost_DR_Table column in the EQM_MASTER__EQ_CONTROLS table
            //The 3 table columns listed are Equipment type (ET), Equipment (EQ) and Cost code Eq (CC)
            //If only ET is selected, the field value is 1
            //If only EQ is selected, the field value is 2
            //If only CC is selected, the field value is 4
            //If ET and EQ are selected, the field value is 3
            //If ET and CC are selected, the field value is 5
            //If EQ and CC are selected, the field value is 6
            //If ET, EQ and CC are selected, the field value is 7

            //This is used when an an account table has been set up
            //Per Timberline help this is the heirarchy:

            //Order | Eq Type-ET | Equip- EQ | Code - CC
            //1    | See Note 1 |           |
            //2    |  X         |  X        | X
            //3    |            |  X        | X
            //4    |  X         |           | X
            //5    |  X         |  X        |
            //6    |            |  X        |
            //7    |  X         |           |
            //8    |            |           | X
            //9    | See Note 2 |           |

            //Note 1 - If JC info is enetered and bolEq_UseJCCostAcct is true, the JC Cost account is used
            //Note 2 - If account is not found in steps 1 - 8 then the defaul account is used
            //NOTE - step 1 is done before we call this function

            sExpAccount = "";
            sEqType = "";

            //First we see is we need to Determine Equipment Type
            if (Strings.InStr("1357", System.Convert.ToString(apDistSettings.JCControls.intEQ_TableColumns), 0) > 0)
            {
                sEqType = GetEquipmentTypeFromEquip(sEquip, ConnectionID);
            }

            if (apDistSettings.JCControls.intEQ_TableColumns == 0)
            {
                return apDistSettings.JCControls.strEq_CostDebitAcct;
            }

            string EqTpCond = "";
            if (CommonFunctions.IsNullOrEmpty(sEqType))
            {
                EqTpCond = "";
            }
            else
            {
                EqTpCond = "Equipment_Type=" + CommonFunctions.QNApos(sEqType) + " AND ";
            }


            switch (apDistSettings.JCControls.intEQ_TableColumns)
            {
                case 1:
                    sQry = "SELECT Account FROM EQS_STANDARD__COST_DEBIT_ACCOUNT_ENTRY WHERE Equipment_Type=" + CommonFunctions.QNApos(sEqType);
                    break;
                case 2:
                    sQry = "SELECT Account FROM EQS_STANDARD__COST_DEBIT_ACCOUNT_ENTRY WHERE Equipment=" + CommonFunctions.QNApos(sEquip);
                    break;
                case 4:
                    sQry = "SELECT Account FROM EQS_STANDARD__COST_DEBIT_ACCOUNT_ENTRY WHERE Cost_Code=" + CommonFunctions.QNApos(sEqCstCode);
                    break;
                case 6:
                    sQry = "SELECT Account FROM EQS_STANDARD__COST_DEBIT_ACCOUNT_ENTRY WHERE Equipment=" + CommonFunctions.QNApos(sEquip) + " AND Cost_Code=" + CommonFunctions.QNApos(sEqCstCode);
                    break;
                case 3:
                    sQry = "SELECT Account FROM EQS_STANDARD__COST_DEBIT_ACCOUNT_ENTRY WHERE " + EqTpCond + " Equipment=" + CommonFunctions.QNApos(sEquip);
                    break;
                case 5:
                    sQry = "SELECT Account FROM EQS_STANDARD__COST_DEBIT_ACCOUNT_ENTRY WHERE " + EqTpCond + " Cost_Code=" + CommonFunctions.QNApos(sEqCstCode);
                    break;
                case 7:
                    sQry = "SELECT Account FROM EQS_STANDARD__COST_DEBIT_ACCOUNT_ENTRY WHERE " + EqTpCond + " Equipment=" + CommonFunctions.QNApos(sEquip) + " AND Cost_Code=" + CommonFunctions.QNApos(sEqCstCode);
                    break;
            }

            if (apDistSettings.JCControls.intEQ_TableColumns == 1 && CommonFunctions.IsNullOrEmpty(sEqType))
            {
                sExpAccount = "";
            }
            else
            {
                aRs = new DataTable();
                //TimberScanDataAccess.DataAcessHelper tLineData = modImage.TimberlineStandardData;
                //tLineData.FillDataTable(aRs, sQry, null, true, false);
                aRs = _docDA.GetDataTableTSync(sQry, ConnectionID);
            }
            if (aRs != null && aRs.Rows.Count > 0)
            {
                sExpAccount = aRs.Rows[0]["Account"].ToString();
                if (apDistSettings.JCControls.bolEq_RetrievePrefixFromEq == true)
                {
                    sExpAccount = _actualTSCTL.DetermineBaseAccount(sExpAccount, true, ConnectionID);
                }
            }
            if (string.IsNullOrEmpty(sExpAccount))
            {
                sExpAccount = apDistSettings.JCControls.strEq_CostDebitAcct;
            }
            return sExpAccount;
        }

        //private string RetrievePrefixFromEquipmentAndPrefixIsWrong(string sNewAccount, string sEquip, string sEquipCost, Core.API.TLSettings.Models.ActualTSCTL _actualTSCTL, int ConnectionID)
        private string RetrievePrefixFromEquipmentAndPrefixIsWrong(string sNewAccount, string sEquip, string sEquipCost, int ConnectionID)
        {
            string msg = "";
            string Equipm = sEquip;
            string EquipCost = sEquipCost;

            if (Equipm != "" && EquipCost != "")
            {
                string Pref = "";
                if (!_docDA.GetEquipmentMemoCost(ConnectionID, sEquipCost.Trim()))
                {
                    //if (this.objAPDistSettings.GL_Prefix && this.grdDetail.FieldLayouts[0].Fields["Company"].Visibility == Visibility.Visible)
                    //{

                    //    Pref = CommonFunctions.TestNullString(detailRecord.Cells["Company"].Value);

                    //}
                    //else
                    //{
                    Pref = _actualTSCTL.DetermineGLPrefix(sNewAccount, "", false, ConnectionID);

                    //}
                    if (Pref != "")
                    {
                        string PrefFromEquip = RetrieveGLPrefixFromEquipment(Equipm, ConnectionID);
                        if (PrefFromEquip != "" && PrefFromEquip != Pref)
                        {
                            msg = "The Timberline settings indicate the retrieval of prefix from equipment." + "\r\n" + "In this case, the GL prefix should be '" + PrefFromEquip + "'.";
                        }
                    }
                }
            }
            return msg;
        }

        private string RetrieveGLPrefixFromEquipment(string sEquip, int ConnectionID)
        {
            string sEquipPrefix = "";
            string sQry = "";
            DataTable aRs = null;
            if (!string.IsNullOrEmpty(sEquip)) // ERF
            {
                aRs = new DataTable();
                sQry = "SELECT GL_Prefix FROM EQM_MASTER__EQUIPMENT WHERE Equipment=" + CommonFunctions.QNApos(sEquip);
                sQry = sQry + " AND ConnectionID=" + ConnectionID;
                APIUtilDataAccessHelper.TimberSynchDataDynamic(ConnectionID).FillDataTable(aRs, sQry, null, true, false);
                if (aRs != null && aRs.Rows.Count > 0)
                    sEquipPrefix = string.IsNullOrEmpty(aRs.Rows[0]["GL_Prefix"].ToString().Trim()) ? string.Empty : aRs.Rows[0]["GL_Prefix"].ToString();

                if (!string.IsNullOrEmpty(sEquipPrefix))
                {
                    sQry = "SELECT Default_Prefix FROM EQM_MASTER__EQ_CONTROLS";
                    sQry = sQry + " AND ConnectionID=" + ConnectionID;
                    aRs = new DataTable();
                    APIUtilDataAccessHelper.TimberSynchDataDynamic(ConnectionID).FillDataTable(aRs, sQry, null, true, false);
                    if (aRs != null && aRs.Rows.Count > 0)
                        sEquipPrefix = string.IsNullOrEmpty(aRs.Rows[0]["Default_Prefix"].ToString()) ? string.Empty : aRs.Rows[0]["Default_Prefix"].ToString();
                }

            }
            return sEquipPrefix;
        }

        //private string GetGLPrefixFromEquipment(string sEquip, Core.API.TLSettings.Models.ActualTSCTL _actualTSCTL, Int32 ConnectionID)
        private string GetGLPrefixFromEquipment(string sEquip, Int32 ConnectionID)
        {
            string prefix = string.Empty, sql = string.Empty, sepChar = string.Empty;
            DataTable dt = new DataTable();

            if (_actualTSCTL.GLPrefixALength == 0)
            {
                return string.Empty;
            }

            if (_actualTSCTL.GLPrefixABCLength > 0)
            {
                sepChar = _actualTSCTL.GLPrefixABCPunct;
            }
            else if (_actualTSCTL.GLPrefixABLength > 0)
            {
                sepChar = _actualTSCTL.GLPrefixABPunct;
            }
            else
            {
                sepChar = _actualTSCTL.GLPrefixAPunct;
            }

            sql = "SELECT GL_Prefix FROM EQM_MASTER__EQUIPMENT WHERE Equipment=" + CommonFunctions.QNApos(sEquip);
            dt = _docDA.GetDataTableTSync(sql, ConnectionID);

            if (dt != null && dt.Rows.Count > 0)
            {
                prefix = CommonFunctions.TestNullString(dt.Rows[0]["GL_Prefix"]);
                if (!string.IsNullOrEmpty(prefix))
                {
                    prefix = prefix + sepChar;
                }
            }

            return prefix;
        }

        #endregion

        #region Routing

        private DataTable GetNextLevelActions(DataTable dtInvoice, DataTable dtDistributions, int actionLevel, int CurrentUserID, out string retMessage, int ConnectionID)
        {
            //GetRoutingInfo_Result routingInfo = null;
            DataTable routingInfo = null;
            DataTable approvalGroups = null, actions = null;
            DataRow distribution = null, action = null;
            string routingType = string.Empty, routingValue = string.Empty,
                nextActionType = string.Empty, actionTarget = string.Empty;
            int nextActionLevel = 0, groupId = 0, badDistributionRowCount = 0;
            bool finalReviewReq = true, hasCompany = false;
            System.Collections.Hashtable htApprovalGroups = new System.Collections.Hashtable();
            string key = string.Empty;

            string CurActionType = dtInvoice.Rows[0]["CurrentActionType"].ToString();
            retMessage = "";
            string strCurrentUserID = _docDA.GetUserId(CurrentUserID);

            //get the routing values from tblRouting
            routingInfo = _docDA.GetRoutingInfo();


            if (routingInfo != null && dtDistributions != null && dtDistributions.Rows.Count > 0)
            {
                hasCompany = dtDistributions.Columns.Contains("Company");

                InvoiceTypes invType = Enum.TryParse<InvoiceTypes>(dtInvoice.Rows[0]["InvoiceType"].ToString().Trim(), true, out invType) ? invType : InvoiceTypes.None;//(InvoiceTypes)Enum.Parse(typeof(TimberScan.InvoiceTypes) , dtInvoice.Rows[0]["InvoiceType"].ToString());
                MatchTypes matchType = Enum.TryParse<MatchTypes>(dtInvoice.Rows[0]["MatchType"].ToString().Trim(), true, out matchType) ? matchType : MatchTypes.None;

                //finalReviewReq = this.FinalReviewRequired(matchType, invType);
                finalReviewReq = true;

                ///actions will store the next level invoice actions
                actions = CreateEmptyActionsDataTable();

                nextActionLevel = actionLevel;

                int iter = 0; //dec 18 2012 Irene

                while (actions.Rows.Count <= 0 && iter < 3) // ( actions.Rows.Count <= 0 ) dec 18 2012 Irene ** sometimes the loop was going continuously
                {

                    ++nextActionLevel;
                    badDistributionRowCount = 0;//02-16-2013 - Issue # 15092
                    if (nextActionLevel > TimberScan.Global.TimberScanServicesSettings.TotalActions + 1)
                    {
                        nextActionLevel = nextActionLevel - 1;
                        iter++; //dec 18 2012 Irene
                    }

                    actionTarget = nextActionLevel > TimberScan.Global.TimberScanServicesSettings.TotalActions ? (nextActionLevel - 1).ToString().PadLeft(2, '0') : nextActionLevel.ToString().PadLeft(2, '0');

                    ///loop through the distributions for the invoice and find an action for each
                    for (int distRowIndex = 0; distRowIndex < dtDistributions.Rows.Count; distRowIndex++)
                    {
                        distribution = dtDistributions.Rows[distRowIndex];


                        bool EntryWithJobOrAccount = (!string.IsNullOrEmpty(distribution["Job"].ToString().Trim()) ||
                            (hasCompany && !string.IsNullOrEmpty(distribution["Company"].ToString().Trim())) ||
                            !string.IsNullOrEmpty(distribution["Expense_Account"].ToString().Trim())
                            );

                        bool EntryWithoutJobOrAccountButAllowed =
                                                       (!(!string.IsNullOrEmpty(distribution["Job"].ToString().Trim()) ||
                            (hasCompany && !string.IsNullOrEmpty(distribution["Company"].ToString().Trim())) ||
                            !string.IsNullOrEmpty(distribution["Expense_Account"].ToString().Trim())
                            )) && (TimberScan.Global.TimberScanServicesSettings.AllowNoJobOrGLEntry) && (CurActionType == "Data Entry");


                        if (EntryWithJobOrAccount || EntryWithoutJobOrAccountButAllowed)
                        {

                            approvalGroups = this.GetApprovalGroupForDistribution(distribution, routingInfo, dtInvoice.Rows[0], out routingType, out routingValue, ConnectionID);

                            if (approvalGroups != null && approvalGroups.Rows.Count > 0)
                            {

                                //provision for problematic default group - 12/19/2012
                                bool DefaultGroupCase = false;
                                if (iter > 2 && routingType == "Dflt" && routingValue == "Dflt")
                                {
                                    if (!((approvalGroups.Rows[0][string.Format("GroupID{0}", actionTarget)] != System.DBNull.Value && (groupId > 0) || groupId == -2) ||
                                        (!string.IsNullOrEmpty(approvalGroups.Rows[0][string.Format("UserID{0}", actionTarget)].ToString()) && approvalGroups.Rows[0][string.Format("UserID{0}", actionTarget)].ToString().ToUpper() != "None".ToUpper())))
                                    {
                                        approvalGroups.Rows[0][string.Format("GroupID{0}", actionTarget)] = -2;
                                        DefaultGroupCase = true;
                                    }
                                }
                                //-----------------------------

                                groupId = !string.IsNullOrEmpty(approvalGroups.Rows[0][string.Format("GroupID{0}", actionTarget)].ToString()) ? Convert.ToInt32(approvalGroups.Rows[0][string.Format("GroupID{0}", actionTarget)]) : 0;


                                if (
                                    (
                                    ((approvalGroups.Rows[0][string.Format("GroupID{0}", actionTarget)] != System.DBNull.Value && (groupId > 0) || groupId == -2) ||
                                    (!string.IsNullOrEmpty(approvalGroups.Rows[0][string.Format("UserID{0}", actionTarget)].ToString()) && approvalGroups.Rows[0][string.Format("UserID{0}", actionTarget)].ToString().ToUpper() != "None".ToUpper()))
                                    ) &&
                                    (!(dtDistributions.Rows.Count > 1 && EntryWithoutJobOrAccountButAllowed))
                                    )
                                {

                                    if (!string.IsNullOrWhiteSpace(approvalGroups.Rows[0][string.Format("UserID{0}", actionTarget)].ToString()))
                                    {
                                        key = approvalGroups.Rows[0][string.Format("UserID{0}", actionTarget)].ToString();
                                    }
                                    else
                                    {
                                        key = approvalGroups.Rows[0][string.Format("GroupID{0}", actionTarget)].ToString();
                                    }

                                    if (!htApprovalGroups.Contains(key))
                                    {

                                        htApprovalGroups.Add(key, key);

                                        nextActionType = string.Empty;
                                        action = actions.NewRow();

                                        action["ConnectionId"] = distribution["ConnectionId"];
                                        action["InvoiceId"] = distribution["InvoiceId"];
                                        action["ProcessGroupId"] = approvalGroups.Rows[0]["GroupId"];///not sure

                                        action["UserID"] = approvalGroups.Rows[0][string.Format("UserID{0}", actionTarget)];
                                        action["GroupID"] = approvalGroups.Rows[0][string.Format("GroupID{0}", actionTarget)];

                                        action["ActionLevel"] = Convert.ToInt32(actionTarget);//nextActionLevel;
                                        action["Hold"] = false;
                                        action["Rejected"] = false;
                                        action["Complete"] = false;
                                        action["DateAssigned"] = _docDA.GetCurrentDateAndTimeOnSqlServerDate();
                                        action["AssignedBy"] = "System";
                                        action["Comment"] = "MOBILE";
                                        action["InvoiceCompleteLevel"] = approvalGroups.Rows[0]["InvoiceCompleteLevel"];
                                        action["RoutingType"] = routingType;
                                        action["RoutingValue"] = routingValue;
                                        action["Amount"] = distribution["Amount"];

                                        //if the next action level equals the total system actions then we are at final review
                                        //if final review is not required we can just export
                                        if ((nextActionLevel == TimberScan.Global.TimberScanServicesSettings.TotalActions && !finalReviewReq)
                                            || nextActionLevel > TimberScan.Global.TimberScanServicesSettings.TotalActions)
                                        {

                                            action["ActionType"] = nextActionType = "Export";
                                            action["ActionLevel"] = TimberScan.Global.TimberScanServicesSettings.TotalActions;
                                            action["GroupID"] = approvalGroups.Rows[0][string.Format("GroupID{0}", TimberScan.Global.TimberScanServicesSettings.TotalActions.ToString().PadLeft(2, '0'))];
                                            action["UserID"] = approvalGroups.Rows[0][string.Format("UserID{0}", TimberScan.Global.TimberScanServicesSettings.TotalActions.ToString().PadLeft(2, '0'))];

                                        }
                                        else
                                        {
                                            action["ActionType"] = approvalGroups.Rows[0][string.Format("Action{0}", actionTarget)];
                                            nextActionType = action["ActionType"].ToString();
                                        }

                                        //provision for problematic default group - 12/19/2012
                                        if (DefaultGroupCase)
                                        {

                                            //DataAccessHelper sqlHlpr = new DataAccessHelper(TimberScan.Global.TimberScanServicesSettings.TimberScanConnectionString, DataAccessType.SqlServer);
                                            //sqlHlpr.FillDataTable(invActionsForInvTb, "select * from dbo.tblInvoiceActions where invoiceid = " + dtInvoice.Rows[0]["InvoiceID"].ToString().Trim() + " and ActionType = 'Final Review'", null, true, false);

                                            DataTable invActionsForInvTb = new DataTable();
                                            invActionsForInvTb = _docDA.GetDataTable("select * from dbo.tblInvoiceActions where invoiceid = " + dtInvoice.Rows[0]["InvoiceID"].ToString().Trim() + " and ActionType = 'Final Review'");

                                            if (invActionsForInvTb == null || invActionsForInvTb.Rows.Count <= 0)
                                            {
                                                action["ActionType"] = "Final Review";
                                                nextActionType = "Final Review";
                                                action["UserID"] = approvalGroups.Rows[0][string.Format("UserID{0}", actionTarget)];
                                                action["GroupID"] = approvalGroups.Rows[0][string.Format("GroupID{0}", actionTarget)];
                                                action["ActionLevel"] = Convert.ToInt32(actionTarget);//nextActionLevel;
                                            }
                                        }
                                        //---------------------

                                        if (groupId == -2)
                                        {
                                            //in this case the action will be final review and we only need one, so clear the collection before adding this action
                                            actions.Rows.Clear();

                                        }

                                        actions.Rows.Add(action);
                                    }

                                    if (routingType == "I" || routingType == "V" || groupId == -2)
                                    {
                                        if (groupId == -2)
                                        {

                                            //AdditionalOperatorMessage = "";

                                            if (action["ActionType"].ToString() == "Final Review" && actionLevel == 1)
                                            {
                                                if (_docDA.UserHasRole(strCurrentUserID, "FinalReview"))
                                                    action["UserID"] = strCurrentUserID;
                                                else
                                                {
                                                    retMessage = "This invoice is scheduled to be routed to " +
                                                    strCurrentUserID + " for Final Review." + Environment.NewLine +
                                                    "This user does not have the Final Review permission.";

                                                    //action["UserID"] = this.GetFinalReviewOperator("This invoice is scheduled to be routed to " +
                                                    //TimberScan.Global.CurrentWUserID + " for Final Review." + Environment.NewLine +
                                                    //"This user does not have the Final Review permission.");
                                                }
                                            }
                                            else if (action["ActionType"].ToString().Equals("Export") && actionLevel == TimberScan.Global.TimberScanServicesSettings.TotalActions)
                                            {
                                                //action["UserID"] = TimberScan.Global.CurrentWUserID;
                                                action["UserID"] = strCurrentUserID;
                                            }
                                            else
                                            {
                                                string retErrMsg = "";
                                                string lstDEOperator = DetermineLastDEOperator(Convert.ToInt32(action["InvoiceID"]), out retErrMsg);
                                                if (string.IsNullOrEmpty(retErrMsg))
                                                    action["UserID"] = lstDEOperator;
                                            }


                                            //if ( string.IsNullOrEmpty( action ["UserID"].ToString().Trim() ) )
                                            //{
                                            //    this.AddMessage( "Invoice should be routed to the data entry operator for final review. The data entry operator could not be determined." );
                                            //    //we could not find the data entry operator to route the final review
                                            //    //action to, so we clear all the actions 
                                            //    actions.Rows.Clear();

                                            //}
                                            if (string.IsNullOrEmpty(action["UserID"].ToString().Trim()))
                                            {
                                                if (!string.IsNullOrEmpty(action["GroupID"].ToString().Trim()))
                                                {
                                                    action["GroupID"] = approvalGroups.Rows[0][string.Format("GroupID{0}", TimberScan.Global.TimberScanServicesSettings.TotalActions.ToString().PadLeft(2, '0'))];
                                                    action["UserID"] = approvalGroups.Rows[0][string.Format("UserID{0}", TimberScan.Global.TimberScanServicesSettings.TotalActions.ToString().PadLeft(2, '0'))];

                                                    //if (string.IsNullOrEmpty(action["UserID"].ToString().Trim()) && action["ActionType"].ToString() == "Final Review" && actionLevel == 1)
                                                    //    action["UserID"] = TimberScan.Global.CurrentWUserID;

                                                }
                                                else
                                                {
                                                    retMessage += Environment.NewLine + "Invalid/Missing User or Group specified for Final Review.  Please verify Approval Group settings.";
                                                    //this.AddMessage("Invalid/Missing User or Group specified for Final Review.  Please verify Approval Group settings.");
                                                    //we could not find the data entry operator to route the final review
                                                    //action to, so we clear all the actions 
                                                    actions.Rows.Clear();
                                                }
                                            }

                                            if (string.IsNullOrEmpty(action["UserID"].ToString().Trim()))
                                            {
                                                if (action["ActionType"].ToString() == "Final Review" && actionLevel == 1 && !_docDA.UserHasRole(strCurrentUserID, "FinalReview"))
                                                {
                                                    retMessage += Environment.NewLine + "This invoice is scheduled to be routed to " +
                                                    strCurrentUserID + " for Final Review." + Environment.NewLine +
                                                    "This user does not have the Final Review permission.";

                                                    //this.AddMessage("This invoice is scheduled to be routed to " +
                                                    //TimberScan.Global.CurrentWUserID + " for Final Review." + Environment.NewLine +
                                                    //"This user does not have the Final Review permission.");
                                                }
                                                else
                                                {
                                                    retMessage += Environment.NewLine + "Invoice should be routed to the data entry operator for final review. The data entry operator could not be determined.";
                                                    //if (AdditionalOperatorMessage != "")
                                                    //    this.AddMessage(AdditionalOperatorMessage);
                                                    //else
                                                    //    this.AddMessage("Invoice should be routed to the data entry operator for final review. The data entry operator could not be determined.");
                                                }
                                                actions.Rows.Clear();
                                            }

                                            return actions;
                                        }
                                        //in this case we only need to see one distribution
                                        break;
                                    }
                                }//if the approval group found has an action defined for the current level
                            }

                        }
                        else
                        {
                            //this means there is nothing we can route by in this distribution
                            //if all distributions are this way then we must break out of the while loop
                            ++badDistributionRowCount;
                        }
                    }//for distributions

                    //if (badDistributionRowCount == dtDistributions.Rows.Count && !(TimberScan.Global.TimberScanServicesSettings.AllowNoJobOrGLEntry && (CurActionType == "Data Entry")))
                    //{
                    //    this.AddMessage(string.Format("Vendor {0} Invoice {1}  must have an Account{2}{3} for each distribution",
                    //        new object[]{
                    //                dtInvoice.Rows[0]["Vendor"].ToString(),
                    //                dtInvoice.Rows[0]["Invoice"].ToString(), 
                    //            ( this.Accounting.APDistributionSettings.Job ? ", Job" : string.Empty ),

                    //            ( hasCompany && this.Accounting.APDistributionSettings.GL_Prefix ) ? (
                    //            ( ", or " +
                    //            ( ( accounting.GLPrefixABCLength > 0 && !string.IsNullOrEmpty( accounting.GLPrefixABCDesc ) ) ? accounting.GLPrefixABCDesc :
                    //            ( accounting.GLPrefixABLength > 0 && !string.IsNullOrEmpty( accounting.GLPrefixABDesc ) ) ? accounting.GLPrefixABDesc :
                    //            ( accounting.GLPrefixADesc) ) ) ) : string.Empty }));

                    //    break;
                    //}


                }///while(actions.Rows.Count<=0)
            }
            else
            {
                if (routingInfo == null || routingInfo.Rows.Count <= 0)
                    //this.AddMessage(string.Format("The routing settings for the system were not found"));
                    retMessage += Environment.NewLine + "The routing settings for the system were not found";
                else
                    //this.AddMessage(string.Format("Invalid distribution information"));
                    retMessage += Environment.NewLine + "Invalid distribution information";

                actions = null;

            }


            return actions;
        }

        private DataTable GetApprovalGroupForDistribution(DataRow Distribution, DataTable RoutingInfo, DataRow invoice, out string routingType, out string routingValue, int ConnectionID)
        {

            DataTable approvalGroups = null;
            int routingColIndex = 0;
            routingType = string.Empty;
            routingValue = string.Empty;
            ///there will always be one row in the RoutingInfo table. Loop through the fields in this row
            ///to find what routing value we can route by, then try to find an approval group for the routing value found
            for (routingColIndex = 0; routingColIndex < RoutingInfo.Columns.Count; routingColIndex++)
            {

                if (Convert.ToBoolean(RoutingInfo.Rows[0][RoutingInfo.Columns[routingColIndex].ColumnName]))
                {

                    this.GetRoutingTypeAndRoutingValue(RoutingInfo.Columns[routingColIndex].ColumnName.ToLower(), out routingType, out routingValue, Distribution, invoice, ConnectionID);

                }//if (Convert.ToBoolean(RoutingInfo.Rows[0][RoutingInfo.Columns[routingColIndex].ColumnName]))

                if (!string.IsNullOrEmpty(routingValue) || routingColIndex == RoutingInfo.Columns.Count - 1)
                {
                    ///we have found a value to route by or we have iterated through all the 
                    ///possible routing values. either way now we need to find a route

                    if (string.IsNullOrEmpty(routingValue) && routingColIndex == RoutingInfo.Columns.Count - 1)
                    {
                        routingValue = "Dflt";
                        routingType = "Dflt";
                    }

                    DataSet dsApprovalGroups = _docDA.GetApprovalGroupByMemberTypeAndMemberValue(routingType, routingValue, routingType == "I" ? 0 : Convert.ToInt32(Distribution["connectionId"]));
                    if (dsApprovalGroups != null && dsApprovalGroups.Tables.Count > 0)
                        approvalGroups = dsApprovalGroups.Tables[0];

                    if (approvalGroups != null && approvalGroups.Rows.Count > 0)
                    {
                        break;
                    }
                    else
                    {
                        routingType = string.Empty;
                        routingValue = string.Empty;

                    }

                }//if we found a routingValue or we have iterated through all the routing values
            }//for routing info

            return approvalGroups;

        }

        private bool GetRoutingTypeAndRoutingValue(string routeBy, out string routingType, out string routingValue, DataRow distribution, DataRow invoice, int ConnectionID)
        {

            bool gotit = false;

            routingType = string.Empty;
            routingValue = string.Empty;

            routeBy = routeBy.ToLower();
            TLSettings.Models.APDistributionSettings apDistSettings = tlSettings.GetAPDistributionSettings(ConnectionID);
            DataTable dtAllSettings;
            DataRow[] specficSettingsRows;
            Core.API.TLSettings.Models.ActualTSCTL _actualTSCTL;
            switch (routeBy)
            {

                case "invoicetype":
                    routingType = "I";
                    routingValue = invoice["InvoiceCode"].ToString();
                    break;
                case "commitment":
                    routingType = "M";
                    routingValue = distribution["commitment"].ToString();
                    break;
                case "vendorjob":
                    routingType = "O";
                    routingValue = string.IsNullOrEmpty(distribution["job"].ToString()) ||
                        string.IsNullOrEmpty(invoice["vendor"].ToString()) ? string.Empty : string.Format("{0}|{1}", invoice["vendor"], distribution["job"]);
                    break;
                case "vendorid":
                    routingType = "V";
                    routingValue = invoice["Vendor"].ToString();
                    break;
                case "jobextra":
                    routingType = "R";
                    routingValue = string.IsNullOrEmpty(distribution["job"].ToString()) ||
                        string.IsNullOrEmpty(distribution["extra"].ToString()) ? string.Empty : string.Format("{0}|{1}", distribution["job"], distribution["extra"]);
                    break;
                case "jobcostcodecategory":
                    routingType = "Y";
                    routingValue = string.IsNullOrEmpty(distribution["job"].ToString()) ||
                        string.IsNullOrEmpty(distribution["cost_code"].ToString()) ||
                        string.IsNullOrEmpty(distribution["category"].ToString()) ? string.Empty : string.Format("{0}|{1}|{2}", distribution["job"], distribution["cost_code"], distribution["category"]);
                    break;
                case "jobcategory":
                    routingType = "T";
                    routingValue = string.IsNullOrEmpty(distribution["job"].ToString()) ||
                        string.IsNullOrEmpty(distribution["category"].ToString()) ? string.Empty : string.Format("{0}|{1}", distribution["job"], distribution["category"]);
                    break;
                case "jobcostcode":
                    routingType = "S";
                    routingValue = string.IsNullOrEmpty(distribution["job"].ToString()) ||
                        string.IsNullOrEmpty(distribution["cost_code"].ToString()) ? string.Empty : string.Format("{0}|{1}", distribution["job"], distribution["cost_code"]);
                    break;
                case "category":
                    routingType = "A";
                    routingValue = distribution["Category"].ToString();
                    break;
                case "costcode":
                    routingType = "D";
                    routingValue = distribution["Cost_Code"].ToString();
                    break;
                case "equipment":
                    routingType = "E";
                    routingValue = distribution["equipment"].ToString();
                    break;
                case "jobauthorization":
                    routingType = "U";
                    routingValue = GetJobAuthorization(distribution["job"].ToString(), ConnectionID);
                    break;
                case "job":
                    routingType = "J";
                    routingValue = distribution["Job"].ToString();
                    break;

                case "fullglaccount":
                    routingType = "G";

                    //Accounting.SetVendorTypeLevel(invoice["vendor"].ToString());
                    dtAllSettings = tlSettings.GetTSCTLSettings(ConnectionID);
                    specficSettingsRows = dtAllSettings.Select("FLDCODE = 9", "Section");
                    _actualTSCTL = new TLSettings.Models.ActualTSCTL(ConnectionID);
                    _actualTSCTL.LoadGLSettings(specficSettingsRows);


                    if ((apDistSettings.GL_Prefix) && (distribution.Table != null && distribution.Table.Columns.Contains("Company")))
                    {
                        string sSepChar = "";
                        //if (mvarGLPrefixABCLength > 0)
                        if (_actualTSCTL.GLPrefixABCLength > 0)
                        {
                            sSepChar = _actualTSCTL.GLPrefixABCPunct;
                        }
                        //else if (mvarGLPrefixABLength > 0)
                        else if (_actualTSCTL.GLPrefixABLength > 0)
                        {
                            sSepChar = _actualTSCTL.GLPrefixABPunct;
                        }
                        else
                        {
                            sSepChar = _actualTSCTL.GLPrefixAPunct;
                        }
                        routingValue = distribution["company"].ToString() + sSepChar + distribution["expense_account"].ToString();
                    }
                    else
                        routingValue = distribution["expense_account"].ToString();

                    break;

                case "baseglaccount":

                    routingType = "B";

                    //Accounting.SetVendorTypeLevel(invoice["vendor"].ToString());
                    dtAllSettings = tlSettings.GetTSCTLSettings(ConnectionID);
                    specficSettingsRows = dtAllSettings.Select("FLDCODE = 9", "Section");
                    _actualTSCTL = new TLSettings.Models.ActualTSCTL(ConnectionID);
                    _actualTSCTL.LoadGLSettings(specficSettingsRows);

                    if ((apDistSettings.GL_Prefix) && (distribution.Table != null && distribution.Table.Columns.Contains("Company")))
                    {
                        routingValue = distribution["expense_account"].ToString();
                    }
                    else
                    {
                        routingValue = _actualTSCTL.DetermineBaseAccount(distribution["expense_account"].ToString(), true, false, ConnectionID);
                    }
                    break;
                case "glprefix":
                    routingType = "C";

                    //SetVendorTypeLevel will set the vendor type for the APDistributionSettings
                    //we do this because APDistributionSettings.GL_Prefix is dependent on the vendor type

                    //Accounting.SetVendorTypeLevel(invoice["vendor"].ToString());

                    dtAllSettings = tlSettings.GetTSCTLSettings(ConnectionID);
                    specficSettingsRows = dtAllSettings.Select("FLDCODE = 9", "Section");
                    _actualTSCTL = new TLSettings.Models.ActualTSCTL(ConnectionID);
                    _actualTSCTL.LoadGLSettings(specficSettingsRows);

                    if (apDistSettings.GL_Prefix && distribution.Table != null && distribution.Table.Columns.Contains("company"))
                    {

                        routingValue = distribution["company"].ToString();

                    }
                    else
                    {

                        if (_actualTSCTL.GLPrefixABCLength > 0)
                        {
                            routingValue = _actualTSCTL.DetermineGLPrefix(distribution["expense_account"].ToString(), "PrefixC", false, ConnectionID);
                        }
                        else if (_actualTSCTL.GLPrefixABLength > 0)
                        {
                            routingValue = _actualTSCTL.DetermineGLPrefix(distribution["expense_account"].ToString(), "PrefixB", false, ConnectionID);
                        }
                        else if (_actualTSCTL.GLPrefixALength > 0)
                        {

                            routingValue = _actualTSCTL.DetermineGLPrefix(distribution["expense_account"].ToString(), "PrefixA", false, ConnectionID);
                        }

                    }
                    break;
                case "extra":
                    routingType = "X";
                    routingValue = distribution["extra"].ToString();
                    break;

            }//case


            return gotit;


        }

        private string GetJobAuthorization(string job, int ConnectionID)
        {

            string jobAuth = string.Empty;
            DataTable dtJobAuth = new DataTable();


            if (string.IsNullOrWhiteSpace(job))
                return jobAuth;

            job = job.Trim();
            jobAuth = tlSettings.GetJobAuthorization(job, ConnectionID);
            //if (!JobAuthDict.ContainsKey(job))
            //{

            //    this.accounting.TimberlineData.FillDataTable(dtJobAuth,
            //                                                 "SELECT  \"Authorization\" FROM JCM_MASTER__JOB WHERE Job='" +
            //                                                 job.Replace("'", "''") + "'", null, true, false);

            //    if (dtJobAuth != null && dtJobAuth.Rows.Count > 0)
            //    {
            //        jobAuth = dtJobAuth.Rows[0]["Authorization"].ToString();
            //        JobAuthDict.Add(job, jobAuth);
            //    }
            //}
            //else
            //{
            //    jobAuth = JobAuthDict[job];

            //}


            return jobAuth;
        }

        private string DetermineLastDEOperator(int invoiceId, out string message)
        {
            DataTable dt = new DataTable();
            //string message = string.Empty;
            string dataEntryOperator = string.Empty;
            message = "";
           // dt = _docDA.GetCompletedDataEntryTasks(invoiceId);
            dataEntryOperator = _docDA.GetCompletedDataEntryTasks(invoiceId);

            //if (dt != null && dt.Rows.Count > 0 && !string.IsNullOrEmpty(dataEntryOperator = dt.Rows[0]["CompletedBy"].ToString().Trim()))
            if (string.IsNullOrEmpty(dataEntryOperator))
            {
                message = "The Data Entry operator for this invoice could not be determined for Final Review.";
            }
            else if (!this._docDA.UserHasRole(dataEntryOperator, "FinalReview"))
            {
                message = "This invoice is scheduled to be routed to " +
                    dataEntryOperator + " for Final Review." + Environment.NewLine +
                    "This user is either inactive or does not have" + Environment.NewLine +
                    "Final Review permission.";
                //AdditionalOperatorMessage = message;
            }

            //if (!string.IsNullOrEmpty(message))
            //{

            //   // dataEntryOperator = this.GetFinalReviewOperator(message);


            //}

            return dataEntryOperator;
        }

        private DataTable CreateEmptyActionsDataTable()
        {

            DataTable dtActions = new DataTable("InvoiceActions");

            DataColumn dcAction = new DataColumn("ActionID");
            dtActions.Columns.Add(dcAction);

            dcAction = new DataColumn("ConnectionId");
            dtActions.Columns.Add(dcAction);

            dcAction = new DataColumn("InvoiceId");
            dtActions.Columns.Add(dcAction);

            dcAction = new DataColumn("ProcessGroupID");
            dtActions.Columns.Add(dcAction);

            dcAction = new DataColumn("UserID");
            dtActions.Columns.Add(dcAction);

            dcAction = new DataColumn("GroupId");
            dtActions.Columns.Add(dcAction);

            dcAction = new DataColumn("ActionType");
            dtActions.Columns.Add(dcAction);

            dcAction = new DataColumn("ActionLevel");
            dtActions.Columns.Add(dcAction);

            dcAction = new DataColumn("Hold");
            dtActions.Columns.Add(dcAction);

            dcAction = new DataColumn("Rejected");
            dtActions.Columns.Add(dcAction);

            dcAction = new DataColumn("Complete");
            dtActions.Columns.Add(dcAction);

            dcAction = new DataColumn("CompletedBy");
            dtActions.Columns.Add(dcAction);

            dcAction = new DataColumn("DateAssigned");
            dtActions.Columns.Add(dcAction);

            dcAction = new DataColumn("AssignedBy");
            dtActions.Columns.Add(dcAction);

            dcAction = new DataColumn("DateComplete");
            dtActions.Columns.Add(dcAction);

            dcAction = new DataColumn("Comment");
            dtActions.Columns.Add(dcAction);

            dcAction = new DataColumn("FinalReviewGroupId");
            dtActions.Columns.Add(dcAction);

            dcAction = new DataColumn("FinalReviewUserId");
            dtActions.Columns.Add(dcAction);

            dcAction = new DataColumn("InvoiceCompleteLevel");
            dtActions.Columns.Add(dcAction);

            dcAction = new DataColumn("RoutingType");
            dtActions.Columns.Add(dcAction);

            dcAction = new DataColumn("RoutingValue");
            dtActions.Columns.Add(dcAction);

            dcAction = new DataColumn("Amount");
            dtActions.Columns.Add(dcAction);

            return dtActions;


        }

        #endregion

        #region Error-Message Logging

        private void LogDocToXML(DocumentEnt doc)
        {

            try
            {
                var stringwriter = new System.IO.StringWriter();
                var serializer = new XmlSerializer(doc.GetType());
                serializer.Serialize(stringwriter, doc);
                string xml = stringwriter.ToString();

                LogAPIMessage(doc.Header.DocumentId, "REQUEST", xml, "DocumentSvc", "SaveDocument", 0);
                //_docDA.LogError("CoreAPIDocumentSvc", "SaveDocument", xml, "Incoming Doc Object", "", "");
            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                _docDA.LogError("CoreAPIDocumentSvc", "SaveDocument", ex.Message, innerExMsg, "", "");
            }
        }

        public void LogAPIMessage(int DocumentID, string MessageType, string Message, string ScreenName, string Function, Int32 UserID)
        {
            try
            {
                _docDA.LogAPIMessage(DocumentID, MessageType, Message, ScreenName, Function, UserID);
            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                _docDA.LogError("CoreAPIDocumentSvc", "LogAPIMessage", ex.Message, innerExMsg, "", "");
            }
        }

        #endregion

        #region Helpers
                
        private string DocumentExists(int DocumentID)
        {
            string retMsg = "";
            try
            {
                retMsg = _docDA.DocumentExists(DocumentID);
            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                _docDA.LogError("CoreAPIDocumentSvc", "DocumentExists", ex.Message, innerExMsg, "", "");
                retMsg = ex.Message + "  --- " + innerExMsg;
            }

            return retMsg;
        }

        private string GetVendorTypeLevel(string VendorType, string tlVendorType1, string tlVendorType2, string tlVendorType3, string tlVendorType4, string tlVendorType5)
        {
            string vendorTypeLvl = "";
            string vndType = VendorType.ToUpper();
            if (tlVendorType1.ToUpper() == vndType)
            {
                vendorTypeLvl = "1";
            }
            else if (tlVendorType2.ToUpper() == vndType)
            {
                vendorTypeLvl = "2";
            }
            else if (tlVendorType3.ToUpper() == vndType)
            {
                vendorTypeLvl = "3";
            }
            else if (tlVendorType4.ToUpper() == vndType)
            {
                vendorTypeLvl = "4";
            }
            else if (tlVendorType5.ToUpper() == vndType)
            {
                vendorTypeLvl = "5";
            }

            return vendorTypeLvl;
        }

        private string GetEntityIDFromPrefix(string sAcct, string sPrefix)
        {
            string returnValue = string.Empty;
            //sPrefix can either be PrefixA, PrefixB or PrefixC
            short iLoop;
            short iCnt;
            short iNum;
            string sChr;
            string sEntID;

            if (!string.IsNullOrEmpty(sAcct))
            {

                if (sPrefix == "PrefixA")
                {
                    iLoop = 1;
                }
                else if (sPrefix == "PrefixB")
                {
                    iLoop = 2;
                }
                else
                {
                    iLoop = 3;
                }

                iCnt = 0;
                sEntID = "";

                for (iNum = 0; iNum < sAcct.Length; iNum++)
                {
                    sChr = sAcct.Substring(iNum, 1).ToUpper();
                    //if a character is not alphanumeric, then it must be an account separator
                    if (!Information.IsNumeric(sChr) && (Convert.ToChar(sChr) < 'A' || Convert.ToChar(sChr) > 'Z'))
                    {
                        iCnt++;
                        if (iCnt == iLoop)
                        {
                            break;
                        }
                        else
                        {
                            sEntID = sEntID + sChr;
                        }
                    }
                    else
                    {
                        sEntID = sEntID + sChr;
                    }
                }
                returnValue = sEntID;
            }

            return returnValue;

        }

        private RetMessage InitTimberLineSettings(int ConnectionID)
        {
            RetMessage retMsg = new RetMessage();
            try
            {
                // Core.API.TLSettings.Models.ActualTSCTL 
                __actualTSCTL = new TLSettings.Models.ActualTSCTL(ConnectionID);

                __apInvoiceSettings = tlSettings.GetAPInvoiceSettings(ConnectionID);
                //TLSettings.Models.APDistributionSettings 
                __apDistSettings = tlSettings.GetAPDistributionSettings(ConnectionID);

                DataTable dtAllSettings = tlSettings.GetTSCTLSettings(ConnectionID);

                DataRow[] specficSettingsRows = dtAllSettings.Select("FLDCODE = 9", "Section");
                //Core.API.TLSettings.Models.ActualTSCTL _actualTSCTL = new TLSettings.Models.ActualTSCTL(ConnectionID);
                _actualTSCTL.LoadGLSettings(specficSettingsRows);

                string vendorTypeLevel = "";
                vendorTypeLevel = GetVendorTypeLevel(adoApVendorRs.Rows[0]["Type"].ToString(), _actualTSCTL.VendorType1, _actualTSCTL.VendorType2, _actualTSCTL.VendorType3, _actualTSCTL.VendorType4, _actualTSCTL.VendorType5);
                apDistSettings.VendorTypeLevel = vendorTypeLevel;

                //DetermineJobAndPrefixes();
                //__actualTSCTL.strJobIn = UserJobIn;
                //__actualTSCTL.strPrefixIn = UserPrefixIn;

            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                _docDA.LogError("CoreAPIDocumentSvc", "InitTimberLineSettings", ex.Message, innerExMsg, ex.StackTrace, "");

                retMsg.Message = ex.Message + "  --- " + innerExMsg;
                retMsg.Message = new RetMessageFormatter("EXCEPTION", retMsg.Message).GetReturnMessage();
            }

            return retMsg;
        }

        private RetMessage FormatReturnMessage(string MessageType, string Message, int DocumentID)
        {
            RetMessage retMsg = new RetMessage();
            retMsg.Id = DocumentID;
            retMsg.Message = new RetMessageFormatter(MessageType, Message).GetReturnMessage();
            return retMsg;
        }

        private bool IsValiDate(DateTime dt)
        {
            bool isValid = false;
            if (dt != null)
            {
                DateTime dtTemp = DateTime.MinValue;
                DateTime.TryParse(dt.ToString(), out dtTemp);
                isValid = dtTemp != DateTime.MinValue;
            }

            return isValid;
        }

        private void SetUserInfo(int userid)
        {
            UserID = userid;
            LoginID = _docDA.GetUserId(userid);
           // LoadUserAndUserPermissions(userid);
        }

        private void LoadUserAndUserPermissions(int userid)
        {
            string sql = "Select * from tblUsers where UserIDNo = " + userid.ToString();
            dtUsers = _docDA.GetDataTable(sql);

            sql = "SELECT PrefixOrJob, PrefixOrJobNo FROM tblUserPermissions WHERE UserID=" + CommonFunctions.QNApos(userid) +
                " AND ConnectionID=" + ConnectionID.ToString();
            dtUserPermissions =  _docDA.GetDataTable(sql);
        }

        //private void DetermineJobAndPrefixes()
        //{
        //    if (dtUsers != null && dtUsers.Rows.Count > 0)
        //    {
        //        bool blnSupervisor = Convert.ToBoolean(dtUsers.Rows[0]["Supervisor"].ToString());
        //        bool blnAdminstrator = Convert.ToBoolean(dtUsers.Rows[0]["Administrator"].ToString());
        //        bool blnViewAll = Convert.ToBoolean(dtUsers.Rows[0]["ViewAll"].ToString());
        //        if (blnSupervisor || blnAdminstrator || blnViewAll)
        //        {
        //            if (dtUserPermissions != null && dtUserPermissions.Rows.Count > 0)
        //                DetermineDerivedUserJobsAndPrefixes(LoginID, ConnectionID);
        //            else
        //                DetermineUserJobsAndPrefixes(LoginID, ConnectionID);
        //        }
        //    }
        //}

        //private void DetermineDerivedUserJobsAndPrefixes(string userId, int connectionId)
        //{
        //    DataTable dt = new DataTable();
        //    string prefixOrJobNo = string.Empty;
        //    string sql = "SELECT PrefixOrJob, PrefixOrJobNo FROM tblUserPermissions WHERE UserID=" + CommonFunctions.QNApos(userId) +
        //        " AND ConnectionID=" + connectionId.ToString();

        //    UserJobIn = string.Empty;
        //    UserPrefixIn = string.Empty;
        //    dt = _docDA.GetDataTable(sql);

        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            prefixOrJobNo = CommonFunctions.QNAposCom(CommonFunctions.TestNullString(dr["PrefixOrJobNo"]).Trim());
        //            if (CommonFunctions.TestNullString(dr["PrefixOrJob"]).Trim().ToUpper() == "PREFIX")
        //                UserPrefixIn += prefixOrJobNo;
        //            else
        //                UserJobIn += prefixOrJobNo;
        //        }
        //        if (!string.IsNullOrEmpty(UserJobIn))
        //            UserJobIn = UserJobIn.TrimEnd(new char[] { ' ', ',' });
        //        if (!string.IsNullOrEmpty(UserPrefixIn))
        //            UserPrefixIn = UserPrefixIn.TrimEnd(new char[] { ' ', ',' });
        //    }
        //}

        //private bool DetermineUserJobsAndPrefixes(string sUsr, int lConnID)
        //{
        //    bool returnValue = true;
        //    //===================================================================
        //    //pass in User ID and return SQL 'IN' conditions for a query
        //    string sIn;
        //    string sQry;
        //    string sOr;
        //    string[] sGrps = new string[3];
        //    string sPfxIn;
        //    string sJbIn;
        //    string sTypIn;
        //    string[] sGroups;
        //    DataTable dt;
        //    DataTable tmpDt;
        //    int lLoop;
        //    int lGrpLoop;
        //    int iPrefixLen;
        //    int iJobLen;
        //    int iAcctBaseLen = 0;

        //    string sMemType = string.Empty;
        //    string sMemValue = string.Empty;
        //    string sPrefixLevel = string.Empty;
        //    string sPrefix = string.Empty;

        //    sPrefixLevel = this._actualTSCTL.GLPrefixDescription;

        //    if (_actualTSCTL.GLPrefixALength > 0)
        //    {
        //        iAcctBaseLen = _actualTSCTL.GLBaseLength + 1;
        //        if (_actualTSCTL.GLSuffixLength > 0)
        //        {
        //            iAcctBaseLen = iAcctBaseLen + _actualTSCTL.GLSuffixLength + 1;
        //        }
        //    }

        //    iPrefixLen = 0;
        //    iJobLen = 0;

        //    //we need to pad the prefixes with spaces to allow for the fact that PrefixA can have less than the allotted characters
        //    if (_actualTSCTL.GLPrefixType == "PrefixA")
        //        iPrefixLen = _actualTSCTL.GLPrefixALength;
        //    else if (_actualTSCTL.GLPrefixType == "PrefixB")
        //        iPrefixLen = _actualTSCTL.GLPrefixALength + _actualTSCTL.GLPrefixABLength + 1;
        //    else if (_actualTSCTL.GLPrefixType == "PrefixC")
        //        iPrefixLen = _actualTSCTL.GLPrefixALength + _actualTSCTL.GLPrefixABLength + _actualTSCTL.GLPrefixABCLength + 2;
        
        //    //we need to pad the jobs with spaces to allow for the fact that jobs can have less than the allotted characters
        //    if (_actualTSCTL.JobSec1Length > 0)
        //    {
        //        iJobLen = _actualTSCTL.JobSec1Length;
        //        if (_actualTSCTL.JobSec2Length > 0)
        //            iJobLen = iJobLen + _actualTSCTL.JobSec2Length + 1;
        //        if (_actualTSCTL.JobSec3Length > 0)
        //            iJobLen = iJobLen + _actualTSCTL.JobSec3Length + 1;
        //    }

        //    //first determine what user groups user is a member of
        //    sGrps[0] = this.GetGroupsForUserByActionType(sUsr, "Approve", "All");
        //    sGrps[1] = this.GetGroupsForUserByActionType(sUsr, "Data Entry", "All");
        //    sGrps[2] = this.GetGroupsForUserByActionType(sUsr, "Final Review", "All");

        //    sIn = string.Empty;
        //    for (lGrpLoop = 0; lGrpLoop <= 2; lGrpLoop++)
        //    {
        //        if (!string.IsNullOrEmpty(sGrps[lGrpLoop]))
        //        {
        //            sGroups = sGrps[lGrpLoop].Split('|');
        //            for (lLoop = 0; lLoop < sGroups.Length; lLoop += 3)
        //            {
        //                if (!sIn.Contains(sGroups[lLoop]))//if (Strings.InStr(sIn, ", " + sGroups[lLoop] + ",", 0) == 0)
        //                {
        //                    sIn = sIn + CommonFunctions.QCom(sGroups[lLoop]);
        //                }
        //            }
        //        }
        //    }

        //    sIn = sIn.Trim().TrimEnd(new char[] { ',' });
        //    sOr = "";
        //    //======================
        //    //now find the approval groups that user is a member of
        //    //Action 1 is always data enter;
        //    sQry = "SELECT GroupID FROM tblApprovalGroups WHERE ";
        //    for (lLoop = 1; lLoop <= 10; lLoop++)
        //    {
        //        if (!string.IsNullOrEmpty(sIn))
        //            sQry = sQry + sOr + "(GroupID" + lLoop.ToString().PadLeft(2, '0') + " IN (" + sIn + ") OR UserID" + lLoop.ToString().PadLeft(2, '0') + "=" + CommonFunctions.QNApos(sUsr) + ")";
        //        else
        //            sQry = sQry + sOr + "UserID" + lLoop.ToString().PadLeft(2, '0') + "=" + CommonFunctions.QNApos(sUsr);

        //        sOr = " OR ";
        //    }

        //    dt = new DataTable();
        //    dt = _docDA.GetDataTable(sQry);

        //    if (dt != null && dt.Rows.Count > 0)//if (aRs.EOF)
        //    {
        //        sIn = "";
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            sIn = sIn + CommonFunctions.QCom(dr["GroupId"]);//CommonFunctions.QCom(aRs.Fields["GroupID"]);
        //        }
        //        sIn = sIn.Trim().Substring(0, sIn.Length - 2);

        //        //now find Jobs, Prefixes and Invoice Types associate with these approval groups
        //        sMemType = "";

        //        sMemValue = "";

        //        //2.3.33===========================================
        //        //sQry = "SELECT MemberType, MemberValue FROM tblApprovalGroupMembers WHERE (ConnectionID=0 OR ConnectionID=" & clsTimberlineConnection.ConnectionID & ") AND GroupID IN (" & sIn & ")"
        //        //sQry = "SELECT MemberType, MemberValue FROM tblApprovalGroupMembers WHERE (ConnectionID=0 OR ConnectionID=" + lConnID + ") AND GroupID IN (" + sIn + ") AND MemberType <> 'G'";
        //        sQry = "SELECT MemberType, MemberValue FROM tblApprovalGroupMembers WHERE (ConnectionID=0 OR ConnectionID=" + lConnID + ") AND GroupID IN (" + sIn + ") ";
        //        //=================================================


        //        dt = new DataTable();
        //        dt = _docDA.GetDataTable(sQry);

        //        if (dt != null && dt.Rows.Count > 0)//if (aRs.EOF)
        //        {
        //            sJbIn = "";
        //            sPfxIn = "";
        //            sTypIn = "";
        //            UserJobIn = "";
        //            UserPrefixIn = "";
        //            //sTypeIn = "";
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                sMemType = dr["MemberType"].ToString().Trim();//Strings.Trim(aRs.Fields["MemberType"].Value);
        //                sMemValue = dr["MemberValue"].ToString().Trim();//Strings.Trim(aRs.Fields["MemberValue"].Value);
        //                switch (sMemType)
        //                {
        //                    case "J": //Job
        //                        if (!sJbIn.Contains(sMemValue))//(Strings.InStr(sJbIn, sMemValue, 0) == 0)
        //                        {
        //                            sJbIn = sJbIn + CommonFunctions.QNAposCom(sMemValue);
        //                        }
        //                        break;
        //                    case "C":

        //                        sPfxIn = sPfxIn + CommonFunctions.QNAposCom(sMemValue); //GL Prefix
        //                        break;
        //                    case "G":

        //                        if (iPrefixLen > 0 && sMemValue.Length > iPrefixLen)
        //                            sPfxIn = sPfxIn + CommonFunctions.QNAposCom(sMemValue.Substring(0, iPrefixLen)); //GL Prefix
        //                        break;
        //                    case "I":

        //                        sTypIn = sTypIn + CommonFunctions.QNAposCom(sMemValue); //Invoice Type
        //                        break;
        //                    case "O": //Vendor|Job
        //                        sMemValue = sMemValue.Substring(Strings.InStr(sMemValue, "|", 0) + 1 - 1);
        //                        if (!sJbIn.Contains(sMemValue))//if (Strings.InStr(sJbIn, sMemValue, 0) == 0)
        //                        {
        //                            sJbIn = sJbIn + CommonFunctions.QNAposCom(sMemValue);
        //                        }
        //                        break;
        //                    case "R":
        //                    case "Y":
        //                    case "T":
        //                    case "S":
        //                        sMemValue = sMemValue.Substring(0, Strings.InStr(sMemValue, "|", 0) - 1);
        //                        if (!sJbIn.Contains(sMemValue))//if (Strings.InStr(sJbIn, sMemValue, 0) == 0)
        //                        {
        //                            sJbIn = sJbIn + CommonFunctions.QNAposCom(sMemValue);
        //                        }
        //                        break;
        //                    case "U": //Job Authorization
        //                        sMemValue = GetJobFromAuthorization(sMemValue, sJbIn);
        //                        sJbIn = sMemValue;
        //                        break;
        //                }
        //                //==============================================
        //            }
        //            if (!string.IsNullOrEmpty(sJbIn))//if (sJbIn > "")
        //            {
        //                UserJobIn = sJbIn.Trim().Substring(0, sJbIn.Length - 2);
        //            }
        //            if (!string.IsNullOrEmpty(sPfxIn))//if (sPfxIn > "")
        //            {
        //                UserPrefixIn = sPfxIn.Trim().Substring(0, sPfxIn.Length - 2);
        //            }
        //            //if (!string.IsNullOrEmpty(sTypIn))//if (sTypIn > "")
        //            //{
        //            //    sTypeIn = sTypIn.Trim().Substring(0, sTypIn.Length - 2);
        //            //}

        //            if (TestForExpenseAccountRouting())
        //            {
        //                sQry = "SELECT DISTINCT LEFT(MemberValue, LEN(MemberValue) - " + (iAcctBaseLen) + ") AS GLPrefix FROM tblApprovalGroupMembers WHERE (ConnectionID=0 OR ConnectionID=" + lConnID + ") AND GroupID IN (" + sIn + ") AND MemberType = 'G'";

        //                if (!string.IsNullOrEmpty(UserPrefixIn))
        //                    sQry += " AND LEFT(MemberValue, LEN(MemberValue) - " + (iAcctBaseLen) + ") NOT IN (" + UserPrefixIn + ")";

        //                dt = new DataTable();
        //                dt = _docDA.GetDataTable(sQry);

        //                foreach (DataRow dr in dt.Rows)
        //                {
        //                    sPfxIn += CommonFunctions.QNApos(dr["GLPrefix"].ToString().Trim());
        //                }

        //                if (!string.IsNullOrEmpty(sPfxIn))
        //                    sPfxIn = sPfxIn.Substring(0, sPfxIn.Length - 2);
        //            }
        //        }
        //    }
        //    returnValue = true;
        //    return returnValue;
        //}

        private string GetGroupsForUserByActionType(string sUser, string sActType, string sUsrType) // done
        {
            //// Linq2SQL
            //DalTsdbLts.tsdbDataContext ldc = new DalTsdbLts.tsdbDataContext(
            //    TimberScan.Properties.Extended.Default.TimberScanConnectionString);

            ////sUsrType - Primary, Alternate, All
            ////sActType - Approve, Review, Data Entry, Final Review, All
            ////this function retrieves all groups that a user is a member of for a given Action Type
            ////RETURNS all appropriate user group IDs & descriptions and Types (P or A) separated by "|"
            ////ie 1|Gold Coast Data Entry|P|5|Main Data Entry|A|10|Corporate Data Entry|P
            StringBuilder sResult = new StringBuilder();
            string sActionType, sQry;

            
            DataTable dtGroups = new DataTable();

            //Export is an alternate action for Final Review and is always at the same level
            //To get group members for Export Action we have to look to Final Review group
            if (sActType == "Export")
            {
                sActionType = "Final Review";
            }
            else
            {
                sActionType = sActType;
            }

            sQry = "SELECT tblUserGroups.UserGroupID, tblUserGroups.GroupDescription, tblUserGroupMembers.UserType FROM tblUserGroups" +
                " INNER JOIN tblUserGroupMembers ON tblUserGroups.UserGroupID = tblUserGroupMembers.UserGroupID" +
                " WHERE tblUserGroupMembers.UserID=" + CommonFunctions.QNApos(sUser);

            if (sActType != "All")
            {
                sQry = sQry + " AND tblUserGroups.ActionType=" + CommonFunctions.QNApos(sActionType);
            }
            if (sUsrType != "All")
            {
                sQry = sQry + " AND tblUserGroupMembers.UserType=" + CommonFunctions.QNApos(sUsrType);
            }

            dtGroups = _docDA.GetDataTable(sQry);

            if (dtGroups != null && dtGroups.Rows.Count > 0)
            {
                
                foreach (DataRow group in dtGroups.Rows)
                {
                    sResult.Append(string.Format("{0}|{1}|{2}|", group["UserGroupID"], group["GroupDescription"], group["UserType"].ToString().Substring(0, 1)));
                }
            }
            return sResult.ToString().TrimEnd(new char[] { '|' });
        }

        private string GetJobFromAuthorization(string sAuth, string sJobIn)
        {
            string returnValue = string.Empty;
            string sQry;
            string sJob;
            string sNewJobIn = "";
            DataTable dt = null;
            bool bFound;
            //===================================

            try
            {
                if (!string.IsNullOrEmpty(sAuth))
                {
                    sNewJobIn = sJobIn;
                    bFound = false;
                    
                    sQry = "SELECT Job FROM JCM_MASTER__JOB WHERE ConnectionID = " + ConnectionID.ToString() + " AND [Authorization] = " + CommonFunctions.QNApos(sAuth);
                    dt = new DataTable();
                    dt = _docDA.GetDataTableTSync(sQry, ConnectionID);
                    
                    if (dt != null && dt.Rows.Count > 0)//if (aRs.EOF == false)
                        bFound = true;
                    foreach (DataRow dr in dt.Rows)
                    {
                        sJob = CommonFunctions.QNApos(dr["Job"].ToString() + "").ToString();
                        if (!string.IsNullOrEmpty(sJob) && !sNewJobIn.Contains(sJob))
                        {
                            sNewJobIn = sNewJobIn + sJob + ", ";
                        }
                    }
                    returnValue = sNewJobIn;
                }
            }
            catch
            {
                throw;
            }

            return returnValue;
        }

        private bool TestForExpenseAccountRouting()
        {
            bool success = false;
            DataTable dt = new DataTable();
            string sql = string.Empty;
            dt = _docDA.GetDataTable("SELECT FullGLAccount FROM tblRouting");
            if (dt != null && dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(CommonFunctions.TestNullNumber(dt.Rows[0]["FullGLAccount"])) > 0)
                    success = true;
            }
            return success;
        }

        internal void RunTScanSql(string sql)
        {
            _docDA.RunTScanSQL(sql);
        }

        private bool IsNewCostCodeAGroupCostCode(string NwCstCd)
        {
            bool NewCostCodeIsAGroupCostCode = false;
            string[] CstCdSections = NwCstCd.Split('-');
            if (CstCdSections.Length > 1)
            {
                for (int i = 1; i < CstCdSections.Length; i++)
                {
                    if (CommonFunctions.StringHasAllZeroes(CstCdSections[i]))
                    {
                        NewCostCodeIsAGroupCostCode = true;
                    }
                }
            }
            return NewCostCodeIsAGroupCostCode;
        }
       
        #endregion

        #region Untilized

        public string StripPunctuation(string sItm, bool bUncludePunctuation)
        {
            string returnValue = null;
            int iPos;
            char sChr;
            string sNewItem;

            sNewItem = "";

            for (iPos = 1; iPos <= sItm.Length; iPos++)
            {
                sChr = System.Convert.ToChar(sItm.ToUpper().Substring(iPos - 1, 1));
                if ((sChr >= 'A' && sChr <= 'Z') || (sChr >= '0' && sChr <= '9'))
                {
                    sNewItem = sNewItem + sChr;
                }
                else
                {
                    if (bUncludePunctuation == true && sChr > ' ')
                    {
                        sNewItem = sNewItem + "|";
                    }
                }
            }
            returnValue = sNewItem;
            return returnValue;
        }

        //private void SetVendorInfo(int DocumentID)
        //{
        //    this.adoApVendorRs = _docDA.GetVendorInfo(DocumentID);
        //}

        //string strGLPrefixFormat = "";
        //int mvarGLPrefixALength = 0;
        //string mvarGLPrefixADesc = "";
        //string mvarGLPrefixAPunct;
        //int intGLPrefixCnt = 0;
        //int mvarGLPrefixABLength = 0;
        //string mvarGLPrefixABDesc = "";
        //string mvarGLPrefixABPunct = "";
        //int mvarGLPrefixABCLength = 0;
        //string mvarGLPrefixABCDesc = "";
        //string mvarGLPrefixABCPunct = "";
        //int mvarGLBaseLength = 0;
        //string mvarGLBasePunct = "";
        //string strGLBaseAccountFormat = "";
        //int mvarGLSuffixLength = 0;
        //string strGLFullAccountFormat = "";


        //private bool GetGLSettings(DataRow[] glSettings)
        //{
        //    bool returnValue = true;
        //    string sSepChar = "";

        //    //if (String.Compare(strTLVersion, "00009.00004.00000") >= 0)
        //    //{

        //    try
        //    {
        //        strGLPrefixFormat = "";

        //        if (glSettings != null && glSettings.Length > 0)
        //        {
        //            foreach (DataRow dr in glSettings)
        //            {
        //                if (System.Convert.ToInt32(dr["Section"]) == 1)
        //                {
        //                    mvarGLPrefixALength = System.Convert.ToInt16(dr["CSLEN"]);
        //                    if (mvarGLPrefixALength > 0)
        //                    {
        //                        mvarGLPrefixADesc = dr["CNDESC"].ToString();
        //                        mvarGLPrefixAPunct = dr["CNPUNCT"].ToString();
        //                        strGLPrefixFormat = string.Empty.PadLeft(mvarGLPrefixALength, 'x');// new string('x', mvarGLPrefixALength);
        //                        sSepChar = mvarGLPrefixAPunct;
        //                        intGLPrefixCnt = 1;
        //                    }
        //                }
        //                else if (System.Convert.ToInt32(dr["Section"]) == 2)
        //                {
        //                    mvarGLPrefixABLength = System.Convert.ToInt16(dr["CSLEN"]);
        //                    if (mvarGLPrefixABLength > 0)
        //                    {
        //                        mvarGLPrefixABDesc = dr["CNDESC"].ToString();
        //                        mvarGLPrefixABPunct = dr["CNPUNCT"].ToString();
        //                        strGLPrefixFormat = strGLPrefixFormat + mvarGLPrefixAPunct + string.Empty.PadLeft(mvarGLPrefixABLength, 'x');//new string('x', mvarGLPrefixABLength);
        //                        sSepChar = mvarGLPrefixABPunct;
        //                        intGLPrefixCnt = 2;
        //                    }
        //                }
        //                else if (System.Convert.ToInt32(dr["Section"]) == 3)
        //                {
        //                    mvarGLPrefixABCLength = System.Convert.ToInt16((dr["CSLEN"]));
        //                    if (mvarGLPrefixABCLength > 0)
        //                    {
        //                        mvarGLPrefixABCDesc = dr["CNDESC"].ToString();
        //                        mvarGLPrefixABCPunct = dr["CNPUNCT"].ToString();
        //                        strGLPrefixFormat = strGLPrefixFormat + mvarGLPrefixABPunct + string.Empty.PadLeft(mvarGLPrefixABCLength, 'x');//new string('x', mvarGLPrefixABCLength);
        //                        sSepChar = mvarGLPrefixABCPunct;
        //                        intGLPrefixCnt = 3;
        //                    }
        //                }
        //                else if (System.Convert.ToInt32(dr["Section"]) == 4)
        //                {



        //                    mvarGLBaseLength = dr["CSLEN"] == null || dr["CSLEN"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CSLEN"]);
        //                    mvarGLBasePunct = dr["CNPUNCT"].ToString();
        //                    strGLBaseAccountFormat = string.Empty.PadLeft(mvarGLBaseLength, 'x') + mvarGLBasePunct;
        //                }
        //                else if (System.Convert.ToInt32(dr["Section"]) == 5)
        //                {
        //                    mvarGLSuffixLength = System.Convert.ToInt32(dr["CSLEN"]);
        //                    strGLBaseAccountFormat = strGLBaseAccountFormat + string.Empty.PadLeft(mvarGLSuffixLength, 'x');//new string('x', mvarGLSuffixLength);
        //                }
        //            }
        //        }
        //        strGLFullAccountFormat = strGLPrefixFormat + sSepChar + strGLBaseAccountFormat;
        //    }
        //    catch (Exception e)
        //    {
        //        throw;
        //    }


        //    // }

        //    return returnValue;
        //}

        //public string DetermineBaseAccount(string sAcct, bool bIncludeSuffix, bool makeSureBaseAccountIsValid)
        //{

        //    if (string.IsNullOrWhiteSpace(sAcct))
        //    {
        //        return string.Empty;
        //    }
        //    string returnValue = null;
        //    string sSuffix;
        //    string sNewAcct;
        //    string sBaseAcct;
        //    string sReturnAcct;

        //    //modIniFiles.LogFile("DetermineBaseAccount");
        //    sNewAcct = StripPunctuation(sAcct, false);
        //    sBaseAcct = "";

        //    sSuffix = "";
        //    if (mvarGLPrefixALength == 0) //no prefix
        //    {
        //        if (mvarGLSuffixLength > 0)
        //        {
        //            sSuffix = Strings.Right(sNewAcct, mvarGLSuffixLength);
        //            sBaseAcct = Strings.Left(sNewAcct, sNewAcct.Length - mvarGLSuffixLength); //sNewAcct.Substring(0, sNewAcct.Length - mvarGLSuffixLength);
        //        }
        //        else
        //        {
        //            sBaseAcct = sNewAcct;
        //        }
        //    }
        //    else
        //        if (mvarGLPrefixALength > 0)
        //        {
        //            sNewAcct = Strings.Right(sNewAcct, mvarGLBaseLength + mvarGLSuffixLength);
        //            sBaseAcct = Strings.Left(sNewAcct, mvarGLBaseLength);//sNewAcct.Substring(0, mvarGLBaseLength);
        //            if (mvarGLSuffixLength > 0)
        //            {
        //                sSuffix = Strings.Right(sNewAcct, mvarGLSuffixLength);
        //            }
        //        }
        //        else
        //        {
        //            if (mvarGLSuffixLength > 0)
        //            {
        //                sSuffix = Strings.Right(sNewAcct, mvarGLSuffixLength);
        //                sBaseAcct = Strings.Left(sNewAcct, sNewAcct.Length - mvarGLBaseLength);
        //            }
        //            else
        //            {
        //                sBaseAcct = sNewAcct;
        //            }
        //        }

        //    if (sSuffix.Length > 0 && bIncludeSuffix == true)
        //    {
        //        sReturnAcct = sBaseAcct + mvarGLBasePunct + sSuffix;
        //    }
        //    else
        //    {
        //        sReturnAcct = sBaseAcct;
        //    }
        //    //only if a suffix is supposed to be included can we test for a valid base accouny
        //    if (bIncludeSuffix == true && makeSureBaseAccountIsValid)
        //    {
        //        Core.API.TLSettings.Models.ActualTSCTL _tsctl = new TLSettings.Models.ActualTSCTL();
        //        if (_tsctl.TestForValidBaseAccount(sReturnAcct) == false)
        //        {
        //            sReturnAcct = "";
        //        }
        //    }

        //    returnValue = sReturnAcct;
        //    return returnValue;
        //}

        //public string ApproveDocument(DocumentEnt doc)
        //{
        //    RetMessage retMsg = new RetMessage(); ;
        //    string title = "SUCCESS";

        //    // TODO - Route Invoice and Threshorder
        //    // TODO -- Validation for Invoice Header and Distribution - get it from TeskOKToSAve

        //    try
        //    {
        //        retMsg.Message = ValidateInvoiceHeader(doc);
        //        if (string.IsNullOrEmpty(retMsg.Message))
        //        {
        //            // TODO -- Verify Discount Amount

        //            // TODO -- SetDetailAmount

        //            // Determine Amount vs Pretax -- ask HH2 to supply Either Amount or PreTax based on Config, to avoid API to determine 
        //            float invoiceAmount = 0;
        //            float invoicePreTaxAmount = 0;
        //            float invoiceTaxAmount = 0;
        //            if (doc != null)
        //            {
        //                if (doc.Header != null && doc.Header.HeaderStatus.ToUpper().Equals("U"))
        //                {
        //                    if (doc.Header.PreTaxAmount > 0)
        //                    {
        //                        doc.Header.Amount = doc.Header.PreTaxAmount + doc.Header.TaxAmount;
        //                        invoicePreTaxAmount = doc.Header.PreTaxAmount;
        //                    }
        //                }
        //            }
        //            retMsg.Message = _docDA.ApproveDocument(doc);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
        //        _docDA.LogError("CoreAPIDocumentSvc", "ApproveDocument", ex.Message, innerExMsg, "", "");
        //        title = "EXCEPTION";
        //        if (retMsg == null)
        //            retMsg = new RetMessage();
        //        retMsg.Message = ex.Message + "  --- " + innerExMsg;
        //        retMsg.Message = new RetMessageFormatter(title, retMsg.Message).GetReturnMessage();
        //    }

        //    if (string.IsNullOrEmpty(retMsg.Message))
        //    {
        //        retMsg.Id = doc.DocumentID;
        //        retMsg.Message = new RetMessageFormatter("SUCCESS", "Document approved successfully.").GetReturnMessage();
        //    }
        //    return retMsg.Message;
        //}

        //private int CountAccountDecimalPoints(String sAcct, Core.API.TLSettings.Models.ActualTSCTL _actualTSCTL)
        //{
        //    int iCnt = 0;
        //    int iPos = 1;

        //    while (sAcct.IndexOf(_actualTSCTL.GLBasePunct, iPos) > 0)
        //    {
        //        iCnt++;
        //        iPos = sAcct.IndexOf(_actualTSCTL.GLBasePunct, iPos) + 1;

        //        if (iPos >= sAcct.Length)
        //        {
        //            break;
        //        }
        //    }
        //    return iCnt;
        //}

        //private bool TestValidCharacters(string sAcct, bool bIgnorePunctuation, Core.API.TLSettings.Models.ActualTSCTL _actualTSCTL)
        //{
        //    int iPos = 0;
        //    string sChr = String.Empty;
        //    string sTestPunct = String.Empty;
        //    bool returnValue = false;

        //    if (bIgnorePunctuation == false)
        //    {
        //        if (_actualTSCTL.GLPrefixALength > 0)
        //        {
        //            sTestPunct = _actualTSCTL.GLPrefixAPunct;
        //            if (_actualTSCTL.GLPrefixABLength > 0 && Strings.InStr(sTestPunct, _actualTSCTL.GLPrefixABCPunct, CompareMethod.Binary) == 0)
        //            {
        //                sTestPunct += _actualTSCTL.GLPrefixABPunct;
        //            }
        //            if (_actualTSCTL.GLPrefixABCLength > 0 && Strings.InStr(sTestPunct, _actualTSCTL.GLPrefixABCPunct, CompareMethod.Binary) == 0)
        //            {
        //                sTestPunct += _actualTSCTL.GLPrefixABCPunct;
        //            }
        //        }
        //        if (_actualTSCTL.GLSuffixLength > 0)
        //        {
        //            sTestPunct += _actualTSCTL.GLBasePunct;   //GLBasePunct is always "."
        //        }
        //    }

        //    for (iPos = 1; iPos <= sAcct.Length; iPos++)
        //    {
        //        sChr = Strings.Mid(sAcct, iPos, 1);

        //        if (sTestPunct.Length > 0)
        //        {
        //            if (!((String.Compare(sChr, "A") >= 0 && String.Compare(sChr, "Z") <= 0) || (String.Compare(sChr, "0") >= 0 && String.Compare(sChr, "9") <= 0) || String.Compare(sChr, " ") == 0 || sTestPunct.IndexOf(sChr) > 0))
        //            {
        //                returnValue = false;
        //                return returnValue;
        //            }
        //        }
        //        else
        //        {
        //            if (!((String.Compare(sChr, "A") >= 0 && String.Compare(sChr, "Z") <= 0) || (String.Compare(sChr, "0") >= 0 && String.Compare(sChr, "9") <= 0) || String.Compare(sChr, " ") == 0))
        //            {
        //                returnValue = false;
        //                return returnValue;
        //            }
        //        }
        //    }
        //    returnValue = true;
        //    return returnValue;
        //}

        //public string FormatFullGLAccount(ref string sAct, Boolean bImportedRec, DataTable adoApVendorRs, Core.API.TLSettings.Models.ActualTSCTL _actualTSCTL, TLSettings.Models.APDistributionSettings apDistSettings, Int32 ConnectionID)
        //{
        //    string returnValue = String.Empty;
        //    string sNewAcct = String.Empty;
        //    string sNewItem = String.Empty;
        //    string sSuffix = String.Empty;
        //    string sAcct = String.Empty;
        //    string[] sSections;
        //    int iMaxLen = 0;
        //    int iLoop = 0;
        //    int iMinLen = 0;
        //    int iMinLenSfx = 0;
        //    int iMaxLenSfx = 0;
        //    int iPos = 0;
        //    int iDecPnts = 0;

        //    sAcct = sAct.Trim();

        //    if (sAcct == "")
        //    {
        //        return returnValue;
        //    }

        //    if (_actualTSCTL.GLSuffixLength > 0)
        //    {
        //        iDecPnts = CountAccountDecimalPoints(sAcct, _actualTSCTL);
        //        if (iDecPnts > 1)
        //        {
        //            returnValue = "Incorrect account format format.|Only one decimal point is allowed.|Correct format is " + _actualTSCTL.GLFullAccountFormat + ".|Reenter account.";
        //            return returnValue;
        //        }

        //        iPos = sAcct.IndexOf(_actualTSCTL.GLBasePunct.ToString());
        //        if ((iPos + 1) == sAcct.Length)//if the GL base punctuation is the last character in the account
        //        {
        //            sNewItem = sAcct.Substring(0, iPos);
        //            sSuffix = string.Empty.PadLeft(_actualTSCTL.GLSuffixLength, Convert.ToChar("0"));
        //        }
        //        else if (iPos > 0)
        //        {
        //            sNewItem = sAcct.Substring(0, iPos);
        //            sSuffix = (sAcct.Substring(iPos + 1) + "".PadLeft(_actualTSCTL.GLSuffixLength, '0')).Substring(0, _actualTSCTL.GLSuffixLength); // Strings.Left(Strings.Mid(sAcct , iPos + 1) + "".PadLeft(this.GLSuffixLength , '0') , this.GLSuffixLength);

        //        }
        //        else
        //        {
        //            if (bImportedRec == true)
        //            {
        //                sNewItem = this.StripGLAccountPunctuation(sAcct, false, _actualTSCTL);
        //                sSuffix = Strings.Right(sNewItem, _actualTSCTL.GLSuffixLength);
        //                sNewItem = Strings.Left(sNewItem, sNewItem.Length - _actualTSCTL.GLSuffixLength);
        //            }
        //            else
        //            {
        //                if (TestValidCharacters(sAcct, false, _actualTSCTL) == false)
        //                {
        //                    returnValue = "Incorrect account format.|Invalid character.|Correct format is " + _actualTSCTL.GLFullAccountFormat + ".|Renter account.";
        //                    return returnValue;
        //                }
        //                sNewItem = StripGLAccountPunctuation(sAcct, false, _actualTSCTL);

        //                if (_actualTSCTL.GLPrefixALength > 0)
        //                {
        //                    iMaxLen = _actualTSCTL.GLPrefixALength + _actualTSCTL.GLPrefixABLength + _actualTSCTL.GLPrefixABCLength + _actualTSCTL.GLBaseLength;
        //                    iMinLen = 1 + _actualTSCTL.GLPrefixABLength + _actualTSCTL.GLPrefixABCLength + _actualTSCTL.GLBaseLength;
        //                    iMaxLenSfx = iMaxLen + _actualTSCTL.GLSuffixLength;
        //                    iMinLenSfx = iMinLen + _actualTSCTL.GLSuffixLength;
        //                }
        //                else
        //                {
        //                    iMaxLen = _actualTSCTL.GLBaseLength;
        //                    iMinLen = 1;
        //                    iMaxLenSfx = iMaxLen + _actualTSCTL.GLSuffixLength;
        //                    iMinLenSfx = iMinLen + _actualTSCTL.GLSuffixLength;
        //                }

        //                if (sNewItem.Length < iMinLen)
        //                {
        //                    returnValue = "Incorrect account format format.|Insufficient digits entered.|Correct format is " + _actualTSCTL.GLFullAccountFormat + ".|Reenter account.";
        //                    return returnValue;
        //                }

        //                if (sNewItem.Length > iMaxLenSfx)
        //                {
        //                    returnValue = "Incorrect account format.|Too many digits before decimal point.|Correct format is " + _actualTSCTL.GLFullAccountFormat + ".|Renter account.";
        //                    return returnValue;
        //                }

        //                if (sNewItem.Length <= iMaxLen)
        //                {
        //                    sSuffix = string.Empty.PadRight(_actualTSCTL.GLSuffixLength, Convert.ToChar("0"));
        //                }
        //                else
        //                {
        //                    sSuffix = Strings.Right(sNewItem, _actualTSCTL.GLSuffixLength);
        //                    sNewItem = Strings.Left(sNewItem, sNewItem.Length - _actualTSCTL.GLSuffixLength);
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        sNewItem = sAcct;
        //    }

        //    if (_actualTSCTL.GLPrefixALength > 0)
        //    {
        //        sNewItem = StripGLAccountPunctuation(sNewItem, true, _actualTSCTL);
        //    }
        //    else
        //    {
        //        if (TestValidCharacters(sNewItem, true, _actualTSCTL) == false)
        //        {
        //            returnValue = "Incorrect account format.|Invalid character.|Correct format is " + _actualTSCTL.GLFullAccountFormat + ".|Renter account.";
        //            return returnValue;
        //        }
        //    }

        //    if (sNewItem == "Invalid character")
        //    {
        //        returnValue = "Incorrect account format.|Invalid character.|Correct format is " + _actualTSCTL.GLFullAccountFormat + ".|Renter account.";
        //        return returnValue;
        //    }

        //    if (!string.IsNullOrEmpty(sSuffix))
        //    {
        //        if (!TestValidCharacters(sSuffix, true, _actualTSCTL))
        //        {
        //            returnValue = "Incorrect account format.|Invalid character.|Correct format is " + _actualTSCTL.GLFullAccountFormat + ".|Renter account.";
        //            return returnValue;
        //        }
        //        sNewItem += "|" + sSuffix;
        //    }

        //    sSections = sNewItem.Split(Convert.ToChar("|"));
        //    sNewAcct = string.Empty;

        //    if (sSections != null && (sSections.Length - 1) > 0) //muliple prefix levels formatted
        //    {
        //        sNewItem = string.Empty;
        //        //reminder - we don't decrement intGLPrefixCnt becuase sSections() is zero based and this allows for base account
        //        if (_actualTSCTL.GLSuffixLength > 0)
        //        {
        //            if ((sSections.Length - 1) < (_actualTSCTL.GLPrefixCnt + 1))
        //            {
        //                sNewItem = "Incorrect account format format.|Insufficient digits entered.|Correct format is " + _actualTSCTL.GLFullAccountFormat + ".|Reenter account.";
        //            }
        //            else
        //                if ((sSections.Length - 1) > (_actualTSCTL.GLPrefixCnt + 1))
        //                {
        //                    sNewItem = "Incorrect account format.|Invalid character.|Correct format is " + _actualTSCTL.GLFullAccountFormat + ".|Renter account.";
        //                }
        //        }
        //        else
        //        {
        //            if ((sSections.Length - 1) < (_actualTSCTL.GLPrefixCnt))
        //            {
        //                sNewItem = "Incorrect account format format.|Insufficient digits entered.|Correct format is " + _actualTSCTL.GLFullAccountFormat + ".|Reenter account.";
        //            }
        //            else
        //                if ((sSections.Length - 1) > (_actualTSCTL.GLPrefixCnt))
        //                {
        //                    sNewItem = "Incorrect account format.|Invalid character.|Correct format is " + _actualTSCTL.GLFullAccountFormat + ".|Renter account.";
        //                }
        //        }

        //        if (!string.IsNullOrEmpty(sNewItem))
        //        {
        //            returnValue = sNewItem;
        //            return returnValue;
        //        }

        //        //we assemble full acount w/o punctuation
        //        sNewAcct = "";
        //        sNewItem = "";
        //        for (iLoop = 0; iLoop < sSections.Length; iLoop++)
        //        {
        //            sNewAcct = sNewAcct + sSections[iLoop];
        //        }
        //    }
        //    else
        //    {
        //        sNewAcct = sNewItem;
        //    }

        //    iMaxLen = _actualTSCTL.GLPrefixALength + _actualTSCTL.GLPrefixABLength + _actualTSCTL.GLPrefixABCLength + _actualTSCTL.GLBaseLength + _actualTSCTL.GLSuffixLength;

        //    if (_actualTSCTL.GLPrefixALength  > 0)
        //    {
        //        iMinLen = 1 + _actualTSCTL.GLPrefixABLength + _actualTSCTL.GLPrefixABCLength + _actualTSCTL.GLBaseLength + _actualTSCTL.GLSuffixLength;
        //    }
        //    else
        //    {
        //        //if there are no GL prefixes defined, the base account length can be less than the defined length
        //        //otherwise it must be the defined length. If this is the case, we just make certain that the suffix
        //        //is formatted correctly then pass back the account and let the calling routine check if the account is valid
        //        iMinLen = 1 + _actualTSCTL.GLSuffixLength;
        //        if (sNewAcct.Length >= iMinLen && sNewAcct.Length <= iMaxLen)
        //        {
        //            if (_actualTSCTL.GLSuffixLength > 0)
        //            {
        //                sNewAcct = sNewAcct.Substring(0, sNewAcct.Length - _actualTSCTL.GLSuffixLength) + _actualTSCTL.GLBasePunct + sNewAcct.Substring(sNewAcct.Length - _actualTSCTL.GLSuffixLength, _actualTSCTL.GLSuffixLength);
        //            }
        //            returnValue = sNewAcct;
        //            return returnValue;
        //        }
        //    }
        //    if (sNewAcct.Length < iMinLen)
        //    {
        //        returnValue = "Incorrect account format format.|Insufficient digits entered.|Correct format is " + _actualTSCTL.GLFullAccountFormat + ".|Reenter account.";
        //        return returnValue;
        //    }
        //    else if (sNewAcct.Length > iMaxLen)
        //    {
        //        returnValue = "Incorrect account format.|Too many digits before decimal point.|Correct format is " + _actualTSCTL.GLFullAccountFormat + ".|Renter account.";
        //        return returnValue;
        //    }

        //    returnValue = FormatItem(sNewAcct, _actualTSCTL.GLFullAccountFormat);
        //    return returnValue;
        //}

        //private string StripGLAccountPunctuation(string sAcct, bool bIncludePunctuation, Core.API.TLSettings.Models.ActualTSCTL _actualTSCTL) //sAcct is GL Account w/o suffix
        //{
        //    int iPos = 0;
        //    int iCnt = 0;
        //    string sChr = String.Empty;
        //    string sItm = String.Empty;
        //    string sNewItem = String.Empty;
        //    string returnValue = String.Empty;
        //    string[] sTestChars;
        //    int testInt = 0;

        //    if (_actualTSCTL.GLPrefixALength == 0)
        //    {
        //        return returnValue;
        //    }

        //    if (_actualTSCTL.GLPrefixABCLength > 0)
        //    {
        //        sTestChars = new string[3];
        //        sTestChars[0] = _actualTSCTL.GLPrefixAPunct;
        //        sTestChars[1] = _actualTSCTL.GLPrefixABPunct;
        //        sTestChars[2] = _actualTSCTL.GLPrefixABCPunct;
        //    }
        //    else if (_actualTSCTL.GLPrefixABLength > 0)
        //    {
        //        sTestChars = new string[2];
        //        sTestChars[0] = _actualTSCTL.GLPrefixAPunct;
        //        sTestChars[1] = _actualTSCTL.GLPrefixABPunct;
        //    }
        //    else
        //    {
        //        sTestChars = new string[1];
        //        sTestChars[0] = _actualTSCTL.GLPrefixAPunct;
        //    }

        //    sItm = sAcct.Trim();

        //    for (iPos = 1; iPos <= sItm.Length; iPos++)
        //    {
        //        sChr = Strings.Mid(sItm, iPos, 1);//sItm.Substring(iPos, 1);
        //        if (iCnt <= (sTestChars.Length - 1))
        //        {
        //            if (sChr != sTestChars[iCnt])
        //            {
        //                //C43112 - Need not check the character for case 
        //                if (((char.IsLetter(sChr, 0)) || int.TryParse(sChr, out testInt) || sChr == " "))
        //                {
        //                    sNewItem += sChr;
        //                }
        //                else
        //                {
        //                    returnValue = "Invalid character";
        //                    return returnValue;
        //                }
        //            }
        //            else
        //            {
        //                if (bIncludePunctuation == true)
        //                {
        //                    sNewItem += "|";
        //                }
        //                iCnt++;
        //            }
        //        }
        //        else
        //        {
        //            sNewItem += sChr;
        //        }
        //    }
        //    if (iCnt == 0 && bIncludePunctuation == true)  //this means the account was not punctuated, but we have to return it punctuated
        //    {
        //        if (sNewItem.Length > _actualTSCTL.GLBaseLength)
        //        {
        //            returnValue = Strings.Right(sNewItem, _actualTSCTL.GLBaseLength);//sNewItem.Substring(sNewItem.Length - this.GLBaseLength + 1, this.GLBaseLength);
        //            sNewItem = Strings.Left(sNewItem, sNewItem.Length - _actualTSCTL.GLBaseLength);//sNewItem.Substring(1, sNewItem.Length - this.GLBaseLength);
        //        }
        //        else
        //        {
        //            returnValue = sNewItem;
        //            return returnValue;
        //        }

        //        if (_actualTSCTL.GLPrefixABCLength > 0)
        //        {
        //            if (sNewItem.Length > _actualTSCTL.GLPrefixABCLength)
        //            {
        //                returnValue = Strings.Right(sNewItem, _actualTSCTL.GLPrefixABCLength) + "|" + returnValue;//sNewItem.Substring(sNewItem.Length - this.GLPrefixABCLength + 1, this.GLPrefixABCLength) + "|" + returnValue;
        //                sNewItem = Strings.Left(sNewItem, sNewItem.Length - _actualTSCTL.GLPrefixABCLength);//sNewItem.Substring(1, sNewItem.Length - this.GLPrefixABCLength);
        //            }
        //            else
        //            {
        //                sNewItem += "|" + returnValue;
        //                returnValue = sNewItem;
        //                return returnValue;
        //            }
        //        }
        //        if (_actualTSCTL.GLPrefixABLength > 0)
        //        {
        //            if (sNewItem.Length > _actualTSCTL.GLPrefixABLength)
        //            {
        //                returnValue = Strings.Right(sNewItem, _actualTSCTL.GLPrefixABLength) + "|" + returnValue;//sNewItem.Substring(sNewItem.Length - this.GLPrefixABLength + 1, this.GLPrefixABLength) + "|" + returnValue;
        //                sNewItem = Strings.Left(sNewItem, sNewItem.Length - _actualTSCTL.GLPrefixABLength);
        //            }
        //            else
        //            {
        //                sNewItem += "|" + returnValue;
        //                returnValue = sNewItem;
        //                return returnValue;
        //            }
        //        }
        //        sNewItem += "|" + returnValue;
        //        returnValue = sNewItem;
        //    }
        //    return sNewItem;
        //}

        //private string FormatItem(string sItm, string sFmt)
        //{
        //    string returnValue = null;
        //    string sNewItm;
        //    int iPos;
        //    int iFmtPos;

        //    sNewItm = "";
        //    iFmtPos = sFmt.Length - 1;
        //    for (iPos = sItm.Length - 1; iPos >= 0; iPos--)
        //    {
        //        if (iFmtPos > 0)
        //        {
        //            if (sFmt.Substring(iFmtPos, 1) != "x")
        //            {
        //                sNewItm = sFmt.Substring(iFmtPos, 1) + sNewItm;
        //                iFmtPos--;
        //            }
        //            iFmtPos--;
        //        }
        //        sNewItm = sItm.Substring(iPos, 1) + sNewItm;
        //    }

        //    //if there are still characters left on the format string, we test to see if there is any punctuation
        //    //if there is we add it so MasterFormat will return an error message
        //    if (iFmtPos > 0)
        //    {
        //        for (iPos = iFmtPos; iPos >= 0; iPos--)
        //        {
        //            if (sFmt.Substring(iPos, 1) != "x")
        //            {
        //                sNewItm = sFmt.Substring(iPos, 1) + sNewItm;
        //            }
        //        }
        //    }

        //    returnValue = sNewItm;
        //    return returnValue;
        //}

        //private string GetConvertedVendorType(int DocumentID, string tlVendorType1, string tlVendorType2, string tlVendorType3, string tlVendorType4, string tlVendorType5)
        //{
        //    string vendorType = "";
        //    //get vendor info
        //    DataTable dt = _docDA.GetVendorInfoByDocumentID(DocumentID);

        //    if (adoApVendorRs != null)
        //    {
        //        if (adoApVendorRs.Rows.Count > 0)
        //        {
        //            string vndType = adoApVendorRs.Rows[0]["Type"].ToString().ToUpper();
        //            if (tlVendorType1.ToUpper() == vndType)
        //            {
        //                vendorType = "Vendor_Type_1";
        //            }
        //            else if (tlVendorType2.ToUpper() == vndType)
        //            {
        //                vendorType = "Vendor_Type_2";
        //            }
        //            else if (tlVendorType3.ToUpper() == vndType)
        //            {
        //                vendorType = "Vendor_Type_3";
        //            }
        //            else if (tlVendorType4.ToUpper() == vndType)
        //            {
        //                vendorType = "Vendor_Type_4";
        //            }
        //            else if (tlVendorType5.ToUpper() == vndType)
        //            {
        //                vendorType = "Vendor_Type_5";
        //            }
        //        }

        //    }

        //    return vendorType;
        //}

        #endregion
    }

    public enum InvoiceTypes { None, Recurring, Upload, UploadRM, Regular, Imported }
    public enum MatchTypes
    {
        None,
        Manual,
        Auto

    }
}
