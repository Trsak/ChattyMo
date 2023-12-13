using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ChattyMoAPI.Models;

[Index(nameof(Username), IsUnique = true)]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [MaxLength(30, ErrorMessage = "Username can not be longer than 30 characters.")]
    [MinLength(3, ErrorMessage = "Username must be at least 3 characters long.")]
    public string Username { get; set; }

    [Required]
    [MaxLength(60, ErrorMessage = "Password can not be longer than 60 characters.")]
    [MinLength(3, ErrorMessage = "Password must be at least 3 characters long.")]
    [JsonIgnore]
    public string Password { get; set; }

    [Required] public DateTime Created { get; set; }

    [Required] public DateTime LastAction { get; set; }
}