using DSharpPlus;
using DSharpPlus.EventArgs;
using System.Threading.Tasks;

namespace BoxcatBot.EventHandlers
{
    public static class MessageEventHandler
    {
        public static async Task MessageCreatedHandler(DiscordClient s, MessageCreateEventArgs e)
        {
            var messageContent = e.Message.Content.ToLower();


            // DON'T DO IT THIS WAY Use MainModule.cs to do commands
            // Use this message handler to do other things based on messages.
            if (messageContent.StartsWith("ping"))
            {
                await e.Message.RespondAsync("pong!");
            }
        }
    }
}
