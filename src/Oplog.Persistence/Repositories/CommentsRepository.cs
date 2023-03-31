using Oplog.Persistence.Models;
using System.Threading.Tasks;

namespace Oplog.Persistence.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        public readonly OplogDbContext _dbContext;
        public CommentsRepository(OplogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Insert(Comment comment)
        {
            await _dbContext.Comments.AddAsync(comment);
        }

        public void Update(Comment comment)
        {
            _dbContext.Comments.Update(comment);
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
