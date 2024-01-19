using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using password.hashedpassword;
using UserTasks.Models.Tasks;
using UserTasks.task.tdo;
using UserTasks.tasksServices;
using UserTasks.UserServices;

namespace UserTasks.Controllers;


[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    
    private TasksRepository repository;
    private HashedPassword hashedPassword;

    public TaskController(TasksRepository repository , HashedPassword hashedPassword)
    {
        this.repository = repository;
        this.hashedPassword = hashedPassword;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<List<UserTask>> GetTasks()
    {
       return await repository.GetTasksAsync();
    }

    [HttpPost]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Post(TaskDTO taskDTO)
    {
         var task = new UserTask
        {
            Title = taskDTO.Title,
            Description = taskDTO.Description,
            ID = taskDTO.UserID,
            CreatedOn = taskDTO.CreatedOn
        };
      
        await repository.CreateUserAsync(task);
        return CreatedAtAction("GetTasks", new { ID = task }, task);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<UserTask>> GetOneTask(int id)
    {
        UserTask task = await repository.findOne(id);
        return task == null? NotFound() : task;
    }

}