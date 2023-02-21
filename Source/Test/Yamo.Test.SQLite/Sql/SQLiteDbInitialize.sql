﻿-- autogenerated with ErikEJ.SqlCeScripting version 3.5.2.74 and manually adjusted
SELECT 1;
PRAGMA foreign_keys=OFF;
BEGIN TRANSACTION;



CREATE TABLE [Label] (
  [TableId] nvarchar(50)  NOT NULL
, [Id] int  NOT NULL
, [Language] nvarchar(3)  NOT NULL
, [Description] nvarchar(100)  NOT NULL
, CONSTRAINT [PK_Label] PRIMARY KEY ([TableId],[Id],[Language])
);

CREATE TABLE [LabelArchive] (
  [TableId] nvarchar(50)  NOT NULL
, [Id] int  NOT NULL
, [Language] nvarchar(3)  NOT NULL
, [Description] nvarchar(100)  NOT NULL
, CONSTRAINT [PK_LabelArchive] PRIMARY KEY ([TableId],[Id],[Language])
);



CREATE TABLE [ItemWithActionHistory] (
  [Id] INTEGER  NOT NULL
, [Description] nvarchar(50)  NOT NULL
, [IntValue] int  NOT NULL
, CONSTRAINT [PK_ItemWithActionHistory] PRIMARY KEY ([Id])
);

CREATE TABLE [ItemWithActionHistoryArchive] (
  [Id] INTEGER  NOT NULL
, [Description] nvarchar(50)  NOT NULL
, [IntValue] int  NOT NULL
, CONSTRAINT [PK_ItemWithActionHistoryArchive] PRIMARY KEY ([Id])
);



CREATE TABLE [ItemWithInitialization] (
  [Id] INTEGER  NOT NULL
, [Description] nvarchar(50)  NOT NULL
, CONSTRAINT [PK_ItemWithInitialization] PRIMARY KEY ([Id])
);

CREATE TABLE [ItemWithInitializationArchive] (
  [Id] INTEGER  NOT NULL
, [Description] nvarchar(50)  NOT NULL
, CONSTRAINT [PK_ItemWithInitializationArchive] PRIMARY KEY ([Id])
);



CREATE TABLE [ItemWithPropertyModifiedTracking] (
  [Id] INTEGER  NOT NULL
, [Description] nvarchar(50)  NOT NULL
, [IntValue] int  NOT NULL
, CONSTRAINT [PK_ItemWithPropertyModifiedTracking] PRIMARY KEY ([Id])
);

CREATE TABLE [ItemWithPropertyModifiedTrackingArchive] (
  [Id] INTEGER  NOT NULL
, [Description] nvarchar(50)  NOT NULL
, [IntValue] int  NOT NULL
, CONSTRAINT [PK_ItemWithPropertyModifiedTrackingArchive] PRIMARY KEY ([Id])
);



CREATE TABLE [ItemWithSupportDbLoad] (
  [Id] INTEGER  NOT NULL
, [Description] nvarchar(50)  NOT NULL
, [IntValue] int  NOT NULL
, CONSTRAINT [PK_ItemWithSupportDbLoad] PRIMARY KEY ([Id])
);

CREATE TABLE [ItemWithSupportDbLoadArchive] (
  [Id] INTEGER  NOT NULL
, [Description] nvarchar(50)  NOT NULL
, [IntValue] int  NOT NULL
, CONSTRAINT [PK_ItemWithSupportDbLoadArchive] PRIMARY KEY ([Id])
);



CREATE TABLE [ItemWithIdentityIdAndDefaultValues] (
  [Id] INTEGER  NOT NULL
, [Description] nvarchar(50)  NOT NULL
, [UniqueidentifierValue] uniqueidentifier NOT NULL
, [IntValue] int DEFAULT 42  NOT NULL
, CONSTRAINT [PK_ItemWithIdentityIdAndDefaultValues] PRIMARY KEY ([Id])
);

CREATE TABLE [ItemWithIdentityIdAndDefaultValuesArchive] (
  [Id] INTEGER  NOT NULL
, [Description] nvarchar(50)  NOT NULL
, [UniqueidentifierValue] uniqueidentifier NOT NULL
, [IntValue] int DEFAULT 42  NOT NULL
, CONSTRAINT [PK_ItemWithIdentityIdAndDefaultValuesArchive] PRIMARY KEY ([Id])
);



CREATE TABLE [ItemWithIdentityId] (
  [Id] INTEGER  NOT NULL
, [Description] nvarchar(50)  NOT NULL
, CONSTRAINT [PK_ItemWithIdentityId] PRIMARY KEY ([Id])
);

CREATE TABLE [ItemWithIdentityIdArchive] (
  [Id] INTEGER  NOT NULL
, [Description] nvarchar(50)  NOT NULL
, CONSTRAINT [PK_ItemWithIdentityIdArchive] PRIMARY KEY ([Id])
);



CREATE TABLE [ItemWithDefaultValueId] (
  [Id] uniqueidentifier NOT NULL
, [Description] nvarchar(50)  NOT NULL
, CONSTRAINT [PK_ItemWithDefaultValueId] PRIMARY KEY ([Id])
);

CREATE TABLE [ItemWithDefaultValueIdArchive] (
  [Id] uniqueidentifier NOT NULL
, [Description] nvarchar(50)  NOT NULL
, CONSTRAINT [PK_ItemWithDefaultValueIdArchive] PRIMARY KEY ([Id])
);



CREATE TABLE [ItemWithAuditFields] (
  [Id] INTEGER  NOT NULL
, [Description] nvarchar(50)  NOT NULL
, [Created] datetime NOT NULL
, [CreatedUserId] int  NOT NULL
, [Modified] datetime NULL
, [ModifiedUserId] int  NULL
, [Deleted] datetime NULL
, [DeletedUserId] int  NULL
, CONSTRAINT [PK_ItemWithAuditFields] PRIMARY KEY ([Id])
);

CREATE TABLE [ItemWithAuditFieldsArchive] (
  [Id] INTEGER  NOT NULL
, [Description] nvarchar(50)  NOT NULL
, [Created] datetime NOT NULL
, [CreatedUserId] int  NOT NULL
, [Modified] datetime NULL
, [ModifiedUserId] int  NULL
, [Deleted] datetime NULL
, [DeletedUserId] int  NULL
, CONSTRAINT [PK_ItemWithAuditFieldsArchive] PRIMARY KEY ([Id])
);



CREATE TABLE [ItemWithAllSupportedValues] (
  [Id] uniqueidentifier NOT NULL
, [UniqueidentifierColumn] uniqueidentifier NOT NULL
, [UniqueidentifierColumnNull] uniqueidentifier NULL
, [Nvarchar50Column] nvarchar(50)  NOT NULL
, [Nvarchar50ColumnNull] nvarchar(50)  NULL
, [NvarcharMaxColumn] ntext NOT NULL
, [NvarcharMaxColumnNull] ntext NULL
, [BitColumn] bit NOT NULL
, [BitColumnNull] bit NULL
, [SmallintColumn] smallint NOT NULL
, [SmallintColumnNull] smallint NULL
, [IntColumn] int  NOT NULL
, [IntColumnNull] int  NULL
, [BigintColumn] bigint  NOT NULL
, [BigintColumnNull] bigint  NULL
, [RealColumn] real NOT NULL
, [RealColumnNull] real NULL
, [FloatColumn] float NOT NULL
, [FloatColumnNull] float NULL
, [Numeric10and3Column] numeric(10,3)  NOT NULL
, [Numeric10and3ColumnNull] numeric(10,3)  NULL
, [Numeric15and0Column] numeric(15,0)  NOT NULL
, [Numeric15and0ColumnNull] numeric(15,0)  NULL
, [DateColumn] [date] NOT NULL
, [DateColumnNull] [date] NULL
, [TimeColumn] [time](7) NOT NULL
, [TimeColumnNull] [time](7) NULL
, [DatetimeColumn] datetime NOT NULL
, [DatetimeColumnNull] datetime NULL
, [Datetime2Column] [datetime2](7) NOT NULL
, [Datetime2ColumnNull] [datetime2](7) NULL
, [DatetimeoffsetColumn] [datetimeoffset](7) NOT NULL
, [DatetimeoffsetColumnNull] [datetimeoffset](7) NULL
, [Varbinary50Column] varbinary(50)  NOT NULL
, [Varbinary50ColumnNull] varbinary(50)  NULL
, [VarbinaryMaxColumn] image NOT NULL
, [VarbinaryMaxColumnNull] image NULL
, CONSTRAINT [PK_ItemWithAllSupportedValues] PRIMARY KEY ([Id])
);

CREATE TABLE [ItemWithAllSupportedValuesArchive] (
  [Id] uniqueidentifier NOT NULL
, [UniqueidentifierColumn] uniqueidentifier NOT NULL
, [UniqueidentifierColumnNull] uniqueidentifier NULL
, [Nvarchar50Column] nvarchar(50)  NOT NULL
, [Nvarchar50ColumnNull] nvarchar(50)  NULL
, [NvarcharMaxColumn] ntext NOT NULL
, [NvarcharMaxColumnNull] ntext NULL
, [BitColumn] bit NOT NULL
, [BitColumnNull] bit NULL
, [SmallintColumn] smallint NOT NULL
, [SmallintColumnNull] smallint NULL
, [IntColumn] int  NOT NULL
, [IntColumnNull] int  NULL
, [BigintColumn] bigint  NOT NULL
, [BigintColumnNull] bigint  NULL
, [RealColumn] real NOT NULL
, [RealColumnNull] real NULL
, [FloatColumn] float NOT NULL
, [FloatColumnNull] float NULL
, [Numeric10and3Column] numeric(10,3)  NOT NULL
, [Numeric10and3ColumnNull] numeric(10,3)  NULL
, [Numeric15and0Column] numeric(15,0)  NOT NULL
, [Numeric15and0ColumnNull] numeric(15,0)  NULL
, [DateColumn] [date] NOT NULL
, [DateColumnNull] [date] NULL
, [TimeColumn] [time](7) NOT NULL
, [TimeColumnNull] [time](7) NULL
, [DatetimeColumn] datetime NOT NULL
, [DatetimeColumnNull] datetime NULL
, [Datetime2Column] [datetime2](7) NOT NULL
, [Datetime2ColumnNull] [datetime2](7) NULL
, [DatetimeoffsetColumn] [datetimeoffset](7) NOT NULL
, [DatetimeoffsetColumnNull] [datetimeoffset](7) NULL
, [Varbinary50Column] varbinary(50)  NOT NULL
, [Varbinary50ColumnNull] varbinary(50)  NULL
, [VarbinaryMaxColumn] image NOT NULL
, [VarbinaryMaxColumnNull] image NULL
, CONSTRAINT [PK_ItemWithAllSupportedValuesArchive] PRIMARY KEY ([Id])
);



CREATE TABLE [ItemWithOnlySQLiteSupportedFields] (
  [Id] INTEGER  NOT NULL
, [DateOnlyColumn] [date] NOT NULL
, [DateOnlyColumnNull] [date] NULL
, [TimeOnlyColumn] [time](7) NOT NULL
, [TimeOnlyColumnNull] [time](7) NULL
, [Nchar1Column] [nchar](1) NOT NULL
, [Nchar1ColumnNull] [nchar](1) NULL
, CONSTRAINT [PK_ItemWithOnlySQLiteSupportedFields] PRIMARY KEY ([Id])
);

CREATE TABLE [ItemWithOnlySQLiteSupportedFieldsArchive] (
  [Id] INTEGER  NOT NULL
, [DateOnlyColumn] [date] NOT NULL
, [DateOnlyColumnNull] [date] NULL
, [TimeOnlyColumn] [time](7) NOT NULL
, [TimeOnlyColumnNull] [time](7) NULL
, [Nchar1Column] [nchar](1) NOT NULL
, [Nchar1ColumnNull] [nchar](1) NULL
, CONSTRAINT [PK_ItemWithOnlySQLiteSupportedFieldsArchive] PRIMARY KEY ([Id])
);



CREATE TABLE [Category] (
  [Id] int  NOT NULL
, CONSTRAINT [PK_Category] PRIMARY KEY ([Id])
);

CREATE TABLE [CategoryArchive] (
  [Id] int  NOT NULL
, CONSTRAINT [PK_CategoryArchive] PRIMARY KEY ([Id])
);



CREATE TABLE [ArticleSubstitution] (
  [OriginalArticleId] int  NOT NULL
, [SubstitutionArticleId] int  NOT NULL
, CONSTRAINT [PK_ArticleSubstitution] PRIMARY KEY ([OriginalArticleId],[SubstitutionArticleId])
);

CREATE TABLE [ArticleSubstitutionArchive] (
  [OriginalArticleId] int  NOT NULL
, [SubstitutionArticleId] int  NOT NULL
, CONSTRAINT [PK_ArticleSubstitutionArchive] PRIMARY KEY ([OriginalArticleId],[SubstitutionArticleId])
);



CREATE TABLE [ArticlePart] (
  [Id] int  NOT NULL
, [ArticleId] int  NOT NULL
, [Price] numeric(10,2)  NOT NULL
, CONSTRAINT [PK_ArticlePart] PRIMARY KEY ([Id])
);


CREATE TABLE [ArticlePartArchive] (
  [Id] int  NOT NULL
, [ArticleId] int  NOT NULL
, [Price] numeric(10,2)  NOT NULL
, CONSTRAINT [PK_ArticlePartArchive] PRIMARY KEY ([Id])
);



CREATE TABLE [ArticleCategory] (
  [ArticleId] int  NOT NULL
, [CategoryId] int  NOT NULL
, CONSTRAINT [PK_ArticleCategory] PRIMARY KEY ([ArticleId],[CategoryId])
);

CREATE TABLE [ArticleCategoryArchive] (
  [ArticleId] int  NOT NULL
, [CategoryId] int  NOT NULL
, CONSTRAINT [PK_ArticleCategoryArchive] PRIMARY KEY ([ArticleId],[CategoryId])
);



CREATE TABLE [Article] (
  [Id] int  NOT NULL
, [Price] numeric(10,2)  NOT NULL
, CONSTRAINT [PK_Article] PRIMARY KEY ([Id])
);

CREATE TABLE [ArticleArchive] (
  [Id] int  NOT NULL
, [Price] numeric(10,2)  NOT NULL
, CONSTRAINT [PK_ArticleArchive] PRIMARY KEY ([Id])
);



CREATE TABLE [LinkedItem] (
  [Id] int  NOT NULL
, [PreviousId] int  NULL
, [Description] nvarchar(50)  NOT NULL
, CONSTRAINT [PK_LinkedItem] PRIMARY KEY ([Id])
);

CREATE TABLE [LinkedItemArchive] (
  [Id] int  NOT NULL
, [PreviousId] int  NULL
, [Description] nvarchar(50)  NOT NULL
, CONSTRAINT [PK_LinkedItemArchive] PRIMARY KEY ([Id])
);



CREATE TABLE [LinkedItemChild] (
  [Id] int  NOT NULL
, [LinkedItemId] int  NOT NULL
, [Description] nvarchar(50)  NOT NULL
, CONSTRAINT [PK_LinkedItemChild] PRIMARY KEY ([Id])
);

CREATE TABLE [LinkedItemChildArchive] (
  [Id] int  NOT NULL
, [LinkedItemId] int  NOT NULL
, [Description] nvarchar(50)  NOT NULL
, CONSTRAINT [PK_LinkedItemChildArchive] PRIMARY KEY ([Id])
);



COMMIT;

ATTACH DATABASE ':memory:' AS [test_schema];



CREATE TABLE [test_schema].[ItemInSchema] (
  [Id] INTEGER  NOT NULL
, [Description] nvarchar(50)  NOT NULL
, [RelatedItemId] int  NULL
, [Deleted] datetime NULL
, CONSTRAINT [PK_ItemInSchema] PRIMARY KEY ([Id])
);

CREATE TABLE [test_schema].[ItemInSchemaArchive] (
  [Id] INTEGER  NOT NULL
, [Description] nvarchar(50)  NOT NULL
, [RelatedItemId] int  NULL
, [Deleted] datetime NULL
, CONSTRAINT [PK_ItemInSchemaArchive] PRIMARY KEY ([Id])
);
