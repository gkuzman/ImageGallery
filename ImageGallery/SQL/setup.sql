IF NOT EXISTS 
   (
     SELECT name FROM master.dbo.sysdatabases 
     WHERE name = N'DistCache'
    )
BEGIN
CREATE DATABASE DistCache
END
GO

IF NOT EXISTS (SELECT * 
               FROM INFORMATION_SCHEMA.TABLES 
               WHERE TABLE_SCHEMA = 'dbo' 
                 AND TABLE_NAME = 'DistCache') 
BEGIN
USE DistCache
CREATE TABLE [dbo].[CacheTable](
    [Id] [nvarchar](449) NOT NULL,
    [Value] [varbinary](max) NOT NULL,
    [ExpiresAtTime] [datetimeoffset](7) NOT NULL,
    [SlidingExpirationInSeconds] [bigint] NULL,
    [AbsoluteExpiration] [datetimeoffset](7) NULL,
 CONSTRAINT [pk_Id] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
       IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, 
       ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
 
CREATE NONCLUSTERED INDEX [Index_ExpiresAtTime] ON [dbo].[CacheTable]
(
    [ExpiresAtTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
       SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, 
       ONLINE = OFF, ALLOW_ROW_LOCKS = ON, 
       ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
END
GO

IF NOT EXISTS 
   (
     SELECT name FROM master.dbo.sysdatabases 
     WHERE name = N'Serilog'
    )
BEGIN
CREATE DATABASE Serilog
END
GO