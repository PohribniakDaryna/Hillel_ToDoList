using Microsoft.AspNetCore.Mvc.Filters;

namespace ToDoList
{
    public class LogFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"Request finished.");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"Request began at {DateTime.Now}.");
        }
    }
}