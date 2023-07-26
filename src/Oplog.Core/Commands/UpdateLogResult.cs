namespace Oplog.Core.Commands
{
    public class UpdateLogResult
    {
        public int LogId { get; set; }
        public string Message { get; set; }
        public ResultType ResultType { get; set; }

        public UpdateLogResult LogUpdated(int logId)
        {
            ResultType = ResultType.Success;
            Message = "Log updated";
            LogId = logId;
            return this;
        }
    }
}
