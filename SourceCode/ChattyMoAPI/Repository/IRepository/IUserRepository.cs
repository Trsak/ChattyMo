using ChattyMoAPI.Models;
using ChattyMoAPI.Models.Request;

namespace ChattyMoAPI.Repository.IRepository;

public interface IUserRepository
{
    Task<UserWithTokenModel> Authenticate(string username, string password);

    Task<User> Register(string username, string passwordHash);

    Task<bool> UpdatePassword(int id, string oldPassword, string newPasswordHash);

    Task<User> GetUserById(int id);

    Task<ICollection<User>> FindByUsername(string? username);
}