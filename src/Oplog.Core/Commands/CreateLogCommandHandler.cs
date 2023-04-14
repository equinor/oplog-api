using Oplog.Core.Infrastructure;
using Oplog.Persistence.Models;
using Oplog.Persistence.Repositories;

namespace Oplog.Core.Commands
{
    public class CreateLogCommandHandler : ICommandHandler<CreateLogCommand>
    {
        private readonly ILogsRepository _logsRepository;

        public CreateLogCommandHandler(ILogsRepository logsRepository)
        {
            _logsRepository = logsRepository;
        }
        public async Task Handle(CreateLogCommand command)
        {
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
        }
    }
}
