using Microsoft.EntityFrameworkCore;
using Oplog.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Persistence.Repositories
{
    public class UserDefinedFilterRepository : IUserDefinedFilterRepository
    {
        public readonly OplogDbContext _dbContext;
        public UserDefinedFilterRepository(OplogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Insert(UserDefinedFilter filter)
        {
            await _dbContext.UserDefinedFilters.AddAsync(filter);
        }

        public async Task<List<UserDefinedFilter>> GetByCreatedUser(string createdBy)
        {
            return await _dbContext.UserDefinedFilters.Where(u => u.CreatedBy == createdBy).Include(u => u.UserDefinedFilterItems).ToListAsync();
        }

        public async Task Delete(int id)
        {
            var filter = await _dbContext.UserDefinedFilters.Where(u=>u.Id ==id).FirstOrDefaultAsync();

            if (filter != null)
            {
                _dbContext.UserDefinedFilters.Remove(filter);
            }
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
