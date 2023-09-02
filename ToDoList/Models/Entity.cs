using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public abstract class Entity<T> where T : class
    {
        [Key]
        public int Id { get; }
        public string Title { get; set; }
    }
}