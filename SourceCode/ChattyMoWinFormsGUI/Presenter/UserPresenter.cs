using ChattyMoWinFormsGUI.Model.Repository;
using ChattyMoWinFormsGUI.Model.Response;
using ChattyMoWinFormsGUI.View;

namespace ChattyMoWinFormsGUI.Presenter;

public class UserPresenter
{
    private readonly IAuthView _authView;
    private readonly IChattyMoView _chatView;
    private readonly IUserRepository _repository;

    public UserPresenter(
        IUserRepository userRepository,
        IAuthView authForm,
        IChattyMoView chattyMoView)
    {
        _authView = authForm;
        _authView.Presenter = this;

        _chatView = chattyMoView;
        _chatView.UserPresenter = this;

        _repository = userRepository;
    }

    public async Task<UserWithToken> Login()
    {
        return await _repository.Authenticate(_authView.Username, _authView.Password);
    }

    public async Task<bool> Register()
    {
        return await _repository.Register(_authView.Username, _authView.Password);
    }

    public async Task<bool> ChangePassword()
    {
        return await _repository.ChangePassword(_chatView.OldPassword, _chatView.NewPassword);
    }

    public async Task<ICollection<User>> FindByUsername()
    {
        return await _repository.FindByUsername(_chatView.UserSearch);
    }

    public void Logout()
    {
        _repository.Logout();
    }

    public UserWithToken? GetCurrentUser()
    {
        return _repository.GetCurrentUser();
    }
}