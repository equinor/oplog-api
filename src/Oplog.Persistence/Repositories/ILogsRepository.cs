using Oplog.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oplog.Persistence.Repositories
{
    public interface ILogsRepository
    {
        Task<Log> Get(int Id);
        Task Insert(Log log);
        void Update(Log log);
        Task<List<Log>> GetAll();
        Task<List<Log>> GetLogsBydate(DateTime fromDate, DateTime toDate);
        Task Save();
    }
}
