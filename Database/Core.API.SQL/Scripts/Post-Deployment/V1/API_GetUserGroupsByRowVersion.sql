/****** Object:  StoredProcedure [dbo].[API_GetUserGroupsByRowVersion]    Script Date: 9/15/2015 8:27:27 PM ******/
DROP PROCEDURE [dbo].[API_GetUserGroupsByRowVersion]
GO

/****** Object:  StoredProcedure [dbo].[API_GetUserGroupsByRowVersion]    Script Date: 9/15/2015 8:27:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[API_GetUserGroupsByRowVersion]
  @RwVersion as bigint
as
BEGIN
    
	 SELECT CAST((SELECT [UserGroupID] AS 'Id'
      ,[GroupDescription] As 'Name'
      ,[ActionType] AS 'Type'
  FROM [dbo].[tblUserGroups]
   WHERE  CAST(RwVersion AS BIGINT) > @RwVersion
  FOR XML PATH ('Group'), ROOT('Groups')) AS VARCHAR(MAX)) AS XmlGroups
 
END
GO

