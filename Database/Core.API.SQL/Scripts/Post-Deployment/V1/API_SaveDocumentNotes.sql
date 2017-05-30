

/****** Object:  StoredProcedure [dbo].[API_SaveDocumentNotes]    Script Date: 10/05/2015 04:38:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[API_SaveDocumentNotes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[API_SaveDocumentNotes]
GO



/****** Object:  StoredProcedure [dbo].[API_SaveDocumentNotes]    Script Date: 10/05/2015 04:38:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE Procedure [dbo].[API_SaveDocumentNotes]
(
	@DocumentID as int,
	@NoteText nvarchar(255),
	@NoteDate datetime,
	@UserID int,
	@ActionLevel int,
	@RetMessage varchar(8000) output 
)
as
BEGIN

	
     declare @ConnectionID int = 0, @Operator nvarchar(8) = ''
	 Declare @ActionType nvarchar(15) = 'Approve',  @Priority nvarchar(10) = 'Normal'
	 set @RetMessage = ''

	 select @ConnectionID = ConnectionID from dbo.tblConnections where DefaultConnection = 1 -- and IsActive = 1

	 select @Operator = UserID from dbo.tblUsers with(nolock) where UserIDNo = @UserID

	 if ISNULL(@DocumentID, 0) = 0
	 begin
		select @RetMessage = @RetMessage + ' Invalid document id'  + char(10) + char(13)
	 end
	 if ISNULL(@NoteText, '') = ''
	 begin
		select @RetMessage = @RetMessage + ' Document note or comment is empty'  + char(10) + char(13)
	 end
	 if ISNULL(@UserID,0) = 0
	 begin
		select @RetMessage = @RetMessage + ' Invalid user'  + char(10) + char(13)
	 end
	  if ISNULL(@ActionLevel, 0) = 0
	 begin
		select @RetMessage = @RetMessage + ' Invalid action level'  + char(10) + char(13)
	 end

	 if ISNULL(@ConnectionID, 0) = 0
	 begin
		select @RetMessage = 'Can not save notes as default comapany is not set or active.'
	 end
	 
	 if isnull(@RetMessage, '') = ''
	 begin
		insert into dbo.tblInvoiceNotes (ConnectionID, InvoiceID, NoteType, ActionType, ActionLevel, Priority, NoteDate, Operator, NoteText)
		values (@ConnectionID, @DocumentID, 'Note', @ActionType, @ActionLevel, @Priority, @NoteDate, @Operator, @NoteText)	
	 end
	 
	 select @RetMessage as 'Message'
	
END

GO


