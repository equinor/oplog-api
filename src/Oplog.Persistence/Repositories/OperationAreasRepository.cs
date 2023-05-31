﻿using Microsoft.EntityFrameworkCore;
using Oplog.Persistence.Models;
using System.Collections.Generic;
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

        public async Task<OperationArea> Get(int id)
        {
            return await _dbContext.OperationAreas.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<OperationArea>> GetAllAreas()
        {
            return await _dbContext.OperationAreas.ToListAsync();
        }
    }
}
