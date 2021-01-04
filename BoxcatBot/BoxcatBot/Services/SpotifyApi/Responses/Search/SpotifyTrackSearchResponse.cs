using Newtonsoft.Json;
using System.Collections.Generic;

namespace BoxcatBot.Services.SpotifyApi.Responses.Search
{
    public class SpotifyTrackSearchResponse
    {
        [JsonProperty("tracks")]
        public Tracks Tracks { get; set; }
    }

    public class Tracks
    {
        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }
    }

    public class ExternalUrls
    {
        [JsonProperty("spotify")]
        public string Spotify { get; set; }
    }
}
