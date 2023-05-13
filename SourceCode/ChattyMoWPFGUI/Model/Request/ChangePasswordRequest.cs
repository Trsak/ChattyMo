namespace ChattyMoWPFGUI.Model.Request;

public class ChangePasswordRequest
{
    public ChangePasswordRequest(string oldPassword, string newPassword)
    {
        OldPassword = oldPassword;
        NewPassword = newPassword;
    }

    public string OldPassword { get; set; }

    public string NewPassword { get; set; }
}