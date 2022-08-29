using gamesPlatform.Shared;
using System.Net.Http.Json;

namespace gamesPlatform.Client.Services
{
    public class ScoreService : IScoreService
    {
        private readonly HttpClient _httpClient;

        public ScoreService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Score>> getLeaderboard(int appID)
        {
            var callResponse = await _httpClient.GetFromJsonAsync<IEnumerable<Score>>($"api/score/leaderboard/{appID}");
            if (callResponse != null)
                return callResponse;
            else
                throw new HttpRequestException("Leaderboard data not found");
        }

        public async Task<Score> getScore(int scoreID)
        {
            var callResponse = await _httpClient.GetFromJsonAsync<Score>($"api/score/{scoreID}");
            if (callResponse != null)
                return callResponse;
            else
                throw new HttpRequestException("Score not found");
        }

        public async Task setScore(Score s)
        {
            var callResponse = await _httpClient.PostAsJsonAsync<Score>("api/score/setscore", s);
        }
    }
}
