
/****** Object:  StoredProcedure [dbo].[API_GetVendorInfo]    Script Date: 9/15/2015 8:29:46 PM ******/
DROP PROCEDURE [dbo].[API_GetVendorInfo]
GO

/****** Object:  StoredProcedure [dbo].[API_GetVendorInfo]    Script Date: 9/15/2015 8:29:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


	CREATE PROCEDURE [dbo].[API_GetVendorInfo]
	 (	
		@InvoiceId varchar(30) = N'' ,
		@retVal nvarchar(max) output
	)
	as
	
	BEGIN
	
		declare @connid as int, @vend as varchar(50) 
		SET @retVal = N''

		SELECT @connid  = ConnectionID from dbo.tblInvoices WITH(NOLOCK) WHERE InvoiceID = @InvoiceId
		SELECT @vend  = Vendor from dbo.APM_MASTER__INVOICE WITH(NOLOCK) WHERE InvoiceID = @InvoiceId

		IF @connid > 0
		BEGIN
			  declare @sQry nvarchar(max) , @retValueInternal nvarchar(max) = N''
			  set @sQry = N''


			SET @sQry = N' 
						 SELECT @result = CONVERT(NVARCHAR(MAX), (SELECT
								AMV.Vendor as Id,
								AMV.Name,
								ISNULL(AMV.Address_1, '''') as Address1,
								ISNULL(AMV.Address_2, '''') as Address2,
								AMV.City, AMV.State, amv.ZIP as ZipCode,
								amv.Telephone as Phone,
								amv.Type
							   FROM [TimberSync_' +  CONVERT(VARCHAR, @connid) + '].[dbo].APM_MASTER__VENDOR AMV WITH(NOLOCK) 
								WHERE UPPER(Vendor) COLLATE DATABASE_DEFAULT   = ''' + @vend + ''''  + ' COLLATE DATABASE_DEFAULT '
							+ ' AND ConnectionID = ' + Convert(varchar(10), @connid ) 
							+ ' FOR XML PATH(''Vendor''), type))'

		
			--PRINT @sQry
	
			 EXEC sp_executesql @sQry, N'@result nvarchar(max) OUTPUT', @result = @retValueInternal OUTPUT

			 SELECT @retVal = @retValueInternal
			 SELECT @retVal = REPLACE(@retVal, '&amp;', '&')
			 --select @retVal
			
	END
	
       
END

GO


