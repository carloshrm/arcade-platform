using Blazored.LocalStorage;
using cmArcade.Shared;
using System.Net.Http.Json;

namespace cmArcade.Client.Services
{
    public class ScoreService : IScoreService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public ScoreService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<IEnumerable<Score>> getLeaderboard(AppID appID)
        {
            var callResponse = await _httpClient.GetFromJsonAsync<IEnumerable<Score>>($"api/score/leaderboard/{(int)appID}");
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

        public async Task<int> setScore(Score newScore)
        {
            var callResponse = await _httpClient.PostAsJsonAsync<Score>("api/score/setscore", newScore);
            int scoreID = int.Parse(await callResponse.Content.ReadAsStringAsync());
            newScore.id = scoreID;
            setLocalScore(newScore);
            return scoreID;
        }

        public async Task<Score> readLocalScore(AppID appID)
        {
            if (await _localStorage.ContainKeyAsync($"highScore_{(int)appID}"))
            {
                return await _localStorage.GetItemAsync<Score>($"highScore_{(int)appID}");
            }
            else
                return new Score(appID);
        }

        public async Task setLocalScore(Score highScore)
        {
            await _localStorage.SetItemAsync<Score>("highScore", highScore);
        }
    }
}
