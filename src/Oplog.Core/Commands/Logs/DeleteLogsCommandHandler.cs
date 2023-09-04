﻿using Oplog.Core.Events.Logs;
using Oplog.Core.Infrastructure;
using Oplog.Persistence.Models;
using Oplog.Persistence.Repositories;

namespace Oplog.Core.Commands.Logs
{
    public class DeleteLogsCommandHandler : ICommandHandler<DeleteLogsCommand, DeleteLogsResult>
    {
        private readonly ILogsRepository _logsRepository;
        private readonly IEventDispatcher _eventDispatcher;
        public DeleteLogsCommandHandler(ILogsRepository logsRepository, IEventDispatcher eventDispatcher)
        {
            _logsRepository = logsRepository;
            _eventDispatcher = eventDispatcher;
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
           
            await _eventDispatcher.RaiseEvent(new LogsDeletedEvent(logsToDelete.Select(log => log.Id).ToList()));
            if (logsNotDeleted.Any())
            {
                return deleteLogsResult.LogsDeletedWithSomeLogsNotFound(logsNotDeleted);
            }

            return deleteLogsResult.AllRequestedLogsDeleted();
        }
    }

}
