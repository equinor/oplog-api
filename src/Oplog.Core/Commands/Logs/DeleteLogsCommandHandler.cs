using Oplog.Core.AzureSearch;
using Oplog.Core.Infrastructure;
using Oplog.Persistence.Models;
using Oplog.Persistence.Repositories;

namespace Oplog.Core.Commands.Logs
{
    public class DeleteLogsCommandHandler : ICommandHandler<DeleteLogsCommand, DeleteLogsResult>
    {
        private readonly ILogsRepository _logsRepository;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IIndexDocumentClient _documentClient;
        public DeleteLogsCommandHandler(ILogsRepository logsRepository, IIndexDocumentClient documentClient)
        {
            _logsRepository = logsRepository;
            _documentClient = documentClient;
        }

        public async Task<DeleteLogsResult> Handle(DeleteLogsCommand command)
        {
            var deleteLogsResult = new DeleteLogsResult();
            var logsToDelete = new List<Log>();
            foreach (var id in command.Ids)
            {
                var log = await _logsRepository.Get(id);

                if (log != null)
                {
                    logsToDelete.Add(log);
                }
            }

            _logsRepository.DeleteBulk(logsToDelete);
            await _logsRepository.Save();

            foreach (var logId in command.Ids)
            {
                await _documentClient.Delete(logId.ToString());
            }

            //Note: Cognitive search index takes time to update. Adding a delay to reflect it
            await Task.Delay(2500);

            return deleteLogsResult.AllRequestedLogsDeleted();
        }
    }

}
