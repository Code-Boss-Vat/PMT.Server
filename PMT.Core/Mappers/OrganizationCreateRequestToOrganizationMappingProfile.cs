using AutoMapper;
using PMT.Core.DTOs;
using PMT.Core.Entities;

namespace PMT.Core.Mappers;

public class OrganizationCreateRequestToOrganizationMappingProfile : Profile
{
    public OrganizationCreateRequestToOrganizationMappingProfile()
    {
        CreateMap<OrganizationCreateRequest, Organization>()
            .ForMember(org => org.IsActive, opt => opt.MapFrom(_ => true))
            .ForMember(org => org.CreatedBy, opt => opt.MapFrom(req => req.CreatedBy))
            .ForMember(org => org.UpdatedBy, opt => opt.MapFrom(req => req.CreatedBy))
            .ForMember(org => org.CreatedOn, opt => opt.MapFrom(_ => DateTime.Now))
            .ForMember(org => org.UpdatedOn, opt => opt.MapFrom(_ => DateTime.Now));
    }
}
