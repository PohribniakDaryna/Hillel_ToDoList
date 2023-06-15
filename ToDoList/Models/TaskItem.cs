namespace ToDoList.Models
{
    public class TaskItem : ITaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DeadLine { get; set; }
        public string Description { get; set; }
        public LifeSphere LifeSphere { get; set; }
    }
}