using Microsoft.EntityFrameworkCore;
using Oplog.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Persistence.Repositories
{
    public class OperationAreasRepository : IOperationsAreasRepository
    {
        public readonly OplogDbContext _dbContext;
        public OperationAreasRepository(OplogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<OperationArea>> GetAllAreas()
        {
            return await _dbContext.OperationAreas.ToListAsync();
        }
    }
}
