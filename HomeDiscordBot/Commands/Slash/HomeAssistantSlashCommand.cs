using System.Text;
using Discord;
using Discord.WebSocket;
using HomeDiscordBot.HomeAssistant;

namespace HomeDiscordBot.Commands.Slash;

public class HomeAssistantSlashCommand(HomeAssistantClient client) : ISlashCommand
{
    public SlashCommandProperties Build() => 
        new SlashCommandBuilder()
            .WithName("ha")
            .WithDescription("Home Assistant controls")
            .AddOption(new SlashCommandOptionBuilder()
                .WithName("light")
                .WithDescription("Commands related to light controls")
                .WithType(ApplicationCommandOptionType.SubCommandGroup)
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("list")
                    .WithDescription("Lists all lights")
                    .WithType(ApplicationCommandOptionType.SubCommand)
                )
            )
            .Build();

    public async Task HandleAsync(SocketSlashCommand command)
    {
        var subcommand = command.Data.Options.First();
        
        switch (subcommand.Name)
        {
            case "light":
                await HandleLightSubcommand(command, subcommand);
                break;
        }
    }

    private async Task HandleLightSubcommand(SocketSlashCommand command, SocketSlashCommandDataOption option)
    {
        var subcommand = option.Options.First();
        
        switch (subcommand.Name)
        {
            case "list":
                await HandleLightListSubcommand(command);
                break;
        }
    }

    private async Task HandleLightListSubcommand(SocketSlashCommand command)
    {
        var lights = await client.GetLightsAsync();
        var builder = new StringBuilder();
        
        builder.AppendLine("Available lights:");
        foreach (var light in lights)
            builder.AppendLine($"- {light.EntityName}: `{light.State}`");

        await command.RespondAsync(builder.ToString());
    }
}