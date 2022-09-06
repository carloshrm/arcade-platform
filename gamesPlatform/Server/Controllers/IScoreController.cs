using gamesPlatform.Shared;
using Microsoft.AspNetCore.Mvc;

namespace gamesPlatform.Server.Controllers
{
    public interface IScoreController
    {
        Task<ActionResult<IEnumerable<Score>>> dbAddScore(Score s);
        Task<ActionResult<IEnumerable<Score>>> dbGetLeaderboard(int appID);
        Task<ActionResult<Score>> dbGetScore(int scoreID);
    }
}