using Oplog.Core.Enums;

namespace Oplog.Core.Commands.Logs
{
    public class CreateLogResult
    {
        public int LogId { get; set; }
        public string Message { get; set; }
        public ResultType ResultType { get; set; }

        public CreateLogResult LogCreated(int logId)
        {
            ResultType = ResultType.Success;
            Message = "New log created";
            LogId = logId;
            return this;
        }

        public CreateLogResult LogCreatedWithFailures(int logId)
        {
            ResultType = ResultType.Failed;
            Message = "New log created, but failed the data to index in search";
            LogId = logId;
            return this;
        }
    }
}
