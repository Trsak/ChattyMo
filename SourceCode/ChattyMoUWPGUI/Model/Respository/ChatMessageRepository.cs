using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ChattyMoUWPGUI.Context;
using ChattyMoUWPGUI.Model.Request;
using ChattyMoUWPGUI.Model.Response;
using Newtonsoft.Json;

namespace ChattyMoUWPGUI.Model.Respository;

public class ChatMessageRepository : BaseRepository, IChatMessageRepository
{
    public async Task<bool> SendMessage(string message)
    {
        var requestBody = new SendMessageRequest(message);
        var jsonContent = JsonConvert.SerializeObject(requestBody);

        var response = await HttpClientManager.Client.PostAsync("ChatMessage/Create",
            new StringContent(jsonContent, Encoding.UTF8, "application/json"));
        await EnsureRequestIsSuccessful(response);

        return true;
    }

    public async Task<ICollection<ChatMessage>> GetLatestMessages()
    {
        var response = await HttpClientManager.Client.GetAsync("ChatMessage/GetLatest");
        await EnsureRequestIsSuccessful(response);

        var messagesJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ICollection<ChatMessage>>(messagesJson);
    }
}