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
        [Cooldown(5, 10, CooldownBucketType.User)]
        [RequirePermissions(Permissions.ManageMessages)]
        public static async Task Add(InteractionContext ctx,
                [Option("icon", "Emoji to get information about")] string icon,
                [Option("name", "Name to assign to the emoji")] string name)
        {
            var match = GetEmojiId().Match(icon);

            if (!match.Success)
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

            var emojiId = match.Value.Replace(">", null);

            var isGif = icon.Contains("<a:") ? ".gif?size=128&quality=lossless" : ".webp?size=128&quality=lossless";
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

        [GeneratedRegex(@"[^:]+$")]
        private static partial Regex GetEmojiId();
    }
}
