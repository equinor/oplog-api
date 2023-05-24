using Microsoft.EntityFrameworkCore;
using Oplog.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oplog.Persistence.Repositories
{
    public class LogsRepository : ILogsRepository
    {
        public readonly OplogDbContext _dbContext;
        public LogsRepository(OplogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Log> Get(int Id)
        {
            return await _dbContext.Logs.SingleOrDefaultAsync(l => l.Id == Id);
        }
        public async Task Insert(Log log)
        {
            await _dbContext.Logs.AddAsync(log);
        }

        public void Update(Log log)
        {
            _dbContext.Logs.Update(log);
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<LogsView>> GetAll()
        {
            return await _dbContext.LogsView.ToListAsync();
        }

        public async Task<List<LogsView>> GetLogsBydate(DateTime fromDate, DateTime toDate)
        {
            return await _dbContext.LogsView.Where(l => l.CreatedDate >= fromDate && l.CreatedDate <= toDate).OrderByDescending(l => l.CreatedDate).Take(1000).ToListAsync();
        }

        public async Task<List<LogsView>> GetFilteredLogs(int[] logTypeIds, int[] areaIds, int[] subTypeIds, int[] unitIds, string searchText, DateTime fromDate, DateTime toDate)
        {
            return await _dbContext.LogsView
                       .Where(l => l.CreatedDate >= fromDate && l.CreatedDate <= toDate
                       && (logTypeIds.Contains(l.LogTypeId.Value) || logTypeIds == null)
                       && (areaIds.Contains(l.OperationAreaId.Value) || areaIds == null)
                       && (subTypeIds.Contains(l.Subtype.Value) || subTypeIds == null)
                       && (unitIds.Contains(l.Unit.Value) || unitIds == null)
                       && (logTypeIds.Contains(l.LogTypeId.Value) || logTypeIds == null)
                       && (l.Text.Contains(searchText) || string.IsNullOrWhiteSpace(searchText)))
                       .OrderByDescending(l => l.CreatedDate).ToListAsync();
        }

    }
}
