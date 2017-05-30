

/****** Object:  StoredProcedure [dbo].[API_GetNumericRwVersionUserByUserID]    Script Date: 9/15/2015 8:34:54 PM ******/
DROP PROCEDURE [dbo].[API_GetNumericRwVersionUserByUserID]
GO

/****** Object:  StoredProcedure [dbo].[API_GetNumericRwVersionUserByUserID]    Script Date: 9/15/2015 8:34:54 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE Procedure [dbo].[API_GetNumericRwVersionUserByUserID]
  @UserID as int
as
BEGIN
     DECLARE @retRwVersion as BIGINT = 0
	 SELECT @retRwVersion =  CAST(RwVersion as BIGINT)
	  FROM [dbo].[tblUsers] WITH(NOLOCK)
	  WHERE UserIdNo = @UserID

	  SELECT @retRwVersion As 'RwVersionNumeric'
END

GO


