using Oplog.Core.Infrastructure;
using Oplog.Persistence.Repositories;

namespace Oplog.Core.Commands
{
    public class DeleteLogCommandHandler : ICommandHandler<DeleteLogCommand>
    {
        private readonly ILogsRepository _logsRepository;
        public DeleteLogCommandHandler(ILogsRepository logsRepository)
        {
            _logsRepository = logsRepository;
        }

        public async Task Handle(DeleteLogCommand command)
        {
            foreach (var id in command.Ids)
            {
                await _logsRepository.Delete(id);
            }

            await _logsRepository.Save();
        }
    }

}
