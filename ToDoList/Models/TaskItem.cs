namespace ToDoList.Models
{
    public class TaskItem : Entity<TaskItem>
    {
        public DateTime DeadLine { get; set; }
        public string Description { get; set; }
        public int? LifeSphereId { get; set; }
    }
}