CREATE TABLE [dbo].[Member] (
    [MemberId]   INT            IDENTITY (1, 1) NOT NULL,
    [MemberName] NVARCHAR (50)  NOT NULL,
    [CreatedBy]      NVARCHAR (255) NOT NULL,
    [CreatedDate]    DATETIME       NOT NULL,
    [ModifiedBy]     NVARCHAR (255) NOT NULL,
    [ModifiedDate]   DATETIME       NOT NULL,
    [IsActive]       BIT            DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([MemberId] ASC)
);