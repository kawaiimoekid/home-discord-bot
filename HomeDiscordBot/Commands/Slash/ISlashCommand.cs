using Discord;
using Discord.WebSocket;

namespace HomeDiscordBot.Commands.Slash;

public interface ISlashCommand
{
    SlashCommandProperties Build();
    Task HandleAsync(SocketSlashCommand command);
}