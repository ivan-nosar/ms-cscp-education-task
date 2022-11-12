namespace ProjectTasksApi.Interfaces;

using ProjectTasksApi.Models;
using ProjectTasksApi.Models.Dto;

public interface ITaskService
{
    public Task<IEnumerable<Task>> GetAll();

    public Task<Task?> Get(int id);

    public Task<Task?> Add(TaskInputDto taskDto);

    public Task<Task?> Update(int id, TaskInputDto taskDto);

    public Task<bool> Delete(int id);
}
