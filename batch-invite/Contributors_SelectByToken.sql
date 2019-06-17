USE [C73_ApiLive]
GO
/****** Object:  StoredProcedure [dbo].[Contributors_SelectByToken]    Script Date: 6/17/2019 12:12:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[Contributors_SelectByToken]

		@Token UNIQUEIDENTIFIER

AS

/*

DECLARE @Token UNIQUEIDENTIFIER = 'a722b2c7-b208-4bb2-bf7d-a7d1375e85e8'
		
EXECUTE [dbo].[Contributors_SelectByToken]
		@Token

SELECT * FROM dbo.Contributor_Invitations

*/
BEGIN 

SELECT  [Id],
		[EventId],
		[UserId],
		[ContributionTypeId]

  FROM [dbo].[Contributor_Invitations]
  WHERE [Token] = @Token

END


