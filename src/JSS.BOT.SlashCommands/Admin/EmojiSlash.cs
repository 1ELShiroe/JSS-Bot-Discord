using DSharpPlus.CommandsNext.Attributes;
using System.Text.RegularExpressions;
using DSharpPlus.SlashCommands;
using DSharpPlus.Entities;
using DSharpPlus;
using Flurl.Http;

namespace JSS.BOT.SlashCommands.Admin
{
    [SlashCommandGroup("Emoji", "description")]
    public partial class IconSlash : ApplicationCommandModule
    {
        [SlashCommand("add", "Get information about the emoji")]
        [RequirePermissions(Permissions.ManageEmojis)]
        public static async Task Add(InteractionContext ctx,
                [Option("emoji", "Emoji to get information about")] string emoji,
                [Option("name", "Name to assign to the emoji")] string name)
        {
            try
            {
                var emojiId = ExtractEmojiId(emoji);

                if (emojiId == ulong.MinValue)
                {
                    await ctx.CreateResponseAsync(
                        InteractionResponseType.ChannelMessageWithSource,
                        new DiscordInteractionResponseBuilder()
                            .AddEmbed(new DiscordEmbedBuilder
                            {
                                Description = $"Emoji invalido!",
                                Color = new DiscordColor("#363636"),
                            })
                            .AsEphemeral(false)
                    );

                    return;
                }

                var isGif = emoji.Contains("<a:") ? ".gif?size=128&quality=lossless" : ".webp?size=128&quality=lossless";
                var url = $"https://cdn.discordapp.com/emojis/{emojiId}{isGif}";

                byte[] imageData = await url.GetBytesAsync();
                using MemoryStream stream = new(imageData);
                stream.Seek(0, SeekOrigin.Begin);

                var newIcon = await ctx.Guild.CreateEmojiAsync(name, stream);

                await ctx.CreateResponseAsync(
                    InteractionResponseType.ChannelMessageWithSource,
                    new DiscordInteractionResponseBuilder()
                        .AddEmbed(new DiscordEmbedBuilder
                        {
                            Description = $"Novo emoji criado com sucesso! {newIcon}",
                            Color = new DiscordColor("#363636"),
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


        [SlashCommand("remove", "Remove an emoji from the server")]
        [Cooldown(5, 10, CooldownBucketType.User)]
        [RequirePermissions(Permissions.ManageEmojis)]
        public static async Task Remove(InteractionContext ctx,
        [Option("emoji", "Emoji to be removed")] string emoji)
        {
            try
            {
                var emojiId = ExtractEmojiId(emoji);

                if (emojiId == ulong.MinValue)
                {
                    await ctx.CreateResponseAsync(
                        InteractionResponseType.ChannelMessageWithSource,
                        new DiscordInteractionResponseBuilder()
                            .WithContent("Formatação do Emoji incorreta!")
                            .AsEphemeral(true)
                    );
                    return;
                }

                var guildEmoji = await ctx.Guild.GetEmojiAsync(emojiId);

                if (guildEmoji == null)
                {
                    await ctx.CreateResponseAsync(
                        InteractionResponseType.ChannelMessageWithSource,
                        new DiscordInteractionResponseBuilder()
                            .WithContent("Nenhum emoji encontrado em nosso servidor!")
                            .AsEphemeral(true)
                    );
                    return;
                }

                await ctx.Guild.DeleteEmojiAsync(guildEmoji);

                await ctx.CreateResponseAsync(
                    InteractionResponseType.ChannelMessageWithSource,
                    new DiscordInteractionResponseBuilder()
                        .AddEmbed(new DiscordEmbedBuilder
                        {
                            Description = $"Emoji ``{guildEmoji.Name}`` removido com sucesso!",
                            Color = new DiscordColor("#363636"),
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


        [GeneratedRegex(@"[^:]+$")]
        private static partial Regex GetEmojiId();
        private static ulong ExtractEmojiId(string emoji)
        {
            var match = GetEmojiId().Match(emoji);

            if (match.Success && ulong.TryParse(match.Value.Replace(">", null), out ulong emojiId))
            {
                return emojiId;
            }

            return ulong.MinValue;
        }
    }
}