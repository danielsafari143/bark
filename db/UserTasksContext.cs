using Microsoft.EntityFrameworkCore;
using UserTasks.Models.Tasks;
using UserTasks.Models.User;

namespace UserTasks.db;

public class UserTasksContext: DbContext {
    public UserTasksContext(DbContextOptions options):base(options){}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Server=sql.bsite.net\MSSQL2016 ;Database=UserTask;Username: safari_userTasks; Trusted_Connection=True");
    }

    public DbSet<User> users{get;set;}
    public DbSet<TaskModel> taskModels{get;set;}
}