using System;

namespace ChattyMoUWPGUI.Model.Response;

public class ChatMessage
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string Message { get; set; }
    public DateTime Created { get; set; }
}