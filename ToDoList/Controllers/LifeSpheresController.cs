using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Controllers
{
    public class LifeSpheresController : Controller
    {
        public static List<LifeSphere> Spheres { get; set; } = new List<LifeSphere>();

        [HttpGet]
        public ActionResult<bool> GetTasks()
        {
            return Ok(Spheres);
        }

        [HttpPost]
        public ActionResult<bool> AddLifeSphere([FromBody] CreateLifeSphereRequest request)
        {
            LifeSphere lifeSphere = new ()
            {
                Id = Spheres.Count,
                Title = request.Title,
                Grade = request.Grade,
                Description = request.Description
            };
            Spheres.Add(lifeSphere);
            return Ok(lifeSphere);
        }


        [HttpPut]
        public ActionResult<bool> UpdateLifeSphere(int id, [FromBody] CreateLifeSphereRequest request)
        {
            var lifeSphere = Spheres.FirstOrDefault(x => x.Id == id);
            if (lifeSphere != null)
            {
                lifeSphere.Title = request.Title;
                lifeSphere.Grade = request.Grade;
                lifeSphere.Description = request.Description;
                return Ok(lifeSphere);
            }
            return StatusCode(204, new { ErrorMessage = "this item doen't exist or wrong id."});
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteLifeSphere(int id, [FromQuery] string title)
        {
            var lifeSphere = Spheres.FirstOrDefault(x => x.Id == id && x.Title == title);
            if (lifeSphere == null) 
                return StatusCode(204, new { ErrorMessage = "this item doen't exist or wrong id." });
            Spheres.Remove(lifeSphere);
            return Ok(lifeSphere);
        }

        [HttpGet("{id}")]
        public ActionResult<bool> GetLifeSphereById(int id)
        {
            var lifesphere = Spheres.FirstOrDefault(x => x.Id == id);
            if (lifesphere != null) return StatusCode(204, new { ErrorMessage = "this item doen't exist or wrong id." });
            return Ok(lifesphere);
        }
    }
}