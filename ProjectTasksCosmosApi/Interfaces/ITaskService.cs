namespace ProjectTasksCosmosApi.Interfaces;

using ProjectTasksCosmosApi.Models;

public interface ITaskService
{
    public Task<IEnumerable<Task>> GetAll(int? projectId);

    public Task<Task?> Get(int id);
}
