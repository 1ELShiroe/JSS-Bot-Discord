using DSharpPlus;

namespace JSS.BOT.Interfaces.Events
{
    public interface IEventBase<T>
    {
        Task Event(DiscordClient sender, T args);
        void Register(DiscordClient client);
    }
}