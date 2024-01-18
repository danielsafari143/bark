using System.ComponentModel.DataAnnotations;
using UserTasks.Models.User;
namespace UserTasks.Models.Tasks;

public class TaskModel
{
     public int ID {set;get;}
     [Required]
     public required string Title{set;get;}
     [Required]
     public required string Description{set;get;}
     [Required]
     public int? UserID{set;get;}
     public DateTime CreatedOn { get; set; }

}

