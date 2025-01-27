using BLL.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EducationsAppWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstituteController : ControllerBase
    {
        private readonly IInstitute _repository;
        public InstituteController(IInstitute repository)
        {
            _repository = repository; // Access to appsettings.json
        }
        [HttpGet]
        public IActionResult GetAllInstitutes()
        {
            var institutes = _repository.GetAllInstitutes();
            return Ok(institutes);
        }

        [HttpGet("{id}")]
        public IActionResult GetInstituteById(int id)
        {
            var institute = _repository.GetInstituteById(id);
            if (institute == null) return NotFound();
            return Ok(institute);
        }

        [HttpPost]
        public IActionResult AddInstitute(Model.Institute institute)
        {
            _repository.AddInstitute(institute);
            return CreatedAtAction(nameof(GetInstituteById), new { id = institute.Id }, institute);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateInstitute(int id, Model.Institute institute)
        {
            if (id != institute.Id) return BadRequest();
            _repository.UpdateInstitute(institute);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteInstitute(int id)
        {
            _repository.DeleteInstitute(id);
            return NoContent();
        }
    }
}
