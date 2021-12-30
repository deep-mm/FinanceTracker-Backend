using FinanceTrackerAPI.Models.DAOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancialTracker.Core.Lib
{
    public interface IInvestmentRepository
    {
        Task<IEnumerable<Investment>> GetInvestments();
        Task<Investment> GetInvestmentById(int investmentId);
        Task<Investment> AddInvestment(Investment investment);
        Task<Investment> UpdateInvestment(Investment investment);
        Task<bool> DeleteInvestment(int investmentId);
        Task<PagedInvestmentResponse> GetPagedInvestments(int pageNumber, int pageSize, string searchText, IList<int> filter);
        Task<IEnumerable<ActiveInvestment>> GetActiveInvestments();
    }
}
