using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using user.tdo;
using UserTasks.db;
using UserTasks.Models.Tasks;

namespace UserTasks.UserServices;

public class UserRepository {

    private UserTasksContext context;
    public UserRepository (UserTasksContext context) {
        this.context = context;
    }

    [HttpGet]
    public async Task<List<UserTask>> GetTaskAsync () {
        return await context.userTasks.ToListAsync();
    }
}