using System;

namespace ChattyMoUWPGUI.Model.Response;

public class User
{
    public long Id { get; set; }
    public string Username { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastAction { get; set; }
}