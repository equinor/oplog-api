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
            var logsNotDeleted = new Dictionary<int, string>();
            foreach (var id in command.Ids)
            {
                var log = await _logsRepository.Get(id);

                if (log == null)
                {
                    logsNotDeleted.Add(id, "Not found!");
                }
                else
                {
                    logsToDelete.Add(log);
                }
            }

            _logsRepository.DeleteBulk(logsToDelete);
            await _logsRepository.Save();

            foreach (var logId in logsToDelete.Select(log => log.Id).ToList())
            {
                await _documentClient.Delete(logId.ToString());
            }

            if (logsNotDeleted.Any())
            {
                return deleteLogsResult.LogsDeletedWithSomeLogsNotFound(logsNotDeleted);
            }

            return deleteLogsResult.AllRequestedLogsDeleted();
        }
    }

}
