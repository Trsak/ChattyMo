using System.Collections.ObjectModel;
using ChattyMoAPI.Data;
using ChattyMoAPI.Helpers;
using ChattyMoAPI.Models;
using ChattyMoAPI.Models.Request;
using ChattyMoAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ChattyMoAPI.Repository;

public class ChatMessageRepository : IChatMessageRepository
{
    private readonly AppSettings _appSettings;
    private readonly ApiDbContext _db;

    public ChatMessageRepository(ApiDbContext db, IOptions<AppSettings> appSettings)
    {
        _db = db;
        _appSettings = appSettings.Value;
    }

    public async Task<ChatMessage> Create(int userId, string messsage)
    {
        var message = new ChatMessage
        {
            UserId = userId,
            Message = messsage,
            Created = DateTime.Now
        };

        _db.ChatMessages.Add(message);
        await _db.SaveChangesAsync();
        return message;
    }

    public async Task<ICollection<ChatMessageWithUserModel>> GetLatest()
    {
        var messagesWithUsers = new Collection<ChatMessageWithUserModel>();
        var messages = await _db.ChatMessages.OrderByDescending(cm => cm.Id).Include(cm => cm.User).Take(50)
            .ToListAsync();

        foreach (var message in messages)
            messagesWithUsers.Add(new ChatMessageWithUserModel(message.Id, message.User.Username, message.Message,
                message.Created));

        return messagesWithUsers;
    }
}