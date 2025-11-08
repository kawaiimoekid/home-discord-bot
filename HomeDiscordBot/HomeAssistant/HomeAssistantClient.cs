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
    private readonly EntitiesService _entityService;
    
    public HomeAssistantClient(IOptions<HomeAssistantOptions> options)
    {
        ClientFactory.Initialize(options.Value.Hostname, options.Value.Token);
        
        _entityClient = ClientFactory.GetClient<EntityClient>();
        _statesClient = ClientFactory.GetClient<StatesClient>();
        _entityService = new EntitiesService(_entityClient, _statesClient);
    }

    public Task<IEnumerable<Light>> GetLightsAsync() =>
        _entityService.GetEntities<Light>();
}
