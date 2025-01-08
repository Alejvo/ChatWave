CREATE DATABASE ChatWave;
go;
USE ChatWave;

CREATE TABLE Friends
(
	UserId VARCHAR(80) FOREIGN KEY REFERENCES Users(Id),
	FriendId VARCHAR(80) FOREIGN KEY REFERENCES Users(Id)
);
go;
CREATE TABLE Messages
(
	Id VARCHAR(80) PRIMARY KEY,
	Text VARCHAR(500),
	SenderId varchar(80) FOREIGN KEY REFERENCES Users(Id),
	SentAt DATETIME
);
go;
CREATE TABLE Users
(
	Id varchar(80) primary key,
	FirstName varchar(80) not null,
	LastName varchar(80) not null,
	Email varchar(80) not null,
	Password varchar(80) not null,
	Birthday date not null,
	UserName varchar(20) not null unique,
	ProfileImage VARBINARY(MAX)
);
CREATE TABLE Tokens
(
	Id varchar(100) PRIMARY KEY,
	UserId varchar(80) Foreign Key References Users(Id),
	Token varchar(100),
	RefreshToken varchar(100),
	ExpiryTime Date
);
CREATE TABLE Groups
(
	Id VARCHAR(80) PRIMARY KEY,
	Name VARCHAR(20) NOT NULL,
	Description varchar(255),
	Image VARBINARY(MAX)
);
CREATE TABLE Group_User
(
	GroupId VARCHAR(80) FOREIGN KEY REFERENCES Groups(Id),
	UserId VARCHAR(80) FOREIGN KEY REFERENCES Users(Id)
);
CREATE TABLE User_Message
(
	SenderId VARCHAR(80) FOREIGN KEY REFERENCES Users(Id),
	ReceiverId VARCHAR(80) FOREIGN KEY REFERENCES Users(Id),
	MessageId VARCHAR(80) FOREIGN KEY REFERENCES Messages(Id)
);
CREATE TABLE Group_Message
(
	GroupId VARCHAR(80) FOREIGN KEY REFERENCES Groups(Id),
	SenderId VARCHAR(80) FOREIGN KEY REFERENCES Users(Id),
	MessageId VARCHAR(80) FOREIGN KEY REFERENCES Messages(Id)
);

CREATE TABLE FriendRequest
(
	Id VARCHAR(80) PRIMARY KEY,
	SenderId VARCHAR(80) FOREIGN KEY REFERENCES Users(Id),
	ReceiverId VARCHAR(80) FOREIGN KEY REFERENCES Users(Id),
	SentAt DATETIME
);