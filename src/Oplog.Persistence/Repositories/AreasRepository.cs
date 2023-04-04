using Microsoft.EntityFrameworkCore;
using Oplog.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Persistence.Repositories
{
    public class AreasRepository : IAreasRepository
    {
        public readonly OplogDbContext _dbContext;
        public AreasRepository(OplogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Area>> GetAllAreas()
        {
            return await _dbContext.Areas.ToListAsync();
        }
    }
}
