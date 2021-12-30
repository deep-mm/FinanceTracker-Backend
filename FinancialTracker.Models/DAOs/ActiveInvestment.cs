using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTrackerAPI.Models.DAOs
{
    public class ActiveInvestment
    {
        public string InvestmentName { get; set; }
        public DateTime InvestmentDate { get; set; }
        public decimal InvestmentAmount { get; set; }
        public string InvestmentTypeName { get; set; }
    }
}
