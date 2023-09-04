using Oplog.Core.Infrastructure;

namespace Oplog.Core.Events.Logs
{
    public class LogCreatedEvent : IEvent
    {
        public LogCreatedEvent(int logId)
        {
            LogId = logId;
        }
        public int LogId { get; set; }
    }
}
