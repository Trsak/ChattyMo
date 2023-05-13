using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using ChattyMoWPFGUI.Context;
using ChattyMoWPFGUI.Model.Request;
using ChattyMoWPFGUI.Model.Response;

namespace ChattyMoWPFGUI.Model.Repository;

public class UserRepository : BaseRepository, IUserRepository
{
    public async Task<bool> Register(string username, string password)
    {
        var requestBody = new AuthenticationRequest(username, password);

        var response = await HttpClientManager.Client.PostAsJsonAsync("User/Register", requestBody);
        await EnsureRequestIsSuccessful(response);

        return true;
    }

    public async Task<UserWithToken> Authenticate(string username, string password)
    {
        var requestBody = new AuthenticationRequest(username, password);

        var response = await HttpClientManager.Client.PostAsJsonAsync("User/Authenticate", requestBody);
        await EnsureRequestIsSuccessful(response);

        var user = await response.Content.ReadFromJsonAsync<UserWithToken>();

        HttpClientManager.Client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", user.Token);

        return user;
    }

    public async Task<ICollection<User>> FindByUsername(string? username)
    {
        var requestUri = "User/FindByUsername";
        if (username != null && !string.IsNullOrWhiteSpace(username))
            requestUri += $"/{HttpUtility.UrlEncode(username)}";

        var response = await HttpClientManager.Client.GetAsync(requestUri);
        await EnsureRequestIsSuccessful(response);

        return await response.Content.ReadFromJsonAsync<ICollection<User>>();
    }

    public void Logout()
    {
        HttpClientManager.Client.DefaultRequestHeaders.Authorization = null;
        UserContext.SetUser(null);
    }

    public async Task<bool> ChangePassword(string oldPassword, string newPassword)
    {
        var requestBody = new ChangePasswordRequest(oldPassword, newPassword);
        var response = await HttpClientManager.Client.PatchAsync($"User/{UserContext.User.Id}/UpdatePassword",
            new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json"));
        await EnsureRequestIsSuccessful(response);

        return true;
    }
}