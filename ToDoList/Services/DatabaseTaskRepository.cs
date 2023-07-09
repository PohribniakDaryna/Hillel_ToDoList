using ToDoList.Models;

namespace ToDoList.Services
{
    public class DatabaseTaskRepository : ITaskRepository
    {
        public ApplicationContext DbContext { get; }
        public DatabaseTaskRepository(ApplicationContext dbContext)
        {
            DbContext = dbContext;
        }

        public ITaskItem? GetTaskById(int taskId)
        {
            return DbContext.Tasks.FirstOrDefault(x => x.Id == taskId);
        }

        public void AddTask(ITaskItem taskItem)
        {
            DbContext.Tasks.Add((TaskItem)taskItem);
            DbContext.SaveChanges();
        }

        public TaskItem DeleteTask(int id)
        {
            TaskItem? task = DbContext.Tasks.FirstOrDefault(x => x.Id == id);
            DbContext.Tasks.Remove(task);
            DbContext.SaveChanges();
            return task;
        }

        public bool UpdateTask(int id, TaskItem taskItem)
        {
            var tasks = DbContext.Tasks.ToList();
            var task = DbContext.Tasks.FirstOrDefault(x => x.Id == id);
            if (task == null) return false;
            int index = tasks.IndexOf(task);
            tasks[index] = taskItem;
            return true;
        }

        public List<TaskItem> GetTasks()
        {
            return DbContext.Tasks.ToList();
        }
    }
}