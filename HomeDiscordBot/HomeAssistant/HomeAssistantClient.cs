using HADotNet.Core;
using HADotNet.Core.Clients;
using HADotNet.Entities;
using HADotNet.Entities.Models;
using Microsoft.Extensions.Options;

namespace HomeDiscordBot.HomeAssistant;

public class HomeAssistantClient
{
    private readonly EntityClient _entityClient;
    private readonly StatesClient _statesClient;
    private readonly ServiceClient _serviceClient;
    
    public HomeAssistantClient(IOptions<HomeAssistantOptions> options)
    {
        ClientFactory.Initialize(options.Value.Hostname, options.Value.Token);
        
        _entityClient = ClientFactory.GetClient<EntityClient>();
        _statesClient = ClientFactory.GetClient<StatesClient>();
        _serviceClient = ClientFactory.GetClient<ServiceClient>();
    }

    public Task TurnOffAllLights() => 
        _serviceClient.CallService("script.turn_off_all_lights", null);
}
