USE [ThreeLayer]
GO
/****** Object:  Table [dbo].[AuthUserData]    Script Date: 9/25/2020 6:30:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuthUserData](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_AuthUserData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Awards]    Script Date: 9/25/2020 6:30:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Awards](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Awards] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 9/25/2020 6:30:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAwardAssociations]    Script Date: 9/25/2020 6:30:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAwardAssociations](
	[UserId] [int] NOT NULL,
	[AwardId] [int] NOT NULL,
 CONSTRAINT [PK_UserAwardAssociations] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[AwardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoleAssociations]    Script Date: 9/25/2020 6:30:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoleAssociations](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_UserRoleAssociations] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 9/25/2020 6:30:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[AuthUserDataId] [int] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[AddAuthUserData]    Script Date: 9/25/2020 6:30:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[AddAuthUserData]
	@Password NVARCHAR(50),
	@UserId INT
AS
BEGIN
	INSERT INTO AuthUserData(Password, UserId)
	VALUES(@Password, @UserId)

	RETURN CAST((SELECT SCOPE_IDENTITY()) AS INT);
END
GO
/****** Object:  StoredProcedure [dbo].[AddAward]    Script Date: 9/25/2020 6:30:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddAward]
	@Title NVARCHAR(50)
AS
BEGIN
	INSERT INTO Awards(Title)
	VALUES(@Title)

	RETURN CAST((SELECT SCOPE_IDENTITY()) AS INT);
END
GO
/****** Object:  StoredProcedure [dbo].[AddRole]    Script Date: 9/25/2020 6:30:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddRole]
	@Title NVARCHAR(50)
AS
BEGIN
	INSERT INTO Roles(Title)
	VALUES(@Title)

	RETURN CAST((SELECT SCOPE_IDENTITY()) AS INT);
END
GO
/****** Object:  StoredProcedure [dbo].[AddUser]    Script Date: 9/25/2020 6:30:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[AddUser]
	@Name NVARCHAR(50),
	@DateOfBirth DATETIME,
	@AuthUserDataId INT
AS
BEGIN
	INSERT INTO Users(Name, DateOfBirth, AuthUserDataId)
	VALUES(@Name, @DateOfBirth, @AuthUserDataId);

	RETURN CAST((SELECT SCOPE_IDENTITY()) AS INT);
END
GO
/****** Object:  StoredProcedure [dbo].[AddUserAwardAssociation]    Script Date: 9/25/2020 6:30:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddUserAwardAssociation]
	@UserId INT,
	@AwardId INT
AS
BEGIN
	INSERT INTO UserAwardAssociations
	VALUES(@UserId, @AwardId)
END
GO
/****** Object:  StoredProcedure [dbo].[AddUserRoleAssociation]    Script Date: 9/25/2020 6:30:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddUserRoleAssociation]
	@UserId INT,
	@RoleId INT
AS
BEGIN
	INSERT INTO UserRoleAssociations
	VALUES(@UserId, @RoleId)
END
GO
/****** Object:  StoredProcedure [dbo].[GetAwardOwners]    Script Date: 9/25/2020 6:30:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAwardOwners]
	@AwardId INT
AS
BEGIN
	SELECT * FROM Users
	WHERE Users.Id IN 
		(SELECT UserAwardAssociations.UserId FROM UserAwardAssociations 
		WHERE UserAwardAssociations.AwardId = @AwardId)
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserAwards]    Script Date: 9/25/2020 6:30:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUserAwards]
	@UserId INT
AS
BEGIN
	SELECT * FROM Awards
	WHERE Awards.Id IN 
		(SELECT UserAwardAssociations.AwardId FROM UserAwardAssociations 
		WHERE UserAwardAssociations.UserId = @UserId)
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserRoles]    Script Date: 9/25/2020 6:30:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUserRoles]
	@UserId INT
AS
BEGIN
	SELECT * FROM Roles
	WHERE Roles.Id IN 
		(SELECT UserRoleAssociations.RoleId FROM UserRoleAssociations 
		WHERE UserRoleAssociations.UserId = @UserId)
END
GO
/****** Object:  StoredProcedure [dbo].[RegisterUser]    Script Date: 9/25/2020 6:30:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegisterUser]
	@Name NVARCHAR(50),
	@DateOfBirth DATETIME,
	@Password NVARCHAR(50)
AS
BEGIN
	INSERT INTO Users(Name, DateOfBirth, AuthUserDataId)
	VALUES(@Name, @DateOfBirth, 1)

	INSERT INTO AuthUserData(UserId, Password)
	VALUES ((SELECT Users.Id FROM Users where Users.Name = @Name), @Password)

	UPDATE Users
	SET AuthUserDataId = (SELECT SCOPE_IDENTITY())
	WHERE Users.Name = @Name
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveAuthUserDataById]    Script Date: 9/25/2020 6:30:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RemoveAuthUserDataById]
	@Id INT
AS
BEGIN
	DELETE FROM AuthUserData
	WHERE AuthUserData.Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveAwardById]    Script Date: 9/25/2020 6:30:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RemoveAwardById]
	@Id int
AS
BEGIN
	DELETE FROM Awards
	WHERE Awards.Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveRoleById]    Script Date: 9/25/2020 6:30:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RemoveRoleById]
	@Id INT
AS
BEGIN
	DELETE FROM Roles
	WHERE Roles.Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveUserAwardAssociation]    Script Date: 9/25/2020 6:30:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RemoveUserAwardAssociation]
	@UserId INT,
	@AwardId INT
AS
BEGIN
	DELETE FROM UserAwardAssociations
	WHERE UserAwardAssociations.UserId = @UserId AND UserAwardAssociations.AwardId = @AwardId
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveUserById]    Script Date: 9/25/2020 6:30:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RemoveUserById]
	@Id int
AS
BEGIN
	DELETE FROM Users
	WHERE Users.Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveUserRoleAssociation]    Script Date: 9/25/2020 6:30:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RemoveUserRoleAssociation]
	@UserId INT,
	@RoleId INT
AS
BEGIN
	DELETE FROM UserRoleAssociations
	WHERE UserRoleAssociations.UserId = @UserId AND UserRoleAssociations.RoleId = @RoleId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateAuthUserData]    Script Date: 9/25/2020 6:30:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateAuthUserData]
	@Id INT,
	@Password NVARCHAR(50),
	@UserId INT
AS
BEGIN
	UPDATE AuthUserData
	SET @Password = @Password, UserId = @UserId
	WHERE AuthUserData.Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateAward]    Script Date: 9/25/2020 6:30:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateAward]
	@Id int, 
	@Title NVARCHAR(50)
AS
BEGIN
	UPDATE Awards
	SET Awards.Title = @Title
	WHERE Awards.Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateRole]    Script Date: 9/25/2020 6:30:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateRole]
	@Id INT,
	@Title NVARCHAR(50)
AS
BEGIN
	UPDATE Roles
	SET Title = @Title
	WHERE Roles.Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateUser]    Script Date: 9/25/2020 6:30:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateUser]
	@Id int, 
	@Name NVARCHAR(50),
	@DateOfBirth DATETIME
AS
BEGIN
	UPDATE Users
	SET Users.Name = @Name, Users.DateOfBirth = @DateOfBirth
	WHERE Users.Id = @Id
END
GO
