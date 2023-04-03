using Microsoft.EntityFrameworkCore;
using Oplog.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Persistence.Repositories
{
    public class ConfiguredTypesRepository : IConfiguredTypesRepository
    {
        public readonly OplogDbContext _dbContext;

        public ConfiguredTypesRepository(OplogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //TODO: Use cancellation tokens
        public async Task<List<ConfiguredType>> GetByCategory(int categoryId)
        {
            return await _dbContext.ConfiguredTypes.Where(c => c.CategoryId == categoryId).ToListAsync();
        }
    }
}
