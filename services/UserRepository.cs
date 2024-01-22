using Microsoft.EntityFrameworkCore;
using password.hashedpassword;
using UserTasks.db;
using UserTasks.Models.User;

namespace UserTasks.UserServices;


public class UserRepository
{

    private UserTasksContext _context;
    private HashedPassword _hashpassword;

    public UserRepository(UserTasksContext context, HashedPassword hashedPassword)
    {
        _context = context;
        _hashpassword = hashedPassword;
    }

    public async Task<List<Users>> GetUsersAsync()
    {
        var user = await _context.users.ToListAsync<Users>();
        var tasks = await _context.userTasks.ToListAsync();
        foreach (var userItem in user)
        {
            foreach (var item in tasks)
            {
                if (userItem.ID == item.UsersId)
                {
                    userItem.Tasks.Add(item);
                }
            }
        }
        return user;
    }

    public async Task<Users> GetUserAsync(string email)
    {
        return await _context.users.SingleAsync(data => data.email == email);
    }

    public async Task<Users> CreateUserAsync(Users userDTO)
    {
        _context.users.Add(userDTO);
        await _context.SaveChangesAsync();
        return userDTO;
    }

    public async Task<Users> findOne(int id)
    {
        Users user = await _context.users.SingleAsync(a => a.ID == id);
        var tasks = await _context.userTasks.ToListAsync();
        foreach (var item in tasks)
        {
            if (item.UsersId == id)
            {
                user.Tasks.Add(item);
            }
        }
        return user;
    }

    public async Task<Users> delete(int id)
    {
        Users user = await findOne(id);
        _context.Remove(await _context.users.SingleAsync(a => a.ID == id));
        _context.SaveChanges();
        return user;
    }

    public async Task<Users> update(int id, Users updatedUser)
    {
        Users user = await _context.users.SingleAsync(a => a.ID == id);

        Users userDTO = new Users
        {
            username = updatedUser.username,
            email = updatedUser.email,
            Password = _hashpassword.hashedpassword(updatedUser.Password),
        };

        user.username = userDTO.username;
        user.email = userDTO.email;
        user.Password = userDTO.Password;

        await _context.SaveChangesAsync();
        return await Task.FromResult(user);
    }
}