using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using DSharpPlus.Interactivity;
using JSS.BOT.Modules;
using DSharpPlus;

internal class Program
{
    private static DiscordClient? Client { get; set; }
    private static SlashCommandsExtension? SlashCommands { get; set; }

    static async Task Main(string[] args)
    {
        DiscordConfiguration configDV = new()
        {
            Intents = DiscordIntents.All,
            Token = "MTEyMTI2OTYwOTE5OTMxNzA1Mg.GRkwoC.tcelL6yWupJZSe5X4TkLQHyZjPNu8qmiVQvY4s",
            TokenType = TokenType.Bot,
            AutoReconnect = true
        };

        Client = new DiscordClient(configDV);

        Client.UseInteractivity(new InteractivityConfiguration()
        {
            Timeout = TimeSpan.FromMinutes(2)
        });

        SlashCommands = Client.UseSlashCommands();

        SlashCommandModule.Load(SlashCommands);
        EventBaseModule.Load(Client);

        await Client.ConnectAsync();
        await Task.Delay(-1);
    }
}
