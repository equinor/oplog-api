using Oplog.Core.Infrastructure;
using Oplog.Persistence.Repositories;

namespace Oplog.Core.Commands.Logs
{
    public class UpdateLogCommandHandler : ICommandHandler<UpdateLogCommand, UpdateLogResult>
    {
        private readonly ILogsRepository _logsRepository;

        public UpdateLogCommandHandler(ILogsRepository logsRepository)
        {
            _logsRepository = logsRepository;
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
            return result.LogUpdated();
        }
    }
}
