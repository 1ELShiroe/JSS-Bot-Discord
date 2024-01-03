using JSS.BOT.Interfaces.Events;
using DSharpPlus.EventArgs;
using DSharpPlus;

namespace JSS.BOT.Events.Guild
{
    public class MemberAdd : IEventBase<GuildMemberAddEventArgs>
    {
        public void Register(DiscordClient client) => client.GuildMemberAdded += Event;

        public Task Event(DiscordClient sender, GuildMemberAddEventArgs args)
        {
            Console.WriteLine("Novo membro: " + args.Member.ToString());
            return Task.CompletedTask;
        }
    }
}
