using ToDoList.Models;

namespace ToDoList.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository register;
        public TaskService(ITaskRepository register)
        {
            this.register = register;
        }

        public ITaskItem? GetTaskById(int id)
        {
            return register.GetTaskById(id);
        }

        public void AddTask(CreateTaskItemRequest request)
        {
            TaskItem task = new()
            {
                Id = register.GetTasks().Count+1,
                Title = request.Title,
                DeadLine = request.DeadLine,
                Description = request.Description
            };
            register.AddTask(task);
        }

        public bool DeleteTask(int id)
        {
            int count = register.GetTasks().Count;
            var task = register.GetTaskById(id);
            if (task == null) return false;
            register.DeleteTask(id);
            int countAfterDeleting = register.GetTasks().Count;
            if (countAfterDeleting >= count) return false;
            return true;
        }

        public ITaskItem? UpdateTask(int id, CreateTaskItemRequest request)
        {
            var task = register.GetTaskById(id);
            if (task != null)
            {
                task.Title = request.Title;
                task.DeadLine = request.DeadLine;
                task.Description = request.Description;
            }
            register.UpdateTask(id, (TaskItem)task);
            return task;
        }

        public List<TaskItem> GetTasks()
        {
            return register.GetTasks();
        }
    }
}