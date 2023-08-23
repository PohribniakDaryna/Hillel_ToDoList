using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using System.Threading.Tasks;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemsController : ControllerBase
    {
        private readonly ITaskService taskService;
        private readonly ITaskValidator validator;
        private readonly ILogger<TaskItemsController> logger;
        public TaskItemsController(
            ITaskService taskService, 
            ITaskValidator validator, 
            ILogger<TaskItemsController> logger)
        {
            this.taskService = taskService;
            this.validator = validator;
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult<bool> GetTasks()
        {
            var tasks = taskService.GetTasks();
            return tasks.Count > 0 ? (ActionResult<bool>)Ok(tasks) : (ActionResult<bool>)StatusCode(204);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult<bool> AddTask([FromBody] CreateTaskItemRequest request)
        {
            if (request == null) return StatusCode(204);
            bool result = validator.ValidateTask(request);
            if (result)
            {
                taskService.AddTask(request);
                return Ok();
            }
            else
            {
                logger.LogInformation("Task \"{0}\" was not added", request.Title);
                return StatusCode(400);
            }

        }
        
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<bool> DeleteTask(int id)
        {
            bool result = taskService.DeleteTask(id);
            if (result == false) return StatusCode(204);
            return Ok();
        }

        [HttpPut]
        [Authorize]
        public ActionResult<bool> UpdateTask(int id, [FromBody] CreateTaskItemRequest request)
        {
            var task = taskService.UpdateTask(id, request);
            if (task == null) return StatusCode(204);
            return Ok(task);
        }

        [HttpGet("{id}")]
        public ActionResult<bool> GetTaskById([FromRoute] int id)
        {
            var task = taskService.GetTaskById(id);
            if (task == null) return StatusCode(204);
            return Ok(task);
        }
    }
}