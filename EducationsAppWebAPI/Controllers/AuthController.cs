using BLL.Interface;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace EducationsAppWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _repository;
        public AuthController(IAuthService repository)
        {
            _repository = repository; // Access to appsettings.json
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginInfo loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.UserName) || loginRequest.UserId == null)
            {
                return BadRequest(new { message = "Invalid login details." });
            }

            var transactionRequest = _repository.Login(loginRequest);

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
    }
}


