namespace HomeDiscordBot.HomeAssistant;

public class HomeAssistantOptions
{
    public const string Section = "HomeAssistant";
    
    public string Hostname { get; set; }
    public string Token { get; set; }
}