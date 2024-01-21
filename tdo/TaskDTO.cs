using System.ComponentModel.DataAnnotations;

namespace UserTasks.task.tdo;

public record TaskDTO(
    [Required][StringLength(50, MinimumLength = 2)]
    string  Title,

    [Required][StringLength(50, MinimumLength = 4)]
    string Description,

    [Required]
    int UserID,

    [Required]
    DateTime EndDate);
