using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Models
{
    public class TaskItem : ITaskItem
    {
        [Key]
        public int Id { get;}
        public string Title { get; set; }
        public DateTime DeadLine { get; set; }
        public string Description { get; set; }
        public int? LifeSphereId { get; set; }
    }
}