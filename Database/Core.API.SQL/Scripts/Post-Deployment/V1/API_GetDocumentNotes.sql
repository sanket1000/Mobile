USE [TimberScan]
GO

DROP PROCEDURE [dbo].[API_GetDocumentNotes]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].[API_GetDocumentNotes]
(
	@DocumentID as INT
)
as
BEGIN

	SELECT CAST((SELECT 
		notes.NoteDate as 'Date',
        case WHEN users.FirstName + ' ' + users.LastName is NULL THEN notes.Operator else users.FirstName + ' ' + users.LastName END  as  'Operator',
		notes.[Priority],
		notes.[NoteText] as 'Text', 
		notes.NoteType,
		notes.Sequence
                            
	FROM tblInvoiceNotes notes WITH(NOLOCK) 
	left join tblUsers users WITH(NOLOCK) on notes.Operator = users.UserId 
    WHERE InvoiceID =  @DocumentID 
	ORDER BY notes.InvoiceID ASC, NoteDate ASC, Sequence DESC
    FOR XML PATH ('Note'), ROOT('Notes')) AS VARCHAR(MAX)) AS XmlDocumentNotes

	
END



GO


