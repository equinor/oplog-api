using Oplog.Core.Events.Logs;
using Oplog.Core.Infrastructure;
using Oplog.Persistence.Repositories;

namespace Oplog.Core.Commands.Logs
{
    public class UpdateLogCommandHandler : ICommandHandler<UpdateLogCommand, UpdateLogResult>
    {
        private readonly ILogsRepository _logsRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public UpdateLogCommandHandler(ILogsRepository logsRepository, IEventDispatcher eventDispatcher)
        {
            _logsRepository = logsRepository;
            _eventDispatcher = eventDispatcher;
        }
        public async Task<UpdateLogResult> Handle(UpdateLogCommand command)
        {
            var result = new UpdateLogResult();
            var log = await _logsRepository.Get(command.Id);

            if (log == null)
            {
                return result.NotFound();
            }

            log.LogTypeId = command.LogType;
            log.OperationAreaId = command.OperationsAreaId;
            log.Author = command.Author;
            log.Unit = command.Unit;
            log.Subtype = command.SubType;
            log.Text = command.Comment;
            log.EffectiveTime = command.EffectiveTime;
            log.UpdatedDate = DateTime.Now;
            log.UpdatedBy = command.UpdatedBy;
            log.IsCritical = command.IsCritical;

            _logsRepository.Update(log);
            await _logsRepository.Save();
            await _eventDispatcher.RaiseEvent(new LogUpdatedEvent(log.Id));

            return result.LogUpdated();
        }
    }
}
