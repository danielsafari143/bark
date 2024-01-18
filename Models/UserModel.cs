using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using UserTasks.Models.Tasks;

namespace UserTasks.Models.User;

public class User
{
    public int ID{set;get;}

    [StringLength(50, MinimumLength = 2)]
    public required string username{set;get;}

    [EmailAddress]
    public required string email {set;get;}
    [PasswordPropertyText]
    public required string  Password {set;get;}

    public List<UserTask> userTasks {get;} = new List<UserTask>(); 
}