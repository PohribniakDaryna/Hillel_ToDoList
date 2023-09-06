namespace ToDoList.Models
{
    public class LifeSphere : Entity<LifeSphere>
    { 
        public string Description { get; set; }
        public int Grade { get; set; }
        public List<TaskItem>? Tasks { get; set; }
    }
}