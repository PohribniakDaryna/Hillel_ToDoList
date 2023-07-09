using ToDoList.Models;

namespace ToDoList.Services
{
    public interface ITaskService
    {
        void AddTask(CreateTaskItemRequest request);
        ITaskItem? UpdateTask(int id, CreateTaskItemRequest request);
        bool DeleteTask(int id);
        ITaskItem? GetTaskById(int id);
        List<TaskItem> GetTasks();
    }
}