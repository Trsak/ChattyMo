using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Web;
using ChattyMoWinFormsGUI.Model.Request;
using ChattyMoWinFormsGUI.Model.Response;

namespace ChattyMoWinFormsGUI.Model.Repository;

public class UserRepository : BaseRepository, IUserRepository
{
    private readonly HttpClient _client;
    private UserWithToken? _currentUser;

    public UserRepository(HttpClient client)
    {
        _client = client;
    }

    public async Task<UserWithToken> Authenticate(string username, string password)
    {
        var requestBody = new AuthenticationRequest(username, password);

        var response = await _client.PostAsJsonAsync("User/Authenticate", requestBody);
        await EnsureRequestIsSuccessful(response);

        var user = await response.Content.ReadFromJsonAsync<UserWithToken>();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
        _currentUser = user;
        return user;
    }

    public async Task<bool> Register(string username, string password)
    {
        var requestBody = new AuthenticationRequest(username, password);

        var response = await _client.PostAsJsonAsync("User/Register", requestBody);
        await EnsureRequestIsSuccessful(response);

        return true;
    }

    public async Task<bool> ChangePassword(string oldPassword, string newPassword)
    {
        var requestBody = new ChangePasswordRequest(oldPassword, newPassword);
        var response = await _client.PatchAsync($"User/{_currentUser.Id}/UpdatePassword",
            new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json"));
        await EnsureRequestIsSuccessful(response);

        return true;
    }

    public async Task<ICollection<User>> FindByUsername(string? username)
    {
        var requestUri = "User/FindByUsername";
        if (username != null && !string.IsNullOrWhiteSpace(username))
            requestUri += $"/{HttpUtility.UrlEncode(username)}";

        var response = await _client.GetAsync(requestUri);
        await EnsureRequestIsSuccessful(response);

        return await response.Content.ReadFromJsonAsync<ICollection<User>>();
    }

    public void Logout()
    {
        _client.DefaultRequestHeaders.Authorization = null;
        _currentUser = null;
    }

    public UserWithToken? GetCurrentUser()
    {
        return _currentUser;
    }
}