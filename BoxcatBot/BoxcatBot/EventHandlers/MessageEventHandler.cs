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

            if (messageContent.StartsWith("ping"))
            {
                await e.Message.RespondAsync("pong!");
            }
        }
    }
}
