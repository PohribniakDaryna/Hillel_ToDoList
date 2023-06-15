using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemsController : ControllerBase
    {
        private readonly ITaskRegister taskRegister;
        public TaskItemsController(ITaskRegister taskregister)
        {
            this.taskRegister = taskregister;
        }

        [HttpGet]
        public ActionResult<bool> GetTasks()
        {
            var tasks = taskRegister.GetTasks();
            if (tasks.Count>0)
                return Ok(tasks);
            else
                return StatusCode(204);
        }

        [HttpPost]
        public ActionResult<bool> AddTask([FromBody] CreateTaskItemRequest request)
        {
            TaskItem task = new()
            {
                Id = taskRegister.GetTasks().Count,
                Title = request.Title,
                DeadLine = request.DeadLine,
                Description = request.Description
            };
            taskRegister.AddTask(task);
            return Ok(task);
        }

        [HttpPut]
        public ActionResult<bool> UpdateTask(int id, [FromBody] CreateTaskItemRequest request)
        {
            var task = taskRegister.UpdateTask(id, request);
            if (task!=null)
                return Ok(task);
            else
                return StatusCode(204);
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteTask(int id, [FromQuery] string title)
        {
            bool result = taskRegister.DeleteTask(id, title);
            if (result)
                return Ok();
            else
                return StatusCode(204);
        }

        [HttpGet("{id}")]
        public ActionResult<bool> GetTaskById(int id)
        {
            var task = taskRegister.GetTaskById(id);
            if (task == null) return StatusCode(204);
            return Ok(task);
        }
    }
}