using cmArcade.Shared;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;

namespace cmArcade.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScoreController : ControllerBase, IScoreController
    {
        private NpgsqlConnection connection { get; }
        private NpgsqlConnectionStringBuilder connectionString { get; }

        public ScoreController(IConfiguration config)
        {
            var envString = config.GetConnectionString("external_db");
            if (envString?.Equals(string.Empty) == false)
            {
                var dbURI = new Uri(envString);
                var dbUserInfo = dbURI.UserInfo.Split(":");
                connectionString = new NpgsqlConnectionStringBuilder
                {
                    Username = dbUserInfo.FirstOrDefault(),
                    Password = dbUserInfo.LastOrDefault(),
                    Host = dbURI.Host,
                    Port = dbURI.Port,
                    Database = dbURI.LocalPath.Substring(1),
                    //SslMode = SslMode.Require,
                    //TrustServerCertificate = true
                };
                connection = new NpgsqlConnection(connectionString.ToString());
            }
            else
                throw new ArgumentException("invalid url");
        }

        private async Task<T?> runQueryFirst<T>(string query, object vals)
        {
            if (connection.State is ConnectionState.Closed) connection.Open();
            try
            {
                var dbResponse = await connection.QueryFirstAsync<T>(query, vals);
                return dbResponse;
            }
            catch (Exception e)
            {
                return default;
            }
            finally
            {
                connection.Close();
            }
        }

        private async Task<IEnumerable<T>?> runQuery<T>(string query, object vals)
        {
            if (connection.State is ConnectionState.Closed) connection.Open();
            try
            {
                var dbResponse = await connection.QueryAsync<T>(query, vals);
                return dbResponse;
            }
            catch (Exception e)
            {
                return default;
            }
            finally
            {
                connection.Close();
            }
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
            var result = await runQueryFirst<int>(query, vals);
            return Ok(result);
        }

        [HttpGet("leaderboard/{appID}")]
        public async Task<ActionResult<IEnumerable<Score>>> dbGetLeaderboard(int appID)
        {
            string query = $"SELECT * FROM scores WHERE appid=@id;";
            var vals = new { id = appID };
            var result = await runQuery<Score>(query, vals);
            return Ok(result);
        }

        [HttpGet("{scoreID}")]
        public async Task<ActionResult<Score>> dbGetScore(int scoreID)
        {
            string query = $"SELECT * FROM scores WHERE id=@id;";
            var vals = new { id = scoreID };
            var results = await runQueryFirst<Score>(query, vals);
            return Ok(results);
        }
    }
}
