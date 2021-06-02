
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/02/2021 02:15:03
-- Generated from EDMX file: C:\Users\tomic\OneDrive\Desktop\BP2\Projekat\Kosarka\DatabaseModel\KosarkaDb.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [KosarkaDb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_TakmicenjeUcestvuje]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ucestvujes] DROP CONSTRAINT [FK_TakmicenjeUcestvuje];
GO
IF OBJECT_ID(N'[dbo].[FK_KlubKosarkas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Kosarkas] DROP CONSTRAINT [FK_KlubKosarkas];
GO
IF OBJECT_ID(N'[dbo].[FK_UcestvujeKlub]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ucestvujes] DROP CONSTRAINT [FK_UcestvujeKlub];
GO
IF OBJECT_ID(N'[dbo].[FK_TrofejUcestvuje]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Trofejs] DROP CONSTRAINT [FK_TrofejUcestvuje];
GO
IF OBJECT_ID(N'[dbo].[FK_Kup_inherits_Takmicenje]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Takmicenjes_Kup] DROP CONSTRAINT [FK_Kup_inherits_Takmicenje];
GO
IF OBJECT_ID(N'[dbo].[FK_Liga_inherits_Takmicenje]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Takmicenjes_Liga] DROP CONSTRAINT [FK_Liga_inherits_Takmicenje];
GO
IF OBJECT_ID(N'[dbo].[FK_Plejmejker_inherits_Kosarkas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Kosarkas_Plejmejker] DROP CONSTRAINT [FK_Plejmejker_inherits_Kosarkas];
GO
IF OBJECT_ID(N'[dbo].[FK_BekSuter_inherits_Kosarkas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Kosarkas_BekSuter] DROP CONSTRAINT [FK_BekSuter_inherits_Kosarkas];
GO
IF OBJECT_ID(N'[dbo].[FK_Krilo_inherits_Kosarkas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Kosarkas_Krilo] DROP CONSTRAINT [FK_Krilo_inherits_Kosarkas];
GO
IF OBJECT_ID(N'[dbo].[FK_KrilniCentar_inherits_Kosarkas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Kosarkas_KrilniCentar] DROP CONSTRAINT [FK_KrilniCentar_inherits_Kosarkas];
GO
IF OBJECT_ID(N'[dbo].[FK_Centar_inherits_Kosarkas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Kosarkas_Centar] DROP CONSTRAINT [FK_Centar_inherits_Kosarkas];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Takmicenjes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Takmicenjes];
GO
IF OBJECT_ID(N'[dbo].[Kosarkas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Kosarkas];
GO
IF OBJECT_ID(N'[dbo].[Ucestvujes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ucestvujes];
GO
IF OBJECT_ID(N'[dbo].[Trofejs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Trofejs];
GO
IF OBJECT_ID(N'[dbo].[Klubs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Klubs];
GO
IF OBJECT_ID(N'[dbo].[Takmicenjes_Kup]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Takmicenjes_Kup];
GO
IF OBJECT_ID(N'[dbo].[Takmicenjes_Liga]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Takmicenjes_Liga];
GO
IF OBJECT_ID(N'[dbo].[Kosarkas_Plejmejker]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Kosarkas_Plejmejker];
GO
IF OBJECT_ID(N'[dbo].[Kosarkas_BekSuter]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Kosarkas_BekSuter];
GO
IF OBJECT_ID(N'[dbo].[Kosarkas_Krilo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Kosarkas_Krilo];
GO
IF OBJECT_ID(N'[dbo].[Kosarkas_KrilniCentar]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Kosarkas_KrilniCentar];
GO
IF OBJECT_ID(N'[dbo].[Kosarkas_Centar]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Kosarkas_Centar];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Takmicenjes'
CREATE TABLE [dbo].[Takmicenjes] (
    [IdTakmicenja] int IDENTITY(1,1) NOT NULL,
    [MestoTakmicenja] nvarchar(max)  NOT NULL,
    [NazivTakmicenja] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Kosarkas'
CREATE TABLE [dbo].[Kosarkas] (
    [IdKosarkasa] int IDENTITY(1,1) NOT NULL,
    [ImeKosarkasa] nvarchar(max)  NOT NULL,
    [PrezimeKosarkasa] nvarchar(max)  NOT NULL,
    [BrojDresaKosarkasa] int  NOT NULL,
    [Klub_IdKluba] int  NOT NULL
);
GO

-- Creating table 'Ucestvujes'
CREATE TABLE [dbo].[Ucestvujes] (
    [Takmicenje_IdTakmicenja] int  NOT NULL,
    [Klub_IdKluba] int  NOT NULL
);
GO

-- Creating table 'Trofejs'
CREATE TABLE [dbo].[Trofejs] (
    [IdTrofeja] int IDENTITY(1,1) NOT NULL,
    [UcestvujeTakmicenje_IdTakmicenja] int  NULL,
    [UcestvujeKlub_IdKluba] int  NULL
);
GO

-- Creating table 'Klubs'
CREATE TABLE [dbo].[Klubs] (
    [IdKluba] int IDENTITY(1,1) NOT NULL,
    [ImeKluba] nvarchar(max)  NOT NULL,
    [VlasnikKluba] nvarchar(max)  NOT NULL,
    [TrenerKluba] nvarchar(max)  NOT NULL,
    [NavijaciKluba] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Takmicenjes_Kup'
CREATE TABLE [dbo].[Takmicenjes_Kup] (
    [IdTakmicenja] int  NOT NULL
);
GO

-- Creating table 'Takmicenjes_Liga'
CREATE TABLE [dbo].[Takmicenjes_Liga] (
    [IdTakmicenja] int  NOT NULL
);
GO

-- Creating table 'Kosarkas_Plejmejker'
CREATE TABLE [dbo].[Kosarkas_Plejmejker] (
    [IdKosarkasa] int  NOT NULL
);
GO

-- Creating table 'Kosarkas_BekSuter'
CREATE TABLE [dbo].[Kosarkas_BekSuter] (
    [IdKosarkasa] int  NOT NULL
);
GO

-- Creating table 'Kosarkas_Krilo'
CREATE TABLE [dbo].[Kosarkas_Krilo] (
    [IdKosarkasa] int  NOT NULL
);
GO

-- Creating table 'Kosarkas_KrilniCentar'
CREATE TABLE [dbo].[Kosarkas_KrilniCentar] (
    [IdKosarkasa] int  NOT NULL
);
GO

-- Creating table 'Kosarkas_Centar'
CREATE TABLE [dbo].[Kosarkas_Centar] (
    [IdKosarkasa] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdTakmicenja] in table 'Takmicenjes'
ALTER TABLE [dbo].[Takmicenjes]
ADD CONSTRAINT [PK_Takmicenjes]
    PRIMARY KEY CLUSTERED ([IdTakmicenja] ASC);
GO

-- Creating primary key on [IdKosarkasa] in table 'Kosarkas'
ALTER TABLE [dbo].[Kosarkas]
ADD CONSTRAINT [PK_Kosarkas]
    PRIMARY KEY CLUSTERED ([IdKosarkasa] ASC);
GO

-- Creating primary key on [Takmicenje_IdTakmicenja], [Klub_IdKluba] in table 'Ucestvujes'
ALTER TABLE [dbo].[Ucestvujes]
ADD CONSTRAINT [PK_Ucestvujes]
    PRIMARY KEY CLUSTERED ([Takmicenje_IdTakmicenja], [Klub_IdKluba] ASC);
GO

-- Creating primary key on [IdTrofeja] in table 'Trofejs'
ALTER TABLE [dbo].[Trofejs]
ADD CONSTRAINT [PK_Trofejs]
    PRIMARY KEY CLUSTERED ([IdTrofeja] ASC);
GO

-- Creating primary key on [IdKluba] in table 'Klubs'
ALTER TABLE [dbo].[Klubs]
ADD CONSTRAINT [PK_Klubs]
    PRIMARY KEY CLUSTERED ([IdKluba] ASC);
GO

-- Creating primary key on [IdTakmicenja] in table 'Takmicenjes_Kup'
ALTER TABLE [dbo].[Takmicenjes_Kup]
ADD CONSTRAINT [PK_Takmicenjes_Kup]
    PRIMARY KEY CLUSTERED ([IdTakmicenja] ASC);
GO

-- Creating primary key on [IdTakmicenja] in table 'Takmicenjes_Liga'
ALTER TABLE [dbo].[Takmicenjes_Liga]
ADD CONSTRAINT [PK_Takmicenjes_Liga]
    PRIMARY KEY CLUSTERED ([IdTakmicenja] ASC);
GO

-- Creating primary key on [IdKosarkasa] in table 'Kosarkas_Plejmejker'
ALTER TABLE [dbo].[Kosarkas_Plejmejker]
ADD CONSTRAINT [PK_Kosarkas_Plejmejker]
    PRIMARY KEY CLUSTERED ([IdKosarkasa] ASC);
GO

-- Creating primary key on [IdKosarkasa] in table 'Kosarkas_BekSuter'
ALTER TABLE [dbo].[Kosarkas_BekSuter]
ADD CONSTRAINT [PK_Kosarkas_BekSuter]
    PRIMARY KEY CLUSTERED ([IdKosarkasa] ASC);
GO

-- Creating primary key on [IdKosarkasa] in table 'Kosarkas_Krilo'
ALTER TABLE [dbo].[Kosarkas_Krilo]
ADD CONSTRAINT [PK_Kosarkas_Krilo]
    PRIMARY KEY CLUSTERED ([IdKosarkasa] ASC);
GO

-- Creating primary key on [IdKosarkasa] in table 'Kosarkas_KrilniCentar'
ALTER TABLE [dbo].[Kosarkas_KrilniCentar]
ADD CONSTRAINT [PK_Kosarkas_KrilniCentar]
    PRIMARY KEY CLUSTERED ([IdKosarkasa] ASC);
GO

-- Creating primary key on [IdKosarkasa] in table 'Kosarkas_Centar'
ALTER TABLE [dbo].[Kosarkas_Centar]
ADD CONSTRAINT [PK_Kosarkas_Centar]
    PRIMARY KEY CLUSTERED ([IdKosarkasa] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Takmicenje_IdTakmicenja] in table 'Ucestvujes'
ALTER TABLE [dbo].[Ucestvujes]
ADD CONSTRAINT [FK_TakmicenjeUcestvuje]
    FOREIGN KEY ([Takmicenje_IdTakmicenja])
    REFERENCES [dbo].[Takmicenjes]
        ([IdTakmicenja])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Klub_IdKluba] in table 'Kosarkas'
ALTER TABLE [dbo].[Kosarkas]
ADD CONSTRAINT [FK_KlubKosarkas]
    FOREIGN KEY ([Klub_IdKluba])
    REFERENCES [dbo].[Klubs]
        ([IdKluba])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_KlubKosarkas'
CREATE INDEX [IX_FK_KlubKosarkas]
ON [dbo].[Kosarkas]
    ([Klub_IdKluba]);
GO

-- Creating foreign key on [Klub_IdKluba] in table 'Ucestvujes'
ALTER TABLE [dbo].[Ucestvujes]
ADD CONSTRAINT [FK_UcestvujeKlub]
    FOREIGN KEY ([Klub_IdKluba])
    REFERENCES [dbo].[Klubs]
        ([IdKluba])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UcestvujeKlub'
CREATE INDEX [IX_FK_UcestvujeKlub]
ON [dbo].[Ucestvujes]
    ([Klub_IdKluba]);
GO

-- Creating foreign key on [UcestvujeTakmicenje_IdTakmicenja], [UcestvujeKlub_IdKluba] in table 'Trofejs'
ALTER TABLE [dbo].[Trofejs]
ADD CONSTRAINT [FK_TrofejUcestvuje]
    FOREIGN KEY ([UcestvujeTakmicenje_IdTakmicenja], [UcestvujeKlub_IdKluba])
    REFERENCES [dbo].[Ucestvujes]
        ([Takmicenje_IdTakmicenja], [Klub_IdKluba])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TrofejUcestvuje'
CREATE INDEX [IX_FK_TrofejUcestvuje]
ON [dbo].[Trofejs]
    ([UcestvujeTakmicenje_IdTakmicenja], [UcestvujeKlub_IdKluba]);
GO

-- Creating foreign key on [IdTakmicenja] in table 'Takmicenjes_Kup'
ALTER TABLE [dbo].[Takmicenjes_Kup]
ADD CONSTRAINT [FK_Kup_inherits_Takmicenje]
    FOREIGN KEY ([IdTakmicenja])
    REFERENCES [dbo].[Takmicenjes]
        ([IdTakmicenja])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [IdTakmicenja] in table 'Takmicenjes_Liga'
ALTER TABLE [dbo].[Takmicenjes_Liga]
ADD CONSTRAINT [FK_Liga_inherits_Takmicenje]
    FOREIGN KEY ([IdTakmicenja])
    REFERENCES [dbo].[Takmicenjes]
        ([IdTakmicenja])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [IdKosarkasa] in table 'Kosarkas_Plejmejker'
ALTER TABLE [dbo].[Kosarkas_Plejmejker]
ADD CONSTRAINT [FK_Plejmejker_inherits_Kosarkas]
    FOREIGN KEY ([IdKosarkasa])
    REFERENCES [dbo].[Kosarkas]
        ([IdKosarkasa])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [IdKosarkasa] in table 'Kosarkas_BekSuter'
ALTER TABLE [dbo].[Kosarkas_BekSuter]
ADD CONSTRAINT [FK_BekSuter_inherits_Kosarkas]
    FOREIGN KEY ([IdKosarkasa])
    REFERENCES [dbo].[Kosarkas]
        ([IdKosarkasa])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [IdKosarkasa] in table 'Kosarkas_Krilo'
ALTER TABLE [dbo].[Kosarkas_Krilo]
ADD CONSTRAINT [FK_Krilo_inherits_Kosarkas]
    FOREIGN KEY ([IdKosarkasa])
    REFERENCES [dbo].[Kosarkas]
        ([IdKosarkasa])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [IdKosarkasa] in table 'Kosarkas_KrilniCentar'
ALTER TABLE [dbo].[Kosarkas_KrilniCentar]
ADD CONSTRAINT [FK_KrilniCentar_inherits_Kosarkas]
    FOREIGN KEY ([IdKosarkasa])
    REFERENCES [dbo].[Kosarkas]
        ([IdKosarkasa])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [IdKosarkasa] in table 'Kosarkas_Centar'
ALTER TABLE [dbo].[Kosarkas_Centar]
ADD CONSTRAINT [FK_Centar_inherits_Kosarkas]
    FOREIGN KEY ([IdKosarkasa])
    REFERENCES [dbo].[Kosarkas]
        ([IdKosarkasa])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------