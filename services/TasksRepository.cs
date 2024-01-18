using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using user.tdo;
using UserTasks.db;
using UserTasks.Models.User;

namespace UserTasks.tasksServices;

public class TasksRepository {

    private UserTasksContext context;
    public TasksRepository(UserTasksContext context) {
        this.context = context;
    }

    public async Task<List<User>> GetUsersAsync () {
        return await context.users.ToListAsync<User>();
    }
    
    public async Task CreateUserAsync(User userDTO) {
        context.users.Add(userDTO);
        await context.SaveChangesAsync();
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
}