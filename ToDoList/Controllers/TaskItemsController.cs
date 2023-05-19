using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemsController : ControllerBase
    {
        public static List<TaskItem> Tasks { get; set; } = new List<TaskItem>();

        [HttpGet]
        public ActionResult<bool> GetTasks()
        {
            return Ok(Tasks);
        }

        [HttpPost]
        public ActionResult<bool> AddTask([FromBody] CreateTaskItemRequest request)
        {
            TaskItem task = new ()
            {
                Id = Tasks.Count,
                Title = request.Title,
                DeadLine = request.DeadLine,
                Description = request.Description
            };
            Tasks.Add(task);
            return Ok(task);
        }


        [HttpPut]
        public ActionResult<bool> UpdateTask(int id, [FromBody] CreateTaskItemRequest request)
        {
            var task = Tasks.FirstOrDefault(x => x.Id == id);
            if (task != null)
            {
                task.Title = request.Title;
                task.DeadLine = request.DeadLine;
                task.Description = request.Description;
                return Ok(task);
            }
            return StatusCode(204, new { ErrorMessage = "this item doen't exist or wrong id." });
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteTask(int id, [FromQuery] string title)
        {
            var task = Tasks.FirstOrDefault(x => x.Id == id && x.Title == title);
            if (task == null)
                return StatusCode(204, new { ErrorMessage = "this item doen't exist or wrong id." });
            Tasks.Remove(task);
            return Ok(task);
        }

        [HttpGet("{id}")]
        public ActionResult<bool> GetTaskById(int id)
        {
            var task = Tasks.FirstOrDefault(x => x.Id == id);
            if (task != null) return StatusCode(204, new { ErrorMessage = "this item doen't exist or wrong id." });
            return Ok(task);
        }
    }
}