using Oplog.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
