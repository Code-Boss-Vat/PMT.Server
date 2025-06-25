
using AutoMapper;
using PMT.Core.DTOs;
using PMT.Core.Entities;

namespace PMT.Core.Mappers;

public class OrganizationUpdateRequestToOrganizationMappingProfile : Profile
{
    public OrganizationUpdateRequestToOrganizationMappingProfile()
    {
        CreateMap<OrganizationUpdateRequest, Organization>()
        .ForMember(org => org.CreatedBy, opt => opt.Ignore())
        .ForMember(org => org.CreatedOn, opt => opt.Ignore())
        .ForMember(org => org.UpdatedBy, opt => opt.MapFrom(req => req.UpdatedBy))
        .ForMember(org => org.UpdatedOn, opt => opt.MapFrom(_ => DateTime.Now));
    }
}
