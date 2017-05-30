using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Core.API.DocumentSvc.Entity;
using CA.Imaging.Annotations;
using CA.Imaging.Annotations.Serialization;
using CA.Imaging.Models;

namespace Core.API.DocumentSvc
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Document : IDocument
    {
        public string GetDocument(int DocumentID)
        {
            string retMsg = "";
            DocumentBO _docBO = new DocumentBO();
            _docBO.LogAPIMessage(DocumentID, "REQUEST", "", "DocumentSvc", "GetDocument", 0);

            retMsg = new DocumentBO().GetDocument(DocumentID);

            _docBO.LogAPIMessage(DocumentID, "RESPONSE", retMsg, "DocumentSvc", "GetDocument", 0);

            return retMsg;
        }

        public string GetDocuments(int? ConnectionID)
        {
            string retMsg = "";

            DocumentBO _docBO = new DocumentBO();
            string msg = "GetDocuments" + (ConnectionID.HasValue ? " ConnectionID = " + ConnectionID.Value.ToString() : "");
            _docBO.LogAPIMessage(0, "REQUEST", "", "DocumentSvc", msg, 0);

            retMsg = new DocumentBO().GetDocumentList(ConnectionID);

            _docBO.LogAPIMessage(0, "RESPONSE", retMsg, "DocumentSvc", msg, 0);

            return retMsg;
        }
        public string GetDocumentNotes(int DocumentID)
        {
            string retMsg = "";

            DocumentBO _docBO = new DocumentBO();
            _docBO.LogAPIMessage(DocumentID, "REQUEST", "", "DocumentSvc", "GetDocumentNotes", 0);

            retMsg = new DocumentBO().GetDocumentNotes(DocumentID);

            _docBO.LogAPIMessage(DocumentID, "RESPONSE", retMsg, "DocumentSvc", "GetDocumentNotes", 0);

            return retMsg;
        }

        public byte[] GetDocumentBinary(int DocumentID)
        {
            DocumentBO _docBO = new DocumentBO();
            _docBO.LogAPIMessage(DocumentID, "REQUEST", "", "DocumentSvc", "GetDocumentBinary", 0);

            byte[] retImg = new DocumentBO().GetDocumentImagePath(DocumentID);

            _docBO.LogAPIMessage(DocumentID, "RESPONSE", "", "DocumentSvc", "GetDocumentBinary", 0);


            return retImg;
        }


        public RetMessage SaveComment(int DocumentID, string NoteText, DateTime NoteDate, int UserId)
        {
            RetMessage retMsg;

            DocumentBO _docBO = new DocumentBO();

            string xmlMsg = new Comment(DocumentID, NoteText, NoteDate, UserId).GetXMLString();
            _docBO.LogAPIMessage(DocumentID, "REQUEST", xmlMsg, "DocumentSvc", "SaveComment", UserId);

            retMsg = new DocumentBO().SaveComment(DocumentID, NoteText, NoteDate, UserId);

            _docBO.LogAPIMessage(DocumentID, "RESPONSE", retMsg.Message, "DocumentSvc", "SaveComment", UserId);


            return retMsg;
        }
        public string RejectDocument(int DocumentID, int ActionLevel, int RejectLevel, string RejectReason, DateTime RejectDate, int UserId)
        {
            string retMsg = "";

            DocumentBO _docBO = new DocumentBO();

            string xmlMsg = new RejectDoc(DocumentID, ActionLevel, RejectLevel, RejectReason, RejectDate, UserId).GetXMLString();
            _docBO.LogAPIMessage(DocumentID, "REQUEST", xmlMsg, "DocumentSvc", "RejectDocument", UserId);

            retMsg = new DocumentBO().RejectDocument(DocumentID, ActionLevel, RejectLevel, RejectReason, RejectDate, UserId);

            _docBO.LogAPIMessage(DocumentID, "RESPONSE", retMsg, "DocumentSvc", "RejectDocument", UserId);

            return retMsg;
        }

        public string DeleteDocument(int DocumentID, int ApproveLevel, string DeleteReason, int UserId)
        {
            string retMsg = "";

            DocumentBO _docBO = new DocumentBO();

            string xmlMsg = new DeleteDoc(DocumentID, ApproveLevel, DeleteReason, UserId).GetXMLString();
            _docBO.LogAPIMessage(DocumentID, "REQUEST", xmlMsg, "DocumentSvc", "DeleteDocument", UserId);


            retMsg = new DocumentBO().DeleteDocument(DocumentID, ApproveLevel, DeleteReason, UserId);

            _docBO.LogAPIMessage(DocumentID, "RESPONSE", retMsg, "DocumentSvc", "DeleteDocument", UserId);


            return retMsg;
        }

        public string APHoldDocument(int DocumentID, int ApproveLevel, string APHoldReason, bool APHold, DateTime APHoldDate, int UserId)
        {
            string retMsg = "";

            DocumentBO _docBO = new DocumentBO();

            string xmlMsg = new APHoldDoc(DocumentID, ApproveLevel, APHoldReason, APHold, APHoldDate, UserId).GetXMLString();
            _docBO.LogAPIMessage(DocumentID, "REQUEST", xmlMsg, "DocumentSvc", "APHoldDocument", UserId);

            retMsg = new DocumentBO().APHoldDocument(DocumentID, ApproveLevel, APHoldReason, APHoldDate, APHold, UserId);

            _docBO.LogAPIMessage(DocumentID, "RESPONSE", retMsg, "DocumentSvc", "APHoldDocument", UserId);


            return retMsg;
        }

        public string HoldDocument(int DocumentID, int ApproveLevel, string HoldReason, DateTime HoldDate, int UserId)
        {
            string retMsg = "";

            DocumentBO _docBO = new DocumentBO();

            string xmlMsg = new HoldDoc(DocumentID, ApproveLevel, HoldReason, HoldDate, UserId).GetXMLString();
            _docBO.LogAPIMessage(DocumentID, "REQUEST", xmlMsg, "DocumentSvc", "HoldDocument", UserId);

            retMsg = new DocumentBO().HoldDocument(DocumentID, ApproveLevel, HoldReason, HoldDate, UserId);

            _docBO.LogAPIMessage(DocumentID, "RESPONSE", retMsg, "DocumentSvc", "HoldDocument", UserId);

            return retMsg;
        }

        public string SaveDocumentBinary(int DocumentID, byte[] DocumentBinary)
        {
            string retMsg = "";

            DocumentBO _docBO = new DocumentBO();
            _docBO.LogAPIMessage(DocumentID, "REQUEST", "", "DocumentSvc", "SaveDocumentBinary", 0);

            retMsg = new DocumentBO().SaveDocumentBinary(DocumentID, DocumentBinary);

            _docBO.LogAPIMessage(DocumentID, "RESPONSE", retMsg, "DocumentSvc", "SaveDocumentBinary", 0);

            return retMsg;
        }

        public string SaveDocument(DocumentEnt Doc)
        {
            string retMsg = "";

            //DocumentBO _docBO = new DocumentBO();
            //_docBO.LogAPIMessage(Doc.Header.DocumentId, "REQUEST", Doc.GetXMLString(), "DocumentSvc", "SaveDocumentBinary", 0);

            retMsg = new DocumentBO().SaveDocument(Doc);

           // _docBO.LogAPIMessage(Doc.Header.DocumentId, "RESPONSE", retMsg, "DocumentSvc", "SaveDocumentBinary", 0);

            return retMsg;
        }

        //public string ApproveDocument(DocumentEnt Doc)
        //{
        //    string retMsg = "";

        //    //DocumentBO _docBO = new DocumentBO();
        //    //_docBO.LogAPIMessage(Doc.Header.DocumentId, "REQUEST", Doc.GetXMLString(), "DocumentSvc", "SaveDocumentBinary", 0);

        //    retMsg = new DocumentBO().ApproveDocument(Doc);

        //    // _docBO.LogAPIMessage(Doc.Header.DocumentId, "RESPONSE", retMsg, "DocumentSvc", "SaveDocumentBinary", 0);

        //    return retMsg;
        //}

        public string ApproveDocument(int DocumentID, int UserID)
        {
            string retMsg = "";

            //DocumentBO _docBO = new DocumentBO();
            //_docBO.LogAPIMessage(Doc.Header.DocumentId, "REQUEST", Doc.GetXMLString(), "DocumentSvc", "SaveDocumentBinary", 0);

            retMsg = new DocumentBO().ApproveDocument(DocumentID, UserID);

            // _docBO.LogAPIMessage(Doc.Header.DocumentId, "RESPONSE", retMsg, "DocumentSvc", "SaveDocumentBinary", 0);

            return retMsg;
        }
       
        public Core.API.DocumentSvc.Entity.AnnotationDataDto GetDocumentAnnotation(int DocumentID)
        {
            DocumentBO _docBO = new DocumentBO();
            _docBO.LogAPIMessage(DocumentID, "REQUEST", "Reqeusting annotation data", "DocumentSvc", "GetDocumentAnnotation", 0);
       
            Core.API.DocumentSvc.Entity.AnnotationDataDto ent = _docBO.GetDocumentAnnotation(DocumentID); 

            _docBO.LogAPIMessage(DocumentID, "RESPONSE", "Returning annotation data", "DocumentSvc", "GetDocumentAnnotation", 0);
       
            return ent;
        }

        public string SaveDocumentAnnotation(int DocumentID, Core.API.DocumentSvc.Entity.AnnotationDataDto Annotation)
        {
            string retMsg = "";

            DocumentBO _docBO = new DocumentBO();
            _docBO.LogAPIMessage(DocumentID, "REQUEST", "Request save annotation", "DocumentSvc", "SaveDocumentAnnotation", 0);

             retMsg = _docBO.SaveDocumentAnnotation(DocumentID, Annotation);

            _docBO.LogAPIMessage(DocumentID, "RESPONSE", "Return save annotation", "DocumentSvc", "SaveDocumentAnnotation", 0);
       

            return retMsg;
        }

        public string RouteDocument(int DocumentID, int ActionLevel, int UserID, int RouteToID, string RouteToComment)
        {

            string retMsg = "";

            DocumentBO _docBO = new DocumentBO();

            string xmlMsg = new RouteDoc(DocumentID, ActionLevel, UserID, RouteToID, RouteToComment).GetXMLString();
            _docBO.LogAPIMessage(DocumentID, "REQUEST", xmlMsg, "DocumentSvc", "RouteDocument", UserID);

            retMsg = new DocumentBO().RouteDocumentManual(DocumentID, ActionLevel, UserID, RouteToID, RouteToComment);

            _docBO.LogAPIMessage(DocumentID, "RESPONSE", retMsg, "DocumentSvc", "RouteDocument", UserID);

            return retMsg;
        }

        //#region UnitTest Methods

        //public string ProcessDocument(DocumentEnt doc)
        //{
        //    return new DocumentBO().ProcessDocument(doc).Message;
        //}

        //public void RunTScanSql(string sql)
        //{
        //    new DocumentBO().RunTScanSql(sql);
        //}
       
        //#endregion
    }
}