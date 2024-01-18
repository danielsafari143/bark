using Microsoft.AspNetCore.Mvc;
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
}
