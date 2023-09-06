namespace ToDoList
{
    public interface ITaskValidator
    {
       bool ValidateTask(CreateTaskItemRequest task);
    }
}