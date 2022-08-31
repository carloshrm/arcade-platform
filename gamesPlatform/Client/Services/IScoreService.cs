using gamesPlatform.Shared;

namespace gamesPlatform.Client.Services
{
    public interface IScoreService
    {
        public Task<Score> getScore(int scoreID);
        public Task<IEnumerable<Score>> getLeaderboard(int appID);
        public Task<int> setScore(Score s);
    }
}