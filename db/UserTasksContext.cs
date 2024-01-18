using Microsoft.EntityFrameworkCore;
using UserTasks.Models.Tasks;
using UserTasks.Models.User;

namespace UserTasks.db;

public class UserTasksContext: DbContext {
    public UserTasksContext(DbContextOptions options):base(options){}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            @"Server=localhost;Port=5432;Database=postgres;Username=task;Password=safari");
    }

    public DbSet<User> users{get;set;}
    public DbSet<TaskModel> taskModels{get;set;}
}