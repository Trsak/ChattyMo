using ChattyMoWinFormsGUI.Model.Response;

namespace ChattyMoWinFormsGUI.Model.Repository;

public interface IUserRepository
{
    Task<UserWithToken> Authenticate(string username, string password);
    Task<bool> Register(string username, string password);

    Task<bool> ChangePassword(string oldPassword, string newPassword);
    Task<ICollection<User>> FindByUsername(string? username);

    void Logout();

    public UserWithToken? GetCurrentUser();
}