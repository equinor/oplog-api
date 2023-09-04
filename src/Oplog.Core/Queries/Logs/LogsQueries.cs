using Oplog.Core.AzureSearch;
using Oplog.Persistence.Repositories;

namespace Oplog.Core.Queries
{
    public class LogsQueries : ILogsQueries
    {
        private readonly ILogsRepository _logsRepository;
        public LogsQueries(ILogsRepository logsRepository)
        {
            _logsRepository = logsRepository;
        }

        public async Task<List<LogsResult>> GetAllLogs()
        {
            var logs = await _logsRepository.GetAll();

            if (logs == null)
            {
                return null;
            }

            var result = new List<LogsResult>();
            foreach (var item in logs)
            {
                var log = new LogsResult
                {
                    Id = item.Id,
                    LogTypeId = item.LogTypeId,
                    ParentId = item.ParentId,
                    LastChangeUserId = item.LastChangeUserId,
                    LastChangeDateTime = item.LastChangeDateTime,
                    UpdatedBy = item.UpdatedBy,
                    UpdatedDate = item.UpdatedDate,
                    CreatedById = item.CreatedById,
                    Author = item.Author,
                    ScheduleItemState = item.ScheduleItemState,
                    CreatedBy = item.CreatedBy,
                    CreatedDate = item.CreatedDate,
                    Text = item.Text,
                    OperationAreaId = item.OperationAreaId,
                    EffectiveTime = item.EffectiveTime,
                    Unit = item.Unit,
                    Subtype = item.Subtype,
                    IsCritical = item.IsCritical,
                    LogTypeName = item.LogTypeName,
                    SubTypeName = item.SubTypeName,
                    UnitName = item.UnitName,
                    AreaName = item.AreaName,
                };

                result.Add(log);
            }

            return result;
        }

        public async Task<List<LogsResult>> GetLogsByDate(DateTime fromDate, DateTime toDate)
        {
            var logs = await _logsRepository.GetLogsBydate(fromDate, toDate);

            if (logs == null)
            {
                return null;
            }

            var result = new List<LogsResult>();
            foreach (var item in logs)
            {
                var log = new LogsResult
                {
                    Id = item.Id,
                    LogTypeId = item.LogTypeId,
                    ParentId = item.ParentId,
                    LastChangeUserId = item.LastChangeUserId,
                    LastChangeDateTime = item.LastChangeDateTime,
                    UpdatedBy = item.UpdatedBy,
                    UpdatedDate = item.UpdatedDate,
                    CreatedById = item.CreatedById,
                    Author = item.Author,
                    ScheduleItemState = item.ScheduleItemState,
                    CreatedBy = item.CreatedBy,
                    CreatedDate = item.CreatedDate,
                    Text = item.Text,
                    OperationAreaId = item.OperationAreaId,
                    EffectiveTime = item.EffectiveTime,
                    Unit = item.Unit,
                    Subtype = item.Subtype,
                    IsCritical = item.IsCritical,
                    LogTypeName = item.LogTypeName,
                    SubTypeName = item.SubTypeName,
                    UnitName = item.UnitName,
                    AreaName = item.AreaName,
                };

                result.Add(log);
            }

            return result;
        }

        public async Task<List<LogsResult>> GetFilteredLogs(LogsFilter filter)
        {
            var logs = await _logsRepository.GetFilteredLogs(filter.LogTypeIds, filter.AreaIds, filter.SubTypeIds, filter.UnitIds, filter.SearchText, filter.FromDate, filter.ToDate, filter.SortField, filter.SortDirection);

            if (logs == null)
            {
                return null;
            }

            var result = new List<LogsResult>();
            foreach (var item in logs)
            {
                var log = new LogsResult
                {
                    Id = item.Id,
                    LogTypeId = item.LogTypeId,
                    ParentId = item.ParentId,
                    LastChangeUserId = item.LastChangeUserId,
                    LastChangeDateTime = item.LastChangeDateTime,
                    UpdatedBy = item.UpdatedBy,
                    UpdatedDate = item.UpdatedDate,
                    CreatedById = item.CreatedById,
                    Author = item.Author,
                    ScheduleItemState = item.ScheduleItemState,
                    CreatedBy = item.CreatedBy,
                    CreatedDate = item.CreatedDate,
                    Text = item.Text,
                    OperationAreaId = item.OperationAreaId,
                    EffectiveTime = item.EffectiveTime,
                    Unit = item.Unit,
                    Subtype = item.Subtype,
                    IsCritical = item.IsCritical,
                    LogTypeName = item.LogTypeName,
                    SubTypeName = item.SubTypeName,
                    UnitName = item.UnitName,
                    AreaName = item.AreaName,
                };

                result.Add(log);
            }

            return result;
        }
    }
}
