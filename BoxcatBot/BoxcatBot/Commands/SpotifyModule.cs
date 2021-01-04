using BoxcatBot.Services.SpotifyApi;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace BoxcatBot.Commands
{
    public class SpotifyModule : BaseCommandModule
    {
        public SpotifyApiService Spotify { private get; set; }

        [Command("lyrics")]
        [Cooldown(5, 30, CooldownBucketType.User)]
        public async Task LyricsCommand(CommandContext context, [RemainingText] string lyrics)
        {
            var url = await Spotify.GetBestMatchSpotifyUrl(lyrics);

            await context.RespondAsync($"{context.User.Mention}, is this your song?\n{url}");
        }
    }
}
