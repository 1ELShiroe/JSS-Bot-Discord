using DSharpPlus.SlashCommands;
using System.Reflection;

namespace JSS.BOT.Modules
{
    public class SlashCommandModule
    {
        public static void Load(SlashCommandsExtension SlashCommands)
        {
            var slashsType =
                Assembly.Load("JSS.BOT.SlashCommands")
                    .GetTypes()
                    .Where(t => t.IsSubclassOf(typeof(ApplicationCommandModule)))
                    .ToList();

            Console.WriteLine($"SlashCommands Load: {slashsType.Count}");

            foreach (Type slash in slashsType)
            {
                SlashCommands.RegisterCommands(slash, 816679663568683079);
            }
        }
    }
}