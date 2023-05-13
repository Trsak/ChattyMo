using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChattyMoAPI.Models.Request;

[NotMapped]
public class AuthenticationModel
{
    public AuthenticationModel(string username, string password)
    {
        Username = username;
        Password = password;
    }

    [Required]
    [MaxLength(30)]
    [MinLength(3)]
    public string Username { get; set; }

    [Required]
    [MaxLength(60)]
    [MinLength(3)]
    public string Password { get; set; }
}