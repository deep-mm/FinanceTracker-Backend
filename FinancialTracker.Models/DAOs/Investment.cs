using System;

#nullable disable

namespace FinanceTrackerAPI.Models.DAOs
{
    public partial class Investment
    {
        public int InvestmentId { get; set; }
        public int InvestmentTypeId { get; set; }
        public DateTime InvestmentDate { get; set; }
        public string InvestmentName { get; set; }
        public decimal InvestmentAmount { get; set; }
        public int MemberId { get; set; }
        public int InvestmentStatusId { get; set; }
        public string InvestmentNotes { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual InvestmentStatus InvestmentStatus { get; set; }
        public virtual InvestmentType InvestmentType { get; set; }
        public virtual Member Member { get; set; }
    }
}
