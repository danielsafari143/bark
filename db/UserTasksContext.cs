using Microsoft.EntityFrameworkCore;
using UserTasks.Models.Tasks;
using UserTasks.Models.User;

namespace UserTasks.db;

public class UserTasksContext: DbContext {
    public UserTasksContext(DbContextOptions options):base(options){}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Server=(localdb)\mssqllocaldb;Database=UserTask;Trusted_Connection=True");
    }

    public DbSet<User> users{get;set;}
    public DbSet<TaskModel> taskModels{get;set;}
}