using BoxcatBot.Commands;
using BoxcatBot.EventHandlers;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace BoxcatBot.Services.Common
{
    public class DiscordClientService
    {
        private readonly IConfiguration _configuration;

        private DiscordClient _discordClient;

        public DiscordClientService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task InitializeClientAsync(ServiceProvider serviceProvider)
        {
            var discordSettings = _configuration.GetSection("Secrets:Discord");

            var token = discordSettings["BotToken"];

            _discordClient = new DiscordClient(new DiscordConfiguration
            {
                Token = token,
                TokenType = TokenType.Bot
            });

            var commands = _discordClient.UseCommandsNext(new CommandsNextConfiguration()
            {

                StringPrefixes = new[] { "^" },
                Services = serviceProvider,
            });

            commands.RegisterCommands<MainModule>();
            commands.RegisterCommands<SpotifyModule>();

            _discordClient.MessageCreated += MessageEventHandler.MessageCreatedHandler;

            await _discordClient.ConnectAsync(new DiscordActivity("Use: ^boxcat", ActivityType.ListeningTo));
            await Task.Delay(-1);
        }
    }
}
