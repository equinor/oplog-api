using Oplog.Core.Infrastructure;

namespace Oplog.Core.Events.Logs
{
    public class LogUpdatedEvent : IEvent
    {
        public LogUpdatedEvent(int logId)
        {
            LogId = logId;
        }
        public int LogId { get; set; }
    }
}
