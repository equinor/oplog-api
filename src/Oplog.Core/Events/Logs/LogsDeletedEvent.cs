using Oplog.Core.Infrastructure;

namespace Oplog.Core.Events.Logs
{
    public class LogsDeletedEvent : IEvent
    {
        public LogsDeletedEvent(List<int> logIds)
        {
            LogIds = logIds;
        }

        public List<int> LogIds { get; set; }
    }
}
