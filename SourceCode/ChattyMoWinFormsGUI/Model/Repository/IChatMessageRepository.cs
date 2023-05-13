using ChattyMoWinFormsGUI.Model.Response;

namespace ChattyMoWinFormsGUI.Model.Repository;

public interface IChatMessageRepository
{
    Task<bool> SendMessage(string message);
    Task<ICollection<ChatMessages>> GetLatestMessages();
}