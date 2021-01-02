using BoxcatBot.EventHandlers;
using DSharpPlus;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace BoxcatBot
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var discord = new DiscordClient(new DiscordConfiguration
            {
                Token = "",
                TokenType = TokenType.Bot,
            });

            discord.MessageCreated += MessageEventHandler.MessageCreatedHandler;

            await discord.ConnectAsync(new DiscordActivity("Use: ^boxcat", ActivityType.ListeningTo));
            await Task.Delay(-1);
        }
    }
}
