using Discord;
using Discord.WebSocket;
using HomeDiscordBot.Commands;
using Microsoft.Extensions.Options;

namespace HomeDiscordBot.Discord;

public class DiscordBot(
    DiscordSocketClient client, 
    CommandRegistry commandRegistry, 
    IOptions<DiscordOptions> options
)
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        client.Ready += OnReady;
        
        await client.LoginAsync(TokenType.Bot, options.Value.Token);
        await client.StartAsync();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await client.StopAsync();
        await client.LogoutAsync();
    }

    private async Task OnReady()
    {
        await commandRegistry.InitializeAsync();
    }
}