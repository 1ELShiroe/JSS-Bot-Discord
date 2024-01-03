using JSS.BOT.Interfaces.Events;
using System.Reflection;
using DSharpPlus;

namespace JSS.BOT.Modules
{
    public class EventBaseModule
    {
        public static void Load(DiscordClient Client)
        {
            var types = Assembly.Load("JSS.BOT.Events")
                    .GetTypes()
                    .Where(p => p
                            .GetInterfaces()
                            .Any(i => i.IsGenericType && i
                            .GetGenericTypeDefinition() == typeof(IEventBase<>)) && !p.IsInterface)
                    .ToList();

            Console.WriteLine($"Events Load: {types.Count}");

            foreach (var type in types)
            {
                try
                {
                    var instance = Activator.CreateInstance(type);
                    if (instance != null)
                    {
                        var method = type.GetMethod("Register");
                        method?.Invoke(instance, [Client]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating instance of type {type.Name}: {ex.Message}");
                }
            }
        }
    }
}