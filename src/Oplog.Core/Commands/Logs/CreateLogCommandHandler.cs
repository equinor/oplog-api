using Oplog.Core.Events.Logs;
using Oplog.Core.Infrastructure;
using Oplog.Persistence.Models;
using Oplog.Persistence.Repositories;

namespace Oplog.Core.Commands.Logs
{
    public class CreateLogCommandHandler : ICommandHandler<CreateLogCommand, CreateLogResult>
    {
        private readonly ILogsRepository _logsRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public CreateLogCommandHandler(ILogsRepository logsRepository, IEventDispatcher eventDispatcher)
        {
            _logsRepository = logsRepository;
            _eventDispatcher = eventDispatcher;
        }
        public async Task<CreateLogResult> Handle(CreateLogCommand command)
        {
            var result = new CreateLogResult();
            var newLog = new Log
            {
                LogTypeId = command.LogType,
                OperationAreaId = command.OperationsAreaId,
                Author = command.Author,
                Unit = command.Unit,
                Subtype = command.SubType,
                Text = command.Comment,
                EffectiveTime = command.EffectiveTime,
                CreatedBy = command.CreatedBy,
                CreatedDate = DateTime.Now,
                IsCritical = command.IsCritical
            };

            await _logsRepository.Insert(newLog);
            await _logsRepository.Save();
            await _eventDispatcher.RaiseEvent(new LogCreatedEvent(newLog.Id));

            return result.LogCreated(newLog.Id);
        }
    }
}
