using FinanceTrackerAPI.Models.DAOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTrackerAPI.Models.DTOs
{
    public class PagedInvestmentResponse
    {
        public int TotalRecords { get; set; }
        public IEnumerable<Investment> Investments { get; set; }
    }
}
