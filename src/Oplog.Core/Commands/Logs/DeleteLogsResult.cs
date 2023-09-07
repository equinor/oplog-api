using Oplog.Core.Common;

namespace Oplog.Core.Commands.Logs
{
    public class DeleteLogsResult
    {
        public string Message { get; private set; }
        public string ResultType { get; private set; }

        public Dictionary<int, string> LogsNotDeleted { get; private set; }

        public DeleteLogsResult AllRequestedLogsDeleted()
        {
            ResultType = ResultTypeConstants.Success;
            Message = "All requested logs deleted!";
            return this;
        }

        public DeleteLogsResult LogsDeletedWithSomeLogsNotFound(Dictionary<int, string> logsNotDeleted)
        {
            ResultType = ResultTypeConstants.Success;
            LogsNotDeleted = logsNotDeleted;
            Message = "Could not delete all of the logs. Some logs were not found!";
            return this;
        }
    }
}
