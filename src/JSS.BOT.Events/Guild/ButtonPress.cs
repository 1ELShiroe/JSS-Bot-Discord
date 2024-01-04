using JSS.BOT.Interfaces.Events;
using DSharpPlus.EventArgs;
using DSharpPlus.Entities;
using DSharpPlus;

namespace JSS.BOT.Events.Guild
{
    public class ButtonPress : IEventBase<ComponentInteractionCreateEventArgs>
    {
        public async Task Event(DiscordClient sender, ComponentInteractionCreateEventArgs args)
        {
            try
            {
                string CustomId = args.Interaction.Data.CustomId;

                if (CustomId == "ticket_type") Ticket(args.Interaction.Data.Values[0], args);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex}");

                await args.Interaction.CreateResponseAsync(
                    InteractionResponseType.ChannelMessageWithSource,
                    new DiscordInteractionResponseBuilder()
                        .WithContent($"Ocorreu um erro durante a interação!")
                        .AsEphemeral(true)
                );
            }
        }

        public void Register(DiscordClient client) => client.ComponentInteractionCreated += Event;

        public static void Ticket(string Id, ComponentInteractionCreateEventArgs args)
        {
            var category = args.Guild.Channels.FirstOrDefault(ch => ch.Key == 942470309376835674).Value;
            if (category != null)
            {
                var newChannel = args.Guild.CreateChannelAsync("Novo Canal", ChannelType.Text, category);
            }
        }
    }
}