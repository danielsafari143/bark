using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserTasks.db;
using UserTasks.Models.Tasks;
using UserTasks.Models.User;
using UserTasks.task.tdo;
using UserTasks.user.tdo;

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

    public async Task<UserTask> delete(int id) {
        UserTask task = await findOne(id);
        context.Remove(await context.userTasks.SingleAsync(a => a.ID == id));
        context.SaveChanges();
        return task;
    }

    public async Task<UserTask> update (UserTask task) {
        UserTask userTask = await context.userTasks.SingleAsync(a => a.ID == task.ID);

        TaskDTO taskDTO = new UserTask{
            Title = userTask.Title,
            Description = userTask.Description,
            UserID = userTask.UserID,
            CreatedOn = userTask.CreatedOn
        };

        userTask.Title = taskDTO.Title;
         userTask.Description = taskDTO.Description;
         userTask.UserID = taskDTO.UserID;
         userTask.CreatedOn = taskDTO.CreatedOn;

        context.SaveChanges();
        return await Task.FromResult(userTask);
    }
}