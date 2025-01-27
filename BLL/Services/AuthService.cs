using BLL.Interface;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Model.Core;
using Model;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtAuthToken(string? username, int? userid)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            Claim[] claims =
            {
                new Claim("username", username), // Custom claim
                new Claim("userid", Convert.ToString(userid)) // Custom claim
              //  new Claim(ClaimTypes.Role, "User") // Example role
            };

            string issuer = jwtSettings["Issuer"];
            string audience = jwtSettings["Audience"];
            string secretKey = jwtSettings["SecretKey"];
         

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));//Get secret key string and Converts into byte[]
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //Creates credentials for signing the token.

            //Creating the JWT
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public TransactionRequest<LoginInfo> Login(LoginInfo loginInfo)
        {
            if (loginInfo == null)
            {
                var errorStatus = new Status
                {
                    StatusTypeCode = "LOGIN_ERROR",
                    StatusCode = "INVALID_CREDENTIALS",
                    StatusDescription = "Invalid username or password.",
                    SequenceNumber = 1
                };

                var rxHeader = new RxHeader
                {
                    RxSession = Guid.NewGuid().ToString(),
                    Source = "System",
                    Destination = "API",
                    TransactionIdentifier = Guid.NewGuid().ToString(),
                    TransactionExternalIdentifier = null,
                    TransactionTimestamp = DateTime.UtcNow,
                    TransactionStatus = "Failed",
                    Environment = "Production",
                    Statuses = new List<Status> { errorStatus }
                };

                var transactionRequest = new TransactionRequest<LoginInfo>
                {
                    RxHeader = rxHeader,
                    RxTransactionBody = null
                };

                return transactionRequest;
            }

            // Step 2: If user is valid, generate JWT token
            loginInfo.Token = GenerateJwtAuthToken(loginInfo.UserName, loginInfo.UserId);

            // Step 3: Create success status
            var successStatus = new Status
            {
                StatusTypeCode = "LOGIN_STATUS",
                StatusCode = "SUCCESS",
                StatusDescription = "User logged in successfully.",
                SequenceNumber = 1
            };

            // Step 4: Prepare RxHeader
            var rxHeaderSuccess = new RxHeader
            {
                RxSession = Guid.NewGuid().ToString(),
                Source = "System",
                Destination = "API",
                TransactionIdentifier = Guid.NewGuid().ToString(),
                TransactionExternalIdentifier = null,
                TransactionTimestamp = DateTime.UtcNow,
                TransactionStatus = "Completed",
                Environment = "Production",
                Statuses = new List<Status> { successStatus }
            };

            // Step 5: Return the response wrapped in TransactionRequest
            var transactionRequestSuccess = new TransactionRequest<LoginInfo>
            {
                RxHeader = rxHeaderSuccess,
                RxTransactionBody = loginInfo
            };

            return transactionRequestSuccess;
        }

    }
}
