using System.Collections.Generic;
using System.Threading.Tasks;
using ChattyMoUWPGUI.Model.Response;

namespace ChattyMoUWPGUI.Model.Respository;

public interface IChatMessageRepository
{
    Task<bool> SendMessage(string message);
    Task<ICollection<ChatMessage>> GetLatestMessages();
}