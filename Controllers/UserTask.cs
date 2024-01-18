using Microsoft.AspNetCore.Mvc;
using user.tdo;
using UserTasks.Models.User;
using UserTasks.tasksServices;

namespace UserTasks.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UserTasks : ControllerBase
{
    private TasksRepository repository;

    public UserTasks(TasksRepository repository)
    {
        this.repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<List<User>> Get()
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
            Password = userTDO.Password,
            email = userTDO.Email
        };

        await repository.CreateUserAsync(user);
        return CreatedAtAction("GetUser", new { id = user.ID }, user);
    }
}
