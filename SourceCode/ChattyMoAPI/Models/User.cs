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
    [MaxLength(30)]
    [MinLength(3)]
    public string Username { get; set; }

    [Required]
    [MaxLength(60)]
    [MinLength(3)]
    [JsonIgnore]
    public string Password { get; set; }

    [Required] public DateTime Created { get; set; }

    [Required] public DateTime LastAction { get; set; }
}