
using AutoMapper;
using PMT.Core.DTOs;
using PMT.Core.Entities;

namespace PMT.Core.Mappers;

public class OrganizationToOrganizationResponseMappingProfile : Profile
{
    public OrganizationToOrganizationResponseMappingProfile()
    {
        CreateMap<Organization, OrganizationResponse>();
    }
}
