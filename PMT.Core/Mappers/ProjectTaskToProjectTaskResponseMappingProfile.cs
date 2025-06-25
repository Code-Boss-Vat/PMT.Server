
using AutoMapper;
using PMT.Core.DTOs;
using PMT.Core.Entities;

namespace PMT.Core.Mappers;

public class ProjectTaskToProjectTaskResponseMappingProfile : Profile
{
    public ProjectTaskToProjectTaskResponseMappingProfile()
    {
        CreateMap<ProjectTask, ProjectTaskResponse>();
    }
}
