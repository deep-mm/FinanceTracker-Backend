using System;

#nullable disable

namespace FinanceTrackerAPI.Models.DTOs
{
    public class Investment
    {
        public int InvestmentId { get; set; }
        public int InvestmentTypeId { get; set; }
        public DateTime InvestmentDate { get; set; }
        public string InvestmentName { get; set; }
        public decimal InvestmentAmount { get; set; }
        public int MemberId { get; set; }
        public int InvestmentStatusId { get; set; }
        public string Notes { get; set; }

        public InvestmentStatus InvestmentStatus { get; set; }
        public InvestmentType InvestmentType { get; set; }
        public Member Member { get; set; }
    }
}
