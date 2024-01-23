using Microsoft.EntityFrameworkCore;
using UserTasks.db;
using UserTasks.Models.Tasks;
using UserTasks.task.tdo;

namespace UserTasks.tasksServices;

public class TasksRepository
{
    private UserTasksContext _context;
    public TasksRepository(UserTasksContext context)
    {
        _context = context;
    }

    public async Task<List<UserTask>> GetTasksAsync()
    {
        return await _context.userTasks.ToListAsync();
    }

    public async Task<UserTask> CreateUserAsync(UserTask userTask)
    {
        _context.userTasks.Add(userTask);
        await _context.SaveChangesAsync();
        return userTask;
    }

    public async Task<UserTask> findOne(int id)
    {
        return await _context.userTasks.FindAsync(id);
    }

    public async Task<UserTask> delete(int id)
    {
        UserTask task = await findOne(id);
        _context.Remove(await _context.userTasks.SingleAsync(a => a.ID == id));
        _context.SaveChanges();
        return task;
    }

    public async Task<UserTask> update(int id, TaskDTO taskDTO)
    {
        UserTask userTask = await _context.userTasks.SingleAsync(a => a.ID == id);

        userTask.Title = taskDTO.Title;
        userTask.Description = taskDTO.Description;
        userTask.UsersId = taskDTO.UserID;
        userTask.EnDate = taskDTO.EndDate;

        _context.SaveChanges();
        return await Task.FromResult(userTask);
    }
}