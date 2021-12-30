using FinanceTrackerAPI.Models.DAOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTrackerAPI.Models.DAOs
{
    public class PagedInvestmentResponse
    {
        public int TotalRecords { get; set; }
        public IEnumerable<Investment> Investments { get; set; }

        public PagedInvestmentResponse(int TotalRecords, IEnumerable<Investment> Investments)
        {
            this.TotalRecords = TotalRecords;
            this.Investments = Investments;
        }
    }
}
