/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


--------------------------------------------
--- tblUsers
--------------------------------------------
ALTER TABLE dbo.tblUsers 
ADD IsMobileUser BIT DEFAULT(0)
GO

ALTER TABLE dbo.tblUsers 
ADD RwVersion ROWVERSION
GO


--------------------------------------------
--- tblUserPermissions
--------------------------------------------
ALTER TABLE dbo.tblUserPermissions 
ADD RwVersion ROWVERSION
GO

--------------------------------------------
--- tblUserGroups
--------------------------------------------

ALTER TABLE dbo.tblUserGroups 
ADD RwVersion ROWVERSION
GO

--------------------------------------------
--- tblUserGroupMembers
--------------------------------------------

ALTER TABLE dbo.tblUserGroupMembers 
ADD RwVersion ROWVERSION
GO

--------------------------------------------
--- tblInvocieNotes
--------------------------------------------

ALTER TABLE dbo.tblInvoiceNotes 
ADD RwVersion ROWVERSION
GO

--------------------------------------------
--- tblInvoice 
--------------------------------------------


ALTER TABLE dbo.tblInvoices   
ADD SentToAPIDate DATETIME NULL
GO
--------------------------------------------
--- tblInvoiceActions
--------------------------------------------


ALTER TABLE dbo.tblInvoiceActions 
ADD ActionSource VARCHAR(50) DEFAULT('')
GO
--------------------------------------------
--- 
--------------------------------------------
ALTER TABLE dbo.APM_MASTER__INVOICE 
ADD RwVersion ROWVERSION
GO

ALTER TABLE dbo.APM_MASTER__DISTRIBUTION 
ADD RwVersion ROWVERSION
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[APIMessageLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[APIMessageLog](
	[MessageLodID] [int] IDENTITY(1,1) NOT NULL,
	[DocumentID] [int] NULL,
	[MessageType] [varchar](100) NULL,
	[Application] [varchar](100) NULL,
	[ScreenName] [varchar](100) NULL,
	[Function] [varchar](200) NULL,
	[Message] [varchar](max) NULL,
	[UserID] [int] NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_APIMessageLogID] PRIMARY KEY CLUSTERED 
(
	[MessageLodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO

SET ANSI_PADDING OFF
GO



