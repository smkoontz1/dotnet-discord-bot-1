using BoxcatBot.Services.ChartLyricsApi;
using BoxcatBot.Services.SpotifyApi.Models;
using BoxcatBot.Services.SpotifyApi.Responses.Search;
using BoxcatBot.Services.SpotifyApi.Responses.Token;
using Microsoft.Extensions.Configuration;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.NewtonsoftJson;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BoxcatBot.Services.SpotifyApi
{
    public class SpotifyApiService
    {
        private const string SPOTIFY_API_BASE_URL = "https://api.spotify.com/v1";
        private const string SPOTIFY_API_ACCOUNTS_BASE_URL = "https://accounts.spotify.com/api";

        private readonly IConfigurationSection _configurationSection;
        private readonly RestClient _restClient;
        private readonly ChartLyricsApiService _chartLyricsApiServce;

        private SpotifyAccessToken _accessToken = new SpotifyAccessToken();

        public SpotifyApiService(
            IConfiguration configuration,
            ChartLyricsApiService chartLyricsApiService)
        {
            _chartLyricsApiServce = chartLyricsApiService;
            _configurationSection = configuration.GetSection("Settings:Spotify");
            
            _restClient = new RestClient();
            _restClient.UseNewtonsoftJson();
        }

        public async Task<string> GetBestMatchSpotifyUrl(string lyrics)
        {
            var searchLyricResult = await _chartLyricsApiServce.SearchChartLyricsAsync(lyrics);
            var spotifyTrackUrl = await SearchSpotiyTracksAsync(searchLyricResult.Artist, searchLyricResult.Song);

            return spotifyTrackUrl;
        }

        private async Task RefreshAccessTokenAsync()
        {
            _restClient.BaseUrl = new Uri(SPOTIFY_API_ACCOUNTS_BASE_URL);
            _restClient.Authenticator = null;

            var base64ClientId = _configurationSection["Base64ClientId"];

            var encodedBody = "grant_type=client_credentials";

            var request = new RestRequest("token", Method.POST)
                .AddHeader("Authorization", $"Basic {base64ClientId}")
                .AddHeader("Content-Type", "application/x-www-form-urlencoded")
                .AddParameter("application/x-www-form-urlencoded", encodedBody, ParameterType.RequestBody);

            var response = await _restClient.ExecuteAsync<SpotifyTokenResponse>(request);

            _accessToken.Token = response.Data.AccessToken;
            _accessToken.ExpiresAt = DateTime.Now.AddSeconds(response.Data.ExpiresIn);
        }

        private async Task<string> SearchSpotiyTracksAsync(string artist, string track)
        {
            if (!TokenValid())
            {
                await RefreshAccessTokenAsync();
            }

            _restClient.BaseUrl = new Uri(SPOTIFY_API_BASE_URL);
            _restClient.Authenticator = new JwtAuthenticator(_accessToken.Token);

            var cleanArtist = PrepareStringForSearch(artist);
            var cleanTrack = PrepareStringForSearch(track);

            var request = new RestRequest("search", Method.GET)
                .AddParameter("q", $"track:{cleanTrack} artist:{cleanArtist}")
                .AddParameter("type", "track");

            var response = await _restClient.ExecuteAsync<SpotifyTrackSearchResponse>(request);

            var matchedTrack = response.Data.Tracks.Items.FirstOrDefault();

            return matchedTrack.ExternalUrls.Spotify;
        }

        private bool TokenValid()
        {
            return !string.IsNullOrWhiteSpace(_accessToken.Token) && _accessToken.ExpiresAt > DateTime.Now;
        }

        private string PrepareStringForSearch(string str)
        {
            return str
                .Replace("\'", string.Empty)
                .Replace("\"", string.Empty);
        }
    }
}
