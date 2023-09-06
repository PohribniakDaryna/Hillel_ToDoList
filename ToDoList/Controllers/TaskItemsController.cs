using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]/[action]")]
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
        public ActionResult GetTasks()
        {
            var tasks = taskService.GetTasks();
            return tasks.Count > 0 ? Ok(tasks) : StatusCode(204);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult AddTask([FromBody] CreateTaskItemRequest request)
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
                logger.LogInformation($"Task \"{request.Title}\" was not added");
                return StatusCode(400);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTask(int id)
        {
            bool result = taskService.DeleteTask(id);
            if (result == false) return StatusCode(204);
            return Ok();
        }

        [HttpPut]
        public ActionResult UpdateTask(int id, [FromBody] CreateTaskItemRequest request)
        {
            var task = taskService.UpdateTask(id, request);
            if (task == null) return StatusCode(400);
            return Ok(task);
        }

        [HttpGet("{id}")]
        public ActionResult GetTaskById([FromRoute] int id)
        {
            var task = taskService.GetTaskById(id);
            if (task == null) return StatusCode(204);
            return Ok(task);
        }
    }
}