using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChattyMoAPI.Models;

public class ChatMessage
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required] [MinLength(1)] public string Message { get; set; }


    [Required] public long UserId { get; set; }

    public User User { get; set; }

    [Required] public DateTime Created { get; set; }
}