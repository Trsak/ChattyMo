using System.Collections.Generic;
using System.Threading.Tasks;
using ChattyMoUWPGUI.Model.Response;

namespace ChattyMoUWPGUI.Model.Respository;

public interface IUserRepository
{
    Task<bool> Register(string username, string password);
    Task<UserWithToken> Authenticate(string username, string password);
    Task<ICollection<User>> FindByUsername(string? username);
    Task<bool> ChangePassword(string oldPassword, string newPassword);
}