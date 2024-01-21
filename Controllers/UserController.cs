using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using password.hashedpassword;
using UserTasks.Models.User;
using UserTasks.user.tdo;
using UserTasks.UserServices;

namespace UserTasks.Controllers;


[Authorize]
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
    public async Task<List<Users>> GetUser()
    {
        return await repository.GetUsersAsync();
    }

    [HttpPost]
    [AllowAnonymous]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Post(UserDTO userTDO)
    {
         var user = new Users
        {
            username = userTDO.Username,
            Password = hashedPassword.hashedpassword(userTDO.Password),
            email = userTDO.Email,
            Tasks = (ICollection<Models.Tasks.UserTask>)userTDO.Tasks 
        };
        
        try
        {
            await repository.CreateUserAsync(user);
        }
        catch  {
            return Conflict(new {Message = "Email already exists"});
        }
        return CreatedAtAction("GetUser", new { id = user.ID }, user);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Users>> GetOneUser(int id)
    {
        try
        {
             Users user = await repository.findOne(id);
             return user;
        }
        catch  {
            
           return NotFound(new {Message="There is no user with this id" , Id=id});
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
             Users user = await repository.delete(id);
             return Accepted(new {Message = "User deleted succefully" });
        }
        catch {
            
            return NotFound(new {Message= "There is no user with this id" , id});
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Users>> update(int id , Users users) {
        try
        {
             Users prUser = await repository.update(id , users);
             return CreatedAtAction("GetOneUser", new { id = prUser.ID }, prUser);
        }
        catch{
            
            return NotFound(new {Message="There is no user with this id" , Id=id});
        }
    }
}
