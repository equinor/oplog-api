using Oplog.Core.AzureSearch;
using Oplog.Core.Infrastructure;
using Oplog.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Core.Events.Logs
{
    public class LogsDeletedEventHandler : IEventHandler<LogsDeletedEvent>
    {
        private readonly IIndexDocumentClient _documentClient;       
        public LogsDeletedEventHandler( IIndexDocumentClient documentClient)
        {
            _documentClient = documentClient;
        }
        public async Task Handle(LogsDeletedEvent @event)
        {
            foreach (var logId in @event.LogIds)
            {
                await _documentClient.Delete(logId.ToString());
            }
        }
    }
}
