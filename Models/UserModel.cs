using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserTasks.Models.Tasks;

namespace UserTasks.Models.User;

public class Users
{
    public int ID{set;get;}

    [StringLength(50, MinimumLength = 2)]
    public required string username{set;get;}

    [EmailAddress]
    public required string email {set;get;}
    [PasswordPropertyText]
    public required string  Password {set;get;}

    public ICollection<UserTask> Tasks {get;set;} = new List<UserTask>();
}