

/****** Object:  StoredProcedure [dbo].[API_GetUsersByRowVersion]    Script Date: 9/15/2015 8:28:21 PM ******/
DROP PROCEDURE [dbo].[API_GetUsersByRowVersion]
GO

/****** Object:  StoredProcedure [dbo].[API_GetUsersByRowVersion]    Script Date: 9/15/2015 8:28:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[API_GetUsersByRowVersion]
  @RwVersion as bigint
as
BEGIN

	 --declare @biRwVersion varbinary(8)
  --   select @biRwVersion = cast(@RwVersion as varbinary)

	 SELECT CAST((SELECT [UserIDNo] AS 'Id',
	  email as 'Email',
	  UserID as 'UserName',
	  FirstName,
	  LastName,
	 ISNULL(EnterVendors, 0)  as CanEnterVendor,
	  ISNULL(EnterInvoices, 0) as CanEnterInvoice,
	  ISNULL(ReviewInvoices, 0) as CanReviewInvoice,
	  ISNULL(ChangeInvoices, 0) as CanChangeInvoice,
	  ISNULL(DeleteInvoices, 0) as CanDeleteInvoice,
	  ISNULL(ApproveInvoices, 0) as CanApproveInvoice,
	  ISNULL(FinalReview, 0) as CanFinalReview,
	  ISNULL(RejectInvoice, 0) as CanRejectInvoice,
	  ISNULL(RouteInvoicePreAprv , 0) as CanRouteInvoicePreApproval,
	  ISNULL(RouteInvoicePostAprv, 0) as CanRouteInvoicePostApproval ,
	  ISNULL(AddImages,  0)  as CanAddImage,
	  ISNULL(RemoveImages, 0) as CanRemoveImage,
	  ISNULL(ChangeDistributions, 0) as CanChangeDistribution,
	  ISNULL(Annotations, 0) as CanDoAnnotation,
	  ISNULL(AllowHoldInAP, 0) as CanHoldInAP,
	  ISNULL(InvoiceOnHold, 0) as CanHoldInTScan,
	  AccountEnabled as 'IsActive',
	  ISNULL(IsMobileUser, 0) as 'IsMobileUser',
	  CAST(RwVersion as BIGINT) As 'Version'
	  FROM [dbo].[tblUsers] WITH(NOLOCK)
	  WHERE  CAST(RwVersion AS BIGINT) > @RwVersion
      FOR XML PATH ('User'), ROOT('Users')) AS VARCHAR(MAX)) AS XmlUsers

  
 
END
GO


