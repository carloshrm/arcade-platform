using gamesPlatform.Shared;

namespace gamesPlatform.Client.Services
{
    public interface IScoreService
    {
        Task<Score> getScore(int scoreID);
        Task<List<Score>> setLeaderboards(int appID);
        Task setScore(Score s);
    }
}