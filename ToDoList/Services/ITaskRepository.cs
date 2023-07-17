using ToDoList.Models;

namespace ToDoList.Services
{
    public interface ITaskRepository
    {
        void AddTask(TaskItem taskItem);
        ITaskItem? GetTaskById(int taskId);
        List<TaskItem> GetTasks();
        TaskItem DeleteTask(int id);
        bool UpdateTask(int id, TaskItem taskItem);
    }
}