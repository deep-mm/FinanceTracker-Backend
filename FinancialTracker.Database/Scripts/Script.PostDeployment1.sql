/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
/*SET IDENTITY_INSERT [dbo].[InvestmentStatus] ON 
GO */
INSERT [dbo].[InvestmentStatus] ([InvestmentStatusId], [InvestmentStatusName], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsActive]) VALUES (1, N'Draft', N'Deep Mehta', CAST(N'2021-12-28T14:53:50.783' AS DateTime), N'Deep Mehta', CAST(N'2021-12-28T14:53:50.783' AS DateTime), 1)
GO
INSERT [dbo].[InvestmentStatus] ([InvestmentStatusId], [InvestmentStatusName], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsActive]) VALUES (2, N'Active', N'Deep Mehta', CAST(N'2021-12-28T14:53:50.783' AS DateTime), N'Deep Mehta', CAST(N'2021-12-28T14:53:50.783' AS DateTime), 1)
GO
INSERT [dbo].[InvestmentStatus] ([InvestmentStatusId], [InvestmentStatusName], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsActive]) VALUES (3, N'Archived', N'Deep Mehta', CAST(N'2021-12-28T14:53:50.783' AS DateTime), N'Deep Mehta', CAST(N'2021-12-28T14:53:50.783' AS DateTime), 1)
GO

/* SET IDENTITY_INSERT [dbo].[InvestmentType] ON 
GO */
INSERT [dbo].[InvestmentType] ([InvestmentTypeId], [InvestmentTypeName], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsActive]) VALUES (1, N'Stocks', N'Deep Mehta', CAST(N'2021-12-28T14:53:50.783' AS DateTime), N'Deep Mehta', CAST(N'2021-12-28T14:53:50.783' AS DateTime), 1)
GO
INSERT [dbo].[InvestmentType] ([InvestmentTypeId], [InvestmentTypeName], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsActive]) VALUES (2, N'Mutual Fund', N'Deep Mehta', CAST(N'2021-12-28T14:53:50.783' AS DateTime), N'Deep Mehta', CAST(N'2021-12-28T14:53:50.783' AS DateTime), 1)
GO
INSERT [dbo].[InvestmentType] ([InvestmentTypeId], [InvestmentTypeName], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsActive]) VALUES (3, N'Property', N'Deep Mehta', CAST(N'2021-12-28T14:53:50.783' AS DateTime), N'Deep Mehta', CAST(N'2021-12-28T14:53:50.783' AS DateTime), 1)
GO
INSERT [dbo].[InvestmentType] ([InvestmentTypeId], [InvestmentTypeName], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsActive]) VALUES (4, N'Savings', N'Deep Mehta', CAST(N'2021-12-28T14:53:50.783' AS DateTime), N'Deep Mehta', CAST(N'2021-12-28T14:53:50.783' AS DateTime), 1)
GO
INSERT [dbo].[InvestmentType] ([InvestmentTypeId], [InvestmentTypeName], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsActive]) VALUES (5, N'Insurance', N'Deep Mehta', CAST(N'2021-12-28T14:53:50.783' AS DateTime), N'Deep Mehta', CAST(N'2021-12-28T14:53:50.783' AS DateTime), 1)
GO
INSERT [dbo].[InvestmentType] ([InvestmentTypeId], [InvestmentTypeName], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsActive]) VALUES (6, N'Others', N'Deep Mehta', CAST(N'2021-12-28T14:53:50.783' AS DateTime), N'Deep Mehta', CAST(N'2021-12-28T14:53:50.783' AS DateTime), 1)
GO

/* SET IDENTITY_INSERT [dbo].[Member] ON 
GO */
INSERT [dbo].[Member] ([MemberId], [MemberName], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsActive]) VALUES (1, N'Manoj Mehta', N'Deep Mehta', CAST(N'2021-12-28T14:53:50.783' AS DateTime), N'Deep Mehta', CAST(N'2021-12-28T14:53:50.783' AS DateTime), 1)
GO
INSERT [dbo].[Member] ([MemberId], [MemberName], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsActive]) VALUES (2, N'Bina Mehta', N'Deep Mehta', CAST(N'2021-12-28T14:53:50.783' AS DateTime), N'Deep Mehta', CAST(N'2021-12-28T14:53:50.783' AS DateTime), 1)
GO
INSERT [dbo].[Member] ([MemberId], [MemberName], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsActive]) VALUES (3, N'Harsh Mehta', N'Deep Mehta', CAST(N'2021-12-28T14:53:50.783' AS DateTime), N'Deep Mehta', CAST(N'2021-12-28T14:53:50.783' AS DateTime), 1)
GO
INSERT [dbo].[Member] ([MemberId], [MemberName], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsActive]) VALUES (4, N'Deep Mehta', N'Deep Mehta', CAST(N'2021-12-28T14:53:50.783' AS DateTime), N'Deep Mehta', CAST(N'2021-12-28T14:53:50.783' AS DateTime), 1)
GO
INSERT [dbo].[Member] ([MemberId], [MemberName], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsActive]) VALUES (5, N'Jayotsna Mehta', N'Deep Mehta', CAST(N'2021-12-28T14:53:50.783' AS DateTime), N'Deep Mehta', CAST(N'2021-12-28T14:53:50.783' AS DateTime), 1)
GO