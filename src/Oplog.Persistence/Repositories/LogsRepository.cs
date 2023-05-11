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

        public async Task<List<Log>> GetAll()
        {
            return await _dbContext.Logs.ToListAsync();
        }

        public async Task<List<Log>> GetLogsBydate(DateTime fromDate, DateTime toDate)
        {
            return await _dbContext.Logs.Where(l => l.CreatedDate >= fromDate && l.CreatedDate <= toDate).OrderByDescending(l => l.CreatedDate).Take(1000).ToListAsync();
        }
    }
}
