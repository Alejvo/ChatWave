Alter PROCEDURE GetGroupMessages 
@GroupId varchar(100)
AS
BEGIN
	SELECT 

	us.Id AS UserId,
	g.Id AS GroupId,
	m.Text,
	m.SentAt
	FROM Group_Message gm
	inner JOIN Messages m ON m.Id = gm.MessageId
	inner JOIN Groups g ON g.Id = gm.GroupId
	inner JOIN Users us ON us.Id = m.SenderId
	WHERE gm.GroupId = @GroupId
	ORDER BY g.Id,m.SentAt
END

alter PROCEDURE GetUserMessages 
@OriginId varchar(100),
@DestinyId varchar(100)
AS
BEGIN
	DECLARE @Status varchar(100);
	SELECT
	dest.Id as DestinyId,
	m.SenderId as OriginId,
	m.Text,
	m.SentAt
	FROM User_Message um
	inner JOIN Messages m ON m.Id = um.MessageId
	inner JOIN Users dest ON dest.Id = um.DestinyId
	inner JOIN Users ori ON m.SenderId = ori.Id
	WHERE (um.DestinyId = @DestinyId AND m.SenderId=@OriginId)
	OR(um.DestinyId = @OriginId AND m.SenderId=@DestinyId)
	ORDER BY m.SentAt
END

alter PROCEDURE SendMessageToUser 
@MessageId varchar(255),
@Text varchar(255),
@OriginId varchar(100),
@DestinyId varchar(100),
@SentAt date
AS
BEGIN
	IF EXISTS(SELECT 1 FROM Users WHERE Id = @OriginId)
	BEGIN
		IF EXISTS(SELECT 1 FROM Users WHERE Id = @DestinyId And Id != @OriginId)
		BEGIN
			INSERT INTO Messages(Id,Text,SenderId,SentAt)
			VALUES (@MessageId,@Text,@OriginId,@SentAt)
			select * from User_Message
			INSERT INTO User_Message(DestinyId,MessageId,OriginId)
			VALUES(@DestinyId,@MessageId,@OriginId)
		END
	END
END

alter PROCEDURE SendMessageToGroup
@MessageId varchar(100),
@Text varchar(255),
@UserId varchar(100),
@GroupId varchar(100),
@SentAt date
AS
BEGIN
	IF EXISTS(SELECT 1 FROM Groups WHERE Id = @GroupId)
	BEGIN
	IF EXISTS(SELECT 1 FROM Group_User WHERE UserId = @UserId)
		BEGIN
			INSERT INTO Messages(Id,Text,SenderId,SentAt)
			VALUES (@MessageId,@Text,@UserId,@SentAt)
			INSERT INTO Group_Message(GroupId,MessageId,UserId)
			VALUES(@GroupId,@MessageId,@UserId)
		END
	END
END