using Dapper;
using Npgsql;
using System.Data;

namespace cmArcade.Server.Controllers
{
    public class QueryHelper : IQueryHelper
    {
        private NpgsqlConnection connection { get; }

        public QueryHelper(IConfiguration config)
        {
            var envString = config.GetValue<string>("external_db");
            if (envString?.Equals(string.Empty) == false)
                connection = new NpgsqlConnection(envString);
            else
            {
                //TODO - move connection string to appsettings
                throw new ArgumentException("invalid db string");
            }
        }

        public async Task<T?> runQueryFirst<T>(string query, object vals)
        {
            if (connection.State is ConnectionState.Closed) connection.Open();
            try
            {
                var dbResponse = await connection.QueryFirstAsync<T>(query, vals);
                return dbResponse;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return default;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<IEnumerable<T>?> runQuery<T>(string query, object vals)
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
    }
}
