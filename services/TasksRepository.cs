using Microsoft.EntityFrameworkCore;
using UserTasks.db;
using UserTasks.Models.Tasks;
using UserTasks.Models.User;
using UserTasks.task.tdo;

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

    public async Task<UserTask> findOne (int id) {
        return  await context.userTasks.FindAsync(id);
    } 

    public async Task<UserTask> delete(int id) {
        UserTask task = await findOne(id);
        context.Remove(await context.userTasks.SingleAsync(a => a.ID == id));
        context.SaveChanges();
        return task;
    }

    public async Task<UserTask> update (int id , TaskDTO taskDTO) {
        UserTask userTask = await context.userTasks.SingleAsync(a => a.ID == id);

        userTask.Title = taskDTO.Title;
        userTask.Description = taskDTO.Description;
        userTask.UsersId = taskDTO.UserID;
        userTask.EnDate = userTask.EnDate;

        context.SaveChanges();
        return await Task.FromResult(userTask);
    }
}