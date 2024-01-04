using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.SlashCommands;
using DSharpPlus.Entities;
using DSharpPlus;

namespace JSS.BOT.SlashCommands.Admin
{
    public class TicketSlash : ApplicationCommandModule
    {
        [SlashCommand("ticket", "Get information about the emoji")]
        [RequirePermissions(Permissions.Administrator)]
        public static async Task Create(InteractionContext ctx)
        {
            try
            {
                var options = new List<DiscordSelectComponentOption>
                        {
                            new("Suporte", "suportt", "Ticket de suporte"),
                        };

                var response = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder
                    {
                        Title = "Menu de Tickets",
                        Description = "Selecione um tipo de ticket abaixo:",
                        Color = new DiscordColor(0x00ff00),
                    })
                    .AddComponents(new DiscordSelectComponent(
                        "ticket_type",
                        "Selecione um tipo de ticket...",
                        options.AsEnumerable()
                    ));

                await ctx.Channel.SendMessageAsync(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex}");

                await ctx.CreateResponseAsync(
                    InteractionResponseType.ChannelMessageWithSource,
                    new DiscordInteractionResponseBuilder()
                        .WithContent($"Ocorreu um erro durante a interação!")
                        .AsEphemeral(true)
                );
            }
        }
    }
}
