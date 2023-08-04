using Oplog.Core.Infrastructure;
using Oplog.Persistence.Models;
using Oplog.Persistence.Repositories;

namespace Oplog.Core.Commands.Logs
{
    public class CreateLogCommandHandler : ICommandHandler<CreateLogCommand, CreateLogResult>
    {
        private readonly ILogsRepository _logsRepository;

        public CreateLogCommandHandler(ILogsRepository logsRepository)
        {
            _logsRepository = logsRepository;
        }
        public async Task<CreateLogResult> Handle(CreateLogCommand command)
        {
            var result = new CreateLogResult();
            var log = new Log
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

            await _logsRepository.Insert(log);
            await _logsRepository.Save();
            return result.LogCreated(log.Id);
        }
    }
}
