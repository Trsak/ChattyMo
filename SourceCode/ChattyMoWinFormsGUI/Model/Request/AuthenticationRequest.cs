namespace ChattyMoWinFormsGUI.Model.Request;

public class AuthenticationRequest
{
    public AuthenticationRequest(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public string Username { get; set; }

    public string Password { get; set; }
}