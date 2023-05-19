namespace ToDoList
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public DateTime DeadLine { get; set; }
        public string Description { get; set; }
        public LifeSphere lifeSphere { get; set; }
    }
}