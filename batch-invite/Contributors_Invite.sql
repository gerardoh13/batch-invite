USE [C73_ApiLive]
GO
/****** Object:  StoredProcedure [dbo].[Contributors_Invite]    Script Date: 6/15/2019 2:04:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROC [dbo].[Contributors_Invite]
			
			@TokenType int,
			@ContributorTokens as ContributorTokens READONLY,
			@isConfirmed bit = 0


AS
 
BEGIN TRY
  Declare @Tran nvarchar(50)  = '_uniquTxNameHere'
BEGIN Transaction @Tran
 
	BEGIN

		INSERT INTO [dbo].[Contributor_Invitations](
						[UserId]
						,[ContributionTypeId]
						,[EventId]
						,[TokenType]
						,[Token]
						, [isConfirmed]
						)

		SELECT		UserId
					,ContributionTypeId
					,EventId
					,@TokenType 
					,Token
					,@isConfirmed
		FROM		@ContributorTokens
			 
	END
	Commit Transaction @Tran

END TRY
BEGIN Catch
        ROLLBACK TRANSACTION @Tran;  
 THROW
End Catch
 
