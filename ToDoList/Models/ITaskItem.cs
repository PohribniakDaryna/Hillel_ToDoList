namespace ToDoList.Models
{
    public interface ITaskItem
    {
        int Id { get; set; }
        string Title { get; set; }
        DateTime DeadLine { get; set; }
        string Description { get; set; }
       // LifeSphere LifeSphere { get; set; }
    }
}