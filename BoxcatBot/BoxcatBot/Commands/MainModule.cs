using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace BoxcatBot.Commands
{
    public class MainModule : BaseCommandModule
    {
        [Command("greet")]
        public async Task GreetCommand(CommandContext context)
        {
            await context.RespondAsync("Greetings!");
        }
    }
}
