using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Core.API.DocumentSvc.Entity;
using CA.Imaging;
using CA.Imaging.Annotations;
using CA.Imaging.Annotations.Serialization;
using CA.Imaging.Models;
namespace Core.API.DocumentSvc
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IDocument
    {
        [OperationContract]
        string GetDocument(int DocumentID);

        [OperationContract]
        string GetDocuments(int? ConnectionID);

         [OperationContract]
        string GetDocumentNotes(int DocumentID);

        [OperationContract]
        RetMessage SaveComment(int DocumentID, string NoteText, DateTime NoteDate, int UserId);

        [OperationContract]
        string RejectDocument(int DocumentID, int ActionLevel, int RejectLevel, string RejectReason, DateTime RejectDate, int UserId);

        [OperationContract]
        string DeleteDocument(int DocumentID, int ApproveLevel, string DeleteReason, int UserId);

        [OperationContract]
        string APHoldDocument(int DocumentID, int ApproveLevel, string APHoldReason, bool APHold, DateTime APHoldDate, int UserId);

        [OperationContract]
        string HoldDocument(int DocumentID, int ApproveLevel, string HoldReason, DateTime HoldDate, int UserId);

        [OperationContract]
        byte[] GetDocumentBinary(int DocumentID);

        [OperationContract]
        string SaveDocumentBinary(int DocumentID, byte[] DocumentBinary);
       
        [OperationContract]
        string SaveDocument (DocumentEnt Doc);

        [OperationContract]
        string RouteDocument(int DocumentID, int ActionLevel, int UserID, int RouteToID, string RouteToComment);

        //[OperationContract]
        //string ApproveDocument(DocumentEnt Doc);

        [OperationContract]
        string ApproveDocument(int DocumentID, int UserID);
        
        [OperationContract]
        Core.API.DocumentSvc.Entity.AnnotationDataDto GetDocumentAnnotation(int DocumentID);

        [OperationContract]
        string SaveDocumentAnnotation(int DocumentID, Core.API.DocumentSvc.Entity.AnnotationDataDto Annotation);

    }
}
