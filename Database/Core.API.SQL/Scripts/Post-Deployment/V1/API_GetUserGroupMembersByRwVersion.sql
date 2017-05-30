
/****** Object:  StoredProcedure [dbo].[API_GetUserGroupMembersByRwVersion]    Script Date: 9/15/2015 8:26:35 PM ******/
DROP PROCEDURE [dbo].[API_GetUserGroupMembersByRwVersion]
GO

/****** Object:  StoredProcedure [dbo].[API_GetUserGroupMembersByRwVersion]    Script Date: 9/15/2015 8:26:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[API_GetUserGroupMembersByRwVersion]
 @RwVersion as bigint
as
BEGIN
    SELECT CAST((SELECT
		[UserGroupID] As 'GroupId',
		[UserType] As 'Type' ,
		[UserIDNo] AS 'UserId',
		CAST(tm.RwVersion AS BIGINT) as 'Version'
	 FROM [dbo].[tblUserGroupMembers] tm WITH(NOLOCK)
	INNER JOIN dbo.tblUsers us WITH(NOLOCK) on tm.UserID = us.UserID
			AND  CAST(tm.RwVersion AS BIGINT) > @RwVersion
  FOR XML PATH ('GroupMember'), ROOT('GroupMembers'))  AS VARCHAR(MAX)) AS XmlUserGroupMembers
 
END
GO


