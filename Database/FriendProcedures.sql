altEr PROCEDURE AddFriend
@UserId VARCHAR(80),
@FriendId VARCHAR(80)
AS
BEGIN
	IF NOT EXISTS(SELECT 1 FROM Friends WHERE UserId = @UserId AND FriendId=@FriendId )
	AND(
	EXISTS(SELECT 1 FROM FriendRequest WHERE SenderId= @UserId AND @FriendId = @FriendId)
	OR 
	EXISTS(SELECT 1 FROM FriendRequest WHERE SenderId= @FriendId AND @FriendId = @UserId)
	)
	BEGIN
		INSERT INTO Friends(UserId,FriendId)
		VALUES(@UserId,@FriendId)

		INSERT INTO Friends(UserId,FriendId)
		VALUES(@FriendId,@UserId)

		DELETE FROM FriendRequest WHERE SenderId=@FriendId AND ReceiverId = @UserId
	END
END

CREATE PROCEDURE RemoveFriend
@UserId VARCHAR(80),
@FriendId VARCHAR(80)
AS
BEGIN
	IF EXISTS(SELECT 1 FROM Friends WHERE UserId = @UserId AND FriendId=@FriendId)
	BEGIN
		Delete From Friends where UserId = @UserId AND FriendId = @FriendId

		Delete From Friends where UserId = @FriendId AND FriendId = @UserId
	END
END

Alter PROCEDURE MakeFriendRequest
@Id VARCHAR(80),
@UserId VARCHAR(80),
@FriendId VARCHAR(80),
@SentAt DATETIME
AS
BEGIN 
	IF NOT EXISTS(SELECT 1 FROM FriendRequest WHERE SenderId= @UserId AND @FriendId = @FriendId)
	OR NOT EXISTS(SELECT 1 FROM FriendRequest WHERE SenderId= @FriendId AND @FriendId = @UserId)
	BEGIN
		INSERT INTO FriendRequest(Id,SenderId,ReceiverId,SentAt)
		VALUES (@Id,@UserId,@FriendId,@SentAt)
	END
END

ALTER PROCEDURE GetFriendRequests
@UserId VARCHAR(80)
AS
BEGIN 
	SELECT se.Id,se.FirstName,se.LastName,se.Username,se.ProfileImage 
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

