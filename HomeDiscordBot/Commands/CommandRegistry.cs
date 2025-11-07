using Discord.WebSocket;
using HomeDiscordBot.Commands.Slash;

namespace HomeDiscordBot.Commands;

public class CommandRegistry
{
    private readonly DiscordSocketClient _client;
    private readonly IEnumerable<ISlashCommand> _slashCommands;
    private readonly ILogger<CommandRegistry> _logger;
    
    private readonly Dictionary<string, ISlashCommand> _commandMap = new();
    
    public CommandRegistry(DiscordSocketClient client, IEnumerable<ISlashCommand> slashCommands, ILogger<CommandRegistry> logger)
    {
        _client = client;
        _slashCommands = slashCommands;
        _logger = logger;
        
        _client.SlashCommandExecuted += HandleSlashCommandAsync;
    }

    public async Task InitializeAsync()
    {
        foreach (var slashCommand in _slashCommands)
        {
            var command = slashCommand.Build();
            _commandMap[command.Name.Value] = slashCommand;
            
            _logger.LogInformation("Command '{Command}' has been initialized.",  command.Name);
        }
        
        var builtCommands = _slashCommands
            .Select(x => x.Build())
            .ToArray();

        foreach (var guild in _client.Guilds)
            await guild.BulkOverwriteApplicationCommandAsync(builtCommands);
        
        _logger.LogInformation("Registered {Count} commands.", builtCommands.Length);
    }

    private async Task HandleSlashCommandAsync(SocketSlashCommand command)
    {
        if (_commandMap.TryGetValue(command.Data.Name, out var handler))
            await handler.HandleAsync(command);
    }
}