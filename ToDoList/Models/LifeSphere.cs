namespace ToDoList.Models
{
    public class LifeSphere : ILifeSphere
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Grade { get; set; }
    }
}