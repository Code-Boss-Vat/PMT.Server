
using AutoMapper;
using PMT.Core.Entities;

namespace PMT.Core.Mappers;

public class OrganizationToOrgAdminMapMappingProfile : Profile
{
    public OrganizationToOrgAdminMapMappingProfile()
    {
        CreateMap<Organization, OrgAdminMap>()
            .ForMember(map => map.OrganizationId, opt => opt.MapFrom(org => org.Id))
            .ForMember(map => map.AdminId, opt => opt.MapFrom(org => org.CreatedBy))
            .ForMember(map => map.IsActive, opt => opt.MapFrom(_ => true))
            .ForMember(map => map.CreatedBy, opt => opt.MapFrom(org => org.CreatedBy))
            .ForMember(map => map.UpdatedBy, opt => opt.MapFrom(org => org.UpdatedBy))
            .ForMember(map => map.CreatedOn, opt => opt.MapFrom(org => DateTime.Now))
            .ForMember(map => map.UpdatedOn, opt => opt.MapFrom(org => DateTime.Now));
    }
}
