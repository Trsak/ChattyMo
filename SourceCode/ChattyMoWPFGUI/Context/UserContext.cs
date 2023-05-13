using ChattyMoWPFGUI.Model.Response;

namespace ChattyMoWPFGUI.Context;

public class UserContext
{
    public static UserWithToken? User;

    public static void SetUser(UserWithToken? user)
    {
        User = user;
    }
}