

/****** Object:  StoredProcedure [dbo].[API_GetNumericRwVersionUserGroupPermissionByUserID]    Script Date: 9/15/2015 8:37:17 PM ******/
DROP PROCEDURE [dbo].[API_GetNumericRwVersionUserGroupPermissionByUserID]
GO

/****** Object:  StoredProcedure [dbo].[API_GetNumericRwVersionUserGroupPermissionByUserID]    Script Date: 9/15/2015 8:37:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE Procedure [dbo].[API_GetNumericRwVersionUserGroupPermissionByUserID]
  @UserID as NVARCHAR(10)
as
BEGIN
     DECLARE @retRwVersion as BIGINT = 0
	 SELECT TOP 1 @retRwVersion =   CAST(RwVersion as BIGINT)
	  FROM [dbo].[tblUserPermissions] WITH(NOLOCK)
	  WHERE UserID = @UserID
	  ORDER BY  CAST(RwVersion as BIGINT) desc

	  SELECT @retRwVersion As 'RwVersionNumeric'
END


GO


