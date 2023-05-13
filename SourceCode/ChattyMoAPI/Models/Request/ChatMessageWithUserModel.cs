using System.ComponentModel.DataAnnotations;

namespace ChattyMoAPI.Models.Request;

public class ChatMessageWithUserModel
{
    public ChatMessageWithUserModel(long id, string username, string message, DateTime created)
    {
        Id = id;
        Username = username;
        Message = message;
        Created = created;
    }

    [Required] public long Id { get; set; }

    [Required]
    [MaxLength(30)]
    [MinLength(3)]
    public string Username { get; set; }

    [Required] [MinLength(1)] public string Message { get; set; }

    [Required] public DateTime Created { get; set; }
}