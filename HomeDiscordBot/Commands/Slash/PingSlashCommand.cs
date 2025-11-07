using Discord;
using Discord.WebSocket;

namespace HomeDiscordBot.Commands.Slash;

public class PingSlashCommand : ISlashCommand
{
    public SlashCommandProperties Build() => 
        new SlashCommandBuilder()
            .WithName("ping")
            .WithDescription("Pongs back")
            .Build();

    public Task HandleAsync(SocketSlashCommand command) => 
        command.RespondAsync("Pong 🏓");
}