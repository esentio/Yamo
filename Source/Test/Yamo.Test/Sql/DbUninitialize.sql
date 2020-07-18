exec sp_MSforeachtable 'DROP TABLE ?'
GO
IF EXISTS (SELECT * FROM sys.schemas WHERE name = N'test_schema') DROP SCHEMA [test_schema]
