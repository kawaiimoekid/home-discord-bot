using Discord;
using Discord.WebSocket;

namespace HomeDiscordBot.Commands.Slash;

public class DonutSlashCommand : ISlashCommand
{
    public SlashCommandProperties Build() => 
        new SlashCommandBuilder()
            .WithName("donut")
            .WithDescription("Returns a beautiful donut")
            .Build();

    public Task HandleAsync(SocketSlashCommand command) => 
        command.RespondAsync("https://tenor.com/view/ace-one-piece-ace-one-piece-donut-luffy-gif-7963656468025218985");
}