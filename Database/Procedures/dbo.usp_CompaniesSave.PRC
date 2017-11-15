SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[dbo.usp_CompaniesSave]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[dbo.usp_CompaniesSave]
GO

CREATE PROCEDURE dbo.usp_CompaniesSave
(
	@Username				NVARCHAR(50),
	@AllCompaniesData		utt_CompaniesChangeSet READONLY
)
AS
BEGIN
	
	/* Declare variables */
	DECLARE @UpdateTime	DATETIME

	/* Set variable */
	SET @UpdateTime = GETDATE();	

	BEGIN TRANSACTION;
	/******************************** MERGE - START *********************************/
	-- Merge Companies
	MERGE [dbo].[tbl_Company] AS T 
	USING (
		-- SOURCE data
		SELECT * FROM @AllCompaniesData

	) AS CD
		ON T.CompanyName = CD.CompanyName
	WHEN MATCHED THEN
		-- Records exists in both SOURCE and TARGET : update
		UPDATE SET
			T.ContactName1 = CD.ContactPerson,
			T.ModifiedBy = @Username,
			T.ModifiedDate = @UpdateTime
	WHEN NOT MATCHED BY TARGET THEN
		-- Records exists in SOURCE but not in TARGET : add new
		INSERT (
			CompanyName, ContactName1, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
		)
		VALUES (
			CD.CompanyName, CD.ContactPerson, 
			@Username, @UpdateTime, @Username, @UpdateTime
		)
	;

	
	/******************************** MERGE - END *********************************/

	COMMIT TRANSACTION


END
GO