ALTER PROCEDURE GetUsers
AS
BEGIN
SELECT 
    u.Id,
	u.FirstName,
	u.LastName,
	u.Email,
	u.Password,
	u.Birthday,
    u.UserName,
	u.ProfileImage,
	g.Id,
    g.Name,
	g.Description,
	g.Image,
	fri.Id,
	fri.UserName,
	fri.FirstName,
	fri.LastName,
	fri.ProfileImage
FROM Users u
LEFT JOIN Group_User gu ON gu.UserId = u.Id
LEFT JOIN Groups g ON g.Id = gu.GroupId
LEFT JOIN Friends f ON f.UserId = u.Id
LEFT JOIN Users fri ON fri.Id = f.FriendId
END

ALTER PROCEDURE GetUserById @Id varchar(100)
AS
BEGIN
SELECT 
    u.Id,
	u.FirstName,
	u.LastName,
	u.Email,
	u.Password,
	u.Birthday,
    u.UserName,
	u.ProfileImage,
	g.Id,
    g.Name,
	g.Description,
	g.Image,
	fri.Id,
	fri.UserName,
	fri.FirstName,
	fri.LastName,
	fri.ProfileImage
FROM Users u
LEFT JOIN Group_User gu ON gu.UserId = u.Id
LEFT JOIN Groups g ON g.Id = gu.GroupId
LEFT JOIN Friends f ON f.UserId = u.Id
LEFT JOIN Users fri ON fri.Id = f.FriendId
WHERE u.Id = @Id
END

alter PROCEDURE LoginUser @Email varchar(100), @Password varchar(100)
AS
BEGIN
	SELECT     
		u.Id,
		u.FirstName,
		u.LastName,
		u.Email,
		u.Password,
		u.Birthday,
		u.UserName,
		u.ProfileImage,
		g.Id,
		g.Name,
		g.Description,
		g.Image,
		fri.Id,
		fri.UserName,
		fri.FirstName,
		fri.LastName,
		fri.ProfileImage
	FROM Users u
	LEFT JOIN Group_User gu ON gu.UserId = u.Id
	LEFT JOIN Groups g ON g.Id = gu.GroupId
	LEFT JOIN Friends f ON f.UserId = u.Id
	LEFT JOIN Users fri ON fri.Id = f.FriendId
	WHERE u.Email=@Email AND u.Password=@Password
END
ALter PROCEDURE GetUsersByUsername @Username varchar(100)
AS
BEGIN
SELECT 
    u.Id,
	u.FirstName,
	u.LastName,
	u.Email,
	u.Password,
	u.Birthday,
    u.UserName,
	u.ProfileImage,
	g.Id,
    g.Name,
	g.Description,
	g.Image,
	fri.Id,
	fri.UserName,
	fri.FirstName,
	fri.LastName,
	fri.ProfileImage
FROM Users u
LEFT JOIN Group_User gu ON gu.UserId = u.Id
LEFT JOIN Groups g ON g.Id = gu.GroupId
LEFT JOIN Friends f ON f.UserId = u.Id
LEFT JOIN Users fri ON fri.Id = f.FriendId
WHERE u.UserName LIKE @Username + '%'
END

CREATE PROCEDURE CreateUser 
@Id varchar(100), @Firstname varchar(100),@Lastname varchar(100),@Email varchar(100),
@Password varchar(100),@Birthday date, @Username varchar(100), @ProfileImage varbinary(max)
AS
BEGIN
	INSERT INTO Users(Id,FirstName,LastName,Email,Password,Birthday,UserName,ProfileImage)
	VALUES (@Id,@Firstname,@Lastname,@Email,@Password,@Birthday,@Username,@ProfileImage)
END

CREATE PROCEDURE UpdateUser 
@Id varchar(100), @Firstname varchar(100),@Lastname varchar(100),@Email varchar(100),
@Password varchar(100),@Birthday date, @Username varchar(100),@ProfileImage varbinary(max)
AS
BEGIN
	UPDATE Users
	SET FirstName = @Firstname, LastName = @Lastname, Email =@Email,
		Password = @Password, Birthday = @Birthday, 
		UserName = @Username, ProfileImage = @ProfileImage
	WHERE Id = @Id
END

CREATE PROCEDURE DeleteUser @Id varchar(100)
AS
BEGIN
	DELETE FROM Users WHERE Id = @Id
END

CREATE FUNCTION IsUserNameUnique(@Username VARCHAR(200))
RETURNS BIT
AS
BEGIN 
	DECLARE @Result BIT;
	SELECT @Result = CASE 
		WHEN COUNT(*) > 0 THEN 0
		ELSE 1
		END
		FROM Users 
		WHERE UserName = @Username

		RETURN @Result;
END

CREATE FUNCTION IsEmailUnique(@Email VARCHAR(200))
RETURNS BIT
AS
BEGIN 
	DECLARE @Result BIT;
	SELECT @Result = CASE 
		WHEN COUNT(*) > 0 THEN 0
		ELSE 1
		END
		FROM Users 
		WHERE Email = @Email

		RETURN @Result;
END