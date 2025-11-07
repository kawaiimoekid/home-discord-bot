using Discord;
using Discord.WebSocket;

namespace HomeDiscordBot.Commands;

public interface ISlashCommand
{
    SlashCommandProperties Build();
    Task HandleAsync(SocketSlashCommand command);
}