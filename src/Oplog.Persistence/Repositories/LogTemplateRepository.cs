using Microsoft.EntityFrameworkCore;
using Oplog.Persistence.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oplog.Persistence.Repositories
{
    public class LogTemplateRepository : ILogTemplateRepository
    {
        public readonly OplogDbContext _dbContext;

        public LogTemplateRepository(OplogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Insert(LogTemplate template)
        {
            await _dbContext.LogTemplates.AddAsync(template);
        }

        public async Task<LogTemplate> GetById(int id)
        {
            return await _dbContext.LogTemplates.SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<LogTemplate>> GetAll()
        {
            return await _dbContext.LogTemplates.ToListAsync();
        }

        public async Task<List<LogTemplate>> GetByUser(string userName)
        {
            return await _dbContext.LogTemplates.Where(t => t.CreatedBy == userName).ToListAsync();
        }

        public async Task Delete(int id)
        {
            var logTemplate = await _dbContext.LogTemplates.SingleOrDefaultAsync(l => l.Id == id);

            if(logTemplate!=null)
            {
                _dbContext.LogTemplates.Remove(logTemplate);
            }
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
