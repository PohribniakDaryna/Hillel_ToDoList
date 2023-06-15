using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LifeSpheresController : ControllerBase
    {
        private readonly ILifeSphereRegister lifeSphereRegister;

        public LifeSpheresController(ILifeSphereRegister lifeSphereRegister)
        {
            this.lifeSphereRegister = lifeSphereRegister;
        }

        [HttpGet]
        public ActionResult<bool> GetLifeSpheres()
        {
            var lifeSpheres = lifeSphereRegister.GetLifeSpheres();
            if (lifeSpheres.Count > 0)
                return Ok(lifeSpheres);
            else
                return StatusCode(204);
        }

        [HttpPost]
        public ActionResult<bool> AddLifeSphere([FromBody] CreateLifeSphereRequest request)
        {
            LifeSphere lifeSphere = new()
            {
                Id = lifeSphereRegister.GetLifeSpheres().Count,
                Title = request.Title,
                Grade = request.Grade,
                Description = request.Description
            };
            lifeSphereRegister.AddLifeSphere(lifeSphere);
            return Ok(lifeSphere);
        }


        [HttpPut]
        public ActionResult<bool> UpdateLifeSphere(int id, [FromBody] CreateLifeSphereRequest request)
        {
            var lifeSphere = lifeSphereRegister.UpdateLifeSphere(id, request);
            if (lifeSphere != null)
                return Ok();
            else
                return StatusCode(204);
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteLifeSphere(int id, [FromQuery] string title)
        {
            bool result = lifeSphereRegister.DeleteLifeSphere(id, title);
            if (result)
                return StatusCode(204);
            else
                return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<bool> GetLifeSphereById(int id)
        {
            var lifeSphere = lifeSphereRegister.GetLifeSphereById(id);
            if (lifeSphere == null) return StatusCode(204);
            return Ok(lifeSphere);
        }
    }
}