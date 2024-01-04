using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.SlashCommands;
using DSharpPlus.Entities;
using DSharpPlus;

namespace JSS.BOT.SlashCommands.Admin
{
    public class ClearSlash : ApplicationCommandModule
    {
        [SlashCommand("clear", "Perform message cleaning")]
        [Cooldown(5, 10, CooldownBucketType.User)]
        [RequirePermissions(Permissions.ManageMessages)]
        public static async Task Command(InteractionContext ctx,
            [Option("quantity", "Number of messages to be deleted")] double quantity)
        {
            try
            {
                if (quantity > 100)
                {
                    var embedResponse = new DiscordInteractionResponseBuilder()
                        .AddEmbed(new DiscordEmbedBuilder
                        {
                            Description = "Você não pode deletar mais de 100 mensagens.",
                            Color = new DiscordColor("#363636"),
                        })
                        .AsEphemeral(true);

                    await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, embedResponse);
                    return;
                }

                var msgs = await ctx.Channel.GetMessagesAsync((int)quantity);
                var filteredMessages = msgs.Where(msg => DateTimeOffset.Now - msg.CreationTimestamp < TimeSpan.FromDays(14)).ToList();

                await ctx.Channel.DeleteMessagesAsync(filteredMessages);

                await ctx.CreateResponseAsync(
                    InteractionResponseType.ChannelMessageWithSource,
                    new DiscordInteractionResponseBuilder()
                        .AddEmbed(new DiscordEmbedBuilder
                        {
                            Description = $"Success! {filteredMessages.Count} mensagens deletadas.",
                            Color = new DiscordColor("#363636"),
                        })
                        .AsEphemeral(true)
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");

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
