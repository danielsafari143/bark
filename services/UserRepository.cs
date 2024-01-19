using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserTasks.db;
using UserTasks.Models.Tasks;
using UserTasks.Models.User;

namespace UserTasks.UserServices;


public class UserRepository {

    private UserTasksContext context;
    public UserRepository (UserTasksContext context) {
        this.context = context;
    }

    public async Task<List<User>> GetUsersAsync () {
        return await context.users.ToListAsync<User>();
    }
    
    public async Task<User> CreateUserAsync(User userDTO) {
        context.users.Add(userDTO);
        await context.SaveChangesAsync();
        return userDTO;
    }

    public async Task<User> GetUserAsync(string email){
       try
       {
         return await context.users.SingleOrDefaultAsync(data => data.email == email);
       }
       catch (System.Exception e)
       {
        
            throw e;
       }
    }

    public async Task<User> findOne (int id) {
        return  await context.users.FindAsync(id);
    } 

    public async Task<User> delete(int id) {
        User user = await findOne(id);
        context.Remove(await context.users.SingleAsync(a => a.ID == id));
        context.SaveChanges();
        return user;
    }

    public async Task<User> update (User updatedUser) {
        User user = await context.users.SingleAsync(a => a.ID == updatedUser.ID);

        User userDTO = new User{
            username= updatedUser.username,
            email = updatedUser.email,
            Password = updatedUser.Password
        };
        user.username = userDTO.username;
        user.email = userDTO.email;
        user.Password = userDTO.Password;

        context.SaveChanges();
        return await Task.FromResult(user);
    }
} 