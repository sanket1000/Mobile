

/****** Object:  StoredProcedure [dbo].[API_GetNumericRwVersionUserGroupByUserGroupID]    Script Date: 9/15/2015 8:36:33 PM ******/
DROP PROCEDURE [dbo].[API_GetNumericRwVersionUserGroupByUserGroupID]
GO

/****** Object:  StoredProcedure [dbo].[API_GetNumericRwVersionUserGroupByUserGroupID]    Script Date: 9/15/2015 8:36:33 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE Procedure [dbo].[API_GetNumericRwVersionUserGroupByUserGroupID]
  @UserGroupID as int
as
BEGIN
     DECLARE @retRwVersion as BIGINT = 0
	 SELECT @retRwVersion =  CAST(RwVersion as BIGINT)
	  FROM [dbo].[tblUserGroups] WITH(NOLOCK)
	  WHERE UserGroupID = @UserGroupID

	  SELECT @retRwVersion As 'RwVersionNumeric'
END


GO


