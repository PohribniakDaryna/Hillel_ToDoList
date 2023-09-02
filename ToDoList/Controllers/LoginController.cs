using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly string secret;
        private readonly string myIssuer;
        private readonly string myAudience;
        private readonly IUserValidator validator;
        private readonly ILogger<LoginController> logger;
        public LoginController(
            IConfiguration configuration, 
            IUserValidator validator, 
            ILogger<LoginController> logger)
        {
            this.validator = validator;
            this.logger = logger;
            secret = configuration.GetValue<string>("Auth:Secret")!;
            myIssuer = configuration.GetValue<string>("Auth:myIssuer")!;
            myAudience = configuration.GetValue<string>("Auth:myAudience")!;
        }

        [HttpPost]
        public ActionResult<string> GenerateToken([FromBody] LoginModel request)
        {
            var result = validator.ValidateLoginModel(request);
            if(result)
            {
                var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.NameIdentifier, request.Login),
                    new Claim(ClaimTypes.Role, "User")
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Issuer = myIssuer,
                    Audience = myAudience,
                    SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            else
            {
                logger.LogInformation("Registration with login \"{request.Login}\" failed", request.Login);
                return StatusCode(400);
            }
        }

        [HttpGet]
        public bool VerifyToken(string token)
        {
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = myIssuer,
                    ValidAudience = myAudience,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}