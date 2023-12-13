using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChattyMoAPI.Models.Request;

[NotMapped]
public class PasswordChangeModel
{
    [Required]
    [MaxLength(60, ErrorMessage = "Password can not be longer than 60 characters.")]
    [MinLength(3, ErrorMessage = "Password must be at least 3 characters long.")]
    public string OldPassword { get; set; }

    [Required]
    [MaxLength(60, ErrorMessage = "Password can not be longer than 60 characters.")]
    [MinLength(3, ErrorMessage = "Password must be at least 3 characters long.")]
    public string NewPassword { get; set; }
}