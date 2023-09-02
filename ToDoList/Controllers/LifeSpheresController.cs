using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LifeSpheresController : ControllerBase
    {
        private readonly ILifeSphereService lifeSphereService;

        public LifeSpheresController(ILifeSphereService lifeSphereService)
        {
            this.lifeSphereService = lifeSphereService;
        }

        [HttpGet]
        public ActionResult GetLifeSpheres()
        {
            var lifeSpheres = lifeSphereService.GetLifeSpheres();
            return lifeSpheres.Count > 0 ? Ok(lifeSpheres) : StatusCode(204);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult AddLifeSphere([FromBody] CreateLifeSphereRequest request)
        {
            if (request == null) return StatusCode(204);
            lifeSphereService.AddLifeSphere(request);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteLifeSphere(int id)
        {
            bool result = lifeSphereService.DeleteLifeSphere(id);
            if (result == false) return StatusCode(204);
            return Ok();
        }

        [HttpPut]
        public ActionResult UpdateLifeSphere(int id, [FromBody] CreateLifeSphereRequest request)
        {
            var lifeSphere = lifeSphereService.UpdateLifeSphere(id, request);
            if (lifeSphere == null) return StatusCode(400);
            return Ok(lifeSphere);
        }

        [HttpGet("{id}")]
        public ActionResult GetLifeSphereById([FromRoute] int id)
        {
            var lifeSphere = lifeSphereService.GetLifeSphereById(id);
            if (lifeSphere == null) return StatusCode(204);
            return Ok(lifeSphere);
        }
    }
}