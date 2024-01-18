using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using password.hashedpassword;
using UserTasks.Models.Tasks;
using UserTasks.UserServices;

namespace UserTasks.Controllers;


[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    
    private UserRepository repository;
    private HashedPassword hashedPassword;

    public TaskController(UserRepository repository , HashedPassword hashedPassword)
    {
        this.repository = repository;
        this.hashedPassword = hashedPassword;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<List<UserTask>> GetTasks()
    {
       return await repository.GetTaskAsync();
    }

}