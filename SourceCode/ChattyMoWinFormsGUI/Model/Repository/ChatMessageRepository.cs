using System.Net.Http.Json;
using ChattyMoWinFormsGUI.Model.Request;
using ChattyMoWinFormsGUI.Model.Response;

namespace ChattyMoWinFormsGUI.Model.Repository;

public class ChatMessageRepository : BaseRepository, IChatMessageRepository
{
    private readonly HttpClient _client;

    public ChatMessageRepository(HttpClient client)
    {
        _client = client;
    }

    public async Task<bool> SendMessage(string message)
    {
        var requestBody = new SendMessageRequest(message);

        var response = await _client.PostAsJsonAsync("ChatMessage/Create", requestBody);
        await EnsureRequestIsSuccessful(response);

        return true;
    }

    public async Task<ICollection<ChatMessages>> GetLatestMessages()
    {
        var response = await _client.GetAsync("ChatMessage/GetLatest");
        await EnsureRequestIsSuccessful(response);

        return await response.Content.ReadFromJsonAsync<ICollection<ChatMessages>>();
    }
}