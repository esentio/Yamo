exec sp_MSforeachtable 'DROP TABLE ?'
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Content] [ntext] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedUserId] [int] NOT NULL,
	[Modified] [datetime] NULL,
	[ModifiedUserId] [int] NULL,
	[Deleted] [datetime] NULL,
	[DeletedUserId] [int] NULL,
 CONSTRAINT [PK_Blog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BlogId] [int] NOT NULL,
	[Content] [ntext] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedUserId] [int] NOT NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Blog] ON 
GO

DECLARE @T1000 TABLE (Id int)
DECLARE @T5 TABLE (Id int)

DECLARE @i int = 0
WHILE @i < 1000
BEGIN
    SET @i = @i + 1
    INSERT INTO @T1000 (Id) VALUES (@i)
END

SET @i = 0
WHILE @i < 5
BEGIN
    SET @i = @i + 1
    INSERT INTO @T5 (Id) VALUES (@i)
END


SET IDENTITY_INSERT Blog ON

INSERT INTO Blog (Id, Title, Content, Created, CreatedUserId, Modified, ModifiedUserId, Deleted, DeletedUserId)
SELECT
Id,
'My awesome blog ' + CAST(Id AS nvarchar),
'Lorem ipsum dolor sit amet ' + CAST(Id AS nvarchar) + '.',
DATEADD(HOUR, Id, GETDATE()),
Id,
DATEADD(HOUR, Id * 2, GETDATE()),
Id,
NULL,
NULL
FROM @T1000

SET IDENTITY_INSERT Blog OFF


SET IDENTITY_INSERT [User] ON

INSERT INTO [User] (Id, [Login], FirstName, LastName, Email)
SELECT
Id,
'login' + CAST(Id AS nvarchar),
'First name' + CAST(Id AS nvarchar),
'Last name' + CAST(Id AS nvarchar),
'mail' + CAST(Id AS nvarchar) + '@example.com'
FROM @T1000

SET IDENTITY_INSERT [User] OFF


SET IDENTITY_INSERT Comment ON

INSERT INTO Comment (Id, BlogId, Content, Created, CreatedUserId)
SELECT
Id,
Id1000,
'Lorem ipsum dolor sit amet ' + CAST(Id AS nvarchar) + '.',
DATEADD(HOUR, Id, GETDATE()),
Id1000
FROM (
	SELECT (t5.Id - 1) * 1000 + t1000.Id Id, t5.Id Id5, t1000.Id Id1000 FROM @T5 t5, @T1000 t1000
) t

SET IDENTITY_INSERT Comment OFF

GO

