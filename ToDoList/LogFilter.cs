using Microsoft.AspNetCore.Mvc.Filters;

namespace ToDoList
{
    public class LogFilter : Attribute, IActionFilter
    {
        public LogFilter(ILogger<LogFilter> logger)
        {
            Logger = logger;
        }
        public ILogger<LogFilter> Logger { get; }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Logger.LogInformation($"Request finished.");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Logger.LogInformation($"Request began at {DateTime.Now}.");
        }
    }
}