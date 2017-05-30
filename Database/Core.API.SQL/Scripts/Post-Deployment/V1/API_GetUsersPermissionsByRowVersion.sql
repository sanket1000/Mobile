

/****** Object:  StoredProcedure [dbo].[API_GetUsersPermissionsByRowVersion]    Script Date: 9/15/2015 8:29:20 PM ******/
DROP PROCEDURE [dbo].[API_GetUsersPermissionsByRowVersion]
GO

/****** Object:  StoredProcedure [dbo].[API_GetUsersPermissionsByRowVersion]    Script Date: 9/15/2015 8:29:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[API_GetUsersPermissionsByRowVersion]
 @RwVersion as bigint
as
BEGIN

  DECLARE @defaultConnectionID as INT = 0

  SELECT @defaultConnectionID = ConnectionID FROM dbo.tblConnections WHERE DefaultConnection = 1

  SELECT CAST((SELECT us.[UserIDNo] AS 'UserId',
	CASE WHEN tp.PrefixOrJob = 'Prefix' THEN LTRIM(RTRIM(tp.PrefixOrJobNo)) ELSE '' END AS 'GLPrefix',
	CASE WHEN tp.PrefixOrJob = 'Job' THEN LTRIM(RTRIM(tp.PrefixOrJobNo)) ELSE '' END AS 'JobId'
  FROM [dbo].[tblUserPermissions] tp WITH(NOLOCK)
  INNER JOIN dbo.tblUsers us WITH(NOLOCK) on tp.UserID = us.UserID
  WHERE  CAST(tp.RwVersion AS BIGINT) >= @RwVersion
  AND tp.ConnectionID = @defaultConnectionID
  FOR XML PATH ('Permission'), ROOT('Permissions')) AS VARCHAR(MAX)) AS XmlUsersPermissions

END
GO


