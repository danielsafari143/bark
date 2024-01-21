using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using password.hashedpassword;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using UserTasks.UserServices;

namespace JwtInDotnetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private HashedPassword password;
        private readonly UserRepository userRepository;

        public LoginController(IConfiguration config, HashedPassword password, UserRepository userRepository)
        {
            _config = config;
            this.password = password;
            this.userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginRequest loginRequest)
        {
            var user = await userRepository.GetUserAsync(loginRequest.Email);
            bool check = password.Decode(user.Password, loginRequest.Password);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Issuer"],
            null,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            return check ? Ok(token) : BadRequest("Wrong email or poassword");

        }
    }
}