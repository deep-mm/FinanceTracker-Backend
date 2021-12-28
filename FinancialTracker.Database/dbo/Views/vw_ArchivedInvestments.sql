CREATE   VIEW dbo.vw_ArchivedInvestments
AS
SELECT 
investment.InvestmentDate,
investment.InvestmentAmount,
investment.InvestmentName
from
Investment investment
INNER JOIN InvestmentStatus ist ON ist.InvestmentStatusId = investment.InvestmentStatusId
where ist.InvestmentStatusName = 'Archived'
