using System.ComponentModel.DataAnnotations;
using UserTasks.Models.Tasks;

namespace UserTasks.user.tdo;

public record UserDTO(
    [Required][StringLength(50, MinimumLength = 2)]
    string  Username,

    [Required][StringLength(50, MinimumLength = 4)]
    string Password,

    UserTask? Tasks,

    [EmailAddress][Required]
    string Email)
    ;
