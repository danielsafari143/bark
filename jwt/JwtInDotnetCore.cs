using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using password.hashedpassword;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using UserTasks.tasksServices;

namespace JwtInDotnetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private HashedPassword password;
        private readonly TasksRepository taskRepository;

        public LoginController(IConfiguration config , HashedPassword password, TasksRepository tasksRepository) 
        {
            _config = config;
            this.password = password;
            this.taskRepository = tasksRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginRequest loginRequest)
        {
                var user = await taskRepository.GetUserAsync(loginRequest.Email);
                bool check = password.Decode(user.Password, loginRequest.Password);

                if(check) {
                    return null;
                }
                 
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

                var token =  new JwtSecurityTokenHandler().WriteToken(Sectoken);

                return Ok(token);

        }
    }
}