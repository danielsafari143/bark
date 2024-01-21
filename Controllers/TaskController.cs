using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using password.hashedpassword;
using UserTasks.Models.Tasks;
using UserTasks.task.tdo;
using UserTasks.tasksServices;
using UserTasks.UserServices;

namespace UserTasks.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{

    private TasksRepository _repository;
    private HashedPassword _hashedPassword;
    private UserRepository _userRepository1;

    public TaskController(TasksRepository repository, HashedPassword hashedPassword, UserRepository userRepository)
    {
        _repository = repository;
        _hashedPassword = hashedPassword;
        _userRepository1 = userRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<List<UserTask>> GetTasks()
    {
        return await _repository.GetTasksAsync();
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
            UsersId = taskDTO.UserID,
            EnDate = taskDTO.EndDate
        };
        try
        {
            await _repository.CreateUserAsync(task);
        }
        catch
        {

            return NotFound(new { Message = "There is no user with this id", Id = taskDTO.UserID });
        }
        return CreatedAtAction("GetTasks", new { ID = task }, task);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<UserTask>> GetOneTask(int id)
    {
        try
        {
            UserTask task = await _repository.findOne(id);
            return task;
        }
        catch
        {
            return NotFound(new { Message = "There is no task with this id", Id = id });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        try
        {
            UserTask user = await _repository.delete(id);
            return user == null ? NotFound() : NoContent();
        }
        catch
        {
            return NotFound(new { Message = "There is no task with this id", Id = id });
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<UserTask>> updated(int id, TaskDTO taskDTO)
    {
        try
        {
            UserTask task = await _repository.update(id, taskDTO);
            return task;
        }
        catch
        {
            return NotFound(new { Message = "There is no task with this id", Id = id });
        }
    }

}