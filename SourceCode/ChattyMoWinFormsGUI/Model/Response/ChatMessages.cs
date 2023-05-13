namespace ChattyMoWinFormsGUI.Model.Response;

public class ChatMessages
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string Message { get; set; }
    public DateTime Created { get; set; }
}