using cmArcade.Shared;
using System.Net.Http.Json;
using System.Text.Json;

namespace cmArcade.Client.Services
{
    public class ScoreService : IScoreService
    {
        private readonly HttpClient _httpClient;
        //private readonly ILocalStorageService _localStorage;

        public ScoreService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
            var scoreVal = new Score(appID);
            var localData = LocalStorageService.GetItem($"highScore_{(int)appID}");
            if (localData != null)
            {
                var parsedInfo = JsonSerializer.Deserialize<Score>(localData);
                if (parsedInfo != null)
                    scoreVal = parsedInfo;
            }
            return scoreVal;
        }

        public async Task setLocalScore(Score highScore)
        {
            LocalStorageService.SetItem($"highScore_{highScore.appID}", JsonSerializer.Serialize(highScore));
        }
    }
}
