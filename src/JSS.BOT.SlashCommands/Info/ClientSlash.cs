using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.SlashCommands;
using DSharpPlus.Entities;
using DSharpPlus;

namespace JSS.BOT.SlashCommands.Info
{
    [SlashCommandGroup("bot", "Commands related to the bot")]
    public class ClientSlash : ApplicationCommandModule
    {
        // [SlashCommand("info", "Retrieve information about the bot")]
        // [Cooldown(5, 10, CooldownBucketType.User)]
        // public static void Info(InteractionContext ctx)
        // {
        //     // Seu código para o comando "info"
        // }

        [SlashCommand("ping", "Check the bot's ping")]
        [Cooldown(5, 10, CooldownBucketType.User)]
        public static async Task Ping(InteractionContext ctx)
        {
            try
            {
                await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

                var followUpMessage = await ctx.FollowUpAsync(
                    new DiscordFollowupMessageBuilder()
                        .AddEmbed(new DiscordEmbedBuilder
                        {
                            Description = $"Calculando latência....",
                            Color = new DiscordColor("#363636"),
                        })
                        .AsEphemeral(false)
                );

                var ping = Math.Max(0, (DateTimeOffset.Now - ctx.Interaction.CreationTimestamp).TotalMilliseconds);
                var formattedLatency = $"{ping:F2}ms";

                await ctx.EditFollowupAsync(
                    followUpMessage.Id,
                    new DiscordWebhookBuilder()
                            .AddEmbed(new DiscordEmbedBuilder
                            {
                                Description = $"🏓 Pong! \n\n  Latência: **{formattedLatency}**\n  API: **{ctx.Client.Ping}ms**",
                                Color = new DiscordColor("#363636"),
                            })
                        );
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
