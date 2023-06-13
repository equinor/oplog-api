﻿using Oplog.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oplog.Persistence.Repositories
{
    public interface ILogsRepository
    {
        Task<Log> Get(int Id);
        Task Insert(Log log);
        Task Delete(int id);
        void Update(Log log);
        Task<List<LogsView>> GetAll();
        Task<List<LogsView>> GetLogsBydate(DateTime fromDate, DateTime toDate);
        Task<List<LogsView>> GetFilteredLogs(int[] logTypeIds, int[] areaIds, int[] subTypeIds, int[] unitIds, string searchText, DateTime fromDate, DateTime toDate);
        Task Save();
    }
}
