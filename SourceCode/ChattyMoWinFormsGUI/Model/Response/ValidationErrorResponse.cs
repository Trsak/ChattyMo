using System.Collections.ObjectModel;

namespace ChattyMoWinFormsGUI.Model.Response;

public class ValidationErrorResponse
{
    public Collection<string>? Errors { get; set; }

    public string ErrorMessage()
    {
        return "rest";
    }
}