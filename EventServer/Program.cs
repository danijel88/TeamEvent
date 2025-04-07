using EventServer.Infrastructure;
using EventServer.Persistence.Interfaces;
using EventServer.Persistence;
using Microsoft.EntityFrameworkCore;
using TeamEvent.Server.Persistence.Interfaces;

namespace EventServer;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        // Add DB context
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
        });

        // Add custom services
        builder.Services.AddScoped<IEventRepository, EventRepository>();
        builder.Services.AddScoped<IFakeEmailSender, FakeEmailSender>();
        builder.Services.AddScoped<IEventService, EventService>();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) app.MapOpenApi();

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}