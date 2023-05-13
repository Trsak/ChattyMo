using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ChattyMoUWPGUI.Context;
using ChattyMoUWPGUI.Model.Request;
using ChattyMoUWPGUI.Model.Response;
using Newtonsoft.Json;

namespace ChattyMoUWPGUI.Model.Respository;

public class UserRepository : BaseRepository, IUserRepository
{
    public async Task<bool> Register(string username, string password)
    {
        var requestBody = new AuthenticationRequest(username, password);
        var jsonContent = JsonConvert.SerializeObject(requestBody);

        var response = await HttpClientManager.Client.PostAsync("User/Register",
            new StringContent(jsonContent, Encoding.UTF8, "application/json"));
        await EnsureRequestIsSuccessful(response);

        return true;
    }

    public async Task<UserWithToken> Authenticate(string username, string password)
    {
        var requestBody = new AuthenticationRequest(username, password);
        var jsonContent = JsonConvert.SerializeObject(requestBody);

        var response = await HttpClientManager.Client.PostAsync("User/Authenticate",
            new StringContent(jsonContent, Encoding.UTF8, "application/json"));
        await EnsureRequestIsSuccessful(response);

        var userJson = await response.Content.ReadAsStringAsync();
        var user = JsonConvert.DeserializeObject<UserWithToken>(userJson);

        HttpClientManager.Client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", user.Token);
        UserContext.SetUser(user);

        return user;
    }

    public async Task<ICollection<User>> FindByUsername(string? username)
    {
        var requestUri = "User/FindByUsername";
        if (username != null && !string.IsNullOrWhiteSpace(username))
            requestUri += $"/{HttpUtility.UrlEncode(username)}";

        var response = await HttpClientManager.Client.GetAsync(requestUri);
        await EnsureRequestIsSuccessful(response);

        var usersJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ICollection<User>>(usersJson);
    }

    public async Task<bool> ChangePassword(string oldPassword, string newPassword)
    {
        var requestBody = new ChangePasswordRequest(oldPassword, newPassword);
        var jsonContent = JsonConvert.SerializeObject(requestBody);
        var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"User/{UserContext.User.Id}/UpdatePassword");
        request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await HttpClientManager.Client.SendAsync(request);
        await EnsureRequestIsSuccessful(response);

        return true;
    }
}