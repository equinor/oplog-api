using Oplog.Core.Enums;

namespace Oplog.Core.Commands.Logs
{
    public class DeleteLogsResult
    {
        public string Message { get; private set; }
        public ResultType ResultType { get; private set; }

        public Dictionary<int, string> LogsNotDeleted { get; private set; }

        public DeleteLogsResult AllRequestedLogsDeleted()
        {
            ResultType = ResultType.Success;
            Message = "All requested logs deleted!";
            return this;
        }

        public DeleteLogsResult LogsDeletedWithIncompleteResults(Dictionary<int, string> logsNotDeleted)
        {
            ResultType = ResultType.Success;
            LogsNotDeleted = logsNotDeleted;
            Message = "Could not delete some or all of the logs!";
            return this;
        }
    }
}
