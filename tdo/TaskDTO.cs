using System.ComponentModel.DataAnnotations;
using UserTasks.Models.Tasks;
using UserTasks.Models.User;

namespace UserTasks.task.tdo;

public record TaskDTO(
    [Required][StringLength(50, MinimumLength = 2)]
    string  Title,

    [Required][StringLength(50, MinimumLength = 4)]
    string Description,

    [Required]
    int UserID,

    [Required]
    DateTime CreatedOn);
