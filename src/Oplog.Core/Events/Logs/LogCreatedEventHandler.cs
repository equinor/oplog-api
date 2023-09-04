using Oplog.Core.AzureSearch;
using Oplog.Core.Infrastructure;
using Oplog.Persistence.Repositories;

namespace Oplog.Core.Events.Logs
{
    public class LogCreatedEventHandler : IEventHandler<LogCreatedEvent>
    {
        private readonly ILogsRepository _logsRepository;
        private readonly IIndexDocumentClient _documentClient;
        public LogCreatedEventHandler(ILogsRepository logsRepository, IIndexDocumentClient documentClient)
        {
            _logsRepository = logsRepository;
            _documentClient = documentClient;
        }
        public async Task Handle(LogCreatedEvent @event)
        {
            var log = await _logsRepository.GetDetailedLogById(@event.LogId);
            if (log != null)
            {
                await _documentClient.Create(new LogDocument
                {
                    Id = log.Id.ToString(),
                    LogTypeId = log.LogTypeId,
                    UpdatedBy = log.UpdatedBy,
                    UpdatedDate = log.UpdatedDate,
                    CreatedBy = log.CreatedBy,
                    Author = log.Author,
                    CreatedDate = log.CreatedDate,
                    Text = log.Text,
                    OperationAreaId = log.OperationAreaId,
                    EffectiveTime = log.EffectiveTime,
                    Unit = log.Unit,
                    Subtype = log.Subtype,
                    IsCritical = log.IsCritical,
                    AreaName = log.AreaName,
                    LogTypeName = log.LogTypeName,
                    SubTypeName = log.SubTypeName,
                    UnitName = log.UnitName,
                });
            }
        }
    }
}
