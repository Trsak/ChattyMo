using ChattyMoAPI.Models;
using ChattyMoAPI.Models.Request;

namespace ChattyMoAPI.Repository.IRepository;

public interface IChatMessageRepository
{
    Task<ChatMessage> Create(int userId, string messsage);
    Task<ICollection<ChatMessageWithUserModel>> GetLatest();
}