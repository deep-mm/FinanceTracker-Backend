using System;

#nullable disable

namespace FinanceTrackerAPI.Models.DAOs
{
    public partial class VwArchivedInvestment
    {
        public DateTime InvestmentDate { get; set; }
        public decimal InvestmentAmount { get; set; }
        public string InvestmentName { get; set; }
    }
}
