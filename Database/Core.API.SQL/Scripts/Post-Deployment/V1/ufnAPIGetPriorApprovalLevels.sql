
CREATE Function [dbo].[ufnAPIGetPriorApprovalLevels]
(
	@DocumentID as INT
)
RETURNS VARCHAR(MAX)
as

BEGIN
	 DECLARE @retValue varchar(max) = ''
	  SELECT @retValue = CAST((
								select 
								ia.ActionType + ' (Level - ' + convert(varchar, ia.ActionLevel) + ')' as LevelName,
								ia.ActionLevel as ApprovalLevel,
								CASE WHEN ia.GroupID is null then '' else  ia.GroupID end as GroupId,
								case 
								when ia.UserID is null then ''
								else (select convert(varchar, u.UserIDNo) from dbo.tblUsers u with(nolock) where u.UserID = ia.UserID) 
								end as UserId,
								'' as Reason
								from dbo.tblInvoiceActions ia WITH(NOLOCK)
								WHERE ia.InvoiceID = @DocumentID
								AND ia.ActionLevel <> 0 and ia.Complete = 1
    FOR XML PATH ('Approval'), ROOT('ApprovalHistory')) AS VARCHAR(MAX))

    RETURN @retValue
	
END
GO


