﻿using Oplog.Persistence.Repositories;

namespace Oplog.Core.Queries
{
    public class LogsQueries : ILogsQueries
    {
        private readonly ILogsRepository _logsRepository;
        public LogsQueries(ILogsRepository logsRepository)
        {
            _logsRepository = logsRepository;
        }

        public async Task<List<GetAllLogsResult>> GetAllLogs()
        {
            var logs = await _logsRepository.GetAll();

            if (logs == null)
            {
                return null;
            }

            var result = new List<GetAllLogsResult>();
            foreach (var item in logs)
            {
                var log = new GetAllLogsResult
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
                    IsCritical = item.IsCritical
                };

                result.Add(log);
            }

            return result;
        }

        public async Task<List<GetLogsByDateResult>> GetLogsByDate(DateTime fromDate, DateTime toDate)
        {
            var logs = await _logsRepository.GetLogsBydate(fromDate, toDate);

            if (logs == null)
            {
                return null;
            }

            var result = new List<GetLogsByDateResult>();
            foreach (var item in logs)
            {
                var log = new GetLogsByDateResult
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
                    IsCritical = item.IsCritical
                };

                result.Add(log);
            }

            return result;
        }
    }
}
