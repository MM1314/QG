
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 02/07/2015 23:03:31
-- Generated from EDMX file: E:\soa物流信息系统简介\QG\MessageService\MessageService.DAL\ServiceData.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TytusB2B];
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
IF OBJECT_ID(N'[dbo].[PO_ShipOrder]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PO_ShipOrder];
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

-- Creating table 'PO_ShipOrder'
CREATE TABLE [dbo].[PO_ShipOrder] (
    [ShipOrderID] uniqueidentifier  NOT NULL,
    [POID] uniqueidentifier  NULL,
    [ShipOrderCode] nvarchar(50)  NULL,
    [ShipOrderName] nvarchar(255)  NULL,
    [ShipDate] datetime  NULL,
    [ShipStatus] nvarchar(10)  NULL,
    [DealerID] uniqueidentifier  NULL,
    [DealerCode] nvarchar(50)  NULL,
    [DealerName] nvarchar(50)  NULL,
    [CheckUser] nvarchar(30)  NULL,
    [CheckDate] datetime  NULL,
    [Remark] nvarchar(100)  NULL,
    [Description] nvarchar(500)  NULL,
    [IsDelete] bit  NOT NULL,
    [IsDisable] bit  NOT NULL,
    [Creator] nvarchar(50)  NOT NULL,
    [CreateTime] datetime  NOT NULL,
    [Modifier] nvarchar(50)  NOT NULL,
    [ModifyTime] datetime  NOT NULL,
    [WarehouseID] uniqueidentifier  NULL,
    [WarehouseCode] nvarchar(50)  NULL,
    [WarehouseName] nvarchar(50)  NULL,
    [ImgURL] nvarchar(500)  NULL,
    [POType] nvarchar(10)  NULL,
    [AdvancedPayment] int  NULL,
    [ActualAmount] decimal(18,2)  NULL
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

-- Creating primary key on [ShipOrderID] in table 'PO_ShipOrder'
ALTER TABLE [dbo].[PO_ShipOrder]
ADD CONSTRAINT [PK_PO_ShipOrder]
    PRIMARY KEY CLUSTERED ([ShipOrderID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------