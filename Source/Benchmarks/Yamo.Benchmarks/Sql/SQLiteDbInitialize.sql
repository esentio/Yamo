DROP TABLE IF EXISTS Blog;
DROP TABLE IF EXISTS Comment;
DROP TABLE IF EXISTS [User];

SELECT 1;
PRAGMA foreign_keys=OFF;
BEGIN TRANSACTION;

CREATE TABLE [Blog] (
  [Id] int NOT NULL
, [Title] nvarchar(50) NOT NULL
, [Content] ntext NOT NULL
, [Created] datetime NOT NULL
, [CreatedUserId] int NOT NULL
, [Modified] datetime NULL
, [ModifiedUserId] int NULL
, [Deleted] datetime NULL
, [DeletedUserId] int NULL
, CONSTRAINT [PK_Blog] PRIMARY KEY ([Id])
);

CREATE TABLE [Comment] (
  [Id] int NOT NULL
, [BlogId] int NOT NULL
, [Content] ntext NOT NULL
, [Created] datetime NOT NULL
, [CreatedUserId] int NOT NULL
, CONSTRAINT [PK_Comment] PRIMARY KEY ([Id])
);

CREATE TABLE [User] (
  [Id] int NOT NULL
, [Login] nvarchar(50) NOT NULL
, [FirstName] nvarchar(50) NOT NULL
, [LastName] nvarchar(50) NOT NULL
, [Email] nvarchar(50) NOT NULL
, CONSTRAINT [PK_User] PRIMARY KEY ([Id])
);


DROP TABLE IF EXISTS temp.T1000;
DROP TABLE IF EXISTS temp.T5;

CREATE TEMP TABLE temp.T1000 (Id int);
CREATE TEMP TABLE temp.T5 (Id int);

WITH RECURSIVE 
	for(i) AS (VALUES(1) UNION ALL SELECT i + 1 FROM for WHERE i < 1000) 
INSERT INTO temp.T1000 SELECT i FROM for;

WITH RECURSIVE 
	for(i) AS (VALUES(1) UNION ALL SELECT i + 1 FROM for WHERE i < 5) 
INSERT INTO temp.T5 SELECT i FROM for;


INSERT INTO Blog (Id, Title, Content, Created, CreatedUserId, Modified, ModifiedUserId, Deleted, DeletedUserId)
SELECT
Id,
'My awesome blog ' || CAST(Id AS nvarchar),
'Lorem ipsum dolor sit amet ' || CAST(Id AS nvarchar) || '.',
datetime('now', '+' || CAST(Id AS nvarchar) || ' hours'),
Id,
datetime('now', '+' || CAST(Id * 2 AS nvarchar) || ' hours'),
Id,
NULL,
NULL
FROM temp.T1000;


INSERT INTO [User] (Id, [Login], FirstName, LastName, Email)
SELECT
Id,
'login' || CAST(Id AS nvarchar),
'First name' || CAST(Id AS nvarchar),
'Last name' || CAST(Id AS nvarchar),
'mail' || CAST(Id AS nvarchar) || '@example.com'
FROM temp.T1000;


INSERT INTO Comment (Id, BlogId, Content, Created, CreatedUserId)
SELECT
Id,
Id1000,
'Lorem ipsum dolor sit amet ' || CAST(Id AS nvarchar) || '.',
datetime('now', '+' || CAST(Id AS nvarchar) || ' hours'),
Id1000
FROM (
	SELECT (t5.Id - 1) * 1000 + t1000.Id Id, t5.Id Id5, t1000.Id Id1000 FROM temp.T5 t5, temp.T1000 t1000
) t;


COMMIT;
