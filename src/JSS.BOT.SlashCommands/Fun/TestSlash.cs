
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.SlashCommands;

namespace JSS.BOT.SlashCommands.Fun
{
    public class TestSlash : ApplicationCommandModule
    {
        [SlashCommand("aasdasdaa", "AQUI È O PRIMEIRO SLASH")]
        [Cooldown(5, 10, CooldownBucketType.User)]
        public static async Task Command(InteractionContext ctx)
        {
            await ctx.Channel.SendMessageAsync(content: "OIE AQUI EU, " + ctx.User.Username);
            await ctx.Interaction.DeferAsync(true);
        }
    }
}