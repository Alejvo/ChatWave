CREATE PROCEDURE GetGroups
AS
BEGIN
	SELECT	g.Id,
			g.Name,
			g.Description,
			g.Image,
			count(u.Id) Members 
			FROM Groups g
	LEFT JOIN  Group_User gu ON g.Id = gu.GroupId
	LEFT JOIN Users u ON u.Id = gu.UserId
	group by g.Id,g.Name,g.Description,g.Image
END

CREATE PROCEDURE CreateGroup 
@Id varchar(100),@Name varchar(100),@Description varchar(100),@Image varbinary(max)
AS
BEGIN
	INSERT INTO Groups(Id,Name,Description,Image)
	VALUES (@Id,@Name,@Description,@Image)
END

CREATE PROCEDURE UpdateGroup 
@Id varchar(100),@Name varchar(100),@Description varchar(100),@Image varbinary(max)
AS
BEGIN
	UPDATE Groups
	SET Name = @Name,Description=@Description,Image=@Image
	WHERE Id = @Id
END

CREATE PROCEDURE DeleteGroup @Id varchar(100)
AS
BEGIN
	DELETE FROM Groups 
	WHERE Id = @Id
END

CREATE PROCEDURE GetGroupById @Id varchar(100)
AS
BEGIN
	SELECT g.Id, g.Name, g.Description, g.Image,COUNT(u.Id) Members FROM Groups g
	LEFT JOIN Group_User gu ON g.Id = gu.GroupId
	LEFT JOIN Users u ON u.Id = gu.UserId
	WHERE g.Id = @Id
	GROUP BY g.Id,g.Name,g.Description,g.Image
END

CREATE PROCEDURE GetGroupsByName @Name varchar(100)
AS
BEGIN
	SELECT g.Id,g.Name,g.Description,g.Image,count(u.Id) Members FROM Groups g
	LEFT JOIN Group_User gu ON g.Id = gu.GroupId
	LEFT JOIN Users u ON u.Id = gu.UserId
	WHERE g.Name LIKE @Name + '%'
	group by g.Name,g.Id,g.Description,g.Image
END

CREATE PROCEDURE JoinGroup
@GroupId varchar(100),@UserId varchar(100)
AS
BEGIN
	IF NOT EXISTS(SELECT 1 FROM Group_User WHERE GroupId=@GroupId AND UserId=@UserId)
	BEGIN
		INSERT INTO Group_User(GroupId,UserId)
		VALUES(@GroupId,@UserId)
	END
	ELSE
	BEGIN
		PRINT 'User is already in this group'
	END
END

CREATE PROCEDURE LeaveGroup
@GroupId varchar(100),@UserId varchar(100)
AS
BEGIN
	IF EXISTS(SELECT 1 FROM Group_User WHERE GroupId=@GroupId AND UserId=@UserId)
	BEGIN
		DELETE FROM Group_User
		WHERE GroupId=@GroupId AND UserId = @UserId
	END
END

CREATE PROCEDURE GetGroupsByUser @UserId VARCHAR(100)
AS
BEGIN
	SELECT 
	g.Id,
    g.Name,
	g.Description,
	g.Image
	FROM Users u
	INNER JOIN Group_User gu ON gu.UserId = u.Id
	INNER JOIN Groups g ON g.Id = gu.GroupId
	WHERE u.Id = @userId
END