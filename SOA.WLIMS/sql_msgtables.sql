
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 02/07/2015 23:03:31
-- Generated from EDMX file: E:\soa物流信息系统简介\QG\MessageService\MessageService.DAL\ServiceData.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;

GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[MD_Message]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MD_Message];
GO
IF OBJECT_ID(N'[dbo].[MD_MessageLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MD_MessageLog];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'MD_Message'
CREATE TABLE [dbo].[MD_Message] (
    [ID] uniqueidentifier  NOT NULL,
    [SourceID] uniqueidentifier  NULL,
    [Mails] nvarchar(1024)  NULL,
    [Mobiles] nvarchar(1024)  NULL,
    [Receiver] nvarchar(255)  NULL,
    [Title] nvarchar(255)  NULL,
    [Content] varchar(max)  NULL,
    [MsgDate] datetime  NULL,
    [IsSended] bit  NULL
);
GO

-- Creating table 'MD_MessageLog'
CREATE TABLE [dbo].[MD_MessageLog] (
    [ID] uniqueidentifier  NOT NULL,
    [MessageID] uniqueidentifier  NULL,
    [SendTime] datetime  NULL,
    [SendType] nvarchar(50)  NULL,
    [IsSuccess] bit  NULL,
    [Exception] varchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'MD_Message'
ALTER TABLE [dbo].[MD_Message]
ADD CONSTRAINT [PK_MD_Message]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'MD_MessageLog'
ALTER TABLE [dbo].[MD_MessageLog]
ADD CONSTRAINT [PK_MD_MessageLog]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------