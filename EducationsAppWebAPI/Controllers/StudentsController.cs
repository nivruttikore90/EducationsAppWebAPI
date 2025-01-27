using BLL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace EducationsAppWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        private readonly IStudent _repository;
        public StudentsController(IStudent repository)
        {
            _repository = repository; // Access to appsettings.json
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<Student>> GetStudents()
        //{
        //    Student student=new Student();
        //    return _repository.GetStudentInfo(student);
        //    return Ok(students);
        //}

        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            Student student = new Student();
            // Fetch students from the repository
            var students = _repository.GetStudentInfo(student);

            // Check if the students list is null or empty
            if (students == null)
            {
                return NotFound("No students found.");
            }
            return Ok(students);
        }

        [HttpPost("SaveInfo")]
        public IActionResult SaveInfo([FromBody] Student request)
        {
            var transactionRequest = _repository.SaveInfo(request);

            if (transactionRequest.RxTransactionBody == null)
            {
                return BadRequest(new
                {
                    message = "Invalid credentials.",
                    response = transactionRequest
                });
            }
            return Ok(transactionRequest);
        }

        [HttpPut("updateStudent/{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] Student request)
        {
             if (id == null)
            {
                return NotFound(new { message = "Student not found." });
            }

            var transactionRequest = _repository.UpdateStudentInfo(id, request);

            if (transactionRequest.RxTransactionBody == null)
            {
                return BadRequest(new
                {
                    message = "Invalid credentials or failed to update student.",
                    response = transactionRequest
                });
            }
            return Ok(new
            {
                message = "Student updated successfully.",
                response = transactionRequest
            });
        }


    }
}
