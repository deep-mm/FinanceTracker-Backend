using AutoMapper;
using FinanceTrackerAPI.Models.DTOs;
using FinancialTracker.Common;
using FinancialTracker.Core.Lib;
using FinancialTracker.Core.Lib.CoreServices;
using FinancialTracker.Models.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracker.Services
{
    public class InvestmentService : IInvestmentService
    {
        private readonly IInvestmentRepository investmentRepository;
        private IMapper mapper;
        private Helper helper;
        private string loggedInUsername;

        public InvestmentService(IInvestmentRepository investmentRepository, IMapper mapper, IOptions<AppInsightsOptions> options, ISessionService sessionService)
        {
            this.investmentRepository = investmentRepository;
            this.mapper = mapper;
            this.helper = new Helper(options, sessionService);
            this.loggedInUsername = helper.getLoggedInUsername();
        }

        public async Task<Investment> AddInvestment(Investment investment)
        {
            FinanceTrackerAPI.Models.DAOs.Investment investmentDAO = mapper.Map<FinanceTrackerAPI.Models.DAOs.Investment>(investment);
            investmentDAO = AddDefaultValues(investmentDAO);
            return mapper.Map<Investment>(await this.investmentRepository.AddInvestment(investmentDAO));
        }

        public async Task<bool> DeleteInvestment(int investmentId)
        {
            return await this.investmentRepository.DeleteInvestment(investmentId);
        }

        public async Task<IEnumerable<FinanceTrackerAPI.Models.DAOs.ActiveInvestment>> GetActiveInvestments()
        {
            IEnumerable<FinanceTrackerAPI.Models.DAOs.ActiveInvestment> activeInvestments = await this.investmentRepository.GetActiveInvestments();
            return mapper.Map<IEnumerable<FinanceTrackerAPI.Models.DAOs.ActiveInvestment>>(activeInvestments);
        }

        public async Task<Investment> GetInvestmentById(int investmentId)
        {
            return mapper.Map<Investment>(await this.investmentRepository.GetInvestmentById(investmentId));
        }

        public async Task<FinanceTrackerAPI.Models.DAOs.Investment> GetInvestmentDAOById(int investmentId)
        {
            return await this.investmentRepository.GetInvestmentById(investmentId);
        }

        public async Task<IEnumerable<Investment>> GetInvestments()
        {
            IEnumerable<FinanceTrackerAPI.Models.DAOs.Investment> investments = await this.investmentRepository.GetInvestments();
            return mapper.Map<IEnumerable<Investment>>(investments);
        }

        public async Task<PagedInvestmentResponse> GetPagedInvestments(int pageNumber, int pageSize, string searchText, IList<int> filter)
        {
            return mapper.Map<PagedInvestmentResponse>(await this.investmentRepository.GetPagedInvestments(pageNumber, pageSize, searchText, filter));
        }

        public async Task<Investment> UpdateInvestment(FinanceTrackerAPI.Models.DAOs.Investment investment)
        {
            investment = AddModifiedValues(investment);
            return mapper.Map<Investment>(await this.investmentRepository.UpdateInvestment(investment));
        }

        public async Task<Investment> UpdateInvestmentPut(Investment investment)
        {
            FinanceTrackerAPI.Models.DAOs.Investment investmentDAO = mapper.Map<FinanceTrackerAPI.Models.DAOs.Investment>(investment);
            FinanceTrackerAPI.Models.DAOs.Investment existingInvestment = await this.investmentRepository.GetInvestmentById(investment.InvestmentId);
            investmentDAO = AddModifiedValues(investmentDAO);
            investmentDAO.CreatedBy = existingInvestment.CreatedBy;
            investmentDAO.CreatedDate = existingInvestment.CreatedDate;
            return mapper.Map<Investment>(await this.investmentRepository.UpdateInvestment(investmentDAO));
        }

        public FinanceTrackerAPI.Models.DAOs.Investment AddDefaultValues(FinanceTrackerAPI.Models.DAOs.Investment investment)
        {
            investment.CreatedBy = loggedInUsername;
            investment.CreatedDate = DateTime.UtcNow;
            investment.ModifiedBy = loggedInUsername;
            investment.ModifiedDate = DateTime.UtcNow;
            investment.IsActive = true;

            return investment;
        }

        public FinanceTrackerAPI.Models.DAOs.Investment AddModifiedValues(FinanceTrackerAPI.Models.DAOs.Investment investment)
        {
            investment.ModifiedBy = loggedInUsername;
            investment.ModifiedDate = DateTime.UtcNow;
            investment.IsActive = true;

            return investment;
        }
    }
}
