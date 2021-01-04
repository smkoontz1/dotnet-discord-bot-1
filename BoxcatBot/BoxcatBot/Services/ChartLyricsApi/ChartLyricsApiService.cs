using BoxcatBot.Services.ChartLyricsApi.Models;
using BoxcatBot.Services.ChartLyricsApi.Responses;
using RestSharp;
using System.Threading.Tasks;

namespace BoxcatBot.Services.ChartLyricsApi
{
    public class ChartLyricsApiService
    {
        private const string CHART_LYRICS_BASE_URL = "http://api.chartlyrics.com/apiv1.asmx";

        private readonly RestClient _restClient;

        public ChartLyricsApiService()
        {
            _restClient = new RestClient(CHART_LYRICS_BASE_URL);
        }

        public async Task<SearchLyricResult> SearchChartLyricsAsync(string lyrics)
        {
            var request = new RestRequest("SearchLyricText", Method.GET)
                .AddParameter("lyricText", lyrics);

            var response = await _restClient.ExecuteAsync<SearchLyricTextResponse>(request);

            return new SearchLyricResult
            {
                Artist = response.Data.Artist,
                Song = response.Data.Song,
            };
        }
    }
}
