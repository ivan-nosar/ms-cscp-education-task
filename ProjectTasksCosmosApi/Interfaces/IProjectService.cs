namespace ProjectTasksCosmosApi.Interfaces;

using ProjectTasksCosmosApi.Models;

public interface IProjectService
{
    public Task<IEnumerable<Project>> GetAll(bool shouldPopulateTasks);

    public Task<Project?> Get(int id, bool shouldPopulateTasks);
}
