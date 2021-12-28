﻿CREATE TABLE [dbo].[Investment] (
    [InvestmentId]                            INT             IDENTITY (1, 1) NOT NULL,
    [InvestmentTypeId]                      INT             NOT NULL,
    [InvestmentDate]                             DATETIME        NOT NULL,
    [InvestmentName]                     NVARCHAR (255)  NOT NULL,
    [InvestmentAmount]                       DECIMAL     NOT NULL,
    [MemberId]                            INT NOT NULL,
    [InvestmentStatusId]                            INT NOT NULL,
    [InvestmentNotes]                     NVARCHAR (2000)  NOT NULL,
    [CreatedBy]                          NVARCHAR (255)  NOT NULL,
    [CreatedDate]                        DATETIME        NOT NULL,
    [ModifiedBy]                         NVARCHAR (255)  NOT NULL,
    [ModifiedDate]                       DATETIME        NOT NULL,
    [IsActive]                           BIT             DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([InvestmentId] ASC),
    CONSTRAINT [FK_Investment_InvestmentType] FOREIGN KEY ([InvestmentTypeId]) REFERENCES [dbo].[InvestmentType] ([InvestmentTypeId]),
    CONSTRAINT [FK_Investment_Member] FOREIGN KEY ([MemberId]) REFERENCES [dbo].[Member] ([MemberId]),
    CONSTRAINT [FK_Investment_InvestmentStatus] FOREIGN KEY ([InvestmentStatusId]) REFERENCES [dbo].[InvestmentStatus] ([InvestmentStatusId]),
);