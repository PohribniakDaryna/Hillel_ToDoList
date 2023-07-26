using Microsoft.AspNetCore.Mvc;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LifeSpheresController : ControllerBase
    {
        private readonly ILifeSphereService lifeSphereService;

        public LifeSpheresController(ILifeSphereService lifeSphereService)
        {
            this.lifeSphereService = lifeSphereService;
        }

        [HttpGet]
        public ActionResult<bool> GetLifeSpheres()
        {
            var lifeSpheres = lifeSphereService.GetLifeSpheres();
            return lifeSpheres.Count > 0 ? (ActionResult<bool>)Ok(lifeSpheres) : (ActionResult<bool>)StatusCode(204);
        }

        [HttpPost]
        public ActionResult<bool> AddLifeSphere([FromBody] CreateLifeSphereRequest request)
        {
            if (request == null) return StatusCode(204);
            lifeSphereService.AddLifeSphere(request);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteLifeSphere(int id)
        {
            bool result = lifeSphereService.DeleteLifeSphere(id);
            if (result == false) return StatusCode(204);
            return Ok();
        }

        [HttpPut]
        public ActionResult<bool> UpdateLifeSphere(int id, [FromBody] CreateLifeSphereRequest request)
        {
            var lifeSphere = lifeSphereService.UpdateLifeSphere(id, request);
            if (lifeSphere == null) return StatusCode(204);
            return Ok(lifeSphere);
        }

        [HttpGet("{id}")]
        public ActionResult<bool> GetLifeSphereById(int id)
        {
            var lifeSphere = lifeSphereService.GetLifeSphereById(id);
            if (lifeSphere == null) return StatusCode(204);
            return Ok(lifeSphere);
        }
    }
}