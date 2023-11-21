CREATE TABLE [dbo].[Patients] (
    [PID]            INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]      NVARCHAR (50)  NULL,
    [LastName]       NVARCHAR (50)  NULL,
    [BirthDate]      DATETIME       NULL,
    [Gender]         VARCHAR (50)   NULL,
    [MiddleName]     NVARCHAR (50)  NULL,
    [Address1]       NVARCHAR (255) NULL,
    [Address2]       NVARCHAR (255) NULL,
    [City]           NVARCHAR (255) NULL,
    [State]          NVARCHAR (255) NULL,
    [Country]        NVARCHAR (255) NULL,
    [ZIP]            NVARCHAR (10)  NULL,
    [SystemStatus]   NVARCHAR (10)  NULL,
    [DateCreated]    DATETIME       NULL,
    [DateLastUpdate] DATETIME       NULL,
    [AgeYears]       AS             (datediff(year,[BirthDate],getdate())),
    PRIMARY KEY CLUSTERED ([PID] ASC)
);

