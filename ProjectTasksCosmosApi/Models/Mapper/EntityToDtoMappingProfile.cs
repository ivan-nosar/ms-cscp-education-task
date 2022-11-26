namespace ProjectTasksCosmosApi.Models.Mapper;

using AutoMapper;
using ProjectTasksCosmosApi.Models.Dto;

public class EntityToDtoMappingProfile : Profile
{
    public EntityToDtoMappingProfile()
    {
        CreateMap<Project, ProjectOutputDto>();
        CreateMap<Task, TaskOutputDto>();
    }
}
