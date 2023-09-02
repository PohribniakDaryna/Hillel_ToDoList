using ToDoList.Models;

namespace ToDoList.Services
{
    public interface ITaskService
    {
        void AddTask(CreateTaskItemRequest request);
        TaskItem? UpdateTask(int id, CreateTaskItemRequest request);
        bool DeleteTask(int id);
        TaskItem? GetTaskById(int id);
        List<TaskItem> GetTasks();
    }
}