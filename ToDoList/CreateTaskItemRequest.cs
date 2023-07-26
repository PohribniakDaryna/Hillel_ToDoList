using ToDoList.Models;

namespace ToDoList
{
    public class CreateTaskItemRequest
    {
        public string Title { get; set; }
        public DateTime DeadLine { get; set; }
        public string Description { get; set; }
        public int? LifeSphereId { get; set; }
    }
}