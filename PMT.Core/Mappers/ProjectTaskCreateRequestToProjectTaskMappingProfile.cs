
using AutoMapper;
using PMT.Core.DTOs;
using PMT.Core.Entities;

namespace PMT.Core.Mappers;

public class ProjectTaskCreateRequestToProjectTaskMappingProfile : Profile
{
    public ProjectTaskCreateRequestToProjectTaskMappingProfile()
    {
        CreateMap<ProjectTaskCreateRequest, ProjectTask>()
            .ForMember(tsk => tsk.IsActive, opt => opt.MapFrom(_ => true))
            .ForMember(req => req.CreatedBy, opt => opt.MapFrom(req => req.CreatedBy))
            .ForMember(req => req.UpdatedBy, opt => opt.MapFrom(req => req.CreatedBy))
            .ForMember(req => req.CreatedOn, opt => opt.MapFrom(_ => DateTime.Now))
            .ForMember(req => req.UpdatedOn, opt => opt.MapFrom(_ => DateTime.Now));
    }
}
