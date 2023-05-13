using ChattyMoWinFormsGUI.Presenter;

namespace ChattyMoWinFormsGUI.View;

public interface IChattyMoView
{
    string UserSearch { get; set; }
    string OldPassword { get; set; }
    string NewPassword { get; set; }
    string ChatMessage { get; set; }
    UserPresenter UserPresenter { set; }
    ChatMessagePresenter ChatMessagePresenter { set; }
}