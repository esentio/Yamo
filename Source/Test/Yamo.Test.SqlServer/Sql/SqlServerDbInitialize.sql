CREATE SCHEMA [test_schema]

GO

SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON



CREATE TABLE [dbo].[Article](
	[Id] [int] NOT NULL,
	[Price] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[ArticleArchive](
	[Id] [int] NOT NULL,
	[Price] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_ArticleArchive] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[ArticleCategory](
	[ArticleId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_ArticleCategory] PRIMARY KEY CLUSTERED 
(
	[ArticleId] ASC,
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[ArticleCategoryArchive](
	[ArticleId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_ArticleCategoryArchive] PRIMARY KEY CLUSTERED 
(
	[ArticleId] ASC,
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[ArticlePart](
	[Id] [int] NOT NULL,
	[ArticleId] [int] NOT NULL,
	[Price] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_ArticlePart] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[ArticlePartArchive](
	[Id] [int] NOT NULL,
	[ArticleId] [int] NOT NULL,
	[Price] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_ArticlePartArchive] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[ArticleSubstitution](
	[OriginalArticleId] [int] NOT NULL,
	[SubstitutionArticleId] [int] NOT NULL,
 CONSTRAINT [PK_ArticleSubstitution] PRIMARY KEY CLUSTERED 
(
	[OriginalArticleId] ASC,
	[SubstitutionArticleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[ArticleSubstitutionArchive](
	[OriginalArticleId] [int] NOT NULL,
	[SubstitutionArticleId] [int] NOT NULL,
 CONSTRAINT [PK_ArticleSubstitutionArchive] PRIMARY KEY CLUSTERED 
(
	[OriginalArticleId] ASC,
	[SubstitutionArticleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[Category](
	[Id] [int] NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[CategoryArchive](
	[Id] [int] NOT NULL,
 CONSTRAINT [PK_CategoryArchive] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[ItemWithActionHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[IntValue] [int] NOT NULL,
 CONSTRAINT [PK_ItemWithActionHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[ItemWithActionHistoryArchive](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[IntValue] [int] NOT NULL,
 CONSTRAINT [PK_ItemWithActionHistoryArchive] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[ItemWithAllSupportedValues](
	[Id] [uniqueidentifier] NOT NULL,
	[UniqueidentifierColumn] [uniqueidentifier] NOT NULL,
	[UniqueidentifierColumnNull] [uniqueidentifier] NULL,
	[Nvarchar50Column] [nvarchar](50) NOT NULL,
	[Nvarchar50ColumnNull] [nvarchar](50) NULL,
	[NvarcharMaxColumn] [nvarchar](max) NOT NULL,
	[NvarcharMaxColumnNull] [nvarchar](max) NULL,
	[BitColumn] [bit] NOT NULL,
	[BitColumnNull] [bit] NULL,
	[SmallintColumn] [smallint] NOT NULL,
	[SmallintColumnNull] [smallint] NULL,
	[IntColumn] [int] NOT NULL,
	[IntColumnNull] [int] NULL,
	[BigintColumn] [bigint] NOT NULL,
	[BigintColumnNull] [bigint] NULL,
	[RealColumn] [real] NOT NULL,
	[RealColumnNull] [real] NULL,
	[FloatColumn] [float] NOT NULL,
	[FloatColumnNull] [float] NULL,
	[Numeric10and3Column] [numeric](10, 3) NOT NULL,
	[Numeric10and3ColumnNull] [numeric](10, 3) NULL,
	[Numeric15and0Column] [numeric](15, 0) NOT NULL,
	[Numeric15and0ColumnNull] [numeric](15, 0) NULL,
	[DateColumn] [date] NOT NULL,
	[DateColumnNull] [date] NULL,
	[TimeColumn] [time](7) NOT NULL,
	[TimeColumnNull] [time](7) NULL,
	[DatetimeColumn] [datetime] NOT NULL,
	[DatetimeColumnNull] [datetime] NULL,
	[Datetime2Column] [datetime2](7) NOT NULL,
	[Datetime2ColumnNull] [datetime2](7) NULL,
	[DatetimeoffsetColumn] [datetimeoffset](7) NOT NULL,
	[DatetimeoffsetColumnNull] [datetimeoffset](7) NULL,
	[Varbinary50Column] [varbinary](50) NOT NULL,
	[Varbinary50ColumnNull] [varbinary](50) NULL,
	[VarbinaryMaxColumn] [varbinary](max) NOT NULL,
	[VarbinaryMaxColumnNull] [varbinary](max) NULL,
 CONSTRAINT [PK_ItemWithAllSupportedValues] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[ItemWithAllSupportedValuesArchive](
	[Id] [uniqueidentifier] NOT NULL,
	[UniqueidentifierColumn] [uniqueidentifier] NOT NULL,
	[UniqueidentifierColumnNull] [uniqueidentifier] NULL,
	[Nvarchar50Column] [nvarchar](50) NOT NULL,
	[Nvarchar50ColumnNull] [nvarchar](50) NULL,
	[NvarcharMaxColumn] [nvarchar](max) NOT NULL,
	[NvarcharMaxColumnNull] [nvarchar](max) NULL,
	[BitColumn] [bit] NOT NULL,
	[BitColumnNull] [bit] NULL,
	[SmallintColumn] [smallint] NOT NULL,
	[SmallintColumnNull] [smallint] NULL,
	[IntColumn] [int] NOT NULL,
	[IntColumnNull] [int] NULL,
	[BigintColumn] [bigint] NOT NULL,
	[BigintColumnNull] [bigint] NULL,
	[RealColumn] [real] NOT NULL,
	[RealColumnNull] [real] NULL,
	[FloatColumn] [float] NOT NULL,
	[FloatColumnNull] [float] NULL,
	[Numeric10and3Column] [numeric](10, 3) NOT NULL,
	[Numeric10and3ColumnNull] [numeric](10, 3) NULL,
	[Numeric15and0Column] [numeric](15, 0) NOT NULL,
	[Numeric15and0ColumnNull] [numeric](15, 0) NULL,
	[DateColumn] [date] NOT NULL,
	[DateColumnNull] [date] NULL,
	[TimeColumn] [time](7) NOT NULL,
	[TimeColumnNull] [time](7) NULL,
	[DatetimeColumn] [datetime] NOT NULL,
	[DatetimeColumnNull] [datetime] NULL,
	[Datetime2Column] [datetime2](7) NOT NULL,
	[Datetime2ColumnNull] [datetime2](7) NULL,
	[DatetimeoffsetColumn] [datetimeoffset](7) NOT NULL,
	[DatetimeoffsetColumnNull] [datetimeoffset](7) NULL,
	[Varbinary50Column] [varbinary](50) NOT NULL,
	[Varbinary50ColumnNull] [varbinary](50) NULL,
	[VarbinaryMaxColumn] [varbinary](max) NOT NULL,
	[VarbinaryMaxColumnNull] [varbinary](max) NULL,
 CONSTRAINT [PK_ItemWithAllSupportedValuesArchive] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]



CREATE TABLE [dbo].[ItemWithAuditFields](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedUserId] [int] NOT NULL,
	[Modified] [datetime] NULL,
	[ModifiedUserId] [int] NULL,
	[Deleted] [datetime] NULL,
	[DeletedUserId] [int] NULL,
 CONSTRAINT [PK_ItemWithAuditFields] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[ItemWithAuditFieldsArchive](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedUserId] [int] NOT NULL,
	[Modified] [datetime] NULL,
	[ModifiedUserId] [int] NULL,
	[Deleted] [datetime] NULL,
	[DeletedUserId] [int] NULL,
 CONSTRAINT [PK_ItemWithAuditFieldsArchive] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[ItemWithDefaultValueId](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_ItemWithDefaultValueId_Id]  DEFAULT (newsequentialid()),
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ItemWithDefaultValueId] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[ItemWithDefaultValueIdArchive](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_ItemWithDefaultValueIdArchive_Id]  DEFAULT (newsequentialid()),
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ItemWithDefaultValueIdArchive] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[ItemWithIdentityId](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ItemWithIdentityId] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[ItemWithIdentityIdArchive](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ItemWithIdentityIdArchive] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[ItemWithIdentityIdAndDefaultValues](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[UniqueidentifierValue] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()),
	[IntValue] [int] NOT NULL DEFAULT (42),
 CONSTRAINT [PK_ItemWithIdentityIdAndDefaultValues] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[ItemWithIdentityIdAndDefaultValuesArchive](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[UniqueidentifierValue] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()),
	[IntValue] [int] NOT NULL DEFAULT (42),
 CONSTRAINT [PK_ItemWithIdentityIdAndDefaultValuesArchive] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[ItemWithInitialization](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ItemWithInitialization] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[ItemWithInitializationArchive](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ItemWithInitializationArchive] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[ItemWithPropertyModifiedTracking](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[IntValue] [int] NOT NULL,
 CONSTRAINT [PK_ItemWithPropertyModifiedTracking] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[ItemWithPropertyModifiedTrackingArchive](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[IntValue] [int] NOT NULL,
 CONSTRAINT [PK_ItemWithPropertyModifiedTrackingArchive] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[ItemWithSupportDbLoad](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[IntValue] [int] NOT NULL,
 CONSTRAINT [PK_ItemWithSupportDbLoad] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[ItemWithSupportDbLoadArchive](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[IntValue] [int] NOT NULL,
 CONSTRAINT [PK_ItemWithSupportDbLoadArchive] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[Label](
	[TableId] [nvarchar](50) NOT NULL,
	[Id] [int] NOT NULL,
	[Language] [nvarchar](3) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Label] PRIMARY KEY CLUSTERED 
(
	[TableId] ASC,
	[Id] ASC,
	[Language] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[LabelArchive](
	[TableId] [nvarchar](50) NOT NULL,
	[Id] [int] NOT NULL,
	[Language] [nvarchar](3) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_LabelArchive] PRIMARY KEY CLUSTERED 
(
	[TableId] ASC,
	[Id] ASC,
	[Language] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[LinkedItem](
	[Id] [int] NOT NULL,
	[PreviousId] [int] NULL,
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_LinkedItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[LinkedItemArchive](
	[Id] [int] NOT NULL,
	[PreviousId] [int] NULL,
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_LinkedItemArchive] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[LinkedItemChild](
	[Id] [int] NOT NULL,
	[LinkedItemId] [int] NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_LinkedItemChild] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[LinkedItemChildArchive](
	[Id] [int] NOT NULL,
	[LinkedItemId] [int] NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_LinkedItemChildArchive] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [test_schema].[ItemInSchema](
	[Id] [int] NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[RelatedItemId] [int] NULL,
	[Deleted] [datetime] NULL,
 CONSTRAINT [PK_ItemInSchema] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [test_schema].[ItemInSchemaArchive](
	[Id] [int] NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[RelatedItemId] [int] NULL,
	[Deleted] [datetime] NULL,
 CONSTRAINT [PK_ItemInSchemaArchive] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
