using System.ComponentModel.DataAnnotations;
using UserTasks.Models.User;
namespace UserTasks.Models.Tasks;

public class UserTask
{
     public int ID {set;get;}
     [Required][StringLength(20, MinimumLength = 2)]
     public required string Title{set;get;}
     
     [Required][StringLength(500, MinimumLength = 5)]
     public required string Description{set;get;}
     [Required]
     public int? UserID{set;get;}
     public DateTime CreatedOn { get; set; }

}

