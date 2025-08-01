using jsonWebTokenDotnet9.Models;
using jsonWebTokenDotnet9.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace jsonWebTokenDotnet9.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
       
        public static List<User> users = new List<User>();

        [HttpPost("register")]
        public ActionResult<string> Register(userDtos request)
        {
            User user = new User();

            var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.password);

            user.name = request.name;
            user.passwordHash = hashedPassword;
            users.Add(user);
            

            return Ok("user registered");

        }

        [HttpGet("teste")]
        public ActionResult Teste()
        {
            return Ok(new { teste = "ratinho" });
        }

        [HttpPost("login")]
        public ActionResult<string> login(userDtos request)
        {
            foreach(var user in users)
            {
                if(request.name == user.name)
                {
                    if (new PasswordHasher<User>().VerifyHashedPassword(user, user.passwordHash, request.password) == PasswordVerificationResult.Success) // agora pegar a senha por hash
                    {
                        return Ok("Login successful");
                    }
                }
                
            }
            return BadRequest(new {erros = "perfil não necontrado"});

        }

        [HttpGet("Get")]
        public ActionResult<List<User>> GetUser()
        {
            return Ok(users);
        }
    }
}
