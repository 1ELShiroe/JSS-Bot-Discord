namespace JSS.BOT.SlashCommands
{
    public class SlashCommandException(string businessMessage) : Exception(businessMessage)
    {
    }
}
