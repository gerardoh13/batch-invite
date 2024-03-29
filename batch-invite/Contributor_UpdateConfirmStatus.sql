USE [C73_ApiLive]
GO
/****** Object:  StoredProcedure [dbo].[Contributor_UpdateConfirmStatus]    Script Date: 6/17/2019 12:12:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[Contributor_UpdateConfirmStatus]
    @Id INT

AS
/*
    DECLARE
	   @_id INT = 21

    SELECT * FROM dbo.Contributor_Invitations WHERE Id = @_id

    EXEC dbo.Contributor_UpdateConfirmStatus
	   @_id

	   SELECT * FROM dbo.Contributor_Invitations

*/
BEGIN

	DECLARE @IsConfirmed BIT = 1

    UPDATE 
	   dbo.Contributor_Invitations
    SET
	   IsConfirmed = @IsConfirmed
    WHERE
	   Id = @Id
END