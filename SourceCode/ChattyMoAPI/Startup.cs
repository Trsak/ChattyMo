using System.Text;
using ChattyMoAPI.Data;
using ChattyMoAPI.Filters;
using ChattyMoAPI.Helpers;
using ChattyMoAPI.Repository;
using ChattyMoAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace ChattyMoAPI;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApiDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("Database")));

        services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AppSettings:JwtKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        services.AddSwaggerGen(setup =>
        {
            setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "No need to put the `bearer` keyword in front of token",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });
            setup.OperationFilter<AuthResponsesOperationFilter>();
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IChatMessageRepository, ChatMessageRepository>();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider provider)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        if (env.IsDevelopment())
        {
            using var serviceScope = provider.CreateScope();
            var services = serviceScope.ServiceProvider;

            var dbContext = services.GetRequiredService<ApiDbContext>();
            if (dbContext.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                dbContext.Database.Migrate();

            DbInitializer.Initialize(dbContext);
        }

        app.UseRouting();
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.Use(async (context, next) =>
        {
            await next(context);

            if (context.User.Identity.IsAuthenticated)
            {
                var currentUserId = int.Parse(context.User.Identity.Name);
                await using (var dbContext = context.RequestServices.GetRequiredService<ApiDbContext>())
                {
                    var user = dbContext.Users.Where(u => u.Id == currentUserId).FirstOrDefault();
                    user.LastAction = DateTime.Now;
                    dbContext.Update(user);
                    await dbContext.SaveChangesAsync();
                }
            }
        });

        app.UseEndpoints(routes => { routes.MapControllers(); });
    }
}