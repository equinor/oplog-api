using Oplog.Core.Infrastructure;
using Oplog.Persistence.Repositories;

namespace Oplog.Core.Commands
{
    public class UpdateLogCommandHandler : ICommandHandler<UpdateLogCommand>
    {
        private readonly ILogsRepository _logsRepository;

        public UpdateLogCommandHandler(ILogsRepository logsRepository)
        {
            _logsRepository = logsRepository;
        }
        public async Task Handle(UpdateLogCommand command)
        {
            var log = await _logsRepository.Get(command.Id);

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
        }
    }
}
