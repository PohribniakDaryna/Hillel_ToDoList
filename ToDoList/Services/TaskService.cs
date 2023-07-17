using Azure.Core;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository taskRegister;
        private readonly ILifeSphereRepository lifeSphereRegister;
        public TaskService(ITaskRepository taskRegister, ILifeSphereRepository lifeSphereRegister)
        {
            this.taskRegister = taskRegister;
            this.lifeSphereRegister = lifeSphereRegister;
        }

        public ITaskItem? GetTaskById(int id)
        {
            return taskRegister.GetTaskById(id);
        }

        public void AddTask(CreateTaskItemRequest request)
        {
            TaskItem task = new();
            InitializeTask(task, request);
            taskRegister.AddTask(task);
        }

        public bool DeleteTask(int id)
        {
            int count = taskRegister.GetTasks().Count;
            var task = taskRegister.GetTaskById(id);
            if (task == null) return false;
            taskRegister.DeleteTask(id);
            int countAfterDeleting = taskRegister.GetTasks().Count;
            if (countAfterDeleting >= count) return false;
            return true;
        }

        public ITaskItem? UpdateTask(int id, CreateTaskItemRequest request)
        {
            var task = taskRegister.GetTaskById(id);
            if (task != null)
            {
                InitializeTask(task, request);
                taskRegister.UpdateTask(id, (TaskItem)task);
            }
            return task;
        }

        public List<TaskItem> GetTasks()
        {
            return taskRegister.GetTasks();
        }

        private List<int> GetLifeSpheresId()
        {
            List<int> sphereIdList = new();
            var spheres = lifeSphereRegister.GetLifeSpheres();
            foreach (var item in spheres)
            {
                sphereIdList.Add(item.Id);
            }
            return sphereIdList;
        }

        private bool ExistsLifeSpheresId(int? id)
        {
            var list = GetLifeSpheresId();
            if (id != null)
                return list.Contains((int)id);
            else
                return false;
        }

        private TaskItem InitializeTask(ITaskItem task, CreateTaskItemRequest request)
        {
            task.Title = request.Title;
            task.DeadLine = request.DeadLine;
            task.Description = request.Description;
            bool result = ExistsLifeSpheresId(request.LifeSphereId);

            if (request.LifeSphereId != null && result)
                task.LifeSphereId = request.LifeSphereId;
            else
                task.LifeSphereId = null;
            return (TaskItem)task;
        }
    }
}