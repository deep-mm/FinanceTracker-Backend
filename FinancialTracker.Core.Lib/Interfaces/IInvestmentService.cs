using FinanceTrackerAPI.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancialTracker.Core.Lib
{
    public interface IInvestmentService
    {
        Task<IEnumerable<Investment>> GetInvestments();
        Task<Investment> GetInvestmentById(int investmentId);
        Task<FinanceTrackerAPI.Models.DAOs.Investment> GetInvestmentDAOById(int investmentId);
        Task<Investment> AddInvestment(Investment investment);
        Task<Investment> UpdateInvestment(FinanceTrackerAPI.Models.DAOs.Investment investment);
        Task<Investment> UpdateInvestmentPut(Investment investment);
        Task<bool> DeleteInvestment(int investmentId);
        Task<PagedInvestmentResponse> GetPagedInvestments(int pageNumber, int pageSize, string searchText, IList<int> filter);
        Task<IEnumerable<FinanceTrackerAPI.Models.DAOs.ActiveInvestment>> GetActiveInvestments();
        Task<IEnumerable<InvestmentType>> GetInvestmentTypes();
        Task<IEnumerable<InvestmentStatus>> GetInvestmentStatuses();
        Task<IEnumerable<Member>> GetMembers();
    }
}
