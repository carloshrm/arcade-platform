using cmArcade.Shared;

namespace cmArcade.Client.Services
{
    public interface IScoreService
    {
        public Task<Score> getScore(int scoreID);
        public Task<IEnumerable<Score>> getLeaderboard(AppID appID);
        public Task<int> setScore(Score newScore);
        public Task<Score> readLocalScore(AppID appID);
        public Task setLocalScore(Score highScore);

    }
}