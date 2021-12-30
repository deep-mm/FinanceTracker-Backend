using AutoMapper;
using FinanceTrackerAPI.Models.DAOs;

namespace FinancialTracker.Common
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<FinanceTrackerAPI.Models.DTOs.InvestmentStatus, InvestmentStatus>().ReverseMap();
            CreateMap<FinanceTrackerAPI.Models.DTOs.InvestmentType, InvestmentType>().ReverseMap();
            CreateMap<FinanceTrackerAPI.Models.DTOs.Member, Member>().ReverseMap();
            CreateMap<FinanceTrackerAPI.Models.DTOs.Investment, Investment>()
                .ForMember(x => x.InvestmentNotes, y => y.MapFrom(z => z.Notes));
            CreateMap<Investment, FinanceTrackerAPI.Models.DTOs.Investment>()
                .ForMember(x => x.Notes, y => y.MapFrom(z => z.InvestmentNotes));
            CreateMap<FinanceTrackerAPI.Models.DTOs.PagedInvestmentResponse, PagedInvestmentResponse>().ReverseMap();
        }
    }
}
