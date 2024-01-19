using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using password.hashedpassword;
using UserTasks.Models.User;
using UserTasks.user.tdo;
using UserTasks.UserServices;

namespace UserTasks.Controllers;



[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private UserRepository repository;
    private HashedPassword hashedPassword;

    public UserController(UserRepository repository , HashedPassword hashedPassword)
    {
        this.repository = repository;
        this.hashedPassword = hashedPassword;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<List<User>> GetUser()
    {
        return await repository.GetUsersAsync();
    }

    [HttpPost]
    [AllowAnonymous]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Post(UserDTO userTDO)
    {
         var user = new User
        {
            username = userTDO.Username,
            Password = hashedPassword.hashedpassword(userTDO.Password),
            email = userTDO.Email
        };
        
        await repository.CreateUserAsync(user);
        return CreatedAtAction("GetUser", new { id = user.ID }, user);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<User>> GetOneUser(int id)
    {
        User user = await repository.findOne(id);
        return user == null? NotFound() : user;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        User user = await repository.delete(id);
        return user == null? NotFound():NoContent();
    }

    [HttpPut("{id}")]
    public async Task <User> update(User user) {
        User prUser = await repository.update(user);
        return prUser;
    }
}
