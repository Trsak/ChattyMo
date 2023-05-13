using System.Collections.Generic;
using System.Threading.Tasks;
using ChattyMoWPFGUI.Model.Response;

namespace ChattyMoWPFGUI.Model.Repository;

public interface IChatMessageRepository
{
    Task<bool> SendMessage(string message);
    Task<ICollection<ChatMessage>> GetLatestMessages();
}