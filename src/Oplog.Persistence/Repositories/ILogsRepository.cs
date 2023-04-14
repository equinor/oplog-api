using Oplog.Persistence.Models;
using System.Threading.Tasks;

namespace Oplog.Persistence.Repositories
{
    public interface ILogsRepository
    {
        Task Insert(Log log);
        void Update(Log log);
        Task Save();
    }
}
