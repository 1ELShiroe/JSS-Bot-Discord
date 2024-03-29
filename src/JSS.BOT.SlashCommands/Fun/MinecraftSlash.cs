using DSharpPlus;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace JSS.BOT.SlashCommands.Fun
{
    [SlashCommandGroup("minecraft", "description")]
    public class MinecraftSlash : ApplicationCommandModule
    {
        [SlashCommand("bust", "View the bust of Minecraft players.")]
        [Cooldown(5, 10, CooldownBucketType.User)]
        public static async Task Bust(InteractionContext ctx,
            [Option("nick", "Player nickname")] string nickname)
        {
            try
            {

                string url = $"https://minotar.net/bust/{nickname}/500.png";

                await ctx.CreateResponseAsync(
                    InteractionResponseType.ChannelMessageWithSource,
                    new DiscordInteractionResponseBuilder()
                        .AddEmbed(new DiscordEmbedBuilder
                        {
                            Description = $"**Nick:** {nickname} \n\n [Click Aqui]({url})",
                            Color = new DiscordColor("#363636"),
                            ImageUrl = url
                        })
                        .AsEphemeral(false)
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


        [SlashCommand("head", "View the head of Minecraft players.")]
        [Cooldown(5, 10, CooldownBucketType.User)]
        public static async Task Head(InteractionContext ctx,
            [Option("nick", "Player nickname")] string nickname)
        {
            try
            {
                await ctx.CreateResponseAsync(
                    InteractionResponseType.ChannelMessageWithSource,
                    new DiscordInteractionResponseBuilder()
                        .AddEmbed(new DiscordEmbedBuilder
                        {
                            Description = $"Success! mensagens deletadas.",
                            Color = new DiscordColor("#363636"),
                            ImageUrl = $"https://mc-heads.net/head/{nickname}/500"
                        })
                        .AsEphemeral(false)
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