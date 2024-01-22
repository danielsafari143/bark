using Microsoft.EntityFrameworkCore;
using UserTasks.Models.Tasks;
using UserTasks.Models.User;

namespace UserTasks.db;

public class UserTasksContext : DbContext
{
    private string? _databaseParams;

    public UserTasksContext(DbContextOptions options , IConfiguration configuration) : base(options) { 
		_databaseParams = configuration["UserTasks:ConnectionString"];	
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{

		optionsBuilder.UseNpgsql(@"");
	}

	public DbSet<UserTask> userTasks { get; set; }
	public DbSet<Users> users { get; set; }

}