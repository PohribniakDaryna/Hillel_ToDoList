using ToDoList.Models;

namespace ToDoList
{
    public interface ITaskValidator
    {
        bool ValidateTask(CreateTaskItemRequest task);
    }
}