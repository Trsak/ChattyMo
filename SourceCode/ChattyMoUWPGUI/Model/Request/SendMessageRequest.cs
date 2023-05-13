namespace ChattyMoUWPGUI.Model.Request;

public class SendMessageRequest
{
    public SendMessageRequest(string message)
    {
        Message = message;
    }

    public string Message { get; set; }
}