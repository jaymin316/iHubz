
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[usp_CompaniesSave]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[usp_CompaniesSave]
GO

IF EXISTS (SELECT * FROM sys.types WHERE is_table_type = 1 AND name = 'utt_CompaniesChangeSet')
	DROP TYPE [dbo].[utt_CompaniesChangeSet]
GO

CREATE TYPE [dbo].[utt_CompaniesChangeSet] AS TABLE(
	[CompanyName] [nvarchar](200) NULL,
	[ContactPerson] [nvarchar](100) NULL,
	[Address1] [nvarchar](500) NULL,
	[Address2] [nvarchar](500) NULL,
	[Website] [nvarchar](100) NULL,
	[City] [nvarchar](100) NULL,
	[Pincode] [nvarchar](6) NULL,
	[StateId] [int] NOT NULL,
	[Email1] [nvarchar](100) NULL,
	[Email2] [nvarchar](100) NULL,
	[Mobile] [nvarchar](20) NULL,
	[WorkPhone1] [nvarchar](50) NULL
)
GO
