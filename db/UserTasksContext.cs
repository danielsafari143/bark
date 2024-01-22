using Microsoft.EntityFrameworkCore;
using UserTasks.Models.Tasks;
using UserTasks.Models.User;

namespace UserTasks.db;

public class UserTasksContext : DbContext
{
    public UserTasksContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseNpgsql(@"");
    }

    public DbSet<UserTask> userTasks { get; set; }
    public DbSet<Users> users { get; set; }

}