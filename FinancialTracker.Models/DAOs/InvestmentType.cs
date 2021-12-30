using System;
using System.Collections.Generic;

#nullable disable

namespace FinanceTrackerAPI.Models.DAOs
{
    public partial class InvestmentType
    {
        public InvestmentType()
        {
            Investments = new HashSet<Investment>();
        }

        public int InvestmentTypeId { get; set; }
        public string InvestmentTypeName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Investment> Investments { get; set; }
    }
}
