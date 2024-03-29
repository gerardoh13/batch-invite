USE [C73_ApiLive]
GO
/****** Object:  StoredProcedure [dbo].[EventContributors_Insert]    Script Date: 6/17/2019 10:43:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[EventContributors_Insert]
			@EventId int
			,@Contributor int
			,@ContributionTypeId int

/*

Declare @_eventId int = 2
		,@_contributor int = 142
		,@_contributionTypeId int = 9

Execute dbo.EventContributors_Insert
		@_eventId
		,@_contributor
		,@_contributionTypeId

			   SELECT * FROM dbo.EventContributors

*/

AS	

BEGIN


INSERT INTO [dbo].[EventContributors]

			([EventId]
			,[Contributor]
			,[ContributionTypeId])

			VALUES

			(@EventId
			,@Contributor
			,@ContributionTypeId)


END
