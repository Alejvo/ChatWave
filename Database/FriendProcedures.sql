ALTER PROCEDURE AddFriend
    @SenderId VARCHAR(80),
    @ReceiverId VARCHAR(80)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;

    IF NOT EXISTS (
        SELECT 1 FROM Friends 
        WHERE (UserId = @ReceiverId AND FriendId = @SenderId)
           OR (UserId = @SenderId AND FriendId = @ReceiverId)
    )

    AND EXISTS (
        SELECT 1 FROM FriendRequest 
        WHERE SenderId = @SenderId AND ReceiverId = @ReceiverId
    )
    BEGIN
        INSERT INTO Friends (UserId, FriendId)
        VALUES (@ReceiverId, @SenderId), (@SenderId, @ReceiverId);

        DELETE FROM FriendRequest 
        WHERE SenderId = @SenderId AND ReceiverId = @ReceiverId;

   
        COMMIT TRANSACTION;
        RETURN 1; 
    END
    ELSE
    BEGIN
        ROLLBACK TRANSACTION;
        RETURN 0; 
    END
END

alter PROCEDURE RemoveFriend
@SenderId VARCHAR(80),
@ReceiverId VARCHAR(80)
AS
BEGIN
	IF EXISTS(SELECT 1 FROM Friends WHERE UserId = @SenderId AND FriendId=@ReceiverId)
	BEGIN
		Delete From Friends where UserId = @SenderId AND FriendId = @ReceiverId

		Delete From Friends where UserId = @ReceiverId AND FriendId = @SenderId
	END
END

ALTER PROCEDURE MakeFriendRequest
    @Id VARCHAR(80),
    @SenderId VARCHAR(80),
    @ReceiverId VARCHAR(80),
    @SentAt DATETIME
AS
BEGIN

    IF NOT EXISTS (
        SELECT 1 
        FROM FriendRequest 
        WHERE (SenderId = @SenderId AND ReceiverId = @ReceiverId)
           OR (SenderId = @ReceiverId AND ReceiverId = @SenderId)
    )
	AND NOT EXISTS(
		SELECT 1
		FROM Friends
		WHERE (UserId = @SenderId AND FriendId = @ReceiverId)
			OR(UserId = @ReceiverId AND FriendId = @SenderId) 
	)
    BEGIN
        INSERT INTO FriendRequest (Id, SenderId, ReceiverId, SentAt)
        VALUES (@Id, @SenderId, @ReceiverId, @SentAt)
    END
END

CREATE PROCEDURE GetFriendRequests
@UserId VARCHAR(80)
AS
BEGIN 
	SELECT 
		se.Id,
		se.FirstName,
		se.LastName,
		se.Username,
		se.ProfileImage 
	FROM FriendRequest fr 
	INNER JOIN Users se On fr.SenderId = se.Id
	WHERE fr.ReceiverId = @UserId
END

CREATE FUNCTION IsUserYourFriend(@UserId VARCHAR(80),@FriendId VARCHAR(80))
RETURNS BIT
AS
BEGIN 
	DECLARE @Result BIT;
	SELECT @Result = CASE 
		WHEN COUNT(*) > 0 THEN 1
		ELSE 0
		END
		FROM Friends 
		WHERE UserId = @UserId AND FriendId = @FriendId

		RETURN @Result;
END

CREATE PROCEDURE GetFriendsByUser @UserId varchar(100)
AS
BEGIN
SELECT
	fri.Id,
	fri.UserName,
	fri.FirstName,
	fri.LastName,
	fri.ProfileImage
FROM Users u
INNER JOIN Friends f ON f.UserId = u.Id
INNER JOIN Users fri ON fri.Id = f.FriendId
WHERE u.Id = @UserId
END
