
using AutoMapper;
using PMT.Core.DTOs;
using PMT.Core.Entities;

namespace PMT.Core.Mappers;

public class ApplicationUserToUserResponseMappingProfile : Profile
{
    public ApplicationUserToUserResponseMappingProfile()
    {
        CreateMap<ApplicationUser, UserResponse>();
    }
}
