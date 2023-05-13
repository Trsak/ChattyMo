using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ChattyMoAPI.Data;
using ChattyMoAPI.Helpers;
using ChattyMoAPI.Models;
using ChattyMoAPI.Models.Exception;
using ChattyMoAPI.Models.Request;
using ChattyMoAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ChattyMoAPI.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppSettings _appSettings;
    private readonly ApiDbContext _db;

    public UserRepository(ApiDbContext db, IOptions<AppSettings> appSettings)
    {
        _db = db;
        _appSettings = appSettings.Value;
    }

    public async Task<UserWithTokenModel> Authenticate(string username, string password)
    {
        var user = await _db.Users.SingleOrDefaultAsync(x => x.Username == username);

        if (user == null) throw new NonExistentUserException();

        if (!BCrypt.Net.BCrypt.Verify(password, user.Password)) throw new InvalidUserPasswordException();

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.JwtKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        var userWithToken = new UserWithTokenModel
        {
            Id = user.Id,
            Username = user.Username,
            Token = tokenHandler.WriteToken(token)
        };

        return userWithToken;
    }

    public async Task<User> Register(string username, string passwordHash)
    {
        ThrowExceptionIfUserWithUsernameExist(username);

        var user = new User
        {
            Username = username,
            Password = passwordHash,
            Created = DateTime.Now,
            LastAction = DateTime.Now
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task<bool> UpdatePassword(int id, string oldPassword, string newPasswordHash)
    {
        var user = await _db.Users.SingleOrDefaultAsync(x => x.Id == id);

        if (user == null) throw new NonExistentUserException();
        if (!BCrypt.Net.BCrypt.Verify(oldPassword, user.Password)) throw new InvalidUserPasswordException();

        user.Password = newPasswordHash;
        _db.Users.Update(user);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<User> GetUserById(int id)
    {
        var user = await _db.Users.SingleOrDefaultAsync(x => x.Id == id);

        if (user == null) throw new NonExistentUserException();

        return user;
    }

    public async Task<ICollection<User>> FindByUsername(string? username)
    {
        if (username == null) return await _db.Users.Take(50).OrderBy(u => u.Id).ToListAsync();

        return await _db.Users.Where(x => x.Username.ToLower().Contains(username.ToLower())).OrderBy(u => u.Id).Take(50)
            .ToListAsync();
    }

    private void ThrowExceptionIfUserWithUsernameExist(string username)
    {
        var userByUsername = _db.Users.Any(x => x.Username == username);

        if (userByUsername) throw new DuplicateUserUsernameException();
    }
}