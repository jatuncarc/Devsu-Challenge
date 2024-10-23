use master
go
IF NOT EXISTS(SELECT * FROM sys.databases where name = 'DEVSU')
CREATE DATABASE DEVSU
GO

USE DEVSU
GO

IF EXISTS (select * from sys.foreign_keys where name = 'FK_Customer_Person')
ALTER TABLE Customer DROP CONSTRAINT [FK_Customer_Person]

GO
IF EXISTS (select * from sys.foreign_keys where name = 'FK_Account_Customer')
ALTER TABLE Account DROP CONSTRAINT FK_Account_Customer

GO
IF EXISTS (select * from sys.foreign_keys where name = 'FK_Movement_Account')
ALTER TABLE Movement DROP CONSTRAINT FK_Movement_Account

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND type in (N'U'))
DROP TABLE Customer
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Person]') AND type in (N'U'))
DROP TABLE Person
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Account]') AND type in (N'U'))
DROP TABLE Account
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Movement]') AND type in (N'U'))
DROP TABLE [dbo].[Movement]
GO

CREATE TABLE [dbo].[Person](
	Id [int] IDENTITY(1,1) NOT NULL,
	Name [varchar](50) NULL,
	Gender [char](1) NULL,
	Age [int] NULL,
	Identification [int] NULL,
	Address [varchar](100) NULL,
	Phone [varchar](20) NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED (Id ASC)) ON [PRIMARY]
GO


CREATE TABLE [dbo].[Customer](
	Id [int] NOT NULL,
	Password [varchar](50) NULL,
	State [bit] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED (Id ASC)) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Person] FOREIGN KEY([Id]) REFERENCES [dbo].[Person] ([Id])
GO

ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_Person]
GO


CREATE TABLE [dbo].[Account](
	Number bigint NOT NULL,
	AccountType [varchar](10) NULL,
	OpeningBalance [decimal](18, 2) NULL,
	State [bit] NULL,
	CustomerId [int] NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED (Number ASC)) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Customer] FOREIGN KEY(CustomerId) REFERENCES [Customer] ([Id])
GO

ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Customer]
GO


CREATE TABLE [dbo].[Movement](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	Date [datetime] DEFAULT(GETDATE()) NULL,
	[MovementType] [char](3) NULL,
	[Value] [decimal](18, 2) NULL,
	[Balance] [decimal](18, 2) NULL,
	[AccountId] [bigint] NULL,
 CONSTRAINT [PK_Movement] PRIMARY KEY CLUSTERED (	[Id] ASC)) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Movement]  WITH CHECK ADD  CONSTRAINT [FK_Movement_Account] FOREIGN KEY([AccountId])REFERENCES [dbo].[Account] (Number)
GO

ALTER TABLE [dbo].[Movement] CHECK CONSTRAINT [FK_Movement_Account]
GO

--****************************************************************************************************

INSERT INTO Person VALUES('JOSE LEMA','M',33,'40404040','AV LIMA 8888','914653332')
INSERT INTO Customer VALUES(SCOPE_IDENTITY(),'A1B2C3D4',1)

INSERT INTO Person VALUES('MARIANELA MONTALVO','F',22,'10101010','AV AMAZONAS 123','995678334')
INSERT INTO Customer VALUES(SCOPE_IDENTITY(),'AAAAAA',1)

INSERT INTO Person VALUES('JUAN OSORIO','M',67,'30303030','AV LAS FLORES 444','987878776')
INSERT INTO Customer VALUES(SCOPE_IDENTITY(),'BBBBBB',1)

INSERT INTO Person VALUES('ROBERTO ROJAS','M',27,'80808080','AV LOS BOSQUES 333','907885282')
INSERT INTO Customer VALUES(SCOPE_IDENTITY(),'CCCCCC',1)

INSERT INTO Person VALUES('DANIEL MORALES','M',43,'60503020','AV FERROCARRIL 777','90909898')
INSERT INTO Customer VALUES(SCOPE_IDENTITY(),'DDDDDD',1)

INSERT INTO Person VALUES('BRENDA ZAPATA','F',22,'1020304050','AV BENAVIDES 909','95767822')
INSERT INTO Customer VALUES(SCOPE_IDENTITY(),'EEEEEE',1)


--****************************************************************************************************
INSERT INTO Account VALUES(1111111111,'Ahorros',500,1,1)
INSERT INTO Account VALUES(2222222222,'Corriente',600,1,1)
INSERT INTO Account VALUES(3333333333,'Ahorros',700,1,2)
INSERT INTO Account VALUES(4444444444,'Ahorros',1000,1,2)
INSERT INTO Account VALUES(5555555555,'Corriente',200,1,3)
INSERT INTO Account VALUES(6666666666,'Corriente',100,1,4)


INSERT INTO Movement VALUES(getdate(),'D',10,510,1111111111)

INSERT INTO Movement VALUES(getdate(),'D',100,700,2222222222)
INSERT INTO Movement VALUES(getdate(),'C',-20,680,2222222222)


INSERT INTO Movement VALUES(getdate(),'D',150,850,3333333333)
INSERT INTO Movement VALUES(getdate(),'C',-50,800,3333333333)


INSERT INTO Movement VALUES(getdate(),'C',-500,500,4444444444)
