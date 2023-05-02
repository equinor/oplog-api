namespace Oplog.Core.Queries
{
    public interface ILogsQueries
    {
        Task<List<GetAllLogsResult>> GetAllLogs();
        Task<List<GetLogsByDateResult>> GetLogsByDate(DateTime fromDate, DateTime toDate);

    }
}
