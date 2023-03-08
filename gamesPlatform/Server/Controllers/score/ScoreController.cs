using cmArcade.Shared;

using Microsoft.AspNetCore.Mvc;

namespace cmArcade.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScoreController : ControllerBase, IScoreController
    {
        private IQueryHelper _queryHelper { get; }

        public ScoreController(IQueryHelper queryHelper)
        {
            _queryHelper = queryHelper;
        }

        [HttpPost("setscore")]
        public async Task<ActionResult<IEnumerable<Score>>> dbAddScore(Score s)
        {
            const string query = @"INSERT INTO 
                    scores (appid, scorevalue, runstart, runlength, nickname, turn) 
                    VALUES (@appid, @scorevalue, @runstart, @runlength, @nickname, @turn) RETURNING id;";

            var vals = new
            {
                appid = s.appID,
                scorevalue = s.scoreValue,
                runStart = s.runStart,
                runLength = s.runLength,
                nickname = s.nickname,
                turn = s.turn
            };
            var result = await _queryHelper.runQueryFirst<int>(query, vals);
            return Ok(result);
        }

        [HttpGet("leaderboard/{appID}")]
        public async Task<ActionResult<IEnumerable<Score>>> dbGetLeaderboard(int appID)
        {
            string query = $"SELECT * FROM scores WHERE appid=@id;";
            var vals = new { id = appID };
            var result = await _queryHelper.runQuery<Score>(query, vals);
            return Ok(result);
        }

        [HttpGet("{scoreID}")]
        public async Task<ActionResult<Score>> dbGetScore(int scoreID)
        {
            string query = $"SELECT * FROM scores WHERE id=@id;";
            var vals = new { id = scoreID };
            var results = await _queryHelper.runQueryFirst<Score>(query, vals);
            return Ok(results);
        }
    }
}
