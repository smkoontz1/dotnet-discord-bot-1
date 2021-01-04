using System;

namespace BoxcatBot.Services.SpotifyApi.Models
{
    public class SpotifyAccessToken
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
