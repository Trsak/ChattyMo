using System.Collections.Generic;
using System.Threading.Tasks;
using ChattyMoWPFGUI.Model.Response;

namespace ChattyMoWPFGUI.Model.Repository;

public interface IUserRepository
{
    Task<bool> Register(string username, string password);
    Task<UserWithToken> Authenticate(string username, string password);
    Task<ICollection<User>> FindByUsername(string? username);
    void Logout();
    Task<bool> ChangePassword(string oldPassword, string newPassword);
}