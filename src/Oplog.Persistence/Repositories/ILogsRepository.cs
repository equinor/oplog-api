using Oplog.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oplog.Persistence.Repositories
{
    public interface ILogsRepository
    {
        Task<Log> Get(int Id);
        Task Insert(Log log);
        void Update(Log log);
        Task<List<LogsView>> GetAll();
        Task<List<LogsView>> GetLogsBydate(DateTime fromDate, DateTime toDate);
        Task<List<LogsView>> GetFilteredLogs(int[] logTypeIds, int[] areaIds, int[] subTypeIds, int[] unitIds, string searchText, bool? isCritical, DateTime fromDate, DateTime toDate);
        Task Save();
    }
}
