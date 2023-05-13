using ChattyMoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ChattyMoAPI.Data;

public static class DbInitializer
{
    public static void Initialize(ApiDbContext context)
    {
        context.Database.EnsureCreated();
        CreateUsers(context);
        CreateChatMessages(context);
    }

    private static void CreateUsers(ApiDbContext context)
    {
        if (context.Users.Any()) return;

        var users = new User[]
        {
            new()
            {
                Password = BCrypt.Net.BCrypt.HashPassword("testpassword"), Username = "GreedyUser",
                Created = DateTime.Now, LastAction = DateTime.Now
            },
            new()
            {
                Password = BCrypt.Net.BCrypt.HashPassword("testpassword"), Username = "Johnny New",
                Created = DateTime.Now, LastAction = DateTime.Now
            },
            new()
            {
                Password = BCrypt.Net.BCrypt.HashPassword("testpassword"), Username = "Jack Sparrow",
                Created = DateTime.Now, LastAction = DateTime.Now
            },
            new()
            {
                Password = BCrypt.Net.BCrypt.HashPassword("testpassword"), Username = "Randy Storm",
                Created = DateTime.Now, LastAction = DateTime.Now
            },
            new()
            {
                Password = BCrypt.Net.BCrypt.HashPassword("testpassword"), Username = "Party Man",
                Created = DateTime.Now, LastAction = DateTime.Now
            },
            new()
            {
                Password = BCrypt.Net.BCrypt.HashPassword("testpassword"), Username = "Frank Nicolson",
                Created = DateTime.Now, LastAction = DateTime.Now
            }
        };

        var currentId = 1;
        foreach (var user in users)
        {
            if (context.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory") user.Id = currentId++;

            context.Users.Add(user);
        }

        try
        {
            context.SaveChanges();
        }
        catch (DbUpdateException exception)
        {
            Console.WriteLine($"Exception while adding init data! {exception.Message}");
        }
    }

    private static void CreateChatMessages(ApiDbContext context)
    {
        if (context.ChatMessages.Any()) return;

        var chatMessages = new ChatMessage[]
        {
            new()
            {
                Message = "Hey there", UserId = 1, Created = DateTime.Now.AddHours(-1).AddMinutes(-5)
            },
            new()
            {
                Message = "Hello, everyone!", UserId = 2, Created = DateTime.Now.AddHours(-1).AddMinutes(-4)
            },
            new()
            {
                Message = "Hi, how are you?", UserId = 3, Created = DateTime.Now.AddHours(-1).AddMinutes(-2)
            },
            new()
            {
                Message = "Good, what about you?", UserId = 2, Created = DateTime.Now.AddHours(-1).AddMinutes(-1)
            },
            new()
            {
                Message = "Doing well.", UserId = 3, Created = DateTime.Now.AddMinutes(-55)
            },
            new()
            {
                Message = "What are you guys doing tomorrow?", UserId = 4, Created = DateTime.Now.AddMinutes(-50)
            },
            new()
            {
                Message = "Why, wanna go out?", UserId = 5, Created = DateTime.Now.AddMinutes(-48)
            },
            new()
            {
                Message = "Yes, lets do something!", UserId = 4, Created = DateTime.Now.AddMinutes(-45)
            },
            new()
            {
                Message = "I'm in!", UserId = 5, Created = DateTime.Now.AddMinutes(-42)
            },
            new()
            {
                Message = "Will join you guys!", UserId = 3, Created = DateTime.Now.AddMinutes(-41)
            },
            new()
            {
                Message = "Does anyone have hove some movie tip for a night?", UserId = 2,
                Created = DateTime.Now.AddMinutes(-40)
            },
            new()
            {
                Message = "Home Alone is the way!", UserId = 1, Created = DateTime.Now
            }
        };

        var currentId = 1;
        foreach (var chatMessage in chatMessages)
        {
            if (context.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory") chatMessage.Id = currentId++;

            context.ChatMessages.Add(chatMessage);
        }

        try
        {
            context.SaveChanges();
        }
        catch (DbUpdateException exception)
        {
            Console.WriteLine($"Exception while adding init data! {exception.Message}");
        }
    }
}