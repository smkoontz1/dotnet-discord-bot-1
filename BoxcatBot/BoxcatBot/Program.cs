using DSharpPlus;
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

            discord.MessageCreated += async (e) =>
            {
                var channel = e.Channel;
                var messageContent = e.Message.Content.ToLower();

                if (messageContent.StartsWith("ping"))
                {
                    await e.Message.RespondAsync("pong!");
                }

            };

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
