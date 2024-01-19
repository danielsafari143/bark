using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserTasks.db;
using UserTasks.Models.Tasks;
using UserTasks.Models.User;

namespace UserTasks.tasksServices;

public class TasksRepository {

    private UserTasksContext context;
    public TasksRepository(UserTasksContext context) {
        this.context = context;
    }

    public async Task<List<UserTask>> GetTasksAsync () {
        return await context.userTasks.ToListAsync();
    }
    
    public async Task<UserTask> CreateUserAsync(UserTask userTask) {
        context.userTasks.Add(userTask);
        await context.SaveChangesAsync();
        return userTask;
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

    public async Task<UserTask> findOne (int id) {
        return  await context.userTasks.FindAsync(id);
    } 

}