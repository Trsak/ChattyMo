using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChattyMoAPI.Models.Request;

[NotMapped]
public class UserWithTokenModel
{
    [Required] public long Id { get; set; }

    [Required]
    [MaxLength(30)]
    [MinLength(3)]
    public string Username { get; set; }

    [Required] public string Token { get; set; }
}