


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[API_GetDefaultConnection]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[API_GetDefaultConnection]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].[API_GetDefaultConnection]

as
BEGIN

	SELECT ConnectionID from dbo.tblConnections WITH(NOLOCK) WHERE DefaultConnection = 1

	
END



GO


