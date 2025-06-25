using AutoMapper;
using PMT.Core.DTOs;
using PMT.Core.Entities;

namespace PMT.Core.Mappers;

public class ProjectTaskUpdateRequestToProjectTaskMappingProfile : Profile
{
    public ProjectTaskUpdateRequestToProjectTaskMappingProfile()
    {
        CreateMap<ProjectTaskUpdateRequest, ProjectTask>()
        .ForMember(tsk => tsk.CreatedBy, opt => opt.Ignore())
        .ForMember(tsk => tsk.CreatedOn, opt => opt.Ignore())
        .ForMember(tsk => tsk.UpdatedBy, opt => opt.MapFrom(req => req.UpdatedBy))
        .ForMember(tsk => tsk.UpdatedOn, opt => opt.MapFrom(_ => DateTime.Now));
    }
}
