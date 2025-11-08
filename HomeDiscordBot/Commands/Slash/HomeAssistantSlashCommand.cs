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
                    .WithName("off")
                    .WithDescription("Turn off")
                    .WithType(ApplicationCommandOptionType.SubCommand)
                    .AddOption(new SlashCommandOptionBuilder()
                        .WithName("id")
                        .WithDescription("Select light to turn off")
                        .WithRequired(true)
                        .AddChoice("all", "all")
                        .AddChoice("office", "office")
                        .WithType(ApplicationCommandOptionType.String)
                    )
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
            case "off":
                await HandleLightOffSubcommand(command, subcommand);
                break;
        }
    }

    private async Task HandleLightOffSubcommand(SocketSlashCommand command, SocketSlashCommandDataOption option)
    {
        var subcommand = option.Options.First();
        switch (subcommand.Value)
        {
            case "all":
                await client.TurnOffAllLights();
                break;
            case "office":
                break;
        }
    }
}