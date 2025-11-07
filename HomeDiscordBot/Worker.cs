using Discord;
using Discord.WebSocket;
using HomeDiscordBot.Commands;
using HomeDiscordBot.Discord;

namespace HomeDiscordBot;

public class Worker(DiscordBot bot) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken) => 
        bot.StartAsync(cancellationToken);

    public Task StopAsync(CancellationToken cancellationToken) =>
        bot.StopAsync(cancellationToken);
}