using ChattyMoWinFormsGUI.Presenter;

namespace ChattyMoWinFormsGUI.View;

public interface IAuthView
{
    string Username { get; set; }
    string Password { get; set; }
    UserPresenter Presenter { set; }
}