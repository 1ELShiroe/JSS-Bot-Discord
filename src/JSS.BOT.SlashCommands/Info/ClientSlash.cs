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

        // [SlashCommand("ping", "Check the bot's ping")]
        // [Cooldown(5, 10, CooldownBucketType.User)]
        // public static async Task Ping(InteractionContext ctx)
        // {
        //     Console.WriteLine("AQUI");

        //     var followUpMessage = await ctx.FollowUpAsync(
        //         new DiscordFollowupMessageBuilder()
        //             .AddEmbed(new DiscordEmbedBuilder
        //             {
        //                 Description = $"Calculando latência....",
        //                 Color = new DiscordColor("#363636"),
        //             })
        //             .AsEphemeral(false)
        //     );

        //     var ping = followUpMessage.CreationTimestamp - ctx.Interaction.CreationTimestamp;
        //     await followUpMessage.ModifyAsync($"🏓 Pong! \n\n  Latência: **{ping}**\n  API: **{ctx.Client.Ping}**");

        // }
    }
}
