using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using password.hashedpassword;
using user.tdo;
using UserTasks.Models.User;
using UserTasks.tasksServices;

namespace UserTasks.Controllers;


[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UserTasks : ControllerBase
{
    private TasksRepository repository;
    private HashedPassword hashedPassword;

    public UserTasks(TasksRepository repository , HashedPassword hashedPassword)
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
        Console.WriteLine(hashedPassword.Decode(user.Password, userTDO.Password));
        await repository.CreateUserAsync(user);
        return CreatedAtAction("GetUser", new { id = user.ID }, user);
    }
}
