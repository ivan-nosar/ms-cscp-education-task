namespace ProjectTasksApi.Interfaces;

using ProjectTasksApi.Models;
using ProjectTasksApi.Models.Dto;

public interface IProjectService
{
    public Task<IEnumerable<Project>> GetAll(bool shouldPopulateTasks);

    public Task<Project?> Get(int id, bool shouldPopulateTasks);

    public Task<Project?> Add(ProjectInputDto projectDto);

    public Task<Project?> Update(int id, ProjectInputDto projectDto);

    public Task<bool> Delete(int id);
}
