using Oplog.Persistence.Models;
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
    }
}
