namespace ProjectTasksApi.Models.Mapper;

using AutoMapper;
using ProjectTasksApi.Models.Dto;

public class EntityToDtoMappingProfile : Profile
{
	public EntityToDtoMappingProfile()
	{
		CreateMap<Project, ProjectOutputDto>();
		CreateMap<Task, TaskOutputDto>();
	}
}
