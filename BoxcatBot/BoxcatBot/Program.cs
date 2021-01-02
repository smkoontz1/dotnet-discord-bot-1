using BoxcatBot.Commands;
using BoxcatBot.EventHandlers;
using DSharpPlus;
using DSharpPlus.CommandsNext;
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
                Token = "NjU4OTA4ODYxMTc1OTU1NDY3.XgGmsg._cuElYzxa0TsT1yXoXxOaGGZK4A",
                TokenType = TokenType.Bot,
            });

            var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { "^" }
            });

            commands.RegisterCommands<MainModule>();

            discord.MessageCreated += MessageEventHandler.MessageCreatedHandler;

            await discord.ConnectAsync(new DiscordActivity("Use: ^boxcat", ActivityType.ListeningTo));
            await Task.Delay(-1);
        }
    }
}
