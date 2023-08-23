using ToDoList.Models;

namespace ToDoList
{
    public interface IUserValidator
    {
        bool ValidateLoginModel(LoginModel login);
    }
}