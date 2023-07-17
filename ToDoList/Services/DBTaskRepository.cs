using ToDoList.Models;

namespace ToDoList.Services
{
    public class DBTaskRepository : ITaskRepository
    {
        public ApplicationContext DbContext { get; }
        public DBTaskRepository(ApplicationContext dbContext)
        {
            DbContext = dbContext;
        }

        public ITaskItem? GetTaskById(int taskId)
        {
            return DbContext.Tasks.FirstOrDefault(x => x.Id == taskId);
        }

        public void AddTask(TaskItem taskItem)
        {
            DbContext.Tasks.Add(taskItem);
            DbContext.SaveChanges();
        }

        public TaskItem DeleteTask(int id)
        {
            TaskItem? task = DbContext.Tasks.FirstOrDefault(x => x.Id == id);
            DbContext.Tasks.Remove(task);
            DbContext.SaveChanges();
            return task;
        }

        public bool UpdateTask(int id, TaskItem newTaskItem)
        {
            var taskItem = DbContext.Tasks.FirstOrDefault(x => x.Id == id);
            if (taskItem == null) return false;
            taskItem = newTaskItem;
            DbContext.SaveChanges();
            return true;
        }

        public List<TaskItem> GetTasks()
        {
            return DbContext.Tasks.ToList();
        }
    }
}