using gamesPlatform.Shared;
using Microsoft.AspNetCore.Mvc;

namespace gamesPlatform.Server.Controllers
{
    public interface IScoreController
    {
        public Task<ActionResult<IEnumerable<Score>>> dbAddScore(Score s);
        public Task<ActionResult<IEnumerable<Score>>> dbGetLeaderboard(int appID);
        public Task<ActionResult<Score>> dbGetScore(int scoreID);
    }
}