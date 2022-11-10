namespace ProjectTasksApi.Servcies;

using ProjectTasksApi.Models;

public static class TaskService
{
    static List<Task> TasksStorage = new List<Task>();
    static int NextId = 1;

    public static IEnumerable<Task> GetAll() => TasksStorage;

    public static Task? Get(int id) => TasksStorage.FirstOrDefault(task => task.Id == id);

    public static void Add(Task task)
    {
        task.Id = NextId++;
        TasksStorage.Add(task);

        ProjectService.AddTask(task.ProjectId, task);
    }

    public static bool Update(int id, Task task)
    {
        int existingTaskIndex = TasksStorage.FindIndex(task => task.Id == id);

        if (existingTaskIndex == -1)
        {
            return false;
        }

        TasksStorage[existingTaskIndex] = task;

        ProjectService.UpdateTask(task.ProjectId, task);

        return true;
    }

    public static bool Delete(int id)
    {
        Task? existingTask = Get(id);

        if (existingTask == null)
        {
            return false;
        }

        TasksStorage.Remove(existingTask);

        ProjectService.RemoveTask(existingTask.ProjectId, existingTask);

        return true;
    }
}
