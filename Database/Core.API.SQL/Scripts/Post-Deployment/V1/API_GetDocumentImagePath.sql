
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[API_GetDocumentImagePath]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[API_GetDocumentImagePath]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[API_GetDocumentImagePath]
(
	@DocumentID as INT
)
as
BEGIN

	SELECT ImagePath FROM tblInvoices WITH(NOLOCK) WHERE InvoiceID = @DocumentID
END


GO


