using System.ComponentModel.DataAnnotations;

namespace UserTasks.user.tdo;

public record UserDTO( 
    [Required][StringLength(50, MinimumLength = 2)] 
    string  Username, 

    [Required][StringLength(50, MinimumLength = 4)] 
    string Password,

    [EmailAddress][Required]
    string Email);
