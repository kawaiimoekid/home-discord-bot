using Discord.WebSocket;
using HomeDiscordBot.Commands;
using HomeDiscordBot.Commands.Slash;
using HomeDiscordBot.Discord;

namespace HomeDiscordBot;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        
        builder.Services.Configure<DiscordOptions>(builder.Configuration.GetSection("Discord"));
        
        builder.Services.AddSingleton<DiscordSocketClient>();
        builder.Services.AddSingleton<CommandRegistry>();
        builder.Services.AddSingleton<ISlashCommand, DonutSlashCommand>();

        builder.Services.AddSingleton<DiscordBot>();
        
        builder.Services.AddHostedService<Worker>();

        var host = builder.Build();
        host.Run();
    }
}