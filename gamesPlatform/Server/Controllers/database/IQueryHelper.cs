namespace cmArcade.Server.Controllers
{
    public interface IQueryHelper
    {
        Task<IEnumerable<T>?> runQuery<T>(string query, object vals);
        Task<T?> runQueryFirst<T>(string query, object vals);
    }
}