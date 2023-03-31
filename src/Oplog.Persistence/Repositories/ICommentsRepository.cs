using Oplog.Persistence.Models;
using System.Threading.Tasks;

namespace Oplog.Persistence.Repositories
{
    public interface ICommentsRepository
    {
        Task Insert(Comment comment);
        void Update(Comment comment);
        Task Save();
    }
}
