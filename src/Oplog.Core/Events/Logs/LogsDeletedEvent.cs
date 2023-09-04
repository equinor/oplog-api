using Oplog.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
