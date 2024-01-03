using DSharpPlus;
using DSharpPlus.EventArgs;
using JSS.BOT.Interfaces.Events;

namespace JSS.BOT.Events.Client
{
    public class Ready : IEventBase<ReadyEventArgs>
    {
        public void Register(DiscordClient client) => client.Ready += Event;

        public Task Event(DiscordClient sender, ReadyEventArgs args)
        {
            Console.WriteLine("Passei aqui com sucesso!");
            return Task.CompletedTask;
        }
    }
}