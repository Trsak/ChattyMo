using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ChattyMoWPFGUI.Model.Request;
using ChattyMoWPFGUI.Model.Response;

namespace ChattyMoWPFGUI.Model.Repository;

public class ChatMessageRepository : BaseRepository, IChatMessageRepository
{
    public async Task<bool> SendMessage(string message)
    {
        var requestBody = new SendMessageRequest(message);

        var response = await HttpClientManager.Client.PostAsJsonAsync("ChatMessage/Create", requestBody);
        await EnsureRequestIsSuccessful(response);

        return true;
    }

    public async Task<ICollection<ChatMessage>> GetLatestMessages()
    {
        var response = await HttpClientManager.Client.GetAsync("ChatMessage/GetLatest");
        await EnsureRequestIsSuccessful(response);

        return await response.Content.ReadFromJsonAsync<ICollection<ChatMessage>>();
    }
}