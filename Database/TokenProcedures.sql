CREATE PROCEDURE GetToken @Token varchar(100)
AS
BEGIN
	SELECT * FROM Tokens
	Where Token = @Token
END

CREATE PROCEDURE SaveToken 
@UserId varchar(100),@Token varchar(100),@Id varchar(100),@ExpiryTime date
AS
BEGIN
	IF NOT EXISTS(SELECT 1 FROM Tokens WHERE UserId=@UserId)
	BEGIN
		INSERT INTO Tokens(Id,UserId,Token,ExpiryTime)
		VALUES (@Id,@UserId,@Token,@ExpiryTime)
	END
	ELSE
	BEGIN
		UPDATE Tokens
		SET Token = @Token,ExpiryTime=@ExpiryTime
	END
END