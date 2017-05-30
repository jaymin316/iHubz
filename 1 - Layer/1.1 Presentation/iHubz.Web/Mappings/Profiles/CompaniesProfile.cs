using AutoMapper;
using iHubz.Domain.MainModule.CompanyEntities;
using iHubz.Infrastructure.CrossCutting.Extensions;
using iHubz.Web.Models;

namespace iHubz.Web.Mappings.Profiles
{
    public class CompaniesProfile : Profile
    {
        protected override void Configure()
        {
            #region CompaniesViewModel -> Companies

            Mapper.CreateMap<CompaniesViewModel, Companies>()
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
                .ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Website))
                .IgnoreAllNonExisting();

            #endregion

            #region Companies -> CompaniesViewModel

            Mapper.CreateMap<Companies, CompaniesViewModel>()
                .IgnoreAllNonExisting();

            #endregion

            Mapper.AssertConfigurationIsValid();
        }
    }
}