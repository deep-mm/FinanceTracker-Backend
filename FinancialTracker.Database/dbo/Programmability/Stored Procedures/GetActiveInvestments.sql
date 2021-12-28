/****** Object:  StoredProcedure [dbo].[GetRequiredAttentionParticipants]    Script Date: 10/28/2021 2:56:44 PM ******/
-- =============================================
-- Author:      Deep Mehta
-- Create Date: October 27,2021
-- Description: Get a list of participants that require attention
-- =============================================
CREATE PROCEDURE [dbo].[GetActiveInvestments]
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
    SELECT Investment.InvestmentName, Investment.InvestmentDate, Investment.InvestmentAmount, InvestmentType.InvestmentTypeName
	from Investment
	INNER JOIN InvestmentStatus on Investment.InvestmentStatusId=InvestmentStatus.InvestmentStatusId
    INNER JOIN InvestmentType on Investment.InvestmentTypeId=InvestmentType.InvestmentTypeId
	WHERE InvestmentStatus.InvestmentStatusName = 'Active'
	
END
