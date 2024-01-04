using JSS.BOT.Interfaces.Events;
using DSharpPlus.EventArgs;
using DSharpPlus;
using DSharpPlus.Entities;

namespace JSS.BOT.Events.Guild
{
    public class MemberAdd : IEventBase<GuildMemberAddEventArgs>
    {
        public void Register(DiscordClient client) => client.GuildMemberAdded += Event;

        public async Task Event(DiscordClient sender, GuildMemberAddEventArgs args)
        {
            var role = args.Guild.Roles.FirstOrDefault(x => x.Key == 887338208520699985).Value;
            await args.Member.GrantRoleAsync(role);

            Console.WriteLine("Novo membro: " + args.Member.ToString());

            var channel = args.Guild.Channels.FirstOrDefault(ch => ch.Key == 1192235495619567718).Value;
            
            await channel.SendMessageAsync(new DiscordEmbedBuilder
            {
                Description = $"Novo membro em nossa comunidade! \n\n {args.Member}",
                Color = new DiscordColor("#363636"),
            });
        }
    }
}
