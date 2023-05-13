using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChattyMoAPI.Models.Request;

[NotMapped]
public class PasswordChangeModel
{
    [Required]
    [MaxLength(60)]
    [MinLength(3)]
    public string OldPassword { get; set; }

    [Required]
    [MaxLength(60)]
    [MinLength(3)]
    public string NewPassword { get; set; }
}