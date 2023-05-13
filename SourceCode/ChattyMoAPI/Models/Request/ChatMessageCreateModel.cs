using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChattyMoAPI.Models.Request;

[NotMapped]
public class ChatMessageCreateModel
{
    [Required] [MinLength(1)] public string Message { get; set; }
}