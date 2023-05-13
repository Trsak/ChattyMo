using ChattyMoUWPGUI.Model.Response;

namespace ChattyMoUWPGUI.Context;

public class UserContext
{
    public static UserWithToken? User;

    public static void SetUser(UserWithToken? user)
    {
        User = user;
    }
}