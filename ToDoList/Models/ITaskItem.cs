namespace ToDoList.Models
{
    public interface ITaskItem
    {
        int Id { get;}
        string Title { get; set; }
        DateTime DeadLine { get; set; }
        string Description { get; set; }
        int? LifeSphereId { get; set; }
    }
}