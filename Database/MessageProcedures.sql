CREATE PROCEDURE GetGroupMessages 
@GroupId varchar(100)
AS
BEGIN
	SELECT 

	se.Id AS SenderId,
	g.Id As GroupId,
	m.Text,
	m.SentAt
	FROM Group_Message gm
	inner JOIN Messages m ON m.Id = gm.MessageId
	inner JOIN Groups g ON g.Id = gm.GroupId
	inner JOIN Users se ON se.Id = m.SenderId
	WHERE gm.GroupId = @GroupId
	ORDER BY g.Id,m.SentAt
END

CREATE PROCEDURE GetUserMessages 
@ReceiverId varchar(100),
@SenderId varchar(100)
AS
BEGIN
	DECLARE @Status varchar(100);
	SELECT
	re.Id as ReceiverId,
	m.SenderId as SenderId,
	m.Text,
	m.SentAt
	FROM User_Message um
	inner JOIN Messages m ON m.Id = um.MessageId
	inner JOIN Users re ON re.Id = um.ReceiverId
	inner JOIN Users se ON m.SenderId = se.Id
	WHERE (um.ReceiverId = @ReceiverId AND m.SenderId=@SenderId)
	OR(um.ReceiverId = @SenderId AND m.SenderId=@ReceiverId)
	ORDER BY m.SentAt
END

CREATE PROCEDURE SendMessageToUser 
@MessageId varchar(255),
@Text varchar(255),
@SenderId varchar(100),
@ReceiverId varchar(100),
@SentAt date
AS
BEGIN
	IF EXISTS(SELECT 1 FROM Users WHERE Id = @SenderId)
	BEGIN
		IF EXISTS(SELECT 1 FROM Users WHERE Id = @ReceiverId And Id != @SenderId)
		BEGIN
		select * from Messages
			INSERT INTO Messages(Id,Text,SenderId,SentAt)
			VALUES (@MessageId,@Text,@SenderId,@SentAt)
			select * from User_Message
			INSERT INTO User_Message(ReceiverId,MessageId,SenderId)
			VALUES(@ReceiverId,@MessageId,@SenderId)
		END
	END
END

CREATE PROCEDURE SendMessageToGroup
@MessageId varchar(100),
@Text varchar(255),
@SenderId varchar(100),
@GroupId varchar(100),
@SentAt date
AS
BEGIN
	IF EXISTS(SELECT 1 FROM Groups WHERE Id = @GroupId)
	BEGIN
	IF EXISTS(SELECT 1 FROM Group_User WHERE UserId = @SenderId)
		BEGIN
			INSERT INTO Messages(Id,Text,SenderId,SentAt)
			VALUES (@MessageId,@Text,@SenderId,@SentAt)
			INSERT INTO Group_Message(GroupId,MessageId,SenderId)
			VALUES(@GroupId,@MessageId,@SenderId)
		END
	END
END