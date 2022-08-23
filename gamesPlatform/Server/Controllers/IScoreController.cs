using gamesPlatform.Shared;
using Microsoft.AspNetCore.Mvc;

namespace gamesPlatform.Server.Controllers
{
    public interface IScoreController
    {
        public Task<ActionResult<List<Score>>> dbAddScore(Score s);
        public Task<ActionResult<List<Score>>> dbGetLeaderboard(int appID);
        public Task<ActionResult<Score>> dbGetScore(int scoreID);
    }
}