/****** Object:  StoredProcedure [dbo].[API_GetUserGroups]    Script Date: 9/15/2015 8:26:59 PM ******/
DROP PROCEDURE [dbo].[API_GetUserGroups]
GO

/****** Object:  StoredProcedure [dbo].[API_GetUserGroups]    Script Date: 9/15/2015 8:26:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[API_GetUserGroups]

as
BEGIN
	
	--DECLARE @retVal VARCHAR(MAX) = ''
	
  SELECT CAST((SELECT [UserGroupID] AS 'Id'
      ,[GroupDescription] As 'Name'
      ,[ActionType] AS 'Type'
  FROM [dbo].[tblUserGroups]
  FOR XML PATH ('Group'), ROOT('Groups')) AS VARCHAR(MAX)) AS XmlGroups
 
END
GO


