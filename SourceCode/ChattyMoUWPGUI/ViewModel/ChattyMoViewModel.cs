using ChattyMoUWPGUI.Context;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ChattyMoUWPGUI.ViewModel;

public partial class ChattyMoViewModel : ObservableObject
{
    [ObservableProperty]
    private string loggedInUsername;

    public ChattyMoViewModel()
    {
        LoggedInUsername = UserContext.User.Username;
    }
}