using ToDoList.Models;

namespace ToDoList.Services
{
    public class TaskRegister : ITaskRegister
    {
        private readonly List<TaskItem> tasks = new();
        public void AddTask(TaskItem taskItem)
        {
            tasks.Add(taskItem);
        }

        public bool DeleteTask(int id, string title)
        {
            int count = tasks.Count;
            var task = tasks.FirstOrDefault(x => x.Id == id && x.Title == title);
            if (task != null)
            {
                tasks.Remove(task);
                int countAfterDeleting = tasks.Count;
                if (countAfterDeleting < count)
                    return true;
            }
            return false;
        }

        public ITaskItem? GetTaskById(int id)
        {
            return tasks.FirstOrDefault(x => x.Id == id);
        }

        public List<TaskItem> GetTasks()
        {
            return tasks;
        }

        public ITaskItem? UpdateTask(int id, CreateTaskItemRequest request)
        {
            var task = tasks.FirstOrDefault(x => x.Id == id);
            if (task != null)
            {
                task.Title = request.Title;
                task.DeadLine = request.DeadLine;
                task.Description = request.Description;
            }
            return task;
        }
    }
}