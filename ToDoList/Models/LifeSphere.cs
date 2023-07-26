using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class LifeSphere : ILifeSphere
    {
        [Key]
        public int Id { get; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Grade { get; set; }
        public List<TaskItem> Tasks { get; set; }
    }
}