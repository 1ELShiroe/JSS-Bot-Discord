using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.SlashCommands;
using DSharpPlus.Entities;
using DSharpPlus;

namespace JSS.BOT.SlashCommands.Info
{
    [SlashCommandGroup("member", "description")]
    public class MemberInfo : ApplicationCommandModule
    {
        [SlashCommand("info", "Retrieve information about a member")]
        [Cooldown(5, 10, CooldownBucketType.User)]
        public static async Task InformationUser(InteractionContext ctx,
            [Option("member", "Mention the member to view information", false)] DiscordUser? member = null)
        {
            try
            {
                member ??= ctx.User;

                var roles = ctx.Guild
                        .GetMemberAsync(member.Id).Result.Roles
                        .Select(role => role.Name)
                        .ToList();

                await ctx.CreateResponseAsync(
                    InteractionResponseType.ChannelMessageWithSource,
                    new DiscordInteractionResponseBuilder()
                        .AddEmbed(new DiscordEmbedBuilder
                        {
                            Description =
                                $"**Name/ID:** {member.Username}/{member.Id}\n" +
                                $"**Status:** {member.Presence.Status}\n" +
                                $"**Flags:** {member.Flags}\n" +
                                $"**Cargos:** {string.Join(", ", roles)}\n",
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


        [SlashCommand("avatar", "Search member avatar")]
        [Cooldown(5, 10, CooldownBucketType.User)]
        public static async Task AvatarUser(InteractionContext ctx,
            [Option("member", "Mention the member you want to see the avatar", false)] DiscordUser? member = null)
        {
            try
            {
                var avatar = (member ?? ctx.User).AvatarUrl;

                await ctx.CreateResponseAsync(
                    InteractionResponseType.ChannelMessageWithSource,
                    new DiscordInteractionResponseBuilder()
                        .AddEmbed(new DiscordEmbedBuilder
                        {
                            Description = $"[Click here]({avatar}) to download the avatar",
                            Color = new DiscordColor("#363636"),
                            ImageUrl = avatar
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
