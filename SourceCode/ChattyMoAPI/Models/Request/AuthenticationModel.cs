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
    [MaxLength(30, ErrorMessage = "Username can not be longer than 30 characters.")]
    [MinLength(3, ErrorMessage = "Username must be at least 3 characters long.")]
    public string Username { get; set; }

    [Required]
    [MaxLength(60, ErrorMessage = "Password can not be longer than 60 characters.")]
    [MinLength(3, ErrorMessage = "Password must be at least 3 characters long.")]
    public string Password { get; set; }
}